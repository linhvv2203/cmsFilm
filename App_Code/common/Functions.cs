using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Collections.Generic;
using System.Web;

namespace VatLid
{

    public class DAL
    {

        public static string ConnStr1 = SecureConnection.GetCnxStringDecode("ConnCenter");
        private static string ConnOnphim = SecureConnection.GetCnxStringDecode("ConnOnphim");


        private static string ConnStr2 = SecureConnection.GetCnxString("ConnStr2");
        private static string ConnStr3 = SecureConnection.GetCnxString("ConnStr3");
        private static string ConnStr4 = SecureConnection.GetCnxString("ConnStr4");
        private static string ConnStr5 = SecureConnection.GetCnxString("ConnStr5");

        public static string currentError = "";

        private static string same_phone = "0123456789";

        public static string ERROR_MESSAGE = "Máy chủ hiện đang quá tải. Xin bạn vui lòng quay lại sau giây lát!";
        //private static SqlConnection conn = null;

        public static bool HasRights(HttpRequest request)
        {
            string FileName = VatLid.Utils.GetFileWithParam(request);
            return DAL.GetRights(HttpContext.Current.Session["USER"].ToString(), HttpContext.Current.Session["USERID"].ToString(), FileName);
        }

        public static bool GetRights(string sUserName, string UserID, string FuncFile)
        {
            FuncFile = VatLid.Utils.KillChars(FuncFile);
            if (sUserName == "admin" || sUserName == "Administrator")
                return true;
            string SQL = @"select ID FROM CategoriesMenu where ParentID>0 and ID IN (select MenuID from RightsMenu where RightsStatus=1 AND UserID=" + UserID + @")
                        AND CHARINDEX(CategoryLink,'" + FuncFile + "') > 0 ";
            try
            {
                ArrayList al = GetDataReaderToArrayList(SQL);
                if (al.Count != 0)
                    return true;
                else
                    if (sUserName.ToUpper() == "SUPERADMIN")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
                return false;
            }
        }

        public static string getConnectionStringOnPhim()
        {
            return ConnOnphim;
        }


        public static string getConnectionString1()
        {
            return ConnStr1;
        }
        public static string getConnectionString2()
        {
            return ConnStr2;
        }

        public static void ResetToken(HiddenField hiddenToken)// Refresh token
        {
            HttpContext.Current.Session["token_Bid"] = Guid.NewGuid().ToString();
            hiddenToken.Value = HttpContext.Current.Session["token_Bid"].ToString();
        }

        public static Boolean CheckPermissionByToken(HiddenField hiddenToken)// Check tooken
        {
            try
            {
                if (HttpContext.Current.Session["token_Bid"].ToString() == hiddenToken.Value)
                {
                    ResetToken(hiddenToken);//sau khi check la thuc hien reset luon
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public static string GenNiceUrl(Object objurl)
        {
            try
            {
                String url = objurl.ToString();
                url = ConvertUnicodeDungSan_ToHop(url); //Convert Unincode dung san thanh Uinicode to hop
                String niceurl = ConvertVietnameseCharacterToEN(url);

                niceurl = StripTags(niceurl);
                niceurl = niceurl.Replace(" -", "-");
                niceurl = niceurl.Replace(" ", "-");
                niceurl = niceurl.Replace("_", "-");
                niceurl = niceurl.Replace("nbsp;", "-");
                niceurl = niceurl.Replace("--", "-");

                niceurl = removeChar(niceurl, new String[] { "'", "/", "m²", ":", ",", "<", ">", "”", "“", ".", "!", "?", "@", "#", "$", "%", "^", "&", "*", "(", ")", "+", "~", "`", "\"" });
                niceurl = niceurl.ToLower();

                return niceurl;
            }
            catch
            {
                return "page";
            }
        }
        public static string StripTags(Object data)
        {
            String html = data as String;
            string pattern = @"<(.|\n)*?>";
            return Regex.Replace(html, pattern, string.Empty);
        }

        public static String removeChar(String niceurl, String[] danhsach)
        {
            foreach (String xoa in danhsach)
            {
                niceurl = niceurl.Replace(xoa, "");
            }
            return niceurl;
        }

        public static string ConvertUnicodeDungSan_ToHop(string input)
        {
            //'Dau ngang 
            input = input.Replace("A", "A");
            input = input.Replace("a", "a");
            input = input.Replace("Ă", "Ă");
            input = input.Replace("ă", "ă");
            input = input.Replace("Â", "Â");
            input = input.Replace("â", "â");
            input = input.Replace("E", "E");
            input = input.Replace("e", "e");
            input = input.Replace("Ê", "Ê");
            input = input.Replace("ê", "ê");
            input = input.Replace("I", "I");
            input = input.Replace("i", "i");
            input = input.Replace("O", "O");
            input = input.Replace("o", "o");
            input = input.Replace("Ô", "Ô");
            input = input.Replace("ô", "ô");
            input = input.Replace("Ơ", "Ơ");
            input = input.Replace("ơ", "ơ");
            input = input.Replace("U", "U");
            input = input.Replace("u", "u");
            input = input.Replace("Ư", "Ư");
            input = input.Replace("ư", "ư");
            input = input.Replace("Y", "Y");
            input = input.Replace("y", "y");

            //Dau huyen
            input = input.Replace("À", "À");
            input = input.Replace("à", "à");
            input = input.Replace("Ằ", "Ằ");
            input = input.Replace("ằ", "ằ");
            input = input.Replace("Ầ", "Ầ");
            input = input.Replace("ầ", "ầ");
            input = input.Replace("È", "È");
            input = input.Replace("è", "è");
            input = input.Replace("Ề", "Ề");
            input = input.Replace("ề", "ề");
            input = input.Replace("Ì", "Ì");
            input = input.Replace("ì", "ì");
            input = input.Replace("Ò", "Ò");
            input = input.Replace("ò", "ò");
            input = input.Replace("Ồ", "Ồ");
            input = input.Replace("ồ", "ồ");
            input = input.Replace("Ờ", "Ờ");
            input = input.Replace("ờ", "ờ");
            input = input.Replace("Ù", "Ù");
            input = input.Replace("ù", "ù");
            input = input.Replace("Ừ", "Ừ");
            input = input.Replace("ừ", "ừ");
            input = input.Replace("Ỳ", "Ỳ");
            input = input.Replace("ỳ", "ỳ");

            //Dau sac
            input = input.Replace("Á", "Á");
            input = input.Replace("á", "á");
            input = input.Replace("Ắ", "Ắ");
            input = input.Replace("ắ", "ắ");
            input = input.Replace("Ấ", "Ấ");
            input = input.Replace("ấ", "ấ");
            input = input.Replace("É", "É");
            input = input.Replace("é", "é");
            input = input.Replace("Ế", "Ế");
            input = input.Replace("ế", "ế");
            input = input.Replace("Í", "Í");
            input = input.Replace("í", "í");
            input = input.Replace("Ó", "Ó");
            input = input.Replace("ó", "ó");
            input = input.Replace("Ố", "Ố");
            input = input.Replace("ố", "ố");
            input = input.Replace("Ớ", "Ớ");
            input = input.Replace("ớ", "ớ");
            input = input.Replace("Ú", "Ú");
            input = input.Replace("ú", "ú");
            input = input.Replace("Ứ", "Ứ");
            input = input.Replace("ứ", "ứ");
            input = input.Replace("Ý", "Ý");
            input = input.Replace("ý", "ý");

            //Dau hoi
            input = input.Replace("Ả", "Ả");
            input = input.Replace("ả", "ả");
            input = input.Replace("Ẳ", "Ẳ");
            input = input.Replace("ẳ", "ẳ");
            input = input.Replace("Ẩ", "Ẩ");
            input = input.Replace("ẩ", "ẩ");
            input = input.Replace("Ẻ", "Ẻ");
            input = input.Replace("ẻ", "ẻ");
            input = input.Replace("Ể", "Ể");
            input = input.Replace("ể", "ể");
            input = input.Replace("Ỉ", "Ỉ");
            input = input.Replace("ỉ", "ỉ");
            input = input.Replace("Ỏ", "Ỏ");
            input = input.Replace("ỏ", "ỏ");
            input = input.Replace("Ổ", "Ổ");
            input = input.Replace("ổ", "ổ");
            input = input.Replace("Ở", "Ở");
            input = input.Replace("ở", "ở");
            input = input.Replace("Ủ", "Ủ");
            input = input.Replace("ủ", "ủ");
            input = input.Replace("Ử", "Ử");
            input = input.Replace("ử", "ử");
            input = input.Replace("Ỷ", "Ỷ");
            input = input.Replace("ỷ", "ỷ");

            //Dau nga
            input = input.Replace("Ã", "Ã");
            input = input.Replace("ã", "ã");
            input = input.Replace("Ẵ", "Ẵ");
            input = input.Replace("ẵ", "ẵ");
            input = input.Replace("Ẫ", "Ẫ");
            input = input.Replace("ẫ", "ẫ");
            input = input.Replace("Ẽ", "Ẽ");
            input = input.Replace("ẽ", "ẽ");
            input = input.Replace("Ễ", "Ễ");
            input = input.Replace("ễ", "ễ");
            input = input.Replace("Ĩ", "Ĩ");
            input = input.Replace("ĩ", "ĩ");
            input = input.Replace("Õ", "Õ");
            input = input.Replace("õ", "õ");
            input = input.Replace("Ỗ", "Ỗ");
            input = input.Replace("ỗ", "ỗ");
            input = input.Replace("Ỡ", "Ỡ");
            input = input.Replace("ỡ", "ỡ");
            input = input.Replace("Ũ", "Ũ");
            input = input.Replace("ũ", "ũ");
            input = input.Replace("Ữ", "Ữ");
            input = input.Replace("ữ", "ữ");
            input = input.Replace("Ỹ", "Ỹ");
            input = input.Replace("ỹ", "ỹ");

            //Dau nang
            input = input.Replace("Ạ", "Ạ");
            input = input.Replace("ạ", "ạ");
            input = input.Replace("Ặ", "Ặ");
            input = input.Replace("ặ", "ặ");
            input = input.Replace("Ậ", "Ậ");
            input = input.Replace("ậ", "ậ");
            input = input.Replace("Ẹ", "Ẹ");
            input = input.Replace("ẹ", "ẹ");
            input = input.Replace("Ệ", "Ệ");
            input = input.Replace("ệ", "ệ");
            input = input.Replace("Ị", "Ị");
            input = input.Replace("ị", "ị");
            input = input.Replace("Ọ", "Ọ");
            input = input.Replace("ọ", "ọ");
            input = input.Replace("Ộ", "Ộ");
            input = input.Replace("ộ", "ộ");
            input = input.Replace("Ợ", "Ợ");
            input = input.Replace("ợ", "ợ");
            input = input.Replace("Ụ", "Ụ");
            input = input.Replace("ụ", "ụ");
            input = input.Replace("Ự", "Ự");
            input = input.Replace("ự", "ự");
            input = input.Replace("Ỵ", "Ỵ");
            input = input.Replace("ỵ", "ỵ");

            input = input.Replace("Đ", "Đ");
            input = input.Replace("đ", "đ");

            return input;
        }
        public static string ConvertVietnameseCharacterToEN(string sCharacter)
        {
            string sTemplate = "ĂẮẰẲẴẶăắằẳẵặÂẤẦẨẪẬâấầẩẫậÁÀẢÃẠáàảãạÔỐỒỔỖỘôốồổỗộƠỚỜỞỠỢơớờởỡợÓÒỎÕỌóòỏõọĐđÊẾỀỂỄỆêếềểễệÉÈẺẼẸéèẻẽẹƯỨỪỬỮỰưứừửữựÚÙỦŨỤúùủũụÍÌỈĨỊíìỉĩịÝỲỶỸỴýỳỷỹỵ";
            string sReplate = "AAAAAAaaaaaaAAAAAAaaaaaaAAAAAaaaaaOOOOOOooooooOOOOOOooooooOOOOOoooooDdEEEEEEeeeeeeEEEEEeeeeeUUUUUUuuuuuuUUUUUuuuuuIIIIIiiiiiYYYYYyyyyy";
            char[] arrChar = sTemplate.ToCharArray();
            char[] arrReChar = sReplate.ToCharArray();
            string sResult = "";//sCharacter;
            char digit;

            for (int ch = 0; ch < sCharacter.Length; ch++)
            {
                digit = Convert.ToChar(sCharacter.Substring(ch, 1));//arrChar[ch].ToString();;
                for (int i = 0; i < arrChar.Length; i++)
                    if (digit.Equals(arrChar[i]))
                        digit = arrReChar[i];
                sResult += digit;
            }
            return sResult;
        }
        public static string getConfig(string configName)
        {
            try
            {
                return ConfigurationManager.AppSettings[configName];
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string checkPhone(string phone)
        {
            phone = phone.Trim();
            if (string.IsNullOrEmpty(phone)) return "";
            for (int i = 0; i < phone.Length; i++)
            {
                if (same_phone.IndexOf(phone[i]) == -1)
                    return "";
            }
            if (phone.StartsWith("0"))
                phone = "84" + phone.Substring(1);
            else
                if (!phone.StartsWith("84"))
                    phone = "84" + phone;

            if (phone.Length < 11)
                phone = "";
            else if (phone.Length > 13)
                phone = "";

            return phone;
        }

        public static void ExceptionProcess(Exception e)
        {
            //MessageBox.Show(e.ToString()); 
        }

        public static void ExceptionProcess1(string e)
        {
            //MessageBox.Show(e.ToString()); 
        }

        public static DataSet GetDataSet(string store, string connectionString)
        {
            DataSet mDataSet = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = store;
                try
                {
                    conn.Open();
                    //cmd.CommandTimeout = 5;
                    using (SqlDataAdapter reader = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            mDataSet = new DataSet();
                            reader.Fill(mDataSet);
                        }
                        catch
                        {
                            mDataSet = null;
                        }
                    }
                }
                catch
                {
                    mDataSet = null;
                }
                finally
                {
                    conn.Close();
                }
            }
            return mDataSet;
        }
        public static void ExecuteNonQuery_pro(string store, SqlCommand cmd)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr1))
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = store;
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static DataSet GetDataSet(string store, CommandType cmdType, string connectionString)
        {
            DataSet mDataSet = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = cmdType;
                cmd.CommandText = store;
                try
                {
                    conn.Open();
                    //cmd.CommandTimeout = 5;
                    using (SqlDataAdapter reader = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            mDataSet = new DataSet();
                            reader.Fill(mDataSet);
                        }
                        catch (Exception e1)
                        {
                            mDataSet = null;
                            currentError = e1.Message + Environment.NewLine + e1.StackTrace;
                        }
                    }
                }
                catch (Exception ex)
                {
                    mDataSet = null;
                    currentError = ex.Message + Environment.NewLine + ex.StackTrace;
                }
                finally
                {
                    conn.Close();
                }
            }
            return mDataSet;
        }
        public static DataSet GetDataSet(string storeprocedure, string connectionString, SqlCommand cmd)
        {
            DataSet mDataSet = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storeprocedure;
                try
                {
                    conn.Open();
                    //cmd.CommandTimeout = 5;
                    using (SqlDataAdapter reader = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            mDataSet = new DataSet();
                            reader.Fill(mDataSet);
                        }
                        catch (Exception ex)
                        {
                            mDataSet = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    mDataSet = null;
                }
                finally
                {
                    conn.Close();
                }
            }
            return mDataSet;
        }

        public static string InsertMO(int ID)
        {
            string tmp2 = "Exec dbo.Insert_FromMOHUY {0}";
            string SQL = string.Format(tmp2, ID);
            return ExecuteQueryE2(SQL);
        }

