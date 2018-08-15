<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResetPw.aspx.cs" Inherits="SYS_ResetPw" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
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
</head>
<body>
   <form id="Form1" runat="server">
			
				<table cellSpacing="1" cellPadding="1" width="100%" border="0" height="120">
					<TBODY>
					
					    <tr>
							<td width="100%" bgColor="#F0EDE1" valign="middle" height="30">&nbsp;
							    <font color="#000000" size="3">
							    <b>
                                <asp:label id="Label1" runat="server" Font-Size="10pt" Font-Bold="True">THIẾT LẬP LẠI MẬT KHẨU</asp:label>&nbsp;
								<asp:label id="lblError" runat="server" Font-Size="10" ForeColor="Red"></asp:label>
								</b>
								</font>
							</td>
						</tr>
						
						
						<tr>
							<td width="100%">
								<TABLE id="AutoNumber2" style="BORDER-COLLAPSE: collapse" cellSpacing="2" cellPadding="1"
									width="100%" border="0" height="106">
									<TBODY>
                                        <tr>
                                            <td class="formLabel" style="width: 205px">
                                            &nbsp;Tên đăng nhập :
                                            </td>
                                            <td>
                                            <asp:TextBox id="txtName" runat="server" Width="304px" TextMode="SingleLine"></asp:TextBox>
                                            </td>
                                        </tr>
										
										<TR>
											<TD class="formLabel" height="19" style="width: 205px">
                                                &nbsp;Mật khẩu mới:</TD>
											<TD height="19"><font style="FONT-WEIGHT: normal; FONT-SIZE: 11px; CURSOR: default; COLOR: #8c8c8c; FONT-FAMILY: Arial, Verdana, Tahoma, Sans-Serif">
													<asp:TextBox id="txtNewPw" runat="server" Width="304px" TextMode="Password"></asp:TextBox></font></TD>
										</TR>
										<TR>
											<TD class="formLabel" style="width: 205px">
                                                &nbsp;Đánh lại mật khẩu:</TD>
											<TD height="29">
												<asp:TextBox id="txtConfirmPw" runat="server" Width="304px" TextMode="Password"></asp:TextBox></TD>
										</TR>
										<TR>
											<TD class="formLabel" style="width: 205px"></TD>
											<TD height="28">
												<asp:Button id="cmdSave" runat="server" Text="Lưu" CssClass="formSubmitBtn" onclick="cmdSave_Click" Font-Bold="True"></asp:Button></TD>
										</TR>
									</TBODY>
								</TABLE>
							</td>
						</tr>
					</TBODY>
				</table>
		</form>
</body>
</html>
