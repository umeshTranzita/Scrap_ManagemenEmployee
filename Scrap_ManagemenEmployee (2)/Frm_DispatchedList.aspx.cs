using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Text;


public partial class Frm_DispatchedList : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDB objdb = new common();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserCode"] == "" || Session["UserCode"] == null || Session[""] == null || Session["SiteId"] == "")
            {
                if (Session["Email"] == "" || Session["Email"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    Session["UserCode"] = Session["Email"];
                }

            }
            ViewState["SiteId"] = Session["SiteId"].ToString();
            ViewState["UserCode"] = Session["UserCode"].ToString();
            GetData();
            
        }
    }
    protected void GetData()
    {
        try
        {
            ds = null;

            ds = objdb.ByProcedure("Sp_ScrapLoad", new string[] { "flag","Site_Id" },
                new string[] { "10", ViewState["SiteId"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        GridDispatch.DataSource = ds.Tables[0];
                        GridDispatch.DataBind();

                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void LnkPrint_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = sender as LinkButton;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            Session["Scrap_Id"] = GridDispatch.DataKeys[row.RowIndex].Values[0].ToString();
            //GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            string RefrenceNo = row.Cells[1].Text;
            Session["Refrence_No"] = RefrenceNo.ToString();

            string url = "Print.aspx";
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.open('");
            sb.Append(url);
            sb.Append("');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        
    }
    protected void LnkPrint1_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = sender as LinkButton;
            GridViewRow row = btn.NamingContainer as GridViewRow;
            Session["Scrap_Id"] = GridDispatch.DataKeys[row.RowIndex].Values[0].ToString();
            //GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            string RefrenceNo = row.Cells[1].Text;
            Session["Refrence_No"] = RefrenceNo.ToString();

            string url = "PrintLoadBagDetails.aspx";
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.open('");
            sb.Append(url);
            sb.Append("');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        
    }
}