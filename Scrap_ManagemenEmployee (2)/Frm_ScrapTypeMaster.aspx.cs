using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class Frm_ScrapTypeMaster : System.Web.UI.Page
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
                lblMsg.Text = "";
               
                ViewState["SiteId"] = Session["SiteId"].ToString();
                ViewState["Email"] = Session["Email"].ToString();
                FillSite();
                FillGrid();
                FillCategory();
                FillUnit();
                ScrapType.Attributes.Add("class", "active");
                ScrapCode.Attributes.Add("class", "active");
                Description.Attributes.Add("class", "active");
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

            if (txtScrapType.Text.Trim() == "")
            {
                msg += "Enter Scrap Type.\\n";
            }
            if (txtScrapCode.Text.Trim() == "")
            {
                msg += "Enter Scrap Code.\\n";
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

                    ds = objdb.ByProcedure("Sp_ScrapType",
                           new string[] { "flag", "Scrap_Type", "Scrap_code", "Scrap_Description", "IsActive", "Create_By","Site_Id","ScrapCategoryId","Scrap_Category","UnitId" },
                           new string[] { "1", txtScrapType.Text, txtScrapCode.Text, TxtScrpDescription.Text, IsActive.ToString(), ViewState["Email"].ToString(), ddlSite.SelectedValue,ddlScrapCategory.SelectedValue,ddlScrapCategory.SelectedItem.Text,ddlUnit.SelectedValue }, "dataset");

                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Successfully Submitted");
                        ClearData();
                        FillGrid();
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["Msg"].ToString());
                    }


                }
                if (btnSubmit.Text == "MODIFY")
                {
                    ds = objdb.ByProcedure("Sp_ScrapType",
                             new string[] { "flag", "Scrap_Type", "Scrap_code", "Scrap_Description", "IsActive", "Create_By", "Scrap_TypeId", "Site_Id", "ScrapCategoryId", "Scrap_Category", "UnitId" },
                             new string[] { "4", txtScrapType.Text, txtScrapCode.Text, TxtScrpDescription.Text, IsActive.ToString(), ViewState["Email"].ToString(), ViewState["Scrap_TypeId"].ToString(), ddlSite.SelectedValue, ddlScrapCategory.SelectedValue, ddlScrapCategory.SelectedItem.Text,ddlUnit.SelectedValue }, "dataset");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {

                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Updated Successfully");
                            ClearData();
                            FillGrid();
                            btnSubmit.Text = "SUBMIT";

                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["Msg"].ToString());
                        }
                    }
                    





                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Cal lMyFunction", "alert('" + msg + "');", true);
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

            ds = objdb.ByProcedure("Sp_ScrapType", new string[] { "flag", "Site_id" }, new string[] { "2", ddlSite.SelectedValue }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            string IsActive = "No";
            ViewState["Scrap_TypeId"] = GridView1.SelectedDataKey.Value;
            ds = objdb.ByProcedure("Sp_ScrapType", new string[] { "flag", "Scrap_TypeId" }, new string[] { "3", ViewState["Scrap_TypeId"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {

                txtScrapType.Text = ds.Tables[0].Rows[0]["Scrap_Type"].ToString();
                txtScrapCode.Text = ds.Tables[0].Rows[0]["Scrap_code"].ToString();
                TxtScrpDescription.Text = ds.Tables[0].Rows[0]["Scrap_Description"].ToString();
                ddlScrapCategory.SelectedValue = ds.Tables[0].Rows[0]["ScrapCategoryId"].ToString();
                ddlUnit.SelectedValue = ds.Tables[0].Rows[0]["UnitId"].ToString();
                ScrapType.Attributes.Add("class", "active");
                ScrapCode.Attributes.Add("class", "active");
                Description.Attributes.Add("class", "active");
                //txtDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
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
    protected void ClearData()
    {
        txtScrapType.Text = "";
        TxtScrpDescription.Text = "";
        txtScrapCode.Text = "";
        ddlUnit.SelectedIndex = 0;
        btnSubmit.Text = "SUBMIT";
    }
    protected void FillSite()
    {
        try
        {
            ds = objdb.ByProcedure("Sp_SiteMaster", new string[] { "flag" }, new string[] { "5" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlSite.DataTextField = "SiteName";
                ddlSite.DataValueField = "SiteID";
                ddlSite.DataSource = ds;
                ddlSite.DataBind();
                ddlSite.Items.Insert(0, new ListItem("Select Site", "0"));
                ddlSite.SelectedValue = ViewState["SiteId"].ToString();
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
            ds = objdb.ByProcedure("Sp_ScrapType", new string[] { "flag", "Site_id" }, new string[] { "6", ddlSite.SelectedValue }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlScrapCategory.DataTextField = "ScrapCategory";
                ddlScrapCategory.DataValueField = "ScrapCategoryId";
                ddlScrapCategory.DataSource = ds;
                ddlScrapCategory.DataBind();
                ddlScrapCategory.Items.Insert(0, new ListItem("Select Category", "0"));
                ddlScrapCategory.SelectedValue = ViewState["SiteId"].ToString();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillUnit()
    {
        try
        {
            ds = objdb.ByProcedure("Sp_UnitMaster", new string[] { "flag", "Site_id" }, new string[] { "5", ddlSite.SelectedValue }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlUnit.DataTextField = "UnitName";
                ddlUnit.DataValueField = "UnitId";
                ddlUnit.DataSource = ds;
                ddlUnit.DataBind();
                ddlUnit.Items.Insert(0, new ListItem("Select Unit", "0"));
                //ddlUnit.SelectedValue = ViewState["SiteId"].ToString();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
        FillCategory();
    }
    protected void ddlScrapType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}