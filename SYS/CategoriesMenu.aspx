<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CategoriesMenu.aspx.cs" Inherits="SYS_CategoriesMenu" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title></title>
		<meta http-equiv="Content-Language" content="en-us">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		  <LINK href="../Common/style.css" type="text/css" rel="stylesheet">
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
	<body class="BODY" leftMargin="1" topMargin="1">
		<form id="Form1" runat="server">
			<table cellSpacing="0" cellPadding="1" width="100%" border="0">
				<TBODY>
					    <tr>
							<td width="100%" bgColor="#F0EDE1" valign="middle" height="30">&nbsp;<img src="../images/selectimage.gif" width="20" height="20" align=absbottom />
							    <font color="#000000" size="3">
							    <b>
                                <asp:label id="Label1" runat="server" Font-Size="10pt" Font-Bold="True">DANH SÁCH MENU</asp:label>&nbsp;
								<asp:label id="lblError" runat="server" Font-Size="10" ForeColor="Red"></asp:label>
								</b>
								</font>
							</td>
						</tr>
						<tr>
							<td width="100%" bgColor="#000000" valign="middle" height="1">
							</td>
						</tr>
					<TR>
						<TD class="RadWWrapperBodyCenter" width="100%">
							<TABLE id="ConfigTable" width="100%">
								<TR>
									<TD class="formLabel">
                                        &nbsp;
                                            Menu root:
                                        <asp:dropdownlist id="ddlCategory" runat="server" AutoPostBack="True" Width="163px" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:dropdownlist>
                                        Tìm theo:
										<asp:dropdownlist id="ddlSearch" runat="server" Width="73px"></asp:dropdownlist>
                                        Từ khóa:
										<asp:textbox id="txtKeyword" runat="server" Width="99px"></asp:textbox><asp:button id="cmdSearch" runat="server" CssClass="formSubmitBtn" Text="Tìm kiếm" OnClick="cmdSearch_Click" Font-Bold="True"></asp:button>&nbsp;&nbsp;
									</TD>
								</TR>
								<TR>
									<TD ><asp:datagrid id="dgrCommon" runat="server" CssClass="GridLabel" Width="100%" PageSize="20" AlternatingItemStyle-BackColor="lightgray"
											HeaderStyle-BackColor="#99ccff" PagerStyle-HorizontalAlign="Left" PagerStyle-Mode="NumericPages" CellPadding="1"
											AllowPaging="True" BorderColor="DarkGray" BorderStyle="Inset" >
											<SelectedItemStyle BackColor="#FFC0C0"></SelectedItemStyle>
											<EditItemStyle></EditItemStyle>
											<AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
											<ItemStyle></ItemStyle>
											<HeaderStyle Font-Names="Arial" Font-Bold="True" BorderStyle="None" BackColor="#d2d7da"></HeaderStyle>
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
												
												
												
						                  <asp:TemplateColumn>
								            <HeaderTemplate>Tên Menu</HeaderTemplate>
							                <ItemTemplate>
							                <%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CategoryName").ToString())%>
							                </ItemTemplate>
						                </asp:TemplateColumn>
						                
						                 <asp:TemplateColumn>
								            <HeaderTemplate>Thứ tự</HeaderTemplate>
							                <ItemTemplate>
							                <%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CategoryOrder").ToString())%>
							                </ItemTemplate>
						                </asp:TemplateColumn>
						                
						                  <asp:TemplateColumn>
								            <HeaderTemplate>Thư mục</HeaderTemplate>
							                <ItemTemplate>
							                <%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CategoryForder").ToString())%>
							                </ItemTemplate>
						                </asp:TemplateColumn>
						                
						                  <asp:TemplateColumn>
								            <HeaderTemplate>TTên file</HeaderTemplate>
							                <ItemTemplate>
							                <%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CategoryLink").ToString())%>
							                </ItemTemplate>
						                </asp:TemplateColumn>
						  
						                <asp:TemplateColumn>
										        <HeaderTemplate>Sửa</HeaderTemplate>
											    <ItemTemplate>
										        <a  href="CategoriesMenu_data.aspx?id=<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "id").ToString())%>&type=edit">
										        <img  alt="" title = " <%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CategoryName").ToString())%>"  src="../images/Edit.gif" height="20px" border="0" width="20px"   />    										       										    
										        </a>
											    </ItemTemplate>
							             </asp:TemplateColumn>
							             
												
												<asp:BoundColumn Visible="False"></asp:BoundColumn>
											</Columns>
											<PagerStyle Font-Bold="True" HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></TD>
								</TR>
							</TABLE>
							<TABLE id="TABLE1" width="100%">
								<TR>
									<TD class="formLabel" height="24"><asp:dropdownlist id="ddlStatus" runat="server" AutoPostBack="True" Width="112px" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"></asp:dropdownlist><asp:button id="cmdAdd" runat="server" CssClass="formSubmitBtn" Text="Thêm mới" OnClick="cmdAdd_Click" Font-Bold="True"></asp:button><asp:button id="cmdDetele" runat="server" CssClass="formSubmitBtn" Text="Xóa" OnClick="cmdDetele_Click" Font-Bold="True"></asp:button><asp:button id="cmdPublish" runat="server" CssClass="formSubmitBtn" Text="Duyệt" OnClick="cmdPublish_Click" Font-Bold="True"></asp:button><asp:button id="cmdRemove" runat="server" CssClass="formSubmitBtn" Text="Gỡ Bỏ" OnClick="cmdRemove_Click" Font-Bold="True"></asp:button>&nbsp;<asp:label id="lblTotalRecords" runat="server" ForeColor="#004000" Font-Bold="True"></asp:label></TD>
								</TR>
							</TABLE>
							<DIV>
                                &nbsp;</DIV>
						</TD>
					</TR>
				</TBODY></table>
		</form>
	</body>
</HTML>