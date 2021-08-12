using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI;


public partial class Frm_ContractorMaster : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDB objdb = new common();
    Kiosk obj_kiosk = new Kiosk_common();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lblMsg.Text = "";

                lblUserID.Text = "1";
                FillSite();

                ViewState["SiteId"] = Session["SiteId"];
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Create_By"] = Session["Email"];
                ViewState["Role_ID"] = Session["Role_ID"];
                FillGrid();
                FillRole();
                ContractorName.Attributes.Add("class", "active");
                ContractorCode.Attributes.Add("class", "active");
                ContractorMob.Attributes.Add("class", "active");
                VendorName.Attributes.Add("class", "active");
                VendorMob.Attributes.Add("class", "active");

                if (ViewState["Role_ID"].ToString() == "1" || ViewState["Role_ID"].ToString() == "2")
                {
                    //ddlSite.Attributes.Add("disabled", "disabled");
                }
                else
                {
                    ddlSite.Attributes.Add("disabled", "disabled");
                }

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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
        try
        {
            lblMsg.Text = "";
            string msg = "";

            if (txtContractorName.Text.Trim() == "")
            {
                msg += "Enter Contractor Name.\\n";
            }
            if (txtContractorCode.Text.Trim() == "")
            {
                msg += "Enter Contractor Code.\\n";
            }
            //if (TxtContractorMobNo.Text.Trim() == "")
            //{
            //    msg += "Enter Contractor Mobile No.\\n";
            //}
            if (TxtVendorName.Text.Trim() == "")
            {
                msg += "Enter Vendor Name.\\n";
            }
            //if (TxtVendorContactNo.Text.Trim() == "")
            //{
            //    msg += "Enter Vendor Contact No.\\n";
            //}
            if (ddlRole.SelectedIndex == 0)
            {
                msg += "Please Select Role.\\n";
            }
            if (msg == "")
            {
                string IsActive = "No";
                if (chkIsActive.Checked == true)
                {
                    IsActive = "Yes";
                }
                if (ddlSite.SelectedValue  == "1")
                {
                    DataSet Kiosk = null;
                    Kiosk = obj_kiosk.GetDatabase("spContractor", new string[] { "Flag", "Contractor_Code" }, new string[] { "40", txtContractorCode.Text });

                    if (Kiosk.Tables[0].Rows.Count != 0)
                    {
                        if (Kiosk.Tables[0].Rows[0]["Msg"].ToString() != "OK")
                        {

                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Kiosk.Tables[0].Rows[0]["Msg"].ToString());
                            return;
                        }
                    }
                }
                if (btnSubmit.Text == "SUBMIT")
                {
                    

                    ds = objdb.ByProcedure("Sp_ContractorMaster",
                           new string[] { "flag", "Contractor_Name", "Contractor_Code", "Contractor_MobileNo", "Vendor_Name", "Vendor_ContactNo", "SiteId", "IsActive", "Create_By", "Role_Id","EmpId" },
                           new string[] { "1", txtContractorName.Text, txtContractorCode.Text, TxtContractorMobNo.Text, TxtVendorName.Text, TxtVendorContactNo.Text, ddlSite.SelectedValue, IsActive.ToString(), ViewState["Create_By"].ToString(), ddlRole.SelectedValue, txtContractorCode.Text }, "dataset");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Submitted Successfully");
                            ClearData();
                            FillGrid();
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["Msg"].ToString());
                        
                        }
                    }
                }
                if (btnSubmit.Text == "MODIFY")
                {
                    ds = objdb.ByProcedure("Sp_ContractorMaster",
                          new string[] { "flag", "Contractor_Name", "Contractor_Code", "Contractor_MobileNo", "Vendor_Name", "Vendor_ContactNo", "SiteId", "IsActive", "Create_By","Id","Role_Id" },
                          new string[] { "4", txtContractorName.Text, txtContractorCode.Text, TxtContractorMobNo.Text, TxtVendorName.Text, TxtVendorContactNo.Text, ddlSite.SelectedValue, IsActive.ToString(), ViewState["Create_By"].ToString(), ViewState["Id"].ToString(),ddlRole.SelectedValue }, "dataset");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Updated Successfully");
                            ClearData();
                            FillGrid();
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["Msg"].ToString());

                        }
                    }
                }
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

    protected void ClearData()
    {
        txtContractorName.Text = "";
        TxtContractorMobNo.Text = "";
        txtContractorCode.Text = "";
        TxtVendorContactNo.Text = "";
        TxtVendorName.Text = "";
        btnSubmit.Text = "SUBMIT";
        ddlRole.SelectedIndex = 0;
    }

    protected void FillGrid()
    {
        try
        {


            ds = objdb.ByProcedure("Sp_ContractorMaster", new string[] { "flag", "SiteID" }, new string[] { "2", ddlSite.SelectedValue }, "dataset");
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
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            string IsActive = "No";
            ViewState["Id"] = GridView1.SelectedDataKey.Value;
            ds = objdb.ByProcedure("Sp_ContractorMaster", new string[] { "flag", "Id" }, new string[] { "3", ViewState["Id"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                txtContractorName.Text = ds.Tables[0].Rows[0]["Contractor_Name"].ToString();
                txtContractorCode.Text = ds.Tables[0].Rows[0]["Contractor_Code"].ToString();
                TxtContractorMobNo.Text = ds.Tables[0].Rows[0]["Contractor_MobileNo"].ToString();
                TxtVendorName.Text = ds.Tables[0].Rows[0]["Vendor_Name"].ToString();
                TxtVendorContactNo.Text = ds.Tables[0].Rows[0]["Vendor_ContactNo"].ToString();
                IsActive = ds.Tables[0].Rows[0]["IsActive"].ToString();
                ddlRole.SelectedValue = ds.Tables[0].Rows[0]["Role_Id"].ToString();
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
    protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    protected void FillRole()
    {
        try
        {
            ds = objdb.ByProcedure("SpEmployeeRoleMap", new string[] { "flag", "LoginUserId" }, new string[] { "4", ViewState["Emp_ID"].ToString()}, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlRole.DataTextField = "Role_Name";
                ddlRole.DataValueField = "Role_ID";
                ddlRole.DataSource = ds;
                ddlRole.DataBind();
                ddlRole.Items.Insert(0, new ListItem("Select Role", "0"));
                ddlRole.SelectedIndex = 0;
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
    }
}