<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Left.aspx.cs" Inherits="Left" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html style="background: none;">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>MenuPage</title>
    <base target="content">
    <link href="Styles/stylecms.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript">

var persistmenu="yes" 
var persisttype="sitewide" 

if (document.getElementById){
document.write('<style type="text/css">\n')
document.write('.submenu{display: none;}\n')
document.write('</style>\n')
}

function SwitchMenu(obj){
	if(document.getElementById){
	var el = document.getElementById(obj);
	var ar = document.getElementById("masterdiv").getElementsByTagName("span");
		if(el.style.display != "block"){
			for (var i=0; i<ar.length; i++){
				if (ar[i].className=="submenu")
				ar[i].style.display = "none";
			}
			el.style.display = "block";
		}else{
			el.style.display = "none";
		}
	}
}

function get_cookie(Name) { 
var search = Name + "="
var returnvalue = "";
if (document.cookie.length > 0) {
offset = document.cookie.indexOf(search)
if (offset != -1) { 
offset += search.length
end = document.cookie.indexOf(";", offset);
if (end == -1) end = document.cookie.length;
returnvalue=unescape(document.cookie.substring(offset, end))
}
}
return returnvalue;
}

function onloadfunction(){
if (persistmenu=="yes"){
var cookiename=(persisttype=="sitewide")? "switchmenu" : window.location.pathname
var cookievalue=get_cookie(cookiename)
if (cookievalue!="")
document.getElementById(cookievalue).style.display="block"
}
}

function savemenustate(){
var inc=1, blockid=""
while (document.getElementById("sub"+inc)){
if (document.getElementById("sub"+inc).style.display=="block"){
blockid="sub"+inc
break
}
inc++
}
var cookiename=(persisttype=="sitewide")? "switchmenu" : window.location.pathname
var cookievalue=(persisttype=="sitewide")? blockid+";path=/" : blockid
document.cookie=cookiename+"="+cookievalue
}

if (window.addEventListener)
window.addEventListener("load", onloadfunction, false)
else if (window.attachEvent)
window.attachEvent("onload", onloadfunction)
else if (document.getElementById)
window.onload=onloadfunction

if (persistmenu=="yes" && document.getElementById)
window.onunload=savemenustate

    </script>

</head>
<body style="background: none;"  >
    <form id="form1" runat="server">    
        <div id="masterdiv" >
            <ul class="menu">
                <asp:Repeater runat="server" ID="rpCenterMenu" OnItemDataBound="rpCenterMenu_ItemDataBound">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li >
                            <div onclick="SwitchMenu('sub<%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ID").ToString())%>')" >
                                <a style="cursor: pointer;">
                                <div class="menuroot"></d><%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CategoryName").ToString())%></div>
                                </a>
                            </div>
                            <span class="submenu" id="sub<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "ID").ToString())%>">
                                <ul>
                                    <asp:Repeater ID="rpSubMenu" runat="server" >
                                        <ItemTemplate>
                                            <li><a href='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CategoryForder").ToString())%>/<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CategoryLink").ToString())%>' target="content" >
                                                <%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "CategoryName").ToString())%>
                                            </a></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </span>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </form>
</body>
</html>
