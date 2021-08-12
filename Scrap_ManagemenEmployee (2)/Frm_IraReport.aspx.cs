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

public partial class Admin_Frm_IraReport : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDB objdb = new common();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillSite();
            Fromdate.Attributes.Add("class", "active");
            Todate.Attributes.Add("class", "active");
        }
    }
    protected void FillSite()
    {
        try
        {
            if (Session["Email"] == "" || Session["Email"] == null || Session["SiteId"] == null || Session["SiteId"] == "")
            {
                Response.Redirect("~/Login.aspx");

            }
            ViewState["SiteId"] = Session["SiteId"].ToString();
            ViewState["UserCode"] = Session["Email"].ToString();
            ds = objdb.ByProcedure("Sp_SiteMaster", new string[] { "flag" }, new string[] { "5" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlSite.DataTextField = "SiteName";
                ddlSite.DataValueField = "SiteID";
                ddlSite.DataSource = ds;
                ddlSite.DataBind();
                ddlSite.Items.Insert(0, new ListItem("Select Site", "0"));
                ddlSite.SelectedIndex = 0;
                ddlSite.SelectedValue = ViewState["SiteId"].ToString();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        ds = null;
        //LblErrorMsg.Text = "";
        lblMsg.Text = "";
        ReportViewer1.LocalReport.DataSources.Clear();
        ds = objdb.ByProcedure("Sp_Reports", new string[] { "flag", "SiteId", "FromDate", "ToDate" },
           new string[] { "15", ddlSite.SelectedValue, Convert.ToDateTime(TxtFromdate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(TxtTodate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/IRAREPORT.rdlc");
                    //CODE CHANGES STARTED BY CHINMAY ON 10-JUN-2019
                    ReportViewer1.LocalReport.DisplayName = ddlSite.SelectedItem.Text;
                    //CODE CHANGES ENDED BY CHINMAY ON 10-JUN-2019
                    ReportDataSource datasource = new ReportDataSource("DataSetForIssue", ds.Tables[0]);
                    ReportDataSource datasource1 = new ReportDataSource("DataSetForProcess", ds.Tables[1]);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                    ReportViewer1.LocalReport.DataSources.Add(datasource1);
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "No Record Foud");
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "No Record Foud");
            }
        }
        else
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "No Record Foud");
        }



    }
}