<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PartnerData.aspx.cs" Inherits="SYS_PartnerData" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title></title>
    <meta http-equiv="Content-Language" content="en-us">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <link href="Styles/Styles.css" type="text/css" rel="stylesheet">
    <link href="Styles/FormContainer.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="../Common/style.css">

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

        function setImage(string) {
            document.getElementById(ImgDataID).src = 'File:\/\/' + string;
        }
        function checkImageSize() {
            if (document.getElementById(ImgDataID).width >= 400 || document.getElementById(ImgDataID).height >= 400) {
                alert("Ban dang nhap anh co so size: (" + document.getElementById(ImgDataID).width + "x" + document.getElementById(ImgDataID).height + "), Ban khong duoc nhap anh qua so size(400x400)");
                return false;
            }
        }
				
				
    </script>

</head>
<body bgcolor="#ffffff" leftmargin="3" topmargin="3">
    <form id="Form1" runat="server">
    <table cellspacing="1" cellpadding="1" width="100%" border="0">
        <tr>
            <td width="100%" bgcolor="#F0EDE1" valign="middle" height="30">
                &nbsp;<img src="../images/selectimage.gif" width="20" height="20" align="absbottom" />
                <font color="#000000" size="3"><b>
                    <asp:Label ID="Label1" runat="server" Font-Size="10pt" Font-Bold="True">THÊM MỚI MENU</asp:Label>&nbsp;
                    <asp:Label ID="lblError" runat="server" Font-Size="10" ForeColor="Red"></asp:Label>
                </b></font>
            </td>
        </tr>
    </table>
    <table cellspacing="1" cellpadding="1" width="100%" border="0">
        <tr>
            <td width="100%">
                <table id="AutoNumber2" cellspacing="1" cellpadding="1" width="100%" border="0">
                    <tr>
                        <td class="formLabel" height="2" style="width: 139px">
                            &nbsp;Tên Đối Tác:&nbsp;Tên Đối Tác:
                        </td>
                        <td height="28">
                            <asp:TextBox ID="txtnamePartner" runat="server" Height="20px" Width="472"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="formLabel" style="width: 139px">
                            &nbsp;Tên đối tác - Popup:
                        </td>
                        <td height="28">
                            <asp:TextBox ID="txtpopup" runat="server" Height="20px" Width="472"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="formLabel" style="width: 139px">
                            Link:
                        </td>
                        <td height="28">
                            <asp:TextBox ID="txtlink" runat="server" Height="20px" Width="472"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="formLabel" style="width: 139px">
                            &nbsp;Số điện thoại:
                        </td>
                        <td height="28">
                            <asp:TextBox ID="txtphone" runat="server" Height="20px" Width="472"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="formLabel" style="width: 139px">
                            &nbsp;Địa chỉ:
                        </td>
                        <td height="28">
                            <font style="font-weight: normal; font-size: 11px; cursor: default; color: #8c8c8c;
                                font-family: Arial, Verdana, Tahoma, Sans-Serif">
                                <asp:TextBox ID="txtaddress" runat="server" Height="20px" Width="472"></asp:TextBox></font>
                        </td>
                    </tr>
                    <tr>
                        <td class="formLabel" style="width: 139px">
                            &nbsp;Email:
                        </td>
                        <td height="28">
                            <asp:TextBox ID="txtemail" runat="server" Height="20px" Width="183px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="formLabel" style="width: 139px; height: 25px;">
                            &nbsp;Fax:
                        </td>
                        <td style="height: 25px">
                            <asp:TextBox ID="txtfax" runat="server" Width="182px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="formLabel" style="width: 139px; height: 25px;">
                            &nbsp;Mô tả:
                        </td>
                        <td style="height: 25px">
                            <asp:TextBox ID="txtdescription" runat="server" Width="469px" Height="90px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="formLabel" style="width: 139px; height: 25px;">
                            &nbsp;Trạng thái:
                        </td>
                        <td style="height: 25px">
                            <asp:DropDownList ID="ddlStatus" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="formLabel" style="width: 139px">
                        </td>
                        <td nowrap colspan="1">
                            <asp:Button ID="cmdSave" runat="server" CssClass="formSubmitBtn" Text="Lưu" OnClick="cmdSave_Click"
                                Font-Bold="True"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