        public static string InsertVIDEOSUB(string ItemCode, string ItemName)
        {
            string tmp2 = "Exec dbo.insert_ItemVideoSub '{0}',N'{1}'";
            string SQL = string.Format(tmp2, ItemCode, ItemName);
            return ExecuteQueryE(SQL);
        }

        public static string InsertGAMESUB(string GameID, string code, string ItemTitle)
        {
            string tmp2 = "Exec dbo.insert_ItemGameSub '{0}','{1}',N'{2}'";
            string SQL = string.Format(tmp2, GameID, code, ItemTitle);
            return ExecuteQueryE(SQL);
        }

        public static string InsertMUSICSUB(string ItemCode, string ItemName)
        {
            string tmp2 = "Exec dbo.insert_ItemMusicSub '{0}',N'{1}'";
            string SQL = string.Format(tmp2, ItemCode, ItemName);
            return ExecuteQueryE(SQL);
        }

        public static string InsertCRBTBYNAME(string ItemCode, string ItemName)
        {
            string tmp2 = "Exec dbo.insert_ItemCRBTNAME '{0}',N'{1}'";
            string SQL = string.Format(tmp2, ItemCode, ItemName);
            return ExecuteQueryE(SQL);
        }
        public static string InsertCRBTTIM(string ItemCode, int catid)
        {
            string tmp2 = "Exec dbo.insert_CRBT_TIM '{0}',{1}";
            string SQL = string.Format(tmp2, ItemCode, catid);
            return ExecuteQueryE(SQL);
        }

        public static string updateTBCRBT_MX(string ItemName, string Desc, int ID)
        {
            string tmp2 = "Exec dbo.update_TBCRBT_MX N'{0}',N'{1}',{2}";
            string SQL = string.Format(tmp2, ItemName, Desc, ID);
            return ExecuteQueryE(SQL);
        }

        public static string updateTBCRBT_VinaMobi(string vinacode, string mobicode, int ID)
        {
            string tmp2 = "Exec dbo.update_TBCRBT_VinaMobi N'{0}',N'{1}',{2}";
            string SQL = string.Format(tmp2, vinacode, mobicode, ID);
            return ExecuteQueryE(SQL);
        }

        public static string InsertMUSICALBUM(int albumID, string ItemCode, string ItemName)
        {
            string tmp2 = "Exec dbo.insert_ItemMusicAlbum {0},'{1}',N'{2}'";
            string SQL = string.Format(tmp2, albumID, ItemCode, ItemName);
            return ExecuteQueryE(SQL);
        }
        public static string InsertMUSICGOI(int catid, string ItemCode, string ItemName)
        {
            string tmp2 = "Exec dbo.insert_MusicStore {0},'{1}',N'{2}'";
            string SQL = string.Format(tmp2, catid, ItemCode, ItemName);
            return ExecuteQueryE(SQL);
        }
        public static string InsertSTORYCGOI(int catid, string ItemCode, string ItemName)
        {
            string tmp2 = "Exec dbo.insert_ItemStoryPocketList {0},'{1}',N'{2}'";
            string SQL = string.Format(tmp2, catid, ItemCode, ItemName);
            return ExecuteQueryE(SQL);
        }
        public static string InsertCOMICCGOI(int catid, string ItemCode, string ItemName)
        {
            string tmp2 = "Exec dbo.insert_ItemComicPocketList {0},'{1}',N'{2}'";
            string SQL = string.Format(tmp2, catid, ItemCode, ItemName);
            return ExecuteQueryE(SQL);
        }
        public static string InsertPHOTOGOI(string ItemCode, string ItemName)
        {
            string tmp2 = "Exec dbo.insert_PhotoStore '{0}',N'{1}'";
            string SQL = string.Format(tmp2, ItemCode, ItemName);
            return ExecuteQueryE(SQL);
        }
        public static string InsertPHOTOGOI_NEW(int catid, string ItemCode, string ItemName)
        {
            string tmp2 = "Exec dbo.insert_PhotoStore_new {0},'{1}',N'{2}'";
            string SQL = string.Format(tmp2, catid, ItemCode, ItemName);
            return ExecuteQueryE(SQL);
        }
        public static string InsertPHOTOGOI_DONG(int catid, string ItemCode, string ItemName)
        {
            string tmp2 = "Exec dbo.insert_PhotoStore_New_Dynamic {0},'{1}',N'{2}'";
            string SQL = string.Format(tmp2, catid, ItemCode, ItemName);
            return ExecuteQueryE(SQL);
        }
        public static string InsertPHOTOSUB(string ItemCode, string ItemName)
        {
            string tmp2 = "Exec dbo.insert_PhotoSub '{0}',N'{1}'";
            string SQL = string.Format(tmp2, ItemCode, ItemName);
            return ExecuteQueryE(SQL);
        }

        public static string InsertMUSIC(string ItemCode, string ItemName)
        {
            string tmp2 = "Exec dbo.insert_MusicStore '{0}',N'{1}'";
            string SQL = string.Format(tmp2, ItemCode, ItemName);
            return ExecuteQueryE(SQL);
        }

        public static string InsertBOOKGOI(int catid, string ItemCode, string ItemName)
        {
            string tmp2 = "Exec dbo.insert_BookStore {0},'{1}',N'{2}'";
            string SQL = string.Format(tmp2, catid, ItemCode, ItemName);
            return ExecuteQueryE(SQL);
        }

        public static string InsertBOOKSUB(string ItemCode, string ItemName)
        {
            string tmp2 = "Exec dbo.insert_BookSub N'{0}',N'{1}'";
            string SQL = string.Format(tmp2, ItemCode, ItemName);
            return ExecuteQueryE(SQL);
        }


        private static string ExecuteQueryE(string SQL)
        {
            int sResult = 0;
            using (SqlConnection conn = new SqlConnection(ConnStr1))
            using (SqlCommand cmd = new SqlCommand(SQL, conn))
            {
                try
                {
                    conn.Open();
                    sResult = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch { sResult = 0; }
            }
            return sResult.ToString();
        }
        private static string ExecuteQueryE2(string SQL)
        {
            int sResult = 0;
            using (SqlConnection conn = new SqlConnection(ConnStr2))
            using (SqlCommand cmd = new SqlCommand(SQL, conn))
            {
                try
                {
                    conn.Open();
                    sResult = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch { sResult = 0; }
            }
            return sResult.ToString();
        }


        public static string getAPPPartID(Object objurl)
        {
            String sql = "select name from AppCps where ID=" + objurl;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            return "Chưa xác định";
        }

        public static string getAPPFromPartID(Object objurl)
        {
            String sql = String.Format("select [Name] from AppCps where id in (select PartID from Apps where id = {0})", objurl);
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            return "Chưa xác định";
        }

        public static string getCategoriesPartID(Object objurl)
        {
            String sql = "select TOP(1) Name from ItemGamesCp where ID=" + objurl;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            return "Chưa xác định";
        }

        public static string getSERVICES(Object objurl)
        {
            String sql = "select TOP(1) Name from SERVICES where ID=" + objurl;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            return "Miễn phí";
        }

        public static int GetParnerIDFromUserLogin(string user)
        {
            try
            {
                string sql = String.Format("select top(1) id from AppCps where UserLogin = '{0}'", user.Trim());
                DataSet ds = VatLid.SqlHelper.ExecuteDataset(VatLid.DAL.getConnectionString1(), CommandType.Text, sql);
                string code = ds.Tables[0].Rows[0]["id"].ToString();

                int id = Int32.Parse(code);

                return id;
            }
            catch
            {
                return 0;
            }
        }

        public static string getFileImage(string id)
        {
            String sql = "select FileImage from ItemGames where ID=" + id;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            return "Template.jpg";
        }
        public static string getStatus(Object objurl)
        {
            string status = "0";
            String sql = "select Status from ItemGames where ID=" + objurl;

            string txt = "";
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                status = dl[0].ToString();
                if (status == "2")
                    txt = "Published";
                else if (status == "1")
                    txt = "Pending";
                else
                    txt = "Deleted";

            }
            return txt;
        }
        public static string getISURL(Object objurl)
        {
            String sql = "select IsFile from ItemGames where ID=" + objurl;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            else
            {
                return dl[0];
            }
        }
        public static string getGamePartID(Object objurl)
        {
            String sql = "";
            sql = "select PartID from ItemGames where ID=" + objurl;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                string id = dl[0];
                sql = "select Name from ItemGamesCp where ID=" + id;
                String[] dl1 = VatLid.DAL.GetDataReaderToStringList(sql);
                if (dl1 != null)
                {
                    return dl1[0];
                }
                else
                {
                    return "Chưa xác định";
                }
            }
            else
            {
                return "Chưa xác định";
            }


        }

        public static string getSERVICE(string ID)
        {
            String sql = "select Name from Services_Name where ID=" + ID;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            return "";
        }

        public static string getGameName(string ID)
        {
            String sql = "select Title from ItemGames where ID=" + ID;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            return "Chưa xác định";
        }
        public static string getGameCode(string ID)
        {
            String sql = "select Code from ItemGames where ID=" + ID;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            return "Chưa xác định";
        }
        public static string getGameID(Object objurl)
        {
            String sql = "select Title from ItemGames where ID=" + objurl;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            return "Chưa xác định";
        }
        public static string getAppID(Object objurl)
        {
            String sql = "select Name from Apps where ID=" + objurl;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            return "Chưa xác định";
        }
        public static string getItemStoryName(string id)
        {
            String sql = "select ItemName from ItemStory where ID=" + id;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            return "Chưa xác định";
        }
        public static string getItemComicName(string id)
        {
            String sql = "select ItemName from ItemComic where ID=" + id;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            return "Chưa xác định";
        }
        public static string getMusic(string id)
        {
            String sql = "select ItemName from MusicRight where ID=" + id;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            return "Chưa xác định";
        }
        public static string getVideoName(string id)
        {
            String sql = "select ItemName from ItemVideo where ID=" + id;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            return "Chưa xác định";
        }
        public static string getItemCode(string id)
        {
            String sql = "select ItemCode from MusicRight where ID=" + id;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            return "Chưa xác định";
        }
        public static string getItemCodeStory(string id)
        {
            String sql = "select ItemCode from ItemStory where ID=" + id;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            return "Chưa xác định";
        }
        public static string getItemCodeComic(string id)
        {
            String sql = "select ItemCode from ItemComic where ID=" + id;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            return "Chưa xác định";
        }
        public static string getItemCodeBook(string id)
        {
            String sql = "select ItemCode from ItemBook where ID=" + id;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            return "Chưa xác định";
        }

        public static string getItemNameBook(string id)
        {
            String sql = "select FileName from ItemBook where ID=" + id;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            return "Chưa xác định";
        }
        public static string getCodeCRBT(string id)
        {
            String sql = "select toneCode from TbRBTs where ID=" + id;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            return "Chưa xác định";
        }
        public static string getNameCRBT(string id)
        {
            String sql = "select toneName from TbRBTs where ID=" + id;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            return "Chưa xác định";
        }
        public static string getItemVideo(string id)
        {
            String sql = "select ItemCode from ItemVideo where ID=" + id;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            return "Chưa xác định";
        }
        public static string getUsersKHDN(string id)
        {
            String sql = "select CommandCode from UsersKHDN where ID=" + id;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            return "Chưa xác định";
        }
        public static string getCategoriesList(Object objurl)
        {
            String sql = "select CategoryName from GameCategories where ID=" + objurl;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            return "Chưa xác định";
        }

        public static string getAPPCategory(Object objurl)
        {
            String sql = "select name from AppCate where ID=" + objurl;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            return "Chưa xác định";
        }

        public static string getCategoriesListNN(Object objurl)
        {
            String sql = "select CategoryNamNN from GameCategories where ID=" + objurl;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            return "Anonymous";
        }


        public static Boolean hwSMS_RBT(string RequestID, string sendNumber, string recvNumber, string ServiceID, string Content, string ContentType)
        {
            string sRes = "-1";
            try
            {
                using (sendMT proxy = new sendMT())
                {
                    proxy.Credentials = new System.Net.NetworkCredential(libConst.mdAdmin, libConst.mdPass);
                    sRes = proxy.InsertCP(libConst.mdCPCode, RequestID, sendNumber, recvNumber, ServiceID, libConst.mdRBTCmd, Content, ContentType);
                }
            }
            catch { }
            return sRes.Equals("1");
        }

