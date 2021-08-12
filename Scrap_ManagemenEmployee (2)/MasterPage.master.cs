using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    DataSet ds;
    AbstApiDB objdb = new common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //try
            //{
                
            //    if (Session["UserType"].ToString() == "CONTRACTOR")
            //    {
            //        if (Session["UserCode"] == "" || Session["UserCode"] == null)
            //        {
            //            Response.Redirect("~/Login.aspx");

            //        }
            //        ViewState["Name"] = Session["UserName"].ToString();
            //        ViewState["VendorName"] = Session["VendorName"].ToString();
            //        ViewState["SiteName"] = Session["SiteName"].ToString();
            //        ViewState["Role"] = Session["Role"].ToString();
            //        if (ViewState["Role"].ToString() == "ScrapLoad")
            //        {
            //            //ContractorLoginLoad.Visible = true;
            //        }
            //        else if (ViewState["Role"].ToString() == "ScrapDispatch")
            //        {
            //           // ContractorLoginDispatch.Visible= true;

            //        }
            //        LblName.Text = ViewState["Name"].ToString();
            //        LblVendorName.Text = ViewState["VendorName"].ToString();
            //        LblSiteName.Text = ViewState["SiteName"].ToString();

            //        ProfileImg.ImageUrl = Session["ImagePath"].ToString();
            //    }
            //    if (Session["UserType"].ToString() == "EMPLOYEE")
            //    {
            //        //EmployeeLogin.Visible = true;
            //        //ContractorLoginDispatch.Visible = false;
            //    }



            //}
            //catch (Exception ex)
            //{
            //    Response.Redirect("~/Login.aspx");
            //}

            try
            {
                if (Session["Name"] == "" || Session["SiteName"] == null)
                {
                    Response.Redirect("~/Login.aspx");

                }
                     ViewState["Name"] = Session["UserName"].ToString();
                     //ViewState["VendorName"] = Session["VendorName"].ToString();
                      ViewState["SiteName"] = Session["SiteName"].ToString();
                if (Request.QueryString["IsMainPage"] != null)
                {
                    Session["Module_Id"] = null;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myModal()", true);
                }


                
                ViewState["Emp_ID"] = Session["Emp_Id"].ToString();

                spnUsername.InnerHtml = Session["UserName"].ToString();



                //ds = objdb.ByProcedure("SpEmployeeRoleMap",
                //      new string[] { "flag", "Emp_ID" },
                //      new string[] { "7", ViewState["Emp_ID"].ToString() }, "dataset");
                //if (ds.Tables[0].Rows.Count > 0)
                //{

                //}
                LblName.Text = ViewState["Name"].ToString();
                //LblVendorName.Text = ViewState["VendorName"].ToString();
                LblSiteName.Text = ViewState["SiteName"].ToString();
                Navigation.InnerHtml = "<ul class='sidebar-menu' data-widget='tree'>";
                Navigation.InnerHtml += "<li class='header' style='text-align: center;'>" + @DateTime.Now.ToString("D") + "</li>";
                Navigation.InnerHtml += "<li><hr style='margin: 0' /></li>";
                Navigation.InnerHtml += "<li><a href='Dashboard.aspx?IsMainPage=1'><i class='fa fa-home'></i><span>&nbsp;Main Page</span></a></li>";

                if (Request.QueryString["Module_ID"] != null)
                {
                    Session["Module_Id"] = Request.QueryString["Module_ID"].ToString();
                }

                if (Session["Module_Id"] == null)
                {
                    ds = objdb.ByProcedure("SpUMHome",
                       new string[] { "flag", "Emp_ID","Type","SiteId" },
                       new string[] { "1", ViewState["Emp_ID"].ToString(), Session["Type"].ToString(), Session["SiteId"].ToString() }, "dataset");
                              Session["Role_ID"] = ds.Tables[0].Rows[0]["Role_ID"].ToString();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i]["Module_ID"].ToString() == "2")
                        {
                            Navigation.InnerHtml += "<li><a href='Dashboard.aspx?Module_Id=" + ds.Tables[0].Rows[i]["Module_ID"].ToString() + "'><i class='fa fa-angle-double-right'></i>&nbsp;" + "<span>" + ds.Tables[0].Rows[i]["Module_Name"].ToString() + "</span></a></li>";
                        }
                        else if (ds.Tables[0].Rows[i]["Module_ID"].ToString() == "3")
                        {
                            Navigation.InnerHtml += "<li><a href='Dashboard.aspx?Module_Id=" + ds.Tables[0].Rows[i]["Module_ID"].ToString() + "'><i class='fa fa-angle-double-right'></i>&nbsp;" + "<span>" + ds.Tables[0].Rows[i]["Module_Name"].ToString() + "</span></a></li>";
                        }
                        else
                        {
                            Navigation.InnerHtml += "<li><a href='Dashboard.aspx?Module_Id=" + ds.Tables[0].Rows[i]["Module_ID"].ToString() + "'><i class='fa fa-angle-double-right'></i>&nbsp;" + "<span>" + ds.Tables[0].Rows[i]["Module_Name"].ToString() + "</span></a></li>";
                        }
                    }
                }
                else
                {
                    ds = objdb.ByProcedure("SpUMHome",
                             new string[] { "flag", "Emp_ID", "Module_ID", "Type", "SiteId" },
                             new string[] { "2", ViewState["Emp_ID"].ToString(), Session["Module_Id"].ToString(), Session["Type"].ToString(), Session["SiteId"].ToString() }, "dataset");

                    string Menu_Name = "";
                    string NavigationLi = "";
                    int IsMainPage = 0;
					int j = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        NavigationLi = "";
                        IsMainPage = 0;
                        Menu_Name = ds.Tables[0].Rows[i]["Menu_Name"].ToString();

                        while (ds.Tables[0].Rows[i]["Menu_Name"].ToString() == Menu_Name)
                        {
                            IsMainPage++;
                            NavigationLi += "<li><a href='" + ds.Tables[0].Rows[i]["Form_Path"].ToString() + "'><i class='fa fa-angle-right'></i>" + "<span>" + ds.Tables[0].Rows[i]["Form_Name"].ToString() + "</span>" + "</a></li>";
                            i++;
                            if (ds.Tables[0].Rows.Count == i)
                            {
                                break;
                            }
                        }
                        i--;
                        if (IsMainPage == 1)
                        {
                            Navigation.InnerHtml += NavigationLi;
                        }
                        else
                        {
                            // Navigation.InnerHtml += "<li class='treeview'>";
                            // Navigation.InnerHtml += "<a href='#'>" + ds.Tables[0].Rows[i]["Menu_Icon"].ToString() + "<span>" + ds.Tables[0].Rows[i]["Menu_Name"].ToString() + "</span><span class='pull-right-container'><i class='fa fa-angle-left pull-right'></i></span></a>";
                            // Navigation.InnerHtml += "<ul class='treeview-menu' style='display: none;'>";
                            // Navigation.InnerHtml += NavigationLi;
                            // Navigation.InnerHtml += "</ul>";
                            // Navigation.InnerHtml += "</li>";
							
							  Navigation.InnerHtml += "<li class='treeview'>";
                            Navigation.InnerHtml += "<a href='#' data-toggle='collapse' data-target='#submenu-" + j + "'>" + ds.Tables[0].Rows[i]["Menu_Icon"].ToString() + "<span>" + ds.Tables[0].Rows[i]["Menu_Name"].ToString() + "</span><span class='pull-right-container'><i class='fa fa-fw fa-angle-down pull-right'></i></span></a>";
                            Navigation.InnerHtml += "<ul class='collapse' id='submenu-" + j + "'>";
                            Navigation.InnerHtml += NavigationLi;
                            Navigation.InnerHtml += "</ul>";
                            Navigation.InnerHtml += "</li>";
							j = j + 1;
							
                        }
                    }
                }

               Navigation.InnerHtml += "<li><a href='http://mdpdigital.ap.pg.com:8070/Login.aspx'><i class='fa fa-power-off'></i><span>Logout</span></a></li>";
                Navigation.InnerHtml += "</ul>";
            }
            catch (Exception)
            {

                throw;
            }

        }

        }
}
