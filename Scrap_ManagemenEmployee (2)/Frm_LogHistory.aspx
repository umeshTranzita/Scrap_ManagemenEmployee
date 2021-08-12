<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Frm_LogHistory.aspx.cs" Inherits="Frm_LogHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="css/select2.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server" ID="script1"></asp:ScriptManager>
    <div class="shipping-checkout-page">
        <div class="row">
            <div class="col s12">
                <div class="section-title">
                    <p><span style="color: #101010">LOG </span>&nbsp;HISTORY REPORT</p>
                </div>
            </div>
        </div>
        <div class="row" style="padding-top: 10px;">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </div>
        <div class="row" style="padding-top: 10px;">
            <div class="input-field col s6 m12 l4">
                <div class="col s12 m12 l12">
                    <asp:DropDownList runat="server" ID="ddlRemark" AutoPostBack="true" class="form-control select2" Visible="True" OnSelectedIndexChanged="ddlRemark_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>
            <asp:Panel runat="server" ID="PanelCustom" Visible="false">
                <div class="input-field col s6 m12 l2">
                    <asp:TextBox ID="TxtFromdate" class="form-control datepicker" runat="server" MaxLength="100" AutoComplete="off"></asp:TextBox>
                    <label id="Fromdate" runat="server">FROM DATE<span style="color: red;">*</span></label>
                </div>
                <div class="input-field col s6 m12 l2">
                    <asp:TextBox ID="TxtTodate" class="form-control datepicker" runat="server" MaxLength="50" AutoPostBack="false" AutoComplete="off"></asp:TextBox>
                    <label id="Todate" runat="server" class="txt">TO DATE<span style="color: red;">*</span></label>
                </div>
                <div class="input-field col s6 m12 l2">
                    <asp:Button ID="btnSubmit" runat="server" class="btn theme-btn-rounded" Text="SEARCH" OnClick="btnSubmit_Click" OnClientClick="return validateform();" />
                </div>
            </asp:Panel>
            <div class="input-field col s6 m12 l1">
                <asp:LinkButton runat="server" class="btn theme-btn-rounded" Text="<i class='fa fa-download'></i>&nbsp;&nbsp;Export" ID="btnExport" OnClick="btnExport_Click"></asp:LinkButton>
            </div>
        </div>
        <div class="mt-20">
            <div class="table-responsive">
                <asp:GridView ID="GridLog" runat="server" class="table table-striped table-bordered" AutoGenerateColumns="False"
                    AllowPaging="True" PageSize="250" DataKeyNames="LogId" OnPageIndexChanging="GridLog_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%#Bind("LogId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="ActionBy" HeaderText="Action By" />
                        <asp:BoundField DataField="OnDate" HeaderText="OnDate(mm/dd/yyy)" />
                        <asp:BoundField DataField="LogData" HeaderText="LogData" />
                        <asp:BoundField DataField="PageName" HeaderText="PageName" />
                        <asp:BoundField DataField="Remark" HeaderText="Remark" />
                    </Columns>
                    <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                </asp:GridView>

            </div>
        </div>

    </div>

</asp:Content>

