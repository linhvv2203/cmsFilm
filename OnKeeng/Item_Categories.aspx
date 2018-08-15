<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Item_Categories.aspx.cs"
    Inherits="CATEGORIES_CHANELS_Item_Categories" %>

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
        .cssPager span {
            background-color: Yellow;
        }

        .style4 {
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
    <form id="Form1" runat="server" class="form-horizontal">

        <div class="form-group">
            <div class="col-sm-12">
                <asp:Label ID="Label1" runat="server" Font-Size="10pt" Font-Bold="True">DANH SÁCH CATEGORIES</asp:Label>
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
                    PagerStyle-PageButtonCount="50" OnSelectedIndexChanged="dgrCommon_SelectedIndexChanged"
                    DataMember="id" OnDeleteCommand="dgrCommon_DeleteCommand" OnPageIndexChanged="dgrCommon_PageIndexChanged"
                    AutoGenerateColumns="False">
                    <PagerStyle CssClass="cssPager" />
                    <SelectedItemStyle BackColor="#FFC0C0"></SelectedItemStyle>
                    <AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
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
                                <img onmouseover="tooltip.show('<IMG src=\'<%=Server.HtmlEncode(VatLid.Variables.ImageUrl_Categories)%><%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "url_images").ToString())%>\'  >')"
                                    onmouseout="tooltip.hide();" src="<%=Server.HtmlEncode(VatLid.Variables.ImageUrl_Categories)%><%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "url_images").ToString())%>"
                                    height="20px" width="20px" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderTemplate>
                                Code
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Literal ID="txtID" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ID").ToString())%>' />
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
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CategoryName").ToString())%>
                                </p>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderTemplate>
                                <p>
                                    Order
                                </p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtOrder" runat="server" Text='<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Order").ToString())%>' />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderTemplate>
                                <p>
                                    Set top
                                </p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <p>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "setTop").ToString())%>
                                </p>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderTemplate>
                                Sửa
                            </HeaderTemplate>
                            <ItemTemplate>
                                <a href="Categories_Data.aspx?module=837&id=<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "id").ToString())%>&type=edit&status=<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "status").ToString())%>">
                                    <img alt="" title=" <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "categoryname").ToString())%>"
                                        src="../images/Edit.gif" height="20px" border="0" width="20px" style="cursor: pointer" />
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
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-12">
                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ComboBoxInputHover_ClassicBlue"
                    AutoPostBack="True" Width="108px" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;<asp:Button ID="cmdAdd" runat="server" CssClass="formSubmitBtn" Font-Bold="True"
                    Text="Thêm mới" OnClick="cmdAdd_Click" Height="20px"></asp:Button>
                <asp:Button ID="cmdDelete" runat="server" CssClass="formSubmitBtn" Text="Xóa" Font-Bold="True"
                    OnClick="cmdDelete_Click" Height="20px" Visible="true"></asp:Button>
                <asp:Button ID="cmdSetShowHome" runat="server" CssClass="formSubmitBtn" Text="Duyệt"
                    Font-Bold="True" OnClick="cmdSetShowHome_Click" Height="20px"></asp:Button><asp:Button
                        ID="cmdRemove" runat="server" CssClass="formSubmitBtn" Text="Gỡ" Font-Bold="True"
                        OnClick="cmdRemove_Click" Height="20px"></asp:Button>
                &nbsp;&nbsp;<asp:Button ID="btnSetTopMenu" runat="server" CssClass="formSubmitBtn"
                    Font-Bold="True" Height="20px" OnClick="btnSetTopMenu_Click" Text="SetTop" />
                &nbsp;&nbsp;<asp:Button ID="btnUnSetMenu" runat="server" CssClass="formSubmitBtn"
                    Font-Bold="True" Height="20px" OnClick="btnUnSetMenu_Click" Text="UnSettop" />
                &nbsp;&nbsp;<asp:Button ID="btnSetOrder" runat="server" CssClass="formSubmitBtn"
                    Font-Bold="True" Height="20px" OnClick="btnSetOrder_Click" Text="SetOrder" />
                <asp:HiddenField ID="hiddenToken" runat="server" />
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
