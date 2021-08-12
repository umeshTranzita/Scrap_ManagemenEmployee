﻿using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class N_UMFormMaster : System.Web.UI.Page
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
                    ViewState["Form_ID"] = "0";
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
                    FillModule();
                    FillGrid();
                    lblMsg.Text = "";
                    lblRecord.Text = "";
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

    protected void FillModule()
    {
        try
        {
            DataSet dsM = objdb.ByProcedure("SpUMModuleMaster",
                 new string[] { "flag" },
                 new string[] { "2" }, "dataset");

            if (dsM != null && dsM.Tables[0].Rows.Count > 0)
            {
                ddlModule_Name.DataSource = dsM;
                ddlModule_Name.DataTextField = "Module_Name";
                ddlModule_Name.DataValueField = "Module_ID";
                ddlModule_Name.DataBind();
                ddlModule_Name.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {

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

            ds = objdb.ByProcedure("SpUMFormMaster", new string[] { "flag" }, new string[] { "6" }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblRecord.Text = "";
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else if (ds.Tables[0].Rows.Count == 0)
            {
                lblRecord.Text = "";
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                lblRecord.Text = "";
                GridView1.DataSource = null;
                GridView1.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FG_AfterInsertion()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

            ds = objdb.ByProcedure("SpUMFormMaster", new string[] { "flag", "Module_ID" }, new string[] { "9", ddlModule_Name.SelectedValue }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblRecord.Text = "";
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else if (ds.Tables[0].Rows.Count == 0)
            {
                lblRecord.Text = "";
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                lblRecord.Text = "";
                GridView1.DataSource = null;
                GridView1.DataBind();
            }

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
            string Form_IsActive = "1";
            if (ddlModule_Name.SelectedIndex == 0)
            {
                msg += "Select Module Name<br/>";
            }
            if (txtForm_Name.Text.Trim() == "")
            {
                msg += "Enter Form Name<br/>";
            }
            if (txtOrderBy.Text.Trim() == "")
            {
                msg += "Enter Order By<br/>";
            }
            if (txtForm_Path.Text.Trim() == "")
            {
                msg += "Enter Form Path";
            }
            if (msg.Trim() == "")
            {
                ds = objdb.ByProcedure("SpUMFormMaster",
                      new string[] { "flag", "Form_Name", "Form_ID" },
                      new string[] { "4", txtForm_Name.Text.Trim(), ViewState["Form_ID"].ToString() }, "dataset");

                if (btnSave.Text == "Save" && ViewState["Form_ID"].ToString() == "0" && ds.Tables[0].Rows.Count == 0)
                {
                    objdb.ByProcedure("SpUMFormMaster",
                    new string[] { "flag", "Form_IsActive", "Form_Name", "Module_ID", "Form_Path", "OrderBy", "Form_UpdatedBy", "Create_By", "Site_Id" },
                    new string[] { "0", Form_IsActive, txtForm_Name.Text.Trim(), ddlModule_Name.SelectedValue, txtForm_Path.Text.Trim(), txtOrderBy.Text.Trim(), ViewState["Emp_ID"].ToString(), Session["UserCode"].ToString(), Session["SiteId"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");

                }

                else if (btnSave.Text == "Edit" && ViewState["Form_ID"].ToString() != "0" && ds.Tables[0].Rows.Count == 0)
                {
                    objdb.ByProcedure("SpUMFormMaster",
                    new string[] { "flag", "Form_ID", "Form_Name", "Module_ID", "Form_Path", "OrderBy", "Form_UpdatedBy", "Create_By", "Site_Id" },
                    new string[] { "7", ViewState["Form_ID"].ToString(), txtForm_Name.Text.Trim(), ddlModule_Name.SelectedValue, txtForm_Path.Text.Trim(), txtOrderBy.Text.Trim(), ViewState["Emp_ID"].ToString(), Session["UserCode"].ToString(), Session["SiteId"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ViewState["Form_ID"] = 0;
                }
                else
                {
                    string Form_Name = ds.Tables[0].Rows[0]["Form_Name"].ToString();
                    string OrderBy = ds.Tables[0].Rows[0]["OrderBy"].ToString();
                    if (Form_Name == txtForm_Name.Text)
                    {
                        // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Alert !", "This Form Name Is Already Exist.");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('This Form Name Is Already Exist');", true);
                    }
                    if (OrderBy == txtOrderBy.Text)
                    {
                        // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Alert !", "Order Number Is Already Exist.");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Order Number Is Already Exist');", true);
                    }

                }

                ClearField();
                btnSave.Text = "Save";
                FG_AfterInsertion();
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
            string Form_ID = chk.ToolTip.ToString();
            string Form_IsActive = "0";
            if (chk != null & chk.Checked)
            {
                Form_IsActive = "1";
            }
            objdb.ByProcedure("SpUMFormMaster",
                       new string[] { "flag", "Form_IsActive", "Form_ID", "Form_UpdatedBy", "Create_By", "Site_Id" },
                       new string[] { "8", Form_IsActive, Form_ID, ViewState["Emp_ID"].ToString(), Session["UserCode"].ToString(), Session["SiteId"].ToString() }, "dataset");
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
            ViewState["Form_ID"] = GridView1.SelectedValue.ToString();
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpUMFormMaster",
                       new string[] { "flag", "Form_ID" },
                       new string[] { "3", ViewState["Form_ID"].ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlModule_Name.ClearSelection();
                ddlModule_Name.Items.FindByValue(ds.Tables[0].Rows[0]["Module_ID"].ToString()).Selected = true;
                txtForm_Name.Text = ds.Tables[0].Rows[0]["Form_Name"].ToString();
                txtForm_Path.Text = ds.Tables[0].Rows[0]["Form_Path"].ToString();
                txtOrderBy.Text = ds.Tables[0].Rows[0]["OrderBy"].ToString();
                btnSave.Text = "Edit";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearField()
    {
        // ddlModule_Name.ClearSelection();
        txtForm_Name.Text = "";
        txtForm_Path.Text = "";
        txtOrderBy.Text = "";
    }
    protected void ddlModule_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            

            if (btnSave.Text != "Edit")
            {
                lblRecord.Text = "";
                txtForm_Name.Text = "";
                txtForm_Path.Text = "";
                txtOrderBy.Text = "";
                ds = objdb.ByProcedure("SpUMFormMaster",
                          new string[] { "flag", "Module_ID" },
                          new string[] { "5", ddlModule_Name.SelectedValue }, "dataset");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblRecord.Text = "";
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
                else if (ds.Tables[0].Rows.Count == 0)
                {
                    lblRecord.Text = "";
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
                else
                {
                    lblRecord.Text = "";
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}