using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class Frm_SiteMaster : System.Web.UI.Page
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
                ViewState["Email"] = Session["Email"].ToString();
                SiteName.Attributes.Add("class", "active");
                SiteDescription.Attributes.Add("class", "active");
                SiteAddress.Attributes.Add("class", "active");
                GstNo.Attributes.Add("class", "active");
                SiteCode.Attributes.Add("class", "active");
                ConsigneeName.Attributes.Add("class", "active");
                ConsigneeAddress.Attributes.Add("class", "active");
                ConsigneeGstNo.Attributes.Add("class", "active");
                FillGrid();
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


            ds = objdb.ByProcedure("Sp_SiteMaster", new string[] { "flag"}, new string[] { "3"}, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GridSite.DataSource = ds;
                GridSite.DataBind();
            }
            else
            {
                GridSite.DataSource = null;
                GridSite.DataBind();
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
            

            if (txtSiteName.Text.Trim() == "")
            {
                msg += "Enter Site Name.\\n";
            }
            if (txtSiteDescription.Text.Trim() == "")
            {
                msg += "Enter Site Description.\\n";
            }
            if (TxtSiteAddress.Text.Trim() == "")
            {
                msg += "Enter Site Address.\\n";
            }
            if (txtSiteCode.Text.Trim() == "")
            {
                msg += "Enter Site Code.\\n";
            }
            if (TxtGstNo.Text.Trim() == "")
            {
                msg += "Enter Site Gst No.\\n";
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

                    ds = objdb.ByProcedure("Sp_SiteMaster",
                           new string[] { "flag", "SiteCode", "SiteName", "SiteDescription", "UserID", "IsActive", "SiteAddress", "SiteGstNo", "ConsigneeName", "ConsigneeAddress", "ConsigneeGst" },
                           new string[] { "0", txtSiteCode.Text, txtSiteName.Text, txtSiteDescription.Text, ViewState["Email"].ToString(), IsActive.ToString(), TxtSiteAddress.Text, TxtGstNo.Text,TxtConsignee.Text,TxtConsigneeAddress.Text,TxtConsigneeGstNo.Text }, "dataset");
                    //if (ds.Tables[0].Rows.Count > 0)
                    //{
                    //    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    //    {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Submitted Successfully");
                            ClearData();
                            FillGrid();
                    //    }
                    //    else
                    //    {
                    //        //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Updarted Successfully");

                    //    }
                    //}
                }
                if (btnSubmit.Text == "MODIFY")
                {
                    objdb.ByProcedure("Sp_SiteMaster",
                           new string[] { "flag", "SiteCode", "SiteName", "SiteDescription", "UserID", "IsActive", "SiteAddress", "SiteGstNo","SiteId","ConsigneeName", "ConsigneeAddress", "ConsigneeGst" },
                           new string[] { "1", txtSiteCode.Text, txtSiteName.Text, txtSiteDescription.Text, ViewState["Email"].ToString(), IsActive.ToString(), TxtSiteAddress.Text, TxtGstNo.Text,ViewState["Id"].ToString(),TxtConsignee.Text,TxtConsigneeAddress.Text,TxtConsigneeGstNo.Text }, "dataset");
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Update Successfully");
                    ClearData();
                    FillGrid();
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ClearData()
    {
        txtSiteCode.Text = "";
        TxtGstNo.Text = "";
        txtSiteDescription.Text = "";
        txtSiteName.Text = "";
        TxtSiteAddress.Text = "";
        TxtConsignee.Text = "";
        TxtConsigneeAddress.Text = "";
        TxtConsigneeGstNo.Text = "";
        btnSubmit.Text = "SUBMIT";
       
    }
    protected void GridSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            string IsActive = "No";
            ViewState["Id"] = GridSite.SelectedDataKey.Value;
            ds = objdb.ByProcedure("Sp_SiteMaster", new string[] { "flag", "SiteID" }, new string[] { "4", ViewState["Id"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                txtSiteName.Text = ds.Tables[0].Rows[0]["SiteName"].ToString();
                txtSiteDescription.Text = ds.Tables[0].Rows[0]["SiteDescription"].ToString();
                TxtSiteAddress.Text = ds.Tables[0].Rows[0]["SiteAddress"].ToString();
                txtSiteCode.Text = ds.Tables[0].Rows[0]["SiteCode"].ToString();
                TxtGstNo.Text = ds.Tables[0].Rows[0]["SiteGstNo"].ToString();
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