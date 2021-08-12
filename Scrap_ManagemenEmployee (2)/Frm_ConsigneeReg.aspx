<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Frm_ConsigneeReg.aspx.cs" Inherits="Frm_ConsigneeReg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row mt-20">

        <div class="shipping-checkout-page">
            <div class="row">
                &nbsp;<div class="col s12">
                    <div class="section-title">
                        <p><span style="color: #101010">Consignee</span> Master</p>
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
                     <div class="input-field col s6 m12 l6 ">
                        <asp:TextBox ID="TxtContactNo" class="validate" runat="server" AutoComplete="off"></asp:TextBox>
                        <label id="Label1" runat="server">Enter Consignee ContactNo</label>
                    </div>
                     <div class="input-field col s6 m12 l6">
                        <div class="col s12 m12 l6">
                            <asp:DropDownList runat="server" ID="ddlScrapCategory" AutoPostBack="false" class="form-control select2">
                              
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col s6 m12 l6 ">
                        <input type="checkbox" class="filled-in" id="chkIsActive" checked="checked" clientidmode="Static" runat="server" />
                        <%--<asp:CheckBox ID="chkIsActive" CssClass="filled-in" runat="server" Checked="true" />--%>
                        <label class="label-checkbox" for="chkIsActive">Active Status</label>
                    </div>
                </div>
                <div class="row">
                    <div class="input-field col s12 m12 l12">
                        <asp:Button ID="btnSubmit" runat="server" class="btn btn-sm" Text="SUBMIT" OnClick="btnSubmit_Click"  />
                        <asp:Button ID="btnCancel" runat="server" class="btn btn-cancel btn-sm" Text="CANCEL" />
                    </div>
                </div>
            </div>
            <div class="mt-20">
                <div class="table-responsive">
                    <asp:GridView ID="GridConsignee" runat="server" class="table table-bordered table-striped" AutoGenerateColumns="False"
                        AllowPaging="True" DataKeyNames="ConsigneeId" PageSize="100" OnSelectedIndexChanged="GridConsignee_SelectedIndexChanged" >
                        <Columns>
                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ConsigneeName" HeaderText="Consignee Name" />
                            <asp:BoundField DataField="ConsigneeAddress" HeaderText="Consignee Address" />
                            <asp:BoundField DataField="ConsigneeGst" HeaderText="Consignee Gst" />
                             <asp:BoundField DataField="ScrapCategory" HeaderText="Category" />
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

                if (document.getElementById('<%=TxtConsignee.ClientID%>').value.trim() == "") {
                    msg += "Enter Site Name. \n";
                }
                if (document.getElementById('<%=TxtConsigneeAddress.ClientID%>').value.trim() == "") {
                    msg += "Enter Site Description. \n";
                }
                if (document.getElementById('<%=TxtConsigneeGstNo.ClientID%>').value.trim() == "") {
                    msg += "Enter Site Address. \n";
                }
                if (document.getElementById('<%=TxtContactNo.ClientID%>').value.trim() == "") {
                    msg += "Enter Contact No. \n";
                }
                if (document.getElementById('<%=ddlScrapCategory.ClientID%>').selectedIndex == 0) {
                    msg += "Please Select Scrap Category.\n";
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


