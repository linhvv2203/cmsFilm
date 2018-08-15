<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IntroManage.aspx.cs" Inherits="OnKeeng_IntroManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Language" content="en-us">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <link href="../Common/style.css" type="text/css" rel="stylesheet">

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

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
                    if ((document.forms[0].elements[i].type == 'checkbox') && (document.forms[0].elements[i].name.indexOf('dgrCommon') > -1)) {
                        document.forms[0].elements[i].checked = true;
                    }
                }
            }

            else {

                var i;

                for (i = 0; i < document.forms[0].elements.length; i++) {

                    if ((document.forms[0].elements[i].type == 'checkbox') && (document.forms[0].elements[i].name.indexOf('dgrCommon') > -1)) {

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
            <h2>Intro Manage</h2>

            <div class="form-group">
                <div class="col-sm-12">
                    <asp:DataGrid ID="dgrCommon" runat="server" CssClass="GridLabel" Width="100%" AlternatingItemStyle-BackColor="lightgray"
                        HeaderStyle-BackColor="#99ccff" PagerStyle-HorizontalAlign="Left" PagerStyle-Mode="NumericPages"
                        CellPadding="1" AllowPaging="True" BorderColor="DarkGray" BorderStyle="Inset"
                        PagerStyle-PageButtonCount="50" PageSize="10" OnSelectedIndexChanged="dgrCommon_SelectedIndexChanged"
                        AutoGenerateColumns="False" OnDeleteCommand="dgrCommon_DeleteCommand">
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
                                        <input type="CheckBox" onclick="SelectAlldgrTemp(this)">
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAllDelete" runat="server" />
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
                                        <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Name").ToString())%>
                                    </p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <p>
                                        Trạng thái
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
                                        Sửa
                                    </p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <a href="ItemmovieIntro_Add.aspx?id=<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "id").ToString())%>&type=edit&status=<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Status").ToString())%>">
                                        <img alt="" title=" <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Name").ToString())%>"
                                            src="../images/Edit.gif" height="20px" border="0" width="20px" />
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <%--<asp:ButtonColumn CommandName="Delete" Text="Xóa" HeaderText="Xóa" ItemStyle-Font-Bold="true" CausesValidation="true"
                                FooterStyle-Font-Size="14px" ButtonType="PushButton">
                                <FooterStyle Font-Size="14px"></FooterStyle>
                                <ItemStyle Font-Bold="True"></ItemStyle>
                            </asp:ButtonColumn>--%>
                            <asp:BoundColumn Visible="False"></asp:BoundColumn>

                        </Columns>
                        <PagerStyle Font-Bold="True" HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                </div>

                <div class="form-group">
                    <div class="col-sm-12">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ComboBoxInputHover_ClassicBlue"
                            AutoPostBack="True" Width="108px" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Button ID="btnAdd" runat="server" CssClass="formSubmitBtn" Text="Add" Font-Bold="True"
                            OnClick="btnAdd_Click" Height="20px"></asp:Button>
                        
                    </div>
                </div>
            </div>

        </div>
    </form>
</body>
</html>
