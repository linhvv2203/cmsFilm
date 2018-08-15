<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListFilm_Synchronous.aspx.cs" Inherits="OnKeeng_ListFilm_Synchronous" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
</head>
<body class="BODY" leftmargin="3" topmargin="1">
    <form id="form1" runat="server" class="form-horizontal">
        <div class="form-group">
            <div class="col-sm-12">
                <asp:Label ID="Label1" runat="server" Font-Size="10pt" Font-Bold="True">DANH SÁCH VIDEO</asp:Label>
                <asp:Label ID="lblError" runat="server" Font-Size="10" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-12">
                Từ khóa:<asp:TextBox ID="txtKeyword" runat="server" Width="159px"></asp:TextBox>&nbsp;<asp:Button
                    ID="cmdSearch" runat="server" CssClass="formSubmitBtn" Text="Tìm kiếm" Font-Bold="True"
                    OnClick="cmdSearch_Click" Height="20px"></asp:Button>
                <asp:DropDownList ID="ddlType" runat="server" CssClass="ComboBoxInputHover_ClassicBlue"
                    AutoPostBack="True" Width="" OnSelectedIndexChanged="ddlType_OnSelectedIndexChanged" >
                </asp:DropDownList>
                <asp:Button runat="server" ID="btnSynchronous" Text="Đồng bộ" OnClick="btnSynchronous_Click" CssClass="formSubmitBtn" />
                <asp:Button runat="server" ID="btnDownload" Text="Download" OnClick="btnDownload_Click" CssClass="formSubmitBtn" />
                <asp:Button runat="server" ID="btnDelete" Text="Xóa" OnClick="btnDelete_Click" CssClass="formSubmitBtn" />
            </div>
            
        </div>
        <div class="form-group">
            <div class="col-sm-12">
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
                                    Name
                                </p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <p>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "name").ToString())%>
                                </p>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderTemplate>
                                <p>
                                    Description
                                </p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <p>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "description").ToString())%>
                                </p>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn>
                            <HeaderTemplate>
                                <p>
                                    published_at
                                </p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <p>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "published_at").ToString())%>
                                </p>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderTemplate>
                                <p>
                                    process_status
                                </p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <p>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "process_status").ToString())%>
                                </p>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn Visible="False"></asp:BoundColumn>
                    </Columns>
                    <PagerStyle Font-Bold="True" HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
                </asp:DataGrid>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-12">
                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ComboBoxInputHover_ClassicBlue"
                    AutoPostBack="True" Width="" OnSelectedIndexChanged="ddlStatus_OnSelectedIndexChanged" >
                </asp:DropDownList>
            </div>
        </div>
    </form>
</body>
</html>
