using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class FrmUnitMaster : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDB objdb = new common();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    FillGrid();
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
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string IsActive = "NO";
            if (chkMailIsActive.Checked == true)
            {
                IsActive = "YES";
            }
            if (btnSave.Text == "Save")
            {
                ds = objdb.ByProcedure("Sp_UnitMaster",
                  new string[] { "flag", "UnitName", "IsActive", "Create_By", "Site_Id" },
                  new string[] { "1", txtUnit.Text, IsActive.ToString(), Session["UserCode"].ToString(), Session["SiteId"].ToString() }, "dataset");
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Unit Successfully Created");
                        FillGrid();
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["Msg"].ToString());
                    }
                    clear();
                }
            }
            else
            {
                ds = objdb.ByProcedure("Sp_UnitMaster",
                 new string[] { "flag", "UnitName", "IsActive", "Create_By", "Site_Id", "UnitId" },
                 new string[] { "4", txtUnit.Text, IsActive.ToString(), Session["UserCode"].ToString(), Session["SiteId"].ToString(),ViewState["UnitId"].ToString() }, "dataset");
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Unit Successfully Updated");
                        FillGrid();
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["Msg"].ToString());
                    }
                    clear();
                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    public void clear()
    {
        txtUnit.Text = "";

    }
    public void FillGrid()
    {
        try
        {
            ds = null;
            GridUnit.DataSource = null;
            GridUnit.DataBind();
            ds = objdb.ByProcedure("Sp_UnitMaster",
            new string[] { "flag", "Site_Id" },
            new string[] { "2", Session["SiteId"].ToString() }, "dataset");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridUnit.DataSource = ds;
                GridUnit.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["UnitId"] = GridUnit.SelectedValue.ToString();
            lblMsg.Text = "";
            ds = objdb.ByProcedure("Sp_UnitMaster",
                       new string[] { "flag", "UnitId", "Site_Id" },
                       new string[] { "3", ViewState["UnitId"].ToString(), Session["SiteId"].ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtUnit.Text = ds.Tables[0].Rows[0]["UnitName"].ToString();


                if (ds.Tables[0].Rows[0]["IsActive"].ToString() == "YES")
                {
                    chkMailIsActive.Checked = true;
                }
                else
                {
                    chkMailIsActive.Checked = false;
                }
                btnSave.Text = "Edit";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}