using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class N_UMMenuMaster : System.Web.UI.Page
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
                    ViewState["Menu_ID"] = "0";
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
                    DataSet dsIcon = objdb.ByProcedure("SpUMMenuMaster", new string[] { "flag" }, new string[] { "1" }, "dataset");

                    //IconDisplay.CssClass = dsIcon.Tables[0].Rows[0]["Menu_Icon"].ToString();
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

            ds = objdb.ByProcedure("SpUMMenuMaster",
                new string[] { "flag" },
                new string[] { "6" }, "dataset");
            GridView1.DataSource = ds;
            GridView1.DataBind();

            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string classname = row.Cells[3].Text;
                    Label lbl = (Label)row.FindControl("IconDisplay");
                    lbl.CssClass = classname;  // lbl.CssClass = ds.Tables[0].Rows[row]["Menu_Icon"].ToString();
                }
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

            ds = objdb.ByProcedure("SpUMMenuMaster",
                new string[] { "flag", "Module_ID" },
                new string[] { "9", ddlModule_Name.SelectedValue }, "dataset");


            GridView1.DataSource = ds;
            GridView1.DataBind();
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string classname = row.Cells[3].Text;
                    Label lbl = (Label)row.FindControl("IconDisplay");
                    lbl.CssClass = classname;  // lbl.CssClass = ds.Tables[0].Rows[row]["Menu_Icon"].ToString();
                }
            }
            if (ds.Tables[0].Rows.Count != 0)
            {
                lblRecord.Text = "";
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
            string Menu_Icon = "";
            string msg = "";
            string Menu_IsActive = "1";
            if (ddlModule_Name.SelectedIndex == 0)
            {
                msg += "Select Module Name<br/>";
            }
            if (txtMenu_Name.Text.Trim() == "")
            {
                msg += "Enter Form Name<br/>";
            }
            if (txtOrderBy.Text.Trim() == "")
            {
                msg += "Enter Order By<br/>";
            }
            if (txtMenu_Icon.Text.Trim() == "")
            {
                msg += "Select Menu Icon";
            }

            if (msg.Trim() == "")
            {
                Menu_Icon = "<i class='fa " + txtMenu_Icon.Text.Trim() + "'></i>";
                ds = objdb.ByProcedure("SpUMMenuMaster",
                      new string[] { "flag", "Menu_Name", "OrderBy", "Menu_ID", "Module_ID" },
                      new string[] { "4", txtMenu_Name.Text.Trim(), txtOrderBy.Text, ViewState["Menu_ID"].ToString(), ddlModule_Name.SelectedValue }, "dataset");

                if (btnSave.Text == "Save" && ViewState["Menu_ID"].ToString() == "0" && ds.Tables[0].Rows.Count == 0)
                {
                    objdb.ByProcedure("SpUMMenuMaster",
                    new string[] { "flag", "Menu_IsActive", "Menu_Name", "Module_ID", "Menu_Icon", "OrderBy", "Menu_UpdatedBy", "Create_By", "Site_Id" },
                    new string[] { "0", Menu_IsActive, txtMenu_Name.Text.Trim(), ddlModule_Name.SelectedValue, Menu_Icon, txtOrderBy.Text.Trim(), ViewState["Emp_ID"].ToString(), Session["UserCode"].ToString(), Session["SiteId"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearField();
                    btnSave.Text = "Save";
                }

                else if (btnSave.Text == "Edit" && ViewState["Menu_ID"].ToString() != "0" && ds.Tables[0].Rows.Count == 0)
                {
                    objdb.ByProcedure("SpUMMenuMaster",
                    new string[] { "flag", "Menu_ID", "Menu_Name", "Module_ID", "Menu_Icon", "OrderBy", "Menu_UpdatedBy", "Create_By", "Site_Id" },
                    new string[] { "7", ViewState["Menu_ID"].ToString(), txtMenu_Name.Text.Trim(), ddlModule_Name.SelectedValue, Menu_Icon, txtOrderBy.Text.Trim(), ViewState["Emp_ID"].ToString(), Session["UserCode"].ToString(), Session["SiteId"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ViewState["Menu_ID"] = 0;
                    ClearField();
                    btnSave.Text = "Save";
                }
                else
                {
                    string Menu_Name = ds.Tables[0].Rows[0]["Menu_Name"].ToString();
                    string OrderBy = ds.Tables[0].Rows[0]["OrderBy"].ToString();
                    if (Menu_Name == txtMenu_Name.Text)
                    {
                        //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Alert !", "This Menu Name Is Already Exist.");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('This Menu Name Is Already Exist');", true);
                    }
                    if (OrderBy == txtOrderBy.Text)
                    {
                        // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Alert !", "Order Number Is Already Exist.");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Order Number Is Already Exist');", true);
                    }

                }
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
            string Menu_ID = chk.ToolTip.ToString();
            string Menu_IsActive = "0";
            if (chk != null & chk.Checked)
            {
                Menu_IsActive = "1";
            }
            objdb.ByProcedure("SpUMMenuMaster",
                       new string[] { "flag", "Menu_IsActive", "Menu_ID", "Menu_UpdatedBy", "Create_By", "Site_Id" },
                       new string[] { "8", Menu_IsActive, Menu_ID, ViewState["Emp_ID"].ToString(), Session["UserCode"].ToString(), Session["SiteId"].ToString() }, "dataset");
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
            ViewState["Menu_ID"] = GridView1.SelectedValue.ToString();
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpUMMenuMaster",
                       new string[] { "flag", "Menu_ID" },
                       new string[] { "3", ViewState["Menu_ID"].ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlModule_Name.ClearSelection();
                ddlModule_Name.Items.FindByValue(ds.Tables[0].Rows[0]["Module_ID"].ToString()).Selected = true;
                txtMenu_Name.Text = ds.Tables[0].Rows[0]["Menu_Name"].ToString();

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
        txtMenu_Name.Text = "";
        txtMenu_Icon.Text = "";
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
                txtMenu_Name.Text = "";
                txtMenu_Icon.Text = "";
                txtOrderBy.Text = "";
                ds = objdb.ByProcedure("SpUMMenuMaster",
                          new string[] { "flag", "Module_ID" },
                          new string[] { "5", ddlModule_Name.SelectedValue }, "dataset");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();

                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            string classname = row.Cells[3].Text;
                            Label lbl = (Label)row.FindControl("IconDisplay");
                            lbl.CssClass = classname;  // lbl.CssClass = ds.Tables[0].Rows[row]["Menu_Icon"].ToString();
                        }
                    }
                }
                else
                {
                    lblRecord.Text = "Record Not Found";
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