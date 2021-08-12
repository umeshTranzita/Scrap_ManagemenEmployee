<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print.aspx.cs" Inherits="Print" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        table, td {
            border: 1px solid black;
            border-collapse: collapse;
        }

        .tab1 {
            border: none !important;
        }

            .tab1 td {
                border: none !important;
            }

        .fontsize {
            font-size: 15px;
        }

        .alignRight {
            text-align: right;
        }

        .BorderT {
            border-top: none !important;
        }

        .BorderL {
            border-left: none !important;
        }

        .BorderR {
            border-right: none !important;
        }

        .BorderB {
            border-bottom: none !important;
        }

        .auto-style1 {
            width: 20px;
        }

        .tr9 td {
            border-bottom: 1px solid #ffffff !important;
        }

        .trBlank td {
            border-top: 1px solid #ffffff !important;
        }

        .trBlank1 td {
            border-top: 1px solid #ffffff !important;
        }

        #background {
            position: relative;
            z-index: 0;
            background: white;
            /*min-height: 50%;
            min-width: 50%;*/
            color: black;
        }

        #content {
            position: relative;
            z-index: 1;
        }

        #bg-image {
            color: lightgrey;
            font-size: 50px;
            transform: rotate(300deg);
            -webkit-transform: rotate(300deg);
        }

        #table11 {
            background-color: white !important;
        }
    </style>
    <script type="text/javascript">


        function printrab() {
            //var printPage = window.open(document.URL, '_blank');
            //setTimeout(printPage.print(), 5);
            window.print();
            setTimeout(window.close, 0);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="background">
            <%-- <p id="bg-image">SAIF PHARMA</p>--%>


            <%--<table style="width: 700px; background-image:url('../image/saif.PNG'); background-size:700px; background-repeat:no-repeat;">--%>
            <table id="table11">
                <%--style="background: url(../img/Pnglogo.png) no-repeat;  background-position: center;"--%>
                <tr>

                    <td colspan="15" style="text-align: center; height: 100px;">
                        <asp:Image ID="Image1" runat="server" ImageUrl="../img/Pnglogo.png" ImageAlign="Left" Width="5%" />
                        <div>
                            <asp:Label ID="LblCompanyName" runat="server" Text="" Style="font-size: 30px; font-weight: 900; color: blue;"></asp:Label>

                            <br />
                            <asp:Label ID="LblAddress" runat="server" Text="" Style="font-size: 15px; font-weight: 300"> </asp:Label>
                            <br />
                            GSTIN/UIN:
                            <asp:Label ID="LblGst" runat="server" Text="" Style="font-size: 15px; font-weight: 300"> </asp:Label>
                            <br />

                            <b style="font-size: large">DELIVERY CHALLAN</b>
                        </div>
                    </td>
                </tr>
                <tr>
                    <%-- <td colspan="15" style="height: 25px;">
                        <lable>DL NO. :</lable>
                        <lable>20B/55/11/2015, 20/B/56/11/2015</lable>
                        <lable>&nbsp;&nbsp;&nbsp;&nbsp;GST :</lable>
                        <lable>23AAMFG9710D1ZL</lable>
                    </td>--%>
                </tr>
                <tr>
                    <td colspan="15" style="height: 30px;">
                        <table style="width: 100%;" class="tab1">
                            <tr>
                                <td style="width: 50%; border-right: 1px solid black !important; font-size: large">Consignee : -<asp:Label ID="LblConsigneeName" runat="server" Text=""> </asp:Label>,
                                    <br />
                                    <br />
                                    Consignee Address: -
                                    <asp:Label ID="LblConsigneeAddress" runat="server" Text=""> </asp:Label>
                                    <br />
                                    <br />
                                    Consignee GST :-<asp:Label ID="LblConsigneeGST" runat="server" Text=""> </asp:Label><br />
                                    <br />
                                    Removal Time :-<asp:Label ID="LblRemovalTime" runat="server" Text=""> </asp:Label><br />
                                    <br />
                                    Removal Date :-<asp:Label ID="LblRemovalDate" runat="server" Text=""> </asp:Label><br />

                                </td>
                                <td style="width: 50%">
                                    <table style="width: 100%;" class="tab1">
                                        <tr>
                                            <td style="width: 50%; border-right: 1px solid black !important; font-size: large">Challan Date : -<asp:Label ID="LblChallanDate" runat="server" Text=""> </asp:Label>,
                                                <br />
                                                <br />
                                                Seal No: -
                                                <asp:Label ID="LblLockNo" runat="server" Text=""> </asp:Label>
                                                <br />
                                                <br />
                                                Vehicle No :-
                                                <asp:Label ID="LblVehicleNo" runat="server" Text=""> </asp:Label><br />
                                                <br />
                                                Initial Time :-<asp:Label ID="LblIntTime" runat="server" Text=""> </asp:Label><br />
                                                <br />
                                                Initial Date :-<asp:Label ID="LblIntDate" runat="server" Text=""> </asp:Label><br />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="font-weight: 600; height: 15px;" class="fontsize">
                    <td class="auto-style1">S.No</td>
                    <td colspan="7">Consigment No.</td>
                    <td>Material Description</td>
                    <td>Total Bags</td>
                    <td>Net Weight</td>
                    <td>UOM</td>
                    <%--<td style="width: 50px;">UOM</td>--%>
                    <%--<td>Remark</td>--%>

                    <%--<td>GST%</td>
                    <td style="text-align: right;">Amount</td>--%>
                </tr>
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <tr class="tr9">
                            <td><%#Container.ItemIndex+1 %></td>
                            <td colspan="7">
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("Refrence_No") %>' Visible="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("Scrap_Type") + " (" + Eval("BU")+")" %>'></asp:Label>
                            </td>



                            <td>
                                <asp:Label ID="Label5" runat="server" Text='<%#Eval("Total_Bags") %>'></asp:Label>
                            </td>
                            <%--<td>
                                <asp:Label ID="Label3" runat="server" Text='<%#Eval("Total_Weight") %>'></asp:Label>
                            </td>--%>

                            <td>
                                <asp:Label ID="Label6" runat="server" Text='<%#Eval("Total_LoadNetWeight") %>'></asp:Label>
                            </td>

                            <td>
                                 <asp:Label ID="Label7" runat="server" Text='<%#Eval("UnitName") %>'></asp:Label>
                            </td>
                            
                                <%--<asp:Label ID="Label4" runat="server" Text='BAG'></asp:Label>--%>
                                
                            
                            <%-- <td>
                                <asp:Label ID="Label5" runat="server" Text='<%#Eval("Qty") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text='<%#Eval("Rate") %>'></asp:Label>
                            </td>

                            <td>
                                <asp:Label ID="Label8" runat="server" Text='<%#Eval("Gst") %>'></asp:Label>
                            </td>--%>
                            <%--  <td style="text-align: right;">
                                <asp:Label ID="Label9" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>

                <tr id="trBlank" runat="server" class="tr12">
                    <td id="tdBlank" runat="server">
                        <div id="testSpace" runat="server">
                        </div>
                    </td>
                    <td colspan="7"></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <%-- <td></td>--%>
                    <%--<td></td>
                    <td></td>--%>
                </tr>

                <tr class="fontsize">
                    <td colspan="10">

                        <label style="font-size: 20px; font-weight: 700"></label>
                        <br />
                        Reason for Movement: It is only material transfer, NOT FOR SALE<br />
                        <p>Remark :-</p>
                        <asp:Label runat="server" ID="lblremark"></asp:Label>

                    </td>

                    <td colspan="6">
                        <div style="height: 206px;">
                            <asp:Label ID="LblCompanyName1" runat="server" Text="" Style="font-size: 25px; font-weight: 700"></asp:Label><br />
                        </div>
                        <div style="margin-top: 99px; text-align: center;">
                            <label style="font-weight: 700;">Authorised Signature</label><br />
                        </div>
                    </td>
                </tr>



            </table>

            
        </div>
    </form>
</body>
</html>
