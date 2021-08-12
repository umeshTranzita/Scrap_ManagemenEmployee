<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Frm_TemplateData.aspx.cs" Inherits="Frm_TemplateData" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server" ID="script1"></asp:ScriptManager>
    <div class="shipping-checkout-page">
        <div class="row">
            <div class="col s12">
                <div class="section-title">
                    <p><span style="color: #101010">INVENTORY DATA</span>&nbsp;REPORT</p>
                </div>
            </div>
        </div>
        <div class="row" style="padding-top: 10px;">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </div>
      <div class="box-body">
        <div class="row" style="padding-top: 10px;">
            <div class="input-field col s6 m12 l2" hidden>
                <div class="col s12 m12 l12">
                    <asp:DropDownList runat="server" ID="ddlSite" AutoPostBack="false" class="form-control select2" Visible="True"></asp:DropDownList>
                </div>
            </div>
            <div class="input-field col s6 m12 l3">
                <div class="col s12 m12 l12">
                    <asp:DropDownList runat="server" ID="ddlReportType" AutoPostBack="false" class="form-control" Visible="True" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged">
                        <asp:ListItem Value="0">SELECT</asp:ListItem>
                        <asp:ListItem Value="1">RECEIVE/ISSUE RECORD</asp:ListItem>
                        <asp:ListItem Value="2">POST PROCESSING RECORD</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <asp:Panel runat="server" ID="CustomPanel" Visible="True">
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


                <asp:LinkButton runat="server" class="btn theme-btn-rounded" Text="<i class='fa fa-download'></i>&nbsp;&nbsp;Export" ID="btnExport" OnClick="btnExport_Click" Visible="false"></asp:LinkButton>

            </div>

        </div>
          </div>


        <div class="mt-20">

             <div id="dvReportData" runat="server" class="br-section-wrapper1" style="height: 800px;" visible="false">
            <div class="row">
                <div class="col s12">
                    <div class="table-responsive">

                        <asp:GridView ID="GridData" runat="server" class="table table-striped table-bordered" AutoGenerateColumns="False"
                            AllowPaging="false" ShowFooter="true" OnRowDataBound="GridData_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="D_Date" HeaderText="Date" />
                                <asp:BoundField DataField="ReceiveTime" HeaderText="ReceiveTime" />


                                <asp:TemplateField HeaderText="Opening Bal Pad">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOpeningPad" Text='<%# Eval("OpeningPadBal") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Opening Bal Diaper">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOpeningDia" Text='<%# Eval("OpeningDiaBal") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Refrence No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRefrenceNo" Text='<%# Eval("Refrence_No") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pad Receive Weight">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPadReceiveWeight" Text='<%# Eval("PadReceiveWeight") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Diaper Receive Weight">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDiaReceiveWeight" Text='<%# Eval("DiaReceiveWeight") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pad Issue Weight">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPadIssueWeight" Text='<%# Eval("PadIssueWeight") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Diaper Issue Weight">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDiaperIssueWeight" Text='<%# Eval("DiaIssueWeight") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ClosingPad - PGArea">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemnantPad" Text='<%# Eval("PadClosingBal") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ClosingDiaper - PGArea">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemnantDiaper" Text='<%# Eval("DiaClosingBal") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
              </div>

               <div id="dvPostReportData" runat="server" class="br-section-wrapper1" style="height: 800px;" visible="false">
            <div class="row">
                <div class="col s12">
                    <div class="table-responsive">

                        <asp:GridView ID="GridPostProcess" runat="server" class="table table-striped table-bordered" AutoGenerateColumns="False"
                            AllowPaging="false" ShowFooter="true">
                            <Columns>
                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                             
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" Text='<%# Eval("D_Date") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Pad Opening Weight">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPadOpeningWeight" Text='0' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Diaper Opening Weight">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDiaperOpeningWeight" Text='0' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pad Issue Weight">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPadIssueWeight" Text='<%# Eval("PadIssueWeight") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Diaper Issue Weight">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDiaperIssueWeight" Text='<%# Eval("DiaperIssueWeight") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Refrence No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRefrenceNo" Text='<%# Eval("Refrence_No") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PP DIAPER CORE WEIGHT">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPP_DiaperCoreWeight" Text='<%# Eval("PP_DiaperCoreWeight") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PP DIAPER REMNANT WEIGHT">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPP_DiaperScrapWeight" Text='<%# Eval("PP_DiaperScrapWeight") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PP PAD CORE WEIGHT">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPP_PadCoreWeight" Text='<%# Eval("PP_PadCoreWeight") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PP PAD REMNANT WEIGHT">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPP_PadScrapWeight" Text='<%# Eval("PP_PadScrapWeight") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SALE DIAPER CORE WEIGHT">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSale_DiaperCoreWeight" Text='<%# Eval("Sale_DiaperCoreWeight") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SALE DIAPER REMNANT WEIGHT">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSale_DiaperScrapWeight" Text='<%# Eval("Sale_DiaperScrapWeight") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SALE PAD CORE WEIGHT">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSale_PadCoreWeight" Text='<%# Eval("Sale_PadCoreWeight") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SALE PAD REMNANT WEIGHT">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSale_PadScrapWeight" Text='<%# Eval("Sale_PadScrapWeight") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Process Loss Pad Wt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPad_ProcessLossWt" Text='0' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Process Loss Dia Wt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDia_ProcessLossWt" Text='0' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Process Loss Pad %">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPad_ProcessLossWtPercentage" Text='<%# Eval("PadLoss") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Process Loss Dia %">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDia_ProcessLossWtPercentage" Text='<%# Eval("DiaLoss") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CLOSING PAD - KDL AREA">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemnantPad" Text='0' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CLOSING DIA - KDL AREA">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemnantDia" Text='0' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>




                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
             </div>
        </div>


    </div>
    <script>
        function validateform() {
            var msg = "";

             if (document.getElementById('<%=ddlReportType.ClientID%>').selectedIndex == 0) {
                  msg += "Please Select Report Type.\n";
               }
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
    </script>
</asp:Content>


