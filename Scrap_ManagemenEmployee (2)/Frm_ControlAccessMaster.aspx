<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Frm_ControlAccessMaster.aspx.cs" Inherits="Frm_ControlAccessMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row mt-20">

        <div class="shipping-checkout-page">
            <div class="row">
                <div class="col s12">
                    <div class="section-title">
                        <p><span style="color: #101010">CONTROL ACCESS</span> MASTER</p>
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
                        <div class="col s12 m12 l6">
                            <asp:DropDownList runat="server" ID="ddlSite" AutoPostBack="true" class="form-control select2" OnSelectedIndexChanged="ddlSite_SelectedIndexChanged"></asp:DropDownList>
                            <%--<label>Select Category<span style="color: red;">*</span></label>--%>
                        </div>
                    </div>
                </div>
            </div>
            <div class="mt-20">
                <div class="table-responsive">
                    <asp:GridView ID="GridViewAccess" runat="server" class="table table-bordered table-striped" AutoGenerateColumns="False"
                        AllowPaging="True" DataKeyNames="DepartmentID" PageSize="100">
                        <Columns>
                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%#Bind("DepartmentID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SiteName" HeaderText="Site Name" />
                            <asp:BoundField DataField="AccessType" HeaderText="Control Name" />
                            <asp:TemplateField HeaderText="Allow / Not Allow" ShowHeader="False" ItemStyle-Width="60">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAllow" runat="server" />
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
            <%--function validateform() {
                var msg = "";
                if (document.getElementById('<%=ddlSite.ClientID%>').selectedIndex == 0) {
                    msg += "Please Select Site.\n";
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
            }--%>
        </script>
    </div>
</asp:Content>

