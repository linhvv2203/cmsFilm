<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FilmGroups.aspx.cs" Inherits="OnKeeng_FilmGroups" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Language" content="en-us">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <link href="../Common/style.css" type="text/css" rel="stylesheet">
    <link href="../tip/style.css" type="text/css" rel="stylesheet">
    <link href="../tip/style_tooltips.css" type="text/css" rel="stylesheet">

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>


    <%--<script language="javascript" src="../common/common.js"></script>--%>

    <script language="JavaScript">

        if (document.all) {
            document.onkeydown = function () {
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
    <div class="">
        <form id="form1" runat="server" class="form-horizontal">
            <h2>Quản lý phim bộ</h2>
            <div class="form-group">
                <div class="col-sm-12">
                    
                    Tìm theo:<asp:DropDownList ID="ddlSearch" runat="server" CssClass="ComboBoxInputHover_ClassicBlue"
                        Width="90px">
                    </asp:DropDownList>
                    Từ khóa:<asp:TextBox ID="txtKeyword" runat="server" Width="159px"></asp:TextBox>&nbsp;
                    <asp:Button ID="cmdSearch" runat="server" CssClass="formSubmitBtn" Text="Tìm kiếm"
                        OnClick="cmdSearch_Click1" Style="font-weight: bold; height: 20px;"></asp:Button>
                    <asp:Label ID="lbError" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12">
                    <asp:DataGrid ID="dgrCommon" runat="server" CssClass="GridLabel" Width="100%" AlternatingItemStyle-BackColor="lightgray"
                        HeaderStyle-BackColor="#99ccff" PagerStyle-HorizontalAlign="Left" PagerStyle-Mode="NumericPages"
                        CellPadding="1" AllowPaging="True" BorderColor="DarkGray" BorderStyle="Inset"
                        PagerStyle-PageButtonCount="50" PageSize="10" OnDeleteCommand="Grid_AddPhim" OnEditCommand="ItemsGrid_TapPhim"
                        OnSelectedIndexChanged="dgrCommon_SelectedIndexChanged" OnCancelCommand="dgrCommon_Cancel" AutoGenerateColumns="False">
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
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    Ảnh
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <img onmouseover="tooltip.show('<IMG src=\'<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FolderName").ToString())%>/<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ItemImage").ToString())%>\'  >')"
                                        onmouseout="tooltip.hide();" src="<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FolderName").ToString())%>/<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ItemImage").ToString())%>"
                                        height="20px" width="20px" />
                                    <%--<img height="20px" width="20px" />--%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <p>
                                        ID
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
                                        Duyệt
                                    </p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p>
                                        <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Status").ToString())%>
                                    </p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <p>
                                        isHome
                                    </p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p>
                                        <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "isHome").ToString())%>
                                    </p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <p>
                                        Datepublish
                                    </p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p>
                                        <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Datepublish").ToString())%>
                                    </p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:ButtonColumn CommandName="Cancel" HeaderText="Edit" Text="Edit"></asp:ButtonColumn>
                            <asp:ButtonColumn CommandName="Edit" HeaderText="Tập phim" Text="Tập phim"></asp:ButtonColumn>
                            <asp:ButtonColumn CommandName="Delete" HeaderText="add Phim" Text="add Phim"></asp:ButtonColumn>
                            <asp:BoundColumn Visible="False"></asp:BoundColumn>
                        </Columns>
                        <PagerStyle Font-Bold="True" HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-12">
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ComboBoxInputHover_ClassicBlue"
                        AutoPostBack="True" Width="108px" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Button ID="btnCmdAdd" runat="server" CssClass="button_admin" OnClick="btnCmdAdd_Click" Text="Thêm mới" />
                    <asp:Button ID="btnDelete" runat="server" Text="Xóa" OnClick="btnDelete_Click" CssClass="button_admin"
                        Width="80"></asp:Button>
                    <asp:Button ID="btnDuyet" runat="server" Text="Duyệt" OnClick="btnDuyet_Click" CssClass="button_admin"
                        Width="80" />
                    <asp:Button ID="btnGoDuyet" runat="server" Text="Gỡ Duyệt" OnClick="btnGoDuyet_Click" CssClass="button_admin"
                        Width="80" />
                    <asp:Button ID="btnSetHome" runat="server" Text="Đặt Home" OnClick="btnSetHome_Click" CssClass="button_admin"
                        Width="80" />
                    <asp:Button ID="btnGoHome" runat="server" Text="Gỡ Home" OnClick="btnHideHome_Click" CssClass="button_admin"
                        Width="80" />
                    <asp:Label ID="lblMess" ForeColor="Red" Font-Bold="true" runat="server" />
                </div>
            </div>
            <div class="form-group">
                <asp:HiddenField ID="hiddenTime" runat="server" />
                <asp:HiddenField ID="hiddenToken" runat="server" />
            </div>


            <script>

                function add_click() {
                    alert(document.getElementById("id-film").value);
                }
            </script>

        </form>
    </div>
</body>
</html>
