<%@ Page Language="C#" AutoEventWireup="true" CodeFile="usergroup_Menu.aspx.cs" Inherits="SYS_usergroup_Menu" %>
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
	<body bgcolor="#ffffff" topmargin="0" leftmargin="0">
		<form runat="server" ID="Form1">
			<table border="0" cellpadding="3" cellspacing="3" style="BORDER-COLLAPSE: collapse" bordercolor="#111111"
				width="100%">
				<TBODY>
					<tr>
						<td width="100%" bgcolor="#d2d7da"><font size="3" color="#000000"><b>
									<asp:Label id="lblGroup" runat="server" Font-Bold="True" Font-Size="10pt"> Phân quyền Nhóm:</asp:Label></b></font></td>
					</tr>
                    <tr>
                        <td width="100%">
								<asp:Label id="lblNguoiDung" runat="server" Font-Size="10pt" Font-Bold="True" ForeColor="#C04000"
									BackColor="Transparent">Người dùng thuộc nhóm</asp:Label></td>
                    </tr>
					<tr>
						<td width="100%" style="height: 24px">
							
                   
							<P>
								<asp:datagrid id="dgrNguoiDung" runat="server" Font-Size="10pt" Font-Names="Arial" CssClass="NormalText"
									AlternatingItemStyle-BackColor="lightgray" HeaderStyle-BackColor="#99ccff" PagerStyle-HorizontalAlign="Left"
									PagerStyle-Mode="NumericPages" Font-Name="Verdana" CellPadding="3"  AllowPaging="True"
									BorderColor="DarkGray" BorderStyle="Dotted" Width="100%" BorderWidth="1px">
									<SelectedItemStyle BackColor="#FFC0C0"></SelectedItemStyle>
									<EditItemStyle CssClass="NormalText"></EditItemStyle>
									<AlternatingItemStyle CssClass="NormalText" BackColor="SkyBlue"></AlternatingItemStyle>
									<ItemStyle CssClass="NormalText" VerticalAlign="Top"></ItemStyle>
									<HeaderStyle Font-Names="Arial" Font-Bold="True" BorderStyle="None" BackColor="ActiveBorder"></HeaderStyle>
									<Columns>
										<asp:BoundColumn Visible="False"></asp:BoundColumn>
									</Columns>
									<PagerStyle Font-Bold="True" HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
								</asp:datagrid>&nbsp;</P>
							
						</td>
					</tr>
                    <tr>
                        <td width="100%">
								<asp:Label id="Label1" runat="server" Font-Size="10pt" Font-Bold="True" ForeColor="#C04000"
									BackColor="Transparent">Chức năng hệ thống</asp:Label></td>
                    </tr>
                    <tr>
                        <td width="100%">
                            &nbsp;Menu root:
                            <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" 
                                Width="264px" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                            </asp:DropDownList>
                            Tìm theo: 
                            <asp:DropDownList ID="ddlSearch" runat="server" Width="120px">
                            </asp:DropDownList>
                            Từ khóa:
                            <asp:TextBox ID="txtKeyword" runat="server" Width="168px"></asp:TextBox><asp:Button
                                ID="cmdSearch" runat="server" CssClass="formSubmitBtn" Font-Bold="True" OnClick="cmdSearch_Click"
                                Text="Tìm kiếm" />
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="100%">
								<asp:datagrid id="dgrCommon" runat="server" Font-Size="10pt" PageSize="20" Font-Names="Arial"
									CssClass="NormalText" AlternatingItemStyle-BackColor="lightgray" HeaderStyle-BackColor="#99ccff"
									PagerStyle-HorizontalAlign="Left" PagerStyle-Mode="NumericPages" Font-Name="Verdana" CellPadding="1"
									 AllowPaging="True" BorderColor="DarkGray" BorderStyle="Dotted" Width="100%"
									BorderWidth="1px">
									<SelectedItemStyle BackColor="#FFC0C0"></SelectedItemStyle>
									<EditItemStyle CssClass="NormalText"></EditItemStyle>
									<AlternatingItemStyle CssClass="NormalText" BackColor="Moccasin"></AlternatingItemStyle>
									<ItemStyle CssClass="NormalText"></ItemStyle>
									<HeaderStyle Font-Names="Arial" Font-Bold="True" BorderStyle="None" BackColor="#DEDBCE"></HeaderStyle>
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
								<asp:Button id="cmdSave" runat="server" Font-Bold="True" Text="Lưu" Font-Size="9pt" Font-Names="Verdana"
									Width="91px" onclick="cmdSave_Click"></asp:Button>&nbsp;
							<asp:Label id="lblTotalRecords" runat="server" Font-Bold="True" ForeColor="#004000"></asp:Label></P>
						</td>
					</tr>
				</TBODY>
			</table>
		</form>
	</body>
</HTML>

