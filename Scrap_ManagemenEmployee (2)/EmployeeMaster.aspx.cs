using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EmployeeMaster : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDB objdb = new common();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null && Session["Email"] != null)
            {
                if (!IsPostBack)
                {
                    lblMsg.Text = "";
                    ViewState["Module_Id"] = "2";
                    ViewState["Emp_ID"] = Session["Email"].ToString();
                    ViewState["Login_ID"] = "0";
                    FillSite();
                    FillGrid();


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
    protected void FillGrid()
    {
        try
        {
            ds = null;

            ds = objdb.ByProcedure("Sp_UserLogin",
                    new string[] { "flag","SiteId" },
                    new string[] { "3",ddlSite.SelectedValue }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
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


    


   




    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {
            ds = null;

            lblMsg.Text = "";
            string msg = "";



            if (txtempname.Text.Trim() == "")
            {
                msg += "Enter Name";
            }

            if (txtTnumber.Text.Trim() == "")
            {
                msg += "Enter Tnumber";
            }


            string strpstatus = "";

            if (EmpStatus.Checked)
            {
                strpstatus = "Yes";
            }
            else
            {
                strpstatus = "No";
            }



            string BUMRP = "";


            if (msg.Trim() == "")
            {
                DataSet DsSipval = null;

                if (btnSave.Text == "Save")
                {
                    DsSipval = objdb.ByProcedure("Sp_UserLogin",
                       new string[] { "flag", "Email", "TNumber" },
                       new string[] { "5", TxtEmail.Text, txtTnumber.Text }, "dataset");
                }

                if (btnSave.Text == "Edit")
                {
                    DsSipval = objdb.ByProcedure("Sp_UserLogin",
                       new string[] { "flag", "Email", "TNumber", "Login_ID" },
                       new string[] { "6", TxtEmail.Text, txtTnumber.Text, ViewState["Login_ID"].ToString() }, "dataset");
                }


                if (DsSipval.Tables[0].Rows.Count == 0)
                {

                    if (btnSave.Text == "Save" && ViewState["Login_ID"].ToString() == "0" && DsSipval.Tables[0].Rows.Count == 0)
                    {
                        objdb.ByProcedure("Sp_UserLogin",
                        new string[] { "flag", "Email", "Status","EmpName", "TNumber","Create_By","SiteId"},
                        new string[] { "7", TxtEmail.Text, strpstatus,txtempname.Text, txtTnumber.Text,ViewState["Emp_ID"].ToString(),ddlSite.SelectedValue }, "dataset");
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Employee Data Save Successfully");
                    }


                    else if (btnSave.Text == "Edit" && ViewState["Login_ID"].ToString() != "0")
                    {

                        objdb.ByProcedure("Sp_UserLogin",
                        new string[] { "flag", "Email", "Status", "EmpName", "TNumber", "Create_By", "SiteId","Login_Id" },
                        new string[] { "8", TxtEmail.Text, strpstatus, txtempname.Text, txtTnumber.Text, ViewState["Emp_ID"].ToString(), ddlSite.SelectedValue, ViewState["Login_ID"].ToString() }, "dataset");
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Employee Data Successfully Updated");

                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Opps !", "Employee Data Already In Exist.");

                    }

                    ViewState["Login_ID"] = "0";
                    txtempname.Text = "";
                    TxtEmail.Text = "";
                    txtTnumber.Text = "";

                    btnSave.Text = "Save";
                    FillGrid();

                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Opps !", "Employee Data Already In Exist");
                }
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

    protected void lbdelete_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            LinkButton btn = (LinkButton)sender;
            GridViewRow gv = (GridViewRow)btn.NamingContainer;
            Label lblRowNumber = (Label)gv.FindControl("lblRowNumber");
            Label lblEmail_ID = (Label)gv.FindControl("lblEmail_ID");
            ViewState["Login_ID"] = lblRowNumber.ToolTip.ToString();

            lblMsg.Text = "";
            ds = objdb.ByProcedure("Sp_Login",
                       new string[] { "flag", "Email_ID", "CreatedBy", "ModuleId", "Login_ID" },
                       new string[] { "8", lblEmail_ID.Text.Trim(), ViewState["Emp_ID"].ToString(), ViewState["Module_Id"].ToString(), ViewState["Login_ID"].ToString() }, "dataset");

            btnSave.Text = "Save";
            lblMsg.Text = "";
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Employee Successfully Deleted");
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void lbEdit_Click(object sender, EventArgs e)
    {
        try
        {

            lblMsg.Text = "";
            LinkButton btn = (LinkButton)sender;
            GridViewRow gv = (GridViewRow)btn.NamingContainer;
            Label lblRowNumber = (Label)gv.FindControl("lblRowNumber");
            ViewState["Login_ID"] = lblRowNumber.ToolTip.ToString();
            Label lblEmail_ID = (Label)gv.FindControl("lblEmail_ID");

            ds = objdb.ByProcedure("Sp_UserLogin",
                       new string[] { "flag", "Email", "Login_ID" },
                       new string[] { "4", lblEmail_ID.Text.Trim(), ViewState["Login_ID"].ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {

                txtempname.Text = ds.Tables[0].Rows[0]["EmpName"].ToString();

                TxtEmail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                TxtEmail.Enabled = false;

                txtTnumber.Text = ds.Tables[0].Rows[0]["TNumber"].ToString();

                string EmpCStatus = ds.Tables[0].Rows[0]["Status"].ToString();

                if (EmpCStatus == "1")
                {
                    EmpStatus.Checked = true;
                }
                else
                {
                    EmpStatus.Checked = true;
                }


                btnSave.Text = "Edit";
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
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}