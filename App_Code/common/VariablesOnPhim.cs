using System;
using System.Configuration;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace VatLidOnPhim
{

    public class Variables
    {
        public static string ImageLinks1 = "../images/Delete.gif";
        public static string ImageLinks2 = "../images/button-huyDK.gif";
        public static string ImageLinks3 = "../images/button-gui.gif";
        public static string ImageLinks4 = "../images/nhac-chuong.gif";
        public static string ImageLinks7 = "../images/button-xem.gif";


        public static string ImageLinks5 = "../images/select.gif";
        public static string ImageLinks6 = "../images/mail.png";

        public static string sHome = ConfigurationSettings.AppSettings["sHome"];
        public static string sWebRoot = ConfigurationSettings.AppSettings["sWebRoot"];
        public static string sWebImage = ConfigurationSettings.AppSettings["TextPath"];

        public static string sImageCK = "../images/";

        public static string sFullPathPlay = ConfigurationSettings.AppSettings["PlayDigital"];
        public static string sPolyDBPathPlay = ConfigurationSettings.AppSettings["PolyDBPath"];
        public static string sTrueDBPathPlay = ConfigurationSettings.AppSettings["TrueDBPath"];
        public static string sMonoDBPathPlay = ConfigurationSettings.AppSettings["MonoDBPath"];

        public static string sNewsImageDB = ConfigurationSettings.AppSettings["NEWS_Image_DB"];
        public static string sNewsImageView = ConfigurationSettings.AppSettings["NEWS_Image_View"];

        public static string sHinhNenDBPath = ConfigurationSettings.AppSettings["HinhNenDBPath"];
        public static string sHinhDongDBPath = ConfigurationSettings.AppSettings["HinhDongDBPath"];
        public static string sGameDBPath = ConfigurationSettings.AppSettings["GameDBPath"];
        public static string sImageGameOnline = ConfigurationSettings.AppSettings["GAONLINE_Play"];

        public static string sPhimPathMp4 = ConfigurationSettings.AppSettings["FilmMp4PathPlay"];
        public static string sPhimPathImage = ConfigurationSettings.AppSettings["FilmPhotoPathPlay"];


        public static string sVideoDBPath = ConfigurationSettings.AppSettings["VideoDBPath"];
        public static string sKaraokeDBPath = ConfigurationSettings.AppSettings["KraokeDBPath"];

        public static string sNameDBPath = ConfigurationSettings.AppSettings["NameDBPath"];
        public static string sThemeDBPath = ConfigurationSettings.AppSettings["ThemeDBPath"];

        public static string sGameDBPathView = ConfigurationSettings.AppSettings["GameDBPath"];

        public static string sBackGroundPathPlay = ConfigurationSettings.AppSettings["HinhNenDBPath"];
        public static string sImageDynamicPathPlay = ConfigurationSettings.AppSettings["HinhDongDBPath"];
        public static string sImageGame = ConfigurationSettings.AppSettings["MOBILE_Path_Play"];


        public static string sImageVideo = ConfigurationSettings.AppSettings["VideoDBPath"];

        public static string sVideoPathLead = ConfigurationSettings.AppSettings["VideoMp4DBPathPlay"];
        //public static string sVideoPathPlay = ConfigurationSettings.AppSettings["VideoDBPathPlay"];

        public static string sVideoPathPlay = ConfigurationSettings.AppSettings["VideoDBPathPlay_OnPhim"];

        public static string ImageVideoPath_Chanel = ConfigurationSettings.AppSettings["ImageVideoPath_Chanel"];
        public static string ImageVideoUrl_Chanel = ConfigurationSettings.AppSettings["ImageVideoUrl_Chanel"];

        public static string ImagePath_Playlist = ConfigurationSettings.AppSettings["ImageVideoPath_Playlist"];
        public static string ImageUrl_PlayList = ConfigurationSettings.AppSettings["ImageVideoUrl_PlayList"];

        public static string Image_Categories = ConfigurationSettings.AppSettings["Image_Categories"];
        public static string ImageUrl_Categories = ConfigurationSettings.AppSettings["ImageUrl_Categories"];
        public static string ImageUrl_Sale = ConfigurationSettings.AppSettings["ImageUrl_Sale"];


        public static string ImageLink3 = VatLidOnPhim.Variables.sWebRoot + "images/button.gif";

        public static string EditTitle = "<img src='" + VatLidOnPhim.Variables.sWebRoot + "images/Edit.gif' border=0 title='Edit' WIDTH='16' HEIGHT='16'></img>";
        public static string RenameTitle = "<img src='" + VatLidOnPhim.Variables.sWebRoot + "images/copy.gif' border=0 title='Rename' WIDTH='16' HEIGHT='16' ></img>";
        public static string DeleteTitle = "<img src='" + VatLidOnPhim.Variables.sWebRoot + "images/delete.gif' border=0 title='Delete' WIDTH='16' HEIGHT='16' ></img>";

        public static string PermissionTitle = "<img src='" + VatLidOnPhim.Variables.sWebRoot + "images/user.gif' border=0 title='Phân quyền'></img>";
        public static string ViewTitle = "<img src='" + VatLidOnPhim.Variables.sWebRoot + "images/bullet.gif' border=0 title='View'></img>";
        public static string DemoMusic = "<img src='" + VatLidOnPhim.Variables.sWebRoot + "images/icon-speaker_blue.gif' border=0 title='Nghe thử'></img>";
        public static string NoteNhac = "<img src='" + VatLidOnPhim.Variables.sWebRoot + "images/notnhac.gif' border=0 title='Nghe thử'></img>";
        public static string ActionTitle = "<img src='" + VatLidOnPhim.Variables.sWebRoot + "images/icon_inbox.gif' border=0 title='Chức năng'></img>";
        public static string ChooseTitle = "<img src='" + VatLidOnPhim.Variables.sWebRoot + "images/user.gif' border=0 title='Chọn khách hàng'></img>";

        public static string sLinkToResource = ConfigurationSettings.AppSettings["linkToResource"];
        public static string sPathToResource = ConfigurationSettings.AppSettings["pathToResource"];
        public static string linkThiAnh = ConfigurationSettings.AppSettings["linkThiAnh"];




        public static int GIAVANG = 1;
        public static int TYGIA = 2;
        public static int THOITIET = 3;
        public static int GIANNONGSAN = 4;
        public static int XOSO = 5;
        public static int BONGDA = 6;
        public static int CHUNGKHOAN = 7;
        public static int DIENTHOAI = 8;
        public static int XEMAY = 9;
        public static int LICHCHIEUFILM = 10;
        public static int TUDIEN = 11;


        public static string ImageCK1 = VatLidOnPhim.Variables.sWebRoot + "images/button.gif";
        public static string ImageCK2 = VatLidOnPhim.Variables.sWebRoot + "images/button.gif";


        public static int XNKCho = 1;
        public static int XNKSo = 2;
        public static int XNKChuong = 3;
        public static int XNKRadio = 4;
        public static int XNKQuaTang = 5;
        public static int XNKTheoTen = 6;
        public static int XNKIstory = 7;
        public static int XNKFlash = 8;
        public static int XNKVideo = 9;
        public static int XNKEmoticon = 10;
        public static int XNKHinhAnhDong = 11;
        public static int XNKHinhAnhNen = 12;
        public static int XNKThiepNhac = 13;
        public static int XNKTruyenTranh = 14;

    }


    public class LogType
    {
        public static string LogIn = "LogIn";
        public static string LogOut = "LogOut";
        public static string ADD = "4";
        public static string UPDATE = "3";
        public static string Delete = "0";
        public static string Publish = "2";
        public static string Remove = "1";
        public static string DeleteAll = "Xóa all";
        public static string AddToGoi = "AddToGoi";
        public static string AddToSub = "AddToSub";
        public static string ShowHome = "Set Home";
        public static string HideHome = "Hide Home";
        public static string ShowDemo = "ShowDemo";
        public static string HideDemo = "HideDemo";
        public static string ShowCount = "ShowCount";
        public static string HideCount = "HideCount";
        public static string ShowMonth = "ShowMonth";
        public static string HideMonth = "HideMonth";
        public static string AddGoiVideoChuDe = "AddGoiVideoChuDe";
        public static string GoiVideoTuan = "GoiVideoTuan";
        public static string ShowTG = "ShowTG";
        public static string HideTG = "HideTG";
        public static string ShowLQ = "ShowLQ";
        public static string HideLQ = "HideLQ";
        public static string ShowTopCate = "ShowTopCate";
        public static string HideTopCate = "HideTopCate";
        public static string AddVinaMobi = "AddVinaMobi";
        public static string ShowFree = "ShowFree";
        public static string HideFree = "HideFree";
        public static string CMDTop = "CMDTop";
        public static string cmdTraTin = "cmdTraTin";
        public static string HotTuan = "HotTuan";
        public static string Hot = "Hot";
        public static string HideHot = "HideHot";
        public static string cmdCon = "cmdCon";
        public static string SendSMS = "SendSMS";
        public static string SendSMSFree = "SendSMSFree";
        public static string SetPriority = "SetPriority";
        public static string HidePriority = "HidePriority";
        public static string ImportExcel = "ImportExcel";
        public static string ChangerPw = "ChangerPw";
        public static string SetSendMail = "SetSendMail";
        public static string UpdateVangOL = "UpdateVangOL";

        public static string InsertAlias = "InsertAlias";
        public static string DKDV = "DKDV";

        //public static int LogIn = 1;
        //public static int LogOut = 2;
        //public static int ADD = 3;
        //public static int UPDATE = 4;
        //public static int Delete = 5;
        //public static int Publish = 6;
        //public static int Remove = 7;
        //public static int DeleteAll = 8;
        //public static int AddToGoi = 9;
        //public static int AddToSub = 10;
        //public static int ShowHome = 11;
        //public static int HideHome = 12;
        //public static int ShowDemo = 13;
        //public static int HideDemo = 14;
        //public static int ShowCount = 15;
        //public static int HideCount = 16;
        //public static int ShowMonth = 17;
        //public static int HideMonth = 18;
        //public static int AddGoiVideoChuDe = 19;
        //public static int GoiVideoTuan = 20;
        //public static int ShowTG = 21;
        //public static int HideTG = 22;
        //public static int ShowLQ = 23;
        //public static int HideLQ = 24;
        //public static int ShowTopCate = 25;
        //public static int HideTopCate = 26;
        //public static int AddVinaMobi = 27;
        //public static int ShowFree = 28;
        //public static int HideFree = 29;
        //public static int CMDTop = 30;
        //public static int cmdTraTin = 31;
        //public static int HotTuan = 32;
        //public static int Hot = 33;
        //public static int HideHot = 34;
        //public static int cmdCon = 35;
        //public static int SendSMS = 36;
        //public static int SendSMSFree = 37;
        //public static int SetPriority = 38;
        //public static int HidePriority = 39;
        //public static int ImportExcel = 40;
        //public static int ChangerPw = 41;
        //public static int SetSendMail = 42;
        //public static int UpdateVangOL = 43;

        //public static int InsertAlias = 44;
        //public static int DKDV = 45;

    }
    public class LogTypeNew
    {
        public static int DELETE = 1;
        public static int ADD = 2;
        public static int UPDATE = 3;
        public static int PUBLISH = 4;
        public static int UNPUBLISH = 5;
        public static int CHANGE_CATEGORY = 6;
        public static int MOVE_BOX = 7;
        public static int SET_PUBLISH_SCHEDULE = 8;
        public static int SET_TOPCATE = 9;
        public static int SET_HOME = 10;
        public static int SET_HOME_3 = 11;
        public static int SET_HOME_VIDEO = 12;
        public static int RELATE = 37;

        //public static int SET_TOP = 12;
        public static int SET_CATE = 13;


        public static int GET_POST = 22;
        public static int NOUSER = 23;
        public static int EDIT = 24;
        public static int REJECT = 25;
        public static int CHANGE_TO_DI_NEW = 26;
        public static int SHOW_SENTENCE = 27;
        public static int UNPUBLISHLONGMESSAGE = 28;
        public static int PUBLISHLONGMESSAGE = 29;
        public static int DELETELONGMESSAGE = 30;
        public static int HIDELONGMESSAGE = 31;
        public static int SET_LONG_MESSAGE = 32;
        public static int PUBLISH_OK = 33;
        public static int AWAITPUBLISH_OK = 34;
        public static int AWAIT1PUBLISH_OK = 35;
        public static int DELETEPUBLISH_OK = 36;
        public static int TO_DATVIET = 37;


        public static int RECOVERY = 38;
        public static int ImageSlideShow = 39;
        public static int CateVideo = 40;
        public static int isOpen = 41;

        public static int dutd = 45;
        public static int datviet = 46;
        public static int Top3 = 47;
        public static int Top5 = 48;
        public static int TopcateWeb = 49;
        public static int TopcateCMWeb = 50;
        public static int TopVideoWeb = 51;
        public static int NewsLong = 52;
        public static int setTT = 53;
        public static int setAudio = 54;
        public static int setShortURL = 55;
        public static int Crawler = 56;
        public static int TieuDiem = 57;
        public static int deleteRadio = 58;
        public static int deleteLong = 59;
        public static int top1radio = 60;
        public static int top3radio = 61;

    }

}
