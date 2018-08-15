<%@ Page language="c#" Inherits="MarketVN.Sysadmin.usergroup_data" CodeFile="usergroup_data.aspx.cs" %>
<HTML>
	<HEAD>
		<title></title>
		<meta http-equiv="Content-Language" content="en-us">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<LINK href="../Styles/Styles.css" type="text/css" rel="stylesheet">
		<LINK href="../Styles/FormContainer.css" type="text/css" rel="stylesheet">
		<LINK rel="stylesheet" type="text/css" href="../Common/style.css">
	</HEAD>
	<body bgcolor="#FFFFFF" topmargin="0" leftmargin="0">
	<div class="formWrapper">
		<form runat="server" ID="Form1">
			<table border="0" cellpadding="3" cellspacing="3" style="BORDER-COLLAPSE: collapse" bordercolor="#111111"
				width="100%">
				<TBODY>
					<tr>
						<td width="100%" bgcolor="#D2D7DA"><font size="3" color="#000000"><b>
									<asp:Label id="Label1" runat="server" Font-Bold="True" Font-Size="10pt"> Nhóm người dùng</asp:Label></b></font></td>
					</tr>
					<tr>
						<td width="100%">
							<TABLE id="AutoNumber2" style="BORDER-COLLAPSE: collapse" borderColor="#111111" cellSpacing="0"
								cellPadding="3" width="100%" border="0">
								<TBODY>
									<TR>
										<TD noWrap>Tên nhóm:</TD>
										<TD width="100%">
											<asp:TextBox id="txtUserGroupName" runat="server" Width="304px"></asp:TextBox></TD>
									</TR>
									<TR>
										<TD noWrap>Mô tả:
										</TD>
										<TD width="100%">
											<asp:TextBox id="txtUserGroupDesc" runat="server" Width="304px" Height="120px"></asp:TextBox></TD>
									</TR>
									<TR>
										<TD noWrap height="1">Trạng thái:</TD>
										<TD width="100%" height="1">
											<asp:DropDownList id="ddlUserGroupStatus" runat="server" Width="152px">
												<asp:ListItem Value="1">Active</asp:ListItem>
												<asp:ListItem Value="0">Tạm ngưng</asp:ListItem>
											</asp:DropDownList>
										</TD>
									</TR>
									<TR>
										<TD noWrap colSpan="2"></TD>
									</TR>
									<TR>
										<TD noWrap colSpan="2">
											<asp:Button id="cmdSave" runat="server" CssClass="formSubmitBtn" Text="Lưu" onclick="cmdSave_Click"></asp:Button></TD>
									</TR>
		</form>

		</div>
	</body>
</HTML>