        public static string DownLoadOnline(string UserID, string ItemCode, string Status)
        {
            string mdAdmin = ConfigurationSettings.AppSettings["mdAdmin"];
            string mdPass = ConfigurationSettings.AppSettings["mdPass"];
            string tmp = ConfigurationSettings.AppSettings["wsSyntaxMO"];
            string SQL = string.Format(tmp, UserID, ItemCode, Status);

            string sRes = "-1";
            try
            {
                using (sendMT proxy = new sendMT())
                {
                    proxy.Credentials = new System.Net.NetworkCredential(mdAdmin, mdPass);
                    sRes = proxy.InsertFMO("IMO", "vtmedia", "tvpl123#$", SQL);
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

            if (sRes == "-1")
                return "Không thể thực hiện: Lỗi cập nhật dữ liệu !";

            return "1";
        }

        public static void FillDataToDropdownList_OnKid(DropDownList ddlList, string TableName, string FieldList, string where)
        {
            string[] arrField;
            arrField = FieldList.Split(',');
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@table", TableName);
            cmd.Parameters.AddWithValue("@id", arrField[0]);
            cmd.Parameters.AddWithValue("@name", arrField[1]);
            cmd.Parameters.AddWithValue("@where", where);
            DataSet ds = new DataSet();
            try
            {
                ds = VatLid.DAL.GetDataSet("[CMS_FillDataToDropdownList_v2]", cmd);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlList.DataSource = ds.Tables[0];
                    ddlList.DataMember = TableName;
                    ddlList.DataValueField = arrField[0];// Field1;
                    ddlList.DataTextField = arrField[1];// Field2;			
                    ddlList.DataBind();
                }
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
            }
        }

        public static void FillDataToDropdownList_Categories(DropDownList ddlList, string TableName, string FieldList)
        {
            string SQL = "SELECT " + FieldList + " FROM " + TableName;
            string[] arrField;
            arrField = FieldList.Split(',');
            DataSet ds = new DataSet();
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(ConnStr1);
                SqlDataAdapter da = new SqlDataAdapter(SQL, conn);
                da.Fill(ds, TableName);
                ddlList.DataSource = ds;
                ddlList.DataMember = TableName;
                ddlList.DataValueField = arrField[0];// Field1;
                ddlList.DataTextField = arrField[1];// Field2;			
                ddlList.DataBind();
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public static void FillDataToDropdownListAll(DropDownList ddlList, string TableName, string FieldList)
        {
            string SQL = "SELECT " + FieldList + " FROM " + TableName;
            string[] arrField;
            arrField = FieldList.Split(',');
            DataSet ds = new DataSet();
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(ConnStr1);
                SqlDataAdapter da = new SqlDataAdapter(SQL, conn);
                da.Fill(ds, TableName);
                ddlList.DataSource = ds;
                ddlList.DataMember = TableName;
                ddlList.DataValueField = arrField[0];// Field1;
                ddlList.DataTextField = arrField[1];
                ddlList.DataBind();
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }
        public static string[] GetDataReaderToStringList(string SQL)
        {
            string[] al = null;
            using (SqlConnection conn = new SqlConnection(ConnStr1))
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

        public static string[] GetDataReaderToStringList(string SQL, String ConnectionString)
        {

            string[] al = null;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
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


        public static int ExecuteQueryNew(string SQL)
        {
            int sResult;
            using (SqlConnection conn = new SqlConnection(ConnStr1))
            using (SqlCommand cmd = new SqlCommand(SQL, conn))
            {
                try
                {
                    conn.Open();
                    sResult = cmd.ExecuteNonQuery();
                }
                catch
                {
                    sResult = -1;
                }
            }
            return sResult;
        }
        public static void ExecuteQuery(string SQL, string ConnectionString)
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }



        public static void ExecuteQuery(string SQL)
        {

            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(ConnStr1);
                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }


        public static void ExecuteQuery2(string SQL)
        {

            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(ConnStr2);
                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public static DataView CreateDataView(string SQL)
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(ConnStr1);
                SqlDataAdapter da = new SqlDataAdapter(SQL, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt.DefaultView;
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
                return null;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }
        public static DataView CreateDataView_LogXemdi(string SQL)
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(ConnStr4);
                SqlDataAdapter da = new SqlDataAdapter(SQL, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt.DefaultView;
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
                return null;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public static DataView CreateDataView_LogSmsVisitorOnbox(string SQL)
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(ConnStr4);
                SqlDataAdapter da = new SqlDataAdapter(SQL, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt.DefaultView;
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
                return null;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }
        public static DataView CreateDataViewOnGet(string SQL)
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(ConnStr2);
                SqlDataAdapter da = new SqlDataAdapter(SQL, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt.DefaultView;
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
                return null;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }
        public static DataView CreateDataView_SMSGATELOG(string SQL)
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(ConnStr5);
                SqlDataAdapter da = new SqlDataAdapter(SQL, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt.DefaultView;
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
                return null;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public static DataView CreateDataView3(string SQL)
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(ConnStr3);
                SqlDataAdapter da = new SqlDataAdapter(SQL, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt.DefaultView;
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
                return null;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }


        public static DataView CreateDataView(string ConnStr, string SQL)
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(ConnStr);
                SqlDataAdapter da = new SqlDataAdapter(SQL, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt.DefaultView;
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
                return null;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public static DataView CreateDataView(SqlConnection conn, string SQL)
        {
            SqlDataAdapter da = new SqlDataAdapter(SQL, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt.DefaultView;
        }



        public static ArrayList GetDataReaderToArrayList(string SQL)
        {
            SqlConnection conn = null;
            SqlDataReader reader = null;
            ArrayList al = new ArrayList();

            try
            {
                conn = new SqlConnection(ConnStr1);
                conn.Open();

                SqlCommand cmd = new SqlCommand(SQL, conn);
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string[] fields = new string[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; ++i)
                            fields[i] = reader[i].ToString();
                        al.Add(fields);
                    }
                }
            }
            catch (Exception e)
            {
                VatLid.DAL.ExceptionProcess(e);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
                if (reader != null)
                    reader.Close();
            }
            return al;
        }



        public static int GetDataReaderToArrayList100(string SQL)
        {
            int sResult;
            using (SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["ConnStr2"]))
            using (SqlCommand cmd = new SqlCommand(SQL, conn))
            {
                try
                {
                    conn.Open();
                    sResult = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    ExceptionProcess(ex);
                    sResult = -1;
                }
            }
            return sResult;
        }





        public static ArrayList GetDataReaderToOneWayArrayList(string SQL)
        {
            SqlConnection conn = null;
            SqlDataReader reader = null;
            ArrayList al = new ArrayList();

            try
            {
                conn = new SqlConnection(ConnStr1);
                conn.Open();

                SqlCommand cmd = new SqlCommand(SQL, conn);
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        al.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception e)
            {
                VatLid.DAL.ExceptionProcess(e);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
                if (reader != null)
                    reader.Close();
            }
            return al;
        }
        public static ArrayList GetDataReaderToOneWayArrayList(string SQL, string connectionStr)
        {
            SqlConnection conn = null;
            SqlDataReader reader = null;
            ArrayList al = new ArrayList();

            try
            {
                conn = new SqlConnection(connectionStr);
                conn.Open();

                SqlCommand cmd = new SqlCommand(SQL, conn);
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        al.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception e)
            {
                VatLid.DAL.ExceptionProcess(e);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
                if (reader != null)
                    reader.Close();
            }
            return al;
        }

        public static void BindDataGrid(System.Web.UI.WebControls.DataGrid dgr, string SQL, string sType)
        {
            string ConnectStr = ConfigurationSettings.AppSettings[sType];
            DataTable table = CreateTableInfo(sType);
            using (SqlConnection conn = new SqlConnection(ConnStr1))
            using (SqlCommand cmd = new SqlCommand(SQL, conn))
            {
                try
                {
                    conn.Open();
                    using (SqlDataAdapter reader = new SqlDataAdapter(cmd))
                    {
                        reader.Fill(table);
                        if (table != null)
                        {
                            dgr.DataSource = table;
                            dgr.DataBind();
                            table.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {
                    VatLid.DAL.ExceptionProcess(ex);
                }
            }

        }


        public static string BindDataGridString(System.Web.UI.WebControls.DataGrid dgr, string SQL, string sType)
        {
            string count = "";
            string ConnectStr = ConfigurationSettings.AppSettings[sType];
            DataTable table = CreateTableInfo(sType);
            using (SqlConnection conn = new SqlConnection(ConnStr1))
            using (SqlCommand cmd = new SqlCommand(SQL, conn))
            {
                try
                {
                    conn.Open();
                    using (SqlDataAdapter reader = new SqlDataAdapter(cmd))
                    {
                        reader.Fill(table);
                        if (table != null)
                        {
                            dgr.DataSource = table;
                            dgr.DataBind();
                            table.Dispose();
                            count = table.Rows.Count + "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    VatLid.DAL.ExceptionProcess(ex);
                }
            }

            return count;

        }

        public static DataTable CreateTableInfo(string sType)
        {
            DataTable tb = new DataTable(sType);
            DataColumn ID = new DataColumn("ID", System.Type.GetType("System.Int32"));
            ID.AllowDBNull = true;
            ID.AutoIncrement = true;
            ID.AutoIncrementSeed = 1;
            tb.Columns.Add(ID);
            switch (sType)
            {
                case "viw_Report":
                    DataColumn NameCode = new DataColumn("NameCode", System.Type.GetType("System.String"));
                    tb.Columns.Add(NameCode);
                    DataColumn Service = new DataColumn("Service", System.Type.GetType("System.String"));
                    tb.Columns.Add(Service);
                    DataColumn User_Recv = new DataColumn("User_Recv", System.Type.GetType("System.String"));
                    tb.Columns.Add(User_Recv);
                    DataColumn SMS_Recv = new DataColumn("SMS_Recv", System.Type.GetType("System.String"));
                    tb.Columns.Add(SMS_Recv);
                    DataColumn SMS_Sent = new DataColumn("SMS_Sent", System.Type.GetType("System.String"));
                    tb.Columns.Add(SMS_Sent);
                    DataColumn SMS_Error = new DataColumn("SMS_Error", System.Type.GetType("System.String"));
                    tb.Columns.Add(SMS_Error);
                    DataColumn SMS_Repair = new DataColumn("SMS_Repair", System.Type.GetType("System.String"));
                    tb.Columns.Add(SMS_Repair);
                    DataColumn SMS_Empty = new DataColumn("SMS_Empty", System.Type.GetType("System.String"));
                    tb.Columns.Add(SMS_Empty);
                    DataColumn SMS_Valid = new DataColumn("SMS_Valid", System.Type.GetType("System.String"));
                    tb.Columns.Add(SMS_Valid);
                    DataColumn Money = new DataColumn("Money", System.Type.GetType("System.String"));
                    tb.Columns.Add(Money);
                    DataColumn MoneyVAT = new DataColumn("MoneyVAT", System.Type.GetType("System.String"));
                    tb.Columns.Add(MoneyVAT);
                    DataColumn ARPU = new DataColumn("ARPU", System.Type.GetType("System.String"));
                    tb.Columns.Add(ARPU);
                    break;
                case "MEMBER":
                    DataColumn memberName = new DataColumn("memberName", System.Type.GetType("System.String"));
                    tb.Columns.Add(memberName);
                    DataColumn memberNumber = new DataColumn("memberNumber", System.Type.GetType("System.String"));
                    tb.Columns.Add(memberNumber);
                    DataColumn memberDetails = new DataColumn("memberDetails", System.Type.GetType("System.String"));
                    tb.Columns.Add(memberDetails);
                    break;
                default://"CALLER"
                    DataColumn callerName = new DataColumn("callerName", System.Type.GetType("System.String"));
                    tb.Columns.Add(callerName);
                    DataColumn callerNumber = new DataColumn("callerNumber", System.Type.GetType("System.String"));
                    tb.Columns.Add(callerNumber);
                    DataColumn Description = new DataColumn("description", System.Type.GetType("System.String"));
                    tb.Columns.Add(Description);
                    break;
            }
            DataColumn[] keys = new DataColumn[1];
            keys[0] = ID;
            tb.PrimaryKey = keys;
            return tb;
        }

        public static void BindDataGrid(System.Web.UI.WebControls.DataGrid dgr, string SQL)
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(ConnStr1);
                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL, conn);
                dgr.DataSource = cmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult);
                dgr.DataBind();
            }
            catch (Exception ex)
            {
                VatLid.DAL.ExceptionProcess(ex);

            }
        }
        public static void FetchDataGridColumn(System.Web.UI.WebControls.DataGrid dg, string FieldName)
        {
            BoundColumn bc = new BoundColumn();
            bc.DataField = FieldName;
            dg.Columns.Add(bc);
        }

        public static void FetchDataGridColumn(System.Web.UI.WebControls.DataGrid dg, string HeaderText, string FieldName)
        {
            BoundColumn bc = new BoundColumn();
            bc.HeaderText = HeaderText;
            bc.DataField = FieldName;
            dg.Columns.Add(bc);
        }
        public static void FetchDataGridColumn(System.Web.UI.WebControls.DataGrid dg, string HeaderText, string FieldName, VerticalAlign va, HorizontalAlign ha)
        {
            BoundColumn bc = new BoundColumn();
            bc.HeaderText = HeaderText;
            bc.ItemStyle.VerticalAlign = va;
            bc.ItemStyle.HorizontalAlign = ha;
            bc.DataField = FieldName;
            dg.Columns.Add(bc);
        }

        public static void FetchDataGridColumn(System.Web.UI.WebControls.DataGrid dg, string HeaderText, string NavigateField, string NavigateURLString, string Field, int Type)
        {
            if (Type == 1)
            {
                //Add HyperLinkColumn
                HyperLinkColumn hlc = new HyperLinkColumn();
                hlc.HeaderText = HeaderText;
                hlc.DataNavigateUrlField = NavigateField;
                hlc.DataNavigateUrlFormatString = NavigateURLString;
                hlc.DataTextField = Field;
                dg.Columns.Add(hlc);
            }
            else
            {
                //Add HyperLinkColumn
                HyperLinkColumn hlc = new HyperLinkColumn();
                hlc.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                hlc.HeaderText = HeaderText;
                hlc.DataNavigateUrlField = NavigateField;
                hlc.DataNavigateUrlFormatString = NavigateURLString;
                hlc.Text = Field;
                hlc.ItemStyle.VerticalAlign = VerticalAlign.Middle;
                hlc.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                dg.Columns.Add(hlc);
            }
        }
        public static void FetchDataGridColumn1(System.Web.UI.WebControls.DataGrid dg, string HeaderText, string NavigateField, string NavigateURLString, string Field, int Type)
        {
            if (Type == 1)
            {
                //Add HyperLinkColumn
                HyperLinkColumn hlc = new HyperLinkColumn();
                hlc.HeaderText = HeaderText;
                hlc.DataNavigateUrlField = NavigateField;
                hlc.DataNavigateUrlFormatString = NavigateURLString;
                hlc.DataTextField = Field;
                dg.Columns.Add(hlc);
            }
            else
            {
                //Add HyperLinkColumn
                HyperLinkColumn hlc = new HyperLinkColumn();
                hlc.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                hlc.HeaderText = HeaderText;
                hlc.DataNavigateUrlField = NavigateField;
                hlc.DataNavigateUrlFormatString = NavigateURLString;
                hlc.Text = Field;
                hlc.ItemStyle.VerticalAlign = VerticalAlign.Middle;
                hlc.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                hlc.Target = "_blank";
                dg.Columns.Add(hlc);
            }
        }
        public static void FillDataToDropdownList(DropDownList ddlList, string SQL, string TableName, string FieldList)
        {
            //string SQL = "SELECT " + FieldList + " FROM " + TableName;
            string[] arrField;
            arrField = FieldList.Split(',');
            DataSet ds = new DataSet();
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(ConnStr1);
                SqlDataAdapter da = new SqlDataAdapter(SQL, conn);
                da.Fill(ds, TableName);
                ddlList.DataSource = ds;
                ddlList.DataMember = TableName;
                ddlList.DataValueField = arrField[0];// Field1;
                ddlList.DataTextField = arrField[1];// Field2;			
                ddlList.DataBind();
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public static void FillDataToDropdownList(string listValues, DropDownList ddlList)
        {
            foreach (string value in listValues.Split(';'))
            {
                string s = value.Replace("{", "");
                s = s.Replace("}", "");
                ddlList.Items.Add(new ListItem(s.Split(',')[0], s.Split(',')[1]));
            }
        }

        public static void FillDataToDropdownList(DropDownList ddlList, string TableName, string FieldList)
        {
            string SQL = "SELECT " + FieldList + " FROM " + TableName;
            string[] arrField;
            arrField = FieldList.Split(',');
            DataSet ds = new DataSet();
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(ConnStr1);
                SqlDataAdapter da = new SqlDataAdapter(SQL, conn);
                da.Fill(ds, TableName);
                ddlList.DataSource = ds;
                ddlList.DataMember = TableName;
                ddlList.DataValueField = arrField[0];// Field1;
                ddlList.DataTextField = arrField[1];// Field2;			
                ddlList.DataBind();
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }
        public static void FillDataToDropdownListStatus(DropDownList ddlList, string TableName, string FieldList)
        {
            string SQL = "SELECT " + FieldList + " FROM " + TableName + " Where status = 2";
            string[] arrField;
            arrField = FieldList.Split(',');
            DataSet ds = new DataSet();
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(ConnStr1);
                SqlDataAdapter da = new SqlDataAdapter(SQL, conn);
                da.Fill(ds, TableName);
                ddlList.DataSource = ds;
                ddlList.DataMember = TableName;
                ddlList.DataValueField = arrField[0];// Field1;
                ddlList.DataTextField = arrField[1];// Field2;			
                ddlList.DataBind();
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public static void FillDataToDropdownListStatusCP(DropDownList ddlList, string TableName, string FieldList)
        {
            string SQL = "SELECT " + FieldList + " FROM " + TableName + " Where status = 2";
            string[] arrField;
            arrField = FieldList.Split(',');
            DataSet ds = new DataSet();
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(ConnStr1);
                SqlDataAdapter da = new SqlDataAdapter(SQL, ConnOnphim);
                da.Fill(ds, TableName);
                ddlList.DataSource = ds;
                ddlList.DataMember = TableName;
                ddlList.DataValueField = arrField[0];// Field1;
                ddlList.DataTextField = arrField[1];// Field2;			
                ddlList.DataBind();
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public static void FillDataToListBox(System.Web.UI.WebControls.ListBox lb, string TableName, string FieldList)
        {
            string SQL = "SELECT " + FieldList + " FROM " + TableName;
            string[] arrField;
            arrField = FieldList.Split(',');
            DataSet ds = new DataSet();
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(ConnStr1);
                SqlDataAdapter da = new SqlDataAdapter(SQL, conn);
                da.Fill(ds, TableName);
                lb.DataSource = ds;
                lb.DataMember = TableName;
                lb.DataValueField = arrField[0];// Field1;
                lb.DataTextField = arrField[1];// Field2;			
                lb.DataBind();
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public static void FillDataToDropdownList2(DropDownList ddlList, string TableName, string FieldList)
        {
            string SQL = "SELECT " + FieldList + " FROM " + TableName;
            string[] arrField;
            arrField = FieldList.Split(',');
            DataSet ds = new DataSet();
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(ConnStr2);
                SqlDataAdapter da = new SqlDataAdapter(SQL, conn);
                da.Fill(ds, TableName);
                ddlList.DataSource = ds;
                ddlList.DataMember = TableName;
                ddlList.DataValueField = arrField[0];// Field1;
                ddlList.DataTextField = arrField[1];// Field2;			
                ddlList.DataBind();
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }


        public static void FillDataToDropdownList(string ConnectionString, string SQL, DropDownList ddlList, string FieldList)
        {
            string[] arrField;
            arrField = FieldList.Split(',');
            DataSet ds = new DataSet();
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(ConnectionString);
                SqlDataAdapter da = new SqlDataAdapter(SQL, conn);
                da.Fill(ds, "a");
                ddlList.DataSource = ds;
                ddlList.DataMember = "a";
                ddlList.DataValueField = arrField[0];// Field1;
                ddlList.DataTextField = arrField[1];// Field2;			
                ddlList.DataBind();
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public static DataSet GetDataSet(string store, SqlCommand cmd)
        {
            return GetDataSet(store, ConnStr1, cmd);
        }

        public static void FillDataToDropdownList_OnRadio(DropDownList ddlList, string TableName, string FieldList, string where)
        {
            string[] arrField;
            arrField = FieldList.Split(',');
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@table", TableName);
            cmd.Parameters.AddWithValue("@id", arrField[0]);
            cmd.Parameters.AddWithValue("@name", arrField[1]);
            cmd.Parameters.AddWithValue("@where", where);
            DataSet ds = new DataSet();
            try
            {
                ds = VatLid.DAL.GetDataSet("[CMS_FillDataToDropdownList_v2]", cmd);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlList.DataSource = ds.Tables[0];
                    ddlList.DataMember = TableName;
                    ddlList.DataValueField = arrField[0];// Field1;
                    ddlList.DataTextField = arrField[1];// Field2;			
                    ddlList.DataBind();
                }
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
            }
        }

        public static void FillDataToDropdownListStore(DropDownList ddlList, string Store, string FieldList, string where)
        {
            string[] arrField;
            arrField = FieldList.Split(',');
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@where", where);
            DataSet ds = new DataSet();
            try
            {
                ds = VatLid.DAL.GetDataSet(Store, cmd);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlList.DataSource = ds.Tables[0];
                    ddlList.DataMember = Store;
                    ddlList.DataValueField = arrField[0];// Field1;
                    ddlList.DataTextField = arrField[1];// Field2;			
                    ddlList.DataBind();
                }
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
            }
        }
        public static void FillDataToDropdownList(string ConnectionString, DropDownList ddlList, string TableName, string FieldList)
        {
            string SQL = "SELECT " + FieldList + " FROM " + TableName;
            string[] arrField;
            arrField = FieldList.Split(',');
            DataSet ds = new DataSet();
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(ConnectionString);
                SqlDataAdapter da = new SqlDataAdapter(SQL, conn);
                da.Fill(ds, TableName);
                ddlList.DataSource = ds;
                ddlList.DataMember = TableName;
                ddlList.DataValueField = arrField[0];// Field1;
                ddlList.DataTextField = arrField[1];// Field2;			
                ddlList.DataBind();
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public static void FetchDataGridColumnFormat(System.Web.UI.WebControls.DataGrid dg, string HeaderText, string DataTextField, string DataTextFormatString)
        {
            HyperLinkColumn hlc = new HyperLinkColumn();

            hlc.HeaderText = HeaderText;
            hlc.DataTextField = DataTextField;
            hlc.DataTextFormatString = DataTextFormatString;
            dg.Columns.Add(hlc);

        }


        public static void FillDataToDropdownListReport(DropDownList ddlList, string TableName, string SQL, string FieldList, string sType)
        {
            string ConnectStr = ConfigurationSettings.AppSettings[sType];
            string[] arrField;
            arrField = FieldList.Split(',');
            DataSet ds = new DataSet();
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(ConnStr1);
                SqlDataAdapter da = new SqlDataAdapter(SQL, conn);
                da.Fill(ds, TableName);
                ddlList.DataSource = ds;
                ddlList.DataMember = TableName;
                ddlList.DataValueField = arrField[0];// Field1;
                ddlList.DataTextField = arrField[1];// Field2;			
                ddlList.DataBind();
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public static void ReGenerateRightsFromUserGroupsAndSysFuncs()
        {
            try
            {
                string SQL;
                SQL = "DELETE FROM Rights";
                VatLid.DAL.ExecuteQuery(SQL);
                ArrayList alUserGroups = GetDataReaderToArrayList("Select ID from UserGroups");
                string[][] arrUserGroups = (string[][])alUserGroups.ToArray(typeof(string[]));
                int iUserGroupsCount = alUserGroups.Count;
                ArrayList alSysFuncs = GetDataReaderToArrayList("Select ID from SysFuncs");
                string[][] arrSysFuncs = (string[][])alSysFuncs.ToArray(typeof(string[]));
                int iSysFuncsCount = alSysFuncs.Count;
                for (int i = 0; i < iUserGroupsCount; i++)
                {
                    for (int j = 0; j < iSysFuncsCount; j++)
                    {
                        SQL = "INSERT INTO Rights(UserGroupID,FuncID,RightsType) VALUES(" + arrUserGroups[i][0] + "," + arrSysFuncs[j][0] + ",'" + "group" + "'" + ")";
                        ExecuteQuery(SQL);
                    }

                }
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
            }

        }
        public static bool GetRights(string sUserName, string FuncFile)
        {
            //string SQL = "SELECT FuncFile FROM viwUsersRights WHERE RightsStatus=1 AND UserName='" + sUserName + "' AND FuncFile='" + FuncFile + "'";
            try
            {
                int nhomdoitacCode = 12;

                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@username", sUserName);
                cmd.Parameters.AddWithValue("@funcfile", FuncFile);
                cmd.Parameters.AddWithValue("@UserGroupID", nhomdoitacCode);

                DataSet ds = VatLidOnPhim.DAL.GetDataSet("getRightsStatus", VatLid.DAL.ConnStr1, cmd);
                //ArrayList al=GetDataReaderToArrayList(SQL);
                if (ds.Tables[0].Rows[0][0].ToString() != "0")
                    return true;
                else
                    if (sUserName.ToUpper() == "ADMIN")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
                return false;
            }
        }
        public static bool GetRights(string sUserName, string FuncFile, int UserGroupID)
        {
            //string SQL = "SELECT FuncFile FROM viwUsersRights WHERE RightsStatus=1 AND UserName='" + sUserName + "' AND FuncFile='" + FuncFile + "'";
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@username", sUserName);
                cmd.Parameters.AddWithValue("@funcfile", FuncFile);
                cmd.Parameters.AddWithValue("@UserGroupID", UserGroupID);

                DataSet ds = VatLidOnPhim.DAL.GetDataSet("getRightsStatus", VatLid.DAL.ConnStr1, cmd);
                //ArrayList al=GetDataReaderToArrayList(SQL);
                if (ds.Tables[0].Rows[0][0].ToString() != "0")
                    return true;
                else
                    if (sUserName.ToUpper() == "ADMIN")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
                return false;
            }
        }

        public static string SafeString(string strRaw)
        {
            strRaw = strRaw.Replace("'", "''");
            return strRaw;
        }
        public static int Instr(string strTemplate, string strFind)
        {
            if (strTemplate.Length < strFind.Length)
                return -1;
            else
            {
                for (int i = 0; i < strTemplate.Length - strFind.Length + 1; i++)
                    if (strTemplate.Substring(i, strFind.Length) == strFind)
                        return i;
            }
            return -1;

        }
        public static bool ValidateGoodString(string strRaw)
        {
            string[] Bad_String = { "'", "<", ">", "--", "select", "insert", "update", "delete", "drop" };
            for (int i = 0; i < Bad_String.Length; i++)
            {
                if (Instr(strRaw, Bad_String[i]) >= 0)
                    return false;
            }
            return true;
        }

        public static string ConvertArrayToString(string[] strArr)
        {
            string temp = "";
            for (int i = 0; i < strArr.Length; i++)
            {
                temp += strArr[i] + "|";
            }
            return temp.Substring(0, temp.Length - 1);
        }
        public static string ConvertDate(string sDate)
        {
            DateTime dtDate = new DateTime();
            dtDate = Convert.ToDateTime(sDate);
            return (dtDate).Hour.ToString() + ":" + (dtDate).Minute.ToString() + " :: " + (dtDate).Day.ToString() + "/" + (dtDate).Month.ToString() + "/" + (dtDate).Year.ToString();
        }
        public static string ConvertDate1(string sDate)
        {
            return sDate.Substring(11, 8) + "  " + sDate.Substring(3, 2) + "/" + sDate.Substring(0, 2) + "/" + sDate.Substring(7, 4);
        }
        public static string ConvertDateNumber(string sDate)
        {
            //18:14' 07/04/2006 (GMT+7) 2006 04 05 15 1946


            return sDate.Substring(8, 2) + ":" + sDate.Substring(10, 2) + "' " + sDate.Substring(6, 2) + "/" + sDate.Substring(4, 2) + "/" + sDate.Substring(0, 4) + "  (GMT+7)";
        }
        public static string ConvertDateAdd48(string sDate)
        {
            //18:14' 07/04/2006 (GMT+7) 2006 04 05 15 1946
            //nam

            return sDate.Substring(8, 2) + ":" + sDate.Substring(10, 2) + "' " + sDate.Substring(6, 2) + "/" + sDate.Substring(4, 2) + "/" + sDate.Substring(0, 4) + "  (GMT+7)";
        }
        public static void ChangeDataGridColumn(System.Web.UI.WebControls.DataGrid dgr, int iColumn, string sOldContent, string sNewContent)
        {
            int i;
            for (i = 0; i < dgr.Items.Count; i++)
            {

                VatLid.MessageBox.Show(dgr.Items[i].Cells[iColumn].Text);
                dgr.Items[i].Cells[iColumn].Text = dgr.Items[i].Cells[iColumn].Text.Replace(sOldContent, sNewContent);

            }
        }
        //0: logo mang
        //1: tin nhan hinh
        public static string ConvertOTAToHexString(string sFileName, int type)
        {
            FileStream fPic = new FileStream(sFileName, FileMode.Open);
            byte[] fByte = new byte[fPic.Length];

            fPic.Read(fByte, 0, Convert.ToInt32(fPic.Length));
            string hexString = "";
            byte hdr;
            hdr = 48;
            hexString += hdr.ToString("X2");
            hdr = 0;
            hexString += hdr.ToString("X2");
            hdr = 0;
            hexString += hdr.ToString("X2");
            hdr = 0;
            //			hexString+=hdr.ToString("X2");
            //			hdr=115;
            hexString += hdr.ToString("X2");
            hdr = 2;
            hexString += hdr.ToString("X2");
            hdr = 1;
            hexString += hdr.ToString("X2");
            hdr = 0;
            hexString += hdr.ToString("X2");
            for (int i = 0; i < fByte.Length; i++)
            {
                hexString += fByte[i].ToString("X2");
            }
            fPic.Close();
            fPic.Dispose();
            if (type == 0) return hexString.Trim().Substring(14);
            else return hexString;
        }

        public static void CreateOTAFromImageLogo(string sImageFileName, string sOTAFileName)
        {
            bool[,] ota = new bool[72, 14];

            PictureBox pbMain = new PictureBox();
            pbMain.Image = System.Drawing.Image.FromFile(sImageFileName);
            Bitmap bmp = (Bitmap)pbMain.Image;
            for (int y = 0; y < 14; y++)
                for (int x = 0; x < 72; x++)
                {
                    if (bmp.GetPixel(x, y) == Color.FromArgb(0, 0, 0))
                        ota[x, y] = false; // 0
                    else
                        ota[x, y] = true; // 1
                }

            if (File.Exists(sOTAFileName))
                File.Delete(sOTAFileName);
            FileStream fs = File.Open(sOTAFileName, FileMode.CreateNew);

            fs.WriteByte(0);
            fs.WriteByte(72);
            fs.WriteByte(14);
            fs.WriteByte(1);
            byte b = new byte();
            for (int y = 0; y < 14; y++)
                for (int c = 0; c < 9; c++)
                {
                    b = 0;
                    for (int x = 0; x < 8; x++)
                    {
                        if (ota[c * 8 + x, y] == false)
                            b += (byte)Math.Pow(2, 7 - x);
                    }
                    fs.WriteByte(b);
                }
            fs.Close();
            fs.Dispose();
        }

        public static bool IsAllowFileType(string ext)
        {
            switch (ext.ToLower())
            {
                case ".gif":
                    return true;
                case ".jpg":
                    return true;
                case ".png":
                    return true;
                case ".mid":
                    return true;
                case ".mp3":
                    return true;
                default:
                    return false;
            }
        }

        public static string ContentTypeFromExt(string ext)
        {
            switch (ext.ToLower())
            {
                case ".gif":
                    return "image/gif";
                case ".jpg":
                    return "image/pjpeg";
                case ".png":
                    return "image/png";
                case ".mid":
                    return "audio/mid";
                default:
                    return "image/pjpeg";
            }

        }
        public static void CreateOTAFromImage(string sImageFileName, string sOTAFileName)
        {
            try
            {

                bool[,] ota = new bool[72, 28];

                PictureBox pbMain = new PictureBox();
                pbMain.Image = System.Drawing.Image.FromFile(sImageFileName);
                //pbMain.Image=System.Drawing.Image.FromFile("C:\\5.gif");
                Bitmap bmp = (Bitmap)pbMain.Image;
                for (int y = 0; y < 28; y++)
                    for (int x = 0; x < 72; x++)
                    {
                        if (bmp.GetPixel(x, y) == Color.FromArgb(0, 0, 0))
                            ota[x, y] = false; // 0
                        else
                            ota[x, y] = true; // 1
                    }

                if (File.Exists(sOTAFileName))
                    File.Delete(sOTAFileName);
                FileStream fs = File.Open(sOTAFileName, FileMode.CreateNew);
                fs.WriteByte(0);
                fs.WriteByte(72);
                fs.WriteByte(28);
                fs.WriteByte(1);
                byte b = new byte();
                for (int y = 0; y < 28; y++)
                    for (int c = 0; c < 9; c++)
                    {
                        b = 0;
                        for (int x = 0; x < 8; x++)
                        {
                            if (ota[c * 8 + x, y] == false)
                                b += (byte)Math.Pow(2, 7 - x);
                        }
                        fs.WriteByte(b);
                    }
                fs.Close();
                fs.Dispose();
            }
            catch (Exception e)
            {
                string a = e.Message;
            }
        }
        public static bool isDeleted(string sUserName, string sFileName)
        {
            try
            {

                if (sUserName.ToLower() == "admin")
                    return true;

                string SQL = "SELECT isDeleted FROM viwFunctionAction WHERE Username='" + sUserName + "' And Funcfile='" + sFileName + "' And isDeleted=1";
                ArrayList al = GetDataReaderToArrayList(SQL);

                if (al.Count != 0)
                    return true;
                else
                    return false;

            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static bool isEdited(string sUserName, string sFileName)
        {
            try
            {

                if (sUserName.ToLower() == "admin")
                    return true;

                string SQL = "SELECT isEdited FROM viwFunctionAction WHERE Username='" + sUserName + "' And Funcfile='" + sFileName + "' And isEdited=1";
                ArrayList al = GetDataReaderToArrayList(SQL);
                if (al.Count != 0)
                    return true;
                else
                    return false;

            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static bool isUpdated(string sUserName, string sFileName)
        {
            try
            {
                if (sUserName.ToLower() == "admin")
                    return true;

                string SQL = "SELECT isUpdated FROM viwFunctionAction WHERE Username='" + sUserName + "' And Funcfile='" + sFileName + "' And isUpdated=1";
                ArrayList al = GetDataReaderToArrayList(SQL);

                if (al.Count != 0)
                    return true;
                else
                    return false;


            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static bool isAdded(string sUserName, string sFileName)
        {
            try
            {
                if (sUserName.ToLower() == "admin")
                    return true;

                string SQL = "SELECT isAdded FROM viwFunctionAction WHERE Username='" + sUserName + "' And Funcfile='" + sFileName + "' And isAdded=1";
                ArrayList al = GetDataReaderToArrayList(SQL);

                if (al.Count != 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {

                return false;
            }
        }

        public static bool isViewed(string sUserName, string sFileName)
        {
            try
            {
                if (sUserName.ToLower() == "admin")
                    return true;

                string SQL = "SELECT isViewed FROM viwFunctionAction WHERE Username='" + sUserName + "' And Funcfile='" + sFileName + "' And isViewed=1";
                ArrayList al = GetDataReaderToArrayList(SQL);

                if (al.Count != 0)
                    return true;
                else
                    return false;


            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static bool isViewedAll(string sUserName, string sFileName)
        {
            try
            {
                if (sUserName.ToLower() == "admin")
                    return true;

                string SQL = "SELECT isAllViewed FROM viwFunctionAction WHERE Username='" + sUserName + "' And Funcfile='" + sFileName + "' And isAllViewed=1";
                ArrayList al = GetDataReaderToArrayList(SQL);

                if (al.Count != 0)
                    return true;
                else
                    return false;


            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static void UpdateHits(string sTableName, string sFieldName, string sID)
        {
            string SQL = "";
            string sCategoryHits = "";
            SQL = "SELECT " + sFieldName + " FROM " + sTableName + " WHERE ID=" + sID;
            try
            {
                ArrayList al = VatLid.DAL.GetDataReaderToArrayList(SQL);
                string[][] arrReturn = (string[][])al.ToArray(typeof(string[]));
                sCategoryHits = arrReturn[0][0];
                int iCategoryHits = Convert.ToInt32(sCategoryHits) + 1;
                SQL = "UPDATE " + sTableName + " SET " + sFieldName + " = " + iCategoryHits.ToString() + " WHERE ID=" + sID;
                VatLid.DAL.ExecuteQuery(SQL);
            }
            catch (Exception e)
            {
                VatLid.DAL.ExceptionProcess(e);
            }
        }

        public static bool isUpdatedRight(string sID)
        {
            try
            {
                //11/24/2006 3:31:22 AM
                string SQL = "SELECT ARRIVE_TIME FROM Cho_MuaBan WHERE ID=" + sID;
                ArrayList al = VatLid.DAL.GetDataReaderToArrayList(SQL);
                string[][] arrReturn = (string[][])al.ToArray(typeof(string[]));
                string sDate = arrReturn[0][0].ToString();
                string sNam = sDate.Substring(6, 4);
                string sThang = sDate.Substring(0, 2);
                string sNgay = sDate.Substring(3, 2);
                //string sgio=sDate.Substring(10,2);
                int intDateEdit = Convert.ToInt32(sNam + sThang + sNgay);
                //Convert.ToInt32(sDate.Substring(6,4)+ sDate.Substring(3,2) + (Convert.ToInt32(sDate.Substring(0,2)) + 2).ToString() + sDate.Substring(12,2));
                int intnow = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));

                if (intDateEdit >= intnow)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
                return false;
            }
        }
        public static string GenerateImageHTML(string Html)
        {
            string imdDBLocal = ConfigurationSettings.AppSettings["BackHome"];
            Html = Html.Replace(imdDBLocal, "");
            return Html;
        }
        public static string GenerateImage(string Html)
        {
            string imgPath = "";
            string imgSrc = "";


            MatchCollection match;
            Regex reg = new Regex("<img ((?:.|\n)*?) />");
            match = reg.Matches(Html);

            for (int i = 0; i < match.Count; i++)
            {

                //int height;
                int width;

                string path = match[i].Groups[1].Value;
                imgSrc = path;
                reg = new Regex("src=\"((?:.|\n)*?)\"");
                MatchCollection imgMatch = reg.Matches(imgSrc);
                imgPath = imgMatch[0].Groups[1].Value;


                Html = imgPath;

                string imdDBLocal = ConfigurationSettings.AppSettings["BackHome"];
                Html = Html.Replace(imdDBLocal, "");
                return Html;

            }

            return Html;
        }
        //Function to update all status in table
        public static void UpdateStatus(string sTableName, string sFieldName, int intvalue, string sID)
        {
            string SQL = "";
            try
            {
                SQL = "UPDATE " + sTableName + " SET " + sFieldName + " = " + intvalue + " WHERE ID in (" + sID + ")";
                VatLid.DAL.ExecuteQuery(SQL);

                //Dong bo db99 to db105
                string strDSTableSYNC = ",ItemBoi,ItemBook,ItemBookStore,Item3GP,ItemGames,ItemCard,ItemKara,MusicStoreCate,ItemVideo,ItemImage,ItemRadio,ItemStory,ItemComic,ItemComicPocket,MusicRight,ItemPlash,tbRBTs,SubMusicCRBTItem,ItemCau,ItemCau2,ItemCauCate,NCHotNgay,VideoChuDeGoiCate,ItemStoryPocket,ItemRadioGoiCate,SubMusicCRBTGoiCate,";
                if (strDSTableSYNC.IndexOf("," + sTableName + ",") >= 0)
                {
                    string updateSQL = "UPDATE " + sTableName + " SET IsSentSYNC=2 WHERE ID in (" + sID + ")";
                    VatLid.DAL.ExecuteQuery(updateSQL);
                }
            }
            catch (Exception e)
            {
                VatLid.DAL.ExceptionProcess(e);
            }
        }

        //Function to update all status in table
        public static void UpdateStatusSync(string sTableName, string sFieldName, int intvalue, string sID)
        {
            string SQL = "";
            try
            {
                SQL = "UPDATE " + sTableName + " SET " + sFieldName + " = " + intvalue + ", issent = 0 WHERE ID in (" + sID + ")";
                VatLid.DAL.ExecuteQuery(SQL);

                //Dong bo db99 to db105
                string strDSTableSYNC = ",ItemBoi,ItemBook,ItemBookStore,Item3GP,ItemGames,ItemCard,ItemKara,MusicStoreCate,ItemVideo,ItemImage,ItemRadio,ItemStory,ItemComic,ItemComicPocket,MusicRight,ItemPlash,tbRBTs,SubMusicCRBTItem,ItemCau,ItemCau2,ItemCauCate,NCHotNgay,VideoChuDeGoiCate,ItemStoryPocket,ItemRadioGoiCate,SubMusicCRBTGoiCate,";
                if (strDSTableSYNC.IndexOf("," + sTableName + ",") >= 0)
                {
                    string updateSQL = "UPDATE " + sTableName + " SET IsSentSYNC=2 WHERE ID in (" + sID + ")";
                    VatLid.DAL.ExecuteQuery(updateSQL);
                }
            }
            catch (Exception e)
            {
                VatLid.DAL.ExceptionProcess(e);
            }
        }


        public static string UpdateVoteImage(string UserID, string ItemCode)
        {
            string str1 = "0";
            string SQL = "";
            try
            {
                SQL = "UPDATE ItemImageMms set ItemVote = ItemVote + 1 WHERE ItemCode ='" + ItemCode + "'";
                VatLid.DAL.ExecuteQuery(SQL);
                str1 = "1";
            }
            catch (Exception e)
            {
                str1 = "0";
            }
            string str3 = str1;
            return str3;
        }

        public static string InsertUpdateVoteImage(string ItemCode, string UserID, int week, string date)
        {
            string str1 = "0";
            string SQL = "";
            try
            {
                SqlParameter[] parameters2 = { 
												 new SqlParameter("@ItemCode", SqlDbType.NVarChar),																																																																								
				};
                parameters2[0].Value = ItemCode;
                DataSet ds;
                ds = VatLid.SqlHelper.ExecuteDataset(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "select_IMAGE_VOTE_ByItemCode", parameters2);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    SQL = "UPDATE IMAGE_VOTE set ItemVote = ItemVote + 1 WHERE ItemCode ='" + ItemCode + "'";
                    VatLid.DAL.ExecuteQuery(SQL);

                }
                else
                {
                    SQL = "INSERT IMAGE_VOTE(ItemVote,ItemCode,UserID,Week,ItemDateView) Values(" + 1 + ",'" + ItemCode + "','" + UserID + "'," + week + ",'" + date + "')";
                    VatLid.DAL.ExecuteQuery(SQL);

                }
                str1 = "1";


            }
            catch (Exception e)
            {
                str1 = "0";
            }
            string str3 = str1;
            return str3;
        }

        public void UpdateFileName(string id, string strFileName)
        {
            SqlParameter[] parameters = 
					{ 
						
						new SqlParameter("@ImageUp", SqlDbType.NVarChar),
						new SqlParameter("@ID", SqlDbType.Int ),
			};
            parameters[0].Value = strFileName;
            parameters[1].Value = Convert.ToInt32(id);

            VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "Cho_UpdateFileName", parameters);
        }

        public static void addWatermarkText(string MarkText, int w, int h, ref Graphics picture)
        {
            int[] sizes = new int[] { 16, 14, 12, 10, 8, 6, 4 };
            Font crFont = null;
            SizeF crSize = new SizeF();
            for (int i = 0; i < 7; i++)
            {
                crFont = new Font("arial", sizes[i]);
                crSize = picture.MeasureString(MarkText, crFont);
                if ((ushort)crSize.Width < (ushort)w)
                    break;
            }

            float xpos = 0;
            float ypos = 0;

            xpos = ((float)w * (float).01) + (crSize.Width / 2);
            ypos = ((float)h * (float).99) - crSize.Height;

            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Center;


            SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(153, 0, 0, 0));
            picture.DrawString(MarkText, crFont, semiTransBrush2, xpos + 1, ypos + 1, StrFormat);

            SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));
            picture.DrawString(MarkText, crFont, semiTransBrush, xpos, ypos, StrFormat);


            semiTransBrush2.Dispose();
            semiTransBrush.Dispose();
        }

        public static void addWatermarkText(string MarkText, string ItemCode,
            int w, int h, ref Graphics picture)
        {

            MarkText = MarkText.Trim().ToUpper() + " " + ItemCode.Trim();

            int[] sizes = new int[] { 16, 14, 12, 10, 8, 6, 4 };
            Font crFont = null;
            SizeF crSize = new SizeF();
            crFont = new Font("arial", sizes[4], FontStyle.Bold);
            crSize = picture.MeasureString(MarkText, crFont);
            float ypos = 0;
            ypos = ((float)h * (float).99) - crSize.Height;
            //Font cFont = new Font("arial",8,FontStyle.Bold);
            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Center;
            picture.DrawString(MarkText, crFont, Brushes.WhiteSmoke, new RectangleF(0, ypos, w, h), StrFormat);

        }

        public static void addWatermarkNiceText(string MarkText,
            string mFont, string mColor, int w, int h, ref Graphics picture)
        {

            string[] tpFont;

            string tpFontList = "Viner Hand ITC,VNI-Briquet,VNI-HLThuphap,VNI-Slogan,VNI-Thufap2,VNI-Thufap3,VNI-Thufapfan,VNI-Trung Kien,TimeScrDBol,TupacHand,Ultra Vertex23,URWHelenSolD,URWLedaD,VivaldiD,Windsong,Viking,VladimirScrD,Walt Disney Script,WolfClan,VnKoala,"

                + ".VnAristote,.VnFree,.VnGothic,"
                + ".VnKoala,.VnLincoln,.VnLinus,.VnMystical,.VnPark,"
                + ".VnPresent,.VnShelley Allegro,.VnUniverse";


            if (mFont.StartsWith("0")) mFont = mFont.Substring(1);

            //1.Font
            if (mFont.Length < 1)
            {
                Random ran = new Random();
                mFont = ran.Next(1, 31) + "";
            }

            tpFont = tpFontList.Split(new char[] { ',' });

            //11			
            int maxF = tpFont.Length;
            if ((Convert.ToInt32(mFont)) > maxF)
            {
                mFont = maxF + "";
            }

            int[] sizes =
                new int[] { 34, 32, 30, 28, 26, 24, 22, 20, 18, 16, 14, 12, 10, 8, 6, 4 };
            Font crFont = null;
            SizeF crSize = new SizeF();

            for (int i = 0; i < 16; i++)
            {
                //font
                crFont = new Font(
                    tpFont[Convert.ToInt32(mFont) - 1],
                    sizes[i]);

                crSize = picture.MeasureString(MarkText, crFont);
                if ((ushort)crSize.Width < (ushort)w)
                    break;
            }

            float xpos = 0;
            float ypos = 0;

            xpos = (w - crSize.Width) / 2 + (crSize.Width / 2);
            ypos = (((float)h * (float)0.99) - crSize.Height);

            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Center;

            //SolidBrush semiTransBrush2=new SolidBrush(Color.FromArgb(153,0,0,0));

            SolidBrush semiTransBrush2;
            mColor = GetIntChars(mColor);

            if (mColor.StartsWith("0")) mColor = mColor.Substring(1);

            if (mColor.Length < 1)
            {
                Random ran = new Random();
                mColor = ran.Next(1, 12) + "";
            }

            switch (mColor)
            {
                case "1":
                    semiTransBrush2 = new SolidBrush(Color.Red);
                    break;

                case "2":
                    semiTransBrush2 = new SolidBrush(Color.Orange);
                    break;

                case "3":
                    semiTransBrush2 = new SolidBrush(Color.Yellow);
                    break;

                case "4":
                    semiTransBrush2 = new SolidBrush(Color.Green);
                    break;

                case "5":
                    semiTransBrush2 = new SolidBrush(Color.DeepSkyBlue);
                    break;

                case "6":
                    semiTransBrush2 = new SolidBrush(Color.Indigo);
                    break;

                case "7":
                    semiTransBrush2 = new SolidBrush(Color.Violet);
                    break;

                case "8":
                    semiTransBrush2 = new SolidBrush(Color.Pink);
                    break;

                case "9":
                    semiTransBrush2 = new SolidBrush(Color.LightSkyBlue);
                    break;

                case "10":
                    semiTransBrush2 = new SolidBrush(Color.Tomato);
                    break;

                case "11":
                    semiTransBrush2 = new SolidBrush(Color.MediumVioletRed);
                    break;

                case "12":
                    semiTransBrush2 = new SolidBrush(Color.OrangeRed);
                    break;

                default:
                    semiTransBrush2 = new SolidBrush(Color.MidnightBlue);
                    break;
            }


            /*
            //Ve hinh
            picture.DrawString(MarkText, crFont, 
                semiTransBrush2, xpos+1, ypos+1, StrFormat);

            // Bong
            SolidBrush semiTransBrush = 
                new SolidBrush(Color.FromArgb(153,255,255, 255));

            picture.DrawString(MarkText, crFont, 
                semiTransBrush, xpos, ypos, StrFormat);

            */

            //Ve bong nen 
            //SolidBrush semiTransBrush = 
            //new SolidBrush(Color.FromArgb(153,255,255, 255));

            SolidBrush semiTransBrush = new SolidBrush(Color.Black);
            picture.DrawString(MarkText, crFont,
                semiTransBrush, xpos + 1, ypos + 1, StrFormat);

            // Chu chinh
            picture.DrawString(MarkText, crFont,
                semiTransBrush2, xpos, ypos, StrFormat);

            semiTransBrush2.Dispose();
            semiTransBrush.Dispose();
        }
        public static String GetIntChars(String numStr)
        {
            String numValue = "";

            try
            {

                if (numStr != null)
                {

                    numStr = numStr.ToUpper();
                    numStr = numStr.Replace("O", "0");
                    numStr = numStr.Replace("I", "1");
                    //numStr = numStr.Replace("L","1");
                    numStr = numStr.Replace("!", "1");

                    numStr = numStr.Replace("  ", " ");
                    numStr = numStr.Replace("  ", " ");
                    numStr = numStr.Trim();

                    char[] ch = numStr.ToCharArray();
                    int n = ch.Length;

                    for (int i = 0; i < n; i++)
                    {
                        if (ch[i] >= '0' && ch[i] <= '9')
                            numValue = numValue + ch[i].ToString();

                    }

                    if (numValue == null) numValue = "";
                }
            }
            catch
            {
                numValue = numStr;

            }

            return numValue.Trim();
        }
        public static DataSet ListImageData(string sSQL, int PageIndex, int PageSize, string TableImage)
        {
            DataSet mDataSet = null;
            using (SqlConnection conn = new SqlConnection(ConnStr1))
            {
                try
                {
                    conn.Open();
                    using (SqlDataAdapter mData = new SqlDataAdapter(sSQL, conn))
                    {
                        mDataSet = new DataSet();
                        mData.Fill(mDataSet, (PageIndex - 1) * PageSize, PageSize, TableImage);
                    }
                }
                catch (Exception ex)
                {
                    VatLid.DAL.ExceptionProcess(ex);
                    mDataSet = null;
                }
            }
            return mDataSet;
        }
        public static String getParameters(String Field, String TableName, String dieukien)
        {
            string SQL = "SELECT " + Field + " FROM " + TableName + " " + dieukien;
            DataSet ds = new DataSet();
            SqlConnection conn = null;
            String mailto = "";
            try
            {
                conn = new SqlConnection(ConnStr1);
                SqlDataAdapter da = new SqlDataAdapter(SQL, conn);
                da.Fill(ds, TableName);
                DataRowCollection rows = ds.Tables[TableName].Rows;
                for (int tt = 0; tt < rows.Count; tt++)
                {
                    mailto += (String)rows[tt][Field].ToString() + ",";
                }
                mailto = mailto.TrimEnd(new char[] { ',' });
            }
            catch (Exception e)
            {
                //return e.ToString();
                VatLid.DAL.ExceptionProcess(e);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
            return mailto;
        }
        public static void FillDataToCheckBoxList(CheckBoxList checkList, string TableName, string FieldList)
        {
            string SQL = "SELECT " + FieldList + " FROM " + TableName;
            string[] arrField;
            arrField = FieldList.Split(',');
            DataSet ds = new DataSet();
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(ConnStr1);
                SqlDataAdapter da = new SqlDataAdapter(SQL, conn);
                da.Fill(ds, TableName);
                checkList.DataSource = ds;
                checkList.DataMember = TableName;
                checkList.DataValueField = arrField[0];// Field1;
                checkList.DataTextField = arrField[1];// Field2;			
                checkList.DataBind();
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }


        public static DateTime convertDateVNtoES(String sDate)
        {
            //try {
            //27/04/1985 -> datetime
            String sDay = sDate.Substring(0, 2);
            String sMonth = sDate.Substring(3, 2);
            String sYear = sDate.Substring(6, 4);
            DateTime time = new DateTime(Int32.Parse(sYear), Int32.Parse(sMonth), Int32.Parse(sDay));
            return time;
            //} catch(Exception aa) {
            //  return null;
            //}
        }

        public static string GenerateImage2Admin(string Html)
        {
            string imgPath = "";
            string imgSrc = "";
            string imdDBLocal = ConfigurationSettings.AppSettings["NewsImagesDirectory"];

            MatchCollection match;
            Regex reg = new Regex("<img ((?:.|\n)*?) />");
            match = reg.Matches(Html);

            for (int i = 0; i < match.Count; i++)
            {
                int width;

                string path = match[i].Groups[1].Value;
                imgSrc = path;
                reg = new Regex("src=\"((?:.|\n)*?)\"");
                MatchCollection imgMatch = reg.Matches(imgSrc);
                imgPath = imgMatch[0].Groups[1].Value;

                Html = imgPath;


                Html = Html.Replace("/archive/images/", imdDBLocal);
                return Html;

            }

            Html = Html.Replace("/archive/images/", imdDBLocal);
            return Html;
        }

        public static void ExecuteQuery7(string Connect, string SQL)
        {

            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(Connect);
                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                ExceptionProcess(e);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }


        public static string getCategoryID(string CategoryName)
        {
            string CategoryID = "";
            try
            {
                SqlParameter[] parameters = { 
						 new SqlParameter("@CategoryName", SqlDbType.NVarChar),																																					
						 new SqlParameter("@ID", SqlDbType.Int),																																					
					};
                parameters[0].Value = CategoryName;
                parameters[1].Value = 0;
                DataSet ds;
                ds = VatLid.SqlHelper.ExecuteDataset(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "select_CategoriesMenu_ByName", parameters);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    CategoryID = ds.Tables[0].Rows[0]["ID"].ToString();
                }
                else
                {
                    CategoryID = CategoryName;
                }

            }
            catch (Exception ex)
            {
                CategoryID = CategoryName;

            }
            return CategoryID;
        }




        public static string getNameDigital(string id)
        {
            String sql = "select ItemName from ItemVideo where ID=" + id;
            try
            {
                String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
                if (dl != null)
                {
                    return dl[0];
                }
                else
                {
                    return "Chưa xác định";
                }
            }
            catch (Exception ex)
            {
                return "Chưa xác định";
            }
        }


        public static string checkPhoneValid(string phoneNumber)
        {
            string phoneSyntax = "0123456789";

            try
            {
                if (phoneNumber == "") return "";
                string phone = "";
                for (int i = 0; i < phoneNumber.Length; i++)
                    if (phoneSyntax.IndexOf(phoneNumber[i]) != -1)
                        phone += phoneNumber[i];

                if (phone != "" && phone.Length > 8)
                {
                    if (phone.StartsWith("84"))
                    {
                        phone = phone.Substring(2);
                        if (phone.Length != 9 && phone.Length != 10) return "";

                    }
                    else
                        if (phone.StartsWith("0")) phone = phone.Substring(1);
                }
                if (phone.Length != 9 && phone.Length != 10 && phone.Length != 8) return "";

                return phoneViettel(phone);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static string checkPhoneValidAll(string phoneNumber)
        {
            string phoneSyntax = "0123456789";
            try
            {

                if (phoneNumber == "") return "";
                string phone = "";
                for (int i = 0; i < phoneNumber.Length; i++)
                    if (phoneSyntax.IndexOf(phoneNumber[i]) != -1)
                        phone += phoneNumber[i];

                if (phone != "" && phone.Length > 8)
                {
                    if (phone.StartsWith("84"))
                    {
                        phone = phone.Substring(2);
                        if (phone.Length != 9 && phone.Length != 10) return "";

                    }
                    else
                        if (phone.StartsWith("0")) phone = phone.Substring(1);
                }
                if (phone.Length != 9 && phone.Length != 10 && phone.Length != 8) return "";

                return phoneAll(phone);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private static string phoneViettel(string phone)
        {
            string wsPhoneViettel = "96,98,97,161,162,163,164,165,166,167,168,169";
            string[] tmp = wsPhoneViettel.Split(',');
            for (int i = 0; i < tmp.Length; i++)
                if (phone.StartsWith(tmp[i])) return phone;
            return "";
        }

        private static string phoneAll(string phone)
        {
            string wsPhoneViettel = "96,98,97,161,162,163,164,165,166,167,168,169,91,90,93,94,92,95,12";
            string[] tmp = wsPhoneViettel.Split(',');
            for (int i = 0; i < tmp.Length; i++)
                if (phone.StartsWith(tmp[i])) return phone;
            return "";
        }

        public static string GetDataToString(string SQL)
        {
            string sResult = "";
            using (SqlConnection conn = new SqlConnection(ConnStr1))
            using (SqlCommand cmd = new SqlCommand(SQL, conn))
            {
                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; ++i)
                                sResult = sResult + reader[i].ToString() + "|";
                            sResult = sResult.Substring(0, sResult.Length - 1) + "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    sResult = ex.Message;
                }
            }
            return sResult;
        }

        public static string GetDataToString(string SQL, string cn)
        {
            string sResult = "";
            using (SqlConnection conn = new SqlConnection(cn))
            using (SqlCommand cmd = new SqlCommand(SQL, conn))
            {
                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; ++i)
                                sResult = sResult + reader[i].ToString() + "|";
                            sResult = sResult.Substring(0, sResult.Length - 1) + "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    sResult = ex.Message;
                }
            }
            return sResult;
        }

        public static string getVideoAlbum(string id)
        {
            String sql = "select Name from VideoAlbum where ID=" + id;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            else
            {
                return "Chưa xác định";
            }
        }
        public static string getVideoCate(string id)
        {
            String sql = "select Name from VideoCate where ID=" + id;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            else
            {
                return "Chưa xác định";
            }
        }


        public static string getSingerPIC(string id)
        {
            String sql = "select LeadImage from List_Singers where ID=" + id;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            else
            {
                return "Chưa xác định";
            }
        }


        public static string getSingerNameByName(string id)
        {
            String sql = "select Name from ItemNameCate where ID=" + id;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            else
            {
                return "Chưa xác định";
            }
        }
        public static string getAlbumVideoName(string id)
        {
            String sql = "select Name from VideoAlbum where ID=" + id;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            else
            {
                return "Chưa xác định";
            }
        }
        public static string getCateVideoName(string id)
        {
            String sql = "select Name from VideoCate where ID=" + id;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            else
            {
                return "Chưa xác định";
            }
        }
        public static string getCategoryName(string id)
        {
            String sql = "select Name from MusicCate where ID=" + id;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            else
            {
                return "Chưa xác định";
            }
        }

        public static string getCategoryStoryName(string id)
        {
            String sql = "select Name from ItemStoryCate where ID=" + id;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            else
            {
                return "Chưa xác định";
            }
        }

        public static string getCategoryRadioName(string id)
        {
            String sql = "select Name from ItemRadioCate where ID=" + id;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            else
            {
                return "Chưa xác định";
            }
        }

        public static string getCategoryMusicMobile(string id)
        {
            String sql = "select Name from MusicCate where ID=" + id;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            else
            {
                return "Chưa xác định";
            }
        }

        public static string getCategoryNameTrue(string id)
        {
            String sql = "select Name from ItemNameCate where ID=" + id;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            else
            {
                return "Chưa xác định";
            }
        }

        public static string getCategoryItem3gpCate(string id)
        {
            String sql = "select Name from Item3gpCate where ID=" + id;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            else
            {
                return "Chưa xác định";
            }
        }


        public static string getSingersName(string id)
        {
            String sql = "select Name from MusicSinger where ID=" + id;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            else
            {
                return "Chưa xác định";
            }
        }

        public static bool IsValidIP(string addr)
        {
            //create our match pattern
            string pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.
                ([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";
            //create our Regular Expression object
            Regex check = new Regex(pattern);
            //boolean variable to hold the status
            bool valid = false;
            //check to make sure an ip address was provided
            if (addr == "")
            {
                //no address provided so return false
                valid = false;
            }
            else
            {
                //address provided so use the IsMatch Method
                //of the Regular Expression object
                valid = check.IsMatch(addr, 0);
            }
            //return the results
            return valid;
        }
        public static string getDownloadMessages(string ServiceID, string CommandCode, string Info, string UserID)
        {
            string result = "0";
            try
            {
                string exeSQL = "Exec Select_Download_1x88_Web '{0}','{1}','{2}','{3}';";
                string SQL = string.Format(exeSQL, CommandCode, Info, ServiceID, UserID);

                result = VatLid.DAL.GetDataToString(SQL);

            }
            catch (Exception e)
            {
                result = "-1";
            }
            return result;

        }

        public static string getURLXNK(string ServiceID, string CommandCode, string type, string UserID)
        {
            string result = "0";
            try
            {
                string exeSQL = "Exec Select_Download_WapXNK '{0}','{1}','{2}','{3}';";
                string SQL = string.Format(exeSQL, CommandCode, type, ServiceID, UserID);

                result = VatLid.DAL.GetDataToString(SQL);

            }
            catch (Exception e)
            {
                result = "-1";
            }
            return result;

        }

        public static string getCalendar(int VID, string Dateset, int TypeID)
        {
            string result = "0";
            try
            {
                string exeSQL = "Exec insert_CalendarItemVideo {0},'{1}',{2};";
                string SQL = string.Format(exeSQL, VID, Dateset, TypeID);

                result = VatLid.DAL.GetDataToString(SQL);

            }
            catch (Exception e)
            {
                result = "-1";
            }
            return result;

        }

        public static string GetMoneyFake(string sEncodedPhone, string serviceId, string commandcode, string info)
        {
            string result = "";
            string tmp = ConfigurationSettings.AppSettings["wsSyntaxMO"];//value="Exec dbo.Insert_SMO 'VTT_8x62','{0}','{1}','{2}','{3}'"/>
            string SQL = string.Format(tmp, sEncodedPhone, serviceId, commandcode, info);
            bool getOk = false;

            try
            {
                if (ED.libUser.ExecuteQueryNew98(SQL) > 0)
                {
                    getOk = true;
                }
                if (getOk)
                {
                    result = "1";
                }
                else
                {
                    result = "0";
                }
            }
            catch (Exception ex)
            {
            }
            return result;

        }
        //public static string GetDataToString(string SQL)
        //{
        //    string sResult = "";
        //    using (SqlConnection conn = new SqlConnection(ConnStr1))
        //    using (SqlCommand cmd = new SqlCommand(SQL, conn))
        //    {
        //        try
        //        {
        //            conn.Open();
        //            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
        //            {
        //                while (reader.Read())
        //                {
        //                    for (int i = 0; i < reader.FieldCount; ++i)
        //                        sResult = sResult + reader[i].ToString() + "|";
        //                    sResult = sResult.Substring(0, sResult.Length - 1) + "";
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            sResult = ex.Message;
        //        }
        //    }
        //    return sResult;
        //}
        //public static void Update_History_SMS(string UserID, string ReceiverID, string ServiceID, string CommandCode, string Info, string UserName, int status)
        //{
        //    try
        //    {
        //        SqlParameter[] parameters = 
        //                                { 
        //                                    new SqlParameter("@USERID", SqlDbType.NVarChar ),
        //                                    new SqlParameter("@ReceiverID", SqlDbType.NVarChar),
        //                                    new SqlParameter("@SERVICEID", SqlDbType.NVarChar ),
        //                                    new SqlParameter("@COMMANDCODE", SqlDbType.NVarChar),
        //                                    new SqlParameter("@INFO", SqlDbType.NVarChar),
        //                                    new SqlParameter("@Status", SqlDbType.Int),
        //                                    new SqlParameter("@UserName", SqlDbType.NVarChar),
        //                                    new SqlParameter("@ID", SqlDbType.Int),

        //                };
        //        parameters[0].Value = UserID;
        //        parameters[1].Value = ReceiverID;
        //        parameters[2].Value = ServiceID;
        //        parameters[3].Value = CommandCode;
        //        parameters[4].Value = Info;
        //        parameters[5].Value = status;
        //        parameters[6].Value = UserName;
        //        parameters[7].Direction = ParameterDirection.Output;

        //        VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "insert_SMS_LOG_WEB", parameters);
        //    }

        //    catch (Exception ex)
        //    {
        //        VatLid.DAL.ExceptionProcess(ex);
        //    }
        //}

        public static string getAlbumNameGames(string id)
        {
            string name = "";
            try
            {
                string SQL = "SELECT AlbumName FROM GameAlbum where ID=" + Convert.ToInt32(id);
                ArrayList al = VatLid.DAL.GetDataReaderToArrayList(SQL);
                string[][] arrReturn = (string[][])al.ToArray(typeof(string[]));
                if (al.Count > 0)
                {
                    name = arrReturn[0][0];
                }

            }
            catch (Exception e)
            {
                VatLid.DAL.ExceptionProcess(e);
            }
            return name;
        }



        public static string getPriceService(string ServicesID)
        {
            String sql = "select PriceSale from Services where Name='" + ServicesID + "'";
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            else
            {
                return "0";
            }
        }

        public static string getCategoryNameGames(string id)
        {
            string name = "";
            try
            {
                string SQL = "SELECT CategoryName FROM GameCategories where ID=" + Convert.ToInt32(id);
                ArrayList al = VatLid.DAL.GetDataReaderToArrayList(SQL);
                string[][] arrReturn = (string[][])al.ToArray(typeof(string[]));
                if (al.Count > 0)
                {
                    name = arrReturn[0][0];
                }

            }
            catch (Exception e)
            {
                VatLid.DAL.ExceptionProcess(e);
            }
            return name;
        }
        public static string getCategoryHN(string id)
        {
            string sql = "select Name from ImageCate where ID=" + id;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            else
            {
                return "Chưa xác định";
            }
        }






        public static string getNameTrue(string code)
        {
            String sql = "select ItemName from MusicRight where ItemCode=" + code;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            else
            {
                return "Chưa xác định";
            }
        }

        public static string getNameFCRBT(string code)
        {
            String sql = "select toneName from tbRBTs where ID=" + code;
            String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
            if (dl != null)
            {
                return dl[0];
            }
            else
            {
                return "Chưa xác định";
            }
        }
        public static string getDownload(string ServiceID, string CommandCode, string Info, string userid)
        {
            string txt = "0";
            getContent send;
            System.Exception exception;
            send = new getContent();
            send.PreAuthenticate = true;
            send.Credentials = new System.Net.NetworkCredential("content", "content123$%^");
            try
            {
                txt = send.getDownloadMessages(ServiceID, CommandCode, Info, userid);
                goto ILO_005f;
            }
            catch (System.Exception exception1)
            {
                exception = exception1;
                txt = "-1";
                txt = exception.Message.ToString();
                goto ILO_005f;
            }
        ILO_005f:
            return txt;

        }


        public static void INSERT_USER_USE_WAP(string UserID, string ItemID, int type)
        {
            try
            {
                SqlParameter[] parameters = 
									    { 
										    new SqlParameter("@UserID", SqlDbType.NVarChar ),
										    new SqlParameter("@ITEMID", SqlDbType.NVarChar),
                                            new SqlParameter("@Type", SqlDbType.Int),
										    
    										
					    };
                parameters[0].Value = UserID;
                parameters[1].Value = ItemID;
                parameters[2].Value = type;


                VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "sp_insert_User_wap", parameters);
            }

            catch (Exception ex)
            {
                VatLid.DAL.ExceptionProcess(ex);
            }
        }

        public static void INSERT_USER_USE_WAP_SO(string UserID, string ItemID, int type)
        {
            try
            {
                SqlParameter[] parameters = 
									    { 
										    new SqlParameter("@UserID", SqlDbType.NVarChar ),
										    new SqlParameter("@ITEMID", SqlDbType.NVarChar),
                                            new SqlParameter("@Type", SqlDbType.Int),
										    
    										
					    };
                parameters[0].Value = UserID;
                parameters[1].Value = ItemID;
                parameters[2].Value = type;


                VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "sp_insert_User_wap_nhacso", parameters);
            }

            catch (Exception ex)
            {
                VatLid.DAL.ExceptionProcess(ex);
            }
        }


        public static void INSERT_USER_USE_WAP_Chuong(string UserID, string ItemID, int type)
        {
            try
            {
                SqlParameter[] parameters = 
									    { 
										    new SqlParameter("@UserID", SqlDbType.NVarChar ),
										    new SqlParameter("@ITEMID", SqlDbType.NVarChar),
                                            new SqlParameter("@Type", SqlDbType.Int),
										    
    										
					    };
                parameters[0].Value = UserID;
                parameters[1].Value = ItemID;
                parameters[2].Value = type;


                VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "sp_insert_User_wap_chuong", parameters);
            }

            catch (Exception ex)
            {
                VatLid.DAL.ExceptionProcess(ex);
            }
        }

        public static void INSERT_USER_USE_WAP_QUATANG(string UserID, string ItemID, int type)
        {
            try
            {
                SqlParameter[] parameters = 
									    { 
										    new SqlParameter("@UserID", SqlDbType.NVarChar ),
										    new SqlParameter("@ITEMID", SqlDbType.NVarChar),
                                            new SqlParameter("@Type", SqlDbType.Int),
										    
    										
					    };
                parameters[0].Value = UserID;
                parameters[1].Value = ItemID;
                parameters[2].Value = type;


                VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "sp_insert_User_wap_Quatang", parameters);
            }

            catch (Exception ex)
            {
                VatLid.DAL.ExceptionProcess(ex);
            }
        }

        public static void INSERT_USER_USE_WAP_RADIO(string UserID, string ItemID, int type)
        {
            try
            {
                SqlParameter[] parameters = 
									    { 
										    new SqlParameter("@UserID", SqlDbType.NVarChar ),
										    new SqlParameter("@ITEMID", SqlDbType.NVarChar),
                                            new SqlParameter("@Type", SqlDbType.Int),
										    
    										
					    };
                parameters[0].Value = UserID;
                parameters[1].Value = ItemID;
                parameters[2].Value = type;


                VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "sp_insert_User_wap_Radio", parameters);
            }

            catch (Exception ex)
            {
                VatLid.DAL.ExceptionProcess(ex);
            }
        }

        public static void INSERT_USER_USE_WAP_ISTORY(string UserID, string ItemID, int type)
        {
            try
            {
                SqlParameter[] parameters = 
									    { 
										    new SqlParameter("@UserID", SqlDbType.NVarChar ),
										    new SqlParameter("@ITEMID", SqlDbType.NVarChar),
                                            new SqlParameter("@Type", SqlDbType.Int),
										    
    										
					    };
                parameters[0].Value = UserID;
                parameters[1].Value = ItemID;
                parameters[2].Value = type;


                VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "sp_insert_User_wap_Istory", parameters);
            }

            catch (Exception ex)
            {
                VatLid.DAL.ExceptionProcess(ex);
            }
        }

        public static void INSERT_USER_USE_WAP_THEOTEN(string UserID, string ItemID, int type)
        {
            try
            {
                SqlParameter[] parameters = 
									    { 
										    new SqlParameter("@UserID", SqlDbType.NVarChar ),
										    new SqlParameter("@ITEMID", SqlDbType.NVarChar),
                                            new SqlParameter("@Type", SqlDbType.Int),
										    
    										
					    };
                parameters[0].Value = UserID;
                parameters[1].Value = ItemID;
                parameters[2].Value = type;


                VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "sp_insert_User_wap_TheoTen", parameters);
            }

            catch (Exception ex)
            {
                VatLid.DAL.ExceptionProcess(ex);
            }
        }

        public static void INSERT_USER_USE_WAP_THIEPNHAC(string UserID, string ItemID, int type)
        {
            try
            {
                SqlParameter[] parameters = 
									    { 
										    new SqlParameter("@UserID", SqlDbType.NVarChar ),
										    new SqlParameter("@ITEMID", SqlDbType.NVarChar),
                                            new SqlParameter("@Type", SqlDbType.Int),
										    
    										
					    };
                parameters[0].Value = UserID;
                parameters[1].Value = ItemID;
                parameters[2].Value = type;


                VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "sp_insert_User_wap_ThiepNhac", parameters);
            }

            catch (Exception ex)
            {
                VatLid.DAL.ExceptionProcess(ex);
            }
        }

        public static void INSERT_USER_USE_WAP_FLASH(string UserID, string ItemID, int type)
        {
            try
            {
                SqlParameter[] parameters = 
									    { 
										    new SqlParameter("@UserID", SqlDbType.NVarChar ),
										    new SqlParameter("@ITEMID", SqlDbType.NVarChar),
                                            new SqlParameter("@Type", SqlDbType.Int),
										    
    										
					    };
                parameters[0].Value = UserID;
                parameters[1].Value = ItemID;
                parameters[2].Value = type;


                VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "sp_insert_User_wap_Flash", parameters);
            }

            catch (Exception ex)
            {
                VatLid.DAL.ExceptionProcess(ex);
            }
        }
        public static void INSERT_USER_USE_WAP_COMIC(string UserID, string ItemID, int type)
        {
            try
            {
                SqlParameter[] parameters = 
									    { 
										    new SqlParameter("@UserID", SqlDbType.NVarChar ),
										    new SqlParameter("@ITEMID", SqlDbType.NVarChar),
                                            new SqlParameter("@Type", SqlDbType.Int),
										    
    										
					    };
                parameters[0].Value = UserID;
                parameters[1].Value = ItemID;
                parameters[2].Value = type;


                VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "sp_insert_User_wap_Comic", parameters);
            }

            catch (Exception ex)
            {
                VatLid.DAL.ExceptionProcess(ex);
            }
        }

        public static void INSERT_USER_USE_WAP_VIDEO(string UserID, string ItemID, int type)
        {
            try
            {
                SqlParameter[] parameters = 
									    { 
										    new SqlParameter("@UserID", SqlDbType.NVarChar ),
										    new SqlParameter("@ITEMID", SqlDbType.NVarChar),
                                            new SqlParameter("@Type", SqlDbType.Int),
										    
    										
					    };
                parameters[0].Value = UserID;
                parameters[1].Value = ItemID;
                parameters[2].Value = type;


                VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "sp_insert_User_wap_Video", parameters);
            }

            catch (Exception ex)
            {
                VatLid.DAL.ExceptionProcess(ex);
            }
        }

        public static void INSERT_USER_USE_WAP_HAD(string UserID, string ItemID, int type)
        {
            try
            {
                SqlParameter[] parameters = 
									    { 
										    new SqlParameter("@UserID", SqlDbType.NVarChar ),
										    new SqlParameter("@ITEMID", SqlDbType.NVarChar),
                                            new SqlParameter("@Type", SqlDbType.Int),
										    
    										
					    };
                parameters[0].Value = UserID;
                parameters[1].Value = ItemID;
                parameters[2].Value = type;


                VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "sp_insert_User_wap_HAD", parameters);
            }

            catch (Exception ex)
            {
                VatLid.DAL.ExceptionProcess(ex);
            }
        }

        public static void INSERT_USER_USE_WAP_HAN(string UserID, string ItemID, int type)
        {
            try
            {
                SqlParameter[] parameters = 
									    { 
										    new SqlParameter("@UserID", SqlDbType.NVarChar ),
										    new SqlParameter("@ITEMID", SqlDbType.NVarChar),
                                            new SqlParameter("@Type", SqlDbType.Int),
										    
    										
					    };
                parameters[0].Value = UserID;
                parameters[1].Value = ItemID;
                parameters[2].Value = type;


                VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "sp_insert_User_wap_HAN", parameters);
            }

            catch (Exception ex)
            {
                VatLid.DAL.ExceptionProcess(ex);
            }
        }


        public static bool DownloadFile(string _URL, string _SaveAs)
        {
            Boolean result = false;

            try
            {
                System.Net.WebClient _WebClient = new System.Net.WebClient();

                _WebClient.DownloadFile(_URL, _SaveAs);
                result = true;
            }
            catch (Exception _Exception)
            {
                result = false;
            }
            return result;
        }

        //public static void INSERT_NHACCHUONG(string ReceiverID, string UserID, string ItemCode, string ItemName, string SingerName, string Price, string Cpname)
        //{
        //    if (ItemCode == null || ItemCode == "") return;
        //    try
        //    {

        //        SqlParameter[] parameters = 
        //                    { 
        //                        new SqlParameter("@ReceiverID", SqlDbType.NVarChar ), 
        //                        new SqlParameter("@USERID", SqlDbType.NVarChar ), 
        //                        new SqlParameter("@ItemCode", SqlDbType.NVarChar ), 			
        //                        new SqlParameter("@ItemName", SqlDbType.NVarChar ), 
        //                        new SqlParameter("@SingerName", SqlDbType.NVarChar ), 
        //                        new SqlParameter("@Price", SqlDbType.NVarChar ), 
        //                        new SqlParameter("@CPName", SqlDbType.NVarChar )
        //                    };
        //        parameters[0].Value = ReceiverID;
        //        parameters[1].Value = UserID;
        //        parameters[2].Value = ItemCode;
        //        parameters[3].Value = ItemName;
        //        parameters[4].Value = SingerName;
        //        parameters[5].Value = Price;
        //        parameters[6].Value = Cpname;


        //        VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "insert_NHACCHUONG", parameters);

        //    }
        //    catch (Exception ex)
        //    {
        //        VatLid.DAL.ExceptionProcess(ex);
        //    }
        //    ItemCode = "";

        //}

        public static void insert_XNKLOG(string ReceiverID, string UserID, string ItemCode, string ItemName, string SingerName, string Price, string Cpname, string type)
        {
            if (ItemCode == null || ItemCode == "") return;
            try
            {

                SqlParameter[] parameters = 
							{ 
								new SqlParameter("@ReceiverID", SqlDbType.NVarChar ), 
								new SqlParameter("@USERID", SqlDbType.NVarChar ), 
								new SqlParameter("@ItemCode", SqlDbType.NVarChar ), 			
								new SqlParameter("@ItemName", SqlDbType.NVarChar ), 
								new SqlParameter("@SingerName", SqlDbType.NVarChar ), 
								new SqlParameter("@Price", SqlDbType.NVarChar ), 
								new SqlParameter("@CPName", SqlDbType.NVarChar ),
                                new SqlParameter("@Type", SqlDbType.NVarChar )
							};
                parameters[0].Value = ReceiverID;
                parameters[1].Value = UserID;
                parameters[2].Value = ItemCode;
                parameters[3].Value = ItemName;
                parameters[4].Value = SingerName;
                parameters[5].Value = Price;
                parameters[6].Value = Cpname;
                parameters[7].Value = type;

                VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "insert_XNKLOG", parameters);

            }
            catch (Exception ex)
            {
                VatLid.DAL.ExceptionProcess(ex);
            }
            ItemCode = "";

        }

        public static void INSERT_LOG_WAP(string UserID, string Name, string mobile)
        {
            try
            {

                SqlParameter[] parameters = 
					{ 
						new SqlParameter("@UserID", SqlDbType.NVarChar ), 
						new SqlParameter("@URL", SqlDbType.NVarChar),
                        new SqlParameter("@Mobile", SqlDbType.NVarChar),
						
				};
                parameters[0].Value = UserID;
                parameters[1].Value = Name;
                parameters[2].Value = mobile;


                VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "insert_USER_WAP_LOG", parameters);
            }
            catch (Exception ex)
            {
                VatLid.DAL.ExceptionProcess(ex);
            }
        }


        public static void GoiVideoTuan_Update(int id, int type)
        {
            try
            {
                string SQL = string.Format("select top(1) ItemCode, ItemName from ItemVideo where id = {0}", id);

                DataSet ds = VatLid.SqlHelper.ExecuteDataset(VatLid.DAL.getConnectionString1(), CommandType.Text, SQL);

                string Code = ds.Tables[0].Rows[0]["ItemCode"].ToString();
                string Name = ds.Tables[0].Rows[0]["ItemName"].ToString();

                SqlParameter[] parameters = 
					{ 
						new SqlParameter("@Code", SqlDbType.NVarChar ), 
						new SqlParameter("@Name", SqlDbType.NVarChar),
                        new SqlParameter("@Type", SqlDbType.Int),
				};

                parameters[0].Value = Code;
                parameters[1].Value = Name;
                parameters[2].Value = type;

                VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "ItemVideoSubTuan$Update", parameters);
            }
            catch (Exception ex)
            {
                VatLid.DAL.ExceptionProcess(ex);
            }
        }

        public static void VideoHotChuDe_Update(int id, int cid, int type)
        {
            try
            {
                string SQL = string.Format("select top(1) ItemCode, ItemName from ItemVideo where id = {0}", id);

                DataSet ds = VatLid.SqlHelper.ExecuteDataset(VatLid.DAL.getConnectionString1(), CommandType.Text, SQL);

                string Code = ds.Tables[0].Rows[0]["ItemCode"].ToString();
                string Name = ds.Tables[0].Rows[0]["ItemName"].ToString();

                Name = Name.Replace("'", "");

                SqlParameter[] parameters = 
					{ 
						new SqlParameter("@cid", SqlDbType.Int), 
						new SqlParameter("@isOther", SqlDbType.Int),
                        new SqlParameter("@code", SqlDbType.NVarChar),
                        new SqlParameter("@name", SqlDbType.NVarChar)
				};

                parameters[0].Value = cid;
                parameters[1].Value = type;
                parameters[2].Value = Code;
                parameters[3].Value = Name;

                VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "SubVideoHot$Update", parameters);

                VTT.VFun.Gate.Service1 srv = new VTT.VFun.Gate.Service1();
                srv.SubVideoHot_Update(cid.ToString(), type.ToString(), Code, Name, "vfun", "1!2@3#4$5%");
            }
            catch (Exception ex)
            {
                VatLid.DAL.ExceptionProcess(ex);
            }
        }

