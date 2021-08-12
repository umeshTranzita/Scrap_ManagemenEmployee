using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;


public partial class Frm_LogHistory : System.Web.UI.Page
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

            Fromdate.Attributes.Add("class", "active");
            Todate.Attributes.Add("class", "active");
            GetLogData();
            FillRemark();

        }
    }
    public void GetLogData()
    {
        try
        {
            ds = null;

            lblMsg.Text = "";
            ds = objdb.ByProcedure("Sp_Reports", new string[] { "flag", "SiteId" },
               new string[] { "5", ViewState["SiteId"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridLog.DataSource = ds.Tables[0];
                        GridLog.DataBind();

                    }
                    else
                    {
                        GridLog.DataSource = null;
                        GridLog.DataBind();

                    }
                }
                else
                {
                    GridLog.DataSource = null;
                    GridLog.DataBind();

                }
            }
            else
            {
                GridLog.DataSource = null;
                GridLog.DataBind();

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridLog_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridLog.PageIndex = e.NewPageIndex;
        GetLogData();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ds = null;
            // LblErrorMsg.Text = "";
            lblMsg.Text = "";

            GridLog.DataSource = null;
            GridLog.DataBind();

            if (ddlRemark.SelectedIndex == 0 || ddlRemark.SelectedIndex == 1)
            {
                ds = objdb.ByProcedure("Sp_Reports", new string[] { "flag", "SiteId", "FromDate", "ToDate" },
                   new string[] { "8", ViewState["SiteId"].ToString(), Convert.ToDateTime(TxtFromdate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(TxtTodate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            }


            else
            {
                ds = objdb.ByProcedure("Sp_Reports", new string[] { "flag", "SiteId", "FromDate", "ToDate", "Remark" },
                      new string[] { "11", ViewState["SiteId"].ToString(), Convert.ToDateTime(TxtFromdate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(TxtTodate.Text, cult).ToString("yyyy/MM/dd"), ddlRemark.SelectedValue }, "dataset");
            }

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridLog.DataSource = ds.Tables[0];
                        GridLog.DataBind();
                    }
                    else
                    {
                        GridLog.DataSource = null;
                        GridLog.DataBind();
                    }

                }
                else
                {
                    GridLog.DataSource = null;
                    GridLog.DataBind();
                }
            }
            else
            {
                GridLog.DataSource = null;
                GridLog.DataBind();
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void FillRemark()
    {
        try
        {
            ds = objdb.ByProcedure("Sp_Reports", new string[] { "flag", "SiteID" }, new string[] { "9", ViewState["SiteId"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlRemark.DataTextField = "Remark";
                ddlRemark.DataValueField = "Remark";
                ddlRemark.DataSource = ds;
                ddlRemark.DataBind();
                ddlRemark.Items.Insert(0, new ListItem("Select All", "0"));
                ddlRemark.Items.Insert(1, new ListItem("Custom", "1"));
                ddlRemark.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlRemark_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ds = null;
            // LblErrorMsg.Text = "";
            lblMsg.Text = "";

            GridLog.DataSource = null;
            GridLog.DataBind();
            PanelCustom.Visible = false;

            if (ddlRemark.SelectedIndex == 0)
            {
                GetLogData();
            }
            else if (ddlRemark.SelectedIndex == 1)
            {
                PanelCustom.Visible = true;
                GetLogData();
            }
            else
            {
                ds = objdb.ByProcedure("Sp_Reports", new string[] { "flag", "SiteId", "Remark" },
                   new string[] { "10", ViewState["SiteId"].ToString(), ddlRemark.SelectedValue }, "dataset");

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            GridLog.DataSource = ds.Tables[0];
                            GridLog.DataBind();
                        }
                        else
                        {
                            GridLog.DataSource = null;
                            GridLog.DataBind();
                        }

                    }
                    else
                    {
                        GridLog.DataSource = null;
                        GridLog.DataBind();
                    }
                }
                else
                {
                    GridLog.DataSource = null;
                    GridLog.DataBind();
                }
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
        Response.AddHeader("content-disposition", "attachment;filename=LogHistoryReport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            GridLog.AllowPaging = false;
            //this.BindGrid();

            GridLog.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in GridLog.HeaderRow.Cells)
            {
                cell.BackColor = GridLog.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in GridLog.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = GridLog.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = GridLog.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            GridLog.RenderControl(hw);

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
}