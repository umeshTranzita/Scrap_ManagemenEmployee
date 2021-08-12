using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Net.Sockets;
using System.Text;
using System.IO.Ports;

public partial class FrmScrapLoad : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDB objdb = new common();
    CultureInfo cult = new CultureInfo("gu-IN", true);

    TcpClient clientSocket = new TcpClient();
    static bool _continue;
    //static SerialPort _serialPort;  
    SerialPort _serialPort = null;
    string @out = "";

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

            lblMsg.Text = "";
            PanelScrap.Visible = true;

            BagNo.Attributes.Add("class", "active");
            BagBatchNo.Attributes.Add("class", "active");
            BagWeight.Attributes.Add("class", "active");
            TruckNo.Attributes.Add("class", "active");
            InitailWeight.Attributes.Add("class", "active");
            LblTareWeight.Attributes.Add("class", "active");
            LblBarcode.Attributes.Add("class", "active");
            TxtRefrenceNo.Attributes.Add("disabled", "disabled");

            ViewState["DepartId"] = "5";//WareHouse DepartmenntId
            ViewState["SiteId"] = Session["SiteId"].ToString();
            ViewState["UserCode"] = Session["UserCode"].ToString();

            //TxtBagNo.Attributes.Add("disabled", "disabled");
            //TxtBagBatchNo.Attributes.Add("disabled", "disabled");
            ViewState["ApplicationStatus"] = "NEW";
            FillDepartment();
            //FillScrapType();
            GetSiteCode();
            GetRefNo();
            //GetData();
            //GetMaxBagNo();
            GetDraftData();
            AccessControl();
            ViewTruckDetail();

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/FrmScrapLoad.aspx");
        // ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#basicExampleModal').modal('toggle');</script>");
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
                        if (ds.Tables[0].Rows[0]["AccessType"].ToString() == "TruckWeight")
                        {
                            TxtInitailWeight.Visible = true;
                            InitailWeight.Visible = true;
                        }
                        else
                        {
                            TxtInitailWeight.Visible = false;
                            InitailWeight.Visible = false;
                        }

                        if (ds.Tables[1].Rows[0]["Is_Active"].ToString() == "YES")
                        {
                            ViewState["QRCODE"] = 1;
                            // SpamBarcode.Visible = true;
                        }
                        else
                        {

                            ViewState["QRCODE"] = 0;
                            //SpamBarcode.Visible = false;
                        }
                    }
                    else
                    {
                        TxtInitailWeight.Visible = false;
                        InitailWeight.Visible = false;
                    }
                }
                else
                {
                    TxtInitailWeight.Visible = false;
                    InitailWeight.Visible = false;
                }
            }
            else
            {
                TxtInitailWeight.Visible = false;
                InitailWeight.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDepartment()
    {
        try
        {
            ds = objdb.ByProcedure("Sp_ProjectMaster", new string[] { "flag", "SiteID" }, new string[] { "9", ViewState["SiteId"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlDepartment.DataTextField = "DepartmentName";
                ddlDepartment.DataValueField = "DepartmentID";
                ddlDepartment.DataSource = ds;
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("Select Department", "0"));
                ddlDepartment.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    //protected void FillScrapType()
    //{
    //    try
    //    {
    //        ds = objdb.ByProcedure("Sp_ScrapType", new string[] { "flag", "Site_Id" }, new string[] { "5", ViewState["SiteId"].ToString() }, "dataset");
    //        if (ds.Tables[0].Rows.Count != 0)
    //        {
    //            ddlScrapType.DataTextField = "Scrap_Type";
    //            ddlScrapType.DataValueField = "Scrap_code";
    //            ddlScrapType.DataSource = ds;
    //            ddlScrapType.DataBind();
    //            ddlScrapType.Items.Insert(0, new ListItem("Select Scrap Type", "0"));
    //            ddlScrapType.SelectedIndex = 0;
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    protected void GetSiteCode()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("Sp_ScrapLoad", new string[] { "flag", "SiteID" },
                new string[] { "0", ViewState["SiteId"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ViewState["SiteCode"] = ds.Tables[0].Rows[0]["SiteCode"].ToString();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetMaxBagNo()
    {
        try
        {
            ds = null;
            DateTime dt = System.DateTime.Now;
            string Year = Convert.ToString(dt.Year);
            string Month = Convert.ToString(dt.Month);
            string Date = Convert.ToString(dt.Day);
            string Hour = Convert.ToString(dt.Hour);
            string Minute = Convert.ToString(dt.Minute);
            if (ViewState["ApplicationStatus"].ToString() == "NEW")
            {
                ViewState["Create_Date"] = Convert.ToDateTime(System.DateTime.Now, cult).ToString("yyyy/MM/dd");

            }
            else
            {
                ViewState["Create_Date"] = Convert.ToDateTime(ViewState["Create_Date"], cult).ToString("yyyy/MM/dd");
            }
            ds = objdb.ByProcedure("Sp_ScrapLoad", new string[] { "flag", "Scrap_Id", "Create_Date", "Site_Id", "SMTrukGatePass_Id" },
                new string[] { "26", ViewState["Scrap_Id"].ToString(), ViewState["Create_Date"].ToString(), ViewState["SiteId"].ToString(), ddlTruckNo.SelectedValue }, "dataset");


            if (ds.Tables[0].Rows.Count != 0)
            {
                string Code = "";
                if (ddlScrapCategory.SelectedValue == "1")
                {
                    Code = ddlScrapType.SelectedValue;
                }
                else
                {
                    Code = "GEN";
                }


                TxtBagNo.Text = ds.Tables[0].Rows[0]["Bag_No"].ToString();
                TxtBagBatchNo.Text = ViewState["SiteCode"].ToString() + '-' + Code.ToString() + '-' + Year + '-' + Month + '-' + Date + '-' + Hour + '-' + Minute + '-' + ViewState["AfterScrap_Id"].ToString() + '-' + TxtBagNo.Text;
            }
            else
            {
                TxtBagNo.Text = ds.Tables[0].Rows[0]["Bag_No"].ToString();

                TxtBagBatchNo.Text = ViewState["SiteCode"].ToString() + '-' + Year + '-' + Month + '-' + Date + '-' + Hour + '-' + Minute + '-' + ViewState["AfterScrap_Id"].ToString() + '-' + TxtBagNo.Text;

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetRefNo()
    {
        try
        {
            ds = null;

            ds = objdb.ByProcedure("Sp_ScrapLoad", new string[] { "flag", "Site_Id" },
                new string[] { "1", ViewState["SiteId"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ViewState["Scrap_Id"] = ds.Tables[0].Rows[0]["Scrap_Id"].ToString();
                ViewState["AfterScrap_Id"] = "00" + "" + ds.Tables[0].Rows[0]["Scrap_Id"].ToString();
            }
            ds = null;
            DateTime dt = System.DateTime.Now;
            string Year = Convert.ToString(dt.Year);
            string Month = Convert.ToString(dt.Month);
            string Date = Convert.ToString(dt.Day);
            ds = objdb.ByProcedure("Sp_ProjectMaster", new string[] { "flag", "SiteID", "DepartmentId" }, new string[] { "10", ViewState["SiteId"].ToString(), "5" }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ViewState["DepartCode"] = ds.Tables[0].Rows[0]["DepartmentCode"].ToString();
                        // LblRefNo.InnerText = ViewState["SiteCode"].ToString() + '-' + "PG" + '-' + ds.Tables[0].Rows[0]["DepartmentCode"].ToString() + '-' + Year + '-' + Month + '-' + Date + '-' + ViewState["AfterScrap_Id"].ToString();
                        LblRefNo.InnerText = ViewState["SiteCode"].ToString() + '-' + Year + '-' + Month + '-' + Date + '-' + ViewState["AfterScrap_Id"].ToString();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void TxtTruckNo_TextChanged(object sender, EventArgs e)
    {
        DateTime dt = System.DateTime.Now;
        string Year = Convert.ToString(dt.Year);
        string Month = Convert.ToString(dt.Month);
        string Date = Convert.ToString(dt.Day);
        TxtRefrenceNo.Text = "M" + '-' + ViewState["DepartCode"].ToString() + '-' + Year + '-' + Month + '-' + Date;
    }
    protected void Btn_Add_Click(object sender, EventArgs e)
    {
        try
        {

            lblMsg.Text = "";
            string msg = "";
            string LoadStatus = "";

            LoadStatus = "SAVE AS DRAFT";

            GetMaxBagNo();

            //if (ViewState["ApplicationStatus"].ToString() == "NEW")
            //{
            //    LoadStatus = "Create";
            //}
            //else
            //{
            //    LoadStatus = "SAVE AS DRAFT";
            //}

            if (ddlTruckNo.SelectedIndex == 0)
            {
                msg += "PLEASE SELECT Truck No.\\n";
            }

            if (ddlScrapCategory.SelectedIndex == 0)
            {
                msg += "PLEASE SELECT Category.\\n";
            }

            if (ddlBu.SelectedIndex == 0)
            {
                msg += "PLEASE SELECT BU.\\n";
            }
            if (ddlScrapType.SelectedIndex == 0)
            {
                msg += "PLEASE SELECT SCRAP TYPE.\\n";
            }
            else if (TxtBagWeight.Text == "0" || TxtBagWeight.Text == "" || TxtBagWeight.Text == null)
            {
                msg += "PLEASE ENTER SCRAP BAG WEIGHT.\\n";
            }
            if (ViewState["QRCODE"].ToString() == "1")
            {
                if (ddlScrapCategory.SelectedValue == "1")
                {
                    if (TxtBarcode.Text == "")
                    {
                        msg += "PLEASE ENTER QR CODE.\\n";
                    }
                }
            }
            if (msg == "")
            {
                ds = null;

                //------------------------------------------------------Tier Weight Calculation Start---------------------------------------------------//
                Double NetWeight = ((double.Parse(TxtBagWeight.Text) * 1000) - double.Parse(TxtTareWeight.Text)) / 1000;


                if (TxtBagWeight.Text == "")
                {
                    TxtBagWeight.Text = "0";
                }

                if (TxtTareWeight.Text == "")
                {
                    NetWeight = 0;
                }
                //------------------------------------------------------Tier Weight Calculation END---------------------------------------------------//

                ds = null;
                ds = objdb.ByProcedure("Sp_ScrapLoad", new string[] { "flag", "Scrap_Id", "Bag_No", "Bag_BatchNo", "Bag_Weight", "Create_By", "Site_Id", "Scrap_Type", "Status", "Barcode", "LoadTierWeight", "LoadNetWeight" },
                   new string[] { "19", ViewState["Scrap_Id"].ToString(), TxtBagNo.Text, TxtBagBatchNo.Text, TxtBagWeight.Text, ViewState["UserCode"].ToString(), ViewState["SiteId"].ToString(), ddlScrapType.SelectedValue, LoadStatus.ToString(), TxtBarcode.Text, TxtTareWeight.Text, NetWeight.ToString() }, "dataset");
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + ds.Tables[0].Rows[0]["Msg"].ToString() + "');", true);
                            return;
                        }
                    }
                }


                objdb.ByProcedure("Sp_ScrapLoad", new string[] { "flag", "Scrap_Id", "Bag_No", "Bag_BatchNo", "Bag_Weight", "Create_By", "Site_Id", "Scrap_Type", "Status", "Barcode", "LoadTierWeight", "LoadNetWeight", "ScrapCategoryId", "BU", "SMTrukGatePass_Id" },
                   new string[] { "3", ViewState["Scrap_Id"].ToString(), TxtBagNo.Text, TxtBagBatchNo.Text, TxtBagWeight.Text, ViewState["UserCode"].ToString(), ViewState["SiteId"].ToString(), ddlScrapType.SelectedValue, LoadStatus.ToString(), TxtBarcode.Text, TxtTareWeight.Text, NetWeight.ToString(), ddlScrapCategory.SelectedValue, ddlBu.SelectedItem.Text, ddlTruckNo.SelectedValue }, "dataset");


                btnSubmit.Visible = true;
                BtnDraft.Visible = true;
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Successfully Submitted");
                if (ViewState["ApplicationStatus"].ToString() == "NEW")
                {
                    ViewState["Create_Date"] = Convert.ToDateTime(System.DateTime.Now, cult).ToString("yyyy/MM/dd");
                    GetMaxBagNo();
                    GetData();


                }
                else
                {
                    ResumeData();
                    GetDraftData();

                }
                ClearData();
                lblMsg.Text = "";

                ddlTruckNo_SelectedIndexChanged(sender, e);
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

    protected void GetData()
    {
        try
        {
            if (ViewState["ApplicationStatus"].ToString() == "NEW")
            {
                ViewState["Create_Date"] = Convert.ToDateTime(System.DateTime.Now, cult).ToString("yyyy/MM/dd");
            }
            ds = null;
            ds = objdb.ByProcedure("Sp_ScrapLoad", new string[] { "flag", "Scrap_Id", "Site_Id", "Create_Date" },
                new string[] { "4", ViewState["Scrap_Id"].ToString(), ViewState["SiteId"].ToString(), ViewState["Create_Date"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        GridScrap.DataSource = ds.Tables[0];
                        GridScrap.DataBind();
                        LblTotalScrapBags.InnerText = Convert.ToString(ds.Tables[0].Rows.Count);
                        LblTotalScrapWeight.InnerText = Convert.ToString(ds.Tables[1].Rows[0]["TotalWeight"].ToString());
                        ddlScrapType.SelectedValue = ds.Tables[0].Rows[0]["Scrap_Type"].ToString();
                        // ddlScrapType.Attributes.Add("disabled", "disabled");
                        btnSubmit.Visible = true;
                        BtnDraft.Visible = true;
                        //ddlBu.Enabled = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetDraftData()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("Sp_ScrapLoad", new string[] { "flag", "Site_Id" },
                new string[] { "13", ViewState["SiteId"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        GridDraft.DataSource = ds.Tables[0];
                        GridDraft.DataBind();
                        //LblTotalScrapBags.InnerText = Convert.ToString(ds.Tables[0].Rows.Count);
                        //LblTotalScrapWeight.InnerText = Convert.ToString(ds.Tables[1].Rows[0]["TotalWeight"].ToString());
                        //ddlScrapType.SelectedValue = ds.Tables[0].Rows[0]["Scrap_Type"].ToString();
                        //ddlScrapType.Attributes.Add("disabled", "disabled");

                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ClearData()
    {
        TxtBagWeight.Text = "";
        TxtBagWeight.Focus();
        TxtBarcode.Text = "";

    }

    protected void LnkDelete_Click(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";

            if (TxtTruckNo.Text == "0" || TxtTruckNo.Text == "" || TxtTruckNo.Text == null)
            {
                msg += "PLEASE ENTER TRUCK NO.\\n";
            }

            else if (ddlTruckNo.SelectedIndex == 0)
            {
                msg += "PLEASE SELECT TruckNo.\\n";
            }

            else if (ddlScrapCategory.SelectedIndex == 0)
            {
                msg += "PLEASE SELECT Category.\\n";
            }

            //else if (ddlScrapType.SelectedIndex == 0)
            //{
            //    msg += "PLEASE SELECT SCRAP TYPE.\\n";
            //}
            //else if (TxtInitailWeight.Text == "0" || TxtInitailWeight.Text == "" || TxtInitailWeight.Text == null)
            //{
            //    msg += "PLEASE ENTER INITAIL WEIGHT.\\n";
            //}
            if (msg == "")
            {
                ds = null;
                DateTime dt = System.DateTime.Now;
                string Year = Convert.ToString(dt.Year);
                string Month = Convert.ToString(dt.Month);
                string Date = Convert.ToString(dt.Day);
                string Hour = Convert.ToString(dt.Hour);
                string Minute = Convert.ToString(dt.Minute);

                DataSet dsType = null;
                dsType = objdb.ByProcedure("Sp_ScrapLoad", new string[] { "flag", "Scrap_Id", "Create_Date", "Site_Id" },
                   new string[] { "16", ViewState["Scrap_Id"].ToString(), ViewState["Create_Date"].ToString(), ViewState["SiteId"].ToString() }, "dataset");
                if (dsType != null)
                {
                    if (dsType.Tables.Count > 0)
                    {
                        if (dsType.Tables[0].Rows.Count > 0)
                        {
                            if (dsType.Tables[0].Rows.Count > 1)
                            {
                                ViewState["ScrapType"] = "MIX";
                            }
                            else
                            {
                                ViewState["ScrapType"] = ddlScrapType.SelectedValue;
                            }
                        }
                    }

                }


                LblRefNo.InnerText = ViewState["SiteCode"].ToString() + '-' + ViewState["ScrapType"].ToString() + '-' + Year + '-' + Month + '-' + Date + '-' + Hour + '-' + Minute + '-' + ViewState["AfterScrap_Id"].ToString() + '-' + LblTotalScrapBags.InnerText;

                ds = objdb.ByProcedure("Sp_ScrapLoad", new string[] { "flag", "Scrap_Id", "Total_Bags", "Refrence_No", "Department_Id", "Initial_TruckWeight", "Final_Weight", "Status", "Create_By", "Site_Id", "Truck_No", "Total_Weight", "ApplicationStatus", "Create_Date", "ScrapCategoryId", "SMTrukGatePass_Id" },
                 new string[] { "6", ViewState["Scrap_Id"].ToString(), LblTotalScrapBags.InnerText, LblRefNo.InnerText, ViewState["DepartId"].ToString(), TxtInitailWeight.Text, "0", "READY TO SEND", ViewState["UserCode"].ToString(), ViewState["SiteId"].ToString(), TxtTruckNo.Text, LblTotalScrapWeight.InnerText, ViewState["ApplicationStatus"].ToString(), ViewState["Create_Date"].ToString(), ddlScrapCategory.SelectedValue, ddlTruckNo.SelectedValue }, "dataset");

                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", LblRefNo.InnerText + '-' + "Submitted Successfully");
                TxtInitailWeight.Text = "0";
                TxtTruckNo.Text = "";
                GetRefNo();
                GetMaxBagNo();
                //GetData();
                LblTotalScrapBags.InnerText = "0";
                LblTotalScrapWeight.InnerText = "0";
                GridScrap.DataSource = null;
                GridScrap.DataBind();

                ddlScrapType.Attributes.Remove("disabled");
                ViewState["ApplicationStatus"] = "NEW";

                ClearData();
                PanelScrap.Visible = false;

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
    protected void ddlScrapType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime dt = System.DateTime.Now;
            string Year = Convert.ToString(dt.Year);
            string Month = Convert.ToString(dt.Month);
            string Date = Convert.ToString(dt.Day);
            string Hour = Convert.ToString(dt.Hour);
            string Minute = Convert.ToString(dt.Minute);
            string Code = "";
            if (ddlScrapCategory.SelectedValue == "1")
            {
                Code = ddlScrapType.SelectedValue;
            }
            else
            {
                Code = "GEN";
            }

            TxtBagBatchNo.Text = ViewState["SiteCode"].ToString() + '-' + Code.ToString() + '-' + Year + '-' + Month + '-' + Date + '-' + Hour + '-' + Minute + '-' + ViewState["AfterScrap_Id"].ToString() + '-' + TxtBagNo.Text;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void GridScrap_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Delete")
            {
                ViewState["Id"] = Convert.ToInt32(e.CommandArgument.ToString());
                ds = null;
                ds = objdb.ByProcedure("Sp_ScrapLoad", new string[] { "flag", "Id", "Create_By", "Site_Id" },
                    new string[] { "5", ViewState["Id"].ToString(), ViewState["UserCode"].ToString(), ViewState["SiteId"].ToString() }, "dataset");
                ResumeData();
                GetData();
                
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void GridScrap_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void BtnDraft_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";

            if (ddlScrapType.SelectedIndex == 0)
            {
                msg += "PLEASE SELECT SCRAP TYPE.\\n";
            }


            if (msg == "")
            {

                ds = null;
                DateTime dt = System.DateTime.Now;
                string Year = Convert.ToString(dt.Year);
                string Month = Convert.ToString(dt.Month);
                string Date = Convert.ToString(dt.Day);
                string Hour = Convert.ToString(dt.Hour);
                string Minute = Convert.ToString(dt.Minute);

                LblRefNo.InnerText = ViewState["SiteCode"].ToString() + '-' + ddlScrapType.SelectedValue + '-' + Year + '-' + Month + '-' + Date + '-' + Hour + '-' + Minute + '-' + ViewState["AfterScrap_Id"].ToString() + '-' + LblTotalScrapBags.InnerText;
                ds = objdb.ByProcedure("Sp_ScrapLoad", new string[] { "flag", "Scrap_Id", "Total_Bags", "Refrence_No", "Department_Id", "Status", "Create_By", "Site_Id", "Truck_No", "Total_Weight" },
                    new string[] { "12", ViewState["Scrap_Id"].ToString(), LblTotalScrapBags.InnerText, LblRefNo.InnerText, ViewState["DepartId"].ToString(), "READY TO SEND", ViewState["UserCode"].ToString(), ViewState["SiteId"].ToString(), TxtTruckNo.Text, LblTotalScrapWeight.InnerText }, "dataset");

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "DRAFT")
                            {
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Save As Draft Submitted");
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["Msg"].ToString());
                                return;
                            }
                        }
                    }
                }


                //GetData();
                LblTotalScrapBags.InnerText = "0";
                LblTotalScrapWeight.InnerText = "0";
                GridScrap.DataSource = null;
                GridScrap.DataBind();
                ClearData();
                ddlScrapType.Attributes.Remove("disabled");
                ViewState["ApplicationStatus"] = "NEW";
                GetDraftData();
                GetRefNo();
                GetMaxBagNo();
                PanelScrap.Visible = false;
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

    protected void LnkResume_Click(object sender, EventArgs e)
    {


    }

    protected void GridDraft_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "Resume")
            {
                ViewState["Scrap_Id"] = Convert.ToInt32(e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                string Create_Date = (string)GridDraft.DataKeys[row.RowIndex].Value;
                ViewState["ApplicationStatus"] = "RESUME";
                ViewState["Create_Date"] = Convert.ToDateTime(Create_Date, cult).ToString("yyyy/MM/dd");

                ResumeData();

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridDraft_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void ResumeData()
    {
        ds = null;
        ds = objdb.ByProcedure("Sp_ScrapLoad", new string[] { "flag", "Scrap_Id", "Create_Date", "SMTrukGatePass_Id" },
            new string[] { "25", ViewState["Scrap_Id"].ToString(), ViewState["Create_Date"].ToString(), ddlTruckNo.SelectedValue }, "dataset");
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    GridScrap.DataSource = ds.Tables[0];
                    GridScrap.DataBind();
                    LblTotalScrapBags.InnerText = Convert.ToString(ds.Tables[0].Rows.Count);
                    LblTotalScrapWeight.InnerText = Convert.ToString(ds.Tables[1].Rows[0]["TotalWeight"].ToString());
                    ddlScrapType.SelectedValue = ds.Tables[0].Rows[0]["Scrap_Type"].ToString();
                    //ddlScrapType.Attributes.Add("disabled", "disabled");
                    ViewState["AfterScrap_Id"] = "00" + "" + ds.Tables[0].Rows[0]["Scrap_Id"].ToString();
                    btnSubmit.Visible = true;
                    BtnDraft.Visible = false;

                }
                else
                {

                    LblTotalScrapBags.InnerText = Convert.ToString(0);
                    LblTotalScrapWeight.InnerText = "0";
                    GridScrap.DataSource = null;
                    GridScrap.DataBind();
                }
            }
            else
            {
                GridScrap.DataSource = null;
                GridScrap.DataBind();
            }
        }
        else
        {
            GridScrap.DataSource = null;
            GridScrap.DataBind();
        }

        GetMaxBagNo();

    }

    protected void BtngetValue_Click(object sender, EventArgs e)
    {

    }
    protected void BtnGetWeigh_Click(object sender, EventArgs e)
    {


        clientSocket.Connect("143.34.186.184", 23);
        NetworkStream serverStream = clientSocket.GetStream();
        @out = "OP" + '\n';
        var outStream = Encoding.ASCII.GetBytes(@out);
        // outStream = outStream + System.Text.Encoding.ASCII.GetBytes("13")
        serverStream.Write(outStream, 0, outStream.Length);
        serverStream.Flush();
        if (serverStream.CanRead)
        {
            var myReadBuffer = new byte[1024];
            var myCompleteMessage = new StringBuilder();
            int numberOfBytesRead = 0;

            // Incoming message may be larger than the buffer size.
            do
            {
                numberOfBytesRead = serverStream.Read(myReadBuffer, 0, myReadBuffer.Length);
                myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
            }
            while (serverStream.DataAvailable);

            // Print out the received message to the console.
            // TextBox1.Text = "DONE";
            // msg["You received the following message : " + myCompleteMessage.ToString()];
        }
        else
        {
            //  msg["Sorry.  You cannot read from this NetworkStream."];
        }



    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        clientSocket.Connect("143.34.186.184", 23);
        NetworkStream serverStream = clientSocket.GetStream();
        @out = "OP" + '\n';
        var outStream = Encoding.ASCII.GetBytes(@out);
        // outStream = outStream + System.Text.Encoding.ASCII.GetBytes("13")
        serverStream.Write(outStream, 0, outStream.Length);
        serverStream.Flush();
        if (serverStream.CanRead)
        {
            var myReadBuffer = new byte[1024];
            var myCompleteMessage = new StringBuilder();
            int numberOfBytesRead = 0;

            // Incoming message may be larger than the buffer size.
            do
            {
                numberOfBytesRead = serverStream.Read(myReadBuffer, 0, myReadBuffer.Length);
                myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
            }
            while (serverStream.DataAvailable);
            // string returndata = System.Text.Encoding.ASCII.GetString(outStream);
            if (myCompleteMessage.ToString() == "0\r00")
            {
                lblMsg.Text = "Client Socket Program - Server Connected ...";
            }
            // Print out the received message to the console.
            // TextBox1.Text = "DONE";
            // msg["You received the following message : " + myCompleteMessage.ToString()];
        }
        else
        {
            //  msg["Sorry.  You cannot read from this NetworkStream."];
        }


        string Out1 = "";
        Out1 = ("GD" + '\n');


        NetworkStream serverStream1 = clientSocket.GetStream();
        byte[] outStream1 = Encoding.ASCII.GetBytes(Out1);


        // outStream = outStream + System.Text.Encoding.ASCII.GetBytes("13")
        serverStream1.Write(outStream1, 0, outStream1.Length);
        System.Threading.Thread.Sleep(150);
        serverStream1.Flush();
        if (serverStream1.CanRead)
        {
            byte[] myReadBuffer = System.Text.Encoding.ASCII.GetBytes("1024");

            StringBuilder myCompleteMessage = new StringBuilder();
            int numberOfBytesRead = 0;
            //  Incoming message may be larger than the buffer size.
            for (
            ; serverStream1.DataAvailable;
            )
            {
                numberOfBytesRead = serverStream1.Read(myReadBuffer, 0, myReadBuffer.Length);
                myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
            }
            while (serverStream1.DataAvailable) ;
            //TextBox1.Text = myCompleteMessage.ToString();
            //TxtBagWeight.Text = myCompleteMessage.ToString();
            string OutPut = myCompleteMessage.ToString();

            if (OutPut.Length > 0)
            {
                int i = OutPut.IndexOf("+") + 1;
                string str = OutPut.Substring(i);
                TxtBagWeight.Text = str.ToString();


                //int comma = str.IndexOf('.');
                //string b = "";
                //if (comma != -1)
                //{
                //    b = str.Substring(0, comma);
                //}

            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Weigh Machine Not Connected");
                return;
            }

            //  Print out the received message to the console.
            //msg(("You received the following message : " + myCompleteMessage.ToString()));
        }
        else
        {
            //msg("Sorry.  You cannot read from this NetworkStream.");
        }
    }
    protected void ddlScrapCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ds = objdb.ByProcedure("Sp_ScrapType", new string[] { "flag", "Site_id", "ScrapCategoryId" }, new string[] { "7", ViewState["SiteId"].ToString(), ddlScrapCategory.SelectedValue }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlScrapType.DataTextField = "Scrap_Type";
                ddlScrapType.DataValueField = "Scrap_code";
                ddlScrapType.DataSource = ds;
                ddlScrapType.DataBind();
                ddlScrapType.Items.Insert(0, new ListItem("Select Scrap Type", "0"));
                ddlScrapType.SelectedIndex = 0;
            }

            if (ddlScrapCategory.SelectedItem.Text == "RE-PROCESSING")
            {
                ddlBu.Enabled = false;
                //ScanPanel.Visible = true;
                TxtBarcode.Visible = true;
                lblqr.Visible = true;
            }
            else
            {
                //ScanPanel.Visible = false;
                ddlBu.Enabled = true;
                TxtBarcode.Visible = false;
                lblqr.Visible = false;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void FillCategory(object sender, EventArgs e)
    {
        try
        {
            ds = objdb.ByProcedure("Sp_ScrapType", new string[] { "flag", "Site_id" }, new string[] { "6", ViewState["SiteId"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlScrapCategory.DataTextField = "ScrapCategory";
                ddlScrapCategory.DataValueField = "ScrapCategoryId";
                ddlScrapCategory.DataSource = ds;
                ddlScrapCategory.DataBind();
                ddlScrapCategory.Items.Insert(0, new ListItem("Select Category", "0"));
                ddlScrapCategory.SelectedValue = "2";
                ddlScrapCategory_SelectedIndexChanged(sender, e);
                //ddlScrapCategory.SelectedValue = ViewState["SiteId"].ToString();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnAddTruck_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";

            if (txtTno.Text == "0" || txtTno.Text == "" || txtTno.Text == null)
            {
                msg += "PLEASE ENTER TRUCK NO.\\n";
            }

            if (txtTIW.Text == "0" || txtTIW.Text == "" || txtTIW.Text == null)
            {
                msg += "PLEASE ENTER RUCK INITIAL WEIGHT.\\n";
            }

            if (msg == "")
            {

                DataSet dsAddTruck = null;

                dsAddTruck = objdb.ByProcedure("Sp_ScrapLoad", new string[] { "flag", "Truck_No", "Initial_TruckWeight", "Create_By" },
                   new string[] { "22", txtTno.Text, txtTIW.Text, ViewState["UserCode"].ToString() }, "dataset");

                if (dsAddTruck != null)
                {
                    if (dsAddTruck.Tables.Count > 0)
                    {
                        if (dsAddTruck.Tables[0].Rows.Count > 0)
                        {
                            if (dsAddTruck.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string success = dsAddTruck.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            }
                            else if (dsAddTruck.Tables[0].Rows[0]["Msg"].ToString() == "Exist")
                            {

                                string Warning = dsAddTruck.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "Cal lMyFunction", "alert('" + Warning + "');", true);
                            }
                            else
                            {
                                string error = dsAddTruck.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());
                            }
                        }
                    }

                }

                ViewTruckDetail();
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

    protected void ViewTruckDetail()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("Sp_ScrapLoad", new string[] { "flag" }, new string[] { "23" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlTruckNo.DataTextField = "Truck_Detail";
                ddlTruckNo.DataValueField = "SMTrukGatePass_Id";
                ddlTruckNo.DataSource = ds;
                ddlTruckNo.DataBind();
                ddlTruckNo.Items.Insert(0, new ListItem("Select Truck", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlTruckNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillCategory(sender, e);

            ds = null;
            ds = objdb.ByProcedure("Sp_ScrapLoad", new string[] { "flag", "SMTrukGatePass_Id" }, new string[] { "24", ddlTruckNo.SelectedValue }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                //ViewState["Truck_No_N"] = ds.Tables[0].Rows[0]["Truck_No"].ToString();
                //ViewState["Initial_TruckWeight_N"] = ds.Tables[0].Rows[0]["Initial_TruckWeight"].ToString(); 

                TxtTruckNo.Text = ds.Tables[0].Rows[0]["Truck_No"].ToString();
                TxtTruckNo.Enabled = false;
                TxtInitailWeight.Text = ds.Tables[0].Rows[0]["Initial_TruckWeight"].ToString();
                TxtInitailWeight.Enabled = false;

                ViewState["Scrap_Id"] = ds.Tables[0].Rows[0]["Scrap_Id"].ToString();
                string Create_Date = ds.Tables[0].Rows[0]["Create_Date"].ToString();
                ViewState["ApplicationStatus"] = "RESUME";
                ViewState["Create_Date"] = Convert.ToDateTime(Create_Date, cult).ToString("yyyy/MM/dd");
                ResumeData();
            }
            else
            {
                TxtTruckNo.Enabled = true;
                TxtInitailWeight.Enabled = true;
                LblTotalScrapBags.InnerText = Convert.ToString(0);
                LblTotalScrapWeight.InnerText = "0";
                GridScrap.DataSource = null;
                GridScrap.DataBind();
                GetMaxBagNo();


            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }


}