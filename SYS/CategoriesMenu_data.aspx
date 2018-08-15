<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CategoriesMenu_data.aspx.cs" Inherits="SYS_CategoriesMenu_data" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<HTML>
	<HEAD>
		<title></title>
		<meta http-equiv="Content-Language" content="en-us">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<LINK href="Styles/Styles.css" type="text/css" rel="stylesheet">
			<LINK href="Styles/FormContainer.css" type="text/css" rel="stylesheet">
				<LINK rel="stylesheet" type="text/css" href="../Common/style.css">
					<script language="javascript" src="../common/common.js"></script>
					<script language="JavaScript">
			if (document.all)
			{     
			document.onkeydown = function ()
			{     var key_enter= 13; // 13 = Enter   
				if (key_enter==event.keyCode)
				{
					event.keyCode=0;
				document.getElementById('cmdSave').click();          
				return false;
				} 
				}
			}
				
				function setImage(string) {
				document.getElementById(ImgDataID).src = 'File:\/\/' + string;
				}
				function checkImageSize()
				{	
					if (document.getElementById(ImgDataID).width>=400 || document.getElementById(ImgDataID).height>=400)
					{
						alert("Ban dang nhap anh co so size: (" + document.getElementById(ImgDataID).width + "x" + document.getElementById(ImgDataID).height + "), Ban khong duoc nhap anh qua so size(400x400)");				
						return false;
					}
				}
				
				
					</script>
	</HEAD>
	<body bgColor="#ffffff" leftMargin="3" topMargin="3">
		<form id="Form1" runat="server">
			<table cellSpacing="1" cellPadding="1" width="100%" border="0">
				<tr>
							<td width="100%" bgColor="#F0EDE1" valign="middle" height="30">&nbsp;<img src="../images/selectimage.gif" width="20" height="20" align=absbottom />
							    <font color="#000000" size="3">
							    <b>
                                <asp:label id="Label1" runat="server" Font-Size="10pt" Font-Bold="True">THÊM MỚI MENU</asp:label>&nbsp;
								<asp:label id="lblError" runat="server" Font-Size="10" ForeColor="Red"></asp:label>
								</b>
								</font>
							</td>
						    </tr>
			</table>
			<table cellSpacing="1" cellPadding="1" width="100%" border="0">
				<tr>
					<td width="100%">
						<TABLE id="AutoNumber2" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD class="formLabel" height="2" style="width: 139px">
                                    &nbsp;Menu Root:</TD>
								<TD height="2">
									<asp:dropdownlist id="ddlCategory" runat="server" Width="403px"></asp:dropdownlist></TD>
							</TR>
                            <tr>
                                <td class="formLabel" style="width: 139px">
                                    &nbsp;Thư mục</td>
                                <td height="28">
                                    <asp:TextBox ID="txtCategoryForder" runat="server" Height="20px" Width="472"></asp:TextBox></td>
                            </tr>
							<TR>
								<TD class="formLabel" style="width: 139px">
                                    &nbsp;Tên Menu:</TD>
								<TD height="28"><FONT style="FONT-WEIGHT: normal; FONT-SIZE: 11px; CURSOR: default; COLOR: #8c8c8c; FONT-FAMILY: Arial, Verdana, Tahoma, Sans-Serif">
										<asp:textbox id="txtCategoryName" runat="server" Height="20px" Width="472"></asp:textbox></FONT></TD>
							</TR>
							<TR>
								<TD class="formLabel" style="width: 139px">
                                    &nbsp;Liên kết</TD>
								<TD height="28">
									<asp:textbox id="txtCategoryLink" runat="server" Height="20px" Width="472"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="formLabel" style="width: 139px; height: 25px;">
                                    &nbsp;Thứ tự:</TD>
								<TD style="height: 25px"><asp:textbox id="txtCategoryOrder" runat="server" Width="96px">0</asp:textbox></TD>
							</TR>
							<TR>
								<TD class="formLabel" style="width: 139px">
                                    &nbsp;Ảnh</TD>
								<TD height="2"><INPUT class="file" id="ImageFile" type="file" onchange="setImage(this.value);" size="23"
										name="ImageFile" runat="server">
									<asp:image id="ImageView" runat="server" cssclass="button"></asp:image></TD>
							</TR>
							<TR>
								<TD class="formLabel" style="width: 139px"></TD>
								<TD noWrap colSpan="1"><asp:button id="cmdSave" runat="server" CssClass="formSubmitBtn" Text="Lưu" OnClick="cmdSave_Click" Font-Bold="True"></asp:button><asp:button id="cmdDelete" runat="server" CssClass="formSubmitBtn" Text="Xóa" OnClick="cmdDelete_Click" Font-Bold="True"></asp:button></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
