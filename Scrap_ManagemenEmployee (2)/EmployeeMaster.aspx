<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmployeeMaster.aspx.cs" Inherits="EmployeeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-4">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Employee Master</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">

                                <div class="col-md-12"> 
                                    <div class="form-group">
                                       
                                        <asp:DropDownList ID="ddlSite" CssClass="form-control select2" DataValueField="SiteId" DataTextField="Email" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlSite_SelectedIndexChanged">
                                        </asp:DropDownList>


                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="form-group">
                                        
                                        <asp:TextBox runat="server"  CssClass="form-control" ID="txtempname" placeholder="Enter Name" autocomplete="off" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                                  <div class="col-md-12">
                                    <div class="form-group">
                                        
                                        <asp:TextBox runat="server"  CssClass="form-control" ID="TxtEmail" placeholder="Enter Email" autocomplete="off" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="form-group">
                                      
                                        <asp:TextBox runat="server"  CssClass="form-control" ID="txtTnumber" placeholder="Enter Tnumber" autocomplete="off" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                                 

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Status<span style="color: red;"> *</span></label>
                                        <asp:CheckBox ID="EmpStatus"  Checked="true" CssClass="form-control" runat="server" />
                                    </div>
                                </div>

                            </div>

                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-success" ID="btnSave" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateform()" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <a href="EmployeeMaster.aspx" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-8">
                    <div class="box box-success">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="GridView1" PageSize="50" runat="server" class="table table-hover table-bordered table-striped pagination-ys " ShowHeaderWhenEmpty="true"
                                        AutoGenerateColumns="False" DataKeyNames="ID">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("ID").ToString()%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Name" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpName" Text='<%# Eval("EmpName").ToString() %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="EmailId" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmail_ID" Text='<%# Eval("Email").ToString() %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="TNumber" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTNumber" Text='<%# Eval("TNumber").ToString() %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbEdit" runat="server" CssClass="label label-default" CausesValidation="False" OnClientClick="return confirm('Do you want to Edit Emp Info')" OnClick="lbEdit_Click" ToolTip="Edit"><i class="fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>&nbsp;&nbsp;
                                                    <%--<asp:LinkButton ID="lbdelete" runat="server" CssClass="label label-danger" OnClientClick="return confirm('Do you want to Delete Emp Info')" CausesValidation="False" OnClick="lbdelete_Click" ToolTip="Delete"><i class="fa fa-trash" aria-hidden="true"></i></asp:LinkButton>&nbsp;&nbsp;&nbsp;--%>
                                                </ItemTemplate>
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
      <script>
          $(function () {
              //Initialize Select2 Elements
              $('.select2').select2()
          })
    </script>
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


            var msg = "";

            if (document.getElementById('<%=txtempname.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Name. \n";
            }



            if (document.getElementById('<%=txtTnumber.ClientID%>').value.trim() == "") {
                msg = msg + "Enter TNumber. \n";
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

