using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login2 : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDB objdb = new common();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    string email = "", user = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.Redirect("https://fedauthtst.pg.com/as/authorization.oauth2?client_id=E-Work+Permit+MX+Test&response_type=code&scope=openid%20pingid%20profile&redirect_uri=http%3A%2F%2Flocalhost%3A3000%2Fcallback.aspx&pfidpadapterid=Oauth", false);
            //Response.Redirect("https://fedauth.pg.com/as/authorization.oauth2?client_id=TWS+MX&response_type=code&scope=openid%20pingid%20profile&redirect_uri=https%3A%2F%2Fmdp-digital.na.pg.com%3A206%2Fcallback.aspx&state=3DMandideepApp&pfidpadapterid=Oauth");


            int loop1, loop2;
            System.Collections.Specialized.NameValueCollection coll = new System.Collections.Specialized.NameValueCollection();
            coll = Request.ServerVariables;
            String[] arr1 = coll.AllKeys;
            for (loop1 = 0; loop1 < arr1.Length; loop1++)
            {
                //Response.Write("Key: " + arr1[loop1] + "<br>");
                if (arr1[loop1].ToUpper() == "HTTP_EMAIL" || arr1[loop1].ToUpper() == "HTTP_MAIL" || arr1[loop1].ToUpper() == "HTTP_HTTP_EMAIL")
                {
                    String[] arr2 = coll.GetValues(arr1[loop1]);
                    if (!string.IsNullOrEmpty(arr2.Length.ToString()))
                    {
                        for (loop2 = 0; loop2 < arr2.Length; loop2++)
                        {
                            email = arr2[loop2];
                            Session["EMail"] = email;
                        }
                    }
                }
                if (arr1[loop1].ToUpper() == "HTTP_FULLNAME" || arr1[loop1].ToUpper() == "HTTP_HTTP_FULLNAME")
                {
                    String[] arr2 = coll.GetValues(arr1[loop1]);
                    if (!string.IsNullOrEmpty(arr2.Length.ToString()))
                    {
                        for (loop2 = 0; loop2 < arr2.Length; loop2++)
                        {
                            user = arr2[loop2];
                            Session["UserName"] = user;
                        }
                    }
                }
            }

            if (email != "")
            {

                Response.Cookies["Email"].Value = email;
                //Response.Redirect("Work/dashboard.aspx");
            }

            else if (user == "NewITC, Ion")
            {

                Response.Cookies["Email"].Value = "newitc.im";
                //Response.Cookies["Email"].Value = "emp@gmail.com";

            }
            else if (user == "Nandmehar, Santosh")
            {
                //Response.Cookies["Email"].Value = "nandmehar.sn@pg.com";
                //Response.Cookies["Email"].Value = "legal@gmail.com";
                Response.Cookies["Email"].Value = "john.b@pg.com";
            }
            //Response.Cookies["Email"].Value = "gedam.s@pg.com";
            if (Request.Cookies["Email"] == null)
            {
                Response.Redirect("Login.aspx");
            }


            Session["Module_Id"] = null;
            Session["Emp_Id"] = null;
            Session["UserCode"] = null;
            Session["SiteId"] = null;
            Session["Email"] = null;
            Session["UserType"] = "EMPLOYEE";
            Session["Type"] = 'E';
            ViewState["LoginType"] = "E";
            BtnEmployee.Visible = false;
            BtnContractor.Visible = false;
            // lblUser.Text = Session["UserName"].ToString();
            // lblEmail.Text = Session["EMail"].ToString();
            login.Visible = true;
            FillSite();
            TxtEmployeeEmail.Text = Response.Cookies["Email"].Value;
            string SiteId = "";
            DataSet GetSiteId = objdb.ByProcedure("Sp_UserLogin", new string[] { "flag", "Email" },
                         new string[] { "9", TxtEmployeeEmail.Text }, "dataset");
            if (GetSiteId != null)
            {
                if (GetSiteId.Tables.Count > 0)
                {
                    if (GetSiteId.Tables[0].Rows.Count > 0)
                    {
                        SiteId = GetSiteId.Tables[0].Rows[0]["SiteId"].ToString();
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Ooops!", "YOU DONT HAVE PERMISSION ACCESS THIS APPLICATION!");
                        return;
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Ooops!", "YOU DONT HAVE PERMISSION ACCESS THIS APPLICATION!");
                    return;
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Ooops!", "YOU DONT HAVE PERMISSION ACCESS THIS APPLICATION!");
                return;
            }


            DataSet ds_ACCESS = objdb.ByProcedure("Sp_UserLogin", new string[] { "flag", "Email", "SiteId" },
                           new string[] { "1", TxtEmployeeEmail.Text, SiteId.ToString() }, "dataset");
            if (ds_ACCESS != null)
            {
                if (ds_ACCESS.Tables.Count > 0)
                {
                    if (ds_ACCESS.Tables[0].Rows.Count > 0)
                    {

                        Session["UserName"] = ds_ACCESS.Tables[0].Rows[0]["EmpName"].ToString();
                        Session["Email"] = TxtEmployeeEmail.Text;
                        Session["SiteId"] = ds_ACCESS.Tables[0].Rows[0]["SiteId"].ToString();
                        Session["SiteName"] = ds_ACCESS.Tables[0].Rows[0]["SiteName"].ToString();
                        Session["Emp_Id"] = ds_ACCESS.Tables[0].Rows[0]["ID"].ToString();

                        BtnLogin.Visible = false;
                        Response.Redirect("~/Dashboard.aspx");
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Ooops!", "INVALID LOGIN CREDENTIALS!");

                    }
                }
            }

            //ValidateUser();



        }

    }







    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        string msg = "";
    //        string NewErrorMsg = "";
    //        string ConfirmErrorMsg = "";
    //        if (Page.IsValid)
    //        {

    //            bool a = NewValidatePassword(txtNewPassword.Text, out NewErrorMsg);

    //            if (a == false)
    //            {
    //                msg += NewErrorMsg.ToString();
    //                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", msg);
    //                return;
    //            }
    //            bool b = ConfirmValidatePassword(txtConfirmPassword.Text, out ConfirmErrorMsg);
    //            if (b == false)
    //            {
    //                msg += ConfirmErrorMsg.ToString();
    //                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", msg);
    //                return;
    //            }

    //            if (msg == "")
    //            {
    //                if (txtNewPassword.Text != txtConfirmPassword.Text)
    //                {
    //                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "New Password and Confirm Password not match. ");
    //                }
    //                else
    //                {

    //                    ds = objdb.ByProcedure("Sp_ForgotPassword",
    //                             new string[] { "flag", "Email" },
    //                             new string[] { "3", ViewState["mobile"].ToString() }, "dataset");
    //                    if (ds.Tables[0].Rows.Count > 0)
    //                    {
    //                        if (txtConfirmPassword.Text != "")
    //                        {
    //                            ds = objdb.ByProcedure("Sp_ForgotPassword",
    //                                new string[] { "flag", "Email", "Password" },
    //                                new string[] { "4", ViewState["mobile"].ToString(), txtConfirmPassword.Text }, "dataset");

    //                            if (ds.Tables[0].Rows.Count > 0)
    //                            {
    //                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
    //                                {
    //                                    lblMsg.ForeColor = System.Drawing.Color.Green;
    //                                    lblMsg.Text = "<b>Password Change Successfully.</b><br />";
    //                                }
    //                                else
    //                                {
    //                                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Password Change Failed!");
    //                                }
    //                            }
    //                            else
    //                            {
    //                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Invalid Credentials");
    //                            }


    //                            forget.Visible = false;
    //                            btnValidate.Visible = false;

    //                            btnSave.Visible = false;

    //                            ViewState["mobile"] = null;
    //                            ViewState["otp"] = null;
    //                            ds.Clear();
    //                        }
    //                        else
    //                        {
    //                            lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Ooops!", "New and Confirm Password not Match!");
    //                        }
    //                    }

    //                    else
    //                    {
    //                        lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Ooops!", "Old Password not Match, Try Again!");
    //                        ds.Clear();
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}

    protected void TxtUserName_TextChanged(object sender, EventArgs e)
    {

        lblMsg.Text = "";
        Session["UserCode"] = "";
        Session["SiteId"] = "";
        try
        {
            if (ddlSite.SelectedIndex != 0)
            {
                if (TxtUserName.Text != "")
                {
                    ForgetPassword.Visible = true;
                    forget.Visible = true;
                    login.Visible = false;
                    BtnLogin.Visible = false;
                    DataSet ds_ACCESS = objdb.ByProcedure("Sp_UserLogin", new string[] { "flag", "Contractor_Code", "SiteId" },
                        new string[] { "0", TxtUserName.Text, ddlSite.SelectedValue }, "dataset");
                    if (ds_ACCESS != null)
                    {
                        if (ds_ACCESS.Tables.Count > 0)
                        {
                            if (ds_ACCESS.Tables[0].Rows.Count > 0)
                            {
                                LblName.Text = ds_ACCESS.Tables[0].Rows[0]["Contractor_Name"].ToString();
                                LblVendorName.Text = ds_ACCESS.Tables[0].Rows[0]["Vendor_Name"].ToString();
                                Session["UserName"] = ds_ACCESS.Tables[0].Rows[0]["Contractor_Name"].ToString();
                                Session["VendorName"] = ds_ACCESS.Tables[0].Rows[0]["Vendor_Name"].ToString();
                                Session["UserCode"] = ds_ACCESS.Tables[0].Rows[0]["Contractor_Code"].ToString();
                                Session["SiteId"] = ds_ACCESS.Tables[0].Rows[0]["SiteId"].ToString();
                                Session["SiteName"] = ds_ACCESS.Tables[0].Rows[0]["SiteName"].ToString();
                                Session["Role"] = ds_ACCESS.Tables[0].Rows[0]["Role_Name"].ToString();
                                Session["Emp_Id"] = ds_ACCESS.Tables[0].Rows[0]["Contractor_Code"].ToString();
                                Session["Email"] = ds_ACCESS.Tables[0].Rows[0]["Contractor_Code"].ToString();
                                BtnLogin.Visible = true;
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Ooops!", "INVALID LOGIN CREDENTIALS!");

                            }
                        }
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Ooops!", "RECORD NOT FOUND!");

                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Ooops!", "PLEASE ENTER VALID UHID NO!");

                }
            }
            else
            {

                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Ooops!", "PLEASE SELECT SITE!");

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }
    protected void lnkbtnBack_Click(object sender, EventArgs e)
    {
        ForgetPassword.Visible = false;
        forget.Visible = false;
        login.Visible = true;
        TxtUserName.Text = "";
        LblName.Text = "";
        LblVendorName.Text = "";
        lblMsg.Text = "";
        ContractorUhid.Visible = false;
        ddlSite.SelectedIndex = 0;
        EmployeeEmailId.Visible = false;
    }
    protected void Linkbutton1_Click(object sender, EventArgs e)
    {
        string url = "WebCam/CS.aspx";
        string s = "window.open('" + url + "', 'popup_window', 'width=900,height=460,left=100,top=100,resizable=no');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }
    protected void BtnLogin_Click(object sender, EventArgs e)
    {
        //Session["ContractorCode"] = TxtUserName.Text;
        Session["UserType"] = "CONTRACTOR";
        Session["ImagePath"] = "";
        string message = string.Empty;
        string Imagepath = "";
        if (ImageUpload.HasFile)
        {
            string ext = System.IO.Path.GetExtension(this.ImageUpload.PostedFile.FileName);
            ext = ext.ToLower();

            if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
            {
                string fileName = System.IO.Path.GetFileName(ImageUpload.PostedFile.FileName);
                Session["fileName"] = fileName;
                ImageUpload.PostedFile.SaveAs(Server.MapPath("~/images/" + fileName).Replace("\\", "//"));
                Imagepath = "~/Images/" + fileName;
                Session["ImagePath"] = Imagepath.ToString();
            }
            else
            {
                message = "Invalid File Format";
            }
        }
        else
        {
            Imagepath = "~/Img/" + "AdminImage.Png";
            Session["ImagePath"] = Imagepath.ToString();

        }

        objdb.ByProcedure("Sp_UserLogin", new string[] { "flag", "UserName", "CodeOrEmailId", "ImagePath" },
                        new string[] { "2", LblName.Text, Session["UserCode"].ToString(), Imagepath.ToString() }, "dataset");
        Response.Redirect("~/Dashboard.aspx");
    }
    protected void BtnContractor_Click(object sender, EventArgs e)
    {
        ContractorUhid.Visible = true;
        UhidCardNo.Visible = false;
        EmployeeEmailId.Visible = false;
        ViewState["LoginType"] = "C";
        Session["Type"] = 'C';
        ddlSite.SelectedIndex = 0;
    }
    protected void BtnEmployee_Click(object sender, EventArgs e)
    {
        Session["UserType"] = "EMPLOYEE";
        Session["Type"] = 'E';
        ViewState["LoginType"] = "E";
        ContractorUhid.Visible = true;
        UhidCardNo.Visible = false;
        EmployeeEmailId.Visible = false;
        ddlSite.SelectedIndex = 0;
        Response.Redirect("~/Frm_EmployeeLogin.aspx");
    }
    protected void FillSite()
    {
        try
        {
            ds = objdb.ByProcedure("Sp_SiteMaster", new string[] { "flag" }, new string[] { "5" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlSite.DataTextField = "SiteName";
                ddlSite.DataValueField = "SiteID";
                ddlSite.DataSource = ds;
                ddlSite.DataBind();
                ddlSite.Items.Insert(0, new ListItem("Select Site", "0"));
                ddlSite.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSite.SelectedIndex == 0)
            {
                UhidCardNo.Visible = false;
                EmployeeEmailId.Visible = false;
            }
            else
            {
                if (ViewState["LoginType"].ToString() == "C")
                {
                    UhidCardNo.Visible = true;
					TxtUserName.Focus();
                }
                else
                {
                    EmployeeEmailId.Visible = true;
					TxtEmployeeEmail.Focus();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void TxtEmployeeEmail_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            try
            {
                if (ddlSite.SelectedIndex != 0)
                {
                    if (TxtEmployeeEmail.Text != "")
                    {
                        ForgetPassword.Visible = false;
                        forget.Visible = false;
                        login.Visible = false;
                        BtnLogin.Visible = false;
                        DataSet ds_ACCESS = objdb.ByProcedure("Sp_UserLogin", new string[] { "flag", "Email", "SiteId" },
                            new string[] { "1", TxtEmployeeEmail.Text, "1" }, "dataset");
                        if (ds_ACCESS != null)
                        {
                            if (ds_ACCESS.Tables.Count > 0)
                            {
                                if (ds_ACCESS.Tables[0].Rows.Count > 0)
                                {

                                    Session["UserName"] = ds_ACCESS.Tables[0].Rows[0]["EmpName"].ToString();
                                    Session["Email"] = TxtEmployeeEmail.Text;
                                    Session["SiteId"] = ds_ACCESS.Tables[0].Rows[0]["SiteId"].ToString();
                                    Session["SiteName"] = ds_ACCESS.Tables[0].Rows[0]["SiteName"].ToString();
                                    Session["Emp_Id"] = ds_ACCESS.Tables[0].Rows[0]["ID"].ToString();

                                    BtnLogin.Visible = false;
                                    Response.Redirect("~/Dashboard.aspx");
                                }
                                else
                                {
                                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Ooops!", "INVALID LOGIN CREDENTIALS!");

                                }
                            }
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Ooops!", "RECORD NOT FOUND!");

                        }
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Ooops!", "PLEASE ENTER VALID UHID NO!");

                    }
                }
                else
                {

                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Ooops!", "PLEASE SELECT SITE!");

                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}