<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Frm_IraReport.aspx.cs" Inherits="Admin_Frm_IraReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

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
        .txt:hover {background-color: RoyalBlue !Important;

        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server" ID="script1"></asp:ScriptManager>
    <div class="shipping-checkout-page">
        <div class="row">
            <div class="col s12">
                <div class="section-title">
                    <p><span style="color: #101010">IRA </span>&nbsp;REPORT</p>
                </div>
            </div>
        </div>
        <div class="row" style="padding-top: 10px;">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </div>
        <div class="row" style="padding-top: 10px;">
            <div class="input-field col s6 m12 l2">
                <div class="col s12 m12 l12">
                    <asp:DropDownList runat="server" ID="ddlSite" AutoPostBack="false" class="form-control select2" Visible="True"></asp:DropDownList>
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

        </div>


        <div class="mt-20">
           
                <div id="dvReportData" runat="server" class="br-section-wrapper1" style="height:800px;">
                    <div class="row">
                        <div class="col s12">

                            <fieldset>
                                <legend>IRA REPORT DATA</legend>
                            

                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%"></rsweb:ReportViewer>

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
    </script>
</asp:Content>

