<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlayDigital.aspx.cs" Inherits="POPUP_PlayDigital" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="videojs/video-js.css" rel="stylesheet" />

    <script src="http://phim.keeng.vn/js/web/jquery-1.9.0.js"></script>
    <script src="videojs/video.js"></script>
    <script src="videojs/videojs-contrib-hls.js"></script>

    <style type="text/css">
        .active{
            background-color:#ddd;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        
         <script type="text/javascript">

             function EpisodeAction(Episode) {

                 $.ajax({
                     type: "POST",
                     data: { 'Episode': Episode, 'id': '<%= Server.HtmlEncode(id)%>' },
                     url: 'api/Episode.ashx',
                     success: function (result) {
                      $('.active').removeClass("active");
                      $('#Episode_' + Episode).addClass("active");

                      if (result.toString().length > 10) {
                          try {
                              var video = document.getElementsByTagName('video')[0];
                              var sources = video.getElementsByTagName('source');
                              sources[0].src = result;
                              video.load();
                          } catch (err) { }

                      }
                      else {
                          alert('Video đang được cập nhật');
                      }
                  }
              });
          }
    </script>
    
     <div class="video-wrap">
        <video width="100%" height="200" autoplay poster="<%=Server.HtmlEncode(URLIMAGE.ToString())%>" preload="metadata" controls="" autohide="true" name="media">
        <source src="<%= Server.HtmlEncode(URLVIDEO.ToString())%>" type="video/mp4"></source>
        <div style="background-image:url('<%=Server.HtmlEncode(URLIMAGE)%>');background-position:center center;background-size:100%;height:118px;padding-top: 40px;margin:0 auto;background-repeat:no-repeat"></div>
        </video>
    </div>
    
    <div class="content-player">
        <h2 class="content-player-name"><%=Server.HtmlEncode(ItemName.ToString())%></h2>
        <div id="film-part" class="onbox-part">
            <strong>Phần: </strong>
              <p>  
              <asp:Repeater ID="RepeaterEpisode" runat="server">
                    <HeaderTemplate></HeaderTemplate>
                    <ItemTemplate>
                    <a id="Episode_<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Episode").ToString())%>" class="" 
                    onclick="EpisodeAction(<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Episode").ToString())%>);">
                    Part <%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Episode").ToString())%></a>
                    </ItemTemplate>
                </asp:Repeater>
              </p>  
        </div>
     
        <script type="text/javascript">
            $(document).ready(function () {
                $('#Episode_1').addClass("active");
            });
        </script>
        
        
             <div class="info-relate-film">
            <strong>Lượt xem: </strong> <%= Server.HtmlEncode(view.ToString().Trim()) %><br />
            <strong>Đạo diễn: </strong>
            <%Stt=0; if (RptDirector.Items.Count > 0){ %>
            <asp:Repeater ID="RptDirector" runat="server">
                <HeaderTemplate></HeaderTemplate>
                <ItemTemplate>
                <a title="<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Name").ToString().Trim())%>">
                <%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Name").ToString().Trim())%></a>
                <%Stt++; if(Stt < RptDirector.Items.Count){ %>,&nbsp;<%} %>
                </ItemTemplate>
            </asp:Repeater>
            <%} else { %>Đang cập nhật<%} %>
            <br />
            <strong>Thể loại: </strong> 
            <a title="<%= Server.HtmlEncode(cate_name.ToString().Trim())%>">
            <%= Server.HtmlEncode(cate_name.ToString().Trim())%></a>
            <br />
            <strong>Năm: </strong> <%= Server.HtmlEncode(YearProduct.ToString())%><br />
            <strong>Quốc gia: </strong>
            <a title="<%= Server.HtmlEncode(Country.ToString())%>">
            <%= Server.HtmlEncode(Country.ToString().Trim())%></a><br />
            <strong>Diễn viên: </strong> 
            <%Stt=0; if (RptActor.Items.Count > 0){ %>
            <asp:Repeater ID="RptActor" runat="server">
                <HeaderTemplate></HeaderTemplate>
                <ItemTemplate>
                <a title="<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Name").ToString())%>">
                <%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "Name").ToString().Trim())%></a>
                <%Stt++; if(Stt < RptActor.Items.Count){ %>,&nbsp;<%} %>
                </ItemTemplate>
            </asp:Repeater>
            <%} else { %>Đang cập nhật<%} %>
            <br />
        </div>
            
            <div class="descript-detail"><%= Server.HtmlEncode(description.ToString())%></div>
        </div>
    
    
        
        
    </div>
    </form>
</body>
</html>
