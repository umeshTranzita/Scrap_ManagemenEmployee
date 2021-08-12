<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Frm_PostProcessSetLossPer.aspx.cs" Inherits="Frm_PostProcessSetLossPer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row mt-20">

        <div class="shipping-checkout-page">
            <div class="row">
                <div class="col s12">
                    <div class="section-title">
                        <p><span style="color: #101010">POST PROCESS</span> LOSS PERCENTAGE</p>
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
                    
                    <div class="input-field col s6 m12 l3">
                        <div class="col s12 m12 l6">
                         <asp:DropDownList runat="server" ID="ddlScrapType" AutoPostBack="True" class="form-control select2"></asp:DropDownList>
                    </div>
                        </div>
                 
                    <div class="input-field col s6 m12 l4">
                        <asp:TextBox ID="TxtLossPercentage" class="validate" runat="server" Text="0"></asp:TextBox>
                        <label id="Description" runat="server">Enter Loss Percentage</label>
                    </div>
                     <div class="col s6 m12 l1">
                    <input type="checkbox" class="filled-in" id="chkIsActive" checked="checked" clientidmode="Static" runat="server" />
                    <%--<asp:CheckBox ID="chkIsActive" CssClass="filled-in" runat="server" Checked="true" />--%>
                    <label class="label-checkbox" for="chkIsActive">Active Status</label>

                       
                </div>
                     <div class="input-field col s12 m12 l3">
                        <asp:Button ID="btnSubmit" runat="server" class="btn btn-sm" Text="SUBMIT" OnClick="btnSubmit_Click" OnClientClick="return validateform()"/>
                  
                    </div>
                </div>


               





                <div class="row">
                    <%-- OnClientClick="return validateform();"--%>
                   
                </div>
            </div>
            <div class="mt-20">
                <div class="table-responsive">

                    <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-striped" AutoGenerateColumns="False"
                        AllowPaging="True" DataKeyNames="Id" PageSize="100" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowCommand="GridView1_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%#Bind("Id") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:BoundField DataField="Scrap_Type" HeaderText="Scrap Type" />
                            <asp:BoundField DataField="Loss_Percentage" HeaderText="Loss %" />                      
                            <asp:BoundField DataField="IsActive" HeaderText="Active" />
                            <asp:TemplateField HeaderText="Edit" ShowHeader="False" ItemStyle-Width="60">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="EDIT" CssClass="btn btn-block btn-info btn-xs" CommandArgument='<%# Bind("Id") %>'></asp:LinkButton>
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

                if (document.getElementById('<%=ddlScrapType.ClientID%>').selectedIndex == 0) {
                    msg += "Please Select Scrap Type.\n";
                }
                if (document.getElementById('<%=TxtLossPercentage.ClientID%>').value.trim() == "") {
                    msg += "Enter Loss Percentage. \n";
                }
                <%--if (document.getElementById('<%=txtScrapCode.ClientID%>').value.trim() == "") {
                    msg += "Enter Scrap Code. \n";
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




    </div>
</asp:Content>

