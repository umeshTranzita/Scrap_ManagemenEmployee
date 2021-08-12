<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Frm_QRTemplate.aspx.cs" Inherits="Frm_QRTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
        .txt:hover {background-color: RoyalBlue !Important;

        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row mt-20">

        <div class="shipping-checkout-page">
            <div class="row">
                <div class="col s12">
                    <div class="section-title">
                        <p><span style="color: #101010">QR</span> TEMPLATE</p>
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
                        <asp:Label ID="lblEmail_ID" runat="server" class="form-control" Visible="false"></asp:Label>
                    </div>
                </div>
                <asp:Panel runat="server" ID="PanelScrap" Visible="True">
                
                 
              
                <div class="row" style="padding-top: 5px;">

                  
                        <div class="input-field col s6 m12 l2">
                             <div class="col s12 m12 l12">
                             <asp:DropDownList runat="server" ID="ddlSite" AutoPostBack="false" class="form-control select2" Visible="True"></asp:DropDownList>
                         </div>
                        </div>
                        <div class="input-field col s6 m12 l2">
                            <asp:TextBox ID="TxtTotalQrNo" class="validate" runat="server" MaxLength="100" ReadOnly="false" onkeypress="return validateDec(this,event)"></asp:TextBox>
                            <label id="BagNo" runat="server">Number Of QR Code<span style="color: red;">*</span></label>
                            <asp:DropDownList runat="server" ID="ddlDepartment" AutoPostBack="true" class="form-control select2" Visible="false"></asp:DropDownList>
                      </div>

                        <div class="input-field col s6 m12 l2">
                            <asp:Button ID="Btn_Add" runat="server" class="btn theme-btn-rounded" Text="Generate" OnClick="Btn_Add_Click" />
                        </div>
                      <div class="input-field col s6 m12 l1">


                <asp:LinkButton runat="server" class="btn theme-btn-rounded" Text="<i class='fa fa-download'></i>&nbsp;&nbsp;Export" ID="btnExport" OnClick="btnExport_Click"></asp:LinkButton>

            </div>
                  
                </div>



               

                <div class="mt-20">
                    <div class="table-responsive">

                        <asp:GridView ID="GridScrap" runat="server" class="table table-bordered table-striped" AutoGenerateColumns="False"
                            AllowPaging="false">
                            <Columns>
                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="MinNo" HeaderText="FIRST QR NUMBER" />
								 <asp:BoundField DataField="MaxNo" HeaderText="LAST QR NUMBER" />
                               
                            </Columns>
                        </asp:GridView>
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
            function validatename(evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if ((charCode < 48 || charCode > 57) && (charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122) && charCode != 32) {
                    return false;
                }
                return true;
            }

            function validateform() {
                var msg = "";
                <%--if (document.getElementById('<%=ddlCategory.ClientID%>').selectedIndex == 0) {
                msg += "Please Select Category.\n";
            }
                if (document.getElementById('<%=TxtTruckNo.ClientID%>').value.trim() == "") {
                    msg += "Enter Truck No. \n";
                }
                if (document.getElementById('<%=TxtInitialWeight.ClientID%>').value.trim() == "") {
                    msg += "Enter Initial Weight. \n";
                }--%>
              <%--  if (document.getElementById('<%=TxtProductUrl.ClientID%>').value.trim() == "") {
                    msg += "Enter Product Url. \n";
                }--%>

                if (msg != "") {
                    alert(msg);
                    return false;
                }
                else {
                   
                   
                }
            }

        </script>

        <%--  <div class="row" style="padding-top: 5px;">
                    <div class="input-field col s6 m12 l3">
                        <asp:TextBox ID="TxtTruckNo" class="validate" runat="server" MaxLength="50" OnTextChanged="TxtTruckNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                     
                        <label id="TruckNo" runat="server">Enter Truck Number<span style="color: red;">*</span></label>
                    </div>

                    <div class="input-field col s6 m12 l3">
                        <asp:TextBox ID="TxtInitialWeight" class="validate" runat="server" MaxLength="50" AutoPostBack="false" onkeypress="return validateNum(event);"></asp:TextBox>
                        <label id="InitialWeight" runat="server">Enter Truck Inital Weight<span style="color: red;">*</span></label>
                    </div>
                    <div class="input-field col s6 m12 l3">
                        <asp:TextBox ID="TxtFinalWeight" class="validate" runat="server" MaxLength="50" AutoPostBack="false" onkeypress="return validateNum(event);"></asp:TextBox>
                        <label id="FinalWeight" runat="server">Enter Truck Final Weight<span style="color: red;">*</span></label>
                    </div>
                </div>--%>
    </div>
</asp:Content>

