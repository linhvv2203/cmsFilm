<%@ Page language="c#" Inherits="MarketVN.Sysadmin.User_Data" CodeFile="User_Data.aspx.cs" %>
<HTML>
	<HEAD>
		<title></title>
		<meta http-equiv="Content-Language" content="en-us">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
	<meta http-equiv="Content-Language" content="en-us">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<LINK href="../Common/style.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../common/common.js"></script>
		<link href="../common/calendar-win2k-2.css" type="text/css" rel="stylesheet">
        <SCRIPT src="../common/calendar.js" type="text/javascript"></SCRIPT>
        <SCRIPT src="../common/calendar-en.js" type="text/javascript"></SCRIPT>
        <SCRIPT src="../common/calendar3.js" type="text/javascript"></SCRIPT>
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
				
	    </script>
        
	</HEAD>
	<body bgColor="#ffffff" leftMargin="0" topMargin="0">
			<form id="Form1" runat="server">
				<table style="BORDER-COLLAPSE: collapse" borderColor="#111111" cellSpacing="3" cellPadding="3"
					width="100%" border="0">
					<TBODY>
						<tr>
							<td bgColor="#F0EDE1" valign="middle" height="30" style="width: 100%">&nbsp;
							    <font color="#000000" size="3">
							    <b>
                                <asp:label id="Label1" runat="server" Font-Size="10pt" Font-Bold="True"> ĐĂNG KÝ NGƯỜI DÙNG</asp:label>&nbsp;
								<asp:label id="lblError" runat="server" Font-Size="10" ForeColor="Red"></asp:label>
								</b>
								</font>
							</td>
						</tr>
						<tr>
							<td style="width: 100%">
								<TABLE id="AutoNumber2" style="BORDER-COLLAPSE: collapse" borderColor="#111111" cellSpacing="0"
									cellPadding="3" width="100%" border="0">
									<TBODY>
										<TR>
											<TD noWrap>
                                                <strong>Họ tên:</strong></TD>
											<TD width="100%"><asp:textbox id="txtUserRealName" runat="server" Width="304px" Font-Bold="True" Font-Size="12pt"></asp:textbox></TD>
										</TR>
										
                                        <tr>
                                            <td nowrap="nowrap" style="height: 33px">
                                                <strong>
                                                Đơn vị:</strong></td>
                                            <td width="100%" style="height: 33px">
                                                <asp:TextBox ID="txtUserDept" runat="server" Width="304px" Font-Bold="True" Font-Size="12pt"></asp:TextBox></td>
                                        </tr>
										<TR>
											<TD noWrap>
                                                <strong>
                                                Số điện thoại:</strong></TD>
											<TD width="100%">
                                                <asp:TextBox ID="txtPhone" runat="server" Width="304px" Font-Bold="True" Font-Size="12pt"></asp:TextBox></TD>
										</TR>
                                        <tr>
                                            <td nowrap="nowrap" style="height: 25px">
                                                <strong>
                                                Địa chỉ Email:</strong></td>
                                            <td width="100%" style="height: 25px">
                                                <asp:TextBox ID="txtEmail" runat="server" Width="304px" Font-Bold="True" Font-Size="12pt"></asp:TextBox></td>
                                        </tr>
                                        
                                        
                                        <TR>
											<TD noWrap><b>Tài khoản:</b></TD>
											<TD width="100%"><asp:textbox id="txtUserName" runat="server" Width="304px" Font-Bold="True" Font-Size="12pt"></asp:textbox></TD>
										</TR>
										<TR>
											<TD noWrap height="18"><b>Mật khẩu:</b>
											</TD>
											<TD width="100%" height="18"><asp:textbox id="txtUserPw" runat="server" Width="304px" Font-Bold="True" Font-Size="12pt"></asp:textbox>&nbsp;
												<asp:CheckBox id="chkIsUpdatePw" runat="server" Text="Chọn checked để cập nhật lại mật khẩu" Checked="True"></asp:CheckBox></TD>
										</TR>
										<TR>
											<TD noWrap style="height: 1px"><b>Đánh lại mật khẩu:</b>
											</TD>
											<TD width="100%" style="height: 1px"><asp:textbox id="txtConfirmPw" runat="server" Width="304px" Font-Bold="True" Font-Size="12pt"></asp:textbox>&nbsp;</TD>
										</TR>
										<TR>
											<TD noWrap><b>Thuộc nhóm:</b></TD>
											<TD width="100%"><asp:dropdownlist id="ddlUserGroup" runat="server" Width="152px" Font-Bold="True" Font-Size="12pt"></asp:dropdownlist></TD>
										</TR>
                                        <tr>
                                            <td nowrap>
                                                <b>Đối tác - CP:</b>
                                            </td>
                                            <td width="100%">
                                                <asp:DropDownList ID="ddlDoiTacCP" runat="server" Width="152px" Font-Bold="True"
                                                    Font-Size="12pt">
                                                </asp:DropDownList>
                                            </td>
                            </tr>
                                        <tr>
                                            <td nowrap="nowrap" style="height: 30px">
                                                &nbsp;<strong>Miêu tả:</strong></td>
                                            <td style="height: 30px" width="100%">
												<asp:textbox id="txtUserDesc" runat="server" Width="318px" Height="72px" TextMode="MultiLine" Font-Bold="True"></asp:textbox></td>
                                        </tr>
                                        <tr>
                                            <td nowrap="nowrap" style="height: 30px">
                                            </td>
                                            <td style="height: 30px" width="100%">
												<asp:Button id="cmdSave" runat="server" Text="Lưu" CssClass="formSubmitBtn" onclick="cmdSave_Click" Font-Bold="True"></asp:Button></td>
                                        </tr>
			</form>

	</body>
</HTML>
