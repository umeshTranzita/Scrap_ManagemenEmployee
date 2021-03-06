<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QrCodeWebCam.aspx.cs" Inherits="QrCodeWebCam" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
   <style>
       .qrcode-text-btn {
  display: inline-block;
  height: 1em;
  width: 1em;
  background: url(qr_icon.svg) 50% 50% no-repeat;
  cursor: pointer;
}

.qrcode-text-btn > input[type=file] {
  position: absolute;
  overflow: hidden;
  width: 1px;
  height: 1px;
  opacity: 0;
}

.qrcode-text {
  padding-right: 1.7em;
  margin-right: 0;
  vertical-align: middle;
}

.qrcode-text + .qrcode-text-btn {
  width: 1.7em;
  margin-left: -1.7em;
  vertical-align: middle;
}

@media only screen and (max-device-width:750px) {
  /* previous CSS code goes here */
}
   </style>
    <script src="https://rawgit.com/sitepoint-editors/jsqrcode/master/src/qr_packed.js">
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <%-- <input type=file   accept="image/*"   capture=environment>--%>
       <label class=qrcode-text-bt <input type=file   accept="image/*"  capture=environment  tabindex=-1>
           <input type=text class=qrcode-text><label class=qrcode-text-btn> <input type=file  accept="image/*"  capture=environment  tabindex=-1 onchange="openQRCamera(this);" onclick="return showQRIntro();">
</label>
</label>
    </div>
       
    </form>
    <script>
        function openQRCamera(node) {
            var reader = new FileReader();
            reader.onload = function () {
                node.value = "";
                qrcode.callback = function (res) {
                    if (res instanceof Error) {
                        alert("No QR code found. Please make sure the QR code is within the camera's frame and try again.");
                    } else {
                        node.parentNode.previousElementSibling.value = res;
                    }
                };
                qrcode.decode(reader.result);
            };
            reader.readAsDataURL(node.files[0]);
        }

        function showQRIntro() {
            return confirm("Use your camera to take a picture of a QR code.");
        }
    </script>
</body>
</html>
