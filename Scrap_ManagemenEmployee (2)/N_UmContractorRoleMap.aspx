<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="N_UmContractorRoleMap.aspx.cs" Inherits="N_UmContractorRoleMap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Contractor Role Mapping</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                 <%--<label id="BagBatchNo" runat="server" style="padding-bottom:10px;">BAG BATCH NO<span style="color: red;">*</span></label>--%>
                                <asp:DropDownList runat="server" ID="ddlSite" AutoPostBack="True" class="form-control select2" Visible="True" OnSelectedIndexChanged="ddlSite_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                 <%--<label id="BagBatchNo" runat="server" style="padding-bottom:10px;">BAG BATCH NO<span style="color: red;">*</span></label>--%>
                                <asp:DropDownList ID="ddlEmployye_Name" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployye_Name_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div id="divGrid" runat="server">
                        <asp:GridView ID="GridView1" PageSize="50" runat="server" class="table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Role_ID").ToString()%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Role_Name" HeaderText="Roles" />
                                <asp:BoundField DataField="Role_ID" HeaderText="Role_ID" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                <asp:TemplateField ItemStyle-Width="30" HeaderText="Exist Roles">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("Role_ID").ToString()%>' Checked='<%# Eval("ExistID").ToString() != "" ? true : false %>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="30px"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success btn-block" OnClick="btnSave_Click" />
                                </div>
                            </div>
                        </div>

                    </div>



                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:GridView ID="GVAssignRoll" PageSize="50" runat="server" class="table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Login_ID").ToString()%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Width="30" HeaderText="EMAIL ID">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" Text='<%# Eval("Email_ID") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Width="30" HeaderText="Assigned Roll">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" Text='<%# Eval("Roll") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px"></ItemStyle>
                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>





                </div>
            </div>
        </section>
    </div>
       <style>
        [type="checkbox"]:not(:checked), [type="checkbox"]:checked {
            position: absolute;
            opacity: 9;
            pointer-events: all;
        }
    </style>
</asp:Content>
