<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ItemMovieGroup_addPartByitemovieID.aspx.cs"
    Inherits="OnKeeng_ItemMovieGroup_addPartByitemovieID" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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

    <link href="../common/css/bootstrap-combined.min.css" rel="stylesheet" type="text/css" />
    <link href="../common/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />


    <script src="../common/js/jquery.min.js" type="text/javascript"></script>
    <script src="../common/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../common/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="../common/js/bootstrap-datetimepicker.pt-BR.js" type="text/javascript"></script>

    <script language="JavaScript">
        if (document.all) {
            document.onkeydown = function () {
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

                reader.onload = function (e) {
                    var img = document.getElementById("imagedd");
                    img.src = e.target.result;
                    img.style.width = 100;
                    img.style.height = 100;
                };

                reader.readAsDataURL(input.files[0]);
            }
        }
        function OnActor() {
            window.open('Actor.aspx', '', 'top=0,left=0,height=550,width=2040,scrollbars=no,toolbar=yes')
        }

        function OnDirector() {
            window.open('Director.aspx', '', 'top=0,left=0,height=550,width=2040,scrollbars=no,toolbar=yes')
        }
    </script>

    <style type="text/css">
        .style4 {
            font-family: Arial, Verdana, Tahoma, Sans-Serif;
            font-size: 13px;
            font-weight: bold;
            color: #000000;
            cursor: default;
            width: 155px;
        }

        .style5 {
            font-family: Arial, Verdana, Tahoma, Sans-Serif;
            font-size: 13px;
            font-weight: bold;
            color: #000000;
            cursor: default;
            height: 28px;
            width: 155px;
        }

        .style6 {
            font-family: Arial, Verdana, Tahoma, Sans-Serif;
            font-size: 13px;
            font-weight: bold;
            color: #000000;
            cursor: default;
            height: 8px;
            width: 155px;
        }
    </style>
    <link href="../Styles/DataGridStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="Form1" runat="server">
        <table cellspacing="1" cellpadding="1" width="100%" border="0" style="border-collapse: collapse; border: solid 1px #aaaaaa;">
            <tbody>
                <tr>
                    <td width="100%" style="width: 100%; border-bottom: solid 1px #aaaaaa" valign="middle"
                        height="30">
                        <b>
                            <asp:Label ID="Label1" runat="server" CssClass="titlePage">THÊM MỚI PART</asp:Label>
                            &nbsp;
                        <asp:Label ID="lblError" runat="server" Font-Size="10" ForeColor="Red"></asp:Label> 
                        </b>
                    </td>
                </tr>
                <tr>
                    <td width="100%">
                        <table id="AutoNumber2" cellspacing="2" cellpadding="1" width="100%" border="0">
                            <tr>
                                <td class="style5">Người Upload: 
                                </td>
                                <td style="height: 28px">
                                    <font style="font-weight: normal; font-size: 11px; cursor: default; color: #8c8c8c; font-family: Arial, Verdana, Tahoma, Sans-Serif">
                                        <asp:TextBox ID="txtUserName" runat="server" CssClass="textboxStyle" Width="96px"></asp:TextBox></font>
                                </td>
                            </tr>
                            <tr>
                                <td class="style6">Tên tập:
                                </td>
                                <td style="height: 8px;">
                                    <asp:DropDownList runat="server" ID="ddlMovie" Width="614px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="style6">Tiêu đề part:
                                </td>
                                <td style="height: 8px">
                                    <font style="font-weight: normal; font-size: 11px; cursor: default; color: #8c8c8c; font-family: Arial, Verdana, Tahoma, Sans-Serif">
                                        <asp:TextBox ID="txtNamePart" runat="server" CssClass="textboxStyle" Width="600px"></asp:TextBox></font>

                                </td>
                            </tr>
                            <tr>
                                <td class="style6">Tập số:
                                </td>
                                <td style="height: 8px">
                                    <font style="font-weight: normal; font-size: 11px; cursor: default; color: #8c8c8c; font-family: Arial, Verdana, Tahoma, Sans-Serif">
                                        <asp:TextBox ID="txtepisode" runat="server" CssClass="textboxStyle" Width="96px">0</asp:TextBox></font>
                                </td>
                            </tr>
                            <tr>
                                <td class="style5">File Video (.mp4)<br />
                                </td>
                                <td style="height: 28px">
                                    <asp:FileUpload ID="ImageFileMp4" runat="server" size="48" class="button_admin" />
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style4"></td>
                                <td nowrap colspan="1">
                                    <asp:Button ID="cmdSave" runat="server" Text="Save" OnClick="cmdSave_Click" CssClass="button_admin"
                                        Width="80px"></asp:Button>
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td width="100%">
                        <table id="Table1" cellspacing="2" cellpadding="1" width="100%" border="0">
                            <tr>
                                <td class="style6">&nbsp;DS Phim đang nhập&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style5">
                                    <asp:DataGrid ID="dgrCommon" runat="server" CssClass="GridLabel" Width="100%" AlternatingItemStyle-BackColor="lightgray"
                                        HeaderStyle-BackColor="#99ccff" PagerStyle-HorizontalAlign="Left" PagerStyle-Mode="NumericPages"
                                        CellPadding="1" AllowPaging="True" BorderColor="DarkGray" BorderStyle="Inset"
                                        PagerStyle-PageButtonCount="50" PageSize="10" OnSelectedIndexChanged="dgrCommon_SelectedIndexChanged"
                                        AutoGenerateColumns="False">
                                        <PagerStyle CssClass="cssPager" />
                                        <SelectedItemStyle BackColor="#FFC0C0"></SelectedItemStyle>
                                        <EditItemStyle></EditItemStyle>
                                        <AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
                                        <ItemStyle></ItemStyle>
                                        <HeaderStyle Font-Names="Arial" Font-Bold="True" BorderStyle="None" BackColor="#d2d7da"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <HeaderTemplate>
                                                    <div align="center">
                                                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAll(this)">
                                                    </div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkAllDelete" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <%-- <asp:TemplateColumn>
                                            <HeaderTemplate>
                                                Ảnh</HeaderTemplate>
                                            <ItemTemplate>
                                                <img onmouseover="tooltip.show('<IMG src=\'<%# DataBinder.Eval(Container.DataItem, "ItemImage")%>\'  >')"
                                                    onmouseout="tooltip.hide();" src="<%# DataBinder.Eval(Container.DataItem, "ItemImage")%>"
                                                    height="20px" width="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>--%>
                                            <asp:TemplateColumn>
                                                <HeaderTemplate>
                                                    <p>
                                                        ID VIDEO
                                                    </p>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <p>
                                                        <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ID").ToString())%>
                                                    </p>
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
                                                        <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ItemName").ToString())%>
                                                    </p>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderTemplate>
                                                    <p>
                                                        Status
                                                    </p>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <p>
                                                        <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ItemStatus").ToString())%>
                                                    </p>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderTemplate>
                                                    <p>
                                                        Ngày nhập
                                                    </p>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <p>
                                                        <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ItemDate").ToString())%>
                                                    </p>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderTemplate>
                                                    <p>
                                                        Người nhập
                                                    </p>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <p>
                                                        <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Userupload").ToString())%>
                                                    </p>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <%--<asp:TemplateColumn>
                                            <HeaderTemplate>
                                                Sửa</HeaderTemplate>
                                            <ItemTemplate>
                                                <a href="ItemMovie_Data.aspx?id=<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "id").ToString())%>&type=edit&status=<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ItemStatus").ToString())%>">
                                                    <img alt="" title=" <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ItemName").ToString())%>"
                                                        src="../images/Edit.gif" height="20px" border="0" width="20px" />
                                                </a>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>--%>
                                            <asp:BoundColumn Visible="False"></asp:BoundColumn>
                                        </Columns>
                                        <PagerStyle Font-Bold="True" HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr>
                    <td class="style5">&nbsp;<asp:Button ID="cmdSetShowHome" runat="server" Text="Duyệt"
                        OnClick="cmdSetShowHome_Click"></asp:Button>&nbsp;<asp:Button ID="cmdDelete"
                            runat="server" Text="Xóa" OnClick="cmdDelete_Click"></asp:Button>
                        &nbsp;
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:HiddenField ID="HiddenField1" runat="server" />
    </form>
</body>
</html>
