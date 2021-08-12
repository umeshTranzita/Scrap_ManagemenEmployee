using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;


public partial class PrintLoadBagDetails : System.Web.UI.Page
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
            ViewState["Site_Id"] = Session["SiteId"];
            ds = objdb.ByProcedure("Sp_ScrapLoad", new string[] { "flag", "SiteId", "Scrap_Id", "Refrence_No" },
                   new string[] { "21", ViewState["Site_Id"].ToString(), ViewState["Scrap_Id"].ToString(), ViewState["Refrence_No"].ToString() }, "dataset");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                LblRefrence.Text = ds.Tables[0].Rows[0]["Refrence_No"].ToString();
                Repeater1.DataSource = ds.Tables[0];
                Repeater1.DataBind();
            }
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount == 12)
            {
                trBlank.Visible = false;
            }
            else
            {
                int heighttd = 12 - rowcount;
                heighttd = heighttd * 17;
                //tdBlank.Attributes.Add("style", "height:" + height.ToString());
                testSpace.Style.Add("height", heighttd.ToString() + "px");
            }
        }
    }
}