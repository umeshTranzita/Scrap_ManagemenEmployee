using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing;
using System.Configuration;
using System.Data.SqlClient;

public partial class Frm_ReceiveApproval : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDB objdb = new common();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Session["Email"] == "" || Session["Email"] == null || Session["SiteId"] == null || Session["SiteId"] == "")
            {
                Response.Redirect("~/Login.aspx");

            }
            ViewState["SiteId"] = Session["SiteId"].ToString();
            ViewState["UserCode"] = Session["Email"].ToString();
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "showModal();", true);
            LockNo.Attributes.Add("class", "active");
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
            ds = objdb.ByProcedure("Sp_ScrapReceive", new string[] { "flag", "Site_Id" },
               new string[] { "3", ViewState["SiteId"].ToString() }, "dataset");
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

                    }
                }
                else
                {
                    ddlConsigment.DataSource = ds;
                    ddlConsigment.Items.Clear();

                }
            }
            else
            {
                ddlConsigment.DataSource = ds;
                ddlConsigment.Items.Clear();

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
            LblErrorMsg.Text = "";
            lblMsg.Text = "";
            ds = objdb.ByProcedure("Sp_ScrapReceive", new string[] { "flag", "Lock_No", "Site_Id", "Create_by", "Refrence_No" },
               new string[] { "1", TxtLockNo.Text, ViewState["SiteId"].ToString(), ViewState["UserCode"].ToString(), TxtLockNo.Text }, "dataset");
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
                        PanelReceive.Visible = true;
                    }
                }
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
            BtnReceive.Visible = true;
            double TotalRecWt = 0, TotalBagWt = 0, PlusOverAllToloerance = 0, MinusOverAllToloerance;
            ds = objdb.ByProcedure("Sp_ScrapReceive", new string[] { "flag", "Refrence_No", "Site_Id" },
               new string[] { "2", ddlConsigment.SelectedItem.Text, ViewState["SiteId"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridReceive.DataSource = ds.Tables[0];
                        GridReceive.DataBind();
                        int I = 0;
                        foreach (GridViewRow item in GridReceive.Rows)
                        {

                            Label ScrapStatus = (Label)item.FindControl("LblStatus");
                            TextBox TxtAllRecWt = (TextBox)item.FindControl("TxtReceiveWeight");
                            TextBox TxtAllBagWt = (TextBox)item.FindControl("TxtBagWeight");
                            TextBox ReceiveComment = (TextBox)item.FindControl("TxtReceiveComment");
                            TextBox PostComment = (TextBox)item.FindControl("TxtPostComment");
                            Label NetWeight = (Label)item.FindControl("LblNetWeight");
                            TextBox TierWeight = (TextBox)item.FindControl("TxtTierWeigh");
                            CheckBox ChkException = (CheckBox)item.FindControl("chkIsActive");

                            Label BagStatus = (Label)item.FindControl("LblBagStatus");




                            if (BagStatus.Text == "OK")
                            {
                                TxtAllRecWt.Enabled = false;
                               // NetWeight.Enabled = false;
                                TierWeight.Enabled = false;
                                TxtAllRecWt.BackColor = Color.LightGray;
                                //NetWeight.BackColor = Color.LightGray;
                                TierWeight.BackColor = Color.LightGray;
                                ReceiveComment.Visible = false;
                                PostComment.Visible = false;
                                ChkException.Visible = false;
                            }

                            if (BagStatus.Text == "NOTOK" && ReceiveComment.Text != "")
                            {
                                I = 1;
                            }
                            else
                            {
                                TxtAllRecWt.Enabled = false;
                                BtnPostAction.Enabled = false;
                                ChkException.Enabled = false;
                                //NetWeight.Enabled = false;
                                TierWeight.Enabled = false;

                            }
                            if (TxtAllRecWt.Text.Trim() == null || TxtAllRecWt.Text.Trim() == "")
                            {
                                TxtAllRecWt.Text = "0";
                            }
                            else
                            {
                                TotalRecWt = TotalRecWt + double.Parse(TxtAllRecWt.Text);
                                TotalBagWt = TotalBagWt + double.Parse(TxtAllBagWt.Text);

                            }
                            GridReceive.FooterRow.Cells[1].Font.Size = 16;
                            GridReceive.FooterRow.Cells[4].Font.Size = 16;
                            GridReceive.FooterRow.Cells[6].Font.Size = 16;

                            GridReceive.FooterRow.Cells[1].Text = "OVERALL";
                            GridReceive.FooterRow.Cells[4].Text = TotalRecWt.ToString();
                        }
                        if (I == 1)
                        {
                            //BtnReceive.Text = "POST ACTION TAKEN";
                            BtnPostAction.Visible = true;
                            BtnReceive.Visible = false;
                            TxtPostActionComment.Visible = false;
                            BtnPostAction.Enabled = true;
                            

                        }
                        ViewState["Tolerance_Weight"] = ds.Tables[0].Rows[0]["Tolerance_ReceiveWeight"].ToString();

                        GridReceive.FooterRow.Cells[8].Text = ds.Tables[0].Rows[0]["Overall_WeightReceiveStatus"].ToString();
                        LblOverAllStatus.Text = ds.Tables[0].Rows[0]["Overall_WeightReceiveStatus"].ToString();


                    }
                }
            }

        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    protected void GridReceive_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void BtnReceive_Click(object sender, EventArgs e)
    {
        try
        {
            if (BtnReceive.Text == "RECEIVE")
            {
                foreach (GridViewRow row in GridReceive.Rows)
                {
                    Label BagStatus = (Label)row.FindControl("LblBagStatus");
                    TextBox Comment = (TextBox)row.FindControl("TxtReceiveComment");
                    if (BagStatus.Text == "NOTOK" && Comment.Text == "")
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Please Enter Receive Comment ");
                        return;

                    }
                }
                foreach (GridViewRow row in GridReceive.Rows)
                {
                    Label Scrap_Id = (Label)row.FindControl("lblRowNumber");
                    Label BagBatchNo = (Label)row.FindControl("LblBag_BatchNo");
                    Label RefNo = (Label)row.FindControl("LblRefrenceNo");
                    Label Status = (Label)row.FindControl("LblStatus");
                    Label BagStatus = (Label)row.FindControl("LblBagStatus");
                    TextBox ReceiveComment = (TextBox)row.FindControl("TxtReceiveComment");
                    Label SiteId = (Label)row.FindControl("LblSiteId");
                    
                    if (BagStatus.Text == "NOTOK")
                    {
                        ds = objdb.ByProcedure("Sp_ScrapReceive", new string[] { "flag", "Refrence_No", "Site_Id", "Receive_Comment", "Create_By", "BagBatchNo","Scrap_Id" },
                        new string[] { "4", ddlConsigment.SelectedItem.Text, SiteId.Text, ReceiveComment.Text, ViewState["UserCode"].ToString(), BagBatchNo.Text, Scrap_Id.ToolTip.ToString() }, "dataset");
                    }

                }
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Scrap Received and Sent to Post action taken");
                PanelReceive.Visible = false;
            }
            else
            {


            }

        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }

    protected void BtnPostAction_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            //if (confirmValue == "Yes")
            //{
                //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);
            double TotalPadReceiveWeight = 0, TotalDiaReceiveWeight = 0;

                foreach (GridViewRow row in GridReceive.Rows)
                {
                    Label Scrap_Id = (Label)row.FindControl("lblRowNumber");
                    Label BagBatchNo = (Label)row.FindControl("LblBag_BatchNo");
                    Label RefNo = (Label)row.FindControl("LblRefrenceNo");
                    Label Status = (Label)row.FindControl("LblStatus");
                    Label BagStatus = (Label)row.FindControl("LblBagStatus");
                    TextBox ReceiveComment = (TextBox)row.FindControl("TxtReceiveComment");
                    TextBox ReceiveWeight = (TextBox)row.FindControl("TxtReceiveWeight");
                    TextBox PostComment = (TextBox)row.FindControl("TxtPostComment");
                    CheckBox ChkPostAction = (CheckBox)row.FindControl("chkIsActive");
                    Label SiteId = (Label)row.FindControl("LblSiteId");
                    if (BagStatus.Text == "NOTOK")
                    {
                        if (PostComment.Text == "" || ChkPostAction.Checked == false)
                        {

                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Adjust Weight or Select Checkbox-Exception OK and Post action comment is filled");
                            return;
                        }
                    }
                    if (BagStatus.Text == "OK" && ReceiveComment.Text != "" && PostComment.Text == "" )
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Please Enter Post Action Comment");
                        return;
                    }





                }

                foreach (GridViewRow row in GridReceive.Rows)
                {
                    String PostStatus = "";
                    Label Scrap_Id = (Label)row.FindControl("lblRowNumber");
                    Label BagBatchNo = (Label)row.FindControl("LblBag_BatchNo");
                    Label RefNo = (Label)row.FindControl("LblRefrenceNo");
                    Label Status = (Label)row.FindControl("LblStatus");
                    Label BagStatus = (Label)row.FindControl("LblBagStatus");
                    TextBox ReceiveComment = (TextBox)row.FindControl("TxtReceiveComment");
                    TextBox ReceiveWeight = (TextBox)row.FindControl("TxtReceiveWeight");
                    TextBox PostComment = (TextBox)row.FindControl("TxtPostComment");
                    CheckBox ChkPostAction = (CheckBox)row.FindControl("chkIsActive");
                    Label NetWeight = (Label)row.FindControl("LblNetWeight");
                    TextBox TierWeight = (TextBox)row.FindControl("TxtTierWeigh");
                    Label ScrapType = (Label)row.FindControl("LblScrapType");
                    Label SiteId = (Label)row.FindControl("LblSiteId");
                    Label ActualWeight = (Label)row.FindControl("LblLoadNetWeight");
                    Label OldReceiveWtWeight = (Label)row.FindControl("LblPreviousReceiveWt");
                   
                    ViewState["RefrenceSiteid"] = SiteId.Text;



                    if (ChkPostAction.Checked == true || PostComment.Text!="")
                    {
                        PostStatus = "EXCEPTION OK";
                        if (ScrapType.Text == "PAD")
                        {
                            TotalPadReceiveWeight = TotalPadReceiveWeight + (double.Parse(ActualWeight.Text) - double.Parse(OldReceiveWtWeight.Text));
                        }
                        else if (ScrapType.Text == "DIA")
                        {
                            TotalDiaReceiveWeight = TotalDiaReceiveWeight + +(double.Parse(ActualWeight.Text) - double.Parse(OldReceiveWtWeight.Text));
                        }
                    }
                    if (BagStatus.Text == "NOTOK" || ReceiveComment.Text!="")
                    {
                        ds = objdb.ByProcedure("Sp_ScrapReceive", new string[] { "flag", "Refrence_No", "Site_Id", "Create_By", "Receive_Weight", "Receive_Status", "Overall_WeightReceiveStatus", "BagBatchNo", "Post_ActionComment", "Post_ActionStatus","NetWeight","TierWeight" },
                            new string[] { "5", ddlConsigment.SelectedItem.Text, SiteId.Text, ViewState["UserCode"].ToString(), ReceiveWeight.Text, BagStatus.Text, LblOverAllStatus.Text, BagBatchNo.Text, PostComment.Text, PostStatus.ToString(), NetWeight.Text, TierWeight.Text }, "dataset");
                    }
                }

                objdb.ByProcedure("Sp_ScrapReceive", new string[] { "flag", "Refrence_No", "Site_Id", "Pad_ReceiveWt", "Dia_ReceiveWt" },
              new string[] { "11", ddlConsigment.SelectedItem.Text, ViewState["RefrenceSiteid"].ToString(),TotalPadReceiveWeight.ToString(), TotalDiaReceiveWeight.ToString() }, "dataset");

                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Scrap Post action taken Successfully");
                PanelReceive.Visible = false;
            //}
            //else
            //{
            //    //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
            //}

        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void TxtReceiveWeight_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow currentRow = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtRecWt = (TextBox)currentRow.FindControl("TxtReceiveWeight");
            TextBox txtBagWt = (TextBox)currentRow.FindControl("TxtBagWeight");
            TextBox txtBagTole = (TextBox)currentRow.FindControl("TxtBagToleranceWt");
            Label BagBatch = (Label)currentRow.FindControl("LblBag_BatchNo");
            Label BagStatus = (Label)currentRow.FindControl("LblBagStatus");
            TextBox TierWeigh = (TextBox)currentRow.FindControl("TxtTierWeigh");
            CheckBox ChkPostAction = (CheckBox)currentRow.FindControl("chkIsActive");
            Label NetWeight = (Label)currentRow.FindControl("LblNetWeight");
            Double PlusBagTolerance = 0, MinusBagTolerance = 0;
            PlusBagTolerance = double.Parse(txtBagWt.Text) + double.Parse(txtBagTole.Text);
            MinusBagTolerance = double.Parse(txtBagWt.Text) - double.Parse(txtBagTole.Text);
            //------------------------------------------------------Tier Weight Calculation---------------------------------------------------//
            Double Tier = ((double.Parse(txtRecWt.Text) * 1000) - double.Parse(TierWeigh.Text)) / 1000;

            NetWeight.Text = Tier.ToString();

            if (txtRecWt.Text == "")
            {
                txtRecWt.Text = "0";
            }

            if (NetWeight.Text == "")
            {
                NetWeight.Text = "0";
            }
            if (double.Parse(NetWeight.Text) >= MinusBagTolerance && double.Parse(txtRecWt.Text) <= PlusBagTolerance)
            {
                BagStatus.Text = "OK";
                currentRow.BackColor = Color.LightGreen;
                ChkPostAction.Visible = false;
            }
            else
            {
                BagStatus.Text = "NOTOK";
                currentRow.BackColor = Color.Red;
                ChkPostAction.Visible = true;
            }
            double TotalRecWt = 0, TotalBagWt = 0;
            int I = 0;
            foreach (GridViewRow item in GridReceive.Rows)
            {
                Label OverallBagStatus = (Label)item.FindControl("LblBagStatus");
                if (OverallBagStatus.Text == "NOTOK")
                {
                    I = 1;
                    BtnReceive.Text = "SAVE";
                }
                TextBox TxtAllRecWt = (TextBox)item.FindControl("TxtReceiveWeight");
                TextBox TxtAllBagWt = (TextBox)item.FindControl("TxtBagWeight");

                if (TxtAllRecWt.Text.Trim() == null || TxtAllRecWt.Text.Trim() == "")
                {

                    TxtAllRecWt.Text = "0";
                }
                else
                {
                    TotalRecWt = TotalRecWt + double.Parse(TxtAllRecWt.Text);
                    TotalBagWt = TotalBagWt + double.Parse(TxtAllBagWt.Text);

                }

            }

            // GridReceive.FooterRow.Cells[0].Font.Size = 16;
            GridReceive.FooterRow.Cells[4].Font.Size = 16;
            GridReceive.FooterRow.Cells[5].Font.Size = 16;

            //GridReceive.FooterRow.Cells[0].Text = "OVERALL";
            GridReceive.FooterRow.Cells[4].Text = TotalRecWt.ToString();
            if (I == 1)
            {
                GridReceive.FooterRow.Cells[8].Text = "NOTOK";
                //BtnReceive.Visible = false;
                LblOverAllStatus.Text = "NOTOK";

            }
            else
            {
                GridReceive.FooterRow.Cells[8].Text = "OK";
                // BtnReceive.Visible = true;
                LblOverAllStatus.Text = "OK";

            }
            //BtnReceive.Visible = true;
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void chkIsActive_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            int J = 0;
            foreach (GridViewRow item in GridReceive.Rows)
            {
                Label OverallBagStatus = (Label)item.FindControl("LblBagStatus");
                CheckBox ChkPostAction = (CheckBox)item.FindControl("chkIsActive");
                if (OverallBagStatus.Text == "NOTOK" && ChkPostAction.Checked == false)
                {
                    J = 1;
                    
                }
                if (J == 1)
                {
                    GridReceive.FooterRow.Cells[6].Text = "NOTOK";
                    //BtnReceive.Visible = false;
                    LblOverAllStatus.Text = "NOTOK";

                }
                else
                {
                    GridReceive.FooterRow.Cells[6].Text = "OK";
                    // BtnReceive.Visible = true;
                    LblOverAllStatus.Text = "OK";

                }

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}