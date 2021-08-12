<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Frm_LoadedDetails.aspx.cs" Inherits="Frm_LoadedDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="css/select2.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server" ID="script1"></asp:ScriptManager>
    <div class="shipping-checkout-page">
        <div class="row">
            <div class="col s12">
                <div class="section-title">
                    <p><span style="color: #101010">SCRAP </span>&nbsp;LOADED LIST</p>
                </div>
            </div>
        </div>
        <div class="row" style="padding-top: 10px;">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            <asp:HiddenField ID="HdnTruck" runat="server" />
            <%--<div class="field col s4 m12 6" style="padding-top:20px;">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <asp:DropDownList runat="server" ID="ddlDoctor" class="form-control select2" ClientIDMode="Static" ></asp:DropDownList>
            </div>

            <div class="input-field col s2 m12 6 ">
                <asp:Button ID="btnSubmit" runat="server" class="btn btn-sm" Text="SEARCH" OnClick="btnSubmit_Click" />
            </div>--%>
        </div>
        <div class="mt-20">
            <div class="table-responsive">
                <asp:GridView ID="GridLoad" runat="server" class="table table-striped table-bordered" AutoGenerateColumns="False"
                    AllowPaging="True" OnPageIndexChanging="GridLoad_PageIndexChanging" DataKeyNames="Scrap_Id" PageSize="100" OnRowCommand="GridLoad_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%#Bind("Scrap_Id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CreateDate" HeaderText="Scrap Load On" />
                        <asp:TemplateField HeaderText="Refrence No">
                            <ItemTemplate>
                                <asp:Label ID="lblRefrenceNo" Text='<%# Eval("Refrence_No") %>' runat="server" />
                                <asp:Label ID="lblSMTrukGatePass_Id" Visible="false" Text='<%# Eval("SMTrukGatePass_Id") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Truck_No" HeaderText="Truck No." />
                        <asp:BoundField DataField="Total_Bags" HeaderText="Total Scrap Bags" />
                        <asp:BoundField DataField="Initial_TruckWeight" HeaderText="Initial Weight (In Kg)" />
                        <asp:TemplateField HeaderText="Category">
                            <ItemTemplate>
                                <asp:Label ID="lblCategory" Text='<%# Eval("ScrapCategory") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status" ShowHeader="False" ItemStyle-Width="120">
                            <ItemTemplate>

                                <asp:Label runat="server" ID="LblStatus" Text='<%# Eval("Status") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action" ShowHeader="False" ItemStyle-Width="60">
                            <ItemTemplate>
                                <%-- <button type="button" class="btn btn-sm" data-toggle="modal" data-target="#basicExampleModal">
                                    DISPATCH
                                </button>--%>
                                <asp:LinkButton runat="server" ID="LnkModal" OnClick="LnkModal_Click" Text="DISPATCH" class="btn btn-sm" CommandName="Dispatch" CommandArgument='<%# Bind("Scrap_Id") %>'></asp:LinkButton>
                                <%--<asp:LinkButton ID="LnkDispatch" runat="server" CausesValidation="False" CommandName="Dispatch" Text="DISPATCH" CssClass="btn btn-info btn-xs" CommandArgument='<%# Bind("Scrap_Id") %>' OnClick="LnkDispatch_Click"></asp:LinkButton>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <div class="modal fade" id="basicExampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
            aria-hidden="true">
          <%--  <asp:UpdatePanel runat="server" ID="panel1">
                <ContentTemplate>--%>
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <asp:Label runat="server"></asp:Label>
                            <div class="modal-header">
                                
                                <h5 class="modal-title" id="exampleModalLabel">ENTER DISPATCH DETAILS</h5>
                                <asp:Label ID="LblModalMsg" runat="server" Text=""></asp:Label>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="input-field col s6 m12 l6">
                                    <asp:TextBox ID="TxtConsigment" class="validate" runat="server" MaxLength="50" AutoPostBack="false" AutoComplete="off" ReadOnly="true"></asp:TextBox>
                                    <label id="ConsigmentNo" runat="server">Consigment No<span style="color: red;">*</span></label>
                                </div>
                                <div class="input-field col s6 m12 l6">
                                    <asp:TextBox ID="TxtTruckNo" class="validate" runat="server" MaxLength="50" AutoPostBack="false" AutoComplete="off" ReadOnly="true"></asp:TextBox>
                                    <label id="TruckNo" runat="server">TRUCK NO<span style="color: red;">*</span></label>
                                </div>
                                <div class="input-field col s6 m12 l6">
                                    <asp:TextBox ID="TxtScrapWeight" class="validate" runat="server" MaxLength="50" AutoPostBack="false" onkeypress="return validateDec(this,event)" AutoComplete="off" ReadOnly="true"></asp:TextBox>
                                    <label id="ScrapWeight" runat="server">TOTAL SCRAP WEIGHT (In Kg)<span style="color: red;">*</span></label>
                                </div>
                                <div class="input-field col s6 m12 l6">
                                    <asp:TextBox ID="TxtInitailWeight" class="validate" runat="server" MaxLength="50" AutoPostBack="false" onkeypress="return validateDec(this,event)" AutoComplete="off" ReadOnly="true"></asp:TextBox>
                                    <label id="InitailWeight" runat="server">TRUCK INITIAL WEIGHT (In Kg)<span style="color: red;">*</span></label>
                                </div>
                                <div class="input-field col s6 m12 l6">
                                    <asp:TextBox ID="TxtTolerance" class="validate" runat="server" MaxLength="50" AutoPostBack="false" onkeypress="return validateDec(this,event)" AutoComplete="off" ReadOnly="true"></asp:TextBox>
                                    <label id="Tolerance" runat="server">TRUCK WEIGHT TOLERANCE (In Kg)<span style="color: red;">*</span></label>
                                </div>
                                <div class="input-field col s6 m12 l6">
                                    <asp:TextBox ID="TxtFinalWeight" class="validate" runat="server" MaxLength="50" AutoPostBack="false" onkeypress="return validateDec(this,event)" AutoComplete="off" onfocusout="Calculatsum();" Text="0"></asp:TextBox>
                                    <label id="FinalWeight" runat="server">ENTER TRUCK FINAL WEIGHT (In Kg)<span style="color: red;">*</span></label>
                                </div>
                                <div class="input-field col s6 m12 l12">
                                    <asp:DropDownList runat="server" ID="ddlConsignee" AutoPostBack="false" class="validate select2"  Visible="True"></asp:DropDownList>
                                    <label id="LblConsignee" runat="server">Consignee<span style="color: red;">*</span></label>
                                </div>
                                <div class="input-field col s6 m12 l6">
                                    <asp:TextBox ID="TxtEnterLockNo" class="validate" runat="server" MinLength="8" AutoPostBack="false" AutoComplete="off" ClientIDMode="Static" onfocusout="ChangeMode()" onkeypress="TextChange()"></asp:TextBox>
                                    <label id="LockNo" runat="server">ENTER LOCK NO<span style="color: red;">*</span></label>
                                </div>
                                <div class="input-field col s6 m12 l6">
                                    <asp:TextBox ID="TxtREEnterLockNo" class="validate" runat="server" MinLength="8" AutoPostBack="false" AutoComplete="off" onfocusout="ChangeMode1()" onkeypress="TextChange1()"></asp:TextBox>
                                    <label id="ReEnterLock" runat="server">RE-ENTER LOCK NO<span style="color: red;">*</span></label>
                                </div>

                            </div>
                            <div class="modal-footer">
                                <span style="color: red; font-weight: 300"><strong>
                                    <asp:Label runat="server" ClientIDMode="Static" ID="LblErrorMsg"></asp:Label></strong></span>

                                <button type="button" class="btn btn-cancel theme-btn-rounded" data-dismiss="modal">Close</button>
                                <%-- <button type="button" class="btn btn-primary">Save changes</button>--%>
                                <asp:Button ID="btnSubmit" runat="server" class="btn theme-btn-rounded" Text="DISPATCH" OnClick="btnSubmit_Click" OnClientClick="return validateform();" ClientIDMode="Static" />
                            </div>
                        </div>
                    </div>
               <%-- </ContentTemplate>
            </asp:UpdatePanel>--%>
        </div>

    </div>
    <%--<script src="js/jquery.min.js"></script>--%>

    <script>



        function enterToTab(e) {
            var intKey = window.Event ? e.which : e.KeyCode;

            if (intKey == 13) {
                e.keyCode = 9;
                return e;
            }
        }

        function ChangeMode() {
            document.getElementById('<%= TxtEnterLockNo.ClientID %>').type = 'password';
        }
        function ChangeMode1() {
            document.getElementById('<%= TxtREEnterLockNo.ClientID %>').type = 'password';
        }
        function TextChange() {
            document.getElementById('<%= TxtEnterLockNo.ClientID %>').type = 'text';
        }
        function TextChange1() {
            document.getElementById('<%= TxtREEnterLockNo.ClientID %>').type = 'text';
        }
        function Calculatsum() {
            debugger;

            <%--    document.getElementById('<%=txtFinalAmt.ClientID%>').value = "";
            document.getElementById('<%=txtBalance.ClientID%>').value = "";--%>
            $("#LblErrorMsg").text(" ");
            $("#btnSubmit").show();
            var ScrapWeight = document.getElementById('<%=TxtScrapWeight.ClientID%>').value.trim();
            var FinalWeight = document.getElementById('<%=TxtFinalWeight.ClientID%>').value.trim();
            var InitialWeight = document.getElementById('<%=TxtInitailWeight.ClientID%>').value.trim();

            var Lock = document.getElementById('<%=TxtEnterLockNo.ClientID%>').value.trim();


            debugger;
            if (document.getElementById('<%=HdnTruck.ClientID%>').value.trim() == "1") {

                var PlusWithTolerance = parseFloat(parseFloat(ScrapWeight) + parseFloat(InitialWeight)).toFixed(0);
                var MinusWithTolerance = parseFloat((parseFloat(ScrapWeight) + parseFloat(InitialWeight))).toFixed(0);

                if (parseFloat(FinalWeight) > parseFloat(PlusWithTolerance)) {
                    $("#LblErrorMsg").text("FINAL WEIGHT CANNOT EXCEED WITH SCRAP + INITIAL WEIGHT");
                    $("#btnSubmit").hide();
                }
                if (parseFloat(FinalWeight) < parseFloat(MinusWithTolerance)) {
                    $("#LblErrorMsg").text("FINAL WEIGHT CANNOT LESS THAN SCRAP + INITIAL WEIGHT");
                    $("#btnSubmit").hide();
                }
            }

            if (document.getElementById('<%=HdnTruck.ClientID%>').value.trim() == "2") {
                var Tolerance = document.getElementById('<%=TxtTolerance.ClientID%>').value.trim();
                var PlusWithTolerance = parseFloat(parseFloat(ScrapWeight) + parseFloat(InitialWeight) + parseFloat(Tolerance)).toFixed(0);
                var MinusWithTolerance = parseFloat((parseFloat(ScrapWeight) + parseFloat(InitialWeight)) - parseFloat(Tolerance)).toFixed(0);

                if (parseFloat(FinalWeight) > parseFloat(PlusWithTolerance)) {
                    $("#LblErrorMsg").text("FINAL WEIGHT CANNOT EXCEED WITH SCRAP + INITIAL + TOLERANCE WEIGHT");
                    $("#btnSubmit").hide();
                }
                if (parseFloat(FinalWeight) < parseFloat(MinusWithTolerance)) {
                    $("#LblErrorMsg").text("FINAL WEIGHT CANNOT LESS THAN SCRAP + INITIAL - TOLERANCE WEIGHT");
                    $("#btnSubmit").hide();
                }
            }



            return false;
        }

    </script>
    <script type="text/javascript">

        $(function () {
            debugger;
            $("#TxtEnterLockNo").blur(function () {
                document.getElementById('<%= TxtEnterLockNo.ClientID %>').type = 'password';
            });
       });

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
            debugger;
            var msg = "";
            if (document.getElementById('<%=TxtFinalWeight.ClientID%>').value.trim() == "") {
                msg += "Enter Final Weight. \n";
            }
            if (document.getElementById('<%=ddlConsignee.ClientID%>').selectedIndex == 0) {
            msg += "Please Select Consignee.\n";
        }
            if (document.getElementById('<%=TxtEnterLockNo.ClientID%>').value.trim() == "") {
                msg += "Enter Lock No. \n";
            }
            if (document.getElementById('<%=TxtREEnterLockNo.ClientID%>').value.trim() == "") {
                msg += "Enter Re-Lock No. \n";
            }
            if (document.getElementById('<%=TxtEnterLockNo.ClientID%>').value.trim() == document.getElementById('<%=TxtREEnterLockNo.ClientID%>').value.trim() == "") {

                msg += "Enter Lock No and Re-Lock No does not match. \n";

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
</asp:Content>


