<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Messages_data.aspx.cs" Inherits="NEW_Messages_data" %>

<%@ Register TagPrefix="rade" Namespace="Telerik.WebControls" Assembly="RadEditor" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>New Page 1</title>
    <meta http-equiv="Content-Language" content="en-us">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <link href="../Common/style.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../common/common.js"></script>

    <script src="../js/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="../js/jquery.datetimepicker.full.js" type="text/javascript"></script>
    <link href="../Styles/jquery.datetimepicker.css" rel="stylesheet" type="text/css" />

    <script language="JavaScript">

        if (document.all) {
            document.onkeydown = function() {
                var key_enter = 13; // 13 = Enter   
                if (key_enter == event.keyCode) {
                    event.keyCode = 0;
                    document.getElementById('cmdSearch').click();
                    return false;
                }
            }
        }

    </script>

    <style type="text/css">
        .style4
        {
            width: 97%;
        }
        .style5
        {
            width: 155px;
        }
        .style6
        {
            width: 78%;
        }
    </style>
</head>
<body bgcolor="#ffffff" leftmargin="0" topmargin="0">
    <div class="formWrapper">
        <table style="border-collapse: collapse" bordercolor="#111111" cellspacing="1" cellpadding="1"
            border="0">
            <form id="Form1" runat="server">
            <tr>
                <td nowrap bgcolor="#dedbce" colspan="2" height="20" colspan="2" class="style6">
                    <b>ADD NEW / UPDATE&nbsp;
                        <asp:Label ID="lblError" runat="server" Font-Size="10" ForeColor="Red"></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td class="style6">
                    <table style="border-collapse: collapse; width: 1248px;" bordercolor="#111111" cellspacing="3"
                        cellpadding="3" border="0">
                        <tr>
                            <td nowrap width="149">
                                Chọn CP<strong>:</strong>
                            </td>
                            <td class="style5">
                                <font style="font-weight: normal; font-size: 11px; cursor: default; color: #8c8c8c;
                                    font-family: Arial, Verdana, Tahoma, Sans-Serif" />
                                <asp:DropDownList ID="ddldt" runat="server" AutoPostBack="false" Width="150px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap width="149">
                                <strong>Title:</strong>
                            </td>
                            <td width="100%" colspan="2">
                                <asp:TextBox ID="txtTitle" runat="server" Width="708px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap width="149" height="1">
                                <strong>Lead:</strong>
                            </td>
                            <td width="100%" height="1" colspan="2">
                                <asp:TextBox ID="txtMsgContent" runat="server" Width="712px" Height="184px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap width="149" height="1">
                                <strong>DatePub :</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="datetimepicker" runat="server" ></asp:TextBox>
                                <script>
                                    $(document).ready(function() {
                                        jQuery('#datetimepicker').datetimepicker();
                                    });
          
                                </script>

                            </td>
                        </tr>
                        <tr>
                            <td nowrap width="149">
                            </td>
                            <td class="style4" colspan="2">
                                <asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="formSubmitBtn" OnClick="cmdSave_Click"
                                    Font-Bold="True" Height="20px"></asp:Button>
                                <asp:Button ID="cmdDelete" runat="server" Text="Back" CssClass="formSubmitBtn" OnClick="cmdDelete_Click"
                                    Font-Bold="True" Height="20px"></asp:Button>
                                <asp:HiddenField ID="hiddenToken" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            </form>
        </table>
    </div>
</body>
</html>
