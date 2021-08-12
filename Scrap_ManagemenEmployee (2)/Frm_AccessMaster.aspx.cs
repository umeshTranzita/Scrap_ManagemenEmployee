using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class Frm_AccessMaster : System.Web.UI.Page
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
                lblMsg.Text = "";
               
                ViewState["SiteId"] = Session["SiteId"].ToString();
                ViewState["Email"] = Session["Email"].ToString();
                ViewState["Role_ID"] = Session["Role_ID"];
                FillSite();
                FillAccessType();
                //lblUserID.Text = Session["UserCode"].ToString();
                FillGrid();

                if (ViewState["Role_ID"].ToString() == "1" || ViewState["Role_ID"].ToString() == "2")
                {
                    //ddlSite.Attributes.Add("disabled", "disabled");
                }
                else
                {
                    ddlSite.Attributes.Add("disabled", "disabled");
                }
                //DepartName.Attributes.Add("class", "active");
                //DepartCode.Attributes.Add("class", "active");
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
                ddlSite.SelectedValue = ViewState["SiteId"].ToString();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillAccessType()
    {
        try
        {
            ds = objdb.ByProcedure("Sp_AccessType", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlAccessType.DataTextField = "AccessType";
                ddlAccessType.DataValueField = "Id";
                ddlAccessType.DataSource = ds;
                ddlAccessType.DataBind();
                ddlAccessType.Items.Insert(0, new ListItem("Select Access", "0"));
                ddlAccessType.SelectedIndex = 0;
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
            string msg = "";
            if (ddlSite.SelectedIndex == 0)
            {
                msg += "PLEASE SELECT SITE.\\n";
            }
            else if (ddlAccessType.SelectedIndex == 0)
            {
                msg += "PLEASE ENTER ACCESS CONTROL.\\n";
            }
            else if (msg == "")
            {
                string IsActive = "NO";
                if (chkIsActive.Checked == true)
                {
                    IsActive = "YES";
                }
                if (btnSubmit.Text == "SUBMIT")
                {
                    ds = objdb.ByProcedure("Sp_AccessType", new string[] { "flag", "Access_TypeId", "Site_Id", "Create_by", "Is_Active" },
                        new string[] { "2", ddlAccessType.SelectedValue, ddlSite.SelectedValue, ViewState["Email"].ToString(), IsActive.ToString() }, "dataset");
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() != "OK")
                                {
                                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["Msg"].ToString());

                                }
                                else
                                {
                                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Successfully Submitted");
                                }
                            }
                        }
                    }
                }
                else
                {
                    objdb.ByProcedure("Sp_AccessType", new string[] { "flag", "Access_TypeId", "Site_Id", "Create_by", "Is_Active", "Access_Id" },
                           new string[] { "6", ddlAccessType.SelectedValue, ddlSite.SelectedValue, ViewState["Email"].ToString(), IsActive.ToString(), ViewState["Access_Id"].ToString() }, "dataset");
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Update Successfully");
                }
                FillGrid();
                
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", msg.ToString());

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
            ds = objdb.ByProcedure("Sp_AccessType", new string[] { "flag","Site_Id" }, new string[] { "3",ddlSite.SelectedValue}, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds.Tables[0];
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

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string IsActive = "NO";
            ViewState["Access_Id"] = Convert.ToInt32(e.CommandArgument.ToString());
            ds = objdb.ByProcedure("Sp_AccessType", new string[] { "flag", "Access_Id" },
                new string[] { "5", ViewState["Access_Id"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlSite.SelectedValue = ds.Tables[0].Rows[0]["Site_Id"].ToString();
                ddlAccessType.SelectedValue = ds.Tables[0].Rows[0]["Access_TypeId"].ToString();

                IsActive = ds.Tables[0].Rows[0]["Is_Active"].ToString();
                if (IsActive == "YES")
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


    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
}