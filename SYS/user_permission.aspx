<%@ Page language="c#" Inherits="MarketVN.Sysadmin.user_permission" CodeFile="user_permission.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title></title>
		<meta http-equiv="Content-Language" content="en-us">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<LINK href="../Styles/Styles.css" type="text/css" rel="stylesheet">
			<LINK href="../Styles/FormContainer.css" type="text/css" rel="stylesheet">
				<LINK rel="stylesheet" type="text/css" href="../Common/style.css">
					<script language="javascript" src="../common/common.js"></script>
	</HEAD>
	<body bgColor="#f7f7ef" leftMargin="0" topMargin="0">
	<div class="formWrapper">
		<form id="Form1" runat="server">
		
			<table style="BORDER-COLLAPSE: collapse" borderColor="#111111" cellSpacing="0" cellPadding="7"
				width="100%" border="0">
				<TBODY>
					<tr>
						<td width="100%"><font color="#4a865a" size="3"><b><asp:label id="lblGroup" runat="server" Font-Size="10pt" Font-Bold="True"> Phân quyền theo chuyên mục:</asp:label></b></font></td>
					</tr>
					<tr>
						<td width="100%">
							<P><asp:label id="Label1" runat="server" Font-Size="10pt" Font-Bold="True" BackColor="Transparent"
									ForeColor="#C04000"> Chuyên mục tin</asp:label></P>
							<P><asp:datagrid id="dgrCommon" runat="server" Font-Size="10pt" BorderWidth="1px" Width="100%" BorderStyle="Dotted"
									BorderColor="DarkGray" AllowPaging="True"  CellPadding="1" Font-Name="Verdana"
									PagerStyle-Mode="NumericPages" PagerStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#99ccff"
									AlternatingItemStyle-BackColor="lightgray" CssClass="NormalText" Font-Names="Arial" PageSize="40">
									<SelectedItemStyle BackColor="#FFC0C0"></SelectedItemStyle>
									<EditItemStyle CssClass="NormalText"></EditItemStyle>
									<AlternatingItemStyle CssClass="NormalText" BackColor="Moccasin"></AlternatingItemStyle>
									<ItemStyle CssClass="NormalText"></ItemStyle>
									<HeaderStyle Font-Names="Arial" Font-Bold="True" BorderStyle="Solid" BackColor="#DEDBCE"></HeaderStyle>
									<Columns>
										<asp:TemplateColumn>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<HeaderTemplate>
												<div align="center"><input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAll(this)"></div>
											</HeaderTemplate>
											<ItemTemplate>
												<asp:CheckBox ID="chkAllDelete" runat="server" />
											</ItemTemplate>
										</asp:TemplateColumn>
										<asp:BoundColumn Visible="False"></asp:BoundColumn>
									</Columns>
									<PagerStyle Font-Bold="True" HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
								</asp:datagrid></P>
						</td>
					</tr>
					<tr>
						<td width="100%">
							<P align="center">&nbsp;
								<asp:button id="cmdSave" runat="server" Text="Lưu" CssClass="formSubmitBtn" onclick="cmdSave_Click"></asp:button>&nbsp;</P>
						</td>
					</tr>
					<TR>
						<TD width="100%"><asp:label id="lblTotalRecords" runat="server" Font-Bold="True" ForeColor="#004000"></asp:label></TD>
					</TR>
				</TBODY>
			</table>
		</form>
		</div>
	</body>
</HTML>
