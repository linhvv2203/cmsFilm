<%@ Page language="c#" Inherits="MarketVN.Sysadmin.Login" CodeFile="Login.aspx.cs" %>
<%@ Register Src="Captcha.ascx" TagName="Captcha" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height:100%"><head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
<title>PLATFORM CONTENT MANAGEMENT</title>
<link href="Styles/style-login.css" type="text/css" rel="stylesheet">
<link href="Styles/style_edit.css" type="text/css" rel="stylesheet">

<script type="text/javascript" language="JavaScript">
if (document.all)
{     
    document.onkeydown = function ()
    {     
        var key_enter= 13; // 13 = Enter   
        if (key_enter==event.keyCode)
        {
            event.keyCode=0;
            document.getElementById('cmdDangNhap').click();          
            return false;
        } 
    }
}
</script>
			
</head>
  <body>
   
<table height="100%" border="0" cellpadding="0" cellspacing="0" width="100%">    
<tbody><tr style="height:80%">
<td align="center">
<table border="0" cellpadding="0" cellspacing="0" width="100%">
<tbody><tr>

<td style="width: 925px">                   
<table height="99%" border="0" cellpadding="0" cellspacing="0" width="100%">
                   	
<tbody><tr> 
<td align="center">
<form runat="server" ID="Form1">
<div>
</div>

<div>
</div>

<div class="cms-login">
	<div class="logo-csm-vt">
    	CMS PLATFORM VIDEO
    </div>                                    
    <div class="form-csm-p">
    	<span class="icon_user">
        	<img src="Styles/icon_user.png">
        </span>
        <input id="txtUserName" runat="server" name="txtUserName" tabindex="1" class="txt-cms" placeholder="Nhập tên tài khoản" type="text" />
    </div>
    
    <div class="form-csm-p">
    	<span class="icon_user">
        	<img src="Styles/icon_pass.png">
        </span>
        <input name="txtPass" id="txtPass" runat="server" tabindex="2" class="txt-cms" placeholder="Nhập mật khẩu" autocomplete="off" type="password">
    </div>
    
     <div class="form-csm-p form-captcha">
         <input id="txtMaBaoMat" runat="server" name="txtMaBaoMat" tabindex="2" autocomplete="off" class="txt-cms" placeholder="Nhập mã Captcha" type="text"/>
         <a class="captcha-lnk">
    <div>
         <uc1:Captcha ID="Captcha1" runat="server" src="Styles/CaptchaImage.gif" />                                                            
    </div>
    </a>
    </div>
    <div class="form-csm-p form-cms-btn">        
    <asp:ImageButton ID="cmdDangNhap" runat="server"  tabindex="4" class="btn-cms"  
            name="cmdDangNhap" src="" value="Đăng nhập" type="image" onclick="cmdDangNhap_Click1" 
            />
    </div>
    <div class="form-csm-p form-cms-btn">
         <asp:Label ID="lblMessage" runat="server" style="color:Red;font-size:12px;font-weight:bold;"></asp:Label>
    </div>

</div>
</form>	
</td>
</tr>
</tbody>
</table>                    
</td>
</tr>
</tbody>
</table>        
</td>
</tr>
</tbody>
</table>
<div class="link">
<table border="0" cellpadding="1" cellspacing="1" width="100%">
<tbody><tr>
<td width="1"></td>
<td height="23" width="16"><a href="#"><img src="Styles/Vt.png" height="16px" width="16px"></a></td>
<td width="76"><a href="#"> <span class="bolt">Viettel Portal</span></a></td>

<td height="23" width="16"><a href="#"><img src="Styles/musicstore2.png" height="16px" width="16px"></a></td>
<td width="74"><a href="#"><span class="bolt">Music Portal</span></a></td>

<td height="23" width="16"><a href="#"><img src="Styles/games.png" height="16px" width="16px"></a></td>
<td width="75"><a href="#"><span class="bolt">Game Portal</span></a></td>
<td height="23" width="764">
</td></tr>
</tbody>
</table>
</div>
<div class="footer">© Copyright 2009 - Viettel Telecom - Trung tâm phát triển nội dung</div>
</body>
</html>