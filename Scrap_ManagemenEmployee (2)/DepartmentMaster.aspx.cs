using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class Admin_DepartmentMaster : System.Web.UI.Page
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
                FillSite();
                //lblUserID.Text = cookie["UserID"].ToString();
                lblUserID.Text = Session["Email"].ToString();
                FillGrid();
                DepartName.Attributes.Add("class", "active");
                DepartCode.Attributes.Add("class", "active");
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("DepartmentMaster.aspx");
    }
    protected void FillGrid()
    {
        try
        {
            // lblMsg.Text = "";
            txtDepartmentName.Text = "";
            txtDepartmentCode.Text = "";

            ds = objdb.ByProcedure("Sp_ProjectMaster", new string[] { "flag", "SiteID" }, new string[] { "1", ddlSite.SelectedValue }, "dataset");
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
                ddlSite.SelectedIndex = 0;
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
            if (ddlSite.SelectedIndex == 0)
            {
                msg += "Select Site Name.\\n";
            }
            if (txtDepartmentName.Text.Trim() == "")
            {
                msg += "Enter Department Name.\\n";
            }
            if (txtDepartmentCode.Text.Trim() == "")
            {
                msg += "Enter Department Code.\\n";
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

                    ds = objdb.ByProcedure("Sp_ProjectMaster",
                           new string[] { "flag", "SiteID", "DepartmentName", "DepartmentCode" },
                           new string[] { "5", ddlSite.SelectedValue, txtDepartmentName.Text.Trim(), txtDepartmentCode.Text.Trim() }, "dataset");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-danger", "Sorry!", "Department Name already exist");
                    }

                    else if (ds.Tables[1].Rows.Count > 0)
                    {
                        lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-danger", "Sorry!", "Department Code already exist");
                    }

                    else
                    {
                        objdb.ByProcedure("Sp_ProjectMaster",
                                 new string[] { "flag", "SiteName", "DepartmentName", "DepartmentCode", "UserID", "IsActive" },
                                 new string[] { "0", ddlSite.SelectedValue.ToString(), txtDepartmentName.Text.Trim(), txtDepartmentCode.Text.Trim(), lblUserID.Text, IsActive }, "dataset");
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Successfully Submitted");
                        ClearData();
                        FillGrid();
                    }
                }
                if (btnSubmit.Text == "MODIFY")
                {
                    DataSet ds1 = objdb.ByProcedure("Sp_ProjectMaster",
                        new string[] { "flag", "SiteID", "DepartmentID", "DepartmentName", "DepartmentCode" },
                        new string[] { "6", ddlSite.SelectedValue, ViewState["DepartmentID"].ToString(), txtDepartmentName.Text.Trim(), txtDepartmentCode.Text.Trim() }, "dataset");

                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-danger", "Sorry!", "Department Name already exist");
                    }

                    else if (ds1.Tables[1].Rows.Count > 0)
                    {
                        lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-danger", "Sorry!", "Department Code already exist");
                    }
                    else
                    {
                        objdb.ByProcedure("Sp_ProjectMaster",
                                 new string[] { "flag", "DepartmentID", "SiteName", "DepartmentName", "DepartmentCode", "UserID", "IsActive" },
                                 new string[] { "2", ViewState["DepartmentID"].ToString(), ddlSite.SelectedValue.ToString(), txtDepartmentName.Text.Trim(), txtDepartmentCode.Text.Trim(), lblUserID.Text, IsActive }, "dataset");
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Updarted Successfully");
                        ClearData();
                        FillGrid();
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
    protected void ClearData()
    {
        txtDepartmentName.Text = "";
        //txtDescription.Text = "";
        txtDepartmentCode.Text = "";
        ddlSite.ClearSelection();
        btnSubmit.Text = "SUBMIT";
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            string IsActive = "No";
            ViewState["DepartmentID"] = GridView1.SelectedDataKey.Value;
            ds = objdb.ByProcedure("Sp_ProjectMaster", new string[] { "flag", "DepartmentID" }, new string[] { "4", ViewState["DepartmentID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlSite.SelectedValue = ds.Tables[0].Rows[0]["SiteID"].ToString();
                txtDepartmentCode.Text = ds.Tables[0].Rows[0]["DepartmentCode"].ToString();
                txtDepartmentName.Text = ds.Tables[0].Rows[0]["DepartmentName"].ToString();
                DepartName.Attributes.Add("class", "active");
                DepartCode.Attributes.Add("class", "active");
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
}