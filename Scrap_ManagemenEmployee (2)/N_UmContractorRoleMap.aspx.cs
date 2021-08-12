using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class N_UmContractorRoleMap : System.Web.UI.Page
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
                    ViewState["SiteId"] = Session["SiteId"].ToString();
                    FillSite();
                    GetAssignRoll();
                    ds = objdb.ByProcedure("SpEmployeeRoleMap",
                        new string[] { "flag", "SiteId" },
                        new string[] { "9", ViewState["SiteId"].ToString() }, "dataset");

                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        ddlEmployye_Name.DataTextField = "Emp";
                        ddlEmployye_Name.DataValueField = "Login_ID";
                        ddlEmployye_Name.DataSource = ds;
                        ddlEmployye_Name.DataBind();
                        ddlEmployye_Name.Items.Insert(0, "Select Contractor");
                    }
                    else
                    {
                        ddlEmployye_Name.Items.Clear();

                    }
                    //BagBatchNo.Attributes.Add("class", "active");

                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }

            
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
                ddlSite.SelectedValue = ViewState["SiteId"].ToString();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlEmployye_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        divGrid.Visible = false;
        lblMsg.Text = "";
        if (ddlEmployye_Name.SelectedIndex > 0)
        {
            divGrid.Visible = true;
            ds = objdb.ByProcedure("SpEmployeeRoleMap",
                       new string[] { "flag", "Emp_ID" },
                       new string[] { "4", ddlEmployye_Name.SelectedValue.ToString() }, "dataset");

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
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            bool minoneselect = false;
            int RowNo = 0;

            ds = objdb.ByProcedure("SpEmployeeRoleMap", new string[] { "flag", "Emp_ID" }, new string[] { "3", ddlEmployye_Name.SelectedValue.ToString() }, "dataset");

            foreach (GridViewRow gvrow in GridView1.Rows)
            {

                CheckBox chk = (CheckBox)gvrow.Cells[0].FindControl("chkSelect") as CheckBox;
                if (chk.Checked)
                {
                    minoneselect = true;
                    string RoleID = GridView1.Rows[RowNo].Cells[2].Text;
                    string EmployeeID = ddlEmployye_Name.SelectedValue.ToString();


                    objdb.ByProcedure("SpEmployeeRoleMap",
                        new string[] { "flag", "Emp_ID", "Role_ID", "EmpRoleMap_UpdatedBy" },
                        new string[] { "2", EmployeeID, RoleID, ViewState["Emp_ID"].ToString() }, "dataset");
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

            GetAssignRoll();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    private void GetAssignRoll()
    {

        try
        {

            lblMsg.Text = "";

            ds = objdb.ByProcedure("SpEmployeeRoleMap",
                       new string[] { "flag" },
                       new string[] { "8" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GVAssignRoll.DataSource = ds;
                GVAssignRoll.DataBind();

            }

            else
            {
                GVAssignRoll.DataSource = null;
                GVAssignRoll.DataBind();

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        ds = objdb.ByProcedure("SpEmployeeRoleMap",
                        new string[] { "flag", "SiteId" },
                        new string[] { "9",ddlSite.SelectedValue }, "dataset");

        if (ds.Tables[0].Rows.Count != 0)
        {
            ddlEmployye_Name.DataTextField = "Emp";
            ddlEmployye_Name.DataValueField = "Login_ID";
            ddlEmployye_Name.DataSource = ds;
            ddlEmployye_Name.DataBind();
            ddlEmployye_Name.Items.Insert(0, "Select Contractor");
        }
        else
        {
            ddlEmployye_Name.Items.Clear();

        }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}