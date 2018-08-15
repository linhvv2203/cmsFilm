using System;
using System.Data;
using System.Web;
//using System.Web.Mobile;
using System.Web.SessionState;
using System.Web.UI;
//using System.Web.UI.MobileControls;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;
using VatLid;


public class libPhone
{
    public libPhone()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static string Phone_Name = "";
    public static string Phone_Des = "";
    public static string Phone_mp3 = "";
    public static string Phone_img = "";
    private static string ConnStr = ConfigurationSettings.AppSettings["ConnStr"];

    private static string[] listBrower ={ "up.browser", "up.link", "windows ce", "iemobile", "mini",
											"mmp", "symbian", "midp", "wap", "phone", "pocket", "mobile", "pda", "psp" };
    private static string[] listPhone ={ "acs-", "alav", "alca", "amoi", "audi", "aste", "avan", 
										   "benq", "bird", "blac", "blaz", "brew", "cell", "cldc", "cmd-", "dang", 
										   "doco", "eric", "hipt", "inno", "ipaq", "java", "jigs", "kddi", "keji", 
										   "leno", "lg-c", "lg-d", "lg-g", "lge-", "maui", "maxo", "midp", "mits", 
										   "mmef", "mobi", "mot-", "moto", "mwbp", "nec-", "newt", "noki", "opwv", 
										   "palm", "pana", "pant", "pdxg", "phil", "play", "pluc", "port", "prox", 
										   "qtek", "qwap", "sage", "sams", "sany", "sch-", "sec-", "send", "seri", 
										   "sgh-", "shar", "sie-", "siem", "smal", "smar", "sony", "sph-", "symb", 
										   "t-mo", "teli", "tim-", "tosh", "tsm-", "upg1", "upsi", "vk-v", "voda", 
										   "w3c ", "wap-", "wapa", "wapi", "wapp", "wapr", "webc", "winw", "xda ", "xda-", " ppc"};

