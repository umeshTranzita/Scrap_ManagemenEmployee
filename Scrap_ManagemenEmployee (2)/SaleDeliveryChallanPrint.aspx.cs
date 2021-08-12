using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class SaleDeliveryChallanPrint : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDB objdb = new common();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Session["PPID"] == "" || Session["PPID"] == null || Session["Refrence_No"] == null || Session["Refrence_No"] == "")
            {
                Response.Redirect("~/Login.aspx");
            }
            ViewState["PPID"] = Session["PPID"];
            ViewState["Refrence_No"] = Session["Refrence_No"];
            ViewState["Site_Id"] = Session["SiteId"];

            ds = objdb.ByProcedure("Sp_Sold", new string[] { "flag", "Site_Id", "PPID", "Consigment_No" },
                new string[] { "9", ViewState["Site_Id"].ToString(), ViewState["PPID"].ToString(), ViewState["Refrence_No"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                LblCompanyName.Text = ds.Tables[0].Rows[0]["SiteDescription"].ToString();
                LblAddress.Text = ds.Tables[0].Rows[0]["SiteAddress"].ToString();
                LblGst.Text = ds.Tables[0].Rows[0]["SiteGstNo"].ToString();
                LblConsigneeName.Text = ds.Tables[0].Rows[0]["ConsigneeName"].ToString();
                LblConsigneeAddress.Text = ds.Tables[0].Rows[0]["ConsigneeAddress"].ToString();
                LblConsigneeGST.Text = ds.Tables[0].Rows[0]["ConsigneeGst"].ToString();
                //LblLockNo.Text = ds.Tables[1].Rows[0]["Lock_No"].ToString();
                LblVehicleNo.Text = ds.Tables[2].Rows[0]["Truck_No"].ToString();
                LblChallanDate.Text = ds.Tables[1].Rows[0]["ChallanDate"].ToString();
                LblRemovalTime.Text = ds.Tables[1].Rows[0]["RemovalTime"].ToString();
                LblRemovalDate.Text = ds.Tables[1].Rows[0]["ChallanDate"].ToString();

                Repeater1.DataSource = ds.Tables[2];
                Repeater1.DataBind();

                int rowcount = ds.Tables[2].Rows.Count;
                if (rowcount == 12)
                {
                    trBlank.Visible = false;
                }
                else
                {
                    int heighttd = 12 - rowcount;
                    heighttd = heighttd * 17;
                    //tdBlank.Attributes.Add("style", "height:" + height.ToString());
                    testSpace.Style.Add("height", heighttd.ToString() + "px");
                }
            }

        
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("../admin/Billing.aspx");
    }
}