using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using VatLid;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class SYS_ResetPw : System.Web.UI.Page
{
    protected string FileName;
    protected void Page_Load(object sender, EventArgs e)
    {
        string P_I = Request.ServerVariables["PATH_INFO"];
        string[] aPI = P_I.Split('/');
        int iLength = aPI.Length;
        FileName = aPI[iLength - 1];

        if (Session["USER"] == null)
            Response.Redirect(VatLid.Variables.sWebRoot + "login.aspx");
    }
    protected void cmdSave_Click(object sender, EventArgs e)
    {
        try
        {
            string user = VatLid.Utils.KillChars(txtName.Text.Trim());            
            user = VatLid.Utils.ValidateXSS(user);
            user = VatLid.Utils.safeString(user);

            string passNew = txtNewPw.Text.Trim();
          

            string passConfirm = txtConfirmPw.Text.Trim();
          
            if (txtNewPw.Text.Length < 8)
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

            string SQL = "SELECT ID From Users WHERE UserName='" + user + "'" ;
            ArrayList al = DAL.GetDataReaderToArrayList(SQL);
            if (al.Count == 0)
                lblError.Text = "User không tồn tại trong CSDL";
            else
                if (txtNewPw.Text != txtConfirmPw.Text)
                    lblError.Text = "Mật khẩu mới Confirm sai";
                else
                {
                  
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@UserName", user);
                    cmd.Parameters.AddWithValue("@UserPw", SaltedHash.EncodeMD5(passNew));
                    cmd.Parameters.AddWithValue("@UserPwN", passNew);
                    VatLid.DAL.GetDataSet("cms_changepass", VatLid.DAL.getConnectionString1(), cmd);
                    VatLid.DAL.INSERT_USER_LOG_NEW(VatLid.Utils.getUserId(Session).ToString(), VatLid.LogType.ChangerPw.ToString(), VatLid.DAL.getCategoryID(FileName), "OK", user, VatLid.Utils.GetIP());
                    lblError.Text = "Đổi mật khẩu thành công";
                    
                }
        }
        catch (Exception err)
        {
            lblError.Text = "Đổi mật khẩu không thành công";
            VatLid.DAL.ExceptionProcess(err);
        }
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


}
