<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Frm_PostProcessDispatch.aspx.cs" Inherits="Frm_PostProcessDispatch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="css/select2.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server" ID="script1"></asp:ScriptManager>
    <div class="shipping-checkout-page">
        <div class="row">
            <div class="col s12">
                <div class="section-title">
                    <p><span style="color: #101010">Dispatch PP</span>&nbsp;Bags</p>
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
                    AllowPaging="True" DataKeyNames="PPID" PageSize="100">
                    <Columns>
                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%#Bind("PPID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                        <asp:BoundField DataField="Refrence_No" HeaderText="Refrence No" />
                          <asp:BoundField DataField="Create_Date" HeaderText="Processed_Date" />
                          <asp:BoundField DataField="Sold_Date" HeaderText="Sold Date" />
                        <asp:BoundField DataField="Load_Date" HeaderText="Load Date" />
                        <%--<asp:BoundField DataField="Load_TruckNo" HeaderText="TruckNo" />--%>
                        <asp:BoundField DataField="TotalWeight" HeaderText="Total Weight" />
                        <asp:BoundField DataField="TotalTierWeight" HeaderText="Total Tare Weight" />
                        <asp:BoundField DataField="TotalNetWeight" HeaderText="Total Net Weight" />
                        <asp:BoundField DataField="TotalBagno" HeaderText="Total Bag" />
                       
                        
                        <asp:TemplateField HeaderText="Dispatch" ShowHeader="False" ItemStyle-Width="60">
                            <ItemTemplate>
                              
                                <asp:LinkButton runat="server" ID="LnkPrint"  Text="PRINT" class="btn btn-sm" CommandName="DISPATCH" CommandArgument='<%# Bind("PPID") %>' OnClick="LnkPrint_Click"></asp:LinkButton>
                               
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
       
    </div>

    
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

       
    </script>
</asp:Content>

