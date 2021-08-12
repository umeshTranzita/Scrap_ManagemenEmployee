using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing;
using System.Configuration;
using System.Data.SqlClient;


public partial class Frm_ReportAllLoaded : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDB objdb = new common();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Email"] == "" || Session["Email"] == null || Session["SiteId"] == null || Session["SiteId"] == "")
            {
                Response.Redirect("~/Login.aspx");
            }
            ViewState["SiteId"] = Session["SiteId"].ToString();
            ViewState["UserCode"] = Session["Email"].ToString();           
            Todate.Attributes.Add("class", "active");
            Fromdate.Attributes.Add("class", "active");
            FillFilter();
        }
    }
    protected void FillFilter()
    {
        try
        {
            ds = objdb.ByProcedure("Sp_Reports", new string[] { "flag", "SiteID" }, new string[] { "3", ViewState["SiteId"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlFilter.DataTextField = "Filter_Type";
                ddlFilter.DataValueField = "Filter_TypeValue";
                ddlFilter.DataSource = ds;
                ddlFilter.DataBind();
                ddlFilter.Items.Insert(0, new ListItem("Select Filter", "0"));
                ddlFilter.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

    }
    protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}