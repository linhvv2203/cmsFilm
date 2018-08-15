<%@ Page language="c#" Inherits="MarketVN.Sysadmin.function_data" CodeFile="function_data.aspx.cs" %>
<HTML>
	<HEAD>
		<title></title>
		<meta http-equiv="Content-Language" content="en-us">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<LINK href="../Styles/Styles.css" type="text/css" rel="stylesheet">
			<LINK href="../FormContainer.css" type="text/css" rel="stylesheet">
				<LINK rel="stylesheet" type="text/css" href="../Common/style.css">
	</HEAD>
	<body bgcolor="#ffffff" topmargin="0" leftmargin="0">
	<div class="formWrapper">
		<form runat="server" ID="Form1">
			<table border="0" cellpadding="3" cellspacing="3" style="BORDER-COLLAPSE: collapse" bordercolor="#111111"
				width="100%">
				<TBODY>
					<tr>
						<td width="100%" bgcolor="#d2d7da"><font size="3" color="#000000"><b>
									<asp:Label id="Label1" runat="server" Font-Bold="True" Font-Size="10pt"> Sửa chức năng hệ thống</asp:Label>
                            <asp:Label ID="lblError" runat="server" Font-Size="10" ForeColor="Red"></asp:Label></b></font></td>
					</tr>
					<tr>
						<td width="100%">
							<TABLE id="AutoNumber2" style="BORDER-COLLAPSE: collapse" borderColor="#111111" cellSpacing="0"
								cellPadding="3" width="100%" border="0">
								<TBODY>
									<TR>
										<TD noWrap>
                                            <strong>Danh mục:</strong></TD>
										<TD width="100%">
											<asp:TextBox id="txtFuncName" runat="server" Width="304px"></asp:TextBox></TD>
									</TR>
									<TR>
										<TD noWrap style="height: 29px">
                                            <strong>Tệp xử lý:</strong></TD>
										<TD width="100%" style="height: 29px">
											<asp:TextBox id="txtFuncFile" runat="server" Width="304px"></asp:TextBox>
										</TD>
									</TR>
                                    <tr>
                                        <td nowrap="nowrap" style="height: 29px">
                                            <strong>Tên nhóm:</strong></td>
                                        <td style="height: 29px" width="100%">
                                            <asp:DropDownList ID="ddlCategory" runat="server"
                                                Width="302px">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtFuncGroup" runat="server" Width="148px"></asp:TextBox></td>
                                    </tr>
									<TR>
										<TD noWrap>
                                            <strong>Mô tả:</strong></TD>
										<TD width="100%">
											<asp:TextBox id="txtFuncDesc" runat="server" Width="304px" Height="80px"></asp:TextBox>
										</TD>
									</TR>
									<TR>
										<TD noWrap colSpan="2"></TD>
									</TR>
									<TR>
										<TD noWrap colSpan="2">
											<asp:Button id="cmdSave" runat="server" Font-Size="8pt" Text="Lưu" CssClass="formSubmitBtn" onclick="cmdSave_Click" Font-Bold="True"></asp:Button></TD>
									</TR>
		</form>
       </div>
	</body>
</HTML>
