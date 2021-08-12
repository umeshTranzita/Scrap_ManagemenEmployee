<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Frm_ScrapIssue.aspx.cs" Inherits="Frm_ScrapIssue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  <style>
        .qrcode-text-btn {
            display: inline-block;
            height: 1em;
            width: 1em;
            background: url(//dab1nmslvvntp.cloudfront.net/wp-content/uploads/2017/07/1499401426qr_icon.svg) 50% 50% no-repeat;
            /*background: url(images/QrICCon.jpg) 50% 50% no-repeat;*/
            cursor: pointer;
        }

            .qrcode-text-btn > input[type=file] {
                position: absolute;
                overflow: hidden;
                width: 1px;
                height: 1px;
                opacity: 0;
            }

        .qrcode-text {
            padding-right: 1.7em;
            margin-right: 0;          
            vertical-align: middle;
             width: 30% !important;
            
            
        }

            .qrcode-text + .qrcode-text-btn {
                 width: 10em !important;
                height: 5em!important;
                margin-left: 0em;
               vertical-align: middle;

            }

        @media only screen and (max-device-width:750px) {
            /* previous CSS code goes here */
        }
    </style>

    <script src="https://rawgit.com/sitepoint-editors/jsqrcode/master/src/qr_packed.js">
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server" ID="script1"></asp:ScriptManager>
    <div class="shipping-checkout-page">
        <div class="row">
            <div class="col s12">
                <div class="section-title">
                    <p><span style="color: #101010">SCRAP </span>&nbsp;ISSUE</p>
                </div>
            </div>
        </div>
        <div class="row" style="padding-top: 10px;">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            <asp:Label ID="LblErrorMsg" runat="server" Text=""></asp:Label>
            <asp:Label ID="LblConsigmentSiteId" runat="server" Text="" Visible="false"></asp:Label>
        </div>
        <div class="mt-20">

           <asp:RadioButtonList runat="server" ID="RadioList" OnTextChanged="RadioList_TextChanged" AutoPostBack="true">
               <asp:ListItem Value="1">Search By Consigment No</asp:ListItem>
               <asp:ListItem Value="2">Search By Scan</asp:ListItem>
           </asp:RadioButtonList>
            <div class="row" style="padding-top: 20px;">
                <asp:Panel runat="server" ID="ConsigmentPanel" Visible="false">
                    <div class="input-field col s6 m12 l4">
                        <div class="col s12 m12 l9">
                            <asp:DropDownList runat="server" ID="ddlConsigment" AutoPostBack="true" class="form-control select2" OnSelectedIndexChanged="ddlConsigment_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="input-field col s6 m12 l4">
                        <div class="col s12 m12 l9">
                            <asp:DropDownList runat="server" ID="ddlScrapType" AutoPostBack="true" class="form-control select2" OnSelectedIndexChanged="ddlScrap_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                </asp:Panel>
              <%--  <asp:Panel runat="server" ID="ScanPanel" Visible="false">
                    <div class="input-field col s6 m12 l2">
                        <asp:TextBox ID="TxtScanBarcode" class="validate" runat="server" MaxLength="100" AutoPostBack="true" AutoComplete="off" OnTextChanged="TxtScanBarcode_TextChanged"></asp:TextBox>
                        <label id="ScanBarcode" runat="server" class="txt">SCAN QR/BAR CODE</label>
                    </div>
                </asp:Panel>--%>
              
                    <asp:Panel runat="server" ID="ScanPanel" Visible="false">
                    <label id="Label1" runat="server" class="txt">SCAN QR/BAR CODE</label> <br />

                    <%-- <label class=qrcode-text-bt <input type=file   accept="image/*"  capture=environment  tabindex=-1>--%>
                  
                        <asp:TextBox ID="TxtScanBarcode" class="qrcode-text" runat="server" MaxLength="100" AutoPostBack="True" AutoComplete="off"></asp:TextBox>
                        <label class="qrcode-text-btn">
                            <input type="file" accept="image/*" capture="environment" tabindex="-1" onchange="openQRCamera(this);" onclick="return showQRIntro();"/></label>
                       <asp:Button ID="Btn_Search" runat="server" class="btn theme-btn-rounded" Text="GO" OnClick="Btn_Search_Click" />
                       
                   </asp:Panel>
                    
            </div>
            <asp:Panel runat="server" ID="PanelISSUE" Visible="false">
                <div class="table-responsive" style="padding-top: 20px;">
                    <asp:GridView ID="GridIssue" runat="server" class="table table-striped table-bordered" AutoGenerateColumns="False"
                        AllowPaging="True" DataKeyNames="Scrap_Id" PageSize="100" ShowFooter="true">
                        <HeaderStyle BackColor="#df5015" Font-Bold="true" ForeColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%#Bind("Scrap_Id") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="50">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="checkAll" runat="server" OnCheckedChanged="checkAll_CheckedChanged" AutoPostBack="true" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkIsActive" runat="server" ClientIDMode="Static" OnCheckedChanged="chkIsActive_CheckedChanged" AutoPostBack="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Consigment No" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblRefrenceNo" Text='<%# Eval("Refrence_No") %>'></asp:Label>
                                    <asp:Label runat="server" ID="LblStatus" Text='<%# Eval("Status") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bag BatchNo" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblBag_BatchNo" Text='<%# Eval("Bag_BatchNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="NewBagWeight" HeaderText="Dispatch Wt" />
                            <asp:TemplateField HeaderText="Receive Wt" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:TextBox ID="TxtReceiveWeight" runat="server" Text='<%# Eval("Receive_Weight") %>' onkeypress="return validateDec(this,event)" AutoPostBack="true"></asp:TextBox>
                                    <asp:TextBox ID="TxtBagWeight" runat="server" Text='<%# Eval("Bag_Weight") %>' Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="TxtBagToleranceWt" runat="server" Text='<%# Eval("Tolerance_Issuebag") %>' Visible="false"></asp:TextBox>
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
                            <asp:TemplateField HeaderText="Issue Wt" ShowHeader="True">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="checkAllIssueWt" runat="server" OnCheckedChanged="checkAllIssueWt_CheckedChanged" AutoPostBack="true" Text="Select All" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TxtIssueWeight" runat="server" Text='<%# Eval("Issue_Weight") %>' onkeypress="return validateDec(this,event)" AutoPostBack="true" OnTextChanged="TxtIssueWeight_TextChanged"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblBagStatus" Text='<%# Eval("Issue_Status") %>'></asp:Label>
                                    <asp:Label runat="server" ID="LblScrapType" Text='<%# Eval("Scrap_Type") %>' Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="LblReceiveStatus" Text='<%# Eval("Receive_Status") %>' Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="LblPostActionStatus" Text='<%# Eval("Post_ActionStatus") %>' Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="LblSiteId" Text='<%# Eval("Site_Id") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </div>
                <div class="input-field col s12 m12 l12">
                    <asp:Label runat="server" ID="LblOverAllStatus" Text='' Visible="false"></asp:Label>
                    <asp:Button ID="BtnIssue" runat="server" class="btn theme-btn-rounded" Text="Issue" Visible="false" OnClick="BtnIssue_Click" />
                </div>
            </asp:Panel>
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

        <%--  function validateform() {
            var msg = "";
           if (document.getElementById('<%=TxtLockNo.ClientID%>').value.trim() == "") {
                msg += "Enter Lock No. \n";
            }

            if (msg != "") {
                $("#LblErrorMsg").text(msg);
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
        }--%>
    </script>
    <script>
        function openQRCamera(node) {
            var reader = new FileReader();
            reader.onload = function () {
                node.value = "";
                qrcode.callback = function (res) {
                    if (res instanceof Error) {
                        alert("No QR code found. Please make sure the QR code is within the camera's frame and try again.");
                    } else {
                        node.parentNode.previousElementSibling.value = res;
                        QrValue = res;
                    }
                };
                qrcode.decode(reader.result);
            };
            reader.readAsDataURL(node.files[0]);
        }

        function showQRIntro() {
            return confirm("Use your camera to take a picture of a QR code.");
        }
    </script>
    <%--<script type="text/javascript">
        function checkAll(objRef) {
            debugger;
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }

    </script>--%>
</asp:Content>

