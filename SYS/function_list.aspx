<%@ Page language="c#" Inherits="MarketVN.Sysadmin.function_list" CodeFile="function_list.aspx.cs" %>
<HTML>
	<HEAD>
		<title></title>
		<meta http-equiv="Content-Language" content="en-us">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
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
	<body bgColor="#ffffff" leftMargin="1" topMargin="1">

			<form runat="server" ID="Form1">
				<table style="BORDER-COLLAPSE: collapse" borderColor="#111111" cellSpacing="2" cellPadding="2"
					width="100%" border="0">
					<TBODY>
						<tr>
							<td width="100%" bgcolor="#d2d7da"><font color="#000000" size="2"><b><asp:label id="Label1" runat="server" Font-Size="10pt" Font-Bold="True">Quản lý chức năng hệ thống</asp:label>:
                                <asp:Label ID="lblError" runat="server" Font-Size="10" ForeColor="Red"></asp:Label></b></font></td>
						</tr>
                        <tr>
                            <td bgcolor="#FFFFFF" width="100%">
                                <strong>Nhóm Menu:
                                    <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" 
                                        Width="173px" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    Tìm theo:
                                    <asp:DropDownList ID="ddlSearch" runat="server" Width="225px">
                                    </asp:DropDownList>
                                    Từ khóa:
                                    <asp:TextBox ID="txtKeyword" runat="server" Width="168px"></asp:TextBox>&nbsp;<asp:Button
                                        ID="cmdSearch" runat="server" CssClass="formSubmitBtn" Font-Bold="True" 
                                        Text="Tìm Kiếm" OnClick="cmdSearch_Click" /></strong></td>
                        </tr>
						<tr>
							<td width="100%"><asp:datagrid id="dgrCommon" runat="server" Font-Size="10pt" PageSize="20" Font-Names="Arial"
									CssClass="NormalText" AlternatingItemStyle-BackColor="lightgray" HeaderStyle-BackColor="#99ccff" PagerStyle-HorizontalAlign="Left"
									PagerStyle-Mode="NumericPages" Font-Name="Verdana" CellPadding="1"  AllowPaging="True" BorderColor="DarkGray"
									BorderStyle="Inset" Width="100%">
									<SelectedItemStyle ></SelectedItemStyle>
									<EditItemStyle CssClass="NormalText"></EditItemStyle>
									<AlternatingItemStyle CssClass="NormalText" ></AlternatingItemStyle>
									<ItemStyle CssClass="NormalText"></ItemStyle>
									<HeaderStyle Font-Names="Arial" Font-Bold="True" BorderStyle="none" ></HeaderStyle>
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
							<td width="100%" style="height: 28px">
								<P align="Left">
									<asp:button id="cmdAdd" runat="server" Text="Thêm" CssClass="formSubmitBtn" onclick="cmdAdd_Click" Font-Bold="True"></asp:button>&nbsp;<asp:button id="cmdDetele" runat="server" Text="Xoá" CssClass="formSubmitBtn" onclick="cmdDetele_Click" Font-Bold="True"></asp:button>&nbsp;<asp:button id="cmdReGen" runat="server" Text="Tạo lại quyền" CssClass="formSubmitBtn" onclick="cmdReGen_Click" Font-Bold="True"></asp:button>&nbsp;&nbsp;<asp:label id="lblTotalRecords" runat="server" Font-Bold="True" ForeColor="#004000"></asp:label>&nbsp;<asp:DropDownList ID="ddlCategoryC" runat="server" 
                                        Width="173px" >
                                    </asp:DropDownList>&nbsp;<asp:Button ID="cmdConvert4" runat="server" CssClass="formSubmitBtn"
                                        Font-Bold="True" OnClick="cmdConvert4_Click" Text="ADD" />
                                    <asp:Button ID="cmdRight" runat="server" CssClass="formSubmitBtn"
                                        Font-Bold="True" Text="ADD RIGHT" OnClick="cmdRight_Click" /></P>
							</td>
						</tr>
					</TBODY>
				</table>
			</form>

	</body>
</HTML>
