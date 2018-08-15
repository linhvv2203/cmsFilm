<%@ Page language="c#" Inherits="MarketVN.Sysadmin.User_List" CodeFile="User_List.aspx.cs" %>
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
	<body bgcolor="#ffffff" topmargin="0" leftmargin="0">
			<form runat="server" ID="Form1">
				<table border="0" cellpadding="1" cellspacing="1" style="BORDER-COLLAPSE: collapse" bordercolor="#111111"
					width="100%">
					<TBODY>
						
						
						 <tr>
							<td width="100%" bgColor="#F0EDE1" valign="middle" height="30">&nbsp;<img src="../images/selectimage.gif" width="20" height="20" align=absbottom />
							    <font color="#000000" size="3">
							    <b>
                                <asp:label id="Label1" runat="server" Font-Size="10pt" Font-Bold="True">DANH SÁCH NGƯỜI DÙNG</asp:label>&nbsp;
								<asp:label id="lblError" runat="server" Font-Size="10" ForeColor="Red"></asp:label>
								</b>
								</font>
							</td>
				        </tr>
					    <tr>
						    <td width="100%" bgColor="#000000" valign="middle" height="1">
						    </td>
					    </tr>
                        <tr>
                            <td height="1" valign="middle" width="100%">
                                <strong>&nbsp;Nhóm ND:</strong>
                                <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" Width="121px" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                </asp:DropDownList><strong>Tìm theo: </strong>
                                <asp:DropDownList ID="ddlSearch" runat="server" Width="91px">
                                </asp:DropDownList>
                                Từ khóa:
                                <asp:TextBox ID="txtKeyword" runat="server" Width="168px"></asp:TextBox>&nbsp;<asp:Button
                                    ID="cmdSearch" runat="server" CssClass="formSubmitBtn" Font-Bold="True" 
                                    Text="Tìm Kiếm" OnClick="cmdSearch_Click" /></td>
                        </tr>
						
						
						<tr>
							<td width="100%" >
								<asp:datagrid id="dgrCommon" runat="server" Width="100%" BorderStyle="Inset" BorderColor="DarkGray"
									AllowPaging="True"  CellPadding="1" Font-Name="Verdana" PagerStyle-Mode="NumericPages"
									PagerStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#99ccff" 
									CssClass="NormalText" Font-Size="10pt" Font-Names="Arial" PageSize="20">
								
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
								</asp:datagrid>
							</td>
						</tr>
						<tr>
							<td width="100%" style="height: 26px">
                                <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" 
                                    Width="157px" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                </asp:DropDownList><asp:Button id="cmdAdd" runat="server" Text="Thêm" CssClass="formSubmitBtn" onclick="cmdAdd_Click" Font-Bold="True"></asp:Button><asp:Button id="cmdDetele" runat="server" Text="Xoá" CssClass="formSubmitBtn" onclick="cmdDetele_Click" Font-Bold="True"></asp:Button><asp:Button ID="cmdPublish" runat="server" CssClass="formSubmitBtn"
                                    Font-Bold="True" OnClick="cmdPublish_Click" Text="Show" /><asp:Button ID="cmdRemove"
                                        runat="server" CssClass="formSubmitBtn" Font-Bold="True" OnClick="cmdRemove_Click"
                                        Text="Hide" />
                                <asp:Button ID="cmdAlert" runat="server" CssClass="formSubmitBtn" Font-Bold="True"
                                    Text="Alert" OnClick="cmdAlert_Click" />
								<asp:Label id="lblTotalRecords" runat="server" Font-Bold="True" ForeColor="#004000"></asp:Label></td>
						</tr>
                        <tr>
                            <td width="100%">
								</td>
                        </tr>
					</TBODY>
				</table>
			</form>
	</body>
</HTML>
