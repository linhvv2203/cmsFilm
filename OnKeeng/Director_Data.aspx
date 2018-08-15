<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Director_Data.aspx.cs" Inherits="OnPhim_Director_Data" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title></title>
    <meta http-equiv="Content-Language" content="vi">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <%--<link href="../Common/style.css" type="text/css" rel="stylesheet">
    <link href="../tip/style.css" type="text/css" rel="stylesheet">
    <link href="../tip/style_tooltips.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="../tip/script.js" type="text/javascript"></script>

    <script language="javascript" src="../tip/javascript.js" type="text/javascript"></script>

    <script language="javascript" src="../tip/tooltips.js" type="text/javascript"></script>

    <script language="javascript" src="../common/common.js"></script>--%>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

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
    </script>
</head>
<body bgcolor="#ffffff" leftmargin="3" topmargin="1">
    <form id="Form1" runat="server" class="form-horizontal">

        <div class="form-group" style="text-align:center;">
            <asp:Label ID="Label1" runat="server" Style="font-size:18px;">THÊM MỚI OR CẬP NHẬT</asp:Label>
            <asp:Label ID="lblError" runat="server" Font-Size="10" ForeColor="Red"></asp:Label>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="txtName">Tên Director:</label>
            <div class="col-sm-4">
                <asp:TextBox runat="server" ID="txtName" CssClass="form-control"></asp:TextBox>
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
            <label class="control-label col-sm-2" for="txtDesc">Mô tả:</label>
            <div class="col-sm-4">
                <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine"
                    CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-4">
                <asp:Button ID="cmdSave" runat="server" Text="Lưu" OnClick="cmdSave_Click" CssClass="btn btn-default"
                    Width="80"></asp:Button>
            </div>
        </div>
        <div>
            <asp:HiddenField ID="hiddenTime" runat="server" />
            <asp:HiddenField ID="hiddenToken" runat="server" />
        </div>
    </form>
</body>
</html>

