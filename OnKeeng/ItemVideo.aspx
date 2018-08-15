<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ItemVideo.aspx.cs" Inherits="DOWNLOAD_ItemVideo" %>

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
            width: 521px;
        }

        #TABLE2 {
            height: 119px;
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
                <asp:Label ID="Label1" runat="server" Font-Size="10pt" Font-Bold="True">DANH SÁCH VIDEO</asp:Label>
                <asp:Label ID="lblError" runat="server" Font-Size="10" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-12">
                Thể loại:<asp:DropDownList ID="ddlCateogry" runat="server" CssClass="ComboBoxInputHover_ClassicBlue"
                    Width="100px" AutoPostBack="True" OnSelectedIndexChanged="ddlCateogry_SelectedIndexChanged">
                </asp:DropDownList>

                &nbsp; Đối tác:<asp:DropDownList ID="ddlcp" runat="server" AutoPostBack="True" CssClass="ComboBoxInputHover_ClassicBlue"
                    OnSelectedIndexChanged="ddlcp_SelectedIndexChanged" Width="110px">
                </asp:DropDownList>

                Type phim:<asp:DropDownList ID="ddTypePhim" runat="server" CssClass="ComboBoxInputHover_ClassicBlue"
                    Width="110px" AutoPostBack="True" OnSelectedIndexChanged="ddTypePhim_SelectedIndexChanged">
                </asp:DropDownList>

                Types Show:<asp:DropDownList ID="ddlTypesShow" runat="server" CssClass="ComboBoxInputHover_ClassicBlue"
                    Width="110px" AutoPostBack="True" OnSelectedIndexChanged="ddlTypesShow_SelectedIndexChanged">
                </asp:DropDownList>

                Tìm theo:<asp:DropDownList ID="ddlImages" runat="server" CssClass="ComboBoxInputHover_ClassicBlue"
                    Width="90px">
                </asp:DropDownList>


                Từ khóa:<asp:TextBox ID="txtKeyword" runat="server" Width="159px"></asp:TextBox>&nbsp;<asp:Button
                    ID="cmdSearch" runat="server" CssClass="formSubmitBtn" Text="Tìm kiếm" Font-Bold="True"
                    OnClick="cmdSearch_Click1" Height="20px"></asp:Button>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-12">
                <asp:DataGrid ID="dgrCommon" runat="server" CssClass="GridLabel" Width="100%" AlternatingItemStyle-BackColor="lightgray"
                    HeaderStyle-BackColor="#99ccff" PagerStyle-HorizontalAlign="Left" PagerStyle-Mode="NumericPages"
                    CellPadding="1" AllowPaging="True" BorderColor="DarkGray" BorderStyle="Inset"
                    PagerStyle-PageButtonCount="50" PageSize="10" OnSelectedIndexChanged="dgrCommon_SelectedIndexChanged"
                    AutoGenerateColumns="False" OnEditCommand="dgrCommon_addPart" OnCancelCommand="dgrCommon_createPart">
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
                                <img onmouseover="tooltip.show('<IMG src=\'<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FolderName").ToString())%>/<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ItemImage").ToString())%>\'  >')"
                                    onmouseout="tooltip.hide();" src="<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "FolderName").ToString())%>/<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ItemImage").ToString())%>"
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
                                    Chuyên Mục
                                </p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <p>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ChuyenMuc").ToString())%>
                                </p>
                            </ItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn>
                            <HeaderTemplate>
                                <p>
                                    IsView
                                </p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <p>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "IsView").ToString())%>
                                </p>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderTemplate>
                                <p>
                                    IsHot
                                </p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <p>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "IsHot").ToString())%>
                                </p>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderTemplate>
                                <p>
                                    IsHome
                                </p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <p>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "IsHome").ToString())%>
                                </p>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderTemplate>
                                <p>
                                    Thời gian nhập
                                </p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <p>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ItemDate").ToString())%>
                                </p>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderTemplate>
                                <p>
                                    UserLogin
                                </p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <p>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "UserLogin").ToString())%>
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
                                        src="../images/Edit.gif" height="20px" border="0" width="20px" visible="false" />
                                </a>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderTemplate>
                                <p>
                                    User Duyệt
                                </p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <p>
                                    <%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "UserLogin").ToString())%>
                                </p>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:ButtonColumn CommandName="Edit" HeaderText="Add Part" Text="Add Part"></asp:ButtonColumn>
                        <asp:ButtonColumn CommandName="Cancel" HeaderText="Create Part" Text="Create"></asp:ButtonColumn>
                        <asp:TemplateColumn>
                            <HeaderTemplate>
                                <p>
                                    Adaptive
                                </p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <a target="_blank" href="<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "url_adaptive").ToString())%>">
                                    Adaptive
                                </a>
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
                    AutoPostBack="True" Width="108px" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:Button ID="cmdAdd" runat="server" CssClass="formSubmitBtn" Font-Bold="True"
                    Text="Thêm mới" OnClick="cmdAdd_Click" Height="20px"></asp:Button>
                <asp:Button ID="cmdDelete" runat="server" CssClass="formSubmitBtn" Text="Xóa" Font-Bold="True"
                    OnClick="cmdDelete_Click" Height="20px" Visible="true"></asp:Button>
                <asp:Button ID="cmdSetShowHome" runat="server" CssClass="formSubmitBtn" Text="Duyệt"
                    Font-Bold="True" OnClick="cmdSetShowHome_Click" Height="20px"></asp:Button>
                <asp:Button ID="cmdRemove" runat="server" CssClass="formSubmitBtn" Text="Chờ duyệt" Font-Bold="True"
                        OnClick="cmdRemove_Click" Height="20px"></asp:Button>
                <asp:Button ID="btnSetXemNhieu" runat="server" CssClass="formSubmitBtn" Text="Set xem nhiều" Font-Bold="True"
                        OnClick="btnSetXemNhieu_Click" Height="20px"></asp:Button>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-12">
                <asp:Button ID="cmdIsHome" runat="server" CssClass="formSubmitBtn" Text="Đặt Home"
                    Font-Bold="True" OnClick="cmdIsHome_Click" Height="20px"></asp:Button>
                <asp:Button ID="cmdUnIsHome" runat="server" CssClass="formSubmitBtn" Text="Gỡ Home"
                    Font-Bold="True" OnClick="cmdUnIsHome_Click" Height="20px"></asp:Button>
                <asp:Button ID="btnSetTop" runat="server" CssClass="formSubmitBtn" Text="Set Top"
                    Font-Bold="True" OnClick="btnSetTop_Click" Height="20px"></asp:Button>
                <asp:Button ID="btnSettopTV" runat="server" CssClass="formSubmitBtn" Text="Set Top TV"
                    Font-Bold="True" OnClick="btnSetTopTV_Click" Height="20px"></asp:Button>
                Types Show:<asp:DropDownList ID="ddlTypesShow1" runat="server" CssClass="ComboBoxInputHover_ClassicBlue"
                    Width="110px" AutoPostBack="True" >
                </asp:DropDownList>
                <asp:Button ID="btnTypesShow" runat="server" CssClass="" Text="set TypesShow"
                    Font-Bold="True" OnClick="setTypesShow_Click" Height="28px" Width="150" style="border-radius:5px"></asp:Button>
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
