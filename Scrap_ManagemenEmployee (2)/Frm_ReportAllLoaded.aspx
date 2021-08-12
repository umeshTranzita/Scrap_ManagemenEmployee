<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Frm_ReportAllLoaded.aspx.cs" Inherits="Frm_ReportAllLoaded" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server" ID="script1"></asp:ScriptManager>
    <div class="shipping-checkout-page">
        <div class="row">
            <div class="col s12">
                <div class="section-title">
                    <p><span style="color: #101010">All </span>&nbsp;SCRAP LOAD REPORT</p>
                </div>
            </div>
        </div>
        <div class="row" style="padding-top: 10px;">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </div>
        <div class="row" style="padding-top: 10px;">
            <div class="input-field col s6 m12 l3">
                <div class="col s12 m12 l12">
                    <asp:DropDownList runat="server" ID="ddlFilter" AutoPostBack="true" class="form-control select2" Visible="True" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>
            <div class="input-field col s6 m12 l3">
                <asp:TextBox ID="TxtFromdate" class="form-control datepicker" runat="server" MaxLength="100" AutoComplete="off"></asp:TextBox>
                <label id="Fromdate" runat="server">FROM DATE<span style="color: red;">*</span></label>
            </div>
            <div class="input-field col s6 m12 l3">
                <asp:TextBox ID="TxtTodate" class="form-control datepicker" runat="server" MaxLength="50" AutoPostBack="false" AutoComplete="off"></asp:TextBox>
                <label id="Todate" runat="server" class="txt">TO DATE<span style="color: red;">*</span></label>
            </div>
            <div class="input-field col s6 m12 l3 ">
                <asp:Button ID="btnSubmit" runat="server" class="btn theme-btn-rounded" Text="SEARCH" OnClick="btnSubmit_Click" OnClientClick="return validateform();" />
            </div>
        </div>

        <div class="mt-20">
            <div class="table-responsive">
                <asp:GridView ID="GridLoad" runat="server" class="table table-striped table-bordered" AutoGenerateColumns="False"
                    AllowPaging="True" DataKeyNames="Scrap_Id" PageSize="100">
                    <Columns>
                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%#Bind("Scrap_Id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="ConsigmentNo" HeaderText="Consigment No" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:BoundField DataField="CreateDate" HeaderText="Load On" />
                        <asp:BoundField DataField="DispatchedDate" HeaderText="Dispatched On" />
                        <asp:BoundField DataField="ReceiveDate" HeaderText="Received On" />
                        <asp:BoundField DataField="IssueDate" HeaderText="Issued On" />
                        <asp:BoundField DataField="PostProcessingDate" HeaderText="Post Proceed On" />




                    </Columns>
                    <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                </asp:GridView>

            </div>
        </div>

    </div>
    <script>
        function validateform() {
            var msg = "";

            if (document.getElementById('<%=TxtFromdate.ClientID%>').value.trim() == "") {
                msg += "Enter From Date. \n";
            }
            if (document.getElementById('<%=TxtTodate.ClientID%>').value.trim() == "") {
                msg += "Enter To Date. \n";
            }
            <%--if (document.getElementById('<%=TxtTodate.ClientID%>').value.trim() < document.getElementById('<%=TxtFromdate.ClientID%>').value.trim()) {
                msg += "To Date Never be lesser than from date . \n";
            }--%>


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
</asp:Content>

