using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class index : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDB objdb = new common();
    
    string email, user;
    protected void Page_PreRender(object sender, EventArgs e)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session.Abandon();

                int loop1, loop2;
                System.Collections.Specialized.NameValueCollection coll = new System.Collections.Specialized.NameValueCollection();
                coll = Request.ServerVariables;
                String[] arr1 = coll.AllKeys;
                for (loop1 = 0; loop1 < arr1.Length; loop1++)
                {
                    //Response.Write("Key: " + arr1[loop1] + "<br>");
                    if (arr1[loop1].ToUpper() == "HTTP_EMAIL")
                    {
                        String[] arr2 = coll.GetValues(arr1[loop1]);
                        if (!string.IsNullOrEmpty(arr2.Length.ToString()))
                        {
                            for (loop2 = 0; loop2 < arr2.Length; loop2++)
                            {
                                email = arr2[loop2];
                                Session["EMail"] = email;
                                //cookie["EMail"] = email;
                            }
                        }
                    }
                    if (arr1[loop1].ToUpper() == "HTTP_FULLNAME")
                    {
                        String[] arr2 = coll.GetValues(arr1[loop1]);
                        if (!string.IsNullOrEmpty(arr2.Length.ToString()))
                        {
                            for (loop2 = 0; loop2 < arr2.Length; loop2++)
                            {
                                user = arr2[loop2];
                                Session["UserName"] = user;
                                //cookie["UserName"] = user;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception Ex)
        {
        }
    }
    protected void btnlogin_Click(object sender, EventArgs e)
    {
      // Session["EMail"] = txtUsername.Text;
        // Session["UserName"] = "";

        // Response.Redirect("login.aspx");
		     try
        {
            LblMsg.Text = "";      
         if (txtpassword.Text != "")
         {
            DataSet ds_ACCESS1 = objdb.ByProcedure("Sp_APP_ACCESS_EMAIL", new string[] { "flag", "Email_ID", "Password" }, new string[] { "4", txtUsername.Text, txtpassword.Text }, "dataset");
             if (ds_ACCESS1 != null)
             {
                 if (ds_ACCESS1.Tables[0].Rows.Count > 0)
                 {
                     Session["EMail"] = txtUsername.Text;
                     Session["UserName"] = "";
                     Response.Redirect("login.aspx");
                 }
                 else if (ds_ACCESS1.Tables[1].Rows.Count > 0)
                 {
                     Session["EMail"] = txtUsername.Text;
                     Session["UserName"] = "";
                     Response.Redirect("login.aspx");
                 }
                 else
                 {
                     LblMsg.Text = "";
                     LblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Please Enter Valid UserName or Password");
                 }
             }
             else
             {
                 LblMsg.Text = "";
                 LblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Please Enter Valid Password");
             }
         }
         else
         {
             LblMsg.Text = "";
             LblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Please Enter Password");
         }
     }
     catch (Exception ex)
     {
        LblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
     }
    }
   
}