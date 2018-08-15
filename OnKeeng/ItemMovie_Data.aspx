<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ItemMovie_Data.aspx.cs" Inherits="MOVIE_ItemMovie_Data" %>

<html>
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
    <div class="container">
        <form id="Form1" runat="server" class="form-horizontal">
            <div class="form-group" style="text-align: center;">
                <asp:Label ID="Label1" runat="server" Style="font-size: 18px;">THÊM MỚI OR CẬP NHẬT</asp:Label>
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
                    <a id="idcate">Chọn chuyên mục</a>
                    <div id="showcate" style="display: none;">
                        <a class="close-cate">X</a>
                        <asp:CheckBoxList ID="chkbCategory" runat="server" RepeatColumns="5">
                        </asp:CheckBoxList>
                    </div>
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-sm-2" for="ddLoaiPhim">Loại Phim:</label>
                <div class="col-sm-4">
                    <asp:DropDownList ID="ddLoaiPhim" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-sm-2" for="ddlTypePhim">Kiểu Phim:</label>
                <div class="col-sm-4">
                    <asp:DropDownList ID="ddlTypePhim" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlTypePhim_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-sm-2" for="ddlProductInfo">Product info:</label>
                <div class="col-sm-4">
                    <asp:DropDownList ID="ddlProductInfo" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProductInfo_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-sm-2" for="ddlIntroFilm">Intro Film:</label>
                <div class="col-sm-4">
                    <asp:DropDownList ID="ddlIntroFilm" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlIntroFilm_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <!--INFO TVOD-->
            <div class="" id="infoTvod" runat="server">
                <div class="form-group">
                    <label class="control-label col-sm-2" for="txtPriceSD">Giá SD:</label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtPriceSD" type="number" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtPriceSD" runat="server" ErrorMessage="Bạn cần nhập kiểu số." ValidationExpression="\d+"></asp:RegularExpressionValidator>
                    </div>
                    <label class="control-label col-sm-2" for="txtPriceSDDATA">Giá SD + FreeDATA:</label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtPriceSDDATA" type="number" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtPriceSDDATA" runat="server" ErrorMessage="Bạn cần nhập kiểu số." ValidationExpression="\d+"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-2" for="txtPriceHD">Giá HD:</label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtPriceHD" type="number" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtPriceHD" runat="server" ErrorMessage="Bạn cần nhập kiểu số." ValidationExpression="\d+"></asp:RegularExpressionValidator>
                    </div>
                    <label class="control-label col-sm-2" for="txtPriceHDDATA">Giá HD + FreeDATA:</label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtPriceHDDATA" type="number" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtPriceHDDATA" runat="server" ErrorMessage="Bạn cần nhập kiểu số." ValidationExpression="\d+"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-2" for="txtPriceSDMin">Giá tối thiểu SD:</label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtPriceSDMin" type="number" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="txtPriceSDMin" runat="server" ErrorMessage="Bạn cần nhập kiểu số." ValidationExpression="\d+"></asp:RegularExpressionValidator>--%>
                    </div>
                    <label class="control-label col-sm-2" for="txtPriceHDMin">Giá tối thiểu HD:</label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtPriceHDMin" type="number" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator6" ControlToValidate="txtPriceHDMin" runat="server" ErrorMessage="Bạn cần nhập kiểu số." ValidationExpression="\d+"></asp:RegularExpressionValidator>--%>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-2" for="txtPhanLoaiVong">Phân loại vòng:</label>
                    <div class="col-sm-2">
                        <%--<asp:TextBox ID="txtPhanLoaiVong" runat="server" CssClass="form-control"></asp:TextBox>--%>
                        <asp:DropDownList ID="ddlPhanLoaiVong" runat ="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <label class="control-label col-sm-2" for="txtRatioShare">Tỉ lệ chia sẻ:</label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtRatioShare" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-2" for="txtFoxID">Fox ID:</label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtFoxID" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <label class="control-label col-sm-2" for="txtAvailYear">Avail Year:</label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtAvailYear" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <!---->
            <div id="infoBigsix" runat="server">
                <div class="form-group">
                    <label class="control-label col-sm-2" for="txtFoxID">Fox ID:</label>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtFoxIdBigsix" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-sm-2" for="txtName">Tiêu đề:</label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-sm-2" for="txtNameEn">Tên tiếng anh:</label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtNameEn" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-sm-2" for="ddlLogo">Logo:</label>
                <div class="col-sm-4">
                    <asp:DropDownList ID="ddlLogo" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-sm-2" for="txtPosition">Vị trí logo:</label>
                <div class="col-sm-8">
                    <asp:DropDownList ID="ddlPosition" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-sm-2" for="txtIMDB">IMDB:</label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtIMDB" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-sm-2" for="txtDirector">Đạo diễn:</label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtDirector" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-sm-2">
                    <input type="button" runat="server" id="cmddirec" onclick="OnDirector()" value="Add director" class="btn btn-default" />
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-sm-2" for="txtItemProductYear">Năm:</label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtItemProductYear" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-sm-2" for="txtItemCountry">Quốc gia:</label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtItemCountry" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-sm-2">
                    <input type="button" runat="server" id="cmdCountry" onclick="OnCountry()" value="Add country" class="btn btn-default" />
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-sm-2" for="txtActor">Diễn viên:</label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtActor" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-sm-2">
                    <input type="button" runat="server" id="cmdactor" onclick="OnActor()" value="Add actor" class="btn btn-default" />
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-sm-2" for="txtPublisher">Nhà phát hành:</label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtPublisher" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-sm-2" for="txtExpired" runat="server">Expired Licence:</label>
                <div id="datetimepickerExpired" runat="server" class="input-append date col-sm-4">
                    <input id="txtExpired" runat="server" type="text" class="form-control" />
                    <span class="add-on">
                        <i data-time-icon="icon-time" data-date-icon="icon-calendar"></i>
                    </span>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-sm-2" for="txtStartLicense" runat="server">Start License:</label>
                <div id="datetimepickerStart" runat="server" class="input-append date col-sm-4">
                    <input id="txtStartLicense" runat="server" type="text" class="form-control" />
                    <span class="add-on">
                        <i data-time-icon="icon-time" data-date-icon="icon-calendar"></i>
                    </span>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-sm-2" for="txtUserName">Người Upload:</label>
                <div class="col-sm-8">
                    <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-sm-2" for="ImageFileImage">File Ảnh Ngang (.jpg)</label>
                <div class="col-sm-4">
                    <asp:FileUpload ID="ImageFileImage" runat="server" CssClass="button_admin" onchange="readURL(this);" />
                </div>
                <div class="col-sm-2">
                    <asp:Image ID="imageNgang" runat="server" Height="70px" Width="70px" />
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-sm-2" for="ImageFileImageBanner">File Ảnh Dọc (.jpg)</label>
                <div class="col-sm-4">
                    <asp:FileUpload ID="ImageFileImageBanner" runat="server" CssClass="button_admin" onchange="readURL(this);" />
                </div>
                <div class="col-sm-2">
                    <asp:Image ID="imageDoc" runat="server" Height="70px" Width="70px" />
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-sm-2" for="ImageFileImageFlash">Ảnh Flash Hot (.jpg)</label>
                <div class="col-sm-4">
                    <asp:FileUpload ID="ImageFileImageFlash" runat="server" CssClass="button_admin" onchange="readURL(this);" />
                </div>
                <div class="col-sm-2">
                    <asp:Image ID="imageFlash" runat="server" Height="70px" Width="70px" />
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-sm-2" for="FlashHotWeb">Ảnh Flash Hot Web (.jpg)</label>
                <div class="col-sm-4">
                    <asp:FileUpload ID="FiUploadFlashHotWeb" runat="server" CssClass="button_admin" onchange="readURL(this);" />
                </div>
                <div class="col-sm-2">
                    <asp:Image ID="imageFlashWeb" runat="server" Height="70px" Width="70px" />
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-sm-2" for="FUFileTrailer">File trailer (.mp4)</label>
                <div class="col-sm-4">
                    <asp:FileUpload ID="FUFileTrailer" runat="server" CssClass="button_admin" />
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
                <label class="control-label col-sm-2" for="txtIsView">isView:</label>
                <div class="col-sm-4">
                    <asp:TextBox ID="txtIsView" runat="server"
                        CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-sm-2" for="txtContract">No-Contract:</label>
                <div class="col-sm-4">
                    <asp:TextBox ID="txtContract" runat="server" MaxLength="200"
                        CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-sm-offset-2">
                        <label class="checkbox-inline">
                            <asp:CheckBox runat="server" ID="chkInternational" />quốc tế
                        </label>
                        <label class="checkbox-inline">
                            <asp:CheckBox runat="server" ID="chkIsmonopoly" />bản quyền
                        </label>
                        <label class="checkbox-inline">
                            <asp:CheckBox runat="server" ID="chkIsControls" />đối soát
                        </label>
                        <label class="checkbox-inline">
                            <asp:CheckBox runat="server" ID="chkFree_content" />Free content
                        </label>
                        <label class="checkbox-inline">
                            <asp:CheckBox runat="server" ID="chkFree_data" />Free data
                        </label>
                        <label class="checkbox-inline">
                            <asp:CheckBox runat="server" ID="chkFree_bigsix" />Free bigsix
                        </label>
                        <label class="checkbox-inline">
                            <asp:CheckBox runat="server" ID="chkFree_content_all" />Free content all
                        </label>
                        <label class="checkbox-inline">
                            <asp:CheckBox runat="server" ID="chkFree_data_all" />Free data all
                        </label>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <%--<div runat="server" id="" class="" for="datetimepicker">DatePublish</div>--%>
                <label class="control-label col-sm-2" for="datetimepicker" runat="server" id="dateName">DatePublish</label>
                <div id="datetimepicker" runat="server" class="input-append date col-sm-4">
                    <input id="inputDatetime" runat="server" type="text" class="form-control" />
                    <span class="add-on">
                        <i data-time-icon="icon-time" data-date-icon="icon-calendar"></i>
                    </span>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-offset-2 col-sm-10" style="margin-left: 160px;">
                    <asp:Button ID="cmdSave" runat="server" Text="Save Film" OnClick="cmdSave_Click"
                        CssClass="btn btn-default"></asp:Button>
                </div>
            </div>

            <div class="form-group" id="idListPhim">
                <div class="col-sm-12">
                    <h4>DS Phim đang nhập</h4>
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
                                    Ảnh
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <img onmouseover="tooltip.show('<IMG src=\'<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ItemImage").ToString())%>\'  >')"
                                        onmouseout="tooltip.hide();" src="<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ItemImage").ToString())%>"
                                        height="20px" width="20px" />
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
                                        Tên
                                    </p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p>
                                        <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ItemName").ToString())%>
                                    </p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <p>
                                        Status
                                    </p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p>
                                        <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ItemStatus").ToString())%>
                                    </p>
                                </ItemTemplate>
                            </asp:TemplateColumn>


                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <p>Ngày nhập</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p>
                                        <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ItemDate").ToString())%>
                                    </p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <p>Người nhập</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p>
                                        <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Userupload").ToString())%>
                                    </p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    Sửa
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <a href="ItemMovie_Data.aspx?id=<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "id").ToString())%>&type=edit&status=<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ItemStatus").ToString())%>">
                                        <img alt="" title=" <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ItemName").ToString())%>"
                                            src="../images/Edit.gif" height="20px" border="0" width="20px" />
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn Visible="False"></asp:BoundColumn>
                        </Columns>
                        <PagerStyle Font-Bold="True" HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>

                    <div>
                        <asp:Button ID="cmdSetShowHome" runat="server" Text="Duyệt"
                            Font-Bold="True" OnClick="cmdSetShowHome_Click" CssClass="btn btn-default"></asp:Button>
                        &nbsp;<asp:Button ID="cmdDelete" runat="server" Text="Xóa" Font-Bold="True"
                            OnClick="cmdDelete_Click" CssClass="btn btn-default"></asp:Button>
                        &nbsp;<asp:Button ID="cmdAddLogo" runat="server" Text="AddLogo" Font-Bold="True"
                            OnClick="cmdAddLogo_Click" CssClass="btn btn-default"></asp:Button>
                    </div>
                </div>
            </div>

            <asp:HiddenField ID="HiddenField1" runat="server" />
        </form>
    </div>
    <script type="text/javascript">
        $('#<%=datetimepicker.ClientID %>').datetimepicker({
            format: 'yyyy-MM-dd hh:mm:ss',
            //language: 'pt-BR'
        });

        $('#<%=datetimepickerExpired.ClientID %>').datetimepicker({
            format: 'dd-MM-yyyy hh:mm:ss',
            //language: 'pt-BR'
        });

        $('#<%=datetimepickerStart.ClientID %>').datetimepicker({
            format: 'dd-MM-yyyy hh:mm:ss',
            //language: 'pt-BR'
        });

        $('#idcate').click(function () {
            $('#showcate').show();

        })

        $('.close-cate').click(function () {
            $('#showcate').hide();
        });
    </script>
</body>
</html>
