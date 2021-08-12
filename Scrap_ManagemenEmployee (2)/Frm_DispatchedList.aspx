<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Frm_DispatchedList.aspx.cs" Inherits="Frm_DispatchedList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="css/select2.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server" ID="script1"></asp:ScriptManager>
    <div class="shipping-checkout-page">
        <div class="row">
            <div class="col s12">
                <div class="section-title">
                    <p><span style="color: #101010">SCRAP </span>&nbsp;DISPATCHED LIST</p>
                </div>
            </div>
        </div>
       <div class="row" style="padding-top:10px;">
                    <div class="col s12">
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                      
                    </div>
                </div>
        <div class="mt-20">
            <div class="table-responsive">
                <asp:GridView ID="GridDispatch" runat="server" class="table table-striped table-bordered" AutoGenerateColumns="False"
                    AllowPaging="True" DataKeyNames="Scrap_Id" PageSize="100">
                    <Columns>
                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%#Bind("Scrap_Id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                        <asp:BoundField DataField="Refrence_No" HeaderText="Refrence No" />
                          <asp:BoundField DataField="Create_Date" HeaderText="Load On" />
                          <asp:BoundField DataField="Dispatched_Date" HeaderText="Dispatch On" />
                        
                        <asp:BoundField DataField="Total_Bags" HeaderText="Scrap Bags" />
                        <asp:BoundField DataField="Total_Weight" HeaderText="Bags Weight (In Kg)" />
                        <asp:BoundField DataField="Initial_TruckWeight" HeaderText="Initial Weight (In Kg)" />
                       
                        <asp:BoundField DataField="Final_Weight" HeaderText="Final Weight (In Kg)" />
                        <asp:BoundField DataField="Lock_No" HeaderText="Lock No" />        
                        <asp:BoundField DataField="Truck_No" HeaderText="Truck No" />
                       

                        <asp:TemplateField HeaderText="PRINT" ShowHeader="False" ItemStyle-Width="60">
                            <ItemTemplate>
                              
                                <asp:LinkButton runat="server" ID="LnkPrint" OnClick="LnkPrint_Click" Text="PRINT" class="btn btn-sm" CommandName="Print" CommandArgument='<%# Bind("Scrap_Id") %>'></asp:LinkButton>
                               
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="BAG DETAILS" ShowHeader="False" ItemStyle-Width="60">
                            <ItemTemplate>
                              
                                <asp:LinkButton runat="server" ID="LnkPrint1" Onclick="LnkPrint1_Click" Text="BAGS PRINT" class="btn btn-sm" CommandName="Print1" CommandArgument='<%# Bind("Scrap_Id") %>'></asp:LinkButton>
                               
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <%--<asp:UpdatePanel runat="server" ID="panel1">
            <ContentTemplate>--%>
            <div class="modal fade" id="basicExampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
                aria-hidden="true">

                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLabel">ENTER DISPATCH DETAILS</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="input-field col s6 m12 l6">
                                <asp:TextBox ID="TxtTruckNo" class="validate" runat="server" MaxLength="50" AutoPostBack="false" AutoComplete="off" ReadOnly="true"></asp:TextBox>
                                <label id="TruckNo" runat="server">TRUCK NO<span style="color: red;">*</span></label>
                            </div>
                              <div class="input-field col s6 m12 l6">
                                <asp:TextBox ID="TxtScrapWeight" class="validate" runat="server" MaxLength="50" AutoPostBack="false" onkeypress="return validateDec(this,event)" AutoComplete="off" ReadOnly="true"></asp:TextBox>
                                <label id="ScrapWeight" runat="server">TOTAL SCRAP WEIGHT<span style="color: red;">*</span></label>
                            </div>
                            <div class="input-field col s6 m12 l6">
                                <asp:TextBox ID="TxtInitailWeight" class="validate" runat="server" MaxLength="50" AutoPostBack="false" onkeypress="return validateDec(this,event)" AutoComplete="off" ReadOnly="true"></asp:TextBox>
                                <label id="InitailWeight" runat="server">TRUCK INITIAL WEIGHT<span style="color: red;">*</span></label>
                            </div>
                             <div class="input-field col s6 m12 l6">
                                <asp:TextBox ID="TxtTolerance" class="validate" runat="server" MaxLength="50" AutoPostBack="false" onkeypress="return validateDec(this,event)" AutoComplete="off" ReadOnly="true"></asp:TextBox>
                                <label id="Tolerance" runat="server">TRUCK WEIGHT TOLERANCE<span style="color: red;">*</span></label>
                            </div>
                            <div class="input-field col s6 m12 l6">
                                <asp:TextBox ID="TxtFinalWeight" class="validate" runat="server" MaxLength="50" AutoPostBack="false" onkeypress="return validateDec(this,event)" AutoComplete="off" onfocusout="Calculatsum();"></asp:TextBox>
                                <label id="FinalWeight" runat="server">ENTER TRUCK FINAL WEIGHT<span style="color: red;">*</span></label>
                            </div>
                           
                            <div class="input-field col s6 m12 l6">
                                <asp:TextBox ID="TxtEnterLockNo" class="validate" runat="server" MaxLength="50" AutoPostBack="false" onkeypress="return validateDec(this,event)" AutoComplete="off" ClientIDMode="Static"></asp:TextBox>
                                <label id="LockNo" runat="server">ENTER LOCK NO<span style="color: red;">*</span></label>
                            </div>
                            <div class="input-field col s6 m12 l6">
                                <asp:TextBox ID="TxtREEnterLockNo" class="validate" runat="server" MaxLength="50" AutoPostBack="false" onkeypress="return validateDec(this,event)" AutoComplete="off"></asp:TextBox>
                                <label id="ReEnterLock" runat="server">RE-ENTER LOCK NO<span style="color: red;">*</span></label>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <span style="color:red;font-weight:300"><strong><asp:Label runat="server" ClientIDMode="Static" id="LblErrorMsg"></asp:Label></strong></span>
                          
                            <button type="button" class="btn btn-cancel btn-sm" data-dismiss="modal">Close</button>
                            <%-- <button type="button" class="btn btn-primary">Save changes</button>--%>
                            <asp:Button ID="btnSubmit" runat="server" class="btn btn-sm" Text="DISPATCH" OnClientClick="validateform();" ClientIDMode="Static" />
                        </div>
                    </div>
                </div>
            </div>
              <%--  </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>

    <script>
        function enterToTab(e) {
            var intKey = window.Event ? e.which : e.KeyCode;

            if (intKey == 13) {
                e.keyCode = 9;
                return e;
            }
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
            var Tolerance = document.getElementById('<%=TxtTolerance.ClientID%>').value.trim();
            var Lock = document.getElementById('<%=TxtEnterLockNo.ClientID%>').value.trim();




          


            var PlusWithTolerance = parseFloat(parseFloat(ScrapWeight) + parseFloat(InitialWeight) + parseFloat(Tolerance)).toFixed(0);
            var MinusWithTolerance = parseFloat((parseFloat(ScrapWeight) + parseFloat(InitialWeight)) - parseFloat(Tolerance)).toFixed(0);

            if (parseFloat(FinalWeight) > parseFloat(PlusWithTolerance)) {
                $("#LblErrorMsg").text("FINAL WEIGHT CANNOT EXCEED WITH INITIAL WEIGHT + TOLERANCE WEIGHT");
                $("#btnSubmit").hide();
            }
            if (parseFloat(FinalWeight) < parseFloat(MinusWithTolerance)) {
                $("#LblErrorMsg").text("FINAL WEIGHT CANNOT EXCEED WITH INITIAL WEIGHT - TOLERANCE WEIGHT");
                $("#btnSubmit").hide();
            }




            return false;
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
            if (document.getElementById('<%=TxtFinalWeight.ClientID%>').value.trim() == "") {
                msg += "Enter Final Weight. \n";
            }
            if (document.getElementById('<%=TxtEnterLockNo.ClientID%>').value.trim() == "") {
                msg += "Enter Lock No. \n";
            }
            if (document.getElementById('<%=TxtREEnterLockNo.ClientID%>').value.trim() == "") {
                msg += "Enter Re-Lock No. \n";
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
</asp:Content>

