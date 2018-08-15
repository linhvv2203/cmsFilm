<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ItemMovie_Groups_Data.aspx.cs"
    Inherits="OnKeeng_ItemMovie_Groups_Data" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <link href="../common/css/bootstrap-combined.min.css" rel="stylesheet" type="text/css" />
    <link href="../common/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
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
    <form id="form1" runat="server" class="form-horizontal">
        <div class="form-group" style="text-align: center;">
            <asp:Label ID="Label1" runat="server" CssClass="titlePage">THÊM MỚI OR CẬP NHẬT</asp:Label>
            <asp:Label ID="lblError" runat="server" Font-Size="10" ForeColor="Red"></asp:Label>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="ddlPartnerID">Đối tác:</label>
            <div class="col-sm-4">
                <asp:DropDownList ID="ddlPartnerID" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="ddlCategory">Chuyên mục:</label>
            <div class="col-sm-4">
                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="txtName">Tên Phim bộ:</label>
            <div class="col-sm-8">
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="txtTotalFilm">Tổng số tập:</label>
            <div class="col-sm-8">
                <asp:TextBox ID="txtTotalFilm" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="ImageFileImage">File ảnh đạt diện (.jpg)</label>
            <div class="col-sm-8">
                <asp:FileUpload ID="ImageFileImage" runat="server" onchange="readURL(this);" />
            </div>
        </div>
        <div class="form-group">
                <label class="control-label col-sm-2" for="datetimepicker" runat="server" id="dateName">DatePublish</label>
                <div id="datetimepicker" runat="server" class="input-append date col-sm-4">
                    <input id="inputDatetime" runat="server" type="text" class="form-control" />
                    <span class="add-on">
                        <i data-time-icon="icon-time" data-date-icon="icon-calendar"></i>
                    </span>
                </div>
            </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="txtDesc">Mô tả: </label>
            <div class="col-sm-8">
                <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>

            </div>
            <div class="col-sm-2">
                <asp:Image ID="imagedd" runat="server" Height="70px" Width="70px" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2" style="margin-left: 220px;">
                <asp:Button ID="cmdSave" runat="server" Text="Lưu" OnClick="cmdSave_Click" CssClass="btn btn-default"></asp:Button>
            </div>
        </div>
        <div>
            <asp:HiddenField ID="HiddenField1" runat="server" />
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
