<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Frm_PostProcessReport.aspx.cs" Inherits="Frm_PostProcessReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="css/select2.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server" ID="script1"></asp:ScriptManager>
    <div class="shipping-checkout-page">
        <div class="row">
            <div class="col s12">
                <div class="section-title">
                    <p><span style="color: #101010">POST </span>&nbsp;PROCESS REPORT</p>
                </div>
            </div>
        </div>
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
                    <asp:DropDownList runat="server" ID="ddlOrder" AutoPostBack="True" class="form-control select2" Visible="True" OnSelectedIndexChanged="ddlOrder_SelectedIndexChanged">
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
             <div class="col s12 m12 l2">
                   
                <asp:ListBox ID="lstEmployee" runat="server" SelectionMode="Multiple" ClientIDMode="Static" CssClass="form-control" OnSelectedIndexChanged="lstEmployee_SelectedIndexChanged" AutoPostBack="true">

                   
                    
                        <asp:ListItem Value="PROCESSED">PROCESSED</asp:ListItem>
                        <asp:ListItem Value="SOLD">SOLD</asp:ListItem>
                        <asp:ListItem Value="LOADED">LOADED</asp:ListItem>
                        <asp:ListItem Value="DISPATCHED">DISPATCHED</asp:ListItem>
                      
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
                    <asp:Button ID="btnSubmit" runat="server" class="btn theme-btn-rounded" Text="SEARCH" OnClientClick="return validateform();" OnClick="btnSubmit_Click" />
                </div>

            </asp:Panel>
            <div class="input-field col s6 m12 l1">


                <asp:LinkButton runat="server" class="btn theme-btn-rounded" Text="<i class='fa fa-download'></i>&nbsp;&nbsp;Export" ID="btnExport" OnClick="btnExport_Click"></asp:LinkButton>

            </div>
        </div>

        <div class="mt-20" style="width:100%;">

            <div class="table-responsive"  style="width:100%;">
                <asp:GridView ID="GridStatus" runat="server" class="table table-striped table-bordered" AutoGenerateColumns="False"
                    AllowPaging="True" DataKeyNames="PPId" PageSize="200" OnRowCommand="GridStatus_RowCommand"  style="width:100%;">
                    <Columns>
                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%#Bind("PPId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PPId" HeaderText="PPId" />
                        <asp:BoundField DataField="Consigment_No" HeaderText="Consigment No" />
                        <asp:BoundField DataField="BagNo" HeaderText="Total Bag" />
                        <asp:BoundField DataField="Weight" HeaderText="Total Weight" />
                        <asp:BoundField DataField="Status" HeaderText="Current Status" />
                        <asp:BoundField DataField="Create_Date" HeaderText="Proceed Date" />
                        <asp:BoundField DataField="SoldDate" HeaderText="SoldDate" />
                        <asp:BoundField DataField="LoadDate" HeaderText="LoadDate" />
                        <asp:BoundField DataField="DispatchDate" HeaderText="DispatchDate" />
                        <asp:TemplateField HeaderText="Action" ShowHeader="False" ItemStyle-Width="60">
                            <ItemTemplate>
                          
                                <asp:LinkButton runat="server" ID="LnkModal"  Text="SHOW" class="btn btn-sm" CommandName="Dispatch" CommandArgument='<%# Bind("Consigment_No") %>'></asp:LinkButton>
                           
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                    </Columns>
                    <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                </asp:GridView>

            </div>
        </div>
        <div class="mt-20">
            <div class="modal fade" id="basicExampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
            aria-hidden="true" >

            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">POST PROCESS BAG DETAILS</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="GridView1" runat="server" class="table table-striped table-bordered" AutoGenerateColumns="False"
                    AllowPaging="True" DataKeyNames="Consigment_No" PageSize="200" style="width:100%;">
                    <Columns>
                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%#Bind("Consigment_No") %>' />
                                
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Consigment_No" HeaderText="Consigment No" />
                        <asp:BoundField DataField="Bag_BatchNo" HeaderText="Bag Batch No" />
                        <asp:BoundField DataField="Bag_No" HeaderText="Bag No" />
                         <asp:BoundField DataField="Weight" HeaderText="Weight" />
                      <asp:BoundField DataField="Type" HeaderText="Type" />
                        <asp:BoundField DataField="ScrapType" HeaderText="Scrap Type" />
                         <asp:BoundField DataField="TierWeight" HeaderText="Tier Weight" />
                         <asp:BoundField DataField="NetWeight" HeaderText="Net Weight" />



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
        </div>

    </div>
    <script>
        function validateform() {
            var msg = "";

            if (document.getElementById('<%=TxtFromdate.ClientID%>').value.trim() == "") {
                msg += "Enter From Date. \n";
            }
            if (document.getElementById('<%=TxtTodate.ClientID%>').value.trim() == "") {
                msg += "Enter To Date. \n";
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

       

       //$(function () {
       //    $('[id*=lstEmployee]').multiselect({
       //        includeSelectAllOption: true,
       //        includeSelectAllOption: true,
       //        buttonWidth: '100%',

       //    });


       //});
    
    </script>
       <script type="text/javascript">
        $(function () {
            $('[id*=lstEmployee]').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>
     <style>
        [type="checkbox"]:not(:checked), [type="checkbox"]:checked {
            position: absolute;
            opacity: 9;
            pointer-events: all;
        }
    </style>
</asp:Content>


