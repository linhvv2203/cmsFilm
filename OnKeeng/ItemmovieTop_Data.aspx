<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ItemmovieTop_Data.aspx.cs" Inherits="OnKeeng_ItemmovieTop_Data" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <meta http-equiv="Content-Language" content="vi">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
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
    <form id="Form1" runat="server" class="form-horizontal">
        <div class="form-group">
            <div class="col-sm-12" style="text-align:center;">
                <asp:Label ID="Label1" runat="server" CssClass="titlePage" >THÊM MỚI OR CẬP NHẬT</asp:Label>&nbsp;
                        <asp:Label ID="lblError" runat="server" Font-Size="10" ForeColor="Red"></asp:Label>
            </div>
            <div> <asp:Label ID="Label2" runat="server" Font-Size="10" ForeColor="Red"></asp:Label> </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="txtName">Tiêu đề:</label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="txtLnk">Link flash hot:</label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtLnk" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="txtPosition">Số thứ tự:</label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtPosition" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="FUImageWap">File ảnh flash WEB (.jpg):</label>
            <div class="col-sm-2">
                <asp:FileUpload ID="FUImageWeb" runat="server" onchange="readURL(this);"
                    size="48" /> 
            </div>
            <div class="col-sm-2">
                <span style="color:red;">(kích thước 2048x878)</span>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="FUImageWap">File ảnh fash WAP,APP (.jpg):</label>
            <div class="col-sm-2">
                <asp:FileUpload ID="FUImageWap" runat="server" onchange="readURL(this);"
                    size="48" />
            </div>
            <div class="col-sm-2">
                <span style="color:red;">(kích thước 1000x560) </span>
            </div>
        </div>
        <div class="form-group">
                <label class="control-label col-sm-2" for="datetimepicker" runat="server" id="dateName">Thời gian đăng:</label>
                <div id="datetimepicker" runat="server" class="input-append date col-sm-4">
                    <input id="inputDatetime" runat="server" type="text" class="form-control" />
                    <span class="add-on">
                        <i data-time-icon="icon-time" data-date-icon="icon-calendar"></i>
                    </span>
                </div>
            </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="ddlStatus">Trạng Thái:</label>
            <div class="col-sm-4">
                <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="false" CssClass="form-control">
                                        </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-12">
                <div class="col-sm-offset-2" style="margin-left: 160px;">
                    <asp:Button ID="cmdSave" runat="server" Text="Lưu" OnClick="cmdSave_Click" CssClass="btn btn-default"></asp:Button>
                </div>
            </div>
        </div>
        <div class="form-group">
            <asp:HiddenField ID="hiddenTime" runat="server" />
            <asp:HiddenField ID="hiddenToken" runat="server" />
        </div>
    </form>
    <script type="text/javascript">
        $('#<%=datetimepicker.ClientID %>').datetimepicker({
            format: 'yyyy-MM-dd hh:mm:ss',
            //language: 'pt-BR'
        });
    </script>
</body>
</html>
