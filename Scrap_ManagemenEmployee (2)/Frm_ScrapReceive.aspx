<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Frm_ScrapReceive.aspx.cs" Inherits="Frm_ScrapReceive" MaintainScrollPositionOnPostback="True" %>

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
            width: 20% !important;
        }

            .qrcode-text + .qrcode-text-btn {
                width: 10em !important;
                height: 5em !important;
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
                    <p><span style="color: #101010">SCRAP </span>&nbsp;RECEIVE</p>
                </div>
            </div>
        </div>
        <div class="row" style="padding-top: 10px;">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            <asp:Label ID="LblSite_Id" runat="server" Text="" Visible="false"></asp:Label>

        </div>
        <div class="mt-20">
            <asp:Panel runat="server" ID="PanelReceive" Visible="false">
                <div class="row" style="padding-top: 20px;">

                    <div class="input-field col s6 m12 l3">
                        <div class="col s12 m12 l12">
                            <asp:DropDownList runat="server" ID="ddlConsigment" AutoPostBack="true" class="form-control select2" OnSelectedIndexChanged="ddlConsigment_SelectedIndexChanged"></asp:DropDownList>
                            <%--<label>Select Category<span style="color: red;">*</span></label>--%>
                        </div>
                    </div>
                    <div class="input-field col s6 m12 l3" hidden>
                        <div class="col s12 m12 l12">
                            <asp:DropDownList runat="server" ID="ddlBagNo" AutoPostBack="true" class="form-control select2" OnSelectedIndexChanged="ddlBagNo_SelectedIndexChanged"></asp:DropDownList>
                            <%--<label>Select Category<span style="color: red;">*</span></label>--%>
                        </div>
                    </div>





                    <asp:Panel runat="server" ID="Panel1" Visible="false">
                        <div class="input-field col s6 m12 l2">
                            <asp:TextBox ID="TxtTierWeight" class="validate" runat="server" MaxLength="50" AutoPostBack="false" AutoComplete="off"></asp:TextBox>
                            <label id="TierWeight" runat="server" class="txt">Tier Weight (In gm)<span style="color: red;">*</span></label>
                        </div>
                        <div class="col s6 m12 l2">

                            <asp:CheckBox ID="chkSapCode" runat="server" OnCheckedChanged="chkSapCode_CheckedChanged" AutoPostBack="true" />
                            <label class="label-checkbox" for="chkIsActive">COPY TIER WEIGHT TO ALL</label>


                        </div>
                    </asp:Panel>

                    <asp:Panel runat="server" ID="ScanPanel" Visible="false">
                        <label id="Label1" runat="server" class="txt">SCAN QR CODE</label>
                        <br />

                        <%-- <label class=qrcode-text-bt <input type=file   accept="image/*"  capture=environment  tabindex=-1>--%>

                        <asp:TextBox ID="TxtScanBarcode" class="qrcode-text" runat="server" MaxLength="100" AutoPostBack="True" AutoComplete="off"></asp:TextBox>
                        <label class="qrcode-text-btn">
                            <input type="file" accept="image/*" capture="environment" tabindex="-1" onchange="openQRCamera(this);" onclick="return showQRIntro();" /></label>
                        <asp:Button ID="Btn_Search" runat="server" class="btn theme-btn-rounded" Text="GO" OnClick="Btn_Search_Click"/>
                        <asp:Button ID="ShowAll" runat="server" class="btn theme-btn-rounded" Text="Show All Record" OnClick="ShowAll_Click" Visible="false"/>
                    </asp:Panel>



                </div>
                <div class="table-responsive" style="padding-top: 20px;">
                    <asp:GridView ID="GridReceive" runat="server" class="table table-striped table-bordered" AutoGenerateColumns="False"
                        AllowPaging="True" DataKeyNames="Scrap_Id" PageSize="100" OnRowDataBound="GridReceive_RowDataBound" ShowFooter="true">
                        <Columns>
                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%#Bind("Scrap_Id") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Consigment No" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblRefrenceNo" Text='<%# Eval("Refrence_No") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bag BatchNo" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblBag_BatchNo" Text='<%# Eval("Bag_BatchNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Barcode" HeaderText="QR Code" />
                            <%--<asp:BoundField DataField="NewBagWeight" HeaderText="Bag Weight" />--%>
                            <asp:TemplateField HeaderText="Receive Weight (In Kg)" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:TextBox ID="TxtReceiveWeight" runat="server" Text='<%# Eval("Receive_Weight") %>' onkeypress="return validateDec(this,event)" OnTextChanged="TxtReceiveWeight_TextChanged" AutoPostBack="true" AutoComplete="off"></asp:TextBox>
                                    <asp:TextBox ID="TxtBagWeight" runat="server" Text='<%# Eval("LoadNetWeight") %>' Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="TxtBagToleranceWt" runat="server" Text='<%# Eval("Tolerance_Receivebag") %>' Visible="false"></asp:TextBox>
                                    <asp:Label runat="server" ID="LblStatus" Text='<%# Eval("Status") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tare Weight (In gm)" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:TextBox ID="TxtTierWeigh" runat="server" Text='<%# Eval("TierWeight") %>' onkeypress="return validateDec(this,event)" OnTextChanged="TxtReceiveWeight_TextChanged" AutoPostBack="true" AutoComplete="off"></asp:TextBox>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Net Weight (In Kg)" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblNetWeight" Text='<%# Eval("NetWeight") %>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblBagStatus" Text='<%# Eval("Receive_Status") %>'></asp:Label>
                                    <asp:Label runat="server" ID="LblScrapType" Text='<%# Eval("Scrap_Type") %>' Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="LblBarcode" Text='<%# Eval("Barcode") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

                <div class="row">

                    <div class="input-field col s12 m12 l12">

                        <asp:Label runat="server" ID="LblOverAllStatus" Text='' Visible="false"></asp:Label>
                        <asp:Button ID="BtnReceive" runat="server" class="btn theme-btn-rounded" Text="RECEIVE" Visible="false" OnClick="BtnReceive_Click" />
                    </div>
                </div>
            </asp:Panel>
        </div>
        <%--<asp:UpdatePanel runat="server" ID="panel1">
            <ContentTemplate>--%>
        <div class="modal fade" id="basicExampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
            aria-hidden="true">

            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">ENTER LOCK DETAIL</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclose="dashboard.aspx">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="input-field col s6 m12 l6">
                            <asp:TextBox ID="TxtLockNo" class="validate" runat="server" MaxLength="50" AutoPostBack="false" AutoComplete="off"></asp:TextBox>
                            <label id="LockNo" runat="server">Lock No OR Consigment No<span style="color: red;">*</span></label>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <span style="color: red; font-weight: 300"><strong>
                            <asp:Label runat="server" ClientIDMode="Static" ID="LblErrorMsg"></asp:Label></strong></span>
                        <%-- <button type="submit" class="btn btn-cancel btn-sm"  onclick="~/dashboard.aspx">Close</button>--%>
                        <a href="dashboard.aspx" class="btn btn-cancel theme-btn-rounded">Cancel</a>
                        <%--<button type="button" class="btn btn-primary">Save changes</button>--%>
                        <asp:Button ID="btnSubmit" runat="server" class="btn theme-btn-rounded" Text="SHOW DETAILS" OnClientClick="return validateform();" ClientIDMode="Static" OnClick="btnSubmit_Click" />
                    </div>
                </div>
            </div>
        </div>
        <%--  </ContentTemplate>
        </asp:UpdatePanel>--%>
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

        function validateform() {
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
        }
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
</asp:Content>

