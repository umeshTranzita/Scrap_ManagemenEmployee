using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI;
public partial class Frm_ScrapPrint : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDB objdb = new common();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Scrap_Id"] == "" || Session["Scrap_Id"] == null || Session["Refrence_No"] == null || Session["Refrence_No"] == "")
            {
                Response.Redirect("~/Login.aspx");
            }
            ViewState["Scrap_Id"] = Session["Scrap_Id"];
            ViewState["Refrence_No"] = Session["Refrence_No"];
            GetData();
        }
    }
    protected void GetData()
    {
        try
        {
            ds = null;

            ds = objdb.ByProcedure("Sp_ScrapLoad", new string[] { "flag", "Scrap_Id", "Refrence_No" },
                new string[] { "11", ViewState["Scrap_Id"].ToString(), ViewState["Refrence_No"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        GridItem.DataSource = ds.Tables[0];
                        GridItem.DataBind();

                        double TotalWeight = 0;

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {

                            TotalWeight = TotalWeight + double.Parse(ds.Tables[0].Rows[i]["Bag_Weight"].ToString());
                        }
                        GridItem.FooterRow.Cells[0].Font.Size = 12;                    
                        GridItem.FooterRow.Cells[4].Font.Size = 12;
                        GridItem.FooterRow.Cells[0].Text = "TOTAL WEIGHT";
                        GridItem.FooterRow.Cells[4].Text = TotalWeight.ToString();

                    }
                }
            }

        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}