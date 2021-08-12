using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI;


public partial class Frm_ToleranceMaster : System.Web.UI.Page
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
                    Tolerance.Attributes.Add("class", "active");
                    ViewState["SiteId"] = Session["SiteId"].ToString();
                    ViewState["Email"] = Session["Email"].ToString();
                    ViewState["Role_ID"] = Session["Role_ID"];
                    FillSite();
                    FillGrid();
                    
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string IsActive = "No";
            if (chkIsActive.Checked == true)
            {
                IsActive = "Yes";
            }
            if (btnSubmit.Text == "SUBMIT")
            {
                ds = objdb.ByProcedure("Sp_Tolerance",
                               new string[] { "flag", "Tolerance_Weight", "Type", "Create_By", "IsActive", "Tolerance_Type","SiteID" },
                               new string[] { "1", txtTolerance.Text, ddlType.SelectedItem.Text, ViewState["Email"].ToString(), IsActive.ToString(), ddlToleranceType.SelectedItem.Text, ddlSite.SelectedValue }, "dataset");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Successfully Submitted");
                        txtTolerance.Text = "0";
                        ddlType.SelectedIndex = 0;
                        ddlToleranceType.SelectedIndex = 0;
                        FillGrid();
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["Msg"].ToString());
                    }
                }
            }
            else
            {
                ds = objdb.ByProcedure("Sp_Tolerance",
                                  new string[] { "flag", "Tolerance_Weight", "Type", "Create_By", "IsActive", "Tolerance_Id", "Tolerance_Type", "SiteID" },
                                  new string[] { "4", txtTolerance.Text, ddlType.SelectedItem.Text, ViewState["Email"].ToString(), IsActive.ToString(), ViewState["Tolerance_Id"].ToString(), ddlToleranceType.SelectedItem.Text, ddlSite.SelectedValue }, "dataset");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Successfully Updated");
                        txtTolerance.Text = "0";
                        ddlType.SelectedIndex = 0;
                        ddlToleranceType.SelectedIndex = 0;
                        FillGrid();
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["Msg"].ToString());
                    }
                }

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


            ds = objdb.ByProcedure("Sp_Tolerance", new string[] { "flag", "SiteID" }, new string[] { "2",ddlSite.SelectedValue }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GridTolerance.DataSource = ds;
                GridTolerance.DataBind();
            }
            else
            {
                GridTolerance.DataSource = null;
                GridTolerance.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void GridTolerance_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            string IsActive = "No";
            ViewState["Tolerance_Id"] = GridTolerance.SelectedDataKey.Value;
            ds = objdb.ByProcedure("Sp_Tolerance", new string[] { "flag", "Tolerance_Id" }, new string[] { "3", ViewState["Tolerance_Id"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlType.SelectedItem.Text = ds.Tables[0].Rows[0]["Type"].ToString();
                txtTolerance.Text = ds.Tables[0].Rows[0]["Tolerance_Weight"].ToString();
                ddlToleranceType.SelectedItem.Text = ds.Tables[0].Rows[0]["Tolerance_Type"].ToString();
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Frm_ToleranceMaster.aspx");
    }
     protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
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
}