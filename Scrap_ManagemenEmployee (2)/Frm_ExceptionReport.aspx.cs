using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frm_ExceptionReport : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDB objdb = new common();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {        
            Fromdate.Attributes.Add("class", "active");
            Todate.Attributes.Add("class", "active");
            
        }
    }
    public void GetData()
    {

        try
        {
            GridData.DataSource = null;
            GridData.DataBind();
            ds = objdb.ByProcedure("Sp_Reports", new string[] { "flag", "FromDate", "ToDate" },
             new string[] { "19", Convert.ToDateTime(TxtFromdate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(TxtTodate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridData.DataSource = ds.Tables[0];
                        GridData.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int rowIndex = GridData.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow currentRow = GridData.Rows[rowIndex];
            GridViewRow previousRow = GridData.Rows[rowIndex + 1];
            string[] arra1 = currentRow.Cells[1].Text.Split(' ');
            string[] arra2 = previousRow.Cells[1].Text.Split(' ');
            currentRow.Cells[1].Text = arra1[0];
            previousRow.Cells[1].Text = arra2[0];
            if (currentRow.Cells[1].Text == previousRow.Cells[1].Text)
            {
                if (previousRow.Cells[1].RowSpan < 2)
                {
                    currentRow.Cells[1].RowSpan = 2;
                }
                else
                {
                    currentRow.Cells[1].RowSpan = previousRow.Cells[1].RowSpan + 1;
                }
                previousRow.Cells[1].Visible = false;
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        GetData();
    }
}