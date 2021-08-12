<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="N_UMMenuMaster.aspx.cs" Inherits="N_UMMenuMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-Manish">
                        <div class="box-header">
                            <h3 class="box-title">Menu Master</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                     
                                        <asp:DropDownList ID="ddlModule_Name" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlModule_Name_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                   
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtMenu_Name" onkeypress="javascript:tbx_fnAlphaOnly(event, this);" placeholder="Enter Menu Name" autocomplete="off" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                       
                                        <asp:TextBox runat="server" CssClass="form-control Number" ID="txtOrderBy" onkeypress="javascript:tbx_Number(event, this);" placeholder="Enter Order By" autocomplete="off" MaxLength="3"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                      
                                        <div class="input-group">
                                            <asp:TextBox ID="txtMenu_Icon" runat="server" data-placement="bottomRight" class="form-control icp icp-auto" MaxLength="100" value="fa fa-angle-double-right"></asp:TextBox>
                                            <span class="input-group-addon action-create"></span>
                                        </div>

                                    </div>
                                </div>
                                <%--<div class="col-md-4">
                                    <div class="form-group">
                                        <label>Icon<span style="color: red;"> *</span></label>
                                        <%--<asp:Label ID="IconDisplay" runat="server">
                                                <i id="IconDisplay1" runat="server"></i>
                                            
                                        </asp:Label>
                                        <asp:Label runat="server" ID="IconDisplay" placeholder="Enter Menu Icon" autocomplete="off"></asp:Label>
                                    </div>
                                </div>--%>
                            </div>

                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-primary" ID="btnSave" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateform()" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <a href="UMMenuMaster.aspx" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblRecord" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    <asp:GridView ID="GridView1" PageSize="50" runat="server" class="table table-hover table-bordered table-striped pagination-ys " ShowHeaderWhenEmpty="true"
                                        AutoGenerateColumns="False" DataKeyNames="Menu_ID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Menu_ID").ToString()%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Module_Name" HeaderText="Module" />
                                            <asp:BoundField DataField="Menu_Name" HeaderText="Menu Name" />
                                            <asp:BoundField DataField="OrderBy" HeaderText="Order By" />
                                            <asp:TemplateField HeaderText="Menu Icon">
                                                <ItemTemplate>
                                                    <asp:Label ID="IconDisplay" runat="server" Text='<%# Eval("Menu_Icon").ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                                                <ItemTemplate>

                                                    <asp:LinkButton ID="Select" runat="server" CssClass="label label-default" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30" HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" OnCheckedChanged="chkSelect_CheckedChanged" runat="server" ToolTip='<%# Eval("Menu_ID").ToString()%>' Checked='<%# Eval("Menu_IsActive").ToString()=="1" ? true : false %>' AutoPostBack="true" />
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
     <script type="text/javascript">
         $(function () {

             $.iconpicker.batch('.icp.iconpicker-element', 'destroy');

             $(document).on('click', '.action-placement', function (e) {
                 $('.action-placement').removeClass('active');
                 $(this).addClass('active');
                 $('.icp-opts').data('iconpicker').updatePlacement($(this).text());
                 e.preventDefault();
                 return false;
             });
             $('.action-create').on('click', function () {
                 $('.icp-auto').iconpicker();

                 $('.icp-dd').iconpicker({
                     //title: 'Dropdown with picker',
                     //component:'.btn > i'
                 });

             }).trigger('click');
         });
    </script>
    <script type="text/javascript">
        function validateform() {
            debugger
            var msg = "";
            if (document.getElementById("ddlModule_Name").selectedIndex == 0) {
                msg += "Select Module Name\n";
            }
            if (document.getElementById('<%=txtMenu_Name.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Form Name. \n";
            }
            if (document.getElementById('<%=txtMenu_Icon.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Form Path. \n";
            }
            if (document.getElementById('<%=txtOrderBy.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Order By. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    if (confirm("Do you really want to Save Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Edit") {
                    if (confirm("Do you really want to Edit Details ?")) {
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

