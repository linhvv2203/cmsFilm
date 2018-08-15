<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopDelete.aspx.cs" Inherits="OnKeeng_PopDelete" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Xóa video</title>
    <style type="text/css">
        input, select, textarea
        {
            font-size: 12px;
            font-family: Arial, Verdana, Sans-Serif;
            height: 25px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table align="center" cellpadding="2" cellspacing="2" style="font-weight: bold; color: black;
            font-family: 'Times New Roman'">
            <tr>
                <td>
                    LÝ DO XÓA NỘI DUNG:<br />
                    <%=Server.HtmlEncode(sItemName)%>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="ddlCategory" runat="server" Width="246px" CssClass="select_style">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtDesc" runat="server" Width="246px" TextMode="MultiLine" Height="111px"
                        CssClass="textboxStyle"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="cmdSave" runat="server" Text="Lưu" OnClick="cmdSave_Click" CssClass="button_admin"
                        Width="80"></asp:Button>
                    <asp:HiddenField ID="hiddenToken" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
