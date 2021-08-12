<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_ScrapPrint.aspx.cs" Inherits="Frm_ScrapPrint" %>

<!DOCTYPE html>

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
            width: 100%;
        }

        .clstd1 {
            width: 73%;
            font-size: 15px;
        }

        .clstd2 {
            width: 73%;
            font-size: 15px;
        }

        .clstd3 {
            width: 35%;
            font-size: 15px;
        }

        .clstd4 {
            width: 30%;
            font-size: 15px;
        }

        .tab2 {
            width: 100%;
            border: 1px solid black;
            border-collapse: collapse;
        }

            .tab2 th, .tab2 td {
                border: 1px solid black;
                text-align: right;
            }

        .tabFooter {
            width: 100%;
            text-align: center;
        }

        .tabcond {
            width: 100%;
            margin: 10px;
            text-align: left;
        }

        .table {
            border: 1px solid #eee;
            margin-bottom: 0;
        }

            .table > tbody > tr > th {
                background: #3c8dbc;
                color: #fff;
                border: 1px solid grey;
                font-size: 14px;
                padding-bottom: 2px;
                padding-top: 2px;
            }

            .table > tbody > tr > td {
                padding: 3px;
                vertical-align: middle;
                font-size: 14px;
                border: 1px solid grey;
            }

        .Abc {
            font-size: 15px;
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
    <%--<script type="text/javascript">

        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('print.htm', 'PrintWindow', 'letf=0,top=0,width=800%,height=600,toolbar=1,scrollbars=1,status=1');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();

            WinPrint.close();
        }
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <div id="divPrint">
            <div style="width: 850px; margin-top: 10px; border: solid;">
                <div style="text-align: center">
                    <label style="font-size: 24px; font-weight: 700;">P&G MANDIDEEP</label><br />
                    <span style="font-family: trebuchet MS; color: #000000; font-size: 10pt; font-weight: 700; font-style: normal; text-decoration: none;">SCRAP DISPATCHED</span><br />

                </div>

                <div class="row" style="padding-top: 10px;">
                    <div class="col-md-12">
                        <div class="table table-responsive">
                            <asp:GridView ID="GridItem" CssClass="table table-bordered table-striped table-responsive" runat="server" AutoGenerateColumns="false" Style="width: 100%;" ShowFooter="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%#Bind("Scrap_Id") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Consigment No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRefrenceNo" Text='<%# Eval("Refrence_No") %>' runat="server" CssClass="Abc" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bag No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBagNo" Text='<%# Eval("Bag_No") %>' runat="server" CssClass="Abc" />
                                        </ItemTemplate>
                                    </asp:TemplateField>        
                                    <asp:TemplateField HeaderText="Scrap Weight">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWeight" Text='<%# Eval("Bag_Weight") %>' runat="server" CssClass="Abc" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>

                            </asp:GridView>


                        </div>


                    </div>
                </div>






            </div>
        </div>
        <asp:Button ID="Button1" runat="server" Text="Print" Width="100px" BorderWidth="1px" OnClick="Button1_Click" />
    </form>
</body>
</html>
