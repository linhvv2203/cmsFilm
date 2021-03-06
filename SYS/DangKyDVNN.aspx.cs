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
using System.Data.SqlClient;

public partial class SYS_DangKyDVNN : System.Web.UI.Page
{
    protected string sFilterCategory = "";
    protected string SQL = "";
    protected DataView dv = null;
    protected string CategoryID = "";
    protected string FileName = "";
    protected string status = "";
  
    protected void Page_Load(object sender, EventArgs e)
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
    }
    protected void cmdSave_Click(object sender, EventArgs e)
    {
        
        String PhoneNumber ="";
        String info ="";
        String Services ="";
        if (txtPhoneNumber.Text.StartsWith("84") == false)
        {
            if (txtPhoneNumber.Text.StartsWith("0") == true)
                PhoneNumber = "84" + txtPhoneNumber.Text.Substring(1);
        }
        else
        {
            PhoneNumber = txtPhoneNumber.Text.Substring(1);
        }

       
        info = VatLid.Utils.safeString(txtInfo.Text.Trim());
        Services = VatLid.Utils.safeString(txtService.Text.Trim());


        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@UserID", PhoneNumber);
        cmd.Parameters.AddWithValue("@Info", info);
        cmd.Parameters.AddWithValue("@ServiceID", Services);
        VatLid.DAL.GetDataSet("DKDV", VatLid.DAL.getConnectionString1(), cmd);
        VatLid.DAL.INSERT_USER_LOG_NEW(VatLid.Utils.getUserId(Session).ToString(), VatLid.LogType.DKDV.ToString(), VatLid.DAL.getCategoryID(FileName), "OK", PhoneNumber, VatLid.Utils.GetIP());
        lblError.Text = "Đăng ký dịch vụ: " + info + " cho số đt: " + PhoneNumber + " thành công";

        clearDate();
    }

    protected void clearDate()
    {
        txtPhoneNumber.Text = "";
        txtService.Text = "";
        txtInfo.Text = "";
    }
}
