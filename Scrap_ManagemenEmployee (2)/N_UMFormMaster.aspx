<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="N_UMFormMaster.aspx.cs" Inherits="N_UMFormMaster" %>

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
                            <h3 class="box-title">Form Master</h3>
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
                                      
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtForm_Name" onkeypress="javascript:tbx_fnAlphaOnly(event, this);" placeholder="Enter Form Name" autocomplete="off" MaxLength="50"></asp:TextBox>
                                    </div>
                                         </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                       
                                        <asp:TextBox runat="server" CssClass="form-control Number" ID="txtOrderBy" onkeypress="javascript:tbx_Number(event, this);" placeholder="Enter Order By" autocomplete="off" MaxLength="3"></asp:TextBox>
                                    </div>
                                         </div>
                                </div>
                                 <div class="row">
                                    <div class="col-md-8">
                                    <div class="form-group">
                                       
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtForm_Path" placeholder="Enter Form Path" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                 </div>
                                 
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-primary" ID="btnSave" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateform()" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <a href="UMFormMaster.aspx" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblRecord" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    <asp:GridView ID="GridView1" PageSize="50" runat="server" class="table table-hover table-bordered table-striped pagination-ys " ShowHeaderWhenEmpty="true" 
                                                  AutoGenerateColumns="False" DataKeyNames="Form_ID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Form_ID").ToString()%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Module_Name" HeaderText="Module" />
                                            <asp:BoundField DataField="Form_Name" HeaderText="Form" />
                                            <asp:BoundField DataField="Form_Path" HeaderText="Form Path" />
                                             <asp:BoundField DataField="OrderBy" HeaderText="Order By" />
                                            <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Select" runat="server" CssClass="label label-default" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30" HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" OnCheckedChanged="chkSelect_CheckedChanged" runat="server" ToolTip='<%# Eval("Form_ID").ToString()%>' Checked='<%# Eval("Form_IsActive").ToString()=="1" ? true : false %>' AutoPostBack="true" />
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
        function tbx_fnAlphaOnly(e, cntrl) {
            if (!e) e = window.event; if (e.charCode) {
                if (e.charCode < 65 || (e.charCode > 90 && e.charCode < 97) || e.charCode > 122)
                { if (e.charCode != 95 && e.charCode != 32) { if (e.preventDefault) { e.preventDefault(); } } }
            } else if (e.keyCode) {
                if (e.keyCode < 65 || (e.keyCode > 90 && e.keyCode < 97) || e.keyCode > 122)
                { if (e.keyCode != 95 && e.keyCode != 32) { try { e.keyCode = 0; } catch (e) { } } }
            }
        }
        function validateform() {
            debugger
            var msg = "";
            if (document.getElementById("ddlModule_Name").selectedIndex == 0) {
                msg += "Select Module Name\n";
            }
            if (document.getElementById('<%=txtForm_Name.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Form Name. \n";
            }
            if (document.getElementById('<%=txtForm_Path.ClientID%>').value.trim() == "") {
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

