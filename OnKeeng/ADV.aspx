<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ADV.aspx.cs" Inherits="OnKeeng_ADV" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
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
    <form id="Form1" runat="server" class="form-horizontal">
        <div class="form-group">
            <div class="col-sm-12">
                <asp:Label ID="Label1" runat="server" Font-Size="10pt" Font-Bold="True">DANH SÁCH Quảng Cáo</asp:Label>
                <asp:Label ID="lblError" runat="server" Font-Size="10" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-12">
                Tìm theo:<asp:DropDownList ID="ddlImages" runat="server" CssClass="ComboBoxInputHover_ClassicBlue"
                    Width="90px" OnSelectedIndexChanged="ddlImages_SelectedIndexChanged">
                </asp:DropDownList>
                Từ khóa:<asp:TextBox ID="txtKeyword" runat="server" Width="159px"></asp:TextBox>&nbsp;<asp:Button
                    ID="cmdSearch" runat="server" CssClass="formSubmitBtn" Text="Tìm kiếm" Font-Bold="True"
                    OnClick="cmdSearch_Click1" Height="20px"></asp:Button>
                <asp:Label ID="lbError" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
            </div>
        </div>
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
                                    <input type="CheckBox"  onclick="SelectAlldgrTemp(this)">
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
                                <img onmouseover="tooltip.show('<IMG src=\'<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "imagePath").ToString())%>\'  >')"
                                    onmouseout="tooltip.hide();" src="<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "imagePath").ToString())%>"
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
                                <%--<asp:ImageButton ID="btnEdit" runat="server" 
                                                    OnClick="btnEdit_Click" ImageUrl="../images/Edit.gif" />--%>
                                <a href="ADV_Data.aspx?id=<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "id").ToString())%>&type=edit&status=<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Status").ToString())%>">
                                    <img alt="" title=" <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Name").ToString())%>"
                                        src="../images/Edit.gif" height="20px" border="0" width="20px" />
                                </a>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:ButtonColumn CommandName="Delete" Text="Xóa" HeaderText="Xóa" ItemStyle-Font-Bold="true" CausesValidation="true"
                            FooterStyle-Font-Size="14px" ButtonType="PushButton">
                            <FooterStyle Font-Size="14px"></FooterStyle>
                            <ItemStyle Font-Bold="True"></ItemStyle>
                        </asp:ButtonColumn>
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
                <asp:Button ID="btnAdd" runat="server" CssClass="formSubmitBtn" Text="Add" Font-Bold="True"
                    OnClick="btnAdd_Click" Height="20px"></asp:Button>
                <asp:Button ID="btnDuyet" runat="server" CssClass="formSubmitBtn" Text="Duyệt" Font-Bold="True"
                    OnClick="btnDuyet_Click" Height="20px"></asp:Button>
                <asp:Button ID="btnGoDuyet" runat="server" CssClass="formSubmitBtn" Text="Gỡ Duyệt" Font-Bold="True"
                    OnClick="btnGoDuyet_Click" Height="20px"></asp:Button>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-12">
                <asp:Label ID="lblTotalRecords" runat="server" Font-Bold="True" ForeColor="#004000"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
