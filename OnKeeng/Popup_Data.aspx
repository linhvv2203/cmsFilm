<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Popup_Data.aspx.cs" Inherits="OnKeeng_Popup_Data" %>

<html>
<head>
    <title></title>
    <meta http-equiv="Content-Language" content="vi">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <link href="../Common/style.css" type="text/css" rel="stylesheet">
    <link href="../tip/style.css" type="text/css" rel="stylesheet">
    <link href="../tip/style_tooltips.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../tip/script.js" type="text/javascript"></script>

    <script language="javascript" src="../tip/javascript.js" type="text/javascript"></script>

    <script language="javascript" src="../tip/tooltips.js" type="text/javascript"></script>

    <script language="javascript" src="../common/common.js"></script>

    <script language="JavaScript">
        if (document.all) {
            document.onkeydown = function() {
                var key_enter = 13; // 13 = Enter   
                if (key_enter == event.keyCode) {
                    event.keyCode = 0;
                    document.getElementById('cmdSave').click();
                    return false;
                }
            }
        }
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function(e) {
                    var img = document.getElementById("imagedd");
                    img.src = e.target.result;
                    img.style.width = 100;
                    img.style.height = 100;
                };

                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>

    <style type="text/css">
        .style4
        {
            font-family: Arial, Verdana, Tahoma, Sans-Serif;
            font-size: 13px;
            font-weight: bold;
            color: #000000;
            cursor: default;
            width: 186px;
        }
        .style5
        {
            font-family: Arial, Verdana, Tahoma, Sans-Serif;
            font-size: 13px;
            font-weight: bold;
            color: #000000;
            cursor: default;
            height: 28px;
            width: 186px;
        }
        .style6
        {
            font-family: Arial, Verdana, Tahoma, Sans-Serif;
            font-size: 13px;
            font-weight: bold;
            color: #000000;
            cursor: default;
            height: 8px;
            width: 186px;
        }
        .style7
        {
            font-weight: bold;
        }
    </style>
    <link href="../Styles/DataGridStyle.css" rel="stylesheet" type="text/css" />
</head>
<body bgcolor="#ffffff" leftmargin="3" topmargin="1">
    <form id="Form1" runat="server">
    <table cellspacing="1" cellpadding="1" width="100%" border="0" style="border-collapse: collapse;
        border: solid 1px #aaaaaa;">
        <tbody>
            <tr>
                <td width="100%" style="width: 100%; border-bottom: solid 1px #aaaaaa" valign="middle"
                    height="30">
                    <b>
                        <asp:Label ID="Label1" runat="server" CssClass="titlePage">THÊM MỚI OR CẬP NHẬT</asp:Label>
                        &nbsp;
                        <center>
                            <asp:Label ID="lblError" runat="server" Font-Size="10" ForeColor="Red"></asp:Label></center>
                    </b>
                </td>
            </tr>
            <tr>
                <td width="100%">
                    <table id="AutoNumber2" cellspacing="2" cellpadding="1" width="100%" border="0">                       
                        <tr>
                            <td class="style4">
                                &nbsp;Tên:
                            </td>
                            <td height="28">
                                <font style="font-weight: normal; font-size: 11px; cursor: default; color: #8c8c8c;
                                    font-family: Arial, Verdana, Tahoma, Sans-Serif">
                                    <asp:TextBox ID="txtName" runat="server" CssClass="textboxStyle" Width="429px"></asp:TextBox></font>
                            </td>
                        </tr>
                              <tr>
                            <td class="style6">
                                &nbsp;Nội dung:
                            </td>
                            <td style="height: 8px">
                                <asp:TextBox ID="txtContent" runat="server" Width="429px" TextMode="MultiLine" Height="111px"
                                    CssClass="textboxStyle"></asp:TextBox>
                                <asp:Image ID="imagedd" runat="server" Height="70px" Width="70px" />
                            </td>
                        </tr>
                         <tr>
                            <td class="style4">
                                &nbsp;Link:
                            </td>
                            <td height="28">
                                <font style="font-weight: normal; font-size: 11px; cursor: default; color: #8c8c8c;
                                    font-family: Arial, Verdana, Tahoma, Sans-Serif">
                                    <asp:TextBox ID="txtLink" runat="server" CssClass="textboxStyle" Width="429px"></asp:TextBox></font>
                            </td>
                        </tr>   
                          <tr>
                            <td class="style4">
                                &nbsp;Vị trí:
                            </td>
                            <td height="28">
                                <font style="font-weight: normal; font-size: 11px; cursor: default; color: #8c8c8c;
                                    font-family: Arial, Verdana, Tahoma, Sans-Serif">
                                    <asp:TextBox ID="txtPosition" runat="server" CssClass="textboxStyle" Width="100px"></asp:TextBox></font>
                            </td>
                        </tr>                    
                        <tr>
                            <td class="style4">
                            </td>
                            <td nowrap colspan="1">
                                <asp:Button ID="cmdSave" runat="server" Text="Lưu" OnClick="cmdSave_Click" CssClass="button_admin"
                                    Width="80"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="formLabel" width="100%" style="height: 26px">
                    &nbsp;
                    <asp:HiddenField ID="hiddenTime" runat="server" />
                    <asp:HiddenField runat="server" ID="hiddenToken" />
                </td>
            </tr>
        </tbody>
    </table>
    </form>
</body>
</html>
