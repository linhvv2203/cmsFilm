<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popup.aspx.cs" Inherits="OnKeeng_popup" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
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

    <style type="text/css">
        .cssPager span
        {
            background-color: Yellow;
        }
        .style4
        {
            font-family: Arial, Verdana, Tahoma, Sans-Serif;
            font-size: 13px;
            font-weight: bold;
            color: #000000;
            cursor: default;
            width: 80%;
        }
    </style>
    <link href="../common/calendar-win2k-2.css" type="text/css" rel="stylesheet">

    <script src="../common/calendar.js" type="text/javascript"></script>

    <script src="../common/calendar-en.js" type="text/javascript"></script>

    <script language="javascript" src="../tip/script.js" type="text/javascript"></script>

    <script language="javascript" src="../tip/javascript.js" type="text/javascript"></script>

    <script language="javascript" src="../tip/tooltips.js" type="text/javascript"></script>

</head>
<body class="BODY" leftmargin="3" topmargin="1">
    <form id="Form1" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tbody>
            <tr>
                <td width="100%" bgcolor="#F0EDE1" valign="middle" height="30">
                    &nbsp; <font color="#000000" size="3"><b>
                        <asp:Label ID="Label1" runat="server" Font-Size="10pt" Font-Bold="True">DANH SÁCH POPUP</asp:Label>
                        &nbsp;
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
                            <td class="style4" height="6">
                                Tìm theo:
                                <asp:DropDownList ID="ddlImages" runat="server" CssClass="ComboBoxInputHover_ClassicBlue"
                                    Width="90px" OnSelectedIndexChanged="ddlImages_SelectedIndexChanged">
                                </asp:DropDownList>
                                Từ khóa:
                                <asp:TextBox ID="txtKeyword" runat="server" Width="159px"></asp:TextBox>&nbsp;<asp:Button
                                    ID="cmdSearch" runat="server" CssClass="formSubmitBtn" Text="Tìm kiếm" Font-Bold="True"
                                    OnClick="cmdSearch_Click1" Height="20px"></asp:Button>
                            </td>
                            <td>
                                <center>
                                    <asp:Label ID="lbError" runat="server" Text=""></asp:Label></center>
                            </td>
                        </tr>
                    </table>
                    <table id="TABLE1" width="100%">
                        <tr>
                            <td>
                                <asp:DataGrid ID="dgrCommon" runat="server" CssClass="GridLabel" Width="100%" AlternatingItemStyle-BackColor="lightgray"
                                    HeaderStyle-BackColor="#99ccff" PagerStyle-HorizontalAlign="Left" PagerStyle-Mode="NumericPages"
                                    CellPadding="1" AllowPaging="True" BorderColor="DarkGray" BorderStyle="Inset" OnPageIndexChanged="dgrCommon_PageIndexChanged" OnDeleteCommand="dgrCommon_DeleteCommand"
                                    PagerStyle-PageButtonCount="50" DataMember="id" AutoGenerateColumns="False">
                                    <PagerStyle CssClass="cssPager" />
                                    <SelectedItemStyle BackColor="#FFC0C0"></SelectedItemStyle>
                                    <AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
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
                                                <p>
                                                    ID</p>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <p>
                                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ID").ToString())%></p>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>                            
                                        <asp:TemplateColumn>
                                            <HeaderTemplate>
                                                <p>
                                                    Tên</p>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <p>
                                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "NamePop").ToString())%></p>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderTemplate>
                                                <p>
                                                    Nội dung</p>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <p>
                                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ContentPop").ToString())%></p>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                         <asp:TemplateColumn>
                                            <HeaderTemplate>
                                                <p>
                                                    Vị trí</p>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <p>
                                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "PositionPop").ToString())%></p>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                          <%--<asp:TemplateColumn>
                                            <HeaderTemplate><p>Thời gian Tạo</p></HeaderTemplate>
                                            <ItemTemplate>
                                            <p><%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "dateset").ToString()%></P>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>--%>
                                        <asp:TemplateColumn>
                                            <HeaderTemplate>
                                                Sửa</HeaderTemplate>
                                            <ItemTemplate>
                                                <a href="Popup_Data.aspx?id=<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "id").ToString())%>&type=edit&status=<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "status").ToString())%>">
                                                    <img alt="" title=" <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "NamePop").ToString())%>"
                                                        src="../images/Edit.gif" height="20px" border="0" width="20px" style="cursor: pointer" />
                                                </a>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderImageUrl="~/images/button-xem.gif">
                                            <HeaderTemplate>
                                                Chi tiết</HeaderTemplate>
                                            <ItemTemplate>
                                                <a href="Popup_Data.aspx?id=<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "id").ToString())%>&type=edit&status=<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "status").ToString())%>">
                                                    <img alt="" title=" <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "NamePop").ToString())%>"
                                                        src="../images/button-xem.gif" style="cursor: pointer" />
                                                </a>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:ButtonColumn CommandName="Delete" Text="Xóa" HeaderText="Xóa" ItemStyle-Font-Bold="true"
                                            FooterStyle-Font-Size="14px">
                                            <FooterStyle Font-Size="14px"></FooterStyle>
                                            <ItemStyle Font-Bold="True"></ItemStyle>
                                        </asp:ButtonColumn>
                                        <asp:BoundColumn Visible="False"></asp:BoundColumn>
                                    </Columns>
                                    <PagerStyle Font-Bold="True" HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="lblTotalRecords" runat="server" Font-Bold="True" ForeColor="#004000"></asp:Label>
                    <table id="TABLE2" width="100%">
                        <tr>
                            <td style="height: 26px">
                                 <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ComboBoxInputHover_ClassicBlue"
                                    AutoPostBack="True" Width="108px" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            &nbsp;<asp:Button ID="cmdAdd" runat="server" CssClass="formSubmitBtn" Font-Bold="True"
                                    Text="Thêm mới" OnClick="cmdAdd_Click" Height="20px"></asp:Button>
                                     <asp:Button ID="cmdDelete" runat="server" CssClass="formSubmitBtn" Text="Xóa" Font-Bold="True"
                                    OnClick="cmdDelete_Click" Height="20px" Visible="true"></asp:Button>
                                            <asp:Button ID="cmdSetShowHome" runat="server" CssClass="formSubmitBtn" Text="Duyệt"
                                    Font-Bold="True" OnClick="cmdSetShowHome_Click" Height="20px"></asp:Button>
                                    <asp:Button
                                        ID="cmdRemove" runat="server" CssClass="formSubmitBtn" Text="Gỡ" Font-Bold="True"
                                        OnClick="cmdRemove_Click" Height="20px"></asp:Button>
                        </tr>
                    </table>
                    <asp:HiddenField runat="server" ID="hiddenToken" />
                </td>
            </tr>
        </tbody>
    </table>
    </form>
</body>
</html>

