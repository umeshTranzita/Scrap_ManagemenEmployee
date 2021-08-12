using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class Frm_LoadedDetails : System.Web.UI.Page
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
            GetData();
            AccessControl();


        }
    }

    protected void GetData()
    {
        try
        {
            ds = null;

            ds = objdb.ByProcedure("Sp_ScrapLoad", new string[] { "flag", "Site_Id" },
                new string[] { "7", ViewState["SiteId"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        GridLoad.DataSource = ds.Tables[0];
                        GridLoad.DataBind();

                        foreach (GridViewRow item in GridLoad.Rows)
                        {
                            Label Status = (Label)item.FindControl("LblStatus");
                            LinkButton Btn = (LinkButton)item.FindControl("LnkModal");
                            if (Status.Text != "READY TO SEND")
                            {
                                Btn.Visible = false;
                            }
                            else
                            {
                                Btn.Visible = true;
                            }
                        }

                    }
                    else
                    {
                        GridLoad.DataSource = null;
                        GridLoad.DataBind();

                    }
                }
                else
                {
                    GridLoad.DataSource = null;
                    GridLoad.DataBind();

                }
            }
            else
            {
                GridLoad.DataSource = null;
                GridLoad.DataBind();

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void AccessControl()
    {
        try
        {
            ds = null;
            lblMsg.Text = "";
            ds = objdb.ByProcedure("Sp_ScrapLoad", new string[] { "flag", "SiteID" }, new string[] { "15", ViewState["SiteId"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count != 0)
                    {

                        if (ds.Tables[0].Rows[0]["AccessType"].ToString() == "TruckWeight" && ds.Tables[0].Rows[0]["Is_Active"].ToString() == "YES")
                        {

                            HdnTruck.Value = "1";
                            TxtInitailWeight.Visible = true;
                            TxtFinalWeight.Visible = true;
                            FinalWeight.Visible = true;
                            InitailWeight.Visible = true;
                            TxtTolerance.Visible = true;
                            Tolerance.Visible = true;
                            ddlConsignee.Visible = true;
                            if (ds.Tables[0].Rows.Count > 1)
                            {
                                if (ds.Tables[0].Rows[1]["AccessType"].ToString() == "Truck Tolerance")
                                {
                                    HdnTruck.Value = "2";

                                }
                                else
                                {
                                    TxtTolerance.Visible = false;
                                    Tolerance.Visible = false;
                                }
                            }
                            else
                            {
                                TxtTolerance.Visible = false;
                                Tolerance.Visible = false;
                            }
                        }
                        else
                        {
                            HdnTruck.Value = "0";
                            TxtInitailWeight.Visible = false;
                            TxtFinalWeight.Visible = true;
                            FinalWeight.Visible = true;
                            InitailWeight.Visible = false;
                            TxtFinalWeight.Text = "0";
                            TxtTolerance.Visible = false;
                            Tolerance.Visible = false;
                            ddlConsignee.Visible = true;


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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ds = null;
            LblModalMsg.Text = "";
            if (ViewState["ScrapCat"].ToString() != "RE-PROCESSING")
            {
                if (TxtFinalWeight.Text == "")
                {
                    LblModalMsg.Text = objdb.Alert("fa-check", "alert-success", "Sorry!", "Plese Enter FInal Weight");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "showModal();", true);
                    return;
                }

            }
            else
            {

                if (ddlConsignee.SelectedIndex == 0)
                {
                    LblModalMsg.Text = objdb.Alert("fa-check", "alert-success", "Sorry!", "Plese Select Consignee Name");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "showModal();", true);
                    return;
                }
                if (TxtEnterLockNo.Text == "")
                {
                    LblModalMsg.Text = objdb.Alert("fa-check", "alert-success", "Sorry!", "Plese Enter Lock No");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "showModal();", true);
                    return;

                }
            }
            ds = objdb.ByProcedure("Sp_ScrapLoad", new string[] { "flag", "Scrap_Id", "Final_Weight", "Lock_No", "Create_By", "Status", "Site_Id", "Refrence_No", "ConsigneeId", "SMTrukGatePass_Id" },
                              new string[] { "9", ViewState["Scrap_Id"].ToString(), TxtFinalWeight.Text, TxtEnterLockNo.Text, ViewState["UserCode"].ToString(), "DISPATCHED", ViewState["SiteId"].ToString(), TxtConsigment.Text, ddlConsignee.SelectedValue, ViewState["SMTrukGatePass_Id"].ToString() }, "Dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "SCRAP DISPATCHED SUCCESSFULLY");
                            GetData();

                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "");
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

    //protected void LnkDispatch_Click(object sender, EventArgs e)
    //{
    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "showModal();", true);
    //}

    protected void LnkModal_Click(object sender, EventArgs e)
    {

    }

    protected void GridLoad_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            ds = null;
            TruckNo.Attributes.Add("class", "active");
            InitailWeight.Attributes.Add("class", "active");
            FinalWeight.Attributes.Add("class", "active");
            Tolerance.Attributes.Add("class", "active");
            LockNo.Attributes.Add("class", "active");
            ReEnterLock.Attributes.Add("class", "active");
            ScrapWeight.Attributes.Add("class", "active");
            ConsigmentNo.Attributes.Add("class", "active");
            LblConsignee.Attributes.Add("class", "active");
            TxtFinalWeight.Text = "0";
            TxtEnterLockNo.Text = "";
            TxtREEnterLockNo.Text = "";
            TxtConsigment.Text = "";



            if (e.CommandName == "Dispatch")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                Label lblRefNO = (Label)gvrow.FindControl("lblRefrenceNo");
                Label lblScrapCat = (Label)gvrow.FindControl("lblCategory");
                ViewState["ScrapCat"] = lblScrapCat.Text;
                ViewState["Scrap_Id"] = Convert.ToInt32(e.CommandArgument.ToString());
                Label lblSMTrukGatePass_Id = (Label)gvrow.FindControl("lblSMTrukGatePass_Id");
                ViewState["SMTrukGatePass_Id"] = lblSMTrukGatePass_Id.Text;

                ds = objdb.ByProcedure("Sp_ScrapLoad", new string[] { "flag", "Scrap_Id", "Site_Id", "Refrence_No" },
                                new string[] { "8", ViewState["Scrap_Id"].ToString(), ViewState["SiteId"].ToString(), lblRefNO.Text }, "Dataset");
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        TxtTruckNo.Text = ds.Tables[0].Rows[0]["Truck_No"].ToString();
                        TxtInitailWeight.Text = ds.Tables[0].Rows[0]["Initial_TruckWeight"].ToString();
                        TxtInitailWeight.Text = ds.Tables[0].Rows[0]["Initial_TruckWeight"].ToString();
                        TxtTolerance.Text = ds.Tables[0].Rows[0]["Tolerance_DispatchWeight"].ToString();
                        TxtScrapWeight.Text = ds.Tables[0].Rows[0]["Total_Weight"].ToString();
                        TxtConsigment.Text = ds.Tables[0].Rows[0]["Refrence_No"].ToString();
                        DataSet dsNew = objdb.ByProcedure("Sp_ScrapLoad", new string[] { "flag" },
                                new string[] { "20" }, "Dataset");
                        if (lblScrapCat.Text == "RE-PROCESSING")
                        {
                            ddlConsignee.DataTextField = "ConsigneeName";
                            ddlConsignee.DataValueField = "ConsigneeId";
                            ddlConsignee.DataSource = dsNew.Tables[0];
                            ddlConsignee.DataBind();
                            ddlConsignee.Items.Insert(0, new ListItem("Select Category", "0"));
                            TxtFinalWeight.Visible = false;
                            FinalWeight.Visible = false;
                        }
                        else
                        {
                            ddlConsignee.DataTextField = "ConsigneeName";
                            ddlConsignee.DataValueField = "ConsigneeId";
                            ddlConsignee.DataSource = dsNew.Tables[1];
                            ddlConsignee.DataBind();
                            ddlConsignee.Items.Insert(0, new ListItem("Select Category", "0"));
                        }

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "showModal();", true);
                        //  ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#myModal').modal('toggle');</script>"); 
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void GridLoad_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridLoad.PageIndex = e.NewPageIndex;
        this.GetData();


    }
}