using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI;


public partial class Frm_ConsigneeReg : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDB objdb = new common();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["Email"] == "" || Session["Email"] == null || Session["SiteId"] == null || Session["SiteId"] == "")
                {

                    Response.Redirect("~/Login.aspx");
                }
                ViewState["Email"] = Session["Email"].ToString();
                ViewState["SiteId"] = Session["SiteId"].ToString();
                FillCategory();
                Fillgrid();
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
            lblMsg.Text = "";
            string msg = "";


            if (TxtConsignee.Text.Trim() == "")
            {
                msg += "Enter Site Name.\\n";
            }
            if (TxtConsigneeAddress.Text.Trim() == "")
            {
                msg += "Enter Site Description.\\n";
            }
            if (TxtConsigneeGstNo.Text.Trim() == "")
            {
                msg += "Enter Site Address.\\n";
            }
            if (TxtContactNo.Text.Trim() == "")
            {
                msg += "Enter Contact No.\\n";
            }
            if (ddlScrapCategory.SelectedIndex == 0)
            {
                msg += "Select Scrap Category.\\n";
            }

            if (msg == "")
            {
                string IsActive = "No";
                if (chkIsActive.Checked == true)
                {
                    IsActive = "Yes";
                }
                if (btnSubmit.Text == "SUBMIT")
                {

                    ds = objdb.ByProcedure("Sp_ConsignneMaster",
                           new string[] { "flag", "ConsigneeName", "ConsigneeGst", "ConsigneeAddress", "ContactNo", "IsActive", "ScrapCategoryId", "Site_Id" },
                           new string[] { "1", TxtConsignee.Text, TxtConsigneeGstNo.Text, TxtConsigneeAddress.Text, TxtContactNo.Text, IsActive.ToString(), ddlScrapCategory.SelectedValue, ViewState["SiteId"].ToString() }, "dataset");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Submitted Successfully");
                          
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["Msg"].ToString());

                        }
                    }
                }
                if (btnSubmit.Text == "MODIFY")
                {
                    ds = objdb.ByProcedure("Sp_ConsignneMaster",
                           new string[] { "flag", "ConsigneeName", "ConsigneeGst", "ConsigneeAddress", "ContactNo", "IsActive", "ScrapCategoryId", "Site_Id","ConsigneeId" },
                           new string[] { "4", TxtConsignee.Text, TxtConsigneeGstNo.Text, TxtConsigneeAddress.Text, TxtContactNo.Text, IsActive.ToString(), ddlScrapCategory.SelectedValue, ViewState["SiteId"].ToString(), ViewState["ConsigneeId"].ToString() }, "dataset");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Update Successfully");

                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["Msg"].ToString());

                        }
                    }

                }
                Fillgrid();
                clear();
                btnSubmit.Text = "SUBMIT";
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillCategory()
    {
        try
        {
            ds = objdb.ByProcedure("Sp_ScrapType", new string[] { "flag", "Site_id" }, new string[] { "6", ViewState["SiteId"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlScrapCategory.DataTextField = "ScrapCategory";
                ddlScrapCategory.DataValueField = "ScrapCategoryId";
                ddlScrapCategory.DataSource = ds;
                ddlScrapCategory.DataBind();
                ddlScrapCategory.Items.Insert(0, new ListItem("Select Category", "0"));
                //ddlScrapCategory.SelectedValue = ViewState["SiteId"].ToString();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    public void Fillgrid()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("Sp_ConsignneMaster", new string[] { "flag", "Site_id" }, 
                new string[] { "2", ViewState["SiteId"].ToString() }, "dataset");
            if (ds!=null && ds.Tables[0].Rows.Count != 0 && ds.Tables.Count > 0)
            {
                GridConsignee.DataSource = ds.Tables[0];
                GridConsignee.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void GridConsignee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ds = null;
            string IsActive = "No";
            ViewState["ConsigneeId"] = GridConsignee.SelectedDataKey.Value;
            ds = objdb.ByProcedure("Sp_ConsignneMaster", new string[] { "flag", "Site_Id", "ConsigneeId" }, new string[] { "3", ViewState["SiteId"].ToString(), ViewState["ConsigneeId"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count != 0 && ds.Tables.Count > 0)
            {
                TxtConsignee.Text = ds.Tables[0].Rows[0]["ConsigneeName"].ToString();
                TxtConsigneeAddress.Text = ds.Tables[0].Rows[0]["ConsigneeAddress"].ToString();
                TxtConsigneeGstNo.Text = ds.Tables[0].Rows[0]["ConsigneeGst"].ToString();
                TxtContactNo.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
                ddlScrapCategory.SelectedValue = ds.Tables[0].Rows[0]["ScrapCategoryId"].ToString();
                IsActive = ds.Tables[0].Rows[0]["IsActive"].ToString();

                if (IsActive == "Yes")
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
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    public void clear()
    {
        TxtConsignee.Text = "";
        TxtConsigneeAddress.Text = "";
        TxtConsigneeGstNo.Text = "";
        TxtContactNo.Text = "";
    }
}