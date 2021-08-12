using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

public partial class Frm_CurrentStatusReport : System.Web.UI.Page
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
            GetData();
            Todate.Attributes.Add("class", "active");
            Fromdate.Attributes.Add("class", "active");
            FillFilter();
            FillSite();
        }
    }
    public void GetData()
    {
        try
        {
            ds = null;
            //LblErrorMsg.Text = "";
            lblMsg.Text = "";
            GridStatus.DataSource = null;
            GridStatus.DataBind();
            ds = objdb.ByProcedure("Sp_Reports", new string[] { "flag", "SiteId" },
               new string[] { "1", ViewState["SiteId"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridStatus.DataSource = ds.Tables[0];
                        GridStatus.DataBind();
                        foreach (GridViewRow item in GridStatus.Rows)
                        {
                           
                            Label LoadWeight = (Label)item.FindControl("LblTotalLoadWeight");
                            Label ReceiveWeight = (Label)item.FindControl("LblTotalReceiveWeight");
                            Label Consigment = (Label)item.FindControl("LblConsigment");
                            DataSet dsNew = objdb.ByProcedure("Sp_Reports", new string[] { "flag", "SiteId", "Consigment_No" },
                             new string[] { "18", ViewState["SiteId"].ToString(), Consigment.Text }, "dataset");
                            if (dsNew != null)
                            {
                                if (dsNew.Tables.Count > 0)
                                {
                                    if (dsNew.Tables[0].Rows.Count > 0)
                                    {
                                        LoadWeight.Text = dsNew.Tables[0].Rows[0]["TotalLoadWeight"].ToString();
                                        ReceiveWeight.Text = dsNew.Tables[0].Rows[0]["TotalReceiveWeight"].ToString();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        GridStatus.DataSource = null;
                        GridStatus.DataBind();

                    }
                }
                else
                {
                    GridStatus.DataSource = null;
                    GridStatus.DataBind();

                }
            }
            else
            {
                GridStatus.DataSource = null;
                GridStatus.DataBind();

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
            // LblErrorMsg.Text = "";
            lblMsg.Text = "";


            if (ddlStatus.SelectedIndex == 0)
            {
                ds = objdb.ByProcedure("Sp_Reports", new string[] { "flag", "SiteId", "FromDate", "ToDate" },
                   new string[] { "2", ddlSite.SelectedValue, Convert.ToDateTime(TxtFromdate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(TxtTodate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            }
            else
            {
                ds = objdb.ByProcedure("Sp_Reports", new string[] { "flag", "SiteId", "FromDate", "ToDate","Status" },
                      new string[] { "7", ddlSite.SelectedValue, Convert.ToDateTime(TxtFromdate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(TxtTodate.Text, cult).ToString("yyyy/MM/dd"), ddlStatus.SelectedItem.Text }, "dataset");
            }



            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridStatus.DataSource = ds.Tables[0];
                        GridStatus.DataBind();
                        foreach (GridViewRow item in GridStatus.Rows)
                        {

                            Label LoadWeight = (Label)item.FindControl("LblTotalLoadWeight");
                            Label ReceiveWeight = (Label)item.FindControl("LblTotalReceiveWeight");
                            Label Consigment = (Label)item.FindControl("LblConsigment");
                            DataSet dsNew = objdb.ByProcedure("Sp_Reports", new string[] { "flag", "SiteId", "Consigment_No" },
                             new string[] { "18", ddlSite.SelectedValue, Consigment.Text }, "dataset");
                            if (dsNew != null)
                            {
                                if (dsNew.Tables.Count > 0)
                                {
                                    if (dsNew.Tables[0].Rows.Count > 0)
                                    {
                                        LoadWeight.Text = dsNew.Tables[0].Rows[0]["TotalLoadWeight"].ToString();
                                        ReceiveWeight.Text = dsNew.Tables[0].Rows[0]["TotalReceiveWeight"].ToString();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        GridStatus.DataSource = null;
                        GridStatus.DataBind();
                    }

                }
                else
                {
                    GridStatus.DataSource = null;
                    GridStatus.DataBind();
                }
            }
            else
            {
                GridStatus.DataSource = null;
                GridStatus.DataBind();
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ds = null;
            lblMsg.Text = "";
            ddlStatus.SelectedIndex = 0;
            GridStatus.DataSource = null;
            GridStatus.DataBind();
            lstEmployee.ClearSelection();
            if (ddlFilter.SelectedItem.Text == "Custom")
            {
                CustomPanel.Visible = true;

            }

            else
            {
                CustomPanel.Visible = false;
                ds = objdb.ByProcedure("Sp_Reports", new string[] { "flag", "SiteId", "FromDate", "ToDate", "FilterValue","OrderBy" },
                       new string[] { "4", ddlSite.SelectedValue, TxtFromdate.Text, TxtTodate.Text, ddlFilter.SelectedItem.Text,ddlOrder.SelectedItem.Text }, "dataset");
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            GridStatus.DataSource = ds.Tables[0];
                            GridStatus.DataBind();
                            foreach (GridViewRow item in GridStatus.Rows)
                            {

                                Label LoadWeight = (Label)item.FindControl("LblTotalLoadWeight");
                                Label ReceiveWeight = (Label)item.FindControl("LblTotalReceiveWeight");
                                Label Consigment = (Label)item.FindControl("LblConsigment");
                                DataSet dsNew = objdb.ByProcedure("Sp_Reports", new string[] { "flag", "SiteId", "Consigment_No" },
                                 new string[] { "18", ViewState["SiteId"].ToString(), Consigment.Text }, "dataset");
                                if (dsNew != null)
                                {
                                    if (dsNew.Tables.Count > 0)
                                    {
                                        if (dsNew.Tables[0].Rows.Count > 0)
                                        {
                                            LoadWeight.Text = dsNew.Tables[0].Rows[0]["TotalLoadWeight"].ToString();
                                            ReceiveWeight.Text = dsNew.Tables[0].Rows[0]["TotalReceiveWeight"].ToString();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
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
                ddlSite.SelectedValue = ViewState["SiteId"].ToString();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillFilter()
    {
        try
        {
            ds = objdb.ByProcedure("Sp_Reports", new string[] { "flag", "SiteID" }, new string[] { "3", ddlSite.SelectedValue }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlFilter.DataTextField = "Filter_Type";
                ddlFilter.DataValueField = "Filter_TypeValue";
                ddlFilter.DataSource = ds;
                ddlFilter.DataBind();
                ddlFilter.Items.Insert(0, new ListItem("Select Filter", "0"));
                ddlFilter.SelectedIndex = 1;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridStatus.DataSource = null;
        GridStatus.DataBind();
        ddlFilter.SelectedIndex = 0;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=CurrentStatusReport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            GridStatus.AllowPaging = false;
            //this.BindGrid();

            GridStatus.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in GridStatus.HeaderRow.Cells)
            {
                cell.BackColor = GridStatus.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in GridStatus.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = GridStatus.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = GridStatus.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            GridStatus.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void lstEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            lblMsg.Text = "";
            GridStatus.DataSource = null;
            GridStatus.DataBind();

            string StatusList = "";

            foreach (ListItem item in lstEmployee.Items)
            {
                if (item.Selected)
                {
                    StatusList += item.Text + ",";
                }
            }

            ds = objdb.ByProcedure("Sp_Reports", new string[] { "flag", "SiteId", "FromDate", "ToDate", "FilterValue", "Status","OrderBy" },
                       new string[] { "6", ddlSite.SelectedValue, TxtFromdate.Text, TxtTodate.Text, ddlFilter.SelectedItem.Text, StatusList.ToString(),ddlOrder.SelectedItem.Text}, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridStatus.DataSource = ds.Tables[0];
                        GridStatus.DataBind();
                        foreach (GridViewRow item in GridStatus.Rows)
                        {

                            Label LoadWeight = (Label)item.FindControl("LblTotalLoadWeight");
                            Label ReceiveWeight = (Label)item.FindControl("LblTotalReceiveWeight");
                            Label Consigment = (Label)item.FindControl("LblConsigment");
                            DataSet dsNew = objdb.ByProcedure("Sp_Reports", new string[] { "flag", "SiteId", "Consigment_No" },
                             new string[] { "18", ddlSite.SelectedValue, Consigment.Text }, "dataset");
                            if (dsNew != null)
                            {
                                if (dsNew.Tables.Count > 0)
                                {
                                    if (dsNew.Tables[0].Rows.Count > 0)
                                    {
                                        LoadWeight.Text = dsNew.Tables[0].Rows[0]["TotalLoadWeight"].ToString();
                                        ReceiveWeight.Text = dsNew.Tables[0].Rows[0]["TotalReceiveWeight"].ToString();
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlOrder_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlOrder_SelectedIndexChanged1(object sender, EventArgs e)
    {
        ddlFilter.SelectedIndex = 0;
    }
    protected void GridStatus_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try 
        {
           
            if (e.CommandName == "Dispatch")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                Label lblRefNO = (Label)gvrow.FindControl("LblRefrenceNo");
            

                ds = objdb.ByProcedure("Sp_ScrapLoad", new string[] { "flag",  "Site_Id", "Refrence_No" },
                                new string[] { "18", ddlSite.SelectedValue, lblRefNO.Text}, "Dataset");
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridView1.DataSource = ds.Tables[0];
                        GridView1.DataBind();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "showModal();", true);
                        //  ClientScript.RegisterStartupScript(GetType(), "Show", "<script> $('#myModal').modal('toggle');</script>"); 
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}