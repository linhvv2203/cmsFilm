using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VatLidOnPhim;

public partial class OnKeeng_ItemmovieIntro_Add : System.Web.UI.Page
{
    protected string id = "";
    protected string TYPE = "";
    protected string itemFile = "";

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
        TYPE = Request.QueryString["type"];

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
                switch (TYPE)
                {
                    case "edit":
                        Label1.Text = "CẬP NHẬT PHIM.";
                        BindData();
                        break;
                    default:
                        Label1.Text = "THÊM MỚI PHIM.";
                        break;
                }
            }
        }
        else
            Response.Redirect(VatLidOnPhim.Variables.sWebRoot + "logout.aspx");
    }
    private void BindData()
    {
        try
        {
            SqlParameter[] parameters = { 
            new SqlParameter("@ID", SqlDbType.Int),																																					
        };
            parameters[0].Value = Convert.ToInt32(id);
            DataSet ds;
            ds = VatLidOnPhim.SqlHelper.ExecuteDataset(VatLidOnPhim.DAL.getConnectionStringOnPhim(), CommandType.StoredProcedure, "getItemMovie_intro_byID", parameters);

            txtName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
            txtDesc.Text = ds.Tables[0].Rows[0]["Description"].ToString();

        }
        catch
        {
        }

    }
    protected void cmdSave_Click(object sender, EventArgs e)
    {

        if (txtName.Text.Length <= 1)
        {
            lblError.Text = "Bạn phải nhập tiêu đề video";
            return;
        }

        if (ImageFileMp4.PostedFile == null || ImageFileMp4.PostedFile.ContentLength <= 0 || !ImageFileMp4.PostedFile.FileName.Contains(".mp4"))
        {

            lblError.Text = "Bạn phải nhập video intro (mp4)";
            return;
        }

        if (ImageFileMp4.PostedFile == null || ImageFileMp4.PostedFile.ContentLength <= 0)
        {
            lblError.Text = "Bạn phải nhập file";
            return;
        }

        if (TYPE == "edit")
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@Description", txtDesc.Text.Trim());
            cmd.Parameters.Add(new SqlParameter("@ItemFile", SqlDbType.NVarChar, 100));
            cmd.Parameters["@ItemFile"].Direction = ParameterDirection.Output;
            cmd.Parameters["@ItemFile"].Value = 100;

            VatLidOnPhim.DAL.GetDataSet("cms_insert_ItemMovie_Intro", cmd);

            itemFile = cmd.Parameters["@ItemFile"].Value.ToString();
            if (itemFile != "" && ImageFileMp4.PostedFile != null && ImageFileMp4.PostedFile.ContentLength > 0)
            {
                byte[] mp4 = new byte[ImageFileMp4.PostedFile.ContentLength];
                ImageFileMp4.PostedFile.InputStream.Read(mp4, 0, ImageFileMp4.PostedFile.ContentLength);
                string folderDay = DateTime.Now.ToString("yyyy/MM/dd").Replace("/", "");
                addMp4Phim(itemFile + ".mp4", mp4);
            }
            lblError.Text = "Upload success";
        }
        else  //Them moi
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@Description", txtDesc.Text.Trim());
            cmd.Parameters.Add(new SqlParameter("@ItemFile", SqlDbType.NVarChar, 100));
            cmd.Parameters["@ItemFile"].Direction = ParameterDirection.Output;
            cmd.Parameters["@ItemFile"].Value = 100;

            VatLidOnPhim.DAL.GetDataSet("cms_insert_ItemMovie_Intro", cmd);

            itemFile = cmd.Parameters["@ItemFile"].Value.ToString();


            if (itemFile != "" && ImageFileMp4.PostedFile != null && ImageFileMp4.PostedFile.ContentLength > 0)
            {
                byte[] mp4 = new byte[ImageFileMp4.PostedFile.ContentLength];
                ImageFileMp4.PostedFile.InputStream.Read(mp4, 0, ImageFileMp4.PostedFile.ContentLength);
                string folderDay = DateTime.Now.ToString("yyyy/MM/dd").Replace("/", "");
                addMp4Phim(itemFile + ".mp4", mp4);
            }
            lblError.Text = "Upload success";

        }

        BindData();
    }

    public void addMp4Phim(string id, byte[] imgdata)
    {

        string imdDBLocal = ConfigurationSettings.AppSettings["IntroFilmDBPath"];
        //string videohis = imdDBLocal + itemdate + "/" + itemFile + ".mp4";
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
        //if (File.Exists(videohis))
        //{
        //    File.Delete(videohis);
        //}
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