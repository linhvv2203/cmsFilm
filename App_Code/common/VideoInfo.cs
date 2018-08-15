using MediaInfoLib;
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;


/// <summary>
/// Summary description for VideoInfo
/// </summary>
public class VideoInfo
{

    public string Codec;
    public int Width;
    public int Heigth;
    public double FrameRate;
    public string FrameRateMode;
    public string ScanType;
    public TimeSpan Duration;
    public int Bitrate;
    public string AspectRatioMode;
    public double AspectRatio;

    public VideoInfo(MediaInfo mi)
    {
        Codec = mi.Get(StreamKind.Video, 0, "Format");
        Width = int.Parse(mi.Get(StreamKind.Video, 0, "Width"));
        Heigth = int.Parse(mi.Get(StreamKind.Video, 0, "Height"));
        Duration = TimeSpan.FromMilliseconds(int.Parse(mi.Get(StreamKind.Video, 0, "Duration")));
        Bitrate = int.Parse(mi.Get(StreamKind.Video, 0, "BitRate"));
        AspectRatioMode = mi.Get(StreamKind.Video, 0, "AspectRatio/String"); //as formatted string
        AspectRatio = double.Parse(mi.Get(StreamKind.Video, 0, "AspectRatio"));
        FrameRate = double.Parse(mi.Get(StreamKind.Video, 0, "FrameRate"));
        FrameRateMode = mi.Get(StreamKind.Video, 0, "FrameRate_Mode");
        ScanType = mi.Get(StreamKind.Video, 0, "ScanType");
    }
	
}