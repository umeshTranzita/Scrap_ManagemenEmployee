<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Gate Pass System</title>
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" />
    <script src="assets/js/core/jquery.3.2.1.min.js"></script>
    <script src="assets/js/core/bootstrap.min.js"></script>
    <style>
        body {
            background-image: url(../images/top_left_bg.png), url(../images/bottom_right_bg.png);
            background-position: top left, bottom right;
            background-repeat: no-repeat, no-repeat;
            height: 100vh;
            font-family: 'Open Sans', sans-serif;
        }

        .newbtn {
            color: #fff;
            border-radius: 50px;
            padding: 10px 50px;
            text-transform: uppercase;
            background: #094ea2;
            display: inline-block;
            -webkit-transition: all 0.3s ease-out;
            -moz-transition: all 0.3s ease-out;
            -ms-transition: all 0.3s ease-out;
            -o-transition: all 0.3s ease-out;
            transition: all 0.3s ease-out;
            border: none;
            cursor: pointer;
            outline: none;
        }

        .txtbox {
            width: 100%;
            box-shadow: 0px 12px 30px #ccc;
            border: none;
            padding: 12px 15px;
            margin-bottom: 20px;
        }

        .newbtn:hover {
            background: #131313;
        }

        .fs-title {
            text-align: center;
            font-family: 'Oswald', sans-serif;
        }

        label {
            color: #5b5b5b;
        }

        .PT20 {
            padding-top: 20px;
        }

        .PT40 {
            padding-top: 50px;
        }

        @media (max-width: 1024px) {
            body {
                background-size: 50%, 50%;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblEmail" runat="server" Visible="false"></asp:Label>
        <div class="container">

            <div class="row ">
                <div class="col-md-4"></div>
                <div class="col-md-4">
                    <div class="text-center PT40">
                        <asp:Image ID="imgLogo" runat="server" ImageUrl="~/images/p&g_logo.png" />
                        <h2 class="fs-title PT20"><b><span style="color: #094fa4; font-size: 36px;">e</span>-GATEPASS</b></h2>
                    </div>
                    <div class="row">
                        <div class="col-md-12 PT40">
                            <asp:Label ID="LblMsg" runat="server" ForeColor="red" Style="display: block;"></asp:Label>
                            <div class="form-group text-left">
                                <%--<label>User Name<span style="color: red;"> *</span></label>--%>
                                <asp:TextBox ID="txtUsername" runat="server" placeholder="Example-ABC@pg.com" class="txtbox"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group text-left">
                                <%--<label>Password<span style="color: red;"> *</span></label>--%>
                                <asp:TextBox ID="txtpassword" runat="server" placeholder="Password" TextMode="Password" class="txtbox"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="text-center">
                                <asp:Button ID="btnLogin" runat="server" class="newbtn" Text="Login" OnClick="btnlogin_Click" />
                                <p class="PT40" style="text-align: center;">Designed & Developed by <a href="http://sfatechgroup.com/" target="_blank" style="color: #094ea2; font-weight:bold;">SFA Technologies</a> </p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-4"></div>
            </div>
        </div>
       
    </form>
</body>
</html>
