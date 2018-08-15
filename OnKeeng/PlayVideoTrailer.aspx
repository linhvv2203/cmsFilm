<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlayVideoTrailer.aspx.cs" Inherits="OnKeeng_PlayVideoTrailer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="videojs/video-js.css" rel="stylesheet" />

    <script src="http://phim.keeng.vn/js/web/jquery-1.9.0.js"></script>
    <script src="videojs/video.js"></script>
    <script src="videojs/videojs-contrib-hls.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Play video intro </h2>

            <video id="content_video" data-setup="{}" title="" webkit-playsinline="true"
                playsinline="true" class="video-js vjs-default-skin" poster="" width="400" height="400"
                controls preload="auto">

                <source src="<%= Server.HtmlEncode(video_path.ToString())%>" type='video/mp4' />

            </video>
        </div>
        
    </form>
</body>
</html>

