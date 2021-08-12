using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing;
using System.Configuration;
using System.Data.SqlClient;
public partial class Frm_ScrapSold : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDB objdb = new common();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

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

            AllSapDock.Attributes.Add("class", "active");
            GetConsigment();
           

        }
    }
    public void GetConsigment()
    {
        try
        {
            ds = null;
            LblErrorMsg.Text = "";
            lblMsg.Text = "";
            ds = objdb.ByProcedure("Sp_Sold", new string[] { "flag", "Site_Id" },
               new string[] { "1", ViewState["SiteId"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlConsigment.DataTextField = "Refrence_No";
                        ddlConsigment.DataValueField = "Refrence_No";
                        ddlConsigment.DataSource = ds;
                        ddlConsigment.DataBind();
                        ddlConsigment.Items.Insert(0, new ListItem("Select Consigment No", "0"));
                        ddlConsigment.SelectedIndex = 0;
                       
                      
                    }
                    else
                    {
                        ddlConsigment.DataSource = ds;
                        ddlConsigment.Items.Clear();
                        ddlConsigment.Items.Insert(0, new ListItem("No Record Found", "0"));

                    }
                }
                else
                {
                    ddlConsigment.DataSource = ds;
                    ddlConsigment.Items.Clear();
                    ddlConsigment.Items.Insert(0, new ListItem("No Record Found", "0"));

                }
            }
            else
            {
                ddlConsigment.DataSource = ds;
                ddlConsigment.Items.Clear();
                ddlConsigment.Items.Insert(0, new ListItem("No Record Found", "0"));

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void checkAll_CheckedChanged(object sender, EventArgs e)
    {
        LblErrorMsg.Text = "";
        lblMsg.Text = "";
        CheckBox CheckAll = (CheckBox)GridSold.HeaderRow.FindControl("checkAll");

        foreach (GridViewRow item in GridSold.Rows)
        {

            CheckBox Chk = (CheckBox)item.FindControl("chkIsActive");
            Label Bag_BactchNo = (Label)item.FindControl("LblBag_BatchNo");
            Label BagStatus = (Label)item.FindControl("LblStatus");
            Label Weight = (Label)item.FindControl("LblBagWeight");
            TextBox SapCode = (TextBox)item.FindControl("TxtSapDockNo");
            
          
            if (CheckAll.Checked == true && BagStatus.Text == "Pending")
            {
                Chk.Checked = true;
                SapCode.Enabled = true;

                if (chkSapCode.Checked == true)
                {
                    SapCode.Text = TxtAllSapDock.Text;

                }
            }
            else if (CheckAll.Checked == true && BagStatus.Text == "SOLD")
            {
                Chk.Checked = false;
                

            }
            else if (CheckAll.Checked == false && BagStatus.Text == "Pending")
            {
                Chk.Checked = false;
                
            }
            

        }
    }
    protected void chkIsActive_CheckedChanged(object sender, EventArgs e)
    {
        LblErrorMsg.Text = "";
        lblMsg.Text = "";
        GridViewRow currentRow = ((GridViewRow)((CheckBox)sender).NamingContainer);
        TextBox txtSapCode = (TextBox)currentRow.FindControl("TxtSapDockNo");
       
        CheckBox Chk = (CheckBox)currentRow.FindControl("chkIsActive");
        if (Chk.Checked == true)
        {
            txtSapCode.Text = "";
            txtSapCode.Enabled = true;

            if (chkSapCode.Checked == true)
            {
                txtSapCode.Text = TxtAllSapDock.Text;

            }
        }
        else
        {
            txtSapCode.Text = "";
            txtSapCode.Enabled = false;
        }
    }
    protected void ddlConsigment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ds = null;
            LblErrorMsg.Text = "";
            lblMsg.Text = "";
            ds = objdb.ByProcedure("Sp_Sold", new string[] { "flag", "Site_Id","Consigment_No" },
               new string[] { "2", ViewState["SiteId"].ToString(),ddlConsigment.SelectedValue }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridSold.DataSource = ds.Tables[0];
                        GridSold.DataBind();

                    }
                }
            }
       
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ds = null;
            LblErrorMsg.Text = "";
            lblMsg.Text = "";
            string Msg = "", Msg1 = "DONE";
            if (ddlSold.SelectedIndex == 0)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Please Select Sold Type");
                return;
            }

            foreach (GridViewRow item in GridSold.Rows)
            {
                CheckBox Chk = (CheckBox)item.FindControl("chkIsActive");
                TextBox txtSapCode = (TextBox)item.FindControl("TxtSapDockNo");

                if (Chk.Checked == true && ddlSold.SelectedIndex == 1 && txtSapCode.Text == "")
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Please Enter Sap Dock No In All Checked Bag");
                    return;
                }

                if (Chk.Checked == true)
                {
                    Msg = "OK";

                }
            }

            if (Msg == "")
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Please Select Atleast One Scrap Bag");
                return;
            }
            foreach (GridViewRow item in GridSold.Rows)
            {
                CheckBox Chk = (CheckBox)item.FindControl("chkIsActive");
                TextBox txtSapCode = (TextBox)item.FindControl("TxtSapDockNo");
                Label Bag_BatchNo = (Label)item.FindControl("LblBag_BatchNo");
                Label Consigment_No = (Label)item.FindControl("LblRefrenceNo");
                Label Id = (Label)item.FindControl("lblRowNumber");
                if (Chk.Checked == true && txtSapCode.Text != "")
                {
                    ds = objdb.ByProcedure("Sp_Sold", new string[] { "flag", "Site_Id", "Consigment_No", "Bag_BatchNo", "SapDockNo","Id","Sold_Status","Create_by" },
                  new string[] { "3", ViewState["SiteId"].ToString(), ddlConsigment.SelectedValue, Bag_BatchNo.Text, txtSapCode.Text, Id.ToolTip.ToString(), ddlSold.SelectedItem.Text, ViewState["UserCode"].ToString() }, "dataset");
                }          
                
            }
            Panel1.Visible = false;
            TxtAllSapDock.Text = "";
            chkSapCode.Checked = false;
            GridSold.DataSource = null;
            GridSold.DataBind();
            ddlConsigment.SelectedIndex = 0;
            ddlSold.SelectedIndex = 0;
            GetConsigment();
            BtnSubmit.Visible = false;
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Scrap Sale Successfully");

        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlSold_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSold.SelectedItem.Text == "YES")
        {
            Panel1.Visible = true;
            BtnSubmit.Visible = true;
        }
        else
        {
            Panel1.Visible = false;
            BtnSubmit.Visible = false;
        }
        
    }
    protected void chkSapCode_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkSapCode.Checked == true)
            {

         
             foreach (GridViewRow item in GridSold.Rows)
             {
                CheckBox Chk = (CheckBox)item.FindControl("chkIsActive");
                TextBox SapDockCode = (TextBox)item.FindControl("TxtSapDockNo");
                if (Chk.Checked == true)
                {
                    SapDockCode.Text = TxtAllSapDock.Text;
                }
             }
            }
            else
            {
                foreach (GridViewRow item in GridSold.Rows)
                {
                   
                    TextBox SapDockCode = (TextBox)item.FindControl("TxtSapDockNo");
                    SapDockCode.Text = "";
                }
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}