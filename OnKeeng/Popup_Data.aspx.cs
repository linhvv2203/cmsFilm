using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.IO;
using VatLidOnPhim;

public partial class OnKeeng_Popup_Data : System.Web.UI.Page
{
    protected string FileName = "";
    protected string itemfile = "";
    protected string SQL = "";
    protected string id = "";
    protected string TYPE = "";
    public static string file_image = "";
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

        id = Request.QueryString["id"];
        if (Utils.IsNumeric(id))
        {
            id = id;
        }
        else
        {
            id = "0";
        }

        if (Session["USER"] != null)
        {
            if (!Page.IsPostBack)
            {

                VatLidOnPhim.DAL.ResetToken(hiddenToken);
                BinData();
                switch (id)
                {
                    case "0":
                        Label1.Text = "THÊM MỚI Popup.";
                        hiddenTime.Value = "";
                        break;
                    default:
                        Label1.Text = "CẬP NHẬT Popup.";
                        Load_Data(id);
                        break;
                }
            }
        }
        else
            Response.Redirect(VatLidOnPhim.Variables.sWebRoot + "logout.aspx");
    }
    protected void BinData()
    {
        //VatLidOnPhim.DAL.FillDataToDropdownListStore(ddldt, "CMS_DropDownList_Partner", "id,namepartner", Session["PartnerID"].ToString());
        // ddldt.SelectedValue = "1";
    }
    protected void Load_Data(string id)
    {

        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@ID", id);

        DataSet ds;
        ds = DAL.GetDataSet("Pop&Select", DAL.getConnectionStringOnPhim(), cmd);

        try
        {
            txtName.Text = ds.Tables[0].Rows[0]["NamePop"].ToString();
            txtContent.Text = ds.Tables[0].Rows[0]["ContentPop"].ToString();
            txtLink.Text = ds.Tables[0].Rows[0]["LinkPop"].ToString();
            txtPosition.Text = ds.Tables[0].Rows[0]["PositionPop"].ToString();
        }
        catch (Exception e)
        {
            lblError.Text = Server.HtmlEncode(e.Message);
        }

    }
    protected void cmdSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!VatLidOnPhim.DAL.CheckPermissionByToken(hiddenToken))
            {
                VatLidOnPhim.DAL.ResetToken(hiddenToken);
                return;
            }
            if (id != "0")
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@id", Utils.safeString(id));
                cmd.Parameters.AddWithValue("@NamePop", Utils.safeString(txtName.Text.Trim()));
                cmd.Parameters.AddWithValue("@ContentPop", Utils.safeString(txtContent.Text.Trim()));
                cmd.Parameters.AddWithValue("@LinkPop", Utils.safeString(txtLink.Text.Trim()));
                cmd.Parameters.AddWithValue("@PositionPop", Utils.safeString(txtPosition.Text.Trim()));
                cmd.Parameters.AddWithValue("@Status", 1);               


                DAL.GetDataSet("Pop&Sync", DAL.getConnectionStringOnPhim(), cmd);
                VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUserId(Session).ToString(), VatLidOnPhim.LogType.UPDATE.ToString(), VatLidOnPhim.DAL.getCategoryID(FileName), "OK", id.ToString(), VatLidOnPhim.Utils.GetIP());
                lblError.Text = Server.HtmlEncode("ĐÃ SỬA THÀNH CÔNG!");
                Load_Data(id);
            }

            else
            {
                
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@id", Utils.safeString(id));
                cmd.Parameters.AddWithValue("@NamePop", Utils.safeString(txtName.Text.Trim()));
                cmd.Parameters.AddWithValue("@ContentPop", Utils.safeString(txtContent.Text.Trim()));
                cmd.Parameters.AddWithValue("@LinkPop", Utils.safeString(txtLink.Text.Trim()));
                cmd.Parameters.AddWithValue("@PositionPop", Utils.safeString(txtPosition.Text.Trim()));
                cmd.Parameters.AddWithValue("@Status", 1);   

               
                DataSet ds = DAL.GetDataSet("Pop&Sync", DAL.getConnectionStringOnPhim(), cmd);
                VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUserId(Session).ToString(), VatLidOnPhim.LogType.ADD.ToString(), VatLidOnPhim.DAL.getCategoryID(FileName), "OK", id.ToString(), VatLidOnPhim.Utils.GetIP());
                lblError.Text = Server.HtmlEncode("ĐÃ THÊM THÀNH CÔNG!");
            }
        }
        catch
        {

        }
    }
  
}
