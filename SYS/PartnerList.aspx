<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PartnerList.aspx.cs" Inherits="SYS_PartnerList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title></title>
    <meta http-equiv="Content-Language" content="en-us">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <link href="../Common/style.css" type="text/css" rel="stylesheet">

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

    </script>

</head>
<body class="BODY" leftmargin="1" topmargin="1">
    <form id="Form1" runat="server">
    <table cellspacing="0" cellpadding="1" width="100%" border="0">
        <tbody>
            <tr>
                <td width="100%" bgcolor="#F0EDE1" valign="middle" height="30">
                    &nbsp;<img src="../images/selectimage.gif" width="20" height="20" align="absbottom" />
                    <font color="#000000" size="3"><b>
                        <asp:Label ID="Label1" runat="server" Font-Size="10pt" Font-Bold="True">DANH SÁCH MENU</asp:Label>&nbsp;
                        <asp:Label ID="lblError" runat="server" Font-Size="10" ForeColor="Red"></asp:Label>
                    </b></font>
                </td>
            </tr>
            <tr>
                <td width="100%" bgcolor="#000000" valign="middle" height="1">
                </td>
            </tr>
            <tr>
                <td class="RadWWrapperBodyCenter" width="100%">
                    <table id="ConfigTable" width="100%">
                        <tr>
                            <td class="formLabel">
                                &nbsp; Tìm theo:
                                <asp:DropDownList ID="ddlSearch" runat="server" Width="73px">
                                </asp:DropDownList>
                                Từ khóa:
                                <asp:TextBox ID="txtKeyword" runat="server" Width="99px"></asp:TextBox><asp:Button
                                    ID="cmdSearch" runat="server" CssClass="formSubmitBtn" Text="Tìm kiếm" OnClick="cmdSearch_Click"
                                    Font-Bold="True"></asp:Button>&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DataGrid ID="dgrCommon" runat="server" CssClass="GridLabel" Width="100%" PageSize="20"
                                    AlternatingItemStyle-BackColor="lightgray" HeaderStyle-BackColor="#99ccff" PagerStyle-HorizontalAlign="Left"
                                    PagerStyle-Mode="NumericPages" CellPadding="1" AllowPaging="True" BorderColor="DarkGray"
                                    BorderStyle="Inset">
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
                                                Tên Công Ty</HeaderTemplate>
                                            <ItemTemplate>
                                                <%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "namePartner").ToString())%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderTemplate>
                                                Số điện thoại</HeaderTemplate>
                                            <ItemTemplate>
                                                <%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "phone").ToString())%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderTemplate>
                                                Địa chỉ</HeaderTemplate>
                                            <ItemTemplate>
                                                <%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "address   ").ToString())%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderTemplate>
                                                Email</HeaderTemplate>
                                            <ItemTemplate>
                                                <%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "email").ToString())%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderTemplate>
                                                Fax</HeaderTemplate>
                                            <ItemTemplate>
                                                <%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "fax").ToString())%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderTemplate>
                                                Sửa</HeaderTemplate>
                                            <ItemTemplate>
                                                <a href="PartnerData.aspx?id=<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "id").ToString())%>&type=edit">
                                                    <img alt="" title=" <%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "namePartner").ToString())%>" src="../images/Edit.gif"
                                                        height="20px" border="0" width="20px" />
                                                </a>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn Visible="False"></asp:BoundColumn>
                                    </Columns>
                                    <PagerStyle Font-Bold="True" HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>
                    <table id="TABLE1" width="100%">
                        <tr>
                            <td class="formLabel" height="24">
                                <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" Width="112px"
                                    OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:Button ID="cmdAdd" runat="server" CssClass="formSubmitBtn" Text="Thêm mới" OnClick="cmdAdd_Click"
                                    Font-Bold="True"></asp:Button><asp:Button ID="cmdDetele" runat="server" CssClass="formSubmitBtn"
                                        Text="Xóa" OnClick="cmdDetele_Click" Font-Bold="True"></asp:Button><asp:Button ID="cmdPublish"
                                            runat="server" CssClass="formSubmitBtn" Text="Duyệt" OnClick="cmdPublish_Click"
                                            Font-Bold="True"></asp:Button><asp:Button ID="cmdRemove" runat="server" CssClass="formSubmitBtn"
                                                Text="Gỡ Bỏ" OnClick="cmdRemove_Click" Font-Bold="True"></asp:Button>&nbsp;<asp:Label
                                                    ID="lblTotalRecords" runat="server" ForeColor="#004000" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <div>
                        &nbsp;</div>
                </td>
            </tr>
        </tbody>
    </table>
    </form>
</body>
</html>

