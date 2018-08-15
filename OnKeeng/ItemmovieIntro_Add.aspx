<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ItemmovieIntro_Add.aspx.cs" Inherits="OnKeeng_ItemmovieIntro_Add" %>

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
    <script lang="JavaScript">
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
        function OnActor() {
            window.open('Actor.aspx', '', 'top=0,left=0,height=550,width=2040,scrollbars=no,toolbar=yes')
        }

        function OnDirector() {
            window.open('Director.aspx', '', 'top=0,left=0,height=550,width=2040,scrollbars=no,toolbar=yes')
        }
        function OnCountry() {
            window.open('Country.aspx', '', 'top=0,left=0,height=600,width=2040,scrollbars=no,toolbar=yes')
        }
    </script>
    <style>
        input[type="text"] {
            height: 30px;
        }

        .add-on {
            height: 30px !important;
        }

        #showcate {
            background-color: #dadada;
            height: 50%;
            position: fixed;
            width: 50%;
            z-index: 9999;
        }

        .close-cate {
            float: right;
            font-size: 20px;
            height: 42px;
            padding: 10px;
            text-align: right;
            width: 100%;
        }
    </style>
</head>
<body>

    <form id="Form1" runat="server" class="form-horizontal">
        <div class="form-group" style="text-align: center;">
            <asp:Label ID="Label1" runat="server" Style="font-size: 18px;">THÊM MỚI OR CẬP NHẬT</asp:Label>
            <asp:Label ID="lblError" runat="server" Font-Size="10" ForeColor="Red"></asp:Label>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="txtName">Tiêu đề:</label>
            <div class="col-sm-8">
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="txtDesc">Mô tả:</label>
            <div class="col-sm-8">
                <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine"
                    Height="71px" CssClass="form-control"></asp:TextBox>
            </div>

        </div>
        <div class="form-group">
            <label class="control-label col-sm-2" for="">File Video (.mp4):</label>
            <div class="col-sm-8">
                <asp:FileUpload ID="ImageFileMp4" runat="server" size="48" class="button_admin" />
            </div>

        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10" style="margin-left: 160px;">
                <asp:Button ID="cmdSave" runat="server" Text="Save" OnClick="cmdSave_Click"
                    CssClass="btn btn-default"></asp:Button>
            </div>
        </div>
    </form>

</body>
</html>
