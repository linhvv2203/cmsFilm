var imgServer = 'http://localhost/cweb/';

String.prototype.trim = function() { //Trim ambas direcciones
   return this.replace(/^[ ]+|[ ]+$/g,"");
}

String.prototype.tripSpace = function() { //Trim ambas direcciones
   return this.replace(/(\s\s+)/g, " ");
	 //replace(/^[ ]+|[ ]+$/g,"");
}

//Kiem tra ki tu dac biet
function existsSpecialChars(str)
{
		//var re = /^[0-9a-zA-Z ]*$/;
		/*
		var re = /([!@#$%^&*]$/;
		str = str.trim();
		var pos = str.search(re);
		if(pos == -1)
			return false;
		else
			return true;
		*/
}	

//Kiem tra URL
function isURL(str) {
	var pattern = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-_.0123456789/~:";
	if (str.length > 0) {
		if (str.length < 5 ) {
			return false;
		} else {
			if (str.lastIndexOf(".") == -1) { 											// khong tim thay dau cham
				return false;
			} else {
				if (str.lastIndexOf(".") == (str.length - 1)) return false;				// dau cham nam o cuoi cung
//				if (!isAlpha(str.charCodeAt(str.lastIndexOf(".") + 1))) return false;	// sau dau cham khong phai ki tu Alphabet
			}
			for (var c=0; c<pattern.length; c++) {
				if (pattern.indexOf(str.charAt(c),0) == -1) return false;				// ki tu khong hop le
			}
		}
	}
	return true;	
}

//Open popup
function openWindow(filename, winname, width, height, feature) {
	var features, top, left;
	var reOpera = /opera/i ;
	var winnameRequired = ((navigator.appName == "Netscape" && parseInt(navigator.appVersion) == 4) || reOpera.test(navigator.userAgent));
	
	left = (window.screen.width - width) / 2;
	top = (window.screen.height - height) / 2;	
	if(feature == '')
		features = "width=" + width + ",height=" + height + ",top=" + top + ",left=" + left + ",status=0,location=0";
	else
		features = "width=" + width + ",height=" + height + ",top=" + top + ",left=" + left + "," + feature;
//	if(!winnameRequired)	winname = "";
	newwindow = window.open(filename,winname,features);
	newwindow.focus();
}
//
//Cookie
function GetCookie(sName)
{
  // cookies are separated by semicolons
 var aCookie = document.cookie.split("; ");
  for (var i=0; i < aCookie.length; i++)
  {
    // a name/value pair (a crumb) is separated by an equal sign
    var aCrumb = aCookie[i].split("=");
    if (sName == aCrumb[0]) 
      return unescape(aCrumb[1]);
  }

  // a cookie with the requested name does not exist
  return null;
}

function cfmDelete(msg)
{
    if( confirm('Are you sure you want to delete: '+ msg+' ?')==false)
    {
        try
        {
            window.event.returnValue = false;
        }
        catch(err){}
        return false;
    }
    return true ;
}

function delAll(msg,l,type,id)
{
//Setup_History
//"window.open('<%=VatLid.Variables.sWebRoot%>ConfirmDelete.aspx?setType=<%# DataBinder.Eval(Container.DataItem,"setType")%>&callerName=<%# DataBinder.Eval(Container.DataItem,"callerName")%>&callerNumber=<%# DataBinder.Eval(Container.DataItem,"callerNumber")%>&settingID=<%# DataBinder.Eval(Container.DataItem,"settingID")%>','','top=100,left=200,height=300,width=300,scrollbars=0,toolbar=0')"
    if(cfmDelete(msg)==false) return;
    var url="delAll.aspx?l="+l+"&type="+type+"&id="+id;
    window.open(url,'','top=0,left=0,height=10,width=10,scrollbars=0,toolbar=0');
}

function openCate(type,l,key)
{
    var url;
    if (type=="l")
        url="listcategory.aspx?l="+l+"&catid="+key;    
    else if (type=="s")
        url="listcategory.aspx?l="+l+"&catid=search&key="+key;
    else
        url="index.aspx?l="+l;
    window.location = url;
}

function selectAct(type,pid,catid,id)
{
    var url,size
    switch(type)
    {
        case "cate3":
            url='Users/Send.aspx?pid='+pid+'&catid='+catid+'&id='+id;
            size='top=100,left=200,height=360,width=380,scrollbars=0,toolbar=0,location=0';
            break;
        case "vote":
            url='Users/showvote.aspx?pid='+pid+'&catid='+catid+'&id='+id;
            size='top=100,left=200,height=300,width=490,scrollbars=0,toolbar=0';
            break;        
        case "one":
            url='Users/SendOne.aspx?pid='+pid+'&catid='+catid+'&id='+id;
            size='top=100,left=200,height=350,width=380,scrollbars=0,toolbar=0,location=0';
            break;
        case "rss":
            url='Users/showvote.aspx?pid='+pid+'&catid='+catid+'&id='+id;
            size='top=100,left=200,height=330,width=550,scrollbars=0,toolbar=0';
            break;        
        case "sub":
            url='Users/SendSub.aspx?pid='+pid+'&catid='+catid+'&id='+id;
            size='top=100,left=200,height=210,width=485,scrollbars=0,toolbar=0,location=0';
            break;
        case "search":
            url='Users/SendSearch.aspx?pid='+pid+'&catid='+catid+'&id='+id;
            size='top=100,left=200,height=350,width=380,scrollbars=0,toolbar=0,location=0';
            break;
         case "binh":
            url='Users/SendVote.aspx?pid='+pid+'&catid='+catid+'&id='+id;
            size='top=100,left=200,height=255,width=380,scrollbars=0,toolbar=0,location=0';
            break;
                     
    }
    if (size=='')window.open(url);
    else window.open(url,'',size)
}

function CloseRefreshParent(controlName)
{
  if (opener && !opener.closed) {
    var x = opener.document.getElementById(controlName);
    if (x) {
      x.value = "True";
      opener.document.forms[0].submit();
    }
  }
  window.close();
}