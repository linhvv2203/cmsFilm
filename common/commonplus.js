

/***********************************************
* Rollover background-color button Script- © Dynamic Drive (www.dynamicdrive.com)
* This notice must stay intact for use
* Visit http://www.dynamicdrive.com/ for full source code
***********************************************/

//Specify optional button target: "_new" for new window, or name of FRAME target (ie "myframe")
var buttontarget=""

function change(e, color){
var el=window.event? event.srcElement: e.target
if (el.tagName=="INPUT"&&el.type=="button")
el.style.backgroundColor=color
}

function jumpto2(url){
if (buttontarget=="")
window.location=url
else if (buttontarget=="_new")
window.open(url)
else
parent[buttontarget].location=url
}



/***********************************************
* Bookmark site script- © Dynamic Drive DHTML code library (www.dynamicdrive.com)
* This notice MUST stay intact for legal use
* Visit Dynamic Drive at http://www.dynamicdrive.com/ for full source code
***********************************************/

/* Modified to support Opera */
function bookmarksite(title,url){
if (window.sidebar) // firefox
	window.sidebar.addPanel(title, url, "");
else if(window.opera && window.print){ // opera
	var elem = document.createElement('a');
	elem.setAttribute('href',url);
	elem.setAttribute('title',title);
	elem.setAttribute('rel','sidebar');
	elem.click();
} 
else if(document.all)// ie
	window.external.AddFavorite(url, title);
}



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

function Search(keyword, type)
{    
	window.opener.location = "http://www.google.com.vn/search?hl=vi&&q=" + encodeURI(keyword);
	window.opener.focus();
}

function PopUpSong(zing)
{
	if (window.ActiveXObject) {
		openWindow('/popupsong.php?zing=' + zing, 'popupsong', 575, 378, '');
	} else {
		openWindow('/popupsong.php?zing=' + zing, 'popupsong', 575, 378, '');
	}
}

