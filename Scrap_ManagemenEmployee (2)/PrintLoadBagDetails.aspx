<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintLoadBagDetails.aspx.cs" Inherits="PrintLoadBagDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                   
                    <td colspan="15" style="text-align: center; height: 50px;">
                         <asp:Image ID="Image1" runat="server" ImageUrl="../img/Pnglogo.png" ImageAlign="Left" Width="5%"/>
                        <div>
                         
                          
                           Refrence No : - 
                           <b><asp:Label ID="LblRefrence" runat="server" Text="" Style="font-size: 15px; font-weight: 300"> </asp:Label></b> 
                            <br />

                            <%--<b style="font-size: large">DELIVERY CHALLAN</b>--%>
                        </div>
                    </td>
                </tr>
                <tr>
                 
                </tr>
                <tr>
                    <td colspan="15" style="height: 30px;">
                      
                    </td>
                </tr>
                <tr style="font-weight: 600; height: 15px;" class="fontsize">
                    <td class="auto-style1">S.No</td>
                    <td colspan="7">Bag Batch No.</td>
                    <td>Scrap Type</td>
                    <td>Bag No</td>
                    <td>Weight</td>
                    <td style="width: 50px;">UOM</td>
                    <%--<td>Remark</td>--%>

                    <%--<td>GST%</td>
                    <td style="text-align: right;">Amount</td>--%>
                </tr>
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <tr class="tr9">
                            <td><%#Container.ItemIndex+1 %></td>
                            <td colspan="7">
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("Bag_BatchNo") %>' Visible="true"></asp:Label>
                            </td>
                            <td>
							<asp:Label ID="Label2" runat="server" Text='<%#Eval("Scrap_Type") + " (" + Eval("BU")+")" %>'></asp:Label>
                                <%-- <asp:Label ID="Label2" runat="server" Text='<%#Eval("Scrap_Type") %>'></asp:Label>>--%>
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text='<%#Eval("Bag_No") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text='<%#Eval("LoadNetWeight") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text='BAG'></asp:Label>
                            </td>
                          
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
               
               
                

               
            </table>

        </div>
    </form>
</body>
</html>
