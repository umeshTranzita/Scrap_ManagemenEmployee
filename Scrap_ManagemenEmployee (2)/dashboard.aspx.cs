using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class dashboard : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDB objdb = new common();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    string email = "", user = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (Session["UserType"].ToString() == "SuperAdmin" || Session["UserType"].ToString() == "SALES")
            //{
            //    AdminPanel.Visible = true;
            //    DoctorPanel.Visible = false;
            //    ds = objdb.ByProcedure("Sp_Reports", new string[] { "flag" },
            //                       new string[] { "7" }, "Dataset");
            //    if (ds != null)
            //    {
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            LblTotDoctor.Text = ds.Tables[0].Rows[0]["TotalDoctor"].ToString();
            //            LblTotRegPatient.Text = ds.Tables[1].Rows[0]["TotalPatient"].ToString();
            //            LblProducts.Text = ds.Tables[2].Rows[0]["TotalProduct"].ToString();
            //            LblPatientVisitonamazon.Text = ds.Tables[3].Rows[0]["Visit_Count"].ToString();
            //        }
            //    }
            //}
            //if (Session["UserType"].ToString() == "DOCTOR")
            //{
            //    AdminPanel.Visible = false;
            //    DoctorPanel.Visible = true;
            //    ds = objdb.ByProcedure("Sp_Reports", new string[] { "flag","Doctor_Id" },
            //                       new string[] { "8", Session["ACCESS_User"].ToString() }, "Dataset");
            //    if (ds != null)
            //    {
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            LblDocTotPatientVisit.Text = ds.Tables[0].Rows[0]["Visit_Count"].ToString();
            //        }
            //    }
            
            //}
            
        }
    }
    protected void Btn_Add_Click(object sender, EventArgs e)
    {

    }
}