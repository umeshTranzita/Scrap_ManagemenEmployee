<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Frm_ScrapSold.aspx.cs" Inherits="Frm_ScrapSold" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server" ID="script1"></asp:ScriptManager>
    <div class="shipping-checkout-page">
        <div class="row">
            <div class="col s12">
                <div class="section-title">
                    <p><span style="color: #101010">SCRAP </span>&nbsp;SALE</p>
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
                <div class="input-field col s6 m12 l3">
                    <div class="col s12 m12 l12">
                        <asp:DropDownList runat="server" ID="ddlSold" AutoPostBack="True" class="form-control select2" OnSelectedIndexChanged="ddlSold_SelectedIndexChanged">
                            <asp:ListItem Value="0">SELECT SOLD TYPE</asp:ListItem>
                            <asp:ListItem Value="1">YES</asp:ListItem>
                            <asp:ListItem Value="2">NO</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <asp:Panel runat="server" ID="Panel1" Visible="false">
                <div class="input-field col s6 m12 l2">
                    <asp:TextBox ID="TxtAllSapDock" class="validate" runat="server" MaxLength="50" AutoPostBack="false" AutoComplete="off"></asp:TextBox>
                    <label id="AllSapDock" runat="server" class="txt">SAP DOCK NO<span style="color: red;">*</span></label>
                </div>
                <div class="col s6 m12 l2">
                   
                    <asp:CheckBox ID="chkSapCode" runat="server" OnCheckedChanged="chkSapCode_CheckedChanged" AutoPostBack="true"/>
                    <label class="label-checkbox" for="chkIsActive">COPY SAP DOCK NO</label>
                     

                </div>
                      </asp:Panel>
                <div class="input-field col s12 m12 l2">
                    <asp:Button ID="BtnSubmit" runat="server" class="btn theme-btn-rounded" Text="SUBMIT" Visible="false" OnClick="BtnSubmit_Click" />
                </div>
                  
                  


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
                                <asp:CheckBox ID="checkAll" runat="server" OnCheckedChanged="checkAll_CheckedChanged" AutoPostBack="true" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkIsActive" runat="server" ClientIDMode="Static" OnCheckedChanged="chkIsActive_CheckedChanged" AutoPostBack="true" Visible='<%# Eval("Sold_Status").ToString()=="Pending" ? true : false %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Consigment No" ShowHeader="False">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="LblRefrenceNo" Text='<%# Eval("Refrence_No") %>'></asp:Label>
                                <asp:Label runat="server" ID="LblStatus" Text='<%# Eval("Sold_Status") %>' Visible="false"></asp:Label>
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
                                <asp:Label runat="server" ID="LblTierWeight" Text='<%# Eval("TierWeight") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Net Weight" ShowHeader="False">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="LblNetWeight" Text='<%# Eval("NetWeight") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sap_Dock/No" ShowHeader="False">
                            <ItemTemplate>
                                <asp:TextBox ID="TxtSapDockNo" runat="server" Text='<%# Eval("SapDockNo") %>' Visible="True" Enabled="false"></asp:TextBox>
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


    </script>

</asp:Content>

