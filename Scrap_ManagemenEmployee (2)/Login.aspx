<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login2" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SCRAP MANAGEMENT</title>
    <meta name="viewport" content="width=device-width, initial-scale=1 maximum-scale=1" />
    <link rel="icon" href="favicon.ico" type="image/x-icon" />
    <link rel="stylesheet" href="lib/font-awesome/web-fonts-with-css/css/fontawesome-all.css" />
    <link rel="stylesheet" href="css/materialize.min.css" />
    <link rel="stylesheet" href="css/normalize.css" />
    <link rel="stylesheet" href="css/style.css" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/select2.min.css" rel="stylesheet" />

    <style>
        input:not([type]), input[type=text]:not(.browser-default), input[type=password]:not(.browser-default), input[type=email]:not(.browser-default), input[type=url]:not(.browser-default), input[type=time]:not(.browser-default), input[type=date]:not(.browser-default), input[type=datetime]:not(.browser-default), input[type=datetime-local]:not(.browser-default), input[type=tel]:not(.browser-default), input[type=number]:not(.browser-default), input[type=search]:not(.browser-default), textarea.materialize-textarea {
            border-bottom: 1px solid #fff !important;
        }

        .input-field label, body a {
            color: #fff;
        }

        .form-control, active {
            color: #fff !important;
        }

        body input:focus:not([type]):not([readonly]) + label,
        body input[type="text"]:focus:not(.browser-default):not([readonly]) + label,
        body input[type="password"]:focus:not(.browser-default):not([readonly]) + label,
        body input[type="email"]:focus:not(.browser-default):not([readonly]) + label,
        body input[type="url"]:focus:not(.browser-default):not([readonly]) + label,
        body input[type="time"]:focus:not(.browser-default):not([readonly]) + label,
        body input[type="date"]:focus:not(.browser-default):not([readonly]) + label,
        body input[type="datetime"]:focus:not(.browser-default):not([readonly]) + label,
        body input[type="datetime-local"]:focus:not(.browser-default):not([readonly]) + label,
        body input[type="tel"]:focus:not(.browser-default):not([readonly]) + label,
        body input[type="number"]:focus:not(.browser-default):not([readonly]) + label,
        body input[type="search"]:focus:not(.browser-default):not([readonly]) + label,
        body textarea.materialize-textarea:focus:not([readonly]) + label {
            color: #fff;
        }

        body input:focus:not([type]):not([readonly]),
        body input[type="text"]:focus:not(.browser-default):not([readonly]),
        body input[type="password"]:focus:not(.browser-default):not([readonly]),
        body input[type="email"]:focus:not(.browser-default):not([readonly]),
        body input[type="url"]:focus:not(.browser-default):not([readonly]),
        body input[type="time"]:focus:not(.browser-default):not([readonly]),
        body input[type="date"]:focus:not(.browser-default):not([readonly]),
        body input[type="datetime"]:focus:not(.browser-default):not([readonly]),
        body input[type="datetime-local"]:focus:not(.browser-default):not([readonly]),
        body input[type="tel"]:focus:not(.browser-default):not([readonly]),
        body input[type="number"]:focus:not(.browser-default):not([readonly]),
        body input[type="search"]:focus:not(.browser-default):not([readonly]),
        body textarea.materialize-textarea:focus:not([readonly]) {
            border-bottom: 1px solid #101010;
            -webkit-box-shadow: 0 1px 0 0 #101010;
            box-shadow: 0 1px 0 0 #101010;
        }

        body .btn, body .btn-large {
            background-color: #101010;
        }
    </style>
    <style type="text/css">
        #profile_pic_wrapper {
            position: relative;
            border: #ccc solid 1px;
            width: 120px;
            height: 120px;
            border: none;
        }



            #profile_pic_wrapper a {
                position: absolute;
                display: none;
                top: 30;
                right: 0;
                margin-top: -30px;
                line-height: 20px;
                padding: 5px;
                color: #fff;
                background-color: #333;
                width: 110px;
                text-decoration: underline;
                text-align: center;
                z-index: 100;
                text-decoration: none;
                font-family: Arial;
                font-size: 10px;
            }



            #profile_pic_wrapper:hover a {
                position: absolute;
                margin: 90px 0px 0px 0px;
                display: block;
                text-decoration: none;
                font-family: Arial;
                font-size: 10px;
            }



                #profile_pic_wrapper:hover a:hover {
                    text-decoration: none;
                    font-family: Arial;
                    font-size: 10px;
                }

        .profile_pic {
            width: 120px;
            height: 120px;
        }
    </style>

