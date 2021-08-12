<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FrmScrapLoad.aspx.cs" Inherits="FrmScrapLoad" %>

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

    <div class="row mt-20">

        <div class="shipping-checkout-page">
            <div class="row">
                <div class="col s12">
                    <div class="section-title">
                        <p><span style="color: #101010">SCRAP</span> LOAD</p>
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
                    <div class="row" style="text-align: right">


                        <button type="button" class="btn theme-btn-rounded" data-toggle="modal" data-target="#basicExampleModal_New">
                            Add Truck
                        </button>

                        <%--runat="server" visible="false"--%>
                        <button type="button" runat="server" visible="false" class="btn theme-btn-rounded" data-toggle="modal" data-target="#basicExampleModal1">
                            RESUME
                        </button>

                        <spam style="font-size: large; font-weight: 200;">TOTAL SCRAP BAGS</spam>
                        : -    <strong>
                            <label id="LblTotalScrapBags" runat="server" style="font-size: large; font-weight: 200; color: red">0</label>
                            &nbsp;&nbsp;&nbsp;
                           <spam style="font-size: large; font-weight: 200;">TOTAL SCRAP WEIGHT (In Kg)</spam>
                            :-
                         <label id="LblTotalScrapWeight" runat="server" style="font-size: large; font-weight: 200; color: red">0</label>
                        </strong>

                    </div>

                    <label id="LblRefNo" runat="server" style="font-size: large; font-weight: 200;" visible="false"></label>
                    <div class="row" style="padding-top: 5px;">

                        <fieldset>
                            <legend>SCRAP BAGS DETAILS</legend>

                            <div class="input-field col s6 m12 l2">
                                <div class="col s12 m12 l12">
                                    <asp:DropDownList runat="server" ID="ddlTruckNo" AutoPostBack="true" class="form-control select2" OnSelectedIndexChanged="ddlTruckNo_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>


                            <div class="input-field col s6 m12 l2">
                                <div class="col s12 m12 l12">
                                    <asp:DropDownList runat="server" ID="ddlScrapCategory" AutoPostBack="true" class="form-control select2" Visible="True" OnSelectedIndexChanged="ddlScrapCategory_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="input-field col s6 m12 l2">
                                <div class="col s12 m12 l12">
                                    <asp:DropDownList runat="server" ID="ddlScrapType" AutoPostBack="true" class="form-control select2" Visible="True" OnSelectedIndexChanged="ddlScrapType_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="input-field col s6 m12 l2">
                                <div class="col s12 m12 l12">
                                    <asp:DropDownList runat="server" ID="ddlBu" AutoPostBack="false" class="form-control select2" Visible="True">
                                        <asp:ListItem Value="0">SELECT BU</asp:ListItem>
                                        <asp:ListItem Value="1">BABY CARE</asp:ListItem>
                                        <asp:ListItem Value="2">FEM CARE</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="input-field col s6 m12 l1">
                                <asp:TextBox ID="TxtBagNo" class="validate" runat="server" MaxLength="100" ReadOnly="true"></asp:TextBox>
                                <label id="BagNo" runat="server">BAG NO<span style="color: red;">*</span></label>
                                <asp:DropDownList runat="server" ID="ddlDepartment" AutoPostBack="true" class="form-control select2" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" Visible="false"></asp:DropDownList>
                            </div>
                            <div class="input-field col s6 m12 l3">
                                <asp:TextBox ID="TxtRefrenceNo" class="validate" runat="server" MaxLength="100" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="TxtBagBatchNo" class="validate" runat="server" MaxLength="100" ReadOnly="true"></asp:TextBox>
                                <label id="BagBatchNo" runat="server">BAG BATCH NO<span style="color: red;">*</span></label>
                            </div>
                            <div class="input-field col s6 m12 l2">
                                <asp:TextBox ID="TxtBagWeight" class="validate" runat="server" MaxLength="50" AutoPostBack="false" onkeypress="return validateDec(this,event)" AutoComplete="off"></asp:TextBox>
                                <label id="BagWeight" runat="server" class="txt">BAG WEIGHT(In Kg)<span style="color: red;">*</span></label>
                            </div>


                            <div class="input-field col s6 m12 l2">
                                <asp:TextBox ID="TxtTareWeight" class="validate" runat="server" MaxLength="50" AutoPostBack="false" onkeypress="return validateDec(this,event)" AutoComplete="off"></asp:TextBox>
                                <label id="LblTareWeight" runat="server" class="txt">Tare Weight(In Gm)<span style="color: red;">*</span></label>
                            </div>
                            <div class="input-field col s6 m12 l2">
                            </div>

                            <asp:Panel runat="server" ID="ScanPanel" Visible="True">
                                <label id="LblBarcode" runat="server" class="txt" visible="false">Scan QR Code<span style="color: red;">*</span></label>

                                <%-- <label class=qrcode-text-bt <input type=file   accept="image/*"  capture=environment  tabindex=-1>--%>

                                <asp:TextBox ID="TxtBarcode" Visible="false" class="qrcode-text" runat="server" MaxLength="100" AutoPostBack="false" AutoComplete="off"></asp:TextBox>
                                <label class="qrcode-text-btn" visible="false" runat="server" id="lblqr">
                                    <input type="file" accept="image/*" capture="environment" tabindex="-1" onchange="openQRCamera(this);" onclick="return showQRIntro();" /></label>

                                <asp:Button ID="BtnGetWeigh" runat="server" class="btn theme-btn-rounded" Text="GET" OnClick="BtnGetWeigh_Click" Visible="false" />
                                <asp:Button ID="Button1" runat="server" class="btn theme-btn-rounded" Text="GET WEIGHT" OnClick="Button1_Click" />
                                <asp:Button ID="Btn_Add" runat="server" class="btn theme-btn-rounded" Text="ADD" OnClick="Btn_Add_Click" />

                            </asp:Panel>

                        </fieldset>
                    </div>



                    <div class="row" style="padding-top: 5px;">

                        <div class="input-field col s6 m12 l3">
                        </div>


                    </div>

                    <div class="mt-20">
                        <div class="table-responsive">

                            <asp:GridView ID="GridScrap" runat="server" class="table table-bordered table-striped" AutoGenerateColumns="False"
                                AllowPaging="True" DataKeyNames="Id" PageSize="100" OnRowCommand="GridScrap_RowCommand" OnRowDeleting="GridScrap_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%#Bind("Scrap_Id") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Bag_No" HeaderText="Bag No" />
                                    <asp:BoundField DataField="Truck_No" HeaderText="Truck No" />
                                    <asp:BoundField DataField="Bag_BatchNo" HeaderText="Bag Batch No" />
                                    <asp:BoundField DataField="Bag_Weight" HeaderText="Bag Weight (In Kg)" />

                                    <asp:BoundField DataField="LoadTierWeight" HeaderText="Tare Weight (In Gm)" />
                                    <asp:BoundField DataField="LoadNetWeight" HeaderText="Net Weight (In Kg)" />
                                    <asp:BoundField DataField="Barcode" HeaderText="Barcode" />
                                    <asp:BoundField DataField="Scrap_TypeName" HeaderText="Scrap Type" />
                                    <asp:BoundField DataField="BU" HeaderText="BU" />
                                    <asp:TemplateField HeaderText="Delete" ShowHeader="False" ItemStyle-Width="60">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LnkDelete" runat="server" CausesValidation="False" CommandName="Delete" Text="DELETE" CssClass="btn btn-block btn-info btn-xs" OnClick="LnkDelete_Click" OnClientClick="return confirm('Are you sure you want to delete this record ?')" CommandArgument='<%# Bind("Id") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                     
                    <div class="row">

                        <div class="input-field col s12 m12 l12">

                            <asp:Button ID="btnCancel" runat="server" class="btn btn-cancel theme-btn-rounded" Text="CANCEL" OnClick="btnCancel_Click" />
                            <button type="button" class="btn theme-btn-rounded" data-toggle="modal" data-target="#basicExampleModal">
                                SUBMIT
                            </button>
                            <asp:Button ID="BtnDraft" runat="server" Enabled="false" class="btn theme-btn-rounded" Text="DRAFT" OnClick="BtnDraft_Click" Visible="false" />
                        </div>
                    </div>
                </asp:Panel>
            </div>

            <div class="modal fade" id="basicExampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
                aria-hidden="true">

                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLabel">ENTER TRUCK DETAILS</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="input-field col s6 m12 l6">
                                <asp:TextBox ID="TxtTruckNo" class="validate" runat="server" MaxLength="10" AutoPostBack="false" AutoComplete="off" onkeypress="return validatename(event);"></asp:TextBox>
                                <label id="TruckNo" runat="server">ENTER TRUCK NO<span style="color: red;">*</span></label>
                            </div>
                            <div class="input-field col s6 m12 l6" runat="server" id="TruckWeigh">
                                <asp:TextBox ID="TxtInitailWeight" class="validate" runat="server" MaxLength="50" AutoPostBack="false" onkeypress="return validateDec(this,event)" AutoComplete="off" Text="0"></asp:TextBox>
                                <label id="InitailWeight" runat="server">ENTER TRUCK INITIAL WEIGHT<span style="color: red;">*</span></label>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-cancel theme-btn-rounded" data-dismiss="modal">Close</button>
                            <%-- <button type="button" class="btn btn-primary">Save changes</button>--%>
                            <asp:Button ID="btnSubmit" runat="server" class="btn theme-btn-rounded" Text="READY TO SEND" OnClick="btnSubmit_Click" Visible="false" />
                        </div>
                    </div>
                </div>
            </div>


            <div class="modal fade" id="basicExampleModal1" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel1"
                aria-hidden="true">

                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLabel1">PREVIOUS LOAD DETAILS</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="table-responsive">

                            <asp:GridView ID="GridDraft" runat="server" class="table table-bordered table-striped" AutoGenerateColumns="False"
                                AllowPaging="True" DataKeyNames="Create_Date" PageSize="100" OnRowCommand="GridDraft_RowCommand" OnRowDeleting="GridDraft_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%#Bind("Scrap_Id") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Refrence_No" HeaderText="Consigment No" />
                                    <asp:BoundField DataField="Total_Bags" HeaderText="Total Bags" />

                                    <%-- <asp:TemplateField HeaderText="Delete" ShowHeader="False" ItemStyle-Width="60">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkDelete" runat="server" CausesValidation="False" CommandName="Delete" Text="DELETE" CssClass="btn btn-block btn-info btn-xs" OnClick="LnkDelete_Click" CommandArgument='<%# Bind("Scrap_id") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Resume" ShowHeader="False" ItemStyle-Width="60">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LnkResume" runat="server" CausesValidation="False" CommandName="Resume" Text="RESUME" CssClass="btn btn-block btn-info btn-xs" OnClick="LnkResume_Click" CommandArgument='<%# Bind("Scrap_id") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>

                    </div>
                </div>
            </div>




            <div class="modal fade" id="basicExampleModal_New" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
                aria-hidden="true">

                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLabel_New">ENTER TRUCK DETAILS</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="input-field col s6 m12 l6">
                                <asp:TextBox ID="txtTno" class="validate" runat="server" MaxLength="10" AutoPostBack="false" AutoComplete="off" onkeypress="return validatename(event);"></asp:TextBox>
                                <label id="Label1" runat="server">ENTER TRUCK NO<span style="color: red;">*</span></label>
                            </div>
                            <div class="input-field col s6 m12 l6" runat="server" id="Div1">
                                <asp:TextBox ID="txtTIW" class="validate" runat="server" MaxLength="50" AutoPostBack="false" onkeypress="return validateDec(this,event)" AutoComplete="off" Text="0"></asp:TextBox>
                                <label id="Label2" runat="server">ENTER TRUCK INITIAL WEIGHT<span style="color: red;">*</span></label>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-cancel theme-btn-rounded" data-dismiss="modal">Close</button> 
                            <asp:Button ID="btnAddTruck" runat="server" class="btn theme-btn-rounded" Text="Add Truck" OnClick="btnAddTruck_Click" />
                        </div>
                    </div>
                </div>
            </div>


        </div>



        <script type="text/javascript">

            function showModal_New() {
                $("#basicExampleModal_New").modal('show');
            }

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

