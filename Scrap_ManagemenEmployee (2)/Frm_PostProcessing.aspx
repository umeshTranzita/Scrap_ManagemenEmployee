<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Frm_PostProcessing.aspx.cs" Inherits="Frm_PostProcessing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        fieldset {
            border: 1px solid #c7c7c7;
            padding: .35em .625em .75em;
            margin-bottom: 10px;
        }

        legend {
            width: initial;
            padding: 0px 10px;
            margin: 0;
            font-weight: bold;
            color: #003FF7;
            text-transform: uppercase;
            background-color: #FFFF00;
            border: 1px solid #ddd;
        }

        .txt:hover {
            background-color: RoyalBlue !Important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row mt-20">

        <div class="shipping-checkout-page">
            <div class="row">
                <div class="col s12">
                    <div class="section-title">
                        <p><span style="color: #101010">POST</span> PROCESSING</p>
                    </div>
                </div>
            </div>
            <div class="billing-detail-wrap ck-box mt-20">
                <div class="row">
                    <div class="col s12">
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblEmail_ID" runat="server" class="form-control" Visible="false"></asp:Label>

                    </div>
                </div>
                <div class="row" style="text-align: right">


                    <spam style="font-size: large; font-weight: 200;">TOTAL BAGS</spam>
                    : -    <strong>
                        <label id="LblTotalScrapBags" runat="server" style="font-size: large; font-weight: 200; color: red">0</label>
                        &nbsp;&nbsp;&nbsp;
                          <%-- <spam style="font-size: large; font-weight: 200;">TOTAL WEIGHT (In Kg)</spam> :---%>
                        <%--  <label id="LblTotalScrapWeight" runat="server" style="font-size: large; font-weight: 200; color: red">0</label>--%>
                    </strong>

                </div>

                <label id="LblRefNo" runat="server" style="font-size: large; font-weight: 200;" visible="false"></label>
                <div class="row" style="padding-top: 5px;">
                    <%-- <fieldset>
                        <legend>SCRAP BAGS DETAILS</legend>--%>
                    <div class="input-field col s6 m12 l2">
                        <div class="col s12 m12 l12">
                            <asp:DropDownList runat="server" ID="ddlType" AutoPostBack="True" class="form-control select2" OnSelectedIndexChanged="ddlType_SelectedIndexChanged1">
                                <asp:ListItem Value="0">Select Type</asp:ListItem>
                                <asp:ListItem Value="1">CORE</asp:ListItem>
                                <asp:ListItem Value="2">SCRAP</asp:ListItem>
                            </asp:DropDownList>

                        </div>
                    </div>
                    <div class="input-field col s6 m12 l2">
                        <div class="col s12 m12 l12">
                            <asp:DropDownList runat="server" ID="ddlScrapType" AutoPostBack="false" class="form-control select2">
                            </asp:DropDownList>

                        </div>
                    </div>
                    <div class="input-field col s6 m12 l3">
                        <asp:TextBox ID="TxtBagBatchNo" class="validate" runat="server" MaxLength="100" AutoComplete="off"></asp:TextBox>
                        <asp:TextBox ID="TxtBagNo" class="validate" runat="server" MaxLength="100" AutoComplete="off" Visible="false"></asp:TextBox>
                        <label id="BagBatchNo" runat="server"></label>
                    </div>
                    <div class="input-field col s6 m12 l2">
                        <asp:TextBox ID="TxtWeight" class="validate" runat="server" MaxLength="100" onkeypress="return validateDec(this,event)" AutoComplete="off"></asp:TextBox>
                        <label id="Weight" runat="server">WEIGHT(In Kg)<span style="color: red;">*</span></label>
                    </div>
                    <div class="input-field col s6 m12 l2">
                        <asp:TextBox ID="TxtBarcode" class="validate" runat="server" MaxLength="50" AutoPostBack="false" onkeypress="return validateDec(this,event)" AutoComplete="off"></asp:TextBox>
                        <label id="LblBarcode" runat="server" class="txt">SCAN QR CODE</label>
                    </div>
                    <div class="input-field col s6 m12 l2">
                            <asp:TextBox ID="TxtTierWeight" class="validate" runat="server" MaxLength="50" AutoPostBack="false" onkeypress="return validateDec(this,event)" AutoComplete="off" Text="0"></asp:TextBox>
                            <label id="LblTierWeight" runat="server" class="txt">Tare Weight (In gm)</label>
                        </div>
                    <div class="input-field col s6 m12 l2">
                        <asp:Button ID="Btn_Add" runat="server" class="btn theme-btn-rounded" Text="ADD" OnClientClick="return validateform();" OnClick="Btn_Add_Click" />
                    </div>
                    <%-- </fieldset>--%>
                </div>
                <div class="row" style="padding-top: 5px;">
                    <div class="input-field col s6 m12 l3">
                    </div>
                </div>
                <div class="mt-20">
                    <div class="table-responsive">
                        <asp:GridView ID="GridScrap" runat="server" class="table table-bordered table-striped" AutoGenerateColumns="False"
                            AllowPaging="True" DataKeyNames="Id" PageSize="100" ShowFooter="true" OnRowCommand="GridScrap_RowCommand" OnRowDeleting="GridScrap_RowDeleting" OnSelectedIndexChanged="GridScrap_SelectedIndexChanged">
                            <HeaderStyle BackColor="#df5015" Font-Bold="true" ForeColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%#Bind("Id") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Consigment No" ShowHeader="False" ItemStyle-Width="120">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="LblConsigmentNo" Text='<%# Eval("Consigment_No") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bag Batch No" ShowHeader="False" ItemStyle-Width="120">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="LblBag_BatchNo" Text='<%# Eval("Bag_BatchNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Scrap Type" ShowHeader="False" ItemStyle-Width="120">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="LblScrapType" Text='<%# Eval("ScrapType") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Weight" ShowHeader="False" ItemStyle-Width="120">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="LblWeight" Text='<%# Eval("Weight") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Barcode" ShowHeader="False" ItemStyle-Width="120">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="LblBarcode" Text='<%# Eval("Barcode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TierWeight" ShowHeader="False" ItemStyle-Width="120">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="LblTierWeight" Text='<%# Eval("TierWeight") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="NetWeight" ShowHeader="False" ItemStyle-Width="120">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="LblNetWeight" Text='<%# Eval("NetWeight") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Edit" ShowHeader="False" ItemStyle-Width="60">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkEdit" runat="server" CausesValidation="False" CommandName="Select" Text="EDIT" CssClass="btn btn-block btn-info btn-xs"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ShowHeader="False" ItemStyle-Width="60" Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkDelete" runat="server" CausesValidation="False" CommandName="Delete" Text="DELETE" CssClass="btn btn-block btn-info btn-xs" CommandArgument='<%# Bind("Id") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <asp:Panel ID="PanelFooter" runat="server" Visible="false">
                    <div class="row">
                        <div class="input-field col s12 m12 l12">
                            <h4>CONVERSATION RATIO : -
                        <b>
                            <asp:Label ID="LblConversationRatio" runat="server" Text="0"></asp:Label></b></h4>
                        </div>
                    </div>
                    <div class="row">
                        <div class="input-field col s12 m12 l12">

                            <asp:Button ID="btnCancel" runat="server" class="btn btn-cancel theme-btn-rounded" Text="CANCEL" OnClick="btnCancel_Click" />

                            <asp:Button ID="BtnSubmit" runat="server" class="btn theme-btn-rounded" Text="SUBMIT" />
                        </div>
                    </div>
                </asp:Panel>
            </div>





        </div>



        <script type="text/javascript">
            function showModal() {
                $("#basicExampleModal").modal('show');
            }
            function showModal1() {
                $("#basicExampleModal1").modal('show');
            }
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
                if (document.getElementById('<%=ddlType.ClientID%>').selectedIndex == "0") {
                    msg += "Select Type. \n";
                }
                if (document.getElementById('<%=ddlScrapType.ClientID%>').selectedIndex == "0") {
                    msg += "Select Scrap Type. \n";
                }
                if (document.getElementById('<%=TxtWeight.ClientID%>').value.trim() == "") {
                    msg += "Please Enter Weight. \n";
                }
                if (document.getElementById('<%=TxtTierWeight.ClientID%>').value.trim() == "") {
                    msg += "Enter Tier Weight. \n";
                }

                if (msg != "") {
                    alert(msg);
                    return false;
                }
                else {
                    if (document.getElementById('<%=btnCancel.ClientID%>').value.trim() == "SUBMIT") {
                        if (confirm("Do you really want to Submit Details ?")) {
                            return true;
                        }
                        else {
                            return false;
                        }
                    }
                    if (document.getElementById('<%=btnCancel.ClientID%>').value.trim() == "MODIFY") {
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


