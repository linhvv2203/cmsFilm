using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for SendEmail
/// </summary>
/// 
namespace SendEmail
{
    public class SendEmail
    {
        //
        // TODO: Add constructor logic here
        //
        public static bool send(string mailto, string subject, string content)
        {
            System.Web.Mail.MailMessage oMessage = new System.Web.Mail.MailMessage();
            oMessage.To = mailto;
            oMessage.From = "cskhmedia@viettel.com.vn";
            oMessage.Subject = subject;
            oMessage.Body = content;
            oMessage.BodyEncoding = System.Text.Encoding.UTF8; 
            oMessage.Fields["http://schemas.microsoft.com/cdo/configuration/sendusing"] = 2;
            oMessage.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserver"] = "tuanbm";
            //tren server :            
            System.Web.Mail.SmtpMail.SmtpServer = "tuanbm";
            //ding dang
            oMessage.BodyFormat = System.Web.Mail.MailFormat.Html;
            

            try
            {
                System.Web.Mail.SmtpMail.Send(oMessage);
                //Console.Write(" Gui email thanh cong : ");
                return true;
            }
            catch (Exception Ex)
            {
                //Console.Write(" Loi : " + Ex);
                return false;
            }
        }
        public static bool send(string mailto, string subject, string content, string fileAttach)
        {
            System.Web.Mail.MailMessage oMessage = new System.Web.Mail.MailMessage();
            oMessage.To = mailto;
            oMessage.From = "cskh@viettelmedia.vn";
            oMessage.Subject = subject;
            oMessage.Body = content;
            oMessage.BodyEncoding = System.Text.Encoding.UTF8; 
            oMessage.Fields["http://schemas.microsoft.com/cdo/configuration/sendusing"] = 2;
            oMessage.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserver"] = "tuanbm";
            //dinh kem
            System.Web.Mail.MailAttachment myAttachment = new System.Web.Mail.MailAttachment(fileAttach, System.Web.Mail.MailEncoding.Base64);
            oMessage.Attachments.Add(myAttachment);
            //tren server
            System.Web.Mail.SmtpMail.SmtpServer = "tuanbm";
            //dinh dang
            //oMessage.BodyFormat = System.Web.Mail.MailFormat.Html;

            try
            {
                System.Web.Mail.SmtpMail.Send(oMessage);
                //Console.Write(" Gui email thanh cong : ");
                return true;
            }
            catch (Exception Ex)
            {
                //Console.Write(" Loi : " + Ex);
                return false;
            }
        }
    }
}