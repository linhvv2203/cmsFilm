<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DangKyDVNN.aspx.cs" Inherits="SYS_DangKyDVNN" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   	<title>DANH SÁCH CHỦ ĐỀ</title>
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
<body class="BODY" leftMargin="3" topMargin="1">
		<form id="Form1" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
			        	<tr>
							<td width="100%" bgColor="#F0EDE1" valign="middle" height="30">&nbsp;
							    <font color="#000000" size="3">
							    <b>
                                <asp:label id="Label1" runat="server" Font-Size="10pt" Font-Bold="True">ĐĂNG KÝ DỊCH VỤ NÔNG NGHIỆP</asp:label>&nbsp;
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
						<table cellSpacing="1" cellPadding="1" width="100%" border="0">
				<tr>
					<td width="100%">
						<TABLE id="AutoNumber2" cellSpacing="1" cellPadding="1" width="100%" border="0">
                            <tr>
                                <td class="formLabel" style="width: 139px">
                                    &nbsp;Số điện thoại:</td>
                                <td height="28">
                                    <asp:TextBox ID="txtPhoneNumber" runat="server" Height="20px" Width="218px"></asp:TextBox></td>
                            </tr>
							<TR>
								<TD class="formLabel" style="width: 139px">
                                    &nbsp;Cú pháp:</TD>
								<TD height="28"><FONT style="FONT-WEIGHT: normal; FONT-SIZE: 11px; CURSOR: default; COLOR: #8c8c8c; FONT-FAMILY: Arial, Verdana, Tahoma, Sans-Serif">
										<asp:TextBox id="txtInfo" runat="server" Height="20px" Width="218px"></asp:TextBox>
                                    
										</FONT></TD>
							</TR>
							<TR>
								<TD class="formLabel" style="width: 139px">
                                    &nbsp;Đầu số</TD>
								<TD height="28">
									<asp:textbox id="txtService" runat="server" Height="20px" Width="218px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD class="formLabel" style="width: 139px"></TD>
								<TD noWrap colSpan="1"><asp:button id="cmdSave" runat="server" CssClass="formSubmitBtn" Text="ĐK dịch vụ" OnClick="cmdSave_Click" Font-Bold="True" Width="68px"></asp:button></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
				
		
					</TR>
				</TBODY></table>
				
				</form>
</body>
</html>
