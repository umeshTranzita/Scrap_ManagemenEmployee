using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing;
using System.Configuration;
using System.Data.SqlClient;

public partial class Frm_PostProcessing : System.Web.UI.Page
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
            ViewState["AppStatusMode"] = "NEW";

            Weight.Attributes.Add("class", "active");
            BagBatchNo.Attributes.Add("class", "active");
            LblBarcode.Attributes.Add("class", "active");
            LblTierWeight.Attributes.Add("class", "active");

            ddlType.SelectedIndex = 1;
            GetRefNo();
            GetMaxBagNo();
            GetData();
            AccessControl();
            FillScrapType();
            TxtBagBatchNo.ReadOnly = true;
            TxtWeight.ReadOnly = false;
            TxtBarcode.ReadOnly = false;
            //FillConsigment();

        }
    }
    protected void FillScrapType()
    {
        try
        {
            ds = objdb.ByProcedure("Sp_PostProcess", new string[] { "flag", "Site_Id" }, new string[] { "11", "1" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlScrapType.DataTextField = "Scrap_Type";
                ddlScrapType.DataValueField = "Scrap_TypeId";
                ddlScrapType.DataSource = ds;
                ddlScrapType.DataBind();
                ddlScrapType.Items.Insert(0, new ListItem("Select Scrap Type", "0"));
                ddlScrapType.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillConsigment()
    {
        try
        {
            ds = null;

            lblMsg.Text = "";
            ds = objdb.ByProcedure("Sp_PostProcessing", new string[] { "flag", "Site_Id", "Create_By" },
               new string[] { "1", ViewState["SiteId"].ToString(), ViewState["UserCode"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlType.DataTextField = "Refrence_No";
                        ddlType.DataValueField = "Scrap_Id";
                        ddlType.DataSource = ds;
                        ddlType.DataBind();
                        ddlType.Items.Insert(0, new ListItem("Select Consigment No", "0"));
                        ddlType.SelectedIndex = 0;

                    }
                    else
                    {
                        ddlType.DataSource = ds;
                        ddlType.Items.Clear();

                    }
                }
                else
                {
                    ddlType.DataSource = ds;
                    ddlType.Items.Clear();

                }
            }
            else
            {
                ddlType.DataSource = ds;
                ddlType.Items.Clear();

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
            ds = objdb.ByProcedure("Sp_PostProcess", new string[] { "flag", "Site_ID", "AccessType" }, new string[] { "10", ViewState["SiteId"].ToString(), "Post Process Barcode" }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                            ViewState["BarcodeMandatory"] = "YES";
                    }
                    else
                    {
                        ViewState["BarcodeMandatory"] = "NO";
                    }
                }
                else
                {
                    ViewState["BarcodeMandatory"] = "NO";
                }
            }
            else
            {
                ViewState["BarcodeMandatory"] = "NO";
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

            ds = null;
            ds = objdb.ByProcedure("Sp_PostProcess", new string[] { "flag", "PPID", "Site_Id" },
                new string[] { "5", ViewState["PPID"].ToString(), ViewState["SiteId"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        GridScrap.DataSource = ds.Tables[0];
                        GridScrap.DataBind();
                        LblTotalScrapBags.InnerText = Convert.ToString(ds.Tables[0].Rows.Count);


                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }




    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Frm_PostProcessing.aspx");
    }
    protected void GetRefNo()
    {
        try
        {
            ds = null;

            ds = objdb.ByProcedure("Sp_PostProcess", new string[] { "flag", "Site_Id" },
                new string[] { "1", ViewState["SiteId"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ViewState["Daily_PPId"] = ds.Tables[0].Rows[0]["Daily_PPId"].ToString();
                ViewState["PPID"] = ds.Tables[0].Rows[0]["PPID"].ToString();
                ViewState["AfterDaily_PPId"] = "00" + "" + 1;
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

            ds = objdb.ByProcedure("Sp_PostProcess", new string[] { "flag", "PPID", "Site_Id" },
                new string[] { "2", ViewState["PPID"].ToString(), ViewState["SiteId"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                TxtBagNo.Text = ds.Tables[0].Rows[0]["Bag_No"].ToString();
                TxtBagBatchNo.Text = "KDL" + '-' + ddlType.SelectedItem.Text + '-' + Year + '-' + Month + '-' + Date + '-' + Hour + '-' + Minute + '-' + ViewState["AfterDaily_PPId"].ToString() + '-' + TxtBagNo.Text;
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    protected void ddlType_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            DateTime dt = System.DateTime.Now;
            string Year = Convert.ToString(dt.Year);
            string Month = Convert.ToString(dt.Month);
            string Date = Convert.ToString(dt.Day);
            string Hour = Convert.ToString(dt.Hour);
            string Minute = Convert.ToString(dt.Minute);
            if (ViewState["AppStatusMode"].ToString() == "NEW")
            {
                TxtBagBatchNo.Text = "KDL" + '-' + ddlType.SelectedItem.Text + '-' + Year + '-' + Month + '-' + Date + '-' + Hour + '-' + Minute + '-' + ViewState["AfterDaily_PPId"].ToString() + '-' + TxtBagNo.Text;
            }
            else
            {
                TxtBagBatchNo.Text = "KDL" + '-' + ddlType.SelectedItem.Text + '-' + Year + '-' + Month + '-' + Date + '-' + Hour + '-' + Minute + '-' + ViewState["AfterDaily_PPId"].ToString() + '-' + ViewState["EditBagNo"];
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void Btn_Add_Click(object sender, EventArgs e)
    {
        try
        {
            ds = null;
            lblMsg.Text = "";
            ds = null;

            if (ViewState["BarcodeMandatory"].ToString() == "YES")
            {
                if (TxtBarcode.Text.Trim() == "")
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Please Enter QR Code");
                    return;
                }
            }

            DateTime dt = System.DateTime.Now;
            string Year = Convert.ToString(dt.Year);
            string Month = Convert.ToString(dt.Month);
            string Date = Convert.ToString(dt.Day);
            string Hour = Convert.ToString(dt.Hour);
            string Minute = Convert.ToString(dt.Minute);

            DataSet dsType = null;
            dsType = objdb.ByProcedure("Sp_PostProcess", new string[] { "flag", "PPID", "Site_Id" },
               new string[] { "6", ViewState["PPID"].ToString(), ViewState["SiteId"].ToString() }, "dataset");
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
                            if (dsType.Tables[0].Rows[0]["Type"].ToString() == ddlType.SelectedItem.Text)
                            {
                                ViewState["ScrapType"] = ddlType.SelectedItem.Text;
                            }
                            else
                            {
                                ViewState["ScrapType"] = "MIX";

                            }
                        }
                    }
                    else
                    {
                        ViewState["ScrapType"] = ddlType.SelectedItem.Text;
                    }
                }
                else
                {
                    ViewState["ScrapType"] = ddlType.SelectedItem.Text;
                }
            }
            else
            {
                ViewState["ScrapType"] = ddlType.SelectedItem.Text;
            }

            if (Btn_Add.Text == "ADD")
            {
                LblRefNo.InnerText = "KDL" + '-' + ViewState["ScrapType"].ToString() + '-' + Year + '-' + Month + '-' + Date + '-' + Hour + '-' + Minute + '-' + ViewState["AfterDaily_PPId"].ToString() + '-' + TxtBagNo.Text;
                ds = objdb.ByProcedure("Sp_PostProcess", new string[] { "flag", "PPID", "Site_Id", "Consigment_No" },
                   new string[] { "3", ViewState["PPID"].ToString(), ViewState["SiteId"].ToString(), LblRefNo.InnerText }, "dataset");
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            //------------------------------------------------------Tier Weight Calculation---------------------------------------------------//
                            Double Tier = ((double.Parse(TxtWeight.Text) * 1000) - double.Parse(TxtTierWeight.Text)) / 1000;

                           Double NetWeight = double.Parse(Tier.ToString());

                           if (TxtWeight.Text == "")
                            {
                                TxtWeight.Text = "0";
                            }

                            if (NetWeight.ToString() == "")
                            {
                                NetWeight = 0;
                            }


                            objdb.ByProcedure("Sp_PostProcess", new string[] { "flag", "PPID", "Consigment_No", "Bag_No", "Bag_BatchNo", "Weight", "Site_Id", "Create_by", "Type_Id", "Type", "Barcode", "ScrapType", "ScrapTypeId", "TierWeight","NetWeight" },
                               new string[] { "4", ViewState["PPID"].ToString(), LblRefNo.InnerText, TxtBagNo.Text, TxtBagBatchNo.Text, TxtWeight.Text, ViewState["SiteId"].ToString(), ViewState["UserCode"].ToString(), ddlType.SelectedValue, ddlType.SelectedItem.Text, TxtBarcode.Text, ddlScrapType.SelectedItem.Text, ddlScrapType.SelectedValue, TxtTierWeight.Text, NetWeight.ToString() }, "dataset");

                            objdb.ByProcedure("Sp_PostProcess", new string[] { "flag", "PPID","Weight","ScrapType" },
                               new string[] { "12", ViewState["PPID"].ToString(), TxtWeight.Text, ddlScrapType.SelectedItem.Text }, "dataset");

                        }
                    }
                }
            }
            else
            {
                LblRefNo.InnerText = "KDL" + '-' + ViewState["ScrapType"].ToString() + '-' + Year + '-' + Month + '-' + Date + '-' + Hour + '-' + Minute + '-' + ViewState["AfterDaily_PPId"].ToString() + '-' + LblTotalScrapBags.InnerText;
                ds = objdb.ByProcedure("Sp_PostProcess", new string[] { "flag", "PPID", "Site_Id", "Consigment_No", "Type_Id", "Type", "Bag_BatchNo", "Id", "Create_by", "Bag_No", "Weight", "ScrapType", "ScrapTypeId", "TierWeight" },
                  new string[] { "9", ViewState["PPID"].ToString(), ViewState["SiteId"].ToString(), LblRefNo.InnerText, ddlType.SelectedValue, ddlType.SelectedItem.Text, TxtBagBatchNo.Text, ViewState["Id"].ToString(), ViewState["UserCode"].ToString(), TxtBagNo.Text, TxtWeight.Text, ddlScrapType.SelectedItem.Text, ddlScrapType.SelectedValue, TxtTierWeight.Text }, "dataset");
                objdb.ByProcedure("Sp_PostProcess", new string[] { "flag", "PPID", "Weight", "ScrapType" },
                              new string[] { "12", ViewState["PPID"].ToString(), TxtWeight.Text, ddlScrapType.SelectedItem.Text }, "dataset");

            }
            objdb.ByProcedure("Sp_ScrapIssue", new string[] { "flag" },
           new string[] { "9" }, "dataset");
            GetData();
            GetMaxBagNo();
            TxtWeight.Text = "";
            TxtBarcode.Text = "";
            TxtWeight.Focus();
            Btn_Add.Text = "ADD";
            ViewState["AppStatusMode"] = "NEW";
            TxtTierWeight.ReadOnly = false;
            TxtWeight.ReadOnly = false;
            TxtBarcode.ReadOnly = false;
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
                ds = objdb.ByProcedure("Sp_PostProcess", new string[] { "flag", "Id", "Create_By", "Site_Id", "PPID" },
                    new string[] { "7", ViewState["Id"].ToString(), ViewState["UserCode"].ToString(), ViewState["SiteId"].ToString(), ViewState["PPID"].ToString() }, "dataset");
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
    protected void GridScrap_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            string IsActive = "No";
            ViewState["Id"] = GridScrap.SelectedDataKey.Value;
            ds = objdb.ByProcedure("Sp_PostProcess", new string[] { "flag", "Id", }, new string[] { "8", ViewState["Id"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlType.SelectedValue = ds.Tables[0].Rows[0]["Type_Id"].ToString();
                ddlScrapType.SelectedValue = ds.Tables[0].Rows[0]["ScrapTypeId"].ToString();
                TxtBagBatchNo.Text = ds.Tables[0].Rows[0]["Bag_BatchNo"].ToString();
                TxtBarcode.Text = ds.Tables[0].Rows[0]["Barcode"].ToString();
                TxtWeight.Text = ds.Tables[0].Rows[0]["Weight"].ToString();
                TxtTierWeight.Text = ds.Tables[0].Rows[0]["TierWeight"].ToString();
                //TxtWeight.Attributes.Add("disabled", "disabled");
                //TxtBarcode.Attributes.Add("disabled", "disabled");
                ViewState["AppStatusMode"] = "EDIT";
                ViewState["EditPPID"] = ds.Tables[0].Rows[0]["PPID"].ToString();
                ViewState["EditBagNo"] = ds.Tables[0].Rows[0]["Bag_No"].ToString();
                TxtBagBatchNo.ReadOnly = true;
                TxtWeight.ReadOnly = true;
                TxtBarcode.ReadOnly = true;
                TxtBarcode.ReadOnly = true;

                TxtTierWeight.ReadOnly = true;
                Btn_Add.Text = "EDIT";
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}