</head>
<body id="homepage" class="bodybg">
    <form id="form1" runat="server">

        <!-- BEGIN PRELOADING -->
        <div class="preloading">
            <div class="wrap-preload">
                <div class="cssload-loader"></div>
            </div>
        </div>

        <!-- CONTENT -->
        <div id="page-content">
            <div class="login-form" style="padding-top: 20px;">
                <div class="container">
                    <div class="row">
                        <div class="col s12">
                            <div class="section-title" style="color: #fff">
                                <p>
                                    <a href="#" class="center">
                                        <img src="img/Pnglogo.png" /></a>
                                </p>
                                <p style="padding-top: 40px;"><span style="color: #101010;">SCRAP</span> MANAGEMENT</p>
                                <p style="padding-top: 10px;"><span style="color: #101010;">LOGIN</span> ACCOUNT</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col s12 mt-20">
                            <div class="row">
                                <div class="input-field col s12 m12 l4  offset-l4">
                                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <asp:Panel ID="login" runat="server" Visible="true">
                                <div class="input-field col s12 m12 l4  offset-l4 text-center">
                                    <asp:Button ID="BtnContractor" runat="server" class="btn btn-sm" Text="CONTRACTOR LOGIN" OnClick="BtnContractor_Click" />
                                    <asp:Button ID="BtnEmployee" runat="server" class="btn btn-sm" Text="EMPLOYEE LOGIN" BackColor="Blue" OnClick="BtnEmployee_Click" />
                                </div>

                                <div class="row" runat="server" visible="false" id="ContractorUhid">
                                    <div class="input-field col s12 m12 l4  offset-l4">
                                        <div class="col s12 m12 l12">
                                            <asp:DropDownList runat="server" ID="ddlSite" AutoPostBack="true" class="form-control" BackColor="Blue" OnSelectedIndexChanged="ddlSite_SelectedIndexChanged"></asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="input-field col s12 m12 l4  offset-l4" style="padding-top: 30px;" runat="server" visible="false" id="UhidCardNo">

                                        <asp:TextBox ID="TxtUserName" class="form-control" runat="server" OnTextChanged="TxtUserName_TextChanged" AutoPostBack="true" onkeypress="return validateNum(event)" AutoComplete="off"></asp:TextBox>
                                        <label for="username" style="padding-top: 30px;">ENTER UHID CARD NO</label>
                                    </div>
                                    <div class="input-field col s12 m12 l4  offset-l4" style="padding-top: 30px;" runat="server" visible="false" id="EmployeeEmailId">

                                        <asp:TextBox ID="TxtEmployeeEmail" class="form-control" runat="server" OnTextChanged="TxtEmployeeEmail_TextChanged" AutoPostBack="true" AutoComplete="off"></asp:TextBox>
                                        <label for="EmployeeEmail" style="padding-top: 30px;">ENTER EMPLOYEE EMAILID</label>
                                    </div>



                                </div>
                                <div class="row" hidden>
                                    <div class="input-field col s12 m12 l4  offset-l4">
                                        <asp:TextBox ID="TxtPassword" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                        <label for="password">Password</label>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div id="ForgetPassword" runat="server" visible="false">
                                <asp:Panel ID="forget" runat="server" Visible="false">
                                    <div class="input-field col s12 m12 l4  offset-l4">
                                        <asp:Label runat="server" class="form-control" ID="LblName" ForeColor="Black" BackColor="Blue"></asp:Label>
                                    </div>
                                    <div class="input-field col s12 m12 l4  offset-l4">
                                        <asp:Label runat="server" class="form-control" ID="LblVendorName" BackColor="Blue"></asp:Label>
                                    </div>
                                    <div class="input-field col s12 m12 l4  offset-l4"  runat="server">
                                        <asp:FileUpload ID="ImageUpload" runat="server"  class="form-control" BackColor="Blue"/>
                                    </div>

                                </asp:Panel>

                                <div class="row">
                                    <div class="input-field col s12 m12 l4  offset-l4">
                                        <asp:LinkButton ID="lnkbtnBack" runat="server" Text="Back to UHID NO" OnClick="lnkbtnBack_Click"></asp:LinkButton>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="input-field col s12 m12 l4  offset-l4">
                                        <asp:Button ID="BtnLogin" runat="server" Text="LOG IN" OnClick="BtnLogin_Click" Visible="false" />
                                    </div>

                                </div>
                            </div>
                            <div style="float: left; padding-left: 35px;" id="profile_pic_wrapper" hidden>

                                <asp:Image ID="img" Width="120" Height="120" runat="server" Style="float: left;" />

                                <asp:LinkButton ID="Linkbutton1" runat="server" OnClick="Linkbutton1_Click">Change  Photo</asp:LinkButton>

                            </div>

                        </div>


                    </div>


                </div>
            </div>
        </div>
        <!-- END CONTENT-->
        <!-- FOOTER  -->
        <footer id="footer" style="background: none;">
            <div class="container">
                <div class="row copyright">
                    Design & Developed by <span>SFA Technologies</span>.
                </div>
            </div>
        </footer>

        <script src="js/jquery.min.js"></script>
        <script src="js/materialize.min.js"></script>
        <script src="js/custom.js"></script>
        <script src="js/bootstrap.min.js"></script>
        <script src="WebCam/Webcam_Plugin/jquery.webcam.js"></script>
        <script src="js/select2.full.min.js"></script>
        <asp:ValidationSummary ID="vsforgetpassword" runat="server" ValidationGroup="continue" ShowMessageBox="true" ShowSummary="false" HeaderText="Errors: " />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="validate" ShowMessageBox="true" ShowSummary="false" HeaderText="Errors: " />
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Login" ShowMessageBox="true" ShowSummary="false" HeaderText="Errors: " />
        <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="final" ShowMessageBox="true" ShowSummary="false" HeaderText="Errors: " />
        <script>


            function validateNum(evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;
            }








            function ValidateStringLength(source, arguments) {
                var slen = arguments.Value.length;
                // alert(arguments.Value + '\n' + slen);
                if (slen >= 6 && slen <= 15) {
                    arguments.IsValid = true;
                }
                else {
                    arguments.IsValid = false;
                }
            }
        </script>
    </form>
</body>
</html>
