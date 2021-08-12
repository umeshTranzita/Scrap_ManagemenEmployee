using System;
using System.Data;
using System.Text;

public partial class Frm_EmployeeLogin : System.Web.UI.Page
{
    common obj = new common();
    DataSet ds = new DataSet();
    DataSet dsMenu = new DataSet();
    DataSet dsForm = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
		string email = "", user = "";
        if (!IsPostBack)
        {
           //Session.Abandon();
             // if (Request.Cookies["UserInfo"] != null)
             // {
                 // HttpCookie myCookie = new HttpCookie("UserInfo");
                 // myCookie.Expires = DateTime.Now.AddDays(-1d);
                 // Response.Cookies.Add(myCookie);
             // }
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

        }
    }
}