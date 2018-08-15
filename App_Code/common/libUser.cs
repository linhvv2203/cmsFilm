using System;
using System.Text ;
using System.Data ;
using System.Data.SqlClient ; 
using System.Configuration ;
using System.IO ;
using System.Web;
//using System.Web.Mobile;
using System.Web.SessionState;
using System.Web.UI;
//using System.Web.UI.MobileControls;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ED
{
	/// <summary>
	/// Summary description for libUser.
	/// </summary>
	public class libUser
	{
		public  static string Phone_Name = "";
		public  static string Phone_Des = "";
		public  static string Phone_mp3 = "";
		public  static string Phone_img = "";
		private static string ConnStr = ConfigurationSettings.AppSettings["ConnStr"];

		
		private static string[] listBrower ={ "midp", "symbian", "ppc","pda",  "windows ce",  "wap1", "wap2", "mobile", "phone", "up.browser", "wap browser",
												"mot-", "-sgh", "lg/","mmp/", "pocket", "psp", "palm", "htc", "mini", "blackberry",
												"portable", "brew", "series", "up.link", "android", "docomo", "avantgo", "vodafone" };
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
		public static Boolean isMobileAccept(string userAgent)
		{
			if (userAgent == null) return false;
			userAgent = userAgent.ToLower();
			foreach (string markMobile in listBrower)
			{
				if (userAgent.IndexOf(markMobile) > -1) return true; 
			}        
			return false;
		}
		// Get Phone type, 
		public static Boolean getPhoneName(string sAgent)
		{
			//string isMobile = sAgent.Substring(0, 4);
			for (int i = 0; i < listPhone.Length; i++)
			{
				int j = sAgent.IndexOf(listPhone[i]); //isMobile.Equals(listPhone[i])
				if (j >-1)
				{
					Phone_Des = listPhone[i];
					if (Phone_Des.StartsWith("lg")) Phone_Des = "lg";
					int k = sAgent.IndexOf("/", j);
					if (k > 0) Phone_Name = sAgent.Substring(j, k-j-1);
					else Phone_Name = sAgent;
					if (Phone_Name.Length > 50) Phone_Name=Phone_Name.Substring(0,50); 
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
			try
			{
				FileStream fStream = new FileStream(filepath, FileMode.Open);
				BinaryReader bw = new BinaryReader(fStream);
				imgdata = bw.ReadBytes((int)fStream.Length);
				bw.Close();
				fStream.Close();
                fStream.Dispose();
                if (bw is IDisposable)
                    ((IDisposable)bw).Dispose();
			}
			catch { }

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
			string SQL=string.Format("Exec dbo.Select_File_Support '{0}'",sAgent);
			string []ext=GetDataReaderToStringList(SQL);
			if (ext !=null && ext.Length > 3)
			{
				Phone_Des =ext[0].ToLower();
				Phone_Name =ext[1];
				Phone_mp3 =ext[2];
				Phone_img =ext[3];
				//				if (Phone_mp3.Equals("mp3")) 
				//					Phone_mp3=getMIME_Type("Mp3Type"); 
				//				SQL=getOrgFileName(roodir, itemCode + "."+ext[0].ToLower());
				//				if (SQL !="-2") return SQL ;
				return true ;
			}
			return false;
		}
		private static string getMidp(string sAgent)
		{
			string tmp=sAgent.Replace("-","").Replace(" ","").ToLower();
			if (tmp.IndexOf("midp2.0")> 0 ) return "2";
			return "1";
		}
		/// <summary>
		/// Return File name form Content Type, Code,[ext]
		/// </summary>
		/// <param name="sID"></param>
		/// <param name="sCode"></param>
		/// <param name="ext"></param>
		/// <returns></returns>		
//		public static string getFileName(string sAgent,string type, string code)
//		{
//			if (type == null || type=="" ||
//				code == null || code==""|| sAgent == null || sAgent =="")
//				return "0";
//			if (!isMobileAccept(sAgent)) return "4";
//			if (!getFileSupport(sAgent))
//				if(!getPhoneName(sAgent)) return "3";
//			
//			string roodir, tmp, ext = "";
//			switch (type)
//			{
//				case "1": //TrueTone
//				case "3":
//					if (Phone_mp3.Equals("mp3")) 
//					{
//						ext=getMIME_Type("Mp3Type");
//						roodir = ConfigurationSettings.AppSettings["downPath_1"];
//						tmp= getOrgFileName(roodir,code+ext);
//						if (tmp !="2") return tmp ;
//					}
//					if (Phone_Des.Equals("sams")) ext = "mmf";
//					else ext = "amr";
//					roodir = ConfigurationSettings.AppSettings["downPath_1"];
//					tmp= getOrgFileName(roodir,code+"."+ext);
//					return tmp;
//				case "2": //Poly Tone
//					if (Phone_mp3.Equals("mp3")) 
//					{
//						ext=getMIME_Type("Mp3Type");
//						roodir = ConfigurationSettings.AppSettings["downPath_1"];
//						tmp= getOrgFileName(roodir,code+ext);
//						if (tmp !="2") return tmp ;
//					}
//					if (Phone_Des.Equals("sams")) ext = "amr";
//					else ext = "mid";
//					roodir = ConfigurationSettings.AppSettings["downPath_2"];
//					tmp= getOrgFileName(roodir,code+"."+ext);
//					return tmp;
//				case "4": // Static Image
//				case "5": // Dynamic				
//					ext = code.Substring(code.Length - 1, 1).ToUpper();
//					if (ext == "M") code = code.Replace("M", "_176x176");
//					else if (ext == "L") code = code.Replace("L", "_240x320");
//					roodir = ConfigurationSettings.AppSettings["downPath_" + type];
//					tmp = getOrgFileName(roodir, code.ToLower() + ".");
//					// Auto fix
//					if (tmp == "2" && (ext == "M" || ext == "L"))
//					{
//						try
//						{
//							code = code.Substring(0, code.IndexOf("_") - 1);
//							tmp = getOrgFileName(roodir, code.ToLower() + ".");
//						}
//						catch
//						{
//							tmp = "2";
//						}
//					}
//					return tmp ; //break;
//				case "7": // Karaoke
//					roodir = ConfigurationSettings.AppSettings["downPath_"+ type];
//					tmp = getOrgFileName(roodir, code + ".kar"); // priority jad: 1, jar: 2
//					if (tmp=="2")
//						tmp = getOrgFileName(roodir, code + ".");
//					return tmp ;
//				case "8": // Game Java, 
//					roodir = ConfigurationSettings.AppSettings["downPath_"+ type];
//					if (Phone_Des.Equals("sams")) ext = ".jad";
//					else ext = ".jar";
//					string midp=getMidp(sAgent);
//					tmp = getOrgFileName(roodir, code + "_"+midp+ext); // priority jad: 1, jar: 2
//					if (tmp=="2")
//						tmp = getOrgFileName(roodir, code + ".");
//					return tmp ;
//				default: 
//					roodir = ConfigurationSettings.AppSettings["downPath_"+ type];
//					tmp = getOrgFileName(roodir, code + ".");
//					return tmp ; //break;				
//			}			
//		}

		public static string getFileName(string sAgent,string type, string code)
		{
			if (type == null || type=="" ||
				code == null || code==""|| sAgent == null || sAgent =="")
				return "0";
			if (!isMobileAccept(sAgent)) return "4";
			if (!getFileSupport(sAgent))
				if(!getPhoneName(sAgent)) return "3";
			
			string roodir, tmp, ext = "";
			switch (type)
			{
				case "1": //TrueTone
				case "3":
					if (Phone_mp3.Equals("mp3")) 
					{
						ext=getMIME_Type("Mp3Type");
						roodir = ConfigurationSettings.AppSettings["downPath_1"];
						tmp= getOrgFileName(roodir,code+ext);
						if (tmp !="2") return tmp ;
					}
					if (Phone_Des.Equals("sams")) ext = "mmf";
					else ext = "amr";
					roodir = ConfigurationSettings.AppSettings["downPath_1"];
					tmp= getOrgFileName(roodir,code+"."+ext);
					return tmp;
				case "2": //Poly Tone
					if (Phone_mp3.Equals("mp3")) 
					{
						ext=getMIME_Type("Mp3Type");
						roodir = ConfigurationSettings.AppSettings["downPath_1"];
						tmp= getOrgFileName(roodir,code+ext);
						if (tmp !="2") return tmp ;
					}
					if (Phone_Des.Equals("sams")) ext = "amr";
					else ext = "mid";
					roodir = ConfigurationSettings.AppSettings["downPath_2"];
					tmp= getOrgFileName(roodir,code+"."+ext);
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
					return tmp ; //break;
				case "7": // Karaoke
					roodir = ConfigurationSettings.AppSettings["downPath_"+ type];
					tmp = getOrgFileName(roodir, code + ".kar"); // priority jad: 1, jar: 2
					if (tmp=="2")
						tmp = getOrgFileName(roodir, code + ".");
					return tmp ;
				case "8": // Game Java, 
					roodir = ConfigurationSettings.AppSettings["downPath_"+ type];
					if (Phone_Des.Equals("sams")) ext = ".jad";
					else ext = ".jar";
					string midp=getMidp(sAgent);
					tmp = getOrgFileName(roodir, code + "_"+midp+ext); // priority jad: 1, jar: 2
					if (tmp=="2")
						tmp = getOrgFileName(roodir, code + ext );//"."
					return tmp ;
				default: 
					roodir = ConfigurationSettings.AppSettings["downPath_"+ type];
					tmp = getOrgFileName(roodir, code + ".");
					return tmp ; //break;				
			}			
		}


		public static string getError(string err)
		{
			string vt_web = ConfigurationSettings.AppSettings["WebPath"];
			switch (err)
			{
				case "0":  return "Xin loi quy khach, he thong khong xac dinh duoc yeu cau download." + vt_web;
				case "1":  return "Xin loi quy khach, ma so download khong hop le." + vt_web;
				case "2":  return "Xin loi quy khach, he thong khong tim thay noi dung yeu cau." + vt_web;
				case "3":  return "Xin loi, may dien thoai cua quy khach khong ho tro play noi dung yeu cau." + vt_web;
				default:   return "Xin loi, yeu cau cua quy khach khong hop le." + vt_web;
			}
		}

		#region Send Fake MO
		public static Boolean sendFMO(string UserID,string ReceiverID,string sType,string Info)
		{			
			if (!ReceiverID.Equals(UserID)) Info+=" "+ ReceiverID;
			string tmp=sType.ToUpper();
			string CommandCode=ConfigurationSettings.AppSettings["COMMANDCODE_"+tmp];
			string ServiceID=ConfigurationSettings.AppSettings["SERVICEID_"+tmp];
			tmp=ConfigurationSettings.AppSettings["wsSyntaxMO"];
			Info=CommandCode+" "+Info;
			string SQL=string.Format(tmp, UserID,ServiceID,CommandCode,Info);
			string wsAddress = ConfigurationSettings.AppSettings["wsAddress"];
			string wsAdmin = ConfigurationSettings.AppSettings["wsAdmin"];
			string wsPass = ConfigurationSettings.AppSettings["wsPass"];
			string sRes = "-1";
			try
			{
				using (sendMT proxy = new sendMT())
				{
					proxy.Credentials = new System.Net.NetworkCredential(wsAdmin, wsPass);
					sRes = proxy.InsertFMO("FMO","vtmedia","tvpl123#$",SQL);
				}
			}
			catch{}
			return (sRes == "1");
		}		
		#endregion
		#region Send Other Fake MO
		public static Boolean sendFMO(string UserID,string ReceiverID,string sType,string Info,string command_code)
		{			
			if (!ReceiverID.Equals(UserID)) Info+=" "+ ReceiverID;
			string tmp=sType.ToUpper();
			string CommandCode= command_code + ConfigurationSettings.AppSettings["COMMANDCODE_"+tmp];;
			string ServiceID=ConfigurationSettings.AppSettings["SERVICEID_"+tmp];
			tmp=ConfigurationSettings.AppSettings["wsSyntaxMO"];
			Info=CommandCode.Trim()+" "+Info.Trim();
			string SQL=string.Format(tmp, UserID,ServiceID,CommandCode.Trim(),Info);
			string wsAddress = ConfigurationSettings.AppSettings["wsAddress"];
			string wsAdmin = ConfigurationSettings.AppSettings["wsAdmin"];
			string wsPass = ConfigurationSettings.AppSettings["wsPass"];
			string sRes = "-1";
			try
			{
				using (sendMT proxy = new sendMT())
				{
					proxy.Credentials = new System.Net.NetworkCredential(wsAdmin, wsPass);
					sRes = proxy.InsertFMO("FMO","vtmedia","tvpl123#$",SQL);
				}
			}
			catch{}
			return (sRes == "1");
		}
		#endregion
		#region Query Database
		public static void LogStatus(string RequestID, string UserID, string ReceiverID, string ItemCode, string ItemName, string status)
		{
			if (UserID == null || UserID == "") UserID = "UNKNOW";
			if (ItemCode == null || ItemCode == "") ItemCode = "NOT VALID";
			string SQL = string.Format("Exec dbo.insert_DOWNLOAD_WAP '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}'",
				RequestID, UserID, ReceiverID, ItemCode, ItemName, status,Phone_Des,Phone_Name );
			ExecuteQueryNew(SQL);
		}
		public static string getStatus(string RequestID, string UserID, string ReceiverID, string ItemCode)
		{
			string SQL = string.Format("Exec dbo.select_DOWNLOAD_WAP '{0}','{1}','{2}','{3}','{4}','{5}'",
				RequestID, UserID, ReceiverID, ItemCode,Phone_Des,Phone_Name);
			string[] tmp = GetDataReaderToStringList(SQL);
			return tmp == null ? "" : tmp[0];
		}
		public static int ExecuteQueryNew(string SQL)
		{
			int sResult;
			using (SqlConnection conn = new SqlConnection(ConnStr))
			using (SqlCommand cmd = new SqlCommand(SQL, conn))
			{
				try
				{
					conn.Open();
					sResult = cmd.ExecuteNonQuery();
				}
				catch(Exception ex)
				{
					string tmp=ex.Message ;
					sResult = -1;
				}
			}
			return sResult;
		}


        #region FAKE NEW
        public static bool wait100(string commandcode, string serviceId, string sEncodedPhone, string info)
        {
            //System.Threading.Thread.Sleep(5000);
            string SQL = string.Format("Exec dbo.Select_SMS_FMO_NOGA '{0}','{1}','{2}','{3}'",
                sEncodedPhone, serviceId, commandcode, info);
            return GetDataReaderToArrayList100(SQL) > 0;
        }

        public static bool SentMT100(string commandcode, string serviceId, string sEncodedPhone, string info)
        {
            //System.Threading.Thread.Sleep(5000);
            string SQL = string.Format("Exec dbo.Select_SMS_FMO_SENT '{0}','{1}','{2}','{3}'",
                sEncodedPhone, serviceId, commandcode, info);
            return GetDataReaderToArrayList100(SQL) > 0;
        }
        public static bool SentMT1001(string commandcode, string serviceId, string sEncodedPhone, string info, string type)
        {
            string SQL = string.Format("Exec dbo.Select_SMS_FMO_SENT_NEW '{0}','{1}','{2}','{3}','{4}'",
                sEncodedPhone, serviceId, commandcode, info, type);
            return GetDataReaderToArrayList100(SQL) > 0;
        }
        public static bool SentMT1002(string CPCode, string RequestID, string UserID, string ReceiverID, string ServiceID, string CommandCode, string ContentType, string Info)
        {
            string SQL = string.Format("Exec insert_FromCP_ALL '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}'",
                CPCode, RequestID, UserID, ReceiverID, ServiceID, CommandCode, ContentType, Info);
            return GetDataReaderToArrayList100(SQL) > 0;

        }
		
        public static int GetDataReaderToArrayList100(string SQL)
        {
            int sResult;
            using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Connect100"]))
            using (SqlCommand cmd = new SqlCommand(SQL, conn))
            {
                try
                {
                    conn.Open();
                    sResult = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    string tmp = ex.Message;
                    sResult = -1;
                }
            }
            return sResult;
        }
        public static bool wait98(string commandcode, string serviceId, string sEncodedPhone, string info)
        {
            System.Threading.Thread.Sleep(5000);
            string SQL = string.Format("Exec dbo.Select_SMS_FMO '{0}','{1}','{2}','{3}'",
                sEncodedPhone, serviceId, commandcode, info);
            return GetDataReaderToArrayList98(SQL) > 0;
        }
        public static int GetDataReaderToArrayList98(string SQL)
        {
            int sResult;
            using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["ConnStr10"]))
            using (SqlCommand cmd = new SqlCommand(SQL, conn))
            {
                try
                {
                    conn.Open();
                    sResult = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    string tmp = ex.Message;
                    sResult = -1;
                }
            }
            return sResult;
        }
       

        #endregion
		public static int ExecuteQueryNew98(string SQL)
		{
			int sResult;
            using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["ConnStr10"]))
			using (SqlCommand cmd = new SqlCommand(SQL, conn))
			{
				try
				{
					conn.Open();
					sResult = Convert.ToInt32(cmd.ExecuteScalar());
				}
				catch(Exception ex)
				{
					string tmp=ex.Message ;
					sResult = -1;
				}
			}
			return sResult;
		}
        public static int ExecuteQuery105(string SQL)
        {
            int sResult;
            using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Connect105"]))
            using (SqlCommand cmd = new SqlCommand(SQL, conn))
            {
                try
                {
                    conn.Open();
                    sResult = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    string tmp = ex.Message;
                    sResult = -1;
                }
            }
            return sResult;
        }
		public static string[] GetDataReaderToStringList(string SQL)
		{
			string[] al = null;
			using (SqlConnection conn = new SqlConnection(ConnStr))
			using (SqlCommand cmd = new SqlCommand(SQL, conn))
			{
				try
				{
					conn.Open();
					using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection |
							   CommandBehavior.SingleResult))
					{
						while (reader.Read())
						{
							al = new string[reader.FieldCount];
							for (int i = 0; i < reader.FieldCount; ++i)
								al[i] = reader[i].ToString();
							break;
						}
					}
				}
				catch { al = null; }
			}
			return al;
		}
		#endregion

		#region EnCode/Decode
		public static string decodePhone(string request)
		{	
			//vao truc tiep
			if(request == null)
				return "";

			string tmp= CodeDecoder(request);
			
			if (!tmp.StartsWith("@") || !tmp.EndsWith("*") || tmp.IndexOf("_")==-1)
				return "" ;
			tmp=tmp.Replace("@","").Replace("*","");  
			string[] nn = tmp.Split('_');
			
			if (!nn[0].StartsWith("98") && !nn[0].StartsWith("97") &&
				!nn[0].StartsWith("168") && !nn[0].StartsWith("169"))
			{
				if (nn[0].Length < 9 || nn[1].Length !=4) return "";
				return "84"+nn[0];
			}

			if (nn[0].Length < 9 || nn[1].Length !=4) return "";
			try 
			{
				int curDate= Convert.ToInt32(nn[1]);
				int maxDate=curDate-Convert.ToInt32(DateTime.Now.ToString("MMdd"));
				//so viettel het han
				if ( curDate==0 || maxDate < 0)
					return "-1" ;
			}
			catch { return "" ;}

			return "84"+nn[0];
			//			string tmp= CodeDecoder(request);
			//			if (!tmp.StartsWith("@") || !tmp.EndsWith("*") || tmp.IndexOf("_")==-1)
			//				return "" ;
			//			tmp=tmp.Replace("@","").Replace("*","");  
			//			string[] nn = tmp.Split('_');
			//			if (!nn[0].StartsWith("98") && !nn[0].StartsWith("97") &&
			//				!nn[0].StartsWith("168") && !nn[0].StartsWith("169") )
			//				return "";
			//			if (nn[0].Length < 9 || nn[1].Length !=4) return "";
			//			try 
			//			{
			//				int curDate= Convert.ToInt32(nn[1]);
			//				int maxDate=curDate-Convert.ToInt32(DateTime.Now.ToString("MMdd"));
			//				if ( curDate==0 || maxDate < 0)
			//					return "" ;
			//			}
			//			catch { return "" ;}
			//
			//			return "84"+nn[0];
		}
		public static string CodeEncoder(string sCode) // code@ext@phone
		{        
			string tmp = sCode;        
			byte[] bbin = Encoding.ASCII.GetBytes(tmp);
			bbin[0] ^= 0x71;
			for (int i = 1; i < bbin.Length; i++)
				bbin[i] ^= (byte)(bbin[i - 1] ^ (i << 3) ^ 0x19);      
			tmp = "";
			for (int j = 0; j < bbin.Length; j++)
				tmp += bbin[j].ToString("X2");
			return tmp;
		}
		private static string CodeDecoder(string sCode)
		{
			try
			{
				byte[] bbin = new byte[sCode.Length / 2];
				for (int i = 0; i < sCode.Length; i += 2)
					bbin[i / 2] = Byte.Parse(sCode.Substring(i, 2), System.Globalization.NumberStyles.HexNumber);
				for (int j = bbin.Length - 1; j > 0; j--)
					bbin[j] ^= (byte)(bbin[j - 1] ^ (j << 3) ^ 0x19);
				bbin[0] ^= 0x71;
				return Encoding.ASCII.GetString(bbin);
			}
			catch
			{
				return "";
			}
		}

		#endregion

		   
		public static Boolean DownLoadAny(string filename,HttpResponse response)
		{
			// Ham tra ve noi dung day
			string sDisplay = filename.Substring(filename.LastIndexOf("\\") + 1);
			response.ContentType = ED.libUser.getMIME_Type(filename.Substring(filename.LastIndexOf(".")).ToLower());
			response.Clear();
			response.BufferOutput = true;
			response.AddHeader("Content-Disposition", "inline;filename=" + sDisplay);
			try
			{
				byte[] imgdata = ED.libUser.getStream(filename);
				response.AppendHeader("Content-Length", imgdata.Length.ToString());
				response.OutputStream.Write(imgdata, 0, imgdata.Length);
				response.Flush();
				return true;
			}
			catch
			{
				return false;
			}        
		} 
		public static string getPhoneNames(string sAgent)
		{
			int pos = 0;
			pos = sAgent.IndexOf(" ");
			if( pos < 0 )
			{
				return "";
			}
			else
			{
				string tem = sAgent.Substring(0,pos);
				pos = tem.IndexOf("/");
				if(pos < 0)
					return "";
				else
				{
					return tem.Substring(0,pos);
				}
			}
			
		}
	}
}
