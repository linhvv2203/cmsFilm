<%@ Page language="c#" Inherits="MarketVN.Sysadmin.error_info" CodeFile="error_info.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<HTML>
	<HEAD>
		<title>PORTAL MANAGEMENT</title>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<LINK href="Common/style.css" type="text/css" rel="stylesheet">
		
		<script language="JavaScript" type="text/javascript">
		function open_win()
		{
		window.location.href='login.aspx';
		}
		</script>
	</HEAD>
	<body bgcolor="#f7f7ef">
		<form runat="server" ID="Form1">
			<P align="center" class="formLabel">
				<asp:Label id="lblErrorInfo" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label><font color="#ff0000"><b></b></font></P>
				
			<p align="center" class="formLabel">
				<button onClick="history.back();" name="B2" style="WIDTH: 180px; HEIGHT: 22px" id="BUTTON1"
					type="button"><b><font face="Verdana" size="1">&lt;&lt; Quay lại</font></b></button>
					
					<button  onClick="open_win();" name="B2" style="WIDTH: 180px; HEIGHT: 22px" id="BUTTON2"
					type="button"><b><font face="Verdana" size="1">Quay lại login</font></b></button>
					
					</p>
		</form>
	</body>
</HTML>