        public static void Insert_ComicSub(string ids, int CategoryID)
        {
            try
            {
                SqlParameter[] parameters = 
									    { 										    
										   new SqlParameter("@ids", SqlDbType.NVarChar),	
									       new SqlParameter("@CategoryID", SqlDbType.Int),	                                  										
					                    };

                parameters[0].Value = ids;
                parameters[1].Value = CategoryID;

                VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "ItemComicSub$SetBatch", parameters);
            }

            catch (Exception ex)
            {
                VatLid.DAL.ExceptionProcess(ex);
            }
        }

        public static void Insert_ItemVideoSubVD3(string ids)
        {
            try
            {
                SqlParameter[] parameters = 
									    { 										    
										   new SqlParameter("@ids", SqlDbType.NVarChar),	
									                                        										
					                    };

                parameters[0].Value = ids;

                VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "ItemVideoSubVD3$SetBatch", parameters);
            }

            catch (Exception ex)
            {
                VatLid.DAL.ExceptionProcess(ex);
            }
        }





        public static void Insert_ItemVideoTaoQuan(string ids)
        {
            try
            {
                SqlParameter[] parameters = 
									    { 										    
										   new SqlParameter("@ids", SqlDbType.NVarChar),	
									                                        										
					                    };

                parameters[0].Value = ids;

                VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "ItemVideoTaoQuan$SetBatch", parameters);
            }

            catch (Exception ex)
            {
                VatLid.DAL.ExceptionProcess(ex);
            }
        }





        #region "Format Content"

        public static string FormatSave(string Html)
        {
            //Html = VatLid.Utils.KillChars1(Html);
            //Html = Html.Replace("http://115.84.178.52/", "./");
            Html = Html.Replace(VatLid.Variables.sLinkToResource, "./");
            Html = Html.Replace("../../../archive/images", "./archive/images");
            Html = Html.Replace("../../archive/images", "./archive/images");
            Html = Html.Replace("../archive/images", "./archive/images");

            Html = Html.Replace(VatLid.Variables.sLinkToResource + "archive/images", "/archive/images");

            //Html = Html.Replace("/cms/archive/images", "./archive/images");
            Html = Html.Replace("/mcontent/archive/images", "./archive/images");
            Html = Html.Replace("/archive/images", "./archive/images");
            //Html = Html.Replace("/archive/flashs", VatLid.Variables.sLinkToResource +"archive/flashs");
            //Html = Html.Replace("/archive/files", VatLid.Variables.sLinkToResource +"archive/files");
            Html = Html.Replace("./archive/media", VatLid.Variables.sLinkToResource + "archive/media");
            Html = Html.Replace(".http://", "http://");
            Html = Html.Replace(".../http://", "http://");
            Html = Html.Replace("/mcontent", "");
            Html = Html.Replace("/cms", "");


            return Html;
        }


        public static string FormatView(string Html)
        {
            string s = VatLid.Variables.sLinkToResource + "image/";

            Html = Html.Replace("../../image/", s);
            Html = Html.Replace("../image/", s);
            Html = Html.Replace("./image/", s);
            Html = Html.Replace("../../Image/", s);
            Html = Html.Replace("../Image/", s);
            Html = Html.Replace("./Image/", s);

            Html = Html.Replace("/cms/", "/");
            Html = Html.Replace("../../../archive/images", "/archive/images");
            Html = Html.Replace("../../archive/images", "/archive/images");
            Html = Html.Replace("../archive/images", "/archive/images");
            Html = Html.Replace("./archive/images", "/archive/images");
            Html = Html.Replace(".../http://", "http://");
            Html = Html.Replace("/archive/images", VatLid.Variables.sLinkToResource + "archive/images"); //swich to new ip
            // Html = Html.Replace("/cms", "");

            return Html;
        }

        #endregion

        public static string getRandom()
        {
            return rnd.Next(999999999).ToString();
        }

        private static Random rnd = new Random();





        #region "CSKH"

        public static string BindDataGridCSKH(System.Web.UI.WebControls.DataGrid dgr, string SQL, string configName)
        {
            string count = "0";
            int curPage = dgr.CurrentPageIndex;

            string ConnectStr = getConfig(configName);
            using (SqlConnection conn = new SqlConnection(ConnectStr))
            using (SqlCommand cmd = new SqlCommand(SQL, conn))
            {
                try
                {
                    conn.Open();
                    cmd.CommandTimeout = 50;
                    using (SqlDataAdapter reader = new SqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable("viw_Report");//new DataTable();
                        DataColumn ID = new DataColumn("ID", System.Type.GetType("System.Int32"));
                        ID.AllowDBNull = true;
                        ID.AutoIncrement = true;
                        ID.AutoIncrementSeed = 1;
                        table.Columns.Add(ID);
                        DataColumn[] keys = new DataColumn[1];
                        keys[0] = ID;
                        table.PrimaryKey = keys;


                        reader.Fill(table);
                        if (table != null)
                        {
                            dgr.DataSource = table;
                            dgr.DataBind();

                            dgr.DataKeyField = "ID";
                            count = table.Rows.Count.ToString();
                            table.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {
                    //_errorDesc = ex.Message;
                    count = "ERROR: " + ex.Message;
                }
            }
            return count;
        }

        public static string BindDataGridCSKH_Old(System.Web.UI.WebControls.DataGrid dgr, string SQL, string ConnectStr)
        {
            string count = "0";
            int curPage = dgr.CurrentPageIndex;

            using (SqlConnection conn = new SqlConnection(ConnectStr))
            using (SqlCommand cmd = new SqlCommand(SQL, conn))
            {
                try
                {
                    conn.Open();
                    cmd.CommandTimeout = 50;
                    using (SqlDataAdapter reader = new SqlDataAdapter(cmd))
                    {
                        DataTable table = CreateTableInfo("viw_Report");//new DataTable();
                        reader.Fill(table);
                        if (table != null)
                        {
                            dgr.DataSource = table;
                            dgr.DataBind();

                            count = table.Rows.Count.ToString();
                            table.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {
                    //_errorDesc = ex.Message;
                    count = "ERROR: " + ex.Message;
                }
            }
            return count;
        }

        public static string getDateFromText(string txtDate)
        {
            try
            {
                if (txtDate == null || txtDate == "")
                    return DateTime.Now.ToString("yyyyMMdd");
                string sDate = txtDate.Trim().Replace("/", "");
                return sDate.Substring(sDate.Length - 4) + sDate.Substring(2, 2) + sDate.Substring(0, 2);
            }
            catch { }
            return DateTime.Now.ToString("yyyyMMdd");
        }

        #endregion


        // ngay 07/05/2012

        public static string getFormatMoney(string amount)
        {
            string tmp = String.Format("{0:### ### ### ###}", Convert.ToDecimal(amount));
            return tmp.Trim().Replace(" ", ".") + " d";
        }


        public static int SendEmail(string sender, string to, string subject, string content)
        {
            if (SendEmail_2.SendMail.send(sender, to, subject, content))
                return 1;
            else
                return 0;
        }

        public static List<String> GetAllImagesInPage(string html)
        {
            Regex Tags = new Regex("<.*?>", RegexOptions.Compiled | RegexOptions.Singleline);
            MatchCollection matchCollection = Tags.Matches(html);

            List<String> links_image = new List<string>();

            foreach (Match match in matchCollection)
            {
                string true_tag = match.Value;
                string tag = true_tag.ToLower();

                if (tag.Contains("<img"))
                {
                    string src = Regex.Match(tag, @"(src)\s*=\s*[""']?([^'"" >]+?)[ '""][^>]*?").Value;

                    if (src.Trim().Length > 0) //?nh
                    {
                        src = src.Replace("src", "");
                        src = src.Replace("'", "");
                        src = src.Replace("\"", "");
                        src = src.Replace("=", "");

                        if (!src.Contains("http")) // Khong co http
                        {
                            src = src.Replace("/cmsdiv2", "");
                            src = src.Replace("./image/", "http://images.dailyinfo.vn/image/");
                            src = src.Replace("../archive/images", "http://images.dailyinfo.vn/archive/images");
                            src = src.Replace("./archive/images", "http://images.dailyinfo.vn/archive/images");
                            src = src.Replace("/CMS/archive/images", "http://images.dailyinfo.vn/archive/images");


                        }

                        links_image.Add(src);
                    }
                }
            }

            return links_image;
        }


        public static string StripWhiteSpace(string input)
        {
            input = System.Text.RegularExpressions.Regex.Replace(input, @"\s{2,}", " ", System.Text.RegularExpressions.RegexOptions.Compiled);
            return input.Trim();
        }


        public static DataSet GetMenuLeft(int USERGROUPID, int USERID)
        {
            DataSet mDataSet = null;
            SqlConnection conn = new SqlConnection(ConnStr1);

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "cms_CategoriesMenu_byUSERGROUPID";
                cmd.Parameters.AddWithValue("@USERGROUPID", USERGROUPID);
                cmd.Parameters.AddWithValue("@USERID", USERID);

                conn.Open();
                using (SqlDataAdapter reader = new SqlDataAdapter(cmd))
                {
                    try
                    {
                        mDataSet = new DataSet();
                        reader.Fill(mDataSet);
                    }
                    catch
                    {
                        mDataSet = null;
                    }
                }
            }
            catch
            {
                mDataSet = null;
            }
            finally
            {
                conn.Close();
            }

            return mDataSet;
        }

        public static DataSet GetMenuChildLeft(int USERGROUPID, int USERID, int catid)
        {
            DataSet mDataSet = null;
            SqlConnection conn = new SqlConnection(ConnStr1);

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "cms_CategoriesMenuChild";
                cmd.Parameters.AddWithValue("@USERGROUPID", USERGROUPID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@catid", catid);

                conn.Open();
                using (SqlDataAdapter reader = new SqlDataAdapter(cmd))
                {
                    try
                    {
                        mDataSet = new DataSet();
                        reader.Fill(mDataSet);
                    }
                    catch
                    {
                        mDataSet = null;
                    }
                }
            }
            catch
            {
                mDataSet = null;
            }
            finally
            {
                conn.Close();
            }

            return mDataSet;
        }

        //VuDoan 13/08/2012 
        // Tìm số lần login fail của user
        public static DataSet SelectCountUserLogin(string UserName)
        {
            DataSet mDataSet = null;
            SqlConnection conn = new SqlConnection(ConnStr1);

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "cms_Select_User_Log_Count";
                cmd.Parameters.AddWithValue("@UserName", UserName);


                conn.Open();
                using (SqlDataAdapter reader = new SqlDataAdapter(cmd))
                {
                    try
                    {
                        mDataSet = new DataSet();
                        reader.Fill(mDataSet);
                    }
                    catch
                    {
                        mDataSet = null;
                    }
                }
            }
            catch
            {
                mDataSet = null;
            }
            finally
            {
                conn.Close();
            }

            return mDataSet;
        }
        //Vũ Đoàn 30082012 kiểm tra user và pass đăng nhập
        public static DataSet UserLogin(string UserName, string Password)
        {
            DataSet mDataSet = null;
            SqlConnection conn = new SqlConnection(ConnStr1);

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "cms_userlogin";
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@PassMD5", Password);


                conn.Open();
                using (SqlDataAdapter reader = new SqlDataAdapter(cmd))
                {
                    try
                    {
                        mDataSet = new DataSet();
                        reader.Fill(mDataSet);
                    }
                    catch
                    {
                        mDataSet = null;
                    }
                }
            }
            catch
            {
                mDataSet = null;
            }
            finally
            {
                conn.Close();
            }

            return mDataSet;
        }

        public static void INSERT_ItemVideoHistory(int VID, int CategoryStatus, string UserLogin, string Description)
        {
            try
            {
                SqlParameter[] parameters = 
									    { 
										    new SqlParameter("@VID", SqlDbType.Int ),
										    new SqlParameter("@CategoryStatus", SqlDbType.Int),
										    new SqlParameter("@UserLogin", SqlDbType.NVarChar ),
                                            new SqlParameter("@Description", SqlDbType.NVarChar )
                                           
                                            
    										
					    };
                parameters[0].Value = VID;
                parameters[1].Value = CategoryStatus;
                parameters[2].Value = UserLogin;
                parameters[3].Value = Description;

                VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "insert_ItemVideoHistory", parameters);
            }

            catch (Exception ex)
            {
                VatLid.DAL.ExceptionProcess(ex);
            }
        }

        public static void INSERT_USER_LOG_NEW(string UserID, string ActionName, string ActionDesc, string ActionStatus, string ActionID, string ip)
        {
            try
            {
                SqlParameter[] parameters = 
									    { 
										    new SqlParameter("@UserID", SqlDbType.NVarChar ),
										    new SqlParameter("@ActionName", SqlDbType.NVarChar),
										    new SqlParameter("@ActionDesc", SqlDbType.NVarChar ),
                                            new SqlParameter("@ActionStatus", SqlDbType.NVarChar ),
                                            new SqlParameter("@ActionID", SqlDbType.NVarChar ),
                                            new SqlParameter("@IP", SqlDbType.NVarChar ),
                                            
    										
					    };
                parameters[0].Value = UserID;
                parameters[1].Value = ActionName;
                parameters[2].Value = ActionDesc;
                parameters[3].Value = ActionStatus;
                parameters[4].Value = ActionID;
                parameters[5].Value = ip;
                VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "insert_ACTION_LOG_NEW", parameters);
            }

            catch (Exception ex)
            {
                VatLid.DAL.ExceptionProcess(ex);
            }
        }
        public static void UserLogs_InsertBatch(int uid, string userName, string mids, int type)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@uid", SqlDbType.Int),
                    new SqlParameter("@userName", SqlDbType.NVarChar),
                    new SqlParameter("@mids", SqlDbType.NVarChar),
                    new SqlParameter("@type", SqlDbType.Int)
                };
                parameters[0].Value = uid;
                parameters[1].Value = userName;
                parameters[2].Value = mids;
                parameters[3].Value = type;

                VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure,
                    "cms_UserLogs$InsertBatch", parameters);
            }
            catch (Exception ex)
            {
                VatLid.DAL.ExceptionProcess(ex);
            }
        }


        public static int CheckMeNu(string MenuID, string UserID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                if (MenuID == null) MenuID = "0";
                cmd.Parameters.AddWithValue("@MenuID", MenuID);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                DataSet ds = VatLid.DAL.GetDataSet("CheckMeNu", VatLid.DAL.getConnectionString1(), cmd);
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            catch
            {
                return 0;
            }
        }


    }
}
