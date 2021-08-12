<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="shipping-checkout-page">
        <div class="row">
            <div class="col s12">
                <div class="section-title">
                    <p><span style="color: #101010">DASHBOARD </span>&nbsp;</p>
                </div>
            </div>
        </div>
        <div class="row mt-20">

            <asp:Panel runat="server" ID="AdminPanel" hidden>
                <div class="col s6 l3 col-produc">
                    <div class="li-pro">
                        <div class="box-product blue-box">
                            <div class="bp-top">
                                <h5 class="text-uppercase"><strong>DASHBOARD</strong></h5>
                                <%--  <div class="price bp-bottom">
                                <asp:Label runat="server" ID="LblTotDoctor" Text="0" Font-Size="XX-Large" ForeColor="White"></asp:Label>
                            </div>--%>
                            </div>
                            <div class="bp-bottom">
                                <a class="btn button-add-cart" href="FrmScrapLoad.aspx">PG</a>
                            </div>
                        </div>
                        <div class="bg-pro"></div>
                    </div>
                </div>

                <div class="col s6 l3 col-produc" hidden>
                    <div class="li-pro">
                        <div class="box-product ylw-box">
                            <div class="bp-top">
                                <h5 class="text-uppercase"><strong></strong></h5>
                                <%-- <div class="price bp-bottom">
                                <asp:Label runat="server" ID="LblTotRegPatient" Text="0" Font-Size="XX-Large" ForeColor="White"></asp:Label>
                            </div>--%>
                            </div>
                            <div class="bp-bottom">
                                <a class="btn button-add-cart" href="Frm_RptSalesPatient.aspx">CM</a>
                            </div>
                        </div>
                        <div class="bg-pro"></div>
                    </div>
                </div>
                <%-- <div class="col s6 l3 col-produc">
                <div class="li-pro">
                    <div class="box-product orng-box">
                        <div class="bp-top">
                            <h5 class="text-uppercase"><strong>Total Products</strong></h5>
                            <div class="price bp-bottom">
                                <asp:Label runat="server" ID="LblProducts" Text="0" Font-Size="XX-Large" ForeColor="White"></asp:Label>
                            </div>
                        </div>
                        <div class="bp-bottom">
                            <a class="btn button-add-cart" href="Frm_ProductMaster.aspx">View All</a>
                        </div>
                    </div>
                    <div class="bg-pro"></div>
                </div>
            </div>
            <div class="col s6 l3 col-produc">
                <div class="li-pro">
                    <div class="box-product orng-box">
                        <div class="bp-top">
                            <h5 class="text-uppercase"><strong>Total Email Clicks</strong></h5>
                            <div class="price bp-bottom">
                                <asp:Label runat="server" ID="LblPatientVisitonamazon" Text="0" Font-Size="XX-Large" ForeColor="White"></asp:Label>
                            </div>
                        </div>
                        <div class="bp-bottom">
                            <a class="btn button-add-cart" href="Frm_RptSalesPatient.aspx">View All</a>
                        </div>
                    </div>
                    <div class="bg-pro"></div>
                </div>
            </div>--%>
            </asp:Panel>
            <asp:Panel runat="server" ID="DoctorPanel" Visible="false">
                <div class="col s6 l3 col-produc">
                    <div class="li-pro">
                        <div class="box-product blue-box">
                            <div class="bp-top">
                                <h5 class="text-uppercase"><strong>Total Patient Visit</strong></h5>
                                <div class="price bp-bottom">
                                    <asp:Label runat="server" ID="LblDocTotPatientVisit" Text="0" Font-Size="XX-Large" ForeColor="White"></asp:Label>
                                </div>
                            </div>
                            <div class="bp-bottom">
                                <a class="btn button-add-cart" href="Frm_RptDoctorPatient.aspx">View All</a>
                            </div>
                        </div>
                        <div class="bg-pro"></div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>

