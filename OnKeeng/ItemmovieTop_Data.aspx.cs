using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VatLidOnPhim;

public partial class OnKeeng_ItemmovieTop_Data : System.Web.UI.Page
{
    protected string id = "";
    protected string strCategoryID = "";
    protected string TYPE = "";
    protected int iStatus = 1;
    protected string SQL = "";
    protected DataView dv = null;
    protected int iCode = 0;
    protected string FileName = "";
    protected string strFileName1;
    protected string strFileName2;
    protected string strFileName3;
    protected string strPartnerID = "";
    protected int idcate = 0;
    protected int cid = 0;
    protected string status = "2";
    protected string itemcode = "";
    public static string itemfile = "";
    public static string folder = "";
    protected string itemdate = "";

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

        TYPE = Request.QueryString["type"];

        //strCategoryID = (Request.QueryString["CategoryID"] == null) ? "0" : Request.QueryString["CategoryID"];
        status = (Request.QueryString["status"] == null) ? "0" : Request.QueryString["status"];

        if (Utils.IsNumeric(strCategoryID))
        {
            strCategoryID = strCategoryID;
        }
        else
        {
            strCategoryID = "0";
        }

        if (Session["USER"] != null)
        {
            if (!Page.IsPostBack)
            {
                DAL.ResetToken(hiddenToken);
                bindDDL();
                switch (TYPE)
                {
                    case "edit":
                        Label1.Text = "CẬP NHẬT FLASH HOT";
                        BindData();
                        break;
                    default:
                        Label1.Text = "THÊM MỚI FLASH HOT";
                        hiddenTime.Value = "";
                        //txtUserName.Text = Session["USER"].ToString().ToUpper();
                        //ddlCategory.SelectedValue = strCategoryID;
                        break;
                }
            }
        }
        else
            Response.Redirect(VatLidOnPhim.Variables.sWebRoot + "logout.aspx");
    }
    private void bindDDL()
    {
        ListItem liShow = new ListItem("--Đã duyệt --", "2");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Chờ duyệt--", "1");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Xoá--", "0");
        ddlStatus.Items.Add(liShow);
        ddlStatus.SelectedValue = "2";
        //ListItem liShow = new ListItem("--chọn vị trí --", "0");
        //ddlPosition.Items.Add(liShow);
        //liShow = new ListItem("--banner chi tiết--", "1");
        //ddlPosition.Items.Add(liShow);
        //liShow = new ListItem("--banner keeng tv--", "4");
        //ddlPosition.Items.Add(liShow);
        //ddlPosition.SelectedValue = "0";
    }
    private void BindData()
    {

        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@id", id);

        DataSet ds;
        ds = DAL.GetDataSet("getItemMovieTop_byID", DAL.getConnectionStringOnPhim(), cmd);

        try
        {

            if (ds != null)
            {
                txtName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                txtLnk.Text = ds.Tables[0].Rows[0]["Url"].ToString();
                txtPosition.Text = ds.Tables[0].Rows[0]["Order_by"].ToString();
                inputDatetime.Value = ds.Tables[0].Rows[0]["DatePublish"].ToString();
                ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["status"].ToString();
            }


        }
        catch (Exception e)
        {
            lblError.Text = e.Message;
        }

    }
    protected void cmdSave_Click(object sender, EventArgs e)
    {
        if (!VatLidOnPhim.DAL.CheckPermissionByToken(hiddenToken))
        {
            VatLidOnPhim.DAL.ResetToken(hiddenToken);
            return;
        }
        if (txtName.Text.Length <= 1)
        {
            lblError.Text = "Bạn phải nhập tiêu đề flash hot";
            return;
        }

        if (((FUImageWeb.PostedFile != null) && (FUImageWeb.PostedFile.ContentLength != 0)))
        {
            if (FUImageWeb.PostedFile.ContentType != "image/jpeg" && FUImageWeb.PostedFile.ContentType != "image/pjpeg" && FUImageWeb.PostedFile.ContentType != "image/gif")
            {
                lblError.Text = "You don't upload type of file \"" + FUImageWeb.PostedFile.ContentType + "\". Only upload type of file .jpg in in File 1.";
                return;
            }

            string filename1 = FUImageWeb.PostedFile.FileName;
            int pos1 = filename1.LastIndexOfAny(new char[] { '/', '\\' });
            if (pos1 >= 0) filename1 = filename1.Substring(pos1 + 1);
        }
        else if (TYPE != "edit")
        {
            lblError.Text = "Bạn phải nhập flash hot WEB";
            return;
        }

        if (((FUImageWap.PostedFile != null) && (FUImageWap.PostedFile.ContentLength != 0)))
        {
            if (FUImageWap.PostedFile.ContentType != "image/jpeg" && FUImageWap.PostedFile.ContentType != "image/pjpeg" && FUImageWap.PostedFile.ContentType != "image/gif")
            {
                lblError.Text = "You don't upload type of file \"" + FUImageWap.PostedFile.ContentType + "\". Only upload type of file .jpg in in File 1.";
                return;
            }

            string filename1 = FUImageWap.PostedFile.FileName;
            int pos1 = filename1.LastIndexOfAny(new char[] { '/', '\\' });
            if (pos1 >= 0) filename1 = filename1.Substring(pos1 + 1);
        }
        else if(TYPE !="edit")
        {
            lblError.Text = "Bạn phải nhập flash hot WAP";
            return;
        }

        if (TYPE == "edit")
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@Link", txtLnk.Text.Trim());
            cmd.Parameters.AddWithValue("@position", txtPosition.Text.Trim());
            cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);
            cmd.Parameters.AddWithValue("@datepublish", inputDatetime.Value);
            cmd.Parameters.AddWithValue("@ID", VatLidOnPhim.Utils.safeString(id));
            itemfile = getFileName(id);

            if (FUImageWeb.HasFile)
            {
                if (FUImageWeb.PostedFile != null && FUImageWeb.PostedFile.ContentLength > 0)
                {
                    cmd.Parameters.AddWithValue("@imageWeb", 1);
                    string imgWeb = id + "_flash_web.jpg";
                    byte[] pic1 = new byte[FUImageWeb.PostedFile.ContentLength];
                    FUImageWeb.PostedFile.InputStream.Read(pic1, 0, FUImageWeb.PostedFile.ContentLength);
                    addImagePhim(imgWeb, pic1);
                }
            }
            else
            {
                cmd.Parameters.AddWithValue("@imageWeb", 0);
            }
            if (FUImageWap.HasFile)
            {
                if (FUImageWap.PostedFile != null && FUImageWap.PostedFile.ContentLength > 0)
                {
                    cmd.Parameters.AddWithValue("@imageWap", 1);
                    string imgWap = id + "_flash_wap.jpg";
                    byte[] pic1 = new byte[FUImageWap.PostedFile.ContentLength];
                    FUImageWap.PostedFile.InputStream.Read(pic1, 0, FUImageWap.PostedFile.ContentLength);
                    addImagePhim(imgWap, pic1);
                }
            }
            else
            {
                cmd.Parameters.AddWithValue("@imageWap", 0);
            }

            VatLidOnPhim.DAL.GetDataSet("update_ItemmovieTop", VatLidOnPhim.DAL.getConnectionStringOnPhim(), cmd);

            lblError.Text = "update " + itemfile + " succeed.";
        }
        else  //Them moi
        {
            int InOutParamValue = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@Url", txtLnk.Text.Trim());
            cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);
            cmd.Parameters.AddWithValue("@datepublish", inputDatetime.Value);
            cmd.Parameters.AddWithValue("@position", txtPosition.Text.Trim() == "" ? "0" : txtPosition.Text);
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
            cmd.Parameters["@ID"].Value = 0;
            cmd.Parameters["@ID"].Direction = ParameterDirection.Output;

            // Set Input Output Parameter
            //SqlParameter InOutParam = new SqlParameter("@ID", InOutParamValue);
            //InOutParam.SqlDbType = SqlDbType.Int;
            //InOutParam.Direction = ParameterDirection.InputOutput;
            //cmd.Parameters.Add(InOutParam);
            VatLidOnPhim.DAL.GetDataSet("Insert_ItemmovieTop", VatLidOnPhim.DAL.getConnectionStringOnPhim(), cmd);
            id = cmd.Parameters["@ID"].Value.ToString();
            //VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUserId(Session).ToString(), VatLidOnPhim.LogType.ADD.ToString(), VatLidOnPhim.DAL.getCategoryID(FileName), "OK", id.ToString(), VatLidOnPhim.Utils.GetIP());

            if (FUImageWeb.HasFile)
            {
                if (FUImageWeb.PostedFile != null && FUImageWeb.PostedFile.ContentLength > 0)
                {
                    string imgWeb = id + "_flash_web.jpg";
                    byte[] pic1 = new byte[FUImageWeb.PostedFile.ContentLength];
                    FUImageWeb.PostedFile.InputStream.Read(pic1, 0, FUImageWeb.PostedFile.ContentLength);
                    addImagePhim(imgWeb, pic1);
                    SyncImageFromServer(id, imgWeb);
                }
            }

            if (FUImageWap.HasFile)
            {
                if (FUImageWap.PostedFile != null && FUImageWap.PostedFile.ContentLength > 0)
                {
                    string imgWap = id + "_flash_wap.jpg";
                    byte[] pic1 = new byte[FUImageWap.PostedFile.ContentLength];
                    FUImageWap.PostedFile.InputStream.Read(pic1, 0, FUImageWap.PostedFile.ContentLength);
                    addImagePhim(imgWap, pic1);
                    SyncImageFromServer(id, imgWap);
                }
            }


            //itemfile = cmd.Parameters["@ItemFile1"].Value.ToString();
            lblError.Text = "Insert " + HttpUtility.HtmlEncode(txtName.Text.Trim()) + " succeed.";
        }
    }
    private string getFileName(string id)
    {
        string code = "";
        SqlConnection conn = null;
        try
        {
            conn = new SqlConnection(VatLidOnPhim.DAL.getConnectionStringOnPhim());
            conn.Open();
            SQL = "SELECT top(1) name FROM ADV where ID=" + Convert.ToInt32(id);
            ArrayList al = VatLidOnPhim.DAL.GetDataReaderToArrayList(SQL);
            string[][] arrReturn = (string[][])al.ToArray(typeof(string[]));
            code = arrReturn[0][0];

        }
        catch (Exception e)
        {
            VatLidOnPhim.DAL.ExceptionProcess(e);
        }
        finally
        {
            if (conn != null)
            {
                conn.Close();
            }
        }

        return code;
    }
    public void addImagePhim(string id, byte[] imgdata)
    {
        string imdDBLocal = ConfigurationSettings.AppSettings["FilmPhotoDBPath_FlashHot"];
        string folderDay = imdDBLocal + DateTime.Now.ToString("yyyy/MM/dd").Replace("/", ""); ;
        string filepath = folderDay + "/" + id.ToString();

        if (!Directory.Exists(folderDay))
        {
            Directory.CreateDirectory(folderDay);
        }


        if (File.Exists(filepath))
        {
            File.Delete(filepath);
        }
        FileStream fStream = new FileStream(filepath, FileMode.CreateNew);
        BinaryWriter bw = new BinaryWriter(fStream);
        bw.Write(imgdata);
        bw.Close();
        fStream.Close();
        fStream.Dispose();
        if (bw is IDisposable)
            ((IDisposable)bw).Dispose();
    }
    public void addImageVideoToFileUpdate(string id, byte[] imgdata, DateTime itemDate)
    {
        string imdDBLocal = ConfigurationSettings.AppSettings["FilmPhotoDBPath_Banner"];
        string imghis = imdDBLocal + itemDate.ToString("yyyy/MM/dd").Replace("/", "") + "/" + id;
        string folderDay = imdDBLocal + itemDate.ToString("yyyy/MM/dd").Replace("/", "");
        string filepath = folderDay + "/" + id.ToString();
        if (!Directory.Exists(folderDay))
        {
            Directory.CreateDirectory(folderDay);
        }
        if (File.Exists(filepath))
        {
            File.Delete(filepath);
        }
        if (File.Exists(imghis))
        {
            File.Delete(imghis);
        }

        FileStream fStream = new FileStream(filepath, FileMode.CreateNew);
        BinaryWriter bw = new BinaryWriter(fStream);
        bw.Write(imgdata);
        bw.Close();
        fStream.Close();
        fStream.Dispose();
        if (bw is IDisposable)
            ((IDisposable)bw).Dispose();
    }

    private void SyncImageFromServer(string code, string image)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@code", code);
        cmd.Parameters.AddWithValue("@image", image);
        VatLidOnPhim.DAL.GetDataSet("SyncImageFromServer", VatLidOnPhim.DAL.getConnectionStringOnPhim(), cmd);
    }
}