<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FilmGroups_listMovie.aspx.cs"
    Inherits="OnKeeng_FilmGroups_listMovie" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Language" content="en-us">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <link href="../Common/style.css" type="text/css" rel="stylesheet">
    <link href="../tip/style.css" type="text/css" rel="stylesheet">
    <link href="../tip/style_tooltips.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../common/common.js"></script>

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

        function SelectAlldgrTemp(CheckBoxControl) {

            if (CheckBoxControl.checked == true) {

                var i;

                for (i = 0; i < document.forms[0].elements.length; i++) {
                    if ((document.forms[0].elements[i].type == 'checkbox') && (document.forms[0].elements[i].name.indexOf('dgrTemp') > -1)) {
                        document.forms[0].elements[i].checked = true;
                    }
                }
            }

            else {

                var i;

                for (i = 0; i < document.forms[0].elements.length; i++) {

                    if ((document.forms[0].elements[i].type == 'checkbox') && (document.forms[0].elements[i].name.indexOf('dgrTemp') > -1)) {

                        document.forms[0].elements[i].checked = false;
                    }
                }
            }
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>
            Danh sách tập phim
        </h2>
        <div>
            <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" CssClass="button-admin"
                Text="Update" />
            <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" CssClass="button-admin"
                Text="Delete" />
            <asp:Button ID="btnSetTapInBo" runat="server" OnClick="btnSetTapInBo_Click" CssClass="button-admin"
                Text="SetFilmGroup" />
            <asp:Label ID="lblMess" runat="server" ForeColor="Red" />
        </div>
        <table id="TABLE1" width="100%">
            <tr>
                <td>
                    <asp:DataGrid ID="dgrCommon" runat="server" CssClass="GridLabel" Width="100%" AlternatingItemStyle-BackColor="lightgray"
                        HeaderStyle-BackColor="#99ccff" PagerStyle-HorizontalAlign="Left" PagerStyle-Mode="NumericPages"
                        CellPadding="1" AllowPaging="True" BorderColor="DarkGray" BorderStyle="Inset"
                        PagerStyle-PageButtonCount="50" PageSize="50" OnSelectedIndexChanged="dgrCommon_SelectedIndexChanged"
                        AutoGenerateColumns="False">
                        <PagerStyle CssClass="cssPager" />
                        <SelectedItemStyle BackColor="#FFC0C0"></SelectedItemStyle>
                        <EditItemStyle></EditItemStyle>
                        <AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
                        <ItemStyle></ItemStyle>
                        <HeaderStyle Font-Names="Arial" Font-Bold="True" BorderStyle="None" BackColor="#d2d7da">
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
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    Ảnh</HeaderTemplate>
                                <ItemTemplate>
                                    <img onmouseover="tooltip.show('<IMG src=\'<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FolderName").ToString())%>/<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ItemImage").ToString())%>\'  >')"
                                        onmouseout="tooltip.hide();" src="<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FolderName").ToString())%>/<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ItemImage").ToString())%>"
                                        height="20px" width="20px" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <p>
                                        Tên
                                    </p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p>
                                        <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ItemName").ToString())%></p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <p>
                                        Order by
                                    </p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtOrderBy" runat="server" type="number" min="1" Max="999" Style="width: 70px"
                                        Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "OrderBy").ToString())%>' />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn Visible="False"></asp:BoundColumn>
                        </Columns>
                        <PagerStyle Font-Bold="True" HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
