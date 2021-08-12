using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
public partial class Frm_ScrapReceive : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDB objdb = new common();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    string x = "";
    string RequestFormat = "";
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "showModal();", true);
            LockNo.Attributes.Add("class", "active");
            TierWeight.Attributes.Add("class", "active");

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
            double TotalRecWt = 0, TotalBagWt = 0, PlusOverAllToloerance = 0, MinusOverAllToloerance;
            ds = objdb.ByProcedure("Sp_ScrapReceive", new string[] { "flag", "Refrence_No", "Site_Id" },
               new string[] { "2", ddlConsigment.SelectedItem.Text, ViewState["SiteId"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        LblSite_Id.Text = ds.Tables[0].Rows[0]["Site_Id"].ToString();
                        GridReceive.DataSource = ds.Tables[0];
                        GridReceive.DataBind();

                        ddlBagNo.DataTextField = "Bag_No";
                        ddlBagNo.DataValueField = "Bag_BatchNo";
                        ddlBagNo.DataSource = ds.Tables[1];
                        ddlBagNo.DataBind();
                        ddlBagNo.Items.Insert(0, new ListItem("Select All Bag No", "0"));
                        ddlBagNo.SelectedIndex = 0;
                        PanelReceive.Visible = true;
                        foreach (GridViewRow item in GridReceive.Rows)
                        {
                            Label ScrapStatus = (Label)item.FindControl("LblStatus");
                            TextBox TxtAllRecWt = (TextBox)item.FindControl("TxtReceiveWeight");
                            TextBox TxtAllBagWt = (TextBox)item.FindControl("TxtBagWeight");
                            // TextBox TxtTierWeight = (TextBox)item.FindControl("TxtTierWeigh");

                            //TxtTierWeight.Enabled = false;
                            if (ScrapStatus.Text == "RECEIVED")
                            {
                                TxtAllRecWt.Enabled = false;
                                TxtAllRecWt.BackColor = Color.LightGray;

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
                            GridReceive.FooterRow.Cells[5].Font.Size = 16;

                            GridReceive.FooterRow.Cells[1].Text = "OVERALL";
                            GridReceive.FooterRow.Cells[4].Text = TotalRecWt.ToString();
                            Panel1.Visible = false;
                            ScanPanel.Visible = true;
                        }
                        ViewState["Tolerance_Weight"] = ds.Tables[0].Rows[0]["Tolerance_ReceiveWeight"].ToString();
                        GridReceive.FooterRow.Cells[5].Text = ds.Tables[0].Rows[0]["Overall_WeightReceiveStatus"].ToString();
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

    protected void TxtReceiveWeight_TextChanged(object sender, EventArgs e)
    {
        GridViewRow currentRow = ((GridViewRow)((TextBox)sender).NamingContainer);

        TextBox txtRecWt = (TextBox)currentRow.FindControl("TxtReceiveWeight");
        TextBox txtBagWt = (TextBox)currentRow.FindControl("TxtBagWeight");
        TextBox txtBagTole = (TextBox)currentRow.FindControl("TxtBagToleranceWt");
        TextBox TierWeigh = (TextBox)currentRow.FindControl("TxtTierWeigh");
        Label BagBatch = (Label)currentRow.FindControl("LblBag_BatchNo");
        Label BagStatus = (Label)currentRow.FindControl("LblBagStatus");
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

        if (double.Parse(NetWeight.Text) >= MinusBagTolerance && double.Parse(NetWeight.Text) <= PlusBagTolerance)
        {
            BagStatus.Text = "OK";
            currentRow.BackColor = Color.LightGreen;
        }
        else
        {
            BagStatus.Text = "NOTOK";
            currentRow.BackColor = Color.Red;
        }
        double TotalRecWt = 0, TotalBagWt = 0;
        int I = 0;
        foreach (GridViewRow item in GridReceive.Rows)
        {
            Label OverallBagStatus = (Label)item.FindControl("LblBagStatus");
            if (OverallBagStatus.Text == "NOTOK")
            {
                I = 1;

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

        GridReceive.FooterRow.Cells[1].Font.Size = 16;
        GridReceive.FooterRow.Cells[4].Font.Size = 16;
        GridReceive.FooterRow.Cells[5].Font.Size = 16;

        GridReceive.FooterRow.Cells[1].Text = "OVERALL";
        GridReceive.FooterRow.Cells[4].Text = TotalRecWt.ToString();
        if (I == 1)
        {
            GridReceive.FooterRow.Cells[7].Text = "NOTOK";
            //BtnReceive.Visible = false;
            LblOverAllStatus.Text = "NOTOK";
            BtnReceive.Text = "SAVE";

        }
        else
        {
            GridReceive.FooterRow.Cells[7].Text = "OK";
            // BtnReceive.Visible = true;
            LblOverAllStatus.Text = "OK";
            BtnReceive.Text = "RECEIVE";

        }
        BtnReceive.Visible = true;
    }

    protected void GridReceive_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void BtnReceive_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GridReceive.Rows)
            {
                Label BagStatus = (Label)row.FindControl("LblBagStatus");

                if (BagStatus.Text == "")
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Please Enter Receive Weight ");
                    return;

                }
            }

            string ScrapStatus = "RECEIVED";
            if (BtnReceive.Text == "SAVE")
            {
                ScrapStatus = "SEND TO APPROVAL";
                RequestFormat = "Action: Investigation report needed for weight Mismatch for Exception";

            }
            else
            {
                ScrapStatus = "RECEIVED";

            }

            //DataTable dt = new DataTable();
            //dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Scrap_Id", typeof(int)),
            //    new DataColumn("Refrence_No", typeof(string)),
            //    new DataColumn("Bag_BatchNo",typeof(string)), 
            //new DataColumn("Receive_Weight",typeof(decimal))
            //});


            int Id = 0;
            string Refrence_No = "", Receive_Weight = "0", Bag_BatchNo = "", Tier_Weight = "0", Net_Weight = "0";
            double PadTotalNetWeight = 0, DiaTotalNetWeight = 0;
            foreach (GridViewRow row in GridReceive.Rows)
            {
                //int Scrap_Id = int.Parse(row.Cells[0].ToolTip.ToString());

                Label Scrap_Id = (Label)row.FindControl("lblRowNumber");
                Label BagBatchNo = (Label)row.FindControl("LblBag_BatchNo");
                Label RefNo = (Label)row.FindControl("LblRefrenceNo");
                Label Status = (Label)row.FindControl("LblStatus");
                Label BagStatus = (Label)row.FindControl("LblBagStatus");
                Label ScrapType = (Label)row.FindControl("LblScrapType");

                //TextBox TWeight = (TextBox)row.FindControl("TxtTierWeigh");
                // Label NetWeight = (Label)row.FindControl("LblNetWeight");

                Bag_BatchNo = BagBatchNo.Text;
                Refrence_No = RefNo.Text;
                Id = int.Parse(Scrap_Id.ToolTip.ToString());

                Receive_Weight = (row.FindControl("TxtReceiveWeight") as TextBox).Text;
                Tier_Weight = (row.FindControl("TxtTierWeigh") as TextBox).Text;
                Net_Weight = (row.FindControl("LblNetWeight") as Label).Text;
                if (ScrapType.Text == "PAD")
                {
                    PadTotalNetWeight = PadTotalNetWeight + double.Parse(Net_Weight.ToString());

                }
                else if (ScrapType.Text == "DIA")
                {
                    DiaTotalNetWeight = DiaTotalNetWeight + double.Parse(Net_Weight.ToString());
                }

                //dt.Rows.Add(Id, Refrence_No, Bag_BatchNo, Receive_Weight);
                string constr = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("Update_ReceiveLoad"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Scrap_Id", Id.ToString());
                        cmd.Parameters.AddWithValue("@Refrence_No", Refrence_No.ToString());
                        cmd.Parameters.AddWithValue("@Bag_BatchNo", Bag_BatchNo.ToString());
                        cmd.Parameters.AddWithValue("@Receive_Weight", Receive_Weight.ToString());
                        cmd.Parameters.AddWithValue("@Create_By", ViewState["UserCode"].ToString());
                        cmd.Parameters.AddWithValue("@BagStatus", BagStatus.Text);
                        cmd.Parameters.AddWithValue("@ReceiveStatus", BagStatus.Text);
                        cmd.Parameters.AddWithValue("@Overall_WeightReceiveStatus", LblOverAllStatus.Text);
                        cmd.Parameters.AddWithValue("@SiteId", LblSite_Id.Text);
                        cmd.Parameters.AddWithValue("@ScrapStatus", ScrapStatus.ToString());
                        cmd.Parameters.AddWithValue("@TierWeight", Tier_Weight.ToString());
                        cmd.Parameters.AddWithValue("@NetWeight", Net_Weight.ToString());


                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            objdb.ByProcedure("Sp_ScrapReceive", new string[] { "flag", "Refrence_No", "Site_Id", "Receive_Status", "Create_By" },
               new string[] { "6", ddlConsigment.SelectedItem.Text, LblSite_Id.Text, ScrapStatus.ToString(), ViewState["UserCode"].ToString() }, "dataset");

            objdb.ByProcedure("Sp_ScrapReceive", new string[] { "flag", "Refrence_No", "Site_Id", "Pad_ReceiveWt", "Dia_ReceiveWt" },
            new string[] { "7", ddlConsigment.SelectedItem.Text, LblSite_Id.Text, PadTotalNetWeight.ToString(), DiaTotalNetWeight.ToString() }, "dataset");
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Scrap Received Successfully");
           // SendEmail();
            PanelReceive.Visible = false;

        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }

    protected void chkSapCode_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkSapCode.Checked == true)
            {


                foreach (GridViewRow item in GridReceive.Rows)
                {

                    TextBox TierWeight = (TextBox)item.FindControl("TxtTierWeigh");
                    TierWeight.Text = TxtTierWeight.Text;

                }
            }
            else
            {
                foreach (GridViewRow item in GridReceive.Rows)
                {

                    TextBox TierWeight = (TextBox)item.FindControl("TxtTierWeigh");
                    TierWeight.Text = "0";

                }
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    protected void TxtTierWeigh_TextChanged(object sender, EventArgs e)
    {
        GridViewRow currentRow = ((GridViewRow)((TextBox)sender).NamingContainer);

        TextBox txtRecWt = (TextBox)currentRow.FindControl("TxtReceiveWeight");
        TextBox txtBagWt = (TextBox)currentRow.FindControl("TxtBagWeight");
        TextBox txtBagTole = (TextBox)currentRow.FindControl("TxtBagToleranceWt");
        TextBox TierWeigh = (TextBox)currentRow.FindControl("TxtTierWeigh");
        Label BagBatch = (Label)currentRow.FindControl("LblBag_BatchNo");
        Label BagStatus = (Label)currentRow.FindControl("LblBagStatus");
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

        if (double.Parse(NetWeight.Text) >= MinusBagTolerance && double.Parse(NetWeight.Text) <= PlusBagTolerance)
        {
            BagStatus.Text = "OK";
            currentRow.BackColor = Color.LightGreen;
        }
        else
        {
            BagStatus.Text = "NOTOK";
            currentRow.BackColor = Color.Red;
        }
        double TotalRecWt = 0, TotalBagWt = 0;
        int I = 0;
        foreach (GridViewRow item in GridReceive.Rows)
        {
            Label OverallBagStatus = (Label)item.FindControl("LblBagStatus");
            if (OverallBagStatus.Text == "NOTOK")
            {
                I = 1;

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

        GridReceive.FooterRow.Cells[1].Font.Size = 16;
        GridReceive.FooterRow.Cells[4].Font.Size = 16;
        GridReceive.FooterRow.Cells[5].Font.Size = 16;

        GridReceive.FooterRow.Cells[1].Text = "OVERALL";
        GridReceive.FooterRow.Cells[4].Text = TotalRecWt.ToString();
        if (I == 1)
        {
            GridReceive.FooterRow.Cells[5].Text = "NOTOK";
            //BtnReceive.Visible = false;
            LblOverAllStatus.Text = "NOTOK";
            BtnReceive.Text = "SAVE";

        }
        else
        {
            GridReceive.FooterRow.Cells[5].Text = "OK";
            // BtnReceive.Visible = true;
            LblOverAllStatus.Text = "OK";
            BtnReceive.Text = "RECEIVE";

        }
        BtnReceive.Visible = true;
    }

    public void SendEmail()
    {
        string str = "";
        string x = "";
        string tabletr = "";
        string id = "1";
      
        string path = "";
        string abc = "";
       
        string XYZ = "";
        XYZ = "Consignment # -" + ddlConsigment.SelectedItem.Text + "  is received at KDL";
        DataSet DsData = objdb.ByProcedure("Sp_ScrapReceive", new string[] { "flag", "Site_Id", "Refrence_No" },
                                                   new string[] { "10", LblSite_Id.Text, ddlConsigment.SelectedItem.Text }, "Dataset");

        if (DsData != null)
        {
            if (DsData.Tables.Count > 0)
            {
                if (DsData.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < DsData.Tables[0].Rows.Count; i++)
                    {

                        //string ImagePath = Emailds.Tables[0].Rows[i]["Image"].ToString();
                        //x += Emailds.Tables[0].Rows[i]["Image"].ToString() + ",";
                        //string productId = Encrypt(Emailds.Tables[0].Rows[i]["Product_Id"].ToString());
                        //string VisitId = Encrypt(ViewState["Visit_Id"].ToString());
                        ////string Url = Emailds.Tables[0].Rows[i]["Product_url"].ToString();
                        ////---------------------------------Local Host-------------------------------------------
                        ////string Url = "http://localhost:51226/Email.aspx?ActivationCode=" + productId + "&ActivationCode1=" + VisitId.ToString();
                        ////---------------------------------Local Server-------------------------------------------
                        ////string Url = "http://192.168.1.13:8028/Email.aspx?ActivationCode=" + productId + "&ActivationCode1=" + VisitId.ToString();
                        ////---------------------------------Live Server-------------------------------------------
                        //string Url = "http://oralb-professional.com/Email.aspx?ActivationCode=" + productId + "&ActivationCode1=" + VisitId.ToString();




                        //string ProductName = Emailds.Tables[0].Rows[i]["Product_Name"].ToString();
                        //string Productdescription = Emailds.Tables[0].Rows[i]["Product_Description"].ToString();

                        //string ProductImageIn = "https://oralb-professional.com/" + ImagePath;

                        str += @"   
                        


                                    <tr>

                                        <td width='25%' align='left' style='border-bottom: 1px solid #AAAAAC; padding: 10px' valign='top'><p style='font-size: 14px; font-weight: bold; color: #1879bf; letter-spacing: 1px; padding: 10px 0; margin: 0px;'> " + DsData.Tables[0].Rows[i]["LoadNetWeight"].ToString() + "</p></td> <td width='25%' align='left' style='border-bottom: 1px solid #AAAAAC; padding: 10px' valign='top'><p style='font-size: 14px; font-weight: bold; color: #1879bf; letter-spacing: 1px; padding: 10px 0; margin: 0px;'> " + DsData.Tables[0].Rows[i]["LoadTierWeight"].ToString() + "</p></td> <td width='25%' align='left' style='border-bottom: 1px solid #AAAAAC; padding: 10px' valign='top'><p style='font-size: 14px; font-weight: bold; color: #1879bf; letter-spacing: 1px; padding: 10px 0; margin: 0px;'> " + DsData.Tables[0].Rows[i]["NetWeight"].ToString() + "</p></td><td width='25%' align='left' style='border-bottom: 1px solid #AAAAAC; padding: 10px' valign='top'><p style='font-size: 14px; font-weight: bold; color: #1879bf; letter-spacing: 1px; padding: 10px 0; margin: 0px;'> " + DsData.Tables[0].Rows[i]["TierWeight"].ToString() + "</p></td></tr>";


                    }
                }
            }
        }

        DataSet Emailds = objdb.ByProcedure("Sp_ScrapReceive", new string[] { "flag", "Site_Id" },
                                                   new string[] { "9", LblSite_Id.Text }, "Dataset");
        if (Emailds != null)
        {
            if (Emailds.Tables.Count > 0)
            {
                if (Emailds.Tables[0].Rows.Count > 0)
                {
                    for (int j = 0; j < Emailds.Tables[0].Rows.Count; j++)
                    {
                        
                        abc = "<h4>" + "Hi " + Emailds.Tables[0].Rows[j]["EmpName"].ToString() + "," + "</h4>" +
                          

           "<h4>" + XYZ.ToString() + "<br/></h4>" +

           "<h4>" + RequestFormat.ToString() + "<br/></h4>" +

           "<h4>" + "Consigment Details : -" + "<br/></h4>" +


           "<table style='width: 100%; border:1px; border-collapse:collapse;'>" +
             "<tr style='background-color: blue; color: white; font-weight: 700;'><td style='border: 1px solid black; padding: 3px;'>Load Net Weight</td><td style='border: 1px solid black; padding: 3px;'>Load Tier Weight</td><td style='border: 1px solid black; padding: 3px;'>Receive Net Weight</td><td style='border: 1px solid black; padding: 3px;'>Receive Tier Weight</td></tr>" +
                str +

                    "</table>";
                        //Thread email = new Thread(delegate()
                        //{
                        //sendHtmlEmail("sonitsahu777@gmail.com", TxtEmailId.Text, Themessage, "Your Dental Prescription", "ORALB PRODUCTS", "smtp.gmail.com", 25);
                       // sendHtmlEmail("sonitsahu777@gmail.com", Emailds.Tables[0].Rows[j]["Email"].ToString(), abc, "Scrap Managment System", "Welcome to Oral-B Professional", "smtp.gmail.com", 25, ddlConsigment.SelectedItem.Text);

                        sendHtmlEmail("noreply@pg.com", Emailds.Tables[0].Rows[j]["Email"].ToString(), abc, "Scrap Managment System", "Welcome to Oral-B Professional", "smtp.gmail.com", 25, ddlConsigment.SelectedItem.Text);
                      
                      
                        //});
                        //email.IsBackground = true;
                        //email.Start();
                    }
                }
            }
        }
    }

    protected void sendHtmlEmail(string from_Email, string to_Email, string body, string from_Name, string Subject, string SMTP_IP, Int32 SMTP_Server_Port,string Refrence_no)
    {
        //create an instance of new mail message
        MailMessage mail = new MailMessage();

        //set the HTML format to true
        mail.IsBodyHtml = true;

        //create Alrternative HTML view
        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
        AlternateView htmlView1 = AlternateView.CreateAlternateViewFromString(body, null, "text/html");


        string[] ToemailArr = x.Split(',');
        int i = 0;

        mail.AlternateViews.Add(htmlView);
        mail.AlternateViews.Add(htmlView1);

        //set the "from email" address and specify a friendly 'from' name
        mail.From = new MailAddress(from_Email, from_Name);

        //set the "to" email address
        mail.To.Add(to_Email);

        //set the Email subject
        //mail.Subject = Subject;

        mail.Subject = "Scrap Load Received  -Con#" + Refrence_no.ToString();

        //set the SMTP info
        //System.Net.NetworkCredential cred = new System.Net.NetworkCredential("sonitsahu777@gmail.com", "SonitMehak7777");
       // SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
       
        System.Net.NetworkCredential cred = new System.Net.NetworkCredential("mdpdigital.ap.pg.com", "");
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtpgw.pg.com";
        smtp.EnableSsl = false;
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = cred;
        smtp.Port = 25;
        //send the email
        smtp.Send(mail);
    }
    protected void ddlBagNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gr in GridReceive.Rows)
            {
                Label BagBatchNo = (Label)gr.FindControl("LblBag_BatchNo");

                if (BagBatchNo.Text != ddlBagNo.SelectedValue && ddlBagNo.SelectedValue != "0")
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
    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gr in GridReceive.Rows)
            {
                Label BarCode = (Label)gr.FindControl("LblBarcode");

                if (BarCode.Text != TxtScanBarcode.Text)
                {
                    gr.Visible = false;
                    ShowAll.Visible = true;
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
    protected void ShowAll_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gr in GridReceive.Rows)
            {
                
                    gr.Visible = true;
                    ShowAll.Visible = false;
                
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
}