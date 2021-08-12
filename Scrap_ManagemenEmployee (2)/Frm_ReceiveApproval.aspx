<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Frm_ReceiveApproval.aspx.cs" Inherits="Frm_ReceiveApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server" ID="script1"></asp:ScriptManager>
    <div class="shipping-checkout-page">
        <div class="row">
            <div class="col s12">
                <div class="section-title">
                    <p><span style="color: #101010">SCRAP </span>&nbsp;RECEIVE APPROVAL</p>
                </div>
            </div>
        </div>
        <div class="row" style="padding-top: 10px;">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </div>
        <div class="mt-20">
            <asp:Panel runat="server" ID="PanelReceive" Visible="true">
                <div class="row">

                    <div class="input-field col s6 m12 l6">
                        <div class="col s12 m12 l7">
                            <asp:DropDownList runat="server" ID="ddlConsigment" AutoPostBack="true" class="form-control select2" OnSelectedIndexChanged="ddlConsigment_SelectedIndexChanged"></asp:DropDownList>
                            <%--<label>Select Category<span style="color: red;">*</span></label>--%>
                        </div>
                    </div>


                </div>
                <div class="table-responsive" style="padding-top: 10px;">
                    <asp:GridView ID="GridReceive" runat="server" class="table table-striped table-bordered" AutoGenerateColumns="False"
                        AllowPaging="True" DataKeyNames="Scrap_Id" PageSize="100" OnRowDataBound="GridReceive_RowDataBound" ShowFooter="true">
                        <Columns>
                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="1%" ItemStyle-CssClass="text-left">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%#Bind("Scrap_Id") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Consigment No" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblRefrenceNo" Text='<%# Eval("Refrence_No") %>'></asp:Label>
                                    <asp:Label runat="server" ID="LblSiteId" Text='<%# Eval("Site_Id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bag BatchNo" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblBag_BatchNo" Text='<%# Eval("Bag_BatchNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                               <asp:TemplateField HeaderText="Actual Weight" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblLoadNetWeight" Text='<%# Eval("LoadNetWeight") %>'></asp:Label>
                                    <asp:Label runat="server" ID="LblPreviousReceiveWt" Text='<%# Eval("Receive_Weight") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="NewBagWeight" HeaderText="Bag Weight" Visible="false" />
                            <asp:TemplateField HeaderText="Receive Weight" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:TextBox ID="TxtReceiveWeight" runat="server" Text='<%# Eval("Receive_Weight") %>' onkeypress="return validateDec(this,event)" AutoPostBack="true" AutoComplete="off" OnTextChanged="TxtReceiveWeight_TextChanged"></asp:TextBox>
                                    <asp:TextBox ID="TxtBagWeight" runat="server" Text='<%# Eval("Bag_Weight") %>' Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="TxtBagToleranceWt" runat="server" Text='<%# Eval("Tolerance_Receivebag") %>' Visible="false"></asp:TextBox>
                                    <asp:Label runat="server" ID="LblStatus" Text='<%# Eval("Status") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Tier Weight (In gm)" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:TextBox ID="TxtTierWeigh" runat="server" Text='<%# Eval("TierWeight") %>' onkeypress="return validateDec(this,event)" AutoPostBack="true" AutoComplete="off" OnTextChanged="TxtReceiveWeight_TextChanged"></asp:TextBox>                                
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Net Weight" ShowHeader="False">
                                <ItemTemplate>
                                    
                                     <asp:Label runat="server" ID="LblNetWeight" Text='<%# Eval("NetWeight") %>' Visible="True"></asp:Label>
                                 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Receive Comment" ItemStyle-Width="25%" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="TxtReceiveComment" Text='<%# Eval("Receive_ApprovalComment") %>' MaxLength="50" placeholder="ENTER RECEIVE COMMENT"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblBagStatus" Text='<%# Eval("Receive_Status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Post Action Comment" ItemStyle-Width="25%" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="TxtPostComment" Text='<%# Eval("Post_ActionComment") %>' MaxLength="50" placeholder="ENTER POST COMMENT"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField ItemStyle-Width="50" HeaderText="Exception Ok">
                             
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="LblScrapType" Text='<%# Eval("Scrap_Type") %>' Visible="false"></asp:Label>
                                    <asp:CheckBox ID="chkIsActive" runat="server" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="chkIsActive_CheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>

                <div class="row">

                    <div class="input-field col s12 m12 l12">

                        <asp:Label runat="server" ID="LblOverAllStatus" Text='' Visible="false"></asp:Label>
                         <div class="col s12 m12 l3">
                        <asp:Button ID="BtnReceive" runat="server" class="btn theme-btn-rounded" Text="RECEIVE" Visible="false" OnClick="BtnReceive_Click" />
                              <asp:Button ID="BtnPostAction" runat="server" class="btn theme-btn-rounded" Text="POST ACTION TAKEN" Visible="false" OnClick="BtnPostAction_Click"/>
                             </div>
                        <div class="col s12 m12 l7">
                            <asp:TextBox runat="server" ID="TxtPostActionComment" MaxLength="30" Visible="false" placeholder="ENTER POST ACTION COMMENT"></asp:TextBox>
                        </div>
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
                        <asp:Button ID="btnSubmit" runat="server" class="btn theme-btn-rounded" Text="SHOW DETAILS" OnClientClick="return validateform();" ClientIDMode="Static" OnClick="btnSubmit_Click"  />
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

      
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want OK with  Bag weight or Adjust a Weight?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
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
            <%--if (document.getElementById('<%=TxtLockNo.ClientID%>').value.trim() == "") {
                msg += "Enter Lock No. \n";
            }--%>

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

