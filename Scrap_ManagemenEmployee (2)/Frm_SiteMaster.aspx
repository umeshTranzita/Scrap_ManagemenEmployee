<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Frm_SiteMaster.aspx.cs" Inherits="Frm_SiteMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row mt-20">

        <div class="shipping-checkout-page">
            <div class="row">
                &nbsp;<div class="col s12">
                    <div class="section-title">
                        <p><span style="color: #101010">SITE</span> MASTER</p>
                    </div>
                </div>
            </div>
            <div class="billing-detail-wrap ck-box mt-20">

                <div class="row">
                    <div class="col s12">
                        <%-- <div class="billing-deatail-form-text"><i class="far fa-id-card"></i>Fill in the form your Product detail :</div>--%>
                    </div>
                </div>
                <div class="row">
                    <div class="col s12">
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblUserID" runat="server" class="form-control" Visible="false"></asp:Label>
                        <asp:Label ID="lblRoleID" runat="server" class="form-control" Visible="false"></asp:Label>
                        <asp:Label ID="lblEmail_ID" runat="server" class="form-control" Visible="false"></asp:Label>
                    </div>
                </div>


                <div class="row" style="padding-top: 20px;">
                    <div class="input-field col s6 m12 l6">
                        <asp:TextBox ID="txtSiteName" class="validate" runat="server" MaxLength="50" AutoComplete="off"></asp:TextBox>
                        <label id="SiteName" runat="server">Enter Site Name<span style="color: red;">*</span></label>
                    </div>
                    <div class="input-field col s6 m12 l6 ">
                        <asp:TextBox ID="txtSiteDescription" class="validate" runat="server" AutoComplete="off"></asp:TextBox>
                        <label id="SiteDescription" runat="server">Enter Site Description<span style="color: red;">*</span></label>
                    </div>
                    <div class="input-field col s6 m12 l6 ">
                        <asp:TextBox ID="TxtSiteAddress" class="validate" runat="server" AutoComplete="off"></asp:TextBox>
                        <label id="SiteAddress" runat="server">Enter Site Address<span style="color: red;">*</span></label>
                    </div>
                    <div class="input-field col s6 m12 l6 ">
                        <asp:TextBox ID="TxtGstNo" class="validate" runat="server" AutoComplete="off"></asp:TextBox>
                        <label id="GstNo" runat="server">Enter GST No<span style="color: red;">*</span></label>
                    </div>
                    <div class="input-field col s6 m12 l6 ">
                        <asp:TextBox ID="txtSiteCode" class="validate" runat="server" AutoComplete="off"></asp:TextBox>
                        <label id="SiteCode" runat="server">Enter Site Code<span style="color: red;">*</span></label>
                    </div>
                    <div class="input-field col s6 m12 l6 ">
                        <asp:TextBox ID="TxtConsignee" class="validate" runat="server" AutoComplete="off"></asp:TextBox>
                        <label id="ConsigneeName" runat="server">Enter Consignee Name</label>
                    </div>
                    <div class="input-field col s6 m12 l6 ">
                        <asp:TextBox ID="TxtConsigneeAddress" class="validate" runat="server" AutoComplete="off"></asp:TextBox>
                        <label id="ConsigneeAddress" runat="server">Enter Consignee Address</label>
                    </div>
                    <div class="input-field col s6 m12 l6 ">
                        <asp:TextBox ID="TxtConsigneeGstNo" class="validate" runat="server" AutoComplete="off"></asp:TextBox>
                        <label id="ConsigneeGstNo" runat="server">Enter Consignee GstNo</label>
                    </div>
                    <div class="col s6 m12 l6 ">
                        <input type="checkbox" class="filled-in" id="chkIsActive" checked="checked" clientidmode="Static" runat="server" />
                        <%--<asp:CheckBox ID="chkIsActive" CssClass="filled-in" runat="server" Checked="true" />--%>
                        <label class="label-checkbox" for="chkIsActive">Active Status</label>
                    </div>
                </div>



                <div class="row">
                    <%-- OnClientClick="return validateform();"--%>
                    <div class="input-field col s12 m12 l12">
                        <asp:Button ID="btnSubmit" runat="server" class="btn btn-sm" Text="SUBMIT" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnCancel" runat="server" class="btn btn-cancel btn-sm" Text="CANCEL" />
                    </div>
                </div>
            </div>
            <div class="mt-20">
                <div class="table-responsive">

                    <asp:GridView ID="GridSite" runat="server" class="table table-bordered table-striped" AutoGenerateColumns="False"
                        AllowPaging="True" DataKeyNames="SiteID" PageSize="100" OnSelectedIndexChanged="GridSite_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%#Bind("SiteID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="SiteName" HeaderText="Site Name" />
                            <asp:BoundField DataField="SiteDescription" HeaderText="Site Description" />
                            <asp:BoundField DataField="SiteAddress" HeaderText="Site Address" />
                            <asp:BoundField DataField="SiteCode" HeaderText="Site Code" />
                            <asp:BoundField DataField="SiteGstNo" HeaderText="Site GstNo" />
                            <asp:BoundField DataField="ConsigneeName" HeaderText="Consignee Name" />
                            <asp:BoundField DataField="ConsigneeAddress" HeaderText="Consignee Address" />
                            <asp:BoundField DataField="ConsigneeGst" HeaderText="Consignee Gst" />

                            <asp:BoundField DataField="IsActive" HeaderText="IsActive" />

                            <asp:TemplateField HeaderText="Edit" ShowHeader="False" ItemStyle-Width="60">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="EDIT" CssClass="btn btn-block btn-info btn-xs"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>
            </div>



        </div>



        <script type="text/javascript">


            function validateNum(evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;

            }

            function validateform() {
                var msg = "";

                if (document.getElementById('<%=txtSiteName.ClientID%>').value.trim() == "") {
                    msg += "Enter Site Name. \n";
                }
                if (document.getElementById('<%=txtSiteDescription.ClientID%>').value.trim() == "") {
                    msg += "Enter Site Description. \n";
                }
                if (document.getElementById('<%=TxtSiteAddress.ClientID%>').value.trim() == "") {
                    msg += "Enter Site Address. \n";
                }
                if (document.getElementById('<%=txtSiteCode.ClientID%>').value.trim() == "") {
                    msg += "Enter Site Code. \n";
                }
                if (document.getElementById('<%=TxtGstNo.ClientID%>').value.trim() == "") {
                    msg += "Enter Gst No. \n";
                }


                if (msg != "") {
                    alert(msg);
                    return false;
                }
                else {
                    if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "SUBMIT") {
                        if (confirm("Do you really want to Submit Details ?")) {
                            return true;
                        }
                        else {
                            return false;
                        }
                    }
                    if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "MODIFY") {
                        if (confirm("Do you really want to Modify Details ?")) {
                            return true;
                        }
                        else {
                            return false;
                        }
                    }
                }
            }
        </script>




    </div>
</asp:Content>


