using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using VatLid;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace HA.Sysadmin
{
	
	public partial class ChangePw : System.Web.UI.Page
	{
        protected string FileName;
        protected string acction = "";

		protected void Page_Load(object sender, System.EventArgs e)
		{
            #region:"check permission to access system and also permission to access system function"
            string P_I = Request.ServerVariables["PATH_INFO"];
            string[] aPI = P_I.Split('/');
            int iLength = aPI.Length;
            string FileName = aPI[iLength - 1];
            if (Session["USER"] != null)
            {
                if (VatLid.DAL.GetRights(Session["USER"].ToString(), FileName) == false)
                {
                    Response.Redirect(VatLid.Variables.sWebRoot + "error_info.aspx?err=21");
                }
            }
            else
            {
                Response.Redirect(VatLid.Variables.sWebRoot + "logout.aspx");
            }
            #endregion

            acction = (Request.QueryString["acction"] == null) ? "0" : Request.QueryString["acction"];
            if (Utils.IsNumeric(acction))
            {
                acction = acction;
            }
            else
            {
                acction = "0";
            }


            if (DateTime.Now.Day == 1)
            {
                Label1.Text = "Mật khẩu bạn hết hạn sự dụng! Bạn phải đổi lại mật khẩu mới!";
            }

            if (acction == "1")
            {
                Label1.Text = "Đổi lại mật khẩu cho lần đăng nhập đầu tiên!";
            }
            if (acction == "2")
            {
                Label1.Text = "Mật khẩu bạn hết hạn sử dụng. Bạn đổi lại mật khẩu!";
            }        

			if(Session["USER"]==null)
                Response.Redirect(VatLid.Variables.sWebRoot + "login.aspx");
		}

        public bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);


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

        protected override void OnError(EventArgs e)
        {

            if (typeof(System.Web.HttpRequestValidationException) == Server.GetLastError().GetType())
            {
                string strErrMsg = Server.GetLastError().Message;

                VatLid.DAL.ExceptionProcess1(strErrMsg);

                Response.Redirect(VatLid.Variables.sWebRoot + "error_info.aspx?err=33");

            }


            if (typeof(System.Web.HttpException) == Server.GetLastError().GetType())
            {
                string strErrMsg = Server.GetLastError().Message;

                VatLid.DAL.ExceptionProcess1(strErrMsg);

                Response.Redirect(VatLid.Variables.sWebRoot + "error_info.aspx?err=34");

            }


            base.OnError(e);
        }



		protected void cmdSave_Click(object sender, System.EventArgs e)
		{
            DataSet ds;
			try
			{
                string Pass = txtOldPw.Text;
                if (Pass.Length > 0)
                {
                    ds = VatLid.DAL.UserLogin(Utils.safeString(Session["USER"].ToString()), SaltedHash.EncodeMD5(Pass));
                    if (ds.Tables[0].Rows.Count == 0)
                        Response.Redirect(VatLid.Variables.sWebRoot + "error_info.aspx?err=27");


                    else
                    {
                        if (txtNewPw.Text != txtConfirmPw.Text)
                            Response.Redirect(VatLid.Variables.sWebRoot + "error_info.aspx?err=26");
                        else if (txtNewPw.Text == txtOldPw.Text)
                            Response.Redirect(VatLid.Variables.sWebRoot + "error_info.aspx?err=37");
                        else if (txtNewPw.Text.Length < 8)
                        {
                            Response.Redirect(VatLid.Variables.sWebRoot + "error_info.aspx?err=31");
                        }
                        else if (IsNumber(txtNewPw.Text))
                        {
                            Response.Redirect(VatLid.Variables.sWebRoot + "error_info.aspx?err=31");
                        }
                        else if (!checkMumber(txtNewPw.Text))
                        {
                            Response.Redirect(VatLid.Variables.sWebRoot + "error_info.aspx?err=31");
                        }

                        else if (!Utils.isSpecial_Characters(txtNewPw.Text))
                        {
                            Response.Redirect(VatLid.Variables.sWebRoot + "error_info.aspx?err=31");
                        }

                        else
                        {
                            SqlCommand cmd = new SqlCommand();
                            cmd.Parameters.AddWithValue("@UserName", Utils.safeString(Session["USER"].ToString()));
                            cmd.Parameters.AddWithValue("@UserPw", SaltedHash.EncodeMD5(txtNewPw.Text.Trim()));
                            cmd.Parameters.AddWithValue("@UserPwN", txtNewPw.Text.Trim());
                            VatLid.DAL.GetDataSet("cms_changepass", VatLid.DAL.getConnectionString1(), cmd);


                            VatLid.DAL.INSERT_USER_LOG_NEW(VatLid.Utils.getUserId(Session).ToString(), VatLid.LogType.ChangerPw.ToString(), VatLid.DAL.getCategoryID(FileName), "OK", Session["USER"].ToString(), VatLid.Utils.GetIP());
                            Response.Redirect(VatLid.Variables.sWebRoot + "error_info.aspx?err=23");

                        }
                    }

                }
                else
                {
                    lblError.Text = "Thay Pass không thành công";
                }
                //string SQL="SELECT ID From Users WHERE UserName='" +  Utils.safeString(Session["USER"].ToString())+"' AND UserPw='" + SaltedHash.EncodeMD5(txtOldPw.Text) + "'";
                //ArrayList al=DAL.GetDataReaderToArrayList(SQL);
                //if(al.Count==0)
                //    Response.Redirect(VatLid.Variables.sWebRoot + "error_info.aspx?err=27");
                //else
                //    if (txtNewPw.Text != txtConfirmPw.Text)
                //    {
                //        Response.Redirect(VatLid.Variables.sWebRoot + "error_info.aspx?err=26");
                //    }

                //    else if (txtNewPw.Text.Length < 8)
                //    {
                //        Response.Redirect(VatLid.Variables.sWebRoot + "error_info.aspx?err=30");
                //    }
                //    else if (IsNumber(txtNewPw.Text))
                //    {
                //        Response.Redirect(VatLid.Variables.sWebRoot + "error_info.aspx?err=31");
                //    }
                //    else if (!checkMumber(txtNewPw.Text))
                //    //  if (!checkMumber(pass))
                //    {
                //        Response.Redirect(VatLid.Variables.sWebRoot + "error_info.aspx?err=32");
                //    }


                //    else
                //    {
                //        //txtNewPw.Text=txtNewPw.Text.Replace("'","''").ToString();
                //        //SQL="UPDATE Users SET UserPw='" + SaltedHash.EncodeMD5(txtNewPw.Text) + "'";
                //        //SQL+=" WHERE UserName='" + Session["USER"]+"'";
                //        //DAL.ExecuteQuery(SQL);

                //        SqlCommand cmd = new SqlCommand();
                //        cmd.Parameters.AddWithValue("@UserName", Utils.safeString(Session["USER"].ToString()));
                //        cmd.Parameters.AddWithValue("@UserPw", SaltedHash.EncodeMD5(txtNewPw.Text.Trim()));
                //        cmd.Parameters.AddWithValue("@UserPwN", txtNewPw.Text.Trim());
                //        VatLid.DAL.GetDataSet("cms_changepass", VatLid.DAL.getConnectionString1(), cmd);


                //        VatLid.DAL.INSERT_USER_LOG_NEW(VatLid.Utils.getUserId(Session).ToString(), VatLid.LogType.ChangerPw.ToString(), VatLid.DAL.getCategoryID(FileName), "OK", Session["USER"].ToString(), VatLid.Utils.GetIP());
                //        Response.Redirect(VatLid.Variables.sWebRoot + "error_info.aspx?err=23");
                //    }
			}
			catch(Exception err)
			{
				VatLid.DAL.ExceptionProcess(err);			
			}

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
	}
}
