<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Director.aspx.cs" Inherits="OnPhim_Director" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title></title>
    <meta http-equiv="Content-Language" content="en-us">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <link href="../Common/style.css" type="text/css" rel="stylesheet">
    <link href="../tip/style.css" type="text/css" rel="stylesheet">
    <link href="../tip/style_tooltips.css" type="text/css" rel="stylesheet">
    <script src="../Styles/jquery.min.js" type="text/javascript"></script>
    <script language="javascript" src="../common/common.js"></script>
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
        function OnSetRelate() {


            //             var sHtml = $('#list').html();
            //             //alert(sHtml);
            //             window.opener.document.getElementById('ListRelate').innerHTML =  sHtml;

            var sHtml = $('#ListMid').html();
            sHtml = sHtml.trim();

            var s;
            s = window.opener.document.getElementById('txtDirector').value;
            s = s.trim();
            if (s.length > 0)
                s = s + ',' + sHtml;
            else
                s = s + sHtml;
            window.opener.document.getElementById('txtDirector').value = s;
            window.close();
        }
        function Ok() {

            var sHtml = $('#list').html(); //oEditor.FCKTools.HTMLEncode($('#list').html());	           
            window.opener.document.getElementById('txtRelate').value = sHtml;
            return true;
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
    <form id="Form1" runat="server">

        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td width="100%" bgcolor="#F0EDE1" valign="middle" height="30">&nbsp;
							    <font color="#000000" size="3">
                                    <b>
                                        <asp:Label ID="Label1" runat="server" Font-Size="10pt" Font-Bold="True">DANH SÁCH INLINEBOX</asp:Label>
                                        &nbsp;
								<asp:Label ID="lblError" runat="server" Font-Size="10" ForeColor="Red"></asp:Label>
                                    </b>
                                </font>
                    </td>
                </tr>
                <tr>
                    <td width="100%" bgcolor="#000000" valign="middle" height="1"></td>
                </tr>
                <tr>
                    <td class="RadWWrapperBodyCenter" width="100%">
                        <table id="ConfigTable" width="100%">
                            <tr>
                                <td class="style4" height="6">Keyword:
											<asp:TextBox ID="txtKeyword" runat="server" Width="159px"></asp:TextBox>&nbsp;<asp:Button
                                                ID="cmdSearch" runat="server" CssClass="formSubmitBtn" Text="Search"
                                                Font-Bold="True" OnClick="cmdSearch_Click1" Height="20px"></asp:Button>
                                </td>
                                <td>
                                    <center>
                                        <asp:Label ID="lbError" runat="server" Text=""></asp:Label></center>
                                </td>
                            </tr>




                        </table>
                        <table id="TABLE1" width="100%">
                            <tr>
                                <td>
                                    <asp:DataGrid ID="dgrCommon" runat="server" CssClass="GridLabel" Width="100%" AlternatingItemStyle-BackColor="lightgray"
                                        HeaderStyle-BackColor="#99ccff" PagerStyle-HorizontalAlign="Left" PagerStyle-Mode="NumericPages"
                                        CellPadding="1" AllowPaging="True" BorderColor="DarkGray" BorderStyle="Inset" PagerStyle-PageButtonCount="50"
                                        OnSelectedIndexChanged="dgrCommon_SelectedIndexChanged" DataMember="id"
                                        OnDeleteCommand="dgrCommon_DeleteCommand"
                                        OnPageIndexChanged="dgrCommon_PageIndexChanged"
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
                                                        <input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAll(this)"></div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkAllDelete" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                            <asp:TemplateColumn>
                                                <HeaderTemplate>
                                                    <p>Title</p>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <p><%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Name").ToString())%></p>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>




                                            <asp:BoundColumn Visible="False"></asp:BoundColumn>

                                        </Columns>
                                        <PagerStyle Font-Bold="True" HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
                                    </asp:DataGrid></td>
                            </tr>
                        </table>
                        <asp:Label ID="lblTotalRecords" runat="server" Font-Bold="True" ForeColor="#004000"></asp:Label>
                        <table id="TABLE2" width="100%">
                            <tr>
                                <td style="height: 26px">

                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ComboBoxInputHover_ClassicBlue" AutoPostBack="True"
                                        Width="108px" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    &nbsp;<asp:Button ID="cmdAdd" runat="server" CssClass="formSubmitBtn"
                                        Font-Bold="True" Text="Add Actor" OnClick="cmdAdd_Click" Height="20px"></asp:Button>
                                    <%--<asp:Button ID="cmdDelete" runat="server" CssClass="formSubmitBtn"
                                        Text="Delete All" Font-Bold="True" OnClick="cmdDelete_Click"
                                        Height="20px" Visible="true"></asp:Button>--%>
                                    <input type="button" runat="server" id="cmdok" onclick="OnSetRelate()" value="Get Director"
                                        name="Lấy tin bài" class="button" style="color: Green" />
                                    <asp:Button ID="cmdRemove" runat="server"
                                        CssClass="formSubmitBtn" Text="Close" Font-Bold="True" OnClick="cmdRemove_Click"
                                        Height="20px"></asp:Button>

                                    &nbsp;&nbsp;
                                           
                                </td>
                            </tr>
                        </table>

                        <asp:HiddenField runat="server" ID="hiddenToken" />
                        <div id="ListMid">
                            <%= Server.HtmlEncode(ListMID.ToString())%>
                        </div>
                    </td>
                </tr>

            </tbody>
        </table>

    </form>

</body>
</html>

