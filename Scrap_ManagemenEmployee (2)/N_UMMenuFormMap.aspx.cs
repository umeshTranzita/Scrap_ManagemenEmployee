using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class N_UMMenuFormMap : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDB objdb = new common();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            divGrid.Visible = false;
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }

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
            ds = objdb.ByProcedure("SpUMModuleMaster",
                        new string[] { "flag" },
                        new string[] { "7" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlModule_Name.DataTextField = "Module_Name";
                ddlModule_Name.DataValueField = "Module_ID";
                ddlModule_Name.DataSource = ds;
                ddlModule_Name.DataBind();
                ddlModule_Name.Items.Insert(0, "Select");
            }
        }
    }
    protected void ddlModule_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMenu_Name.ClearSelection();
        divGrid.Visible = false;
        lblMsg.Text = "";
        if (ddlModule_Name.SelectedIndex > 0)
        {
            divGrid.Visible = true;
            string ModuleID = ddlModule_Name.SelectedValue.ToString();
            ds = objdb.ByProcedure("SpUMMenuFormMap",
                            new string[] { "flag", "Module_ID" },
                            new string[] { "3", ModuleID }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                btnSave.Visible = true;
            }
            else if (ds != null && ds.Tables[0].Rows.Count == 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                btnSave.Visible = false;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                btnSave.Visible = false;
            }


            DataSet ds1 = objdb.ByProcedure("SpUMMenuMaster",
                        new string[] { "flag", "Module_ID" },
                        new string[] { "10", ModuleID }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlMenu_Name.DataTextField = "Menu_Name";
                ddlMenu_Name.DataValueField = "Menu_ID";
                ddlMenu_Name.DataSource = ds1;
                ddlMenu_Name.DataBind();
                ddlMenu_Name.Items.Insert(0, "Select");
            }

        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            bool minoneselect = false;
            int RowNo = 0;

            ds = objdb.ByProcedure("SpUMMenuFormMap", new string[] { "flag", "Menu_ID","Create_By", "Site_Id"  }, 
			new string[] { "2", ddlMenu_Name.SelectedValue.ToString(),Session["UserCode"].ToString(), Session["SiteId"].ToString() }, "dataset");

            foreach (GridViewRow gvrow in GridView1.Rows)
            {

                CheckBox chk = (CheckBox)gvrow.Cells[0].FindControl("chkSelect") as CheckBox;
                if (chk.Checked && chk.Enabled == true)
                {
                    minoneselect = true;
                    HiddenField FormID = (HiddenField)gvrow.FindControl("hdnForm_ID");
                    string FormPath = GridView1.Rows[RowNo].Cells[2].Text;
                    string MenuID = ddlMenu_Name.SelectedValue.ToString();
                    string ModuleID = ddlModule_Name.SelectedValue.ToString();

                    objdb.ByProcedure("SpUMMenuFormMap",
                        new string[] { "flag", "Menu_ID", "Form_ID", "Module_ID", "MenuForm_FormPath", "MenuFormMap_UpdatedBy" },
                        new string[] { "0", MenuID, FormID.Value, ModuleID, FormPath, ViewState["Emp_ID"].ToString() }, "dataset");
                    RowNo++;
                    divGrid.Visible = true;
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                }
                else
                {
                    minoneselect = true;
                    RowNo++;
                    divGrid.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlMenu_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlMenu_Name.SelectedIndex > 0)
            {
                foreach (GridViewRow row in GridView1.Rows)
                {
                    lblMsg.Text = "";
                    HiddenField hdnForm_ID = (HiddenField)row.FindControl("hdnForm_ID");
                    CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                    //ds = objdb.ByProcedure("SpUMMenuFormMap",
                    //   new string[] { "flag", "Module_ID", "Menu_ID", "Form_ID" },
                    //   new string[] { "4", ddlModule_Name.SelectedValue, ddlMenu_Name.SelectedValue.ToString(), hdnForm_ID.Value }, "dataset");

                    //if (ds != null && ds.Tables[0].Rows.Count > 0)
                    //{
                    //    chkSelect.Checked = true;
                    //}
                    //else
                    //{
                    //    chkSelect.Checked = false;
                    //}
                    DataSet ds11 = objdb.ByProcedure("SpUMMenuFormMap",
                        new string[] { "flag", "Module_ID", "Form_ID" },
                     new string[] { "5", ddlModule_Name.SelectedValue, hdnForm_ID.Value }, "dataset");
                    if (ds11 != null && ds11.Tables[0].Rows.Count > 0)
                    {
                        if (ds11.Tables[0].Rows[0]["STATUS"].ToString() == "TRUE")
                        {
                            if (ds11.Tables[0].Rows[0]["Menu_ID"].ToString() == ddlMenu_Name.SelectedValue.ToString())
                            {
                                chkSelect.Checked = true;
                                chkSelect.Enabled = true;
                            }
                            else
                            {
                                chkSelect.Checked = true;
                                chkSelect.Enabled = false;
                            }

                        }
                        else
                        {
                            ds = objdb.ByProcedure("SpUMMenuFormMap",
                           new string[] { "flag", "Module_ID", "Menu_ID", "Form_ID" },
                           new string[] { "4", ddlModule_Name.SelectedValue, ddlMenu_Name.SelectedValue.ToString(), hdnForm_ID.Value }, "dataset");

                            if (ds != null && ds.Tables[0].Rows.Count > 0)
                            {
                                chkSelect.Checked = true;
                            }
                            else
                            {
                                chkSelect.Checked = false;
                            }
                        }
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