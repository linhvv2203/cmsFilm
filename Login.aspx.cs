using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using System.Configuration;
using VatLid;
using System.Text.RegularExpressions;

namespace MarketVN.Sysadmin
{
    public partial class Login : System.Web.UI.Page
    {

        protected string sIp = "";
        protected string sGio = "";
        protected string FileName;
        protected int intLoginResult = 0;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            sIp = GetIP();
            DateTime dtNow = DateTime.Now;
            string Gio = dtNow.ToLocalTime().ToShortTimeString().ToString();
            sGio = Gio;

            string P_I = Request.ServerVariables["PATH_INFO"];
            string[] aPI = P_I.Split('/');
            int iLength = aPI.Length;
            FileName = aPI[iLength - 1];


            if (!Page.IsPostBack)
            {
                Session.Abandon();
            }
        }
        public string GetIP()
        {
            try
            {
                HttpRequest currentRequest = HttpContext.Current.Request;
                string ipAddress = currentRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipAddress == null || ipAddress.ToLower() == "unknown")
                    ipAddress = currentRequest.ServerVariables["REMOTE_ADDR"];
                return ipAddress;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);


        }
        protected int Random()
        {
            Random rndNum = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            int randomrequest = rndNum.Next(0, 9999);
            return randomrequest;
        }



        protected bool checkMumber(string str)
        {
            bool result = false;
            for (int i = 0; i < str.Length; i++)
            {
                if (IsNumber(str.Substring(i, 1)))
                    result = true;

            }
            return result;
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        protected void Log_login(int intResult)
        {
            try
            {
                SqlParameter[] parameters = 
							{ 
								new SqlParameter("@Username", SqlDbType.NVarChar ), 
								new SqlParameter("@Password", SqlDbType.NVarChar ),
                                new SqlParameter("@UsernameChar", SqlDbType.NVarChar ), 
								new SqlParameter("@PasswordChar", SqlDbType.NVarChar ), 
								new SqlParameter("@Gio", SqlDbType.NVarChar ), 			
								new SqlParameter("@IP", SqlDbType.NVarChar ), 
                                new SqlParameter("@Result", SqlDbType.Int), 
							};
                parameters[0].Value = txtUserName.Value;
                parameters[1].Value = txtPass.Value;
                parameters[2].Value = VatLid.Utils.ValidateXSS(VatLid.Utils.KillChars(txtUserName.Value));
                parameters[3].Value = VatLid.Utils.ValidateXSS(VatLid.Utils.KillChars(txtPass.Value));
                parameters[4].Value = sGio;
                parameters[5].Value = sIp;
                parameters[6].Value = intResult;

                VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "insert_UsersLoginLog", parameters);

            }
            catch (Exception ex)
            {
                VatLid.DAL.ExceptionProcess(ex);
            }
        }




        protected void cmdDangNhap_Click1(object sender, ImageClickEventArgs e)
        {
            DataSet ds;
            string userName = txtUserName.Value.Trim().Replace("'", "''");
            if (!VatLid.Utils.KillChars3(userName))
            {
                MessageBox.Show("Tên nhập có ký tự đặc biệt !");
                return;
            }
            Captcha1.ValidateCaptcha(txtMaBaoMat.Value.Trim());

            if (!Captcha1.UserValidated)
            {
                MessageBox.Show("Bạn nhập sai ma capcha !");
                return;
            }

            if (userName.Length == 0)
            {
                MessageBox.Show("Bạn nhập tên đăng nhập !");
                return;
            }

            string Pass = txtPass.Value;

            if (Pass.Length == 0)
            {
                MessageBox.Show("Bạn nhập password !");
                return;
            }

            //ds = VatLid.DAL.SelectCountUserLogin(txtUserName.Value.Trim().Replace("'", "''"));

            //if (ds.Tables[1].Rows.Count > 0)
            //{
            //    if (Convert.ToInt32(ds.Tables[1].Rows[0]["countLog"].ToString()) >= 5)
            //    {
            //        Response.Redirect(VatLid.Variables.sWebRoot + "error_info.aspx?err=36");
            //    }
            //}



            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@PassMD5", SaltedHash.EncodeMD5(Pass));

                ds = VatLid.DAL.GetDataSet("[cms_userlogin]", VatLid.DAL.getConnectionString1(), cmd);
                DataTable dt = ds.Tables[0];

                userName = VatLid.Utils.safeString(userName);
                if (dt.Rows.Count > 0)
                {

                    Session["USER"] = txtUserName.Value.Trim();
                    Session["USERGROUPID"] = dt.Rows[0][2].ToString();
                    Session["USERID"] = dt.Rows[0][0].ToString();
                    Session["USERNAME"] = dt.Rows[0][1].ToString();
                    Session["PartnerID"] = dt.Rows[0]["PartnerID"].ToString();


                    //intLoginResult = 1;


                    //if (ds.Tables[2].Rows[0]["isnew"].ToString() == "1")
                    //{
                    //    Response.Redirect("Sys/ChangePw.aspx?acction=1");
                    //} 
                    //if (DateTime.Now.Day == 1 && ds.Tables[2].Rows[0]["dateChange"].ToString() == "0")
                    //{
                    //    Response.Redirect("Sys/ChangePw.aspx?acction=2");
                    //}
                    //else
                    //    Response.Redirect("default.aspx");


                    //VatLid.DAL.INSERT_USER_LOG_NEW(VatLid.Utils.getUserId(Session).ToString(), VatLid.LogType.LogIn.ToString(), VatLid.DAL.getCategoryID(FileName), "OK", "0", VatLid.Utils.GetIP());
                    Response.Redirect("default.aspx");
                }
                else
                {
                    Session.Abandon();
                    Session["USER"] = null;
                    Session["USERNAME"] = "";
                    lblMessage.Text = "Đăng nhập không thành công !";
                    VatLid.DAL.INSERT_USER_LOG_NEW(VatLid.Utils.getUserId(Session).ToString(), VatLid.LogType.LogIn.ToString(), VatLid.DAL.getCategoryID(FileName), "NOK", "0", VatLid.Utils.GetIP());
                    if (!Page.IsValid)
                        FormsAuthentication.RedirectFromLoginPage(txtUserName.Value, false);
                }

            }
            catch (Exception err)
            {
                VatLid.DAL.ExceptionProcess(err);
            }
            finally
            {
                Log_login(intLoginResult);
            }
        }
    }
}
