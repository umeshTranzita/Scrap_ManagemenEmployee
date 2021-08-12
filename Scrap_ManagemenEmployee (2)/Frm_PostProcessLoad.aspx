<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Frm_PostProcessLoad.aspx.cs" Inherits="Frm_PostProcessLoad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server" ID="script1"></asp:ScriptManager>
    <div class="shipping-checkout-page">
        <div class="row">
            <div class="col s12">
                <div class="section-title">
                    <p><span style="color: #101010">Load PP </span>&nbsp;Bags</p>
                </div>
            </div>
        </div>
        <div class="row" style="padding-top: 10px;">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            <asp:Label ID="LblErrorMsg" runat="server" Text=""></asp:Label>
        </div>
        <div class="mt-20">


            <div class="row" style="padding-top: 20px;">

                <div class="input-field col s6 m12 l3">
                    <div class="col s12 m12 l12">
                        <asp:DropDownList runat="server" ID="ddlConsigment" AutoPostBack="true" class="form-control select2" OnSelectedIndexChanged="ddlConsigment_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
               <asp:Panel runat="server" ID="Panel1" Visible="false">
                <div class="input-field col s6 m12 l2">
                    <asp:TextBox ID="TxtTruckNo" class="validate" runat="server" MaxLength="50" AutoPostBack="false" AutoComplete="off" onkeypress="return validatename(event);"></asp:TextBox>
                    <label id="TruckNo" runat="server" class="txt">Truck No<span style="color: red;">*</span></label>
                </div>
                
                <div class="input-field col s12 m12 l2">
                    <asp:Button ID="BtnSubmit" runat="server" class="btn theme-btn-rounded" Text="SUBMIT" Visible="True" OnClick="BtnSubmit_Click" />
                </div>
                </asp:Panel>
                  
                  
                  


            </div>
            <%-- <asp:Panel runat="server" ID="PanelISSUE" Visible="false">--%>
            <div class="table-responsive" style="padding-top: 90px;">
                <asp:GridView ID="GridSold" runat="server" class="table table-striped table-bordered" AutoGenerateColumns="False"
                    AllowPaging="True" DataKeyNames="PPID" PageSize="100" ShowFooter="true">
                    <HeaderStyle BackColor="#df5015" Font-Bold="true" ForeColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%#Bind("Id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="50">
                       <HeaderTemplate>
                                <asp:CheckBox ID="checkAll" runat="server"  AutoPostBack="true" OnCheckedChanged="checkAll_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkIsActive" runat="server" ClientIDMode="Static" AutoPostBack="false" Visible='<%# Eval("BagLoad_Status").ToString()=="Pending" ? true : false %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Consigment No" ShowHeader="False">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="LblRefrenceNo" Text='<%# Eval("Refrence_No") %>'></asp:Label>
                                <asp:Label runat="server" ID="LblStatus" Text='<%# Eval("BagLoad_Status") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bag BatchNo" ShowHeader="False">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="LblBag_BatchNo" Text='<%# Eval("Bag_BatchNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Weight" ShowHeader="False">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="LblBagWeight" Text='<%# Eval("Weight") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Tare Weight" ShowHeader="False">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="LblTareWeight" Text='<%# Eval("TierWeight") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Net Weight" ShowHeader="False">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="LblNetWeight" Text='<%# Eval("NetWeight") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sap_Dock/No" ShowHeader="False">
                            <ItemTemplate>
                                  <asp:Label runat="server" ID="LblSapDockNo" Text='<%# Eval("SapDockNo") %>'></asp:Label>
                               
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Truck No" ShowHeader="False">
                            <ItemTemplate>
                                  <asp:Label runat="server" ID="LblTruckNo" Text='<%# Eval("LoadTruck_No") %>'></asp:Label>
                               
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <%-- </asp:Panel>--%>
        </div>
    </div>
    <style>
        [type="checkbox"]:not(:checked), [type="checkbox"]:checked {
            position: absolute;
            opacity: 9;
            pointer-events: all;
        }
    </style>
    <script>
        function enterToTab(e) {
            var intKey = window.Event ? e.which : e.KeyCode;

            if (intKey == 13) {
                e.keyCode = 9;
                return e;
            }
        }
    </script>
    <script type="text/javascript">
        function validateNum(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;

        }
        function validatename(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if ((charCode < 48 || charCode > 57) && (charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122) && charCode != 32) {
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
         <%--    if (document.getElementById('<%=ddlCategory.ClientID%>').selectedIndex == 0) {
                msg += "Please Select Category.\n";
            }--%>
                if (document.getElementById('<%=TxtTruckNo.ClientID%>').value.trim() == "") {
                    msg += "Please Enter Truck No. \n";
                }
               

             if (msg != "") {
                 alert(msg);
                 return false;
             }
             else {
                 if (document.getElementById('<%=BtnSubmit.ClientID%>').value.trim() == "SUBMIT") {
                        if (confirm("Do you really want to Submit Details ?")) {
                            return true;
                        }
                        else {
                            return false;
                        }
                    }
                    if (document.getElementById('<%=BtnSubmit.ClientID%>').value.trim() == "MODIFY") {
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

