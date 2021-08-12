<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="N_UMRoleFormMap.aspx.aspx.cs" Inherits="N_UMRoleFormMap_aspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <div class="box box-Manish">
                <div class="box-header">
                    <h3 class="box-title">Role Form Mapping</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                
                                <asp:DropDownList ID="ddlModule_Name" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlModule_Name_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                              
                                <asp:DropDownList ID="ddlRole_Name" runat="server" CssClass="form-control select2" ClientIDMode="Static" OnSelectedIndexChanged="ddlRole_Name_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div id="divGrid" runat="server">
                        <asp:GridView ID="GridView1" DataKeyNames = "Form_ID" runat="server" class="table table-hover table-bordered" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Form_ID").ToString()%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <%-- Checked='<%# Eval("Role_ID").ToString() != "" ? true : false %>'--%>
                                <asp:BoundField DataField="Form_Name" HeaderText="Form Name" />
                                <asp:BoundField DataField="Form_Path" HeaderText="Form Path" />
                               <%-- <asp:BoundField DataField="Form_ID" HeaderText="Form_ID" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />--%>
                                <asp:TemplateField ItemStyle-Width="30" HeaderText="Exist Roles">
                                    <ItemTemplate>
                                         <asp:HiddenField ID="hdnForm_ID" runat="server" ClientIDMode="Static" Value='<%# Eval("Form_ID").ToString()%>' />
                                        <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("Form_ID").ToString()%>'  />
                                    </ItemTemplate>
                                    <ItemStyle Width="30px"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary btn-block" OnClick="btnSave_Click" OnClientClick="return validateform();" />
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
     <script>
         function validateform() {
             var msg = "";
             var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

             if (document.getElementById('<%=ddlRole_Name.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Role Name. \n";
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
            }
        }
    </script>
</asp:Content>

