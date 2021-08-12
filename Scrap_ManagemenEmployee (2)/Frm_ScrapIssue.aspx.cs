using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing;
using System.Configuration;
using System.Data.SqlClient;

public partial class Frm_ScrapIssue : System.Web.UI.Page
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
            //ScanBarcode.Attributes.Add("class", "active");
            Label1.Attributes.Add("class", "active");
            GetConsigment();
            FillScrapType();
            BtnIssue.Visible = false;
            objdb.ByProcedure("Sp_ScrapIssue", new string[] { "flag", "Create_By" },
            new string[] { "8", Session["UserCode"].ToString() }, "dataset");

        }
    }
    protected void ddlConsigment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ds = null;
            LblErrorMsg.Text = "";
            lblMsg.Text = "";
            double TotalIssueWt = 0, TotalBagWt = 0;
            ds = objdb.ByProcedure("Sp_ScrapIssue", new string[] { "flag", "Refrence_No", "Site_Id" },
               new string[] { "2", ddlConsigment.SelectedItem.Text, ViewState["SiteId"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        LblConsigmentSiteId.Text = ds.Tables[0].Rows[0]["Site_Id"].ToString();
                        objdb.ByProcedure("Sp_ScrapIssue", new string[] { "flag", "Create_by", "Site_Id" },
                        new string[] { "6", ViewState["UserCode"].ToString(), LblConsigmentSiteId.Text }, "dataset");

                        GridIssue.DataSource = ds.Tables[0];
                        GridIssue.DataBind();
                        foreach (GridViewRow item in GridIssue.Rows)
                        {
                            Label RefrenceNo = (Label)item.FindControl("LblRefrenceNo");
                            Label BagBatchNo = (Label)item.FindControl("LblBag_BatchNo");
                            Label Status = (Label)item.FindControl("LblStatus");
                            TextBox Issue = (TextBox)item.FindControl("TxtIssueWeight");
                            CheckBox Chk = (CheckBox)item.FindControl("chkIsActive");
                            Label ReceiveStatus = (Label)item.FindControl("LblReceiveStatus");
                            Label PostActionStatus = (Label)item.FindControl("LblPostActionStatus");
                            TextBox txtRecWt = (TextBox)item.FindControl("TxtReceiveWeight");

                            txtRecWt.ReadOnly = true;
                            TextBox TxtAllIssueWt = (TextBox)item.FindControl("TxtIssueWeight");
                            TextBox TxtAllBagWt = (TextBox)item.FindControl("TxtBagWeight");
                            Issue.Attributes.Add("Disabled", "Disabled");
                            if (Status.Text == "ISSUED" || ReceiveStatus.Text == "NOTOK")
                            {
                                Chk.Checked = true;
                                Chk.Visible = false;

                            }
                            if (Status.Text == "RECEIVED" && PostActionStatus.Text == "EXCEPTION OK")
                            {
                                Chk.Checked = false;
                                Chk.Visible = true;

                            }

                            if (TxtAllIssueWt.Text.Trim() == null || TxtAllIssueWt.Text.Trim() == "")
                            {

                                TxtAllIssueWt.Text = "0";
                            }
                            else
                            {
                                TotalIssueWt = TotalIssueWt + double.Parse(TxtAllIssueWt.Text);
                                TotalBagWt = TotalBagWt + double.Parse(TxtAllBagWt.Text);

                            }
                            //Issue_Weight.Text = ds.Tables[0].Rows[0]["Issue_Weight"].ToString();
                        }
                        //BtnIssue.Visible = true;
                        ViewState["Tolerance_Weight"] = ds.Tables[0].Rows[0]["Tolerance_IssueWeight"].ToString();
                        GridIssue.FooterRow.Cells[0].Font.Size = 16;
                        GridIssue.FooterRow.Cells[7].Font.Size = 16;
                        GridIssue.FooterRow.Cells[9].Font.Size = 16;

                        GridIssue.FooterRow.Cells[0].Text = "OVERALL";
                        GridIssue.FooterRow.Cells[9].Text = ds.Tables[0].Rows[0]["Overall_WeightIssueStatus"].ToString();
                        LblOverAllStatus.Text = ds.Tables[0].Rows[0]["Overall_WeightIssueStatus"].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    public void GetConsigment()
    {
        try
        {
            ds = null;
            LblErrorMsg.Text = "";
            lblMsg.Text = "";
            ds = objdb.ByProcedure("Sp_ScrapIssue", new string[] { "flag", "Site_Id" },
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
                        PanelISSUE.Visible = true;
                    }
                    else
                    {
                        ddlConsigment.DataSource = ds;
                        ddlConsigment.Items.Clear();
                        ddlConsigment.Items.Insert(0, new ListItem("Select Consigment No", "0"));

                    }
                }
                else
                {
                    ddlConsigment.DataSource = ds;
                    ddlConsigment.Items.Clear();
                    ddlConsigment.Items.Insert(0, new ListItem("Select Consigment No", "0"));

                }
            }
            else
            {
                ddlConsigment.DataSource = ds;
                ddlConsigment.Items.Clear();
                ddlConsigment.Items.Insert(0, new ListItem("Select Consigment No", "0"));
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void BtnIssue_Click(object sender, EventArgs e)
    {
        try
        {

            ds = null;
            LblErrorMsg.Text = "";
            lblMsg.Text = "";
            string Msg = "", Msg1 = "DONE";
            double TotalPadIssueWt = 0, TotalDiaIssueWeight = 0, TotalPadReceiveWeight = 0, TotalDiaReceiveWeight = 0;
            foreach (GridViewRow item in GridIssue.Rows)
            {
                Label RefrenceNo = (Label)item.FindControl("LblRefrenceNo");
                Label BagBatchNo = (Label)item.FindControl("LblBag_BatchNo");
                TextBox Issue_Weight = (TextBox)item.FindControl("TxtIssueWeight");
                Label Status = (Label)item.FindControl("LblStatus");
                Label BagStatus = (Label)item.FindControl("LblBagStatus");
                Label ScrapId = (Label)item.FindControl("lblRowNumber");
                Label ScrapType = (Label)item.FindControl("LblScrapType");
                Label Net_Weight = (Label)item.FindControl("LblNetWeight");
                Label RefSiteId = (Label)item.FindControl("LblSiteId");
                LblConsigmentSiteId.Text = RefSiteId.Text;


                ViewState["ScrapId"] = ScrapId.ToolTip.ToString();
                ViewState["RefrenceNo"] = RefrenceNo.Text;
                CheckBox Chk = (CheckBox)item.FindControl("chkIsActive");
                Label ReceiveStatus = (Label)item.FindControl("LblReceiveStatus");
                Label PostActionStatus = (Label)item.FindControl("LblPostActionStatus");

                if (ScrapType.Text == "PAD")
                {
                    TotalPadReceiveWeight = TotalPadReceiveWeight + double.Parse(Net_Weight.Text);

                }
                else if (ScrapType.Text == "DIA")
                {
                    TotalDiaReceiveWeight = TotalDiaReceiveWeight + double.Parse(Net_Weight.Text);
                }



                if (Chk.Checked == true && ReceiveStatus.Text != "" && Status.Text != "ISSUED" && (ReceiveStatus.Text == "OK" || PostActionStatus.Text == "EXCEPTION OK"))
                {
                    Msg = "A";
                    ds = objdb.ByProcedure("Sp_ScrapIssue", new string[] { "flag", "Refrence_No", "Scrap_Id", "Site_Id", "Bag_BatchNo", "Issue_Weight", "Create_By", "Issue_Status", "Overall_WeightIssueStatus" },
                       new string[] { "3", RefrenceNo.Text, ViewState["ScrapId"].ToString(), LblConsigmentSiteId.Text, BagBatchNo.Text, Issue_Weight.Text, ViewState["UserCode"].ToString(), BagStatus.Text, LblOverAllStatus.Text }, "dataset");

                    objdb.ByProcedure("Sp_ScrapIssue", new string[] { "flag", "Site_Id", "Refrence_No", "Scrap_Id", "Create_By" },
                   new string[] { "4", LblConsigmentSiteId.Text, RefrenceNo.Text, ViewState["ScrapId"].ToString(), ViewState["UserCode"].ToString() }, "dataset");

                    if (ScrapType.Text == "PAD")
                    {
                        TotalPadIssueWt = TotalPadIssueWt + double.Parse(Net_Weight.Text);

                    }
                    else if (ScrapType.Text == "DIA")
                    {
                        TotalDiaIssueWeight = TotalDiaIssueWeight + double.Parse(Net_Weight.Text);
                    }
                }

            }
            if (Msg.ToString() == "")
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Please Select bag");
                return;
            }
            objdb.ByProcedure("Sp_ScrapReceive", new string[] { "flag", "Refrence_No", "Site_Id", "Pad_IssueWt", "Dia_IssueWt", "Pad_ReceiveWt", "Dia_ReceiveWt" },
            new string[] { "8", ViewState["RefrenceNo"].ToString(), LblConsigmentSiteId.Text, TotalPadIssueWt.ToString(), TotalDiaIssueWeight.ToString(), TotalPadReceiveWeight.ToString(), TotalDiaReceiveWeight.ToString() }, "dataset");
            objdb.ByProcedure("Sp_ScrapIssue", new string[] { "flag"},
            new string[] { "9"}, "dataset");
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Scrap Issued Successfully");


            ddlConsigment.SelectedIndex = 0;
            GridIssue.DataSource = null;
            GridIssue.DataBind();

            GetConsigment();
            BtnIssue.Visible = false;
            RadioList.ClearSelection();
            ConsigmentPanel.Visible = false;
            ScanPanel.Visible = false;
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Scrap Issued Successfully");
            TxtScanBarcode.Text = "";
            objdb.ByProcedure("Sp_ScrapIssue", new string[] { "flag", "Create_by", "Site_Id" },
             new string[] { "6", ViewState["UserCode"].ToString(), LblConsigmentSiteId.Text }, "dataset");



        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void TxtIssueWeight_TextChanged(object sender, EventArgs e)
    {
        GridViewRow currentRow = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox txtRecWt = (TextBox)currentRow.FindControl("TxtReceiveWeight");
        TextBox txtBagWt = (TextBox)currentRow.FindControl("TxtBagWeight");
        TextBox txtBagTole = (TextBox)currentRow.FindControl("TxtBagToleranceWt");
        TextBox txtIssueWeight = (TextBox)currentRow.FindControl("TxtIssueWeight");
        Label BagBatch = (Label)currentRow.FindControl("LblBag_BatchNo");
        Label BagStatus = (Label)currentRow.FindControl("LblBagStatus");

        Double PlusBagTolerance = 0, MinusBagTolerance = 0;
        PlusBagTolerance = double.Parse(txtRecWt.Text) + double.Parse(txtBagTole.Text);
        MinusBagTolerance = double.Parse(txtRecWt.Text) - double.Parse(txtBagTole.Text);
        if (txtRecWt.Text == "")
        {
            txtRecWt.Text = "0";
        }
        if (double.Parse(txtIssueWeight.Text) >= MinusBagTolerance && double.Parse(txtIssueWeight.Text) <= PlusBagTolerance)
        {
            BagStatus.Text = "OK";

        }
        else
        {
            BagStatus.Text = "NOTOK";

        }
        double TotalIssueWt = 0, TotalBagWt = 0;
        int I = 0;
        foreach (GridViewRow item in GridIssue.Rows)
        {
            Label OverallBagStatus = (Label)item.FindControl("LblBagStatus");
            Label ReceiveStatus = (Label)item.FindControl("LblReceiveStatus");
            if (OverallBagStatus.Text == "NOTOK" && ReceiveStatus.Text == "OK")
            {
                I = 1;
            }
            TextBox TxtAllIssueWt = (TextBox)item.FindControl("TxtIssueWeight");
            TextBox TxtAllRecBagWt = (TextBox)item.FindControl("TxtReceiveWeight");

            if (TxtAllIssueWt.Text.Trim() == null || TxtAllIssueWt.Text.Trim() == "")
            {

                TxtAllIssueWt.Text = "0";
            }
            else
            {
                TotalIssueWt = TotalIssueWt + double.Parse(TxtAllIssueWt.Text);
                TotalBagWt = TotalBagWt + double.Parse(TxtAllIssueWt.Text);
            }
        }
        GridIssue.FooterRow.Cells[0].Font.Size = 16;
        GridIssue.FooterRow.Cells[7].Font.Size = 16;
        GridIssue.FooterRow.Cells[8].Font.Size = 16;

        GridIssue.FooterRow.Cells[0].Text = "OVERALL";
        GridIssue.FooterRow.Cells[7].Text = TotalIssueWt.ToString();
        if (I == 1)
        {
            //GridIssue.FooterRow.Cells[8].Text = "NOTOK";
            //BtnIssue.Visible = false;
            LblOverAllStatus.Text = "NOTOK";

        }
        else
        {
            GridIssue.FooterRow.Cells[7].Text = "OK";
            BtnIssue.Visible = true;
            LblOverAllStatus.Text = "OK";

        }
        //double TotalIssueWt = 0, TotalBagWt = 0, PlusOverAllToloerance = 0, MinusOverAllToloerance;

        //foreach (GridViewRow item in GridIssue.Rows)
        //{
        //    TextBox TxtAllIssueWt = (TextBox)item.FindControl("TxtIssueWeight");
        //    TextBox TxtAllRecBagWt = (TextBox)item.FindControl("TxtReceiveWeight");

        //    if (TxtAllIssueWt.Text.Trim() == null || TxtAllIssueWt.Text.Trim() == "")
        //    {

        //        TxtAllIssueWt.Text = "0";
        //    }
        //    else
        //    {
        //        TotalIssueWt = TotalIssueWt + double.Parse(TxtAllIssueWt.Text);
        //        TotalBagWt = TotalBagWt + double.Parse(TxtAllRecBagWt.Text);

        //    }
        //    GridIssue.FooterRow.Cells[0].Font.Size = 16;
        //    GridIssue.FooterRow.Cells[6].Font.Size = 16;
        //    GridIssue.FooterRow.Cells[7].Font.Size = 16;

        //    GridIssue.FooterRow.Cells[0].Text = "OVERALL";
        //    GridIssue.FooterRow.Cells[6].Text = TotalIssueWt.ToString();

        //}

        //double ToleranceWt = Convert.ToDouble(ViewState["Tolerance_Weight"].ToString());
        //PlusOverAllToloerance = TotalBagWt + ToleranceWt;
        //MinusOverAllToloerance = TotalBagWt - ToleranceWt;

        //if (double.Parse(TotalIssueWt.ToString()) >= MinusOverAllToloerance && double.Parse(TotalIssueWt.ToString()) <= PlusOverAllToloerance)
        //{
        //    GridIssue.FooterRow.Cells[7].Text = "OK";
        //    BtnIssue.Visible = true;
        //    LblOverAllStatus.Text = "OK";
        //}
        //else
        //{

        //    GridIssue.FooterRow.Cells[7].Text = "NOTOK";
        //    BtnIssue.Visible = false;
        //    LblOverAllStatus.Text = "NOTOK";
        //}
    }
    protected void chkIsActive_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow currentRow = ((GridViewRow)((CheckBox)sender).NamingContainer);
        TextBox txtIssueWeight = (TextBox)currentRow.FindControl("TxtIssueWeight");
        Label BagStatus = (Label)currentRow.FindControl("LblBagStatus");
        CheckBox Chk = (CheckBox)currentRow.FindControl("chkIsActive");
        if (Chk.Checked == true)
        {
            txtIssueWeight.Text = "0";
            // txtIssueWeight.Attributes.Remove("disabled");
            BagStatus.Text = "";
        }
        else
        {
            txtIssueWeight.Text = "0";
            txtIssueWeight.Attributes.Add("Disabled", "Disabled");
            BagStatus.Text = "";

        }
        TextBox txtRecWt = (TextBox)currentRow.FindControl("TxtReceiveWeight");
        TextBox txtBagWt = (TextBox)currentRow.FindControl("TxtBagWeight");
        TextBox txtBagTole = (TextBox)currentRow.FindControl("TxtBagToleranceWt");

        Label BagBatch = (Label)currentRow.FindControl("LblBag_BatchNo");

        Double PlusBagTolerance = 0, MinusBagTolerance = 0;
        PlusBagTolerance = double.Parse(txtRecWt.Text) + double.Parse(txtBagTole.Text);
        MinusBagTolerance = double.Parse(txtRecWt.Text) - double.Parse(txtBagTole.Text);
        if (txtRecWt.Text == "")
        {
            txtRecWt.Text = "0";
        }
        if (double.Parse(txtIssueWeight.Text) >= MinusBagTolerance && double.Parse(txtIssueWeight.Text) <= PlusBagTolerance)
        {
            //BagStatus.Text = "OK";

        }
        else
        {
            //BagStatus.Text = "NOTOK";

        }

        double TotalIssueWt = 0, TotalBagWt = 0, PlusOverAllToloerance = 0, MinusOverAllToloerance;

        foreach (GridViewRow item in GridIssue.Rows)
        {
            TextBox TxtAllIssueWt = (TextBox)item.FindControl("TxtIssueWeight");
            TextBox TxtAllBagWt = (TextBox)item.FindControl("TxtBagWeight");

            if (TxtAllIssueWt.Text.Trim() == null || TxtAllIssueWt.Text.Trim() == "")
            {

                TxtAllIssueWt.Text = "0";
            }
            else
            {
                TotalIssueWt = TotalIssueWt + double.Parse(TxtAllIssueWt.Text);
                TotalBagWt = TotalBagWt + double.Parse(TxtAllBagWt.Text);

            }
            GridIssue.FooterRow.Cells[0].Font.Size = 16;
            GridIssue.FooterRow.Cells[7].Font.Size = 16;
            GridIssue.FooterRow.Cells[8].Font.Size = 16;

            GridIssue.FooterRow.Cells[0].Text = "OVERALL";
            GridIssue.FooterRow.Cells[7].Text = TotalIssueWt.ToString();

        }

        double ToleranceWt = Convert.ToDouble(ViewState["Tolerance_Weight"].ToString());
        PlusOverAllToloerance = TotalBagWt + ToleranceWt;
        MinusOverAllToloerance = TotalBagWt - ToleranceWt;

        //if (double.Parse(TotalIssueWt.ToString()) >= MinusOverAllToloerance && double.Parse(TotalIssueWt.ToString()) <= PlusOverAllToloerance)
        //{
        //    GridIssue.FooterRow.Cells[7].Text = "OK";
        //    BtnIssue.Visible = true;
        //    LblOverAllStatus.Text = "OK";
        //}
        //else
        //{

        //    GridIssue.FooterRow.Cells[7].Text = "NOTOK";
        //    BtnIssue.Visible = false;
        //    LblOverAllStatus.Text = "NOTOK";
        //}


    }
    //protected void checkAll_CheckedChanged(object sender, EventArgs e)
    //{


    //    foreach (GridViewRow item in GridIssue.Rows)
    //    {
    //        CheckBox Chk = (CheckBox)item.FindControl("chkIsActive");
    //        CheckBox ChkAll = (CheckBox)item.FindControl("checkAll");

    //    }
    //}
    protected void checkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox CheckAll = (CheckBox)GridIssue.HeaderRow.FindControl("checkAll");

        foreach (GridViewRow item in GridIssue.Rows)
        {

            CheckBox Chk = (CheckBox)item.FindControl("chkIsActive");
            TextBox txtIssueWeight = (TextBox)item.FindControl("TxtIssueWeight");
            Label BagStatus = (Label)item.FindControl("LblBagStatus");
            Label ReceiveStatus = (Label)item.FindControl("LblReceiveStatus");
            Label PostActionStatus = (Label)item.FindControl("LblPostActionStatus");
            if (CheckAll.Checked == true && BagStatus.Text == "" && ReceiveStatus.Text == "OK")
            {
                Chk.Checked = true;
                txtIssueWeight.Text = "0";
                // txtIssueWeight.Attributes.Remove("disabled");
                BagStatus.Text = "";

            }
            else if (CheckAll.Checked == true && BagStatus.Text == "" && ReceiveStatus.Text == "NOTOK" && PostActionStatus.Text == "EXCEPTION OK")
            {
                Chk.Checked = true;
                txtIssueWeight.Text = "0";
                //txtIssueWeight.Attributes.Remove("disabled");
                BagStatus.Text = "";

            }
            else if (CheckAll.Checked == false && BagStatus.Text == "")
            {
                Chk.Checked = false;
                txtIssueWeight.Text = "0";
                txtIssueWeight.Attributes.Add("Disabled", "Disabled");
                BagStatus.Text = "";
                LblOverAllStatus.Text = "";
                GridIssue.FooterRow.Cells[6].Text = "0";
                GridIssue.FooterRow.Cells[7].Text = "";
                BtnIssue.Visible = false;


            }

        }
    }
    protected void checkAllIssueWt_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

            CheckBox checkAllIssueWt = (CheckBox)GridIssue.HeaderRow.FindControl("checkAllIssueWt");
            if (checkAllIssueWt.Checked == true)
            {
                double TotalIssueWt = 0;
                foreach (GridViewRow item in GridIssue.Rows)
                {

                    TextBox txtRecWt = (TextBox)item.FindControl("TxtReceiveWeight");
                    TextBox txtIssueWeight = (TextBox)item.FindControl("TxtIssueWeight");
                    CheckBox Chk = (CheckBox)item.FindControl("chkIsActive");
                    Label BagStatus = (Label)item.FindControl("LblBagStatus");
                    Label ReceiveStatus = (Label)item.FindControl("LblReceiveStatus");
                    Label NetWeight = (Label)item.FindControl("LblNetWeight");

                    TextBox txtBagWt = (TextBox)item.FindControl("TxtBagWeight");
                    TextBox txtBagTole = (TextBox)item.FindControl("TxtBagToleranceWt");
                    Label PostActionStatus = (Label)item.FindControl("LblPostActionStatus");
                    if (Chk.Checked == true && BagStatus.Text == "" && (ReceiveStatus.Text == "OK" || PostActionStatus.Text == "EXCEPTION OK"))
                    {
                        txtIssueWeight.Text = NetWeight.Text;
                        //txtIssueWeight.Attributes.Remove("disabled");
                    }

                    Double PlusBagTolerance = 0, MinusBagTolerance = 0;
                    PlusBagTolerance = double.Parse(txtRecWt.Text) + double.Parse(txtBagTole.Text);
                    MinusBagTolerance = double.Parse(txtRecWt.Text) - double.Parse(txtBagTole.Text);
                    if (txtRecWt.Text == "")
                    {
                        txtRecWt.Text = "0";
                    }

                    if (Chk.Checked == true)
                    {

                        if (double.Parse(txtIssueWeight.Text) >= MinusBagTolerance && double.Parse(txtIssueWeight.Text) <= PlusBagTolerance)
                        {
                            BagStatus.Text = "OK";

                        }
                        else
                        {
                            BagStatus.Text = "NOTOK";

                        }
                    }
                    double TotalBagWt = 0, PlusOverAllToloerance = 0, MinusOverAllToloerance;


                    TextBox TxtAllIssueWt = (TextBox)item.FindControl("TxtIssueWeight");
                    TextBox TxtAllBagWt = (TextBox)item.FindControl("TxtBagWeight");

                    if (TxtAllIssueWt.Text.Trim() == null || TxtAllIssueWt.Text.Trim() == "")
                    {

                        TxtAllIssueWt.Text = "0";
                    }
                    else
                    {
                        TotalIssueWt = TotalIssueWt + double.Parse(TxtAllIssueWt.Text);
                        TotalBagWt = TotalBagWt + double.Parse(TxtAllBagWt.Text);

                    }




                    double ToleranceWt = Convert.ToDouble(ViewState["Tolerance_Weight"].ToString());
                    PlusOverAllToloerance = TotalIssueWt + ToleranceWt;
                    MinusOverAllToloerance = TotalIssueWt - ToleranceWt;

                    if (double.Parse(TotalIssueWt.ToString()) >= MinusOverAllToloerance && double.Parse(TotalIssueWt.ToString()) <= PlusOverAllToloerance)
                    {
                        GridIssue.FooterRow.Cells[9].Text = "OK";
                        BtnIssue.Visible = true;
                        LblOverAllStatus.Text = "OK";
                    }
                    else
                    {

                        GridIssue.FooterRow.Cells[9].Text = "NOTOK";
                        BtnIssue.Visible = false;
                        LblOverAllStatus.Text = "NOTOK";
                    }
                }
                GridIssue.FooterRow.Cells[0].Font.Size = 16;
                GridIssue.FooterRow.Cells[7].Font.Size = 16;
                GridIssue.FooterRow.Cells[8].Font.Size = 16;

                GridIssue.FooterRow.Cells[0].Text = "OVERALL";
                GridIssue.FooterRow.Cells[7].Text = TotalIssueWt.ToString();
            }
            else
            {

                foreach (GridViewRow item in GridIssue.Rows)
                {

                    TextBox txtRecWt = (TextBox)item.FindControl("TxtReceiveWeight");
                    TextBox txtIssueWeight = (TextBox)item.FindControl("TxtIssueWeight");
                    CheckBox Chk = (CheckBox)item.FindControl("chkIsActive");
                    Label BagStatus = (Label)item.FindControl("LblBagStatus");
                    Label Status = (Label)item.FindControl("LblStatus");
                    if (Status.Text != "ISSUED")
                    {

                        txtIssueWeight.Text = "0";

                        BagStatus.Text = "";
                        LblOverAllStatus.Text = "";

                        GridIssue.FooterRow.Cells[6].Text = "0";
                        GridIssue.FooterRow.Cells[7].Text = "";
                        BtnIssue.Visible = false;
                    }
                }
            }

        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillScrapType()
    {
        try
        {
            ds = objdb.ByProcedure("Sp_ScrapType", new string[] { "flag", "Site_id" }, new string[] { "5", "1" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlScrapType.DataTextField = "Scrap_Type";
                ddlScrapType.DataValueField = "Scrap_code";
                ddlScrapType.DataSource = ds;
                ddlScrapType.DataBind();
                ddlScrapType.Items.Insert(0, new ListItem("Select ALL Scrap Type", "0"));
                ddlScrapType.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlScrap_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gr in GridIssue.Rows)
            {
                Label ScrapType = (Label)gr.FindControl("LblScrapType");

                if (ScrapType.Text != ddlScrapType.SelectedValue && ddlScrapType.SelectedValue != "0")
                {
                    gr.Visible = false;
                }
                else
                {
                    gr.Visible = true;

                }
            }
        }
        catch (Exception Ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Ex.Message.ToString());
            //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", objdb.Alert("fa-exclamation", "alert-danger", "Sorry!", Ex.Message), true);
        }
        finally
        {
        }
    }
    protected void TxtScanBarcode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ds = null;
            LblErrorMsg.Text = "";
            lblMsg.Text = "";
            double TotalIssueWt = 0, TotalBagWt = 0;


            ds = objdb.ByProcedure("Sp_ScrapIssue", new string[] { "flag", "Barcode", "Site_Id" },
               new string[] { "5", TxtScanBarcode.Text, LblConsigmentSiteId.Text }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataSet DsNew = objdb.ByProcedure("Sp_ScrapIssue", new string[] { "flag", "Scrap_Id", "Refrence_No", "Status", "Bag_BatchNo", "Receive_Weight", "Bag_Weight", "Tolerance_Issuebag"
                           ,"Issue_Weight","Issue_Status", "Scrap_Type", "Receive_Status", "Post_ActionStatus", "Create_by", "Site_id","TierWeight","NetWeight" },
                new string[] { "7", ds.Tables[0].Rows[0]["Scrap_Id"].ToString(), ds.Tables[0].Rows[0]["Refrence_No"].ToString(), ds.Tables[0].Rows[0]["Status"].ToString(), ds.Tables[0].Rows[0]["Bag_BatchNo"].ToString(), ds.Tables[0].Rows[0]["Receive_Weight"].ToString(), ds.Tables[0].Rows[0]["Bag_Weight"].ToString(), ds.Tables[0].Rows[0]["Tolerance_Issuebag"].ToString()
                     ,ds.Tables[0].Rows[0]["Issue_Weight"].ToString(),ds.Tables[0].Rows[0]["Issue_Status"].ToString(), ds.Tables[0].Rows[0]["Scrap_Type"].ToString(), ds.Tables[0].Rows[0]["Receive_Status"].ToString(), ds.Tables[0].Rows[0]["Post_ActionStatus"].ToString(), ViewState["UserCode"].ToString(), LblConsigmentSiteId.Text,ds.Tables[0].Rows[0]["TierWeight"].ToString(),ds.Tables[0].Rows[0]["NetWeight"].ToString() }, "dataset");
                        if (DsNew != null)
                        {
                            if (DsNew.Tables.Count > 0)
                            {
                                if (DsNew.Tables[0].Rows.Count > 0)
                                {
                                    GridIssue.DataSource = DsNew.Tables[0];
                                    GridIssue.DataBind();
                                }
                            }
                        }


                        //  ddlConsigment.SelectedValue = ds.Tables[0].Rows[0]["Refrence_no"].ToString();
                        CheckBox checkAllIssueWt = (CheckBox)GridIssue.HeaderRow.FindControl("checkAllIssueWt");
                        foreach (GridViewRow item in GridIssue.Rows)
                        {
                            Label RefrenceNo = (Label)item.FindControl("LblRefrenceNo");
                            Label BagBatchNo = (Label)item.FindControl("LblBag_BatchNo");
                            Label Status = (Label)item.FindControl("LblStatus");
                            TextBox Issue = (TextBox)item.FindControl("TxtIssueWeight");
                            CheckBox Chk = (CheckBox)item.FindControl("chkIsActive");
                            Label ReceiveStatus = (Label)item.FindControl("LblReceiveStatus");
                            Label PostActionStatus = (Label)item.FindControl("LblPostActionStatus");


                            TextBox TxtAllIssueWt = (TextBox)item.FindControl("TxtIssueWeight");
                            TextBox TxtAllBagWt = (TextBox)item.FindControl("TxtBagWeight");
                            Issue.Attributes.Add("Disabled", "Disabled");
                            if (Status.Text == "ISSUED" || ReceiveStatus.Text == "NOTOK")
                            {
                                Chk.Checked = true;
                                Chk.Visible = false;
                                // checkAllIssueWt.Visible = false;

                            }
                            if (Status.Text == "RECEIVED" && PostActionStatus.Text == "EXCEPTION OK")
                            {
                                Chk.Checked = false;
                                Chk.Visible = true;
                                //checkAllIssueWt.Visible = true;

                            }

                            if (TxtAllIssueWt.Text.Trim() == null || TxtAllIssueWt.Text.Trim() == "")
                            {

                                TxtAllIssueWt.Text = "0";
                            }
                            else
                            {
                                TotalIssueWt = TotalIssueWt + double.Parse(TxtAllIssueWt.Text);
                                TotalBagWt = TotalBagWt + double.Parse(TxtAllBagWt.Text);

                            }
                            //Issue_Weight.Text = ds.Tables[0].Rows[0]["Issue_Weight"].ToString();
                        }
                        //BtnIssue.Visible = true;
                        ViewState["Tolerance_Weight"] = ds.Tables[0].Rows[0]["Tolerance_IssueWeight"].ToString();
                        GridIssue.FooterRow.Cells[0].Font.Size = 16;
                        GridIssue.FooterRow.Cells[6].Font.Size = 16;
                        GridIssue.FooterRow.Cells[9].Font.Size = 16;

                        GridIssue.FooterRow.Cells[0].Text = "OVERALL";
                        GridIssue.FooterRow.Cells[9].Text = ds.Tables[0].Rows[0]["Overall_WeightIssueStatus"].ToString();
                        LblOverAllStatus.Text = ds.Tables[0].Rows[0]["Overall_WeightIssueStatus"].ToString();
                        TxtScanBarcode.Text = "";
                        TxtScanBarcode.Focus();
                    }
                }
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    public void GetQrcodeData()
    {
        try
        {
            ds = null;
            LblErrorMsg.Text = "";
            lblMsg.Text = "";
            double TotalIssueWt = 0, TotalBagWt = 0;


            ds = objdb.ByProcedure("Sp_ScrapIssue", new string[] { "flag", "Barcode", "Site_Id" },
               new string[] { "5", TxtScanBarcode.Text, ViewState["SiteId"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataSet DsNew = objdb.ByProcedure("Sp_ScrapIssue", new string[] { "flag", "Scrap_Id", "Refrence_No", "Status", "Bag_BatchNo", "Receive_Weight", "Bag_Weight", "Tolerance_Issuebag"
                           ,"Issue_Weight","Issue_Status", "Scrap_Type", "Receive_Status", "Post_ActionStatus", "Create_by", "Site_id","TierWeight","NetWeight" },
                new string[] { "7", ds.Tables[0].Rows[0]["Scrap_Id"].ToString(), ds.Tables[0].Rows[0]["Refrence_No"].ToString(), ds.Tables[0].Rows[0]["Status"].ToString(), ds.Tables[0].Rows[0]["Bag_BatchNo"].ToString(), ds.Tables[0].Rows[0]["Receive_Weight"].ToString(), ds.Tables[0].Rows[0]["Bag_Weight"].ToString(), ds.Tables[0].Rows[0]["Tolerance_Issuebag"].ToString()
                     ,ds.Tables[0].Rows[0]["Issue_Weight"].ToString(),ds.Tables[0].Rows[0]["Issue_Status"].ToString(), ds.Tables[0].Rows[0]["Scrap_Type"].ToString(), ds.Tables[0].Rows[0]["Receive_Status"].ToString(), ds.Tables[0].Rows[0]["Post_ActionStatus"].ToString(), ViewState["UserCode"].ToString(), ds.Tables[0].Rows[0]["Site_Id"].ToString(),ds.Tables[0].Rows[0]["TierWeight"].ToString(),ds.Tables[0].Rows[0]["NetWeight"].ToString() }, "dataset");
                        if (DsNew != null)
                        {
                            if (DsNew.Tables.Count > 0)
                            {
                                if (DsNew.Tables[0].Rows.Count > 0)
                                {
                                    GridIssue.DataSource = DsNew.Tables[0];
                                    GridIssue.DataBind();
                                }
                            }
                        }


                        //  ddlConsigment.SelectedValue = ds.Tables[0].Rows[0]["Refrence_no"].ToString();
                        CheckBox checkAllIssueWt = (CheckBox)GridIssue.HeaderRow.FindControl("checkAllIssueWt");
                        foreach (GridViewRow item in GridIssue.Rows)
                        {
                            Label RefrenceNo = (Label)item.FindControl("LblRefrenceNo");
                            Label BagBatchNo = (Label)item.FindControl("LblBag_BatchNo");
                            Label Status = (Label)item.FindControl("LblStatus");
                            TextBox Issue = (TextBox)item.FindControl("TxtIssueWeight");
                            CheckBox Chk = (CheckBox)item.FindControl("chkIsActive");
                            Label ReceiveStatus = (Label)item.FindControl("LblReceiveStatus");
                            Label PostActionStatus = (Label)item.FindControl("LblPostActionStatus");


                            TextBox TxtAllIssueWt = (TextBox)item.FindControl("TxtIssueWeight");
                            TextBox TxtAllBagWt = (TextBox)item.FindControl("TxtBagWeight");
                            Issue.Attributes.Add("Disabled", "Disabled");
                            if (Status.Text == "ISSUED" || ReceiveStatus.Text == "NOTOK")
                            {
                                Chk.Checked = true;
                                Chk.Visible = false;
                                // checkAllIssueWt.Visible = false;

                            }
                            if (Status.Text == "RECEIVED" && PostActionStatus.Text == "EXCEPTION OK")
                            {
                                Chk.Checked = false;
                                Chk.Visible = true;
                                //checkAllIssueWt.Visible = true;

                            }

                            if (TxtAllIssueWt.Text.Trim() == null || TxtAllIssueWt.Text.Trim() == "")
                            {

                                TxtAllIssueWt.Text = "0";
                            }
                            else
                            {
                                TotalIssueWt = TotalIssueWt + double.Parse(TxtAllIssueWt.Text);
                                TotalBagWt = TotalBagWt + double.Parse(TxtAllBagWt.Text);

                            }
                            //Issue_Weight.Text = ds.Tables[0].Rows[0]["Issue_Weight"].ToString();
                        }
                        //BtnIssue.Visible = true;
                        ViewState["Tolerance_Weight"] = ds.Tables[0].Rows[0]["Tolerance_IssueWeight"].ToString();
                        GridIssue.FooterRow.Cells[0].Font.Size = 16;
                        GridIssue.FooterRow.Cells[6].Font.Size = 16;
                        GridIssue.FooterRow.Cells[9].Font.Size = 16;

                        GridIssue.FooterRow.Cells[0].Text = "OVERALL";
                        GridIssue.FooterRow.Cells[9].Text = ds.Tables[0].Rows[0]["Overall_WeightIssueStatus"].ToString();
                        LblOverAllStatus.Text = ds.Tables[0].Rows[0]["Overall_WeightIssueStatus"].ToString();
                        TxtScanBarcode.Text = "";
                        TxtScanBarcode.Focus();
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "QR CODE NOT FOUND");
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "QR CODE NOT FOUND");
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "QR CODE NOT FOUND");
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void RadioList_TextChanged(object sender, EventArgs e)
    {
        try
        {

            objdb.ByProcedure("Sp_ScrapIssue", new string[] { "flag", "Create_By" },
           new string[] { "8", Session["UserCode"].ToString() }, "dataset");
            GridIssue.DataSource = null;
            GridIssue.DataBind();
            if (RadioList.SelectedValue == "1")
            {

                ConsigmentPanel.Visible = true;
                ScanPanel.Visible = false;
                TxtScanBarcode.Text = "";
                ddlConsigment.SelectedIndex = 0;
                ddlScrapType.SelectedIndex = 0;

            }
            else
            {
                ConsigmentPanel.Visible = false;
                ScanPanel.Visible = true;

                TxtScanBarcode.Text = "";
                ddlConsigment.SelectedIndex = 0;
                ddlScrapType.SelectedIndex = 0;

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        GetQrcodeData();
    }


}