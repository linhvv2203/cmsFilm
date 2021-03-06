using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//using System.Web.Mail;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Net;
/// <summary>
/// Summary description for SendEmail
/// </summary>
/// 
namespace SendEmail_2
{
    public class SendMail
    {
        //
        // TODO: Add constructor logic here
        //
        public static bool send(  string sender, string to, string subject, string content)
        {

            MailMessage Email = new MailMessage();
            MailAddress MailFrom = new MailAddress("vudoan2@viettel.com.vn", "info@viettel.com.vn");
            Email.From = MailFrom;

            to = to.Replace("\r\n", "");
            to = to.Trim();
            Email.To.Add(to);
            
            // Smtp Client
            SmtpClient SmtpMail = new SmtpClient("smtp.viettel.com.vn", 465);

            SmtpMail.Port = 465;

            SmtpMail.Credentials = new NetworkCredential("vudoan2", "hungvuong!@@!");
            SmtpMail.EnableSsl = true;
            //SmtpMail.EnableSsl = false;

           
           // SmtpMail.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;            
            SmtpMail.Timeout = 20000;


            Email.Subject = subject;
            content = content ;

            Email.Body = content;
            Email.IsBodyHtml = true;
            Email.BodyEncoding = System.Text.Encoding.UTF8;                 

            try
            {
                SmtpMail.Send(Email);
                // log email, cat doan email tu cho Mọi thắc mắc xin liên hệ 1900...
                //content = content.Replace(VatLid.Constant.EmailFooter, "");
                int senttype = 2;
                InsertEmailLog( senttype.ToString(), sender, to, content);
                return true;
            }
            catch (SmtpFailedRecipientsException e)
            {
                return false;
            }
        }
         private static void InsertEmailLog( string senttype, string sender,string email, string info)
        {
            SqlParameter[] parameters = 
			{ 
               // new SqlParameter("@serviceid", SqlDbType.NVarChar ), //0
                new SqlParameter("@senttype", SqlDbType.NVarChar ),// 1
                new SqlParameter("@sender", SqlDbType.NVarChar ),//2
                new SqlParameter("@sentdate", SqlDbType.DateTime ),//3
                new SqlParameter("@email", SqlDbType.NVarChar ), //4
				new SqlParameter("@info", SqlDbType.NVarChar ), // 5
			};
           // parameters[0].Value = moduleid;
            parameters[0].Value = senttype;
            parameters[1].Value = sender;
            parameters[2].Value = DateTime.Now;
            parameters[3].Value = email;
            parameters[4].Value = info;
            try
            {
                VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "email_insert_info", parameters);
            }
            catch (Exception e)
            { 
            
            }
        }
        static void SmtpMail_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MailMessage mail = (MailMessage)e.UserState;
            string subject = mail.Subject;
            string ErrorMsg = "";
            if (e.Cancelled)
            {
                string cancelled = string.Format("[{0}] Send canceled.", subject);
                ErrorMsg = "Cancelled";
            }
            if (e.Error != null)
            {
                ErrorMsg = String.Format("[{0}] {1}", subject, e.Error.ToString());
            }
            else
            {
                ErrorMsg = "Message sent.";
            }
        }

        
    }
}