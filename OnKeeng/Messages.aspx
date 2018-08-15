<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Messages.aspx.cs" Inherits="NEW_Messages" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title></title>
    <meta http-equiv="Content-Language" content="en-us">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <link href="../Common/style.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../common/common.js"></script>

    <link href="../tip/style.css" type="text/css" rel="stylesheet">
    <link href="../tip/style_tooltips.css" type="text/css" rel="stylesheet">

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

    <script language="javascript" src="../tip/script.js" type="text/javascript"></script>

    <script language="javascript" src="../tip/javascript.js" type="text/javascript"></script>

    <script language="javascript" src="../tip/tooltips.js" type="text/javascript"></script>

</head>
<body bgcolor="#ffffff" leftmargin="1" topmargin="1">
    <form id="frmMain" runat="server">
    <table style="border-collapse: collapse" bordercolor="#111111" cellspacing="3" cellpadding="3"
        width="100%" border="0">
        <tr>
            <td bgcolor="#d2d7da" style="width: 100%">
                <font color="#000000" size="3"><b>
                    <asp:Label ID="Label1" runat="server" Font-Size="10pt" Font-Bold="True">LIST NEWS</asp:Label>&nbsp;
                    <asp:Label ID="lblError" runat="server" Font-Size="10" ForeColor="Red"></asp:Label></b></font>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                Search:
                <asp:DropDownList ID="ddlType" runat="server" Width="120px">
                </asp:DropDownList>
                &nbsp;Keyword:
                <asp:TextBox ID="txtKeyword" runat="server" Width="232px"></asp:TextBox><asp:Button
                    ID="cmdSearch" runat="server" CssClass="formSubmitBtn" Text="Tìm kiếm" OnClick="cmdSearch_Click"
                    Font-Bold="True"></asp:Button>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <asp:DataGrid ID="dgrCommon" runat="server" Font-Size="10pt" Width="100%" CssClass="NormalText"
                    BorderStyle="Inset" BorderColor="DarkGray" AllowPaging="True" CellPadding="1"
                    PagerStyle-Mode="NumericPages" PagerStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#99ccff"
                    AlternatingItemStyle-BackColor="lightgray" PageSize="10" Font-Names="Arial" AutoGenerateColumns="False">
                    <SelectedItemStyle BackColor="#FFC0C0"></SelectedItemStyle>
                    <EditItemStyle CssClass="NormalText"></EditItemStyle>
                    <AlternatingItemStyle CssClass="NormalText" BackColor="Gainsboro"></AlternatingItemStyle>
                    <ItemStyle CssClass="NormalText"></ItemStyle>
                    <HeaderStyle Font-Names="Arial" Font-Bold="True" BorderStyle="None" BackColor="#D2D7DA">
                    </HeaderStyle>
                    <Columns>
                        <asp:TemplateColumn>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <HeaderTemplate>
                                <div align="center">
                                    <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAll(this)"></div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkAllDelete" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn Visible="False"></asp:BoundColumn>
                    </Columns>
                    <PagerStyle Font-Bold="True" HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
                </asp:DataGrid>
            </td>
        </tr>
        <tr>
            <td style="height: 26px; width: 100%;">
                <p align="left">
                    <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" Width="160px"
                        OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Button ID="cmdAdd" runat="server" CssClass="formSubmitBtn" Text="Add New" DESIGNTIMEDRAGDROP="110"
                        OnClick="cmdAdd_Click" Font-Bold="True"></asp:Button><asp:Button ID="cmdDetele" runat="server"
                            CssClass="formSubmitBtn" Text="Delete" OnClick="cmdDetele_Click" Font-Bold="True">
                        </asp:Button>&nbsp;<asp:Button ID="cmdPublish" runat="server" CssClass="formSubmitBtn"
                            Font-Bold="True" OnClick="cmdPublish_Click" Text="Show" /><asp:Button ID="cmdRemove"
                                runat="server" CssClass="formSubmitBtn" Font-Bold="True" OnClick="cmdRemove_Click"
                                Text="Hide" />
                </p>
                <p align="left">
                    <asp:Label ID="lblTotalRecords" runat="server" Font-Bold="True" ForeColor="#004000"></asp:Label>&nbsp;<asp:HiddenField
                        ID="hiddenToken" runat="server"  />
                </p>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
