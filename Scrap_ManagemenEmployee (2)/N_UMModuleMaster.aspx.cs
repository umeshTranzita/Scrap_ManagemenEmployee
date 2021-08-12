using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class N_UMModuleMaster : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDB objdb = new common();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Module_ID"] = "0";

                    if (Session["UserCode"] == "" || Session["UserCode"] == null || Session["SiteId"] == null || Session["SiteId"] == "")
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
                    FillGrid();
                    lblMsg.Text = "";
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
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
            GridView1.DataSource = null;
            GridView1.DataBind();

            ds = objdb.ByProcedure("SpUMModuleMaster",
                new string[] { "flag" },
                new string[] { "1" }, "dataset");
            GridView1.DataSource = ds;
            GridView1.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            string Module_IsActive = "1";
            if (txtModule_Name.Text.Trim() == "")
            {
                msg += "Enter Module Name";
            }
            if (msg.Trim() == "")
            {
                ds = objdb.ByProcedure("SpUMModuleMaster",
                       new string[] { "flag", "Module_Name", "Module_ID" },
                       new string[] { "4", txtModule_Name.Text.Trim(), ViewState["Module_ID"].ToString() }, "dataset");


                if (btnSave.Text == "Save" && ViewState["Module_ID"].ToString() == "0" && ds.Tables[0].Rows.Count == 0)
                {
                    objdb.ByProcedure("SpUMModuleMaster",
                    new string[] { "flag", "Module_IsActive", "Module_Name", "Module_UpdatedBy","Create_By","Site_Id" },
                    new string[] { "0", Module_IsActive, txtModule_Name.Text.Trim(), ViewState["Emp_ID"].ToString(), Session["UserCode"].ToString(), Session["SiteId"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                }


                else if (btnSave.Text == "Edit" && ViewState["Module_ID"].ToString() != "0" && ds.Tables[0].Rows.Count == 0)
                {
                    objdb.ByProcedure("SpUMModuleMaster",
                    new string[] { "flag", "Module_ID", "Module_Name", "Module_UpdatedBy", "Create_By", "Site_Id" },
                    new string[] { "5", ViewState["Module_ID"].ToString(), txtModule_Name.Text.Trim(), ViewState["Emp_ID"].ToString(), Session["UserCode"].ToString(), Session["SiteId"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                }
                else
                {
                    //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Alert !", "This Module Is Already Exist.");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('This Module  Is Already Exist');", true);
                }

                txtModule_Name.Text = "";
                btnSave.Text = "Save";
                FillGrid();
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Alert !", msg);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
            CheckBox chk = (CheckBox)GridView1.Rows[selRowIndex].FindControl("chkSelect");
            string Module_ID = chk.ToolTip.ToString();
            string Module_IsActive = "0";
            if (chk != null & chk.Checked)
            {
                Module_IsActive = "1";
            }
            objdb.ByProcedure("SpUMModuleMaster",
                       new string[] { "flag", "Module_IsActive", "Module_ID", "Module_UpdatedBy", "Create_By", "Site_Id" },
                       new string[] { "6", Module_IsActive, Module_ID, ViewState["Emp_ID"].ToString(), Session["UserCode"].ToString(), Session["SiteId"].ToString() }, "dataset");
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
            ViewState["Module_ID"] = GridView1.SelectedValue.ToString();
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpUMModuleMaster",
                       new string[] { "flag", "Module_ID" },
                       new string[] { "3", ViewState["Module_ID"].ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtModule_Name.Text = ds.Tables[0].Rows[0]["Module_Name"].ToString();
                btnSave.Text = "Edit";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}