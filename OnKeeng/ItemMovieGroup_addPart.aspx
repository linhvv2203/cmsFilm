<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ItemMovieGroup_addPart.aspx.cs"
    Inherits="OnKeeng_ItemMovieGroup_addPart" %>

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
    
    <link href="../common/css/bootstrap-combined.min.css" rel="stylesheet" type="text/css" />
    <link href="../common/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    

    <script src="../common/js/jquery.min.js" type="text/javascript"></script>
    <script src="../common/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../common/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="../common/js/bootstrap-datetimepicker.pt-BR.js" type="text/javascript"></script>

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
<body class="BODY" leftmargin="3" topmargin="1">
    <form id="Form1" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tbody>
            <tr>
                <td width="100%" bgcolor="#F0EDE1" valign="middle" height="30">
                    &nbsp; <font color="#000000" size="3"><b>
                        <asp:Label ID="Label1" runat="server" Font-Size="10pt" Font-Bold="True">DANH SÁCH PART MOVIE</asp:Label>
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
                            <td>
                                <asp:Label ID="lblMessage" ForeColor="Red" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="" height="">
                                Phim:
                                <asp:DropDownList ID="ddlItemMovie" runat="server" CssClass="ComboBoxInputHover_ClassicBlue"
                                    Width="100px" AutoPostBack="True">
                                </asp:DropDownList>
                                &nbsp;&nbsp;
                                <asp:Button ID="btnAddPart" runat="server" OnClick="btnAddPart_Click"
                                    Text="Add Part" />
                                Tên phim:
                                <asp:TextBox ID="txtSearch" runat="server" />
                                <asp:Button ID="btnSeach" runat="server" OnClick="btnSearch_Click" Text="Tìm kiếm" />
                            </td>
                </td>
            </tr>
    </table>
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
                                <p>
                                    ItemName
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
                                    ItemFile
                                </p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <p>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ItemFile").ToString())%></p>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderTemplate>
                                <p>
                                    Episode
                                </p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <p>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Episode").ToString())%></p>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderTemplate>
                                <p>
                                    ItemDate
                                </p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <p>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ItemDate").ToString())%></p>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn Visible="False"></asp:BoundColumn>
                    </Columns>
                    <PagerStyle Font-Bold="True" HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
                </asp:DataGrid>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblTotalRecords" runat="server" Font-Bold="True" ForeColor="#004000"></asp:Label>
    </form>
</body>
</html>
