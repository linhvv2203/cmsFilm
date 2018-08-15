<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlayDigital_Group.aspx.cs" Inherits="CLIP_PlayDigital_Group" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Xem video</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table align="center" cellpadding="2" cellspacing="2" style="font-weight: bold; color: black;
            font-family: 'Times New Roman'">
            <tr>
                <td colspan="2" style="height: 29px">
                    <asp:Label ID="lblDichVu" runat="server" Font-Bold="True" ForeColor="red"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lbsapo" runat="server" Font-Bold="True" ForeColor="red"></asp:Label>
                     <br />
                    <br />
                    <asp:Label ID="lbtag" runat="server" Font-Bold="True" ForeColor="red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" height="3">
                    <div id="tinnganVideoPlayer" style="height: auto !important">
                        <video width="100%" height="200" autoplay  controls>
  <source src="<%=Server.HtmlEncode(sNgheThu.ToString()) %>" type="video/mp4">
</video>
                    </div>
                    <%--        <script src="../js/jwplayer.js" type="text/javascript"></script>

                    
    <div id="tinnganVideoPlayer" style="height:auto !important"></div>
   
    <script type="text/javascript">

     var videoPlayerName = "tinnganVideoPlayer";
     var videoPlayerLink = "<%=sNgheThu %>";
     var videoImageLink = "<%=sAnhNgheThu %>";
  
    var Player = jwplayer("tinnganVideoPlayer").setup({
    flashplayer: "../swf/playervideo.swf",
    skin: "../swf/videoplayer.zip",
      file: videoPlayerLink,
      image: videoImageLink ,
      startparam: "starttime",
      type: "http",
      autostart: "false",
      height: "200",  
      width: "100%",
      "controlbar.position": "bottom",
      "controlbar.idlehide": "false",
      events: {
        onComplete: function(event) {
          document.getElementById('relate').style.display = 'block';
        },
        onPlay: function(event) {
          document.getElementById('relate').style.display = 'none';
        }
      }
    });
    function playFile(fileName) {
      var jw = jwplayer('tinnganVideoPlayer');
      jw.load(fileName);
      jw.play();
    }
  </script>  --%>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