    // check web browser
    private static Boolean isMobileAccept(string sAgent)
    {
        if (sAgent.IndexOf("windows") > -1 &&
            sAgent.IndexOf("windows ce") == -1) return false;
        for (int i = 0; i < listBrower.Length; i++)
        {
            if (sAgent.IndexOf(listBrower[i]) > -1)
                return true;
        }
        return false;
    }
    // Get Phone type, 
    private static Boolean getPhoneName(string sAgent)
    {
        //string isMobile = sAgent.Substring(0, 4);
        for (int i = 0; i < listPhone.Length; i++)
        {
            int j = sAgent.IndexOf(listPhone[i]); //isMobile.Equals(listPhone[i])
            if (j > -1)
            {
                Phone_Des = listPhone[i];
                if (Phone_Des.StartsWith("lg")) Phone_Des = "lg";
                int k = sAgent.IndexOf("/", j);
                if (k > 0) Phone_Name = sAgent.Substring(j, k - j - 1);
                else Phone_Name = sAgent;
                if (Phone_Name.Length > 50) Phone_Name = Phone_Name.Substring(0, 50);
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// Get MIME from Registry of Server
    /// </summary>
    /// <param name="ext"></param>
    /// <returns></returns>
    public static string getMIME_Type(string ext)
    {
        try
        {
            return ConfigurationSettings.AppSettings[ext];
        }
        catch
        {
            return "application/octet-stream";
        }

    }
    /// <summary>
    /// Get Stream Data from File
    /// </summary>
    /// <param name="filepath"></param>
    /// <returns></returns>
    public static byte[] getStream(string filepath)
    {
        byte[] imgdata = null;
        FileStream fStream = null;
        BinaryReader bw = null;
        try
        {
            fStream = new FileStream(filepath, FileMode.Open);
            bw = new BinaryReader(fStream);
            imgdata = bw.ReadBytes((int)fStream.Length);
            bw.Close();
            fStream.Close();
            fStream.Dispose();
            if (bw is IDisposable)
                ((IDisposable)bw).Dispose();
        }
        catch { }
        finally
        {
        }

        return imgdata;
    }
    /// <summary>
    /// Return origin File name: Folder\Name (Game, Karaoke,Image)\code.*
    /// </summary>
    /// <param name="cpfolder"></param>
    /// <param name="phonemodel"></param>
    /// <returns></returns>
    private static string getOrgFileName(string folder, string code)
    {
        string[] files = Directory.GetFiles(folder);
        foreach (string filename in files)
        {
            if (filename.ToLower().IndexOf("\\" + code) != -1)
                return filename;
        }
        return "2";
    }
    private static Boolean getFileSupport(string sAgent)
    {
        string SQL = string.Format("Exec dbo.Select_File_Support '{0}'", sAgent);
        string[] ext = VatLid.DAL.GetDataReaderToStringList(SQL);
        if (ext != null && ext.Length > 3)
        {
            Phone_Des = ext[0].ToLower();
            Phone_Name = ext[1];
            Phone_mp3 = ext[2];
            Phone_img = ext[3];
            //				if (Phone_mp3.Equals("mp3")) 
            //					Phone_mp3=getMIME_Type("Mp3Type"); 
            //				SQL=getOrgFileName(roodir, itemCode + "."+ext[0].ToLower());
            //				if (SQL !="-2") return SQL ;
            return true;
        }
        return false;
    }
    private static string getMidp(string sAgent)
    {
        string tmp = sAgent.Replace("-", "").Replace(" ", "").ToLower();
        if (tmp.IndexOf("midp2.0") > 0) return "2";
        return "1";
    }
    /// <summary>
    /// Return File name form Content Type, Code,[ext]
    /// </summary>
    /// <param name="sID"></param>
    /// <param name="sCode"></param>
    /// <param name="ext"></param>
    /// <returns></returns>		
    public static string getFileName(string sAgent, string type, string code)
    {
        if (type == null || type == "" || code == null || code == ""
            || sAgent == null || sAgent == "")
            return "0";

        if (!isMobileAccept(sAgent)) return "4";
        if (!getFileSupport(sAgent))
            if (!getPhoneName(sAgent)) return "3";

        string roodir, tmp, ext = "";
        switch (type)
        {
            case "1": //TrueTone
            case "3":
                roodir = ConfigurationSettings.AppSettings["downPath_1"];
                if (Phone_mp3.Equals("mp3"))
                {
                    ext = getMIME_Type("Mp3Type");
                    tmp = getOrgFileName(roodir, code + ext);
                    if (tmp != "2") return tmp;
                }
                if (Phone_Des.Equals("sams")) ext = "mmf";
                else ext = "amr";
                tmp = getOrgFileName(roodir, code + "." + ext);
                return tmp;
            case "2": //Poly Tone
                roodir = ConfigurationSettings.AppSettings["downPath_2"];
                if (Phone_mp3.Equals("mp3"))
                {
                    ext = getMIME_Type("Mp3Type");
                    tmp = getOrgFileName(roodir, code + ext);
                    if (tmp != "2") return tmp;
                }
                if (Phone_Des.Equals("sams")) ext = "amr";
                else ext = "mid";
                tmp = getOrgFileName(roodir, code + "." + ext);
                return tmp;
            case "4": // Static Image
            case "5": // Dynamic				
                ext = code.Substring(code.Length - 1, 1).ToUpper();
                if (ext == "M") code = code.Replace("M", "_176x176");
                else if (ext == "L") code = code.Replace("L", "_240x320");
                roodir = ConfigurationSettings.AppSettings["downPath_" + type];
                tmp = getOrgFileName(roodir, code.ToLower() + ".");
                // Auto fix
                if (tmp == "2" && (ext == "M" || ext == "L"))
                {
                    try
                    {
                        code = code.Substring(0, code.IndexOf("_") - 1);
                        tmp = getOrgFileName(roodir, code.ToLower() + ".");
                    }
                    catch
                    {
                        tmp = "2";
                    }
                }
                return tmp; //break;
            case "7": // Karaoke
                roodir = ConfigurationSettings.AppSettings["downPath_" + type];
                tmp = getOrgFileName(roodir, code + ".kar"); // priority jad: 1, jar: 2
                if (tmp == "2")
                    tmp = getOrgFileName(roodir, code + ".");
                return tmp;
            case "8": // Game Java, 
                roodir = ConfigurationSettings.AppSettings["downPath_" + type];
                if (Phone_Des.Equals("sams")) ext = ".jad";
                else ext = ".jar";
                string midp = getMidp(sAgent);
                tmp = getOrgFileName(roodir, code + "_" + midp + ext); // priority jad: 1, jar: 2
                if (tmp == "2")
                    tmp = getOrgFileName(roodir, code + ".");
                return tmp;
            default:
                //				case "6": //MP3
                //				case "9": //VIDEO
                roodir = ConfigurationSettings.AppSettings["downPath_" + type];
                //					tmp=getFileSupport(roodir,code,type);
                //					if (tmp=="")
                tmp = getOrgFileName(roodir, code + ".");
                return tmp; //break;				
        }

    }
    public static string getError(string err)
    {
        string vt_web = ConfigurationSettings.AppSettings["WebPath"];
        switch (err)
        {
            case "0": return "Xin loi quy khach, he thong khong xac dinh duoc yeu cau download." + vt_web;
            case "1": return "Xin loi quy khach, ma so download khong hop le." + vt_web;
            case "2": return "Xin loi quy khach, he thong khong tim thay noi dung yeu cau." + vt_web;
            case "3": return "Xin loi, may dien thoai cua quy khach khong ho tro play noi dung yeu cau." + vt_web;
            default: return "Xin loi, yeu cau cua quy khach khong hop le." + vt_web;
        }
    }


    #region Query Database
    public static void LogStatus(string RequestID, string UserID, string ReceiverID, string ItemCode, string ItemName, string status)
    {
        if (UserID == null || UserID == "") UserID = "UNKNOW";
        if (ItemCode == null || ItemCode == "") ItemCode = "NOT VALID";
        string SQL = string.Format("Exec dbo.insert_DOWNLOAD_WAP '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}'",
            RequestID, UserID, ReceiverID, ItemCode, ItemName, status, Phone_Des, Phone_Name);
        VatLid.DAL.ExecuteQueryNew(SQL);
    }
    public static string getStatus(string RequestID, string UserID, string ReceiverID, string ItemCode)
    {
        string SQL = string.Format("Exec dbo.select_DOWNLOAD_WAP '{0}','{1}','{2}','{3}','{4}','{5}'",
            RequestID, UserID, ReceiverID, ItemCode, Phone_Des, Phone_Name);
        string[] tmp = VatLid.DAL.GetDataReaderToStringList(SQL);
        return tmp == null ? "" : tmp[0];
    }
    #endregion

}

