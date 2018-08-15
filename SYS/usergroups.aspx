<%@ Page language="c#" Inherits="MarketVN.Sysadmin.usergroups" CodeFile="usergroups.aspx.cs" %>
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
	<body bgColor="#ffffff" leftMargin="0" topMargin="0">
		<div class="formWrapper">
			<form runat="server" ID="Form1">
				<table style="BORDER-COLLAPSE: collapse" borderColor="#111111" cellSpacing="3" cellPadding="3"
					width="100%" border="0">
					<TBODY>
						<tr>
							<td width="100%" bgcolor="#d2d7da"><font color="#000000" size="3"><b><asp:label id="Label1" runat="server" Font-Size="10pt" Font-Bold="True"> Quản lý Nhóm người dùng</asp:label></b></font></td>
						</tr>
						<tr>
							<td width="100%"><asp:datagrid id="dgrCommon" runat="server" Font-Size="10pt" PageSize="15" Font-Names="Arial"
									CssClass="NormalText" AlternatingItemStyle-BackColor="lightgray" HeaderStyle-BackColor="#99ccff" PagerStyle-HorizontalAlign="Left"
									PagerStyle-Mode="NumericPages" Font-Name="Verdana" CellPadding="1"  AllowPaging="True" BorderColor="DarkGray"
									BorderStyle="Inset" Width="100%">
									<SelectedItemStyle BackColor="#FFC0C0"></SelectedItemStyle>
									<EditItemStyle CssClass="NormalText"></EditItemStyle>
									<AlternatingItemStyle CssClass="NormalText" BackColor="Gainsboro"></AlternatingItemStyle>
									<ItemStyle CssClass="NormalText"></ItemStyle>
									<HeaderStyle Font-Names="Arial" Font-Bold="True" BorderStyle="none" BackColor="#d2d7da"></HeaderStyle>
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
								</asp:datagrid></td>
						</tr>
						<tr>
							<td width="100%">
								<P align="center">&nbsp;
									<asp:button id="cmdAdd" runat="server" Text="Thêm" CssClass="formSubmitBtn" onclick="cmdAdd_Click" Font-Bold="True"></asp:button>&nbsp;
									<asp:button id="cmdDetele" runat="server" Text="Xoá" CssClass="formSubmitBtn" onclick="cmdDetele_Click" Font-Bold="True"></asp:button>&nbsp;&nbsp;
									<asp:label id="lblTotalRecords" runat="server" Font-Bold="True" ForeColor="#004000"></asp:label>
								</P>
							</td>
						</tr>
						<TR>
							<TD width="100%"></TD>
						</TR>
					</TBODY>
				</table>
			</form>
		</div>
	</body>
</HTML>
