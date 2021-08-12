<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Frm_ToleranceMaster.aspx.cs" Inherits="Frm_ToleranceMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row mt-20">

        <div class="shipping-checkout-page">
            <div class="row">
                <div class="col s12">
                    <div class="section-title">
                        <p><span style="color: #101010">TOLERANCE</span> MASTER</p>
                    </div>
                </div>
            </div>
           
            <div class="billing-detail-wrap ck-box mt-20">
                 <div class="row" style="padding-top: 20px;">
                       <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                    <div class="input-field col s6 m12 l6">
                        <div class="col s12 m12 l6">
                            <asp:DropDownList runat="server" ID="ddlSite" AutoPostBack="True" class="form-control select2" OnSelectedIndexChanged="ddlSite_SelectedIndexChanged"></asp:DropDownList>
                            <%--    <label>Select Category<span style="color: red;">*</span></label>--%>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col s12">
                       
                        <asp:Label ID="lblUserID" runat="server" class="form-control" Visible="false"></asp:Label>
                        <asp:Label ID="lblRoleID" runat="server" class="form-control" Visible="false"></asp:Label>
                        <asp:Label ID="lblEmail_ID" runat="server" class="form-control" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="input-field col s6 m6 l4">
                        <div class="col s12 m12 l6">
                            <asp:DropDownList runat="server" ID="ddlType" AutoPostBack="false" class="form-control select2">
                                <asp:ListItem Value="0">SELECT TYPE</asp:ListItem>
                                <asp:ListItem Value="1">PG-CM</asp:ListItem>
                                <asp:ListItem Value="2">CM-PG</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="input-field col s6 m6 l4">
                        <div class="col s12 m12 l6">
                            <asp:DropDownList runat="server" ID="ddlToleranceType" AutoPostBack="false" class="form-control select2">
                                <asp:ListItem Value="0">TOLERANCE TYPE</asp:ListItem>
                                 <asp:ListItem Value="1">FOR DISPATCH</asp:ListItem>
                                <asp:ListItem Value="2">FOR RECEIVE BAG</asp:ListItem>
                                <asp:ListItem Value="3">FOR RECEIVE OVERALL</asp:ListItem>
                                <asp:ListItem Value="4">FOR ISSUE BAG</asp:ListItem>
                                <asp:ListItem Value="5">FOR ISSUE OVERALL</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="input-field col s6 m12 l3 ">
                        <asp:TextBox ID="txtTolerance" class="validate" runat="server" Text="0" onkeypress="return validateDec(this,event)"></asp:TextBox>
                        <label id="Tolerance" runat="server">Enter Tolerance Weight (In Kg)<span style="color: red;">*</span></label>
                    </div>
                    <div class="col s6 m12 l3" style="padding-top: 20px;">
                        <input type="checkbox" class="filled-in" id="chkIsActive" checked="checked" clientidmode="Static" runat="server" />
                        <%--<asp:CheckBox ID="chkIsActive" CssClass="filled-in" runat="server" Checked="true" />--%>
                        <label class="label-checkbox" for="chkIsActive">Active Status</label>
                    </div>
                </div>
                <div class="row">
                    <%-- OnClientClick="return validateform();"--%>
                    <div class="input-field col s12 m12 l12">
                        <asp:Button ID="btnSubmit" runat="server" class="btn btn-sm" Text="SUBMIT" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnCancel" runat="server" class="btn btn-cancel btn-sm" Text="CANCEL" OnClick="btnCancel_Click" />
                    </div>
                </div>
            </div>
            <div class="mt-20">
                <div class="table-responsive">

                    <asp:GridView ID="GridTolerance" runat="server" class="table table-bordered table-striped" AutoGenerateColumns="False"
                        AllowPaging="True" DataKeyNames="Tolerance_Id" PageSize="100" OnSelectedIndexChanged="GridTolerance_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%#Bind("Tolerance_Id") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="Type" HeaderText="Type" />
                            <asp:BoundField DataField="Tolerance_Type" HeaderText="Tolerance Type" />
                            <asp:BoundField DataField="Tolerance_Weight" HeaderText="Tolerance Weight (In Kg)" />
                            <asp:BoundField DataField="IsActive" HeaderText="Active" />
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
            function validateDec(el, evt) {
                var charCode = (evt.which) ? evt.which : event.keyCode;
                var number = el.value.split('.');
                if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                //just one dot (thanks ddlab)
                if (number.length > 1 && charCode == 46) {
                    return false;
                }
                //get the carat position
                var caratPos = getSelectionStart(el);
                var dotPos = el.value.indexOf(".");
                if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
                    return false;
                }
                return true;
            }

            function validateform() {
                var msg = "";
                if (document.getElementById('<%=ddlType.ClientID%>').selectedIndex == 0) {
                    msg += "Please Select Site.\n";
                }
               <%-- if (document.getElementById('<%=txtDepartmentName.ClientID%>').value.trim() == "") {
                    msg += "Enter Department Name. \n";
                }--%>
                if (document.getElementById('<%=txtTolerance.ClientID%>').value.trim() == "") {
                    msg += "Enter Tolearnce. \n";
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

