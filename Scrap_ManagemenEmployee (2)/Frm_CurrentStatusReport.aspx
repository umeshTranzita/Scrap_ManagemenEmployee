<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Frm_CurrentStatusReport.aspx.cs" Inherits="Frm_CurrentStatusReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">



    <script src="js/bootstrap-multiselect.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server" ID="script1"></asp:ScriptManager>
    <div class="shipping-checkout-page">
        <div class="row">
            <div class="col s12">
                <div class="section-title">
                    <p><span style="color: #101010">CURRENT </span>&nbsp;STATUS REPORT</p>
                </div>
            </div>
        </div>
        <%--<div class="row">
            <div class="input-field" style="padding: 30px 0;">
                <div class="col s6 m6">
                    <select class="form-control select2">
                        <option selected="selected" value="0">Select</option>
                        <option value="7">Employee</option>
                        <option value="8">Distributor</option>
                        <option value="9">Sub Distributor</option>
                        <option value="10">Booth</option>
                        <option value="11">Producer</option>
                        <option value="12">Customer</option>
                        <option value="13">MCU User</option>
                    </select>
                </div>
                <div class="col s6 m6">
                    <select class="form-control selectpicker" multiple data-live-search="true">
                        <option>Mustard</option>
                        <option>Ketchup</option>
                        <option>Relish</option>
                    </select>
                </div>
            </div>
        </div>--%>
        <div class="row" style="padding-top: 10px;">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </div>
        <div class="row" style="padding-top: 10px;">
            <div class="input-field col s6 m12 l2">
                <div class="col s12 m12 l12">
                    <asp:DropDownList runat="server" ID="ddlSite" AutoPostBack="true" class="form-control select2" Visible="True" OnSelectedIndexChanged="ddlSite_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>
            <div class="input-field col s6 m12 l1">
                <div class="col s12 m12 l12">
                    <asp:DropDownList runat="server" ID="ddlOrder" AutoPostBack="True" class="form-control select2" Visible="True" OnSelectedIndexChanged="ddlOrder_SelectedIndexChanged1">
                          <asp:ListItem Value="0">Desc</asp:ListItem>
                        <asp:ListItem Value="1">Asc</asp:ListItem>
                         
                    </asp:DropDownList>
                </div>
            </div>
            <div class="input-field col s6 m12 l2">
                <div class="col s12 m12 l12">
                    <asp:DropDownList runat="server" ID="ddlFilter" AutoPostBack="true" class="form-control select2" Visible="True" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>
             
            <div class="input-field col s6 m12 l2" hidden>
                <div class="col s12 m12 l12">
                    <asp:DropDownList runat="server" ID="ddlStatus" AutoPostBack="true" Visible="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" SeletionMode="Multiple">
                        <asp:ListItem Value="0">SELECT ALL</asp:ListItem>
                        <asp:ListItem Value="1">READY TO SEND</asp:ListItem>
                        <asp:ListItem Value="2">DISPATCHED</asp:ListItem>
                        <asp:ListItem Value="3">SEND TO APPROVAL</asp:ListItem>
                        <asp:ListItem Value="4">RECEIVED</asp:ListItem>
                        <%-- <asp:ListItem Value="5">RECEIVE WITH EXCEPTION</asp:ListItem>
                        <asp:ListItem Value="6">ISSUED PARTIALLY</asp:ListItem>--%>
                        <asp:ListItem Value="7">ISSUED</asp:ListItem>


                    </asp:DropDownList>
                </div>
            </div>





               <div class="col s12 m12 l2">
                   
                <asp:ListBox ID="lstEmployee" runat="server" SelectionMode="Multiple" ClientIDMode="Static" CssClass="form-control" OnSelectedIndexChanged="lstEmployee_SelectedIndexChanged" AutoPostBack="true">

                   
                       <%--<asp:ListItem Value="0">SELECT ALL</asp:ListItem>--%>
                        <asp:ListItem Value="1">READY TO SEND</asp:ListItem>
                        <asp:ListItem Value="2">DISPATCHED</asp:ListItem>
                        <asp:ListItem Value="3">SEND TO APPROVAL</asp:ListItem>
                        <asp:ListItem Value="4">RECEIVED</asp:ListItem>
                        <%-- <asp:ListItem Value="5">RECEIVE WITH EXCEPTION</asp:ListItem>
                        <asp:ListItem Value="6">ISSUED PARTIALLY</asp:ListItem>--%>
                        <asp:ListItem Value="7">ISSUED</asp:ListItem>               
                </asp:ListBox>
                      
            </div>





            <asp:Panel runat="server" ID="CustomPanel" Visible="false">
                <div class="input-field col s6 m12 l2">
                    <asp:TextBox ID="TxtFromdate" class="form-control datepicker" runat="server" MaxLength="100" AutoComplete="off"></asp:TextBox>
                    <label id="Fromdate" runat="server">FROM DATE<span style="color: red;">*</span></label>
                </div>
                <div class="input-field col s6 m12 l2">
                    <asp:TextBox ID="TxtTodate" class="form-control datepicker" runat="server" MaxLength="50" AutoPostBack="false" AutoComplete="off"></asp:TextBox>
                    <label id="Todate" runat="server" class="txt">TO DATE<span style="color: red;">*</span></label>
                </div>
                <div class="input-field col s6 m12 l2">
                    <asp:Button ID="btnSubmit" runat="server" class="btn theme-btn-rounded" Text="SEARCH" OnClick="btnSubmit_Click" OnClientClick="return validateform();" />
                </div>

            </asp:Panel>
            <div class="input-field col s6 m12 l1">


                <asp:LinkButton runat="server" class="btn theme-btn-rounded" Text="<i class='fa fa-download'></i>&nbsp;&nbsp;Export" ID="btnExport" OnClick="btnExport_Click"></asp:LinkButton>

            </div>
        </div>

        <div class="mt-20">

            <div class="table-responsive">
                <asp:GridView ID="GridStatus" runat="server" class="table table-striped table-bordered" AutoGenerateColumns="False"
                    AllowPaging="True" DataKeyNames="Scrap_Id" PageSize="200" OnRowCommand="GridStatus_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%#Bind("Scrap_Id") %>' />
                                 <asp:Label ID="LblRefrenceNo"  runat="server" Text='<%# Eval("ConsigmentNo") %>' Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>

                      

                         <asp:TemplateField HeaderText="Consigment No" ShowHeader="False" ItemStyle-Width="60">
                            <ItemTemplate>                     
                               <asp:Label runat="server" ID="LblConsigment" Text='<%# Eval("ConsigmentNo") %>'></asp:Label>                     
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Status" HeaderText="Current Status" />
                        <asp:BoundField DataField="CreateDate" HeaderText="Load On" />
                          <asp:TemplateField HeaderText="Load Weight" ShowHeader="False" ItemStyle-Width="60">
                            <ItemTemplate>
                          
                               <asp:Label runat="server" ID="LblTotalLoadWeight"></asp:Label>
                                 <asp:Label runat="server" ID="LblSiteId" Visible="false"></asp:Label>
                           
                            </ItemTemplate>
                        </asp:TemplateField>
                         
                        <asp:BoundField DataField="DispatchedDate" HeaderText="Dispatched On" />
                        <asp:BoundField DataField="ReceiveDate" HeaderText="Received On" />
                         <asp:TemplateField HeaderText="Receive Weight" ShowHeader="False" ItemStyle-Width="60">
                            <ItemTemplate>
                          
                               <asp:Label runat="server" ID="LblTotalReceiveWeight"></asp:Label>
                           
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="IssueDate" HeaderText="Issued On" />
                        <asp:BoundField DataField="PostProcessingDate" HeaderText="Post Proceed On" />
                         <asp:TemplateField HeaderText="Action" ShowHeader="False" ItemStyle-Width="60">
                            <ItemTemplate>
                          
                                <asp:LinkButton runat="server" ID="LnkModal"  Text="SHOW" class="btn btn-sm" CommandName="Dispatch" CommandArgument='<%# Bind("ConsigmentNo") %>'></asp:LinkButton>
                           
                            </ItemTemplate>
                        </asp:TemplateField>



                    </Columns>
                    <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                </asp:GridView>

            </div>
        </div>
           <div class="modal fade" id="basicExampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
            aria-hidden="true" >

            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">SCRAP LOAD BAG DETAILS</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridView1" runat="server" class="table table-striped table-bordered" AutoGenerateColumns="False"
                    AllowPaging="True" DataKeyNames="Refrence_No" PageSize="200" style="width:100%;">
                    <Columns>
                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%#Bind("Refrence_No") %>' />
                                
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Refrence_No" HeaderText="Consigment No" />
                        <asp:BoundField DataField="Bag_BatchNo" HeaderText="Bag Batch No" />
                      <%--  <asp:BoundField DataField="Bag_No" HeaderText="Bag No" />--%>
                         <asp:BoundField DataField="Bag_Weight" HeaderText="Weight" />
                      <asp:BoundField DataField="LoadTierWeight" HeaderText="Load Tare Weight" />
                        <asp:BoundField DataField="LoadNetWeight" HeaderText="Load Net Weight" />
                        <asp:BoundField DataField="Barcode" HeaderText="QR Code" />



                    </Columns>
                    <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                </asp:GridView>
                    </div>
                    <div class="modal-footer">
                        <span style="color: red; font-weight: 300"><strong>
                            <asp:Label runat="server" ClientIDMode="Static" ID="LblErrorMsg"></asp:Label></strong></span>

                        <button type="button" class="btn btn-cancel theme-btn-rounded" data-dismiss="modal">Close</button>
                      
                  
                    </div>
                </div>
            </div>
        </div>
 <script>

        $(function () {
            $('[id*=lstEmployee]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });


        });
    </script>
    </div>
    <link href="css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="js/bootstrap-multiselect.js"></script>

   
    <%--<script type="text/javascript">
        $(function () {
            $('[id*=lstEmployee]').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>--%>
    <style>
        .multiselect-native-select .multiselect {
            text-align: left !important;
        }

        .multiselect-native-select .multiselect-selected-text {
            width: 100% !important;

        }

        .multiselect-native-select .checkbox, .multiselect-native-select .dropdown-menu {
            width: 100% !important;
        }

        .multiselect-native-select .btn .caret {
            float: right !important;
            vertical-align: middle !important;
            margin-top: 8px;
            /*border-top: 6px dashed;*/
        }
    </style>
    <style>
        [type="checkbox"]:not(:checked), [type="checkbox"]:checked {
            position: absolute;
            opacity: 9;
            pointer-events: all;
        }
    </style>
    <script>
        function validateform() {
            var msg = "";

            if (document.getElementById('<%=TxtFromdate.ClientID%>').value.trim() == "") {
                msg += "Enter From Date. \n";
            }
            if (document.getElementById('<%=TxtTodate.ClientID%>').value.trim() == "") {
                msg += "Enter To Date. \n";
            }
            <%--if (document.getElementById('<%=TxtTodate.ClientID%>').value.trim() < document.getElementById('<%=TxtFromdate.ClientID%>').value.trim()) {
                msg += "To Date Never be lesser than from date . \n";
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
</asp:Content>

