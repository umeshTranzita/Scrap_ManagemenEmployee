using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

public partial class Frm_PostProcessSetLossPer : System.Web.UI.Page
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
            FillScrapType();
            FillGrid();

        }
    }
    protected void FillScrapType()
    {
        try
        {
            ds = objdb.ByProcedure("Sp_LossPercentage", new string[] { "flag" }, new string[] { "3" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlScrapType.DataTextField = "Scrap_Type";
                ddlScrapType.DataValueField = "Scrap_TypeId";
                ddlScrapType.DataSource = ds;
                ddlScrapType.DataBind();
                ddlScrapType.Items.Insert(0, new ListItem("Select Scrap Type", "0"));
                ddlScrapType.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int IsActive = 0;
            if (chkIsActive.Checked == true)
            {
                IsActive = 1;
            }

            if (double.Parse(TxtLossPercentage.Text) < 1)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Percentage value cannot be less than 1");
                return;
            }

            if (btnSubmit.Text == "SUBMIT")
            { 

            ds =  objdb.ByProcedure("Sp_LossPercentage", new string[] { "flag", "Scarp_typeId", "Loss_Percentage", "IsActive", "Create_by" },
                new string[] { "1", ddlScrapType.SelectedValue, TxtLossPercentage.Text, IsActive.ToString(), ViewState["UserCode"].ToString() }, "dataset");
               
            }
            else
            {
            ds =  objdb.ByProcedure("Sp_LossPercentage", new string[] { "flag", "Scarp_typeId", "Loss_Percentage", "IsActive", "Update_by", "Id" },
              new string[] { "5", ddlScrapType.SelectedValue, TxtLossPercentage.Text, IsActive.ToString(), ViewState["UserCode"].ToString(), ViewState["Id"].ToString() }, "dataset");
            }
            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
            {
                FillGrid();
                btnSubmit.Text = "SUBMIT";
                ddlScrapType.SelectedIndex = 0;
                TxtLossPercentage.Text = "0";
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Successfully Submitted");

            }
            else
            {

                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["Msg"].ToString());
            }
           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGrid()
    {
        try
        {
            ds = objdb.ByProcedure("Sp_LossPercentage", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        GridView1.DataSource = ds.Tables[0];
                        GridView1.DataBind();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "Select")
            {
                ViewState["Id"] = Convert.ToInt32(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                GetData();

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetData()
    {
        try
        {
            ds = objdb.ByProcedure("Sp_LossPercentage", new string[] { "flag","Id"}, new string[] { "4",ViewState["Id"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        ddlScrapType.SelectedValue = ds.Tables[0].Rows[0]["Scarp_typeId"].ToString();
                        TxtLossPercentage.Text = ds.Tables[0].Rows[0]["Loss_Percentage"].ToString();
                        if (ds.Tables[0].Rows[0]["IsActive"].ToString() == "1")
                        {
                            chkIsActive.Checked = true;
                        }
                        else
                        {
                            chkIsActive.Checked = false;

                        }
                        btnSubmit.Text = "MODIFY";
                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}