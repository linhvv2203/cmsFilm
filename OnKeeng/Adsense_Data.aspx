<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Adsense_Data.aspx.cs" Inherits="OnKeeng_Adsense_Data" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
            <div class="col-sm-12">
                <asp:Label ID="Label1" runat="server" CssClass="titlePage">THÊM MỚI OR CẬP NHẬT</asp:Label>&nbsp;
                        <asp:Label ID="lblError" runat="server" Font-Size="10" ForeColor="Red"></asp:Label>
            </div>
            <div>
                <asp:Label ID="Label2" runat="server" Font-Size="10" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="ddlTypeShow">Chuyên mục:</label>
            <div class="col-sm-4">
                <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="txtName">Tiêu đề:</label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="txtLnk">Link quảng cáo:</label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtLnk" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="ImageFileImage">File ảnh đạt diện (.jpg):</label>
            <div class="col-sm-2">
                <asp:FileUpload ID="ImageFileImage" runat="server" onchange="readURL(this);" ToolTip="Kích thước 1024x96" 
                    size="48" /> 
            </div>
            <div class="col-sm-4">
                <strong style="color:red;">(Web: Top, Bot: 1024x96; Right :300x750 )</strong>
                
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="txtDesc">Mô tả:</label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Height="200"
                    CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="ddlPosition">Position:</label>
            <div class="col-sm-4">
                <asp:DropDownList runat="server" ID="ddlPosition" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="ddlResponsive">Responsive:</label>
            <div class="col-sm-4">
                <asp:DropDownList runat="server" ID="ddlResponsive" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="ddlTypeShow">Type Show:</label>
            <div class="col-sm-4">
                <asp:DropDownList runat="server" ID="ddlTypeShow" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="txtExpired" runat="server">Date start:</label>
            <div id="datetimepickerStart" runat="server" class="input-append date col-sm-3">
                <input id="txtDateStart" runat="server" type="text" class="form-control" />
                <span class="add-on">
                    <i data-time-icon="icon-time" data-date-icon="icon-calendar"></i>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="" runat="server">Date end:</label>
            <div id="datetimepickerEnd" runat="server" class="input-append date col-sm-3">
                <input id="txtDateEnd" runat="server" type="text" class="form-control" />
                <span class="add-on">
                    <i data-time-icon="icon-time" data-date-icon="icon-calendar"></i>
                </span>
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
