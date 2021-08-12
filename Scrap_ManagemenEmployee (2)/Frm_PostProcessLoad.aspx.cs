using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing;
using System.Configuration;
using System.Data.SqlClient;

public partial class Frm_PostProcessLoad : System.Web.UI.Page
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
            TruckNo.Attributes.Add("class", "active");
            ViewState["SiteId"] = Session["SiteId"].ToString();
            ViewState["UserCode"] = Session["UserCode"].ToString();           
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
               new string[] { "4", ViewState["SiteId"].ToString() }, "dataset");
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
    protected void ddlConsigment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ds = null;
            LblErrorMsg.Text = "";
            lblMsg.Text = "";
            ds = objdb.ByProcedure("Sp_Sold", new string[] { "flag", "Site_Id", "Consigment_No" },
               new string[] { "5", ViewState["SiteId"].ToString(), ddlConsigment.SelectedValue }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridSold.DataSource = ds.Tables[0];
                        GridSold.DataBind();
                        Panel1.Visible = true;



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

            lblMsg.Text = "";
            string msg = "";
            string LoadStatus = "";
           
            if (TxtTruckNo.Text == "")
            {
                msg += "Please Enter Truck No.\\n";
            }
            if (msg == "")
            {
                
                LblErrorMsg.Text = "";
                lblMsg.Text = "";


                foreach (GridViewRow item in GridSold.Rows)
                {
                    CheckBox Chk = (CheckBox)item.FindControl("chkIsActive");
                    Label Bag_BatchNo = (Label)item.FindControl("LblBag_BatchNo");
                    Label Consigment_No = (Label)item.FindControl("LblRefrenceNo");
                    Label Id = (Label)item.FindControl("lblRowNumber");
                    if (Chk.Checked == true)
                    {
                        ds = objdb.ByProcedure("Sp_Sold", new string[] { "flag", "Site_Id", "Consigment_No", "Bag_BatchNo", "Id", "Create_by","Load_TruckNo" },
                      new string[] { "10", ViewState["SiteId"].ToString(), ddlConsigment.SelectedValue, Bag_BatchNo.Text, Id.ToolTip.ToString(),ViewState["UserCode"].ToString(),TxtTruckNo.Text }, "dataset");
                    }

                }

                //objdb.ByProcedure("Sp_Sold", new string[] { "flag", "Site_Id", "Consigment_No","Create_by","Load_TruckNo" },
                //   new string[] { "6", ViewState["SiteId"].ToString(), ddlConsigment.SelectedValue, ViewState["UserCode"].ToString(),TxtTruckNo.Text }, "dataset");
               
                Panel1.Visible = false;
                GetConsigment();
                TxtTruckNo.Text = "";
                GridSold.DataSource = null;
                GridSold.DataBind();
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Scrap Sale Successfully");
               
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Cal lMyFunction", "alert('" + msg + "');", true);
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
           


            if (CheckAll.Checked == true && BagStatus.Text == "Pending")
            {
                Chk.Checked = true;
                       
            }
            else
            {
                Chk.Checked = false;
            }
            
           
            

        }
      }
   
}