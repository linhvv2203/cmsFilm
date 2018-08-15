<%@ Page language="c#" Inherits="MarketVN.Sysadmin.UserRight" CodeFile="UserRight.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>UserRight</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
				<LINK href="../Styles/Styles.css" type="text/css" rel="stylesheet">
			<LINK href="../Styles/FormContainer.css" type="text/css" rel="stylesheet">
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
                 document.getElementById('cmdSearch').click();          
                 return false;
                 } 
                }
            }
		</script>
		
	</HEAD>
	<body >
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 16px; POSITION: absolute; TOP: 0px; BORDER-COLLAPSE: collapse"
				borderColor="#111111" cellSpacing="0" cellPadding="7" width="100%" border="0">
				<TR>
					<TD width="100%"><FONT color="#4a865a" size="3"><B><asp:label id="lblGroup" runat="server" Font-Bold="True" Font-Size="10pt"> Phân quyền User:</asp:label>&nbsp;
								<asp:label id="lblError" runat="server" ForeColor="#ff0000"></asp:label></B></FONT></TD>
				</TR>
				<TR>
					<TD width="100%">
						<P><asp:datagrid id="dgrNguoiDung" runat="server" Font-Size="10pt" Width="100%" BorderStyle="Dotted"
								BorderColor="DarkGray" AllowPaging="True"  CellPadding="3" Font-Name="Verdana"
								PagerStyle-Mode="NumericPages" PagerStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#99ccff"
								AlternatingItemStyle-BackColor="lightgray" CssClass="NormalText" PageSize="2" Font-Names="Arial"
								BorderWidth="1px">
								<SelectedItemStyle BackColor="#FFC0C0"></SelectedItemStyle>
								<EditItemStyle CssClass="NormalText"></EditItemStyle>
								<AlternatingItemStyle CssClass="NormalText" BackColor="SkyBlue"></AlternatingItemStyle>
								<ItemStyle CssClass="NormalText" VerticalAlign="Top"></ItemStyle>
								<HeaderStyle Font-Names="Arial" Font-Bold="True" BorderStyle="None" BackColor="ActiveBorder"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False"></asp:BoundColumn>
								</Columns>
								<PagerStyle Font-Bold="True" HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
						</P>
					</TD>
				</TR>
				<TR>
					<TD width="100%">
						<P><asp:label id="Label3" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="#C04000"
								BackColor="Transparent">Các chức năng hệ thống :</asp:label><asp:label id="lblTotalRecords" runat="server" Font-Bold="True" ForeColor="#004000"></asp:label></P>
					</TD>
				</TR>
                <tr>
                    <td width="100%">
                        <strong>Nhóm Menu:
                            <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
                                Width="173px">
                            </asp:DropDownList>
                            Tìm theo: </strong>
                        <asp:DropDownList ID="ddlSearch" runat="server" Width="225px">
                        </asp:DropDownList>
                        Từ khóa:
                        <asp:TextBox ID="txtKeyword" runat="server" Width="168px"></asp:TextBox>&nbsp;<asp:Button
                            ID="cmdSearch" runat="server" CssClass="formSubmitBtn" Font-Bold="True" 
                            Text="Tìm Kiếm" OnClick="cmdSearch_Click" /></td>
                </tr>
				<TR>
					<TD width="100%"><asp:datagrid id="dgrCommon" runat="server" Width="100%" BorderStyle="Dotted" BorderColor="DarkGray"
							AllowPaging="True"  CellPadding="1" PagerStyle-Mode="NumericPages" PagerStyle-HorizontalAlign="Left"
							PageSize="20" BorderWidth="1px">
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
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD width="100%">
						<asp:checkbox id="chkIsAdd" runat="server" Width="88px" Text="Thêm"></asp:checkbox>&nbsp;
						<asp:checkbox id="chkIsDeleted" runat="server" Width="88px" Text="Xoá"></asp:checkbox>
						<asp:checkbox id="chkisEdited" runat="server" Width="80px" Text="Sửa"></asp:checkbox>
						<asp:checkbox id="chkisUpdated" runat="server" Width="120px" Text="Cập nhật"></asp:checkbox>
						<asp:checkbox id="chkisViewed" runat="server" Width="80px" Text="Xem"></asp:checkbox>&nbsp;<asp:CheckBox
                            ID="chkIsXemAll" runat="server" 
                            Text="Xem All" Width="80px" />
						<asp:Button id="cmdAllow" runat="server" Text="Các chức chuy cập" OnClick="cmdAllow_Click" ></asp:Button></TD>
				</TR>
				<TR>
					<TD width="100%">
						<asp:label id="Label1" runat="server" Font-Size="10pt" Font-Bold="True" ForeColor="#C04000"
							BackColor="Transparent">Các chức năng hệ thống được chọn :</asp:label></TD>
				</TR>
				<TR>
					<TD width="100%">
						<asp:datagrid id="dgrCommon1" runat="server" BorderWidth="1px" PageSize="20" PagerStyle-HorizontalAlign="Left"
							PagerStyle-Mode="NumericPages" CellPadding="1"  AllowPaging="True" BorderColor="DarkGray"
							BorderStyle="Dotted" Width="100%">
							<Columns>
								<asp:TemplateColumn>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<HeaderTemplate>
										<div align="center"><input type="CheckBox" name="SelectAllCheckBox" onclick="SelectAll1(this)"></div>
									</HeaderTemplate>
									<ItemTemplate>
										<asp:CheckBox ID="chkAllDelete1" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn Visible="False"></asp:BoundColumn>
							</Columns>
							<PagerStyle Font-Bold="True" HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD width="100%">
						<asp:Button id="cmdThem" runat="server" Text="Thêm quyền" OnClick="cmdThem_Click" ></asp:Button>&nbsp;
						<asp:Button id="cmdGo" runat="server" Text="Bỏ quyền" OnClick="cmdGo_Click"></asp:Button>&nbsp;
						<asp:Button id="cmdXoa" runat="server" Text="Xóa quyền" OnClick="cmdXoa_Click"></asp:Button></TD>
				</TR>
			</TABLE>
			<P>&nbsp;</P>
			<P>&nbsp;</P>
		</form>
	</body>
</HTML>
