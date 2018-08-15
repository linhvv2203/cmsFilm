<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListMovieView.aspx.cs" Inherits="OnKeeng_ListMovieView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../common/css/bootstrap-combined.min.css" rel="stylesheet" type="text/css" />
    <link href="../common/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <script src="../common/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="../common/js/bootstrap-datetimepicker.pt-BR.js" type="text/javascript"></script>
    <style>
        input[type="text"] {
            height: 30px;
        }

        .add-on {
            height: 30px !important;
        }
    </style>
</head>
<body>
    <div class="container">
        <h2 style="text-align: center;">THỐNG KÊ LƯỢT VIEW</h2>
        <form id="form1" runat="server" class="form-inline">
           <div class="form-group">
                <label class="" for="ddlPartnerID">Đối tác:</label>
                <asp:DropDownList ID="ddlPartnerID" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPartnerID_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label class="" for="ddlCategory">Thể loại:</label>
                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label class="" for="txtExpired" runat="server">Date start:</label>
                <div id="datetimepickerStart" runat="server" class="input-append date">
                    <input id="txtDateStart" runat="server" type="text" class="form-control" />
                    <span class="add-on">
                        <i data-time-icon="icon-time" data-date-icon="icon-calendar"></i>
                    </span>
                </div>
            </div>
            <div class="form-group">
                <label class="" for="" runat="server">Date end:</label>
                <div id="datetimepickerEnd" runat="server" class="input-append date">
                    <input id="txtDateEnd" runat="server" type="text" class="form-control" />
                    <span class="add-on">
                        <i data-time-icon="icon-time" data-date-icon="icon-calendar"></i>
                    </span>
                </div>
            </div>
            <div class="form-group">
                <asp:Button ID="btnSeach" runat="server" Text="Tìm kiếm" OnClick="btnSeach_Click"
                    CssClass="btn btn-default"></asp:Button>
                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click"
                    CssClass="btn btn-default"></asp:Button>
                
            </div>
            <div class="form-group" style="margin-top:10px;">
                <asp:Button ID="btnExportExcel" runat="server" Text="ExportExcel" OnClick="btnExportExcel_Click"
                    CssClass="btn btn-default"></asp:Button>
            </div>
            <div style="margin-top: 30px;">
                <asp:DataGrid ID="dgrCommon" runat="server" CssClass="GridLabel" Width="100%" AlternatingItemStyle-BackColor="lightgray"
                    HeaderStyle-BackColor="#99ccff" PagerStyle-HorizontalAlign="Left" PagerStyle-Mode="NumericPages"
                    CellPadding="1" AllowPaging="True" BorderColor="DarkGray" BorderStyle="Inset" OnPageIndexChanged="dgrCommon_PageIndexChanged"
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
                                    Time
                                </p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <p>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "times").ToString())%>
                                </p>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn>
                            <HeaderTemplate>
                                <p>
                                    Total
                                </p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <p>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "total").ToString())%>
                                </p>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <PagerStyle Font-Bold="True" HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
                </asp:DataGrid>

                <asp:Label runat="server" ID="lblTotalRecords"></asp:Label>
            </div>
        </form>
    </div>
    <script type="text/javascript">

        $('#<%=datetimepickerStart.ClientID %>').datetimepicker({
            format: 'yyyy-MM-dd',
            //language: 'pt-BR'
        });
        $('#<%=datetimepickerEnd.ClientID %>').datetimepicker({
            format: 'yyyy-MM-dd',// hh:mm:ss
            //language: 'pt-BR'
        });
    </script>
</body>
</html>
