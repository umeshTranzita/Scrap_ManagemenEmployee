using Microsoft.Reporting.WebForms;
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
public partial class Frm_TemplateData : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDB objdb = new common();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillSite();
            Fromdate.Attributes.Add("class", "active");
            Todate.Attributes.Add("class", "active");
        }
    }
      protected void FillSite()
    {
        try
        {
            if (Session["Email"] == "" || Session["Email"] == null || Session["SiteId"] == null || Session["SiteId"] == "")
            {
                Response.Redirect("~/Login.aspx");

            }
            ViewState["SiteId"] = Session["SiteId"].ToString();
            ViewState["UserCode"] = Session["Email"].ToString();
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        btnExport.Visible = true;
        ds = null;
        //LblErrorMsg.Text = "";
        lblMsg.Text = "";
        GridData.DataSource = null;
        GridData.DataBind();
        ds = objdb.ByProcedure("Sp_TemplateData", new string[] { "flag", "SiteId", "FromDate", "ToDate" },
           new string[] { "3", ddlSite.SelectedValue, Convert.ToDateTime(TxtFromdate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(TxtTodate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridData.DataSource = ds.Tables[0];
                    GridData.DataBind();


                    //foreach (GridViewRow item in GridData.Rows)
                    //{
                    //    Label PadReceiveWeight = (Label)item.FindControl("lblPadReceiveWeight");
                    //    Label DiaReceiveWeight = (Label)item.FindControl("lblDiaReceiveWeight");
                    //    Label PadIssueWeight = (Label)item.FindControl("lblPadIssueWeight");
                    //    Label DiaIssueWeight = (Label)item.FindControl("lblDiaperIssueWeight");
                    //    Label RemnantPad = (Label)item.FindControl("lblRemnantPad");
                    //    Label RemnantDiaper = (Label)item.FindControl("lblRemnantDiaper");
                    //    Label OpeningPadBal = (Label)item.FindControl("lblOpeningPad");
                    //    Label OpeningDiaBal = (Label)item.FindControl("lblOpeningDia");
                        

                    //    RemnantPad.Text = Convert.ToString(Math.Round((double.Parse(PadReceiveWeight.Text) + double.Parse(OpeningPadBal.Text)) - double.Parse(PadIssueWeight.Text), 2));
                    //    RemnantDiaper.Text = Convert.ToString(Math.Round((double.Parse(DiaReceiveWeight.Text) + double.Parse(OpeningDiaBal.Text)) - double.Parse(DiaIssueWeight.Text), 2));


                    //}





                    double TotalPadReceiveWt = 0, TotalDiaReceiveWt = 0, TotalDiaIssueWt = 0, TotalPadIssueWt = 0,TotalClosingPad = 0,TotalClosingDia = 0;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        TotalPadReceiveWt = TotalPadReceiveWt + double.Parse(ds.Tables[0].Rows[i]["PadReceiveWeight"].ToString());
                        TotalDiaReceiveWt = TotalDiaReceiveWt + double.Parse(ds.Tables[0].Rows[i]["DiaReceiveWeight"].ToString());
                        TotalDiaIssueWt = TotalDiaIssueWt + double.Parse(ds.Tables[0].Rows[i]["DiaIssueWeight"].ToString());
                        TotalPadIssueWt = TotalPadIssueWt + double.Parse(ds.Tables[0].Rows[i]["PadIssueWeight"].ToString());
                        TotalClosingPad = TotalClosingPad + double.Parse(ds.Tables[0].Rows[i]["PadClosingBal"].ToString());
                        TotalClosingDia = TotalClosingDia + double.Parse(ds.Tables[0].Rows[i]["DiaClosingBal"].ToString());
                    }

                    GridData.FooterRow.Cells[1].Font.Size = 15;
                    GridData.FooterRow.Cells[3].Font.Size = 15;
                    GridData.FooterRow.Cells[4].Font.Size = 15;
                    GridData.FooterRow.Cells[6].Font.Size = 15;
                    GridData.FooterRow.Cells[7].Font.Size = 15;
                    GridData.FooterRow.Cells[8].Font.Size = 15;
                    GridData.FooterRow.Cells[9].Font.Size = 15;
                    GridData.FooterRow.Cells[10].Font.Size = 15;
                    GridData.FooterRow.Cells[11].Font.Size = 15;

                    GridData.FooterRow.Cells[1].Text = "TOTAL";
                    GridData.FooterRow.Cells[3].Text = ds.Tables[1].Rows[0]["OpeningPadBal"].ToString();
                    GridData.FooterRow.Cells[4].Text = ds.Tables[1].Rows[0]["OpeningDiaBal"].ToString();
                    GridData.FooterRow.Cells[6].Text = TotalPadReceiveWt.ToString();
                    GridData.FooterRow.Cells[7].Text = TotalDiaReceiveWt.ToString();
                    GridData.FooterRow.Cells[8].Text = TotalPadIssueWt.ToString();
                    GridData.FooterRow.Cells[9].Text = TotalDiaIssueWt.ToString();
                    GridData.FooterRow.Cells[10].Text = ds.Tables[2].Rows[0]["PadClosingBal"].ToString();
                    GridData.FooterRow.Cells[11].Text = ds.Tables[2].Rows[0]["DiaClosingBal"].ToString();
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "No Record Foud");
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "No Record Foud");
            }
        }
        else
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "No Record Foud");
        }

        GridPostProcess.DataSource = null;
        GridPostProcess.DataBind();
        ds = null;
        ds = objdb.ByProcedure("Sp_TemplateData", new string[] { "flag", "SiteId", "FromDate", "ToDate" },
          new string[] { "2", ddlSite.SelectedValue, Convert.ToDateTime(TxtFromdate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(TxtTodate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridPostProcess.DataSource = ds.Tables[0];
                    GridPostProcess.DataBind();
                    double TotalPadLossWt = 0, TotalDiaLossWt = 0, TotalPadLossPercentage = 0, TotalDiaLossPercentage = 0, TotalPadIssueWt = 0, TotalDiaIssueWt = 0, TotalPadClosing = 0, TotalDiaClosing = 0;
                  
                    foreach (GridViewRow item in GridPostProcess.Rows)
                    {

                        Label PadIssueWeight = (Label)item.FindControl("lblPadIssueWeight");
                        Label DiaIssueWeight = (Label)item.FindControl("lblDiaperIssueWeight");
                        Label SalePadCore = (Label)item.FindControl("lblSale_PadCoreWeight");
                        Label SalePadScrap = (Label)item.FindControl("lblSale_PadScrapWeight");
                        Label SaleDiaScrap = (Label)item.FindControl("lblSale_DiaperScrapWeight");
                        Label SaleDiaCore = (Label)item.FindControl("lblSale_DiaperCoreWeight");
                        Label RemnantPad = (Label)item.FindControl("lblRemnantPad");
                        Label RemnantDia = (Label)item.FindControl("lblRemnantDia");
                        Label Pad_ProcessLossWt = (Label)item.FindControl("lblPad_ProcessLossWt");
                        Label Dia_ProcessLossWt = (Label)item.FindControl("lblDia_ProcessLossWt");
                        Label Pad_ProcessLossWtPercentage = (Label)item.FindControl("lblPad_ProcessLossWtPercentage");
                        Label Dia_ProcessLossWtPercentage = (Label)item.FindControl("lblDia_ProcessLossWtPercentage");
                        Label ConsigmentNo = (Label)item.FindControl("lblRefrenceNo");
                        Label D_Date = (Label)item.FindControl("lblDate");
                        

                        Label PadCoreWeight = (Label)item.FindControl("lblPP_PadCoreWeight");
                        Label PadScrapWeight = (Label)item.FindControl("lblPP_PadScrapWeight");



                        Label DiaperCoreWeight = (Label)item.FindControl("lblPP_DiaperCoreWeight");
                        Label DiaperScrapWeight = (Label)item.FindControl("lblPP_DiaperScrapWeight");

                        Label PadOpenWeight = (Label)item.FindControl("lblPadOpeningWeight");
                        Label DiaOpenWeight = (Label)item.FindControl("lblDiaperOpeningWeight");
                        Label RowNo = (Label)item.FindControl("lblRowNumber");
                       

                        DataSet DsNew = objdb.ByProcedure("Sp_PostProcess", new string[] { "flag", "D_Date" },
                                        new string[] { "13", D_Date.Text }, "dataset");
                      if (DsNew!=null)
                      {
                          if (DsNew.Tables.Count > 0)
                          {
                              if (DsNew.Tables[0].Rows.Count > 0)
                              {
                                  PadOpenWeight.Text = DsNew.Tables[0].Rows[0]["OpeningPadBal"].ToString();
                                  DiaOpenWeight.Text = DsNew.Tables[0].Rows[0]["OpeningDiaBal"].ToString();
                                  RemnantPad.Text = DsNew.Tables[0].Rows[0]["ClosingPadBal"].ToString();
                                  RemnantDia.Text = DsNew.Tables[0].Rows[0]["ClosingDiaBal"].ToString();
                              }
                          }

                      }


                        
                      
                        if (ConsigmentNo.Text == "")
                        {
                            Pad_ProcessLossWtPercentage.Text = "0";
                            Dia_ProcessLossWtPercentage.Text = "0";
                        }
                        else
                        {

                            Pad_ProcessLossWt.Text = Convert.ToString(Math.Round((double.Parse(PadCoreWeight.Text) + double.Parse(PadScrapWeight.Text)) * (double.Parse(Pad_ProcessLossWtPercentage.Text) / 100), 2));
                            Dia_ProcessLossWt.Text = Convert.ToString(Math.Round((double.Parse(DiaperCoreWeight.Text) + double.Parse(DiaperScrapWeight.Text)) * (double.Parse(Dia_ProcessLossWtPercentage.Text) / 100), 2));
                        }

                        //Closing//

                        //RemnantPad.Text = Convert.ToString(Math.Round((double.Parse(PadOpenWeight.Text) + double.Parse(PadIssueWeight.Text)) - (double.Parse(PadCoreWeight.Text) + double.Parse(PadScrapWeight.Text) - double.Parse(Pad_ProcessLossWt.Text)), 2));
                        //RemnantDia.Text = Convert.ToString(Math.Round((double.Parse(DiaOpenWeight.Text) + double.Parse(DiaIssueWeight.Text)) - (double.Parse(DiaperCoreWeight.Text) + double.Parse(DiaperScrapWeight.Text) - double.Parse(Dia_ProcessLossWt.Text)), 2));



                       
                       

                        

                        TotalPadLossWt = TotalPadLossWt + double.Parse(Pad_ProcessLossWt.Text);
                        TotalDiaLossWt = TotalDiaLossWt + double.Parse(Dia_ProcessLossWt.Text);
                        TotalPadLossPercentage = TotalPadLossPercentage + double.Parse(Pad_ProcessLossWtPercentage.Text);
                        TotalDiaLossPercentage = TotalDiaLossPercentage + double.Parse(Dia_ProcessLossWtPercentage.Text);
                        TotalPadIssueWt = TotalPadIssueWt + double.Parse(PadIssueWeight.Text);
                        TotalDiaIssueWt = TotalDiaIssueWt + double.Parse(DiaIssueWeight.Text);
                        TotalPadClosing = TotalPadClosing + double.Parse(RemnantPad.Text);
                        TotalDiaClosing = TotalDiaClosing + double.Parse(RemnantDia.Text);
                        //k = k + 1;
                    }

                    double TotalPPDiaCoreWt = 0, TotalPPDiaScrapWt = 0, TotalPPPadCoreWt = 0, TotalPPPadScrapWt = 0, TotalSaleDiaCoreWt = 0, TotalSaleDiaScrapWt = 0, TotalSalePadCoreWt = 0, TotalSalePadScrapWt = 0;
                       
                       

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        TotalPPDiaCoreWt = TotalPPDiaCoreWt + double.Parse(ds.Tables[0].Rows[i]["PP_DiaperCoreWeight"].ToString());
                        TotalPPDiaScrapWt = TotalPPDiaScrapWt + double.Parse(ds.Tables[0].Rows[i]["PP_DiaperScrapWeight"].ToString());
                        TotalPPPadCoreWt = TotalPPPadCoreWt + double.Parse(ds.Tables[0].Rows[i]["PP_PadCoreWeight"].ToString());
                        TotalPPPadScrapWt = TotalPPPadScrapWt + double.Parse(ds.Tables[0].Rows[i]["PP_PadScrapWeight"].ToString());


                        TotalSaleDiaCoreWt = TotalSaleDiaCoreWt + double.Parse(ds.Tables[0].Rows[i]["Sale_DiaperCoreWeight"].ToString());
                        TotalSaleDiaScrapWt = TotalSaleDiaScrapWt + double.Parse(ds.Tables[0].Rows[i]["Sale_DiaperScrapWeight"].ToString());
                        TotalSalePadCoreWt = TotalSalePadCoreWt + double.Parse(ds.Tables[0].Rows[i]["Sale_PadCoreWeight"].ToString());
                        TotalSalePadScrapWt = TotalSalePadScrapWt + double.Parse(ds.Tables[0].Rows[i]["Sale_PadScrapWeight"].ToString());

                       
                    }

                    GridPostProcess.FooterRow.Cells[1].Font.Size = 15;
                    GridPostProcess.FooterRow.Cells[4].Font.Size = 15;
                    GridPostProcess.FooterRow.Cells[5].Font.Size = 15;
                    //GridPostProcess.FooterRow.Cells[6].Font.Size = 15;
                    GridPostProcess.FooterRow.Cells[7].Font.Size = 15;
                    GridPostProcess.FooterRow.Cells[8].Font.Size = 15;                  
                    GridPostProcess.FooterRow.Cells[9].Font.Size = 15;
                    GridPostProcess.FooterRow.Cells[10].Font.Size = 15;
                    GridPostProcess.FooterRow.Cells[11].Font.Size = 15;
                    GridPostProcess.FooterRow.Cells[12].Font.Size = 15;
                    GridPostProcess.FooterRow.Cells[13].Font.Size = 15;
                    GridPostProcess.FooterRow.Cells[14].Font.Size = 15;
                    GridPostProcess.FooterRow.Cells[15].Font.Size = 15;
                    GridPostProcess.FooterRow.Cells[16].Font.Size = 15;
                    GridPostProcess.FooterRow.Cells[17].Font.Size = 15;
                    GridPostProcess.FooterRow.Cells[18].Font.Size = 15;
                    GridPostProcess.FooterRow.Cells[19].Font.Size = 15;
                    GridPostProcess.FooterRow.Cells[20].Font.Size = 15;

                    GridPostProcess.FooterRow.Cells[1].Text = "TOTAL";
                    GridPostProcess.FooterRow.Cells[4].Text = TotalPadIssueWt.ToString();
                    GridPostProcess.FooterRow.Cells[5].Text = TotalDiaIssueWt.ToString();
                    GridPostProcess.FooterRow.Cells[7].Text = TotalPPDiaCoreWt.ToString();
                    GridPostProcess.FooterRow.Cells[8].Text = TotalPPDiaScrapWt.ToString();
                    GridPostProcess.FooterRow.Cells[9].Text = TotalPPPadCoreWt.ToString();
                    GridPostProcess.FooterRow.Cells[10].Text = TotalPPPadScrapWt.ToString();

                    GridPostProcess.FooterRow.Cells[11].Text = TotalSaleDiaCoreWt.ToString();
                    GridPostProcess.FooterRow.Cells[12].Text = TotalSaleDiaScrapWt.ToString();
                    GridPostProcess.FooterRow.Cells[13].Text = TotalSalePadCoreWt.ToString();
                    GridPostProcess.FooterRow.Cells[14].Text = TotalSalePadScrapWt.ToString();


                    GridPostProcess.FooterRow.Cells[15].Text = TotalPadLossWt.ToString();
                    GridPostProcess.FooterRow.Cells[16].Text = TotalDiaLossWt.ToString();
                    GridPostProcess.FooterRow.Cells[17].Text = TotalPadLossPercentage.ToString();
                    GridPostProcess.FooterRow.Cells[18].Text = TotalDiaLossPercentage.ToString();
                    GridPostProcess.FooterRow.Cells[19].Text = TotalPadClosing.ToString();
                    GridPostProcess.FooterRow.Cells[20].Text = TotalDiaClosing.ToString();
                }
                else
                {
                    //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "No Record Foud");
                }
            }
            else
            {
               // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "No Record Foud");
            }
        }
        else
        {
           // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "No Record Foud");
        }
    }

    protected void GridData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //for (int rowIndex = GridData.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        //{
        //    GridViewRow currentRow = GridData.Rows[rowIndex];
        //    GridViewRow previousRow = GridData.Rows[rowIndex + 1];
        //    string[] arra1 = currentRow.Cells[1].Text.Split(' ');
        //    string[] arra2 = previousRow.Cells[1].Text.Split(' ');
        //    currentRow.Cells[1].Text = arra1[0];
        //    previousRow.Cells[1].Text = arra2[0];
        //    if (currentRow.Cells[1].Text == previousRow.Cells[1].Text)
        //    {
        //        if (previousRow.Cells[1].RowSpan < 2)
        //        {
        //            currentRow.Cells[1].RowSpan = 2;
        //        }
        //        else
        //        {
        //            currentRow.Cells[1].RowSpan = previousRow.Cells[1].RowSpan + 1;
        //        }
        //        previousRow.Cells[1].Visible = false;
        //    }
        //}
    }
    
    protected void BtnShowPostProcess_Click(object sender, EventArgs e)
    {

    }
    protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlReportType.SelectedValue == "1")
        {
            dvReportData.Visible = true;
            dvPostReportData.Visible = false;

        }
        else if (ddlReportType.SelectedValue == "2")
        {

            dvReportData.Visible = false;
            dvPostReportData.Visible = true;
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (ddlReportType.SelectedValue == "1")
        { 
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=RECEIVE/ISSUE.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            GridData.AllowPaging = false;
            //this.BindGrid();

            GridData.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in GridData.HeaderRow.Cells)
            {
                cell.BackColor = GridData.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in GridData.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = GridData.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = GridData.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            GridData.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
        }
        else if (ddlReportType.SelectedValue == "2")
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=POST_PROCESSING_RECORD.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridPostProcess.AllowPaging = false;
                //this.BindGrid();

                GridPostProcess.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GridPostProcess.HeaderRow.Cells)
                {
                    cell.BackColor = GridPostProcess.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridPostProcess.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridPostProcess.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridPostProcess.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridPostProcess.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}