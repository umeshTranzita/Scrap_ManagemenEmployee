using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Frm_PostProcessReport : System.Web.UI.Page
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
            ds = objdb.ByProcedure("Sp_Reports", new string[] { "flag", "SiteID" }, new string[] { "3", ViewState["SiteId"].ToString() }, "dataset");
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
    public void GetData()
    {
        try
        {
            ds = null;
            //LblErrorMsg.Text = "";
            lblMsg.Text = "";
            ds = objdb.ByProcedure("Sp_Reports", new string[] { "flag", "SiteId","OrderBy" },
               new string[] { "13", ViewState["SiteId"].ToString(),ddlOrder.SelectedItem.Text }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridStatus.DataSource = ds.Tables[0];
                        GridStatus.DataBind();
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
            //ddlStatus.SelectedIndex = 0;
            GridStatus.DataSource = null;
            GridStatus.DataBind();
            if (ddlFilter.SelectedItem.Text == "Custom")
            {
                CustomPanel.Visible = true;

            }

            else
            {
                CustomPanel.Visible = false;
                ds = objdb.ByProcedure("Sp_Reports", new string[] { "flag", "SiteId", "FromDate", "ToDate", "FilterValue","Orderby" },
                       new string[] { "12", ddlSite.SelectedValue, TxtFromdate.Text, TxtTodate.Text, ddlFilter.SelectedItem.Text,ddlOrder.SelectedItem.Text }, "dataset");
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            GridStatus.DataSource = ds.Tables[0];
                            GridStatus.DataBind();
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
    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ds = null;
            //LblErrorMsg.Text = "";
            lblMsg.Text = "";
            ds = objdb.ByProcedure("Sp_Reports", new string[] { "flag", "SiteId" },
               new string[] { "13", ddlSite.SelectedValue }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridStatus.DataSource = ds.Tables[0];
                        GridStatus.DataBind();
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

            string StatusList = "";

            foreach (ListItem item in lstEmployee.Items)
            {
                if (item.Selected)
                {
                    StatusList += item.Text + ",";
                }
            }

            if (StatusList.ToString() == "")
            {
                ds = objdb.ByProcedure("Sp_Reports", new string[] { "flag", "SiteId", "FromDate", "ToDate","OrderBy" },
                  new string[] { "14", ddlSite.SelectedValue, Convert.ToDateTime(TxtFromdate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(TxtTodate.Text, cult).ToString("yyyy/MM/dd"),ddlOrder.SelectedItem.Text }, "dataset");
            }
            else
            {
                ds = objdb.ByProcedure("Sp_Reports", new string[] { "flag", "SiteId", "FromDate", "ToDate", "Status","OrderBy" },
                      new string[] { "16", ddlSite.SelectedValue, Convert.ToDateTime(TxtFromdate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(TxtTodate.Text, cult).ToString("yyyy/MM/dd"), StatusList.ToString(),ddlOrder.SelectedItem.Text }, "dataset");
            }

           

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridStatus.DataSource = ds.Tables[0];
                        GridStatus.DataBind();
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
    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=PostProcessReportReport.xls");
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

            ds = objdb.ByProcedure("Sp_Reports", new string[] { "flag", "SiteId", "FromDate", "ToDate", "FilterValue", "Status", "OrderBy" },
                       new string[] { "15", ddlSite.SelectedValue, TxtFromdate.Text, TxtTodate.Text, ddlFilter.SelectedItem.Text, StatusList.ToString(), ddlOrder.SelectedItem.Text }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridStatus.DataSource = ds.Tables[0];
                        GridStatus.DataBind();
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
        ddlFilter.SelectedIndex = 0;
    }
    protected void GridStatus_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {



            if (e.CommandName == "Dispatch")
            {
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                Label lblRefNO = (Label)gvrow.FindControl("lblRowNumber");


                ds = objdb.ByProcedure("Sp_Reports", new string[] { "flag", "SiteId", "PPID" },
                                new string[] { "17", ViewState["SiteId"].ToString(), lblRefNO.ToolTip.ToString() }, "Dataset");
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