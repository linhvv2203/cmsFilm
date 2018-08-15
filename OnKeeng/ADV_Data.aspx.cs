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
using System.Data.SqlClient;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Configuration;
using VatLidOnPhim;
using Microsoft.WindowsAPICodePack.Shell;

public partial class OnKeeng_ADV_Data : System.Web.UI.Page
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
                        Label1.Text = "CẬP NHẬT QUẢNG CÁO.";
                        BindData();
                        break;
                    default:
                        Label1.Text = "THÊM MỚI QUẢNG CÁO.";
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
        ListItem liShow = new ListItem("--chọn vị trí --", "0");
        ddlPosition.Items.Add(liShow);
        liShow = new ListItem("--banner chi tiết--", "1");
        ddlPosition.Items.Add(liShow);
        liShow = new ListItem("--banner keeng tv--", "4");
        ddlPosition.Items.Add(liShow);
        ddlPosition.SelectedValue = "0";
    }
    private void BindData()
    {

        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@ID", id);

        DataSet ds;
        ds = DAL.GetDataSet("select_ADV_byID", DAL.getConnectionStringOnPhim(), cmd);

        try
        {

            if (ds != null)
            {
                //Label2.Text = HttpUtility.HtmlEncode(ds.Tables[0].Rows[0]["Name"].ToString());
                //Label2.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                txtName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                txtDesc.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                txtLnk.Text = ds.Tables[0].Rows[0]["LinkAd"].ToString();
                hiddenTime.Value = ds.Tables[0].Rows[0]["CreateDate"].ToString();
                ddlPosition.SelectedValue = ds.Tables[0].Rows[0]["Position"].ToString();
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
            lblError.Text = "Bạn phải nhập tiêu đề video";
            return;
        }
        if (ddlPosition.SelectedValue == "0")
        {
            lblError.Text = "Bạn cần chọn vị trí hiển thị";
            return;
        }

        if (((ImageFileImage.PostedFile != null) && (ImageFileImage.PostedFile.ContentLength != 0)))
        {
            if (ImageFileImage.PostedFile.ContentType != "image/jpeg" && ImageFileImage.PostedFile.ContentType != "image/pjpeg" && ImageFileImage.PostedFile.ContentType != "image/gif")
            {
                lblError.Text = "You don't upload type of file \"" + ImageFileImage.PostedFile.ContentType + "\". Only upload type of file .jpg in in File 1.";
                return;
            }

            string filename1 = ImageFileImage.PostedFile.FileName;
            int pos1 = filename1.LastIndexOfAny(new char[] { '/', '\\' });
            if (pos1 >= 0) filename1 = filename1.Substring(pos1 + 1);
        }

        if (TYPE == "edit")
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@Desc", txtDesc.Text.Trim());
            cmd.Parameters.AddWithValue("@LinkAd", txtLnk.Text.Trim());
            cmd.Parameters.AddWithValue("@Status", 1);
            cmd.Parameters.AddWithValue("@ID", VatLidOnPhim.Utils.safeString(id));
            cmd.Parameters.AddWithValue("@position", ddlPosition.SelectedValue);
            cmd.Parameters.AddWithValue("@DateStart", txtDateStart.Value);
            cmd.Parameters.AddWithValue("@DateEnd", txtDateEnd.Value);
            itemfile = getFileName(id);

            if (ImageFileImage.HasFile)
            {
                if (ImageFileImage.PostedFile != null && ImageFileImage.PostedFile.ContentLength > 0)
                {
                    cmd.Parameters.AddWithValue("@StatusIMG", 1);
                    byte[] pic1 = new byte[ImageFileImage.PostedFile.ContentLength];
                    ImageFileImage.PostedFile.InputStream.Read(pic1, 0, ImageFileImage.PostedFile.ContentLength);
                    addImageVideoToFileUpdate(id + ".jpg", pic1, Convert.ToDateTime(hiddenTime.Value));
                }
            }
            else
            {
                cmd.Parameters.AddWithValue("@StatusIMG", 0);
            }

            VatLidOnPhim.DAL.GetDataSet("update_ADV", VatLidOnPhim.DAL.getConnectionStringOnPhim(), cmd);

            lblError.Text = "Insert " + itemfile + " succeed.";
        }
        else  //Them moi
        {
            int InOutParamValue = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@Desc", txtDesc.Text.Trim());
            cmd.Parameters.AddWithValue("@LinkAd", txtLnk.Text.Trim());
            cmd.Parameters.AddWithValue("@Status", 1);
            cmd.Parameters.AddWithValue("@Position", ddlPosition.SelectedValue);
            cmd.Parameters.AddWithValue("@DateStart", txtDateStart.Value);
            cmd.Parameters.AddWithValue("@DateEnd", txtDateEnd.Value);
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
            cmd.Parameters["@ID"].Value = 0;
            cmd.Parameters["@ID"].Direction = ParameterDirection.Output;

            // Set Input Output Parameter
            //SqlParameter InOutParam = new SqlParameter("@ID", InOutParamValue);
            //InOutParam.SqlDbType = SqlDbType.Int;
            //InOutParam.Direction = ParameterDirection.InputOutput;
            //cmd.Parameters.Add(InOutParam);
            VatLidOnPhim.DAL.GetDataSet("insert_ADV", VatLidOnPhim.DAL.getConnectionStringOnPhim(), cmd);
            id = cmd.Parameters["@ID"].Value.ToString();
            //VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUserId(Session).ToString(), VatLidOnPhim.LogType.ADD.ToString(), VatLidOnPhim.DAL.getCategoryID(FileName), "OK", id.ToString(), VatLidOnPhim.Utils.GetIP());

            byte[] pic1 = new byte[ImageFileImage.PostedFile.ContentLength];
            ImageFileImage.PostedFile.InputStream.Read(pic1, 0, ImageFileImage.PostedFile.ContentLength);
            addImagePhim(id + ".jpg", pic1);


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
        string imdDBLocal = ConfigurationSettings.AppSettings["FilmPhotoDBPath_Banner"];
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

}
