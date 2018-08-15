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

public partial class OnKeeng_ItemMovie_Groups_Data : System.Web.UI.Page
{
    protected string TYPE = "";
    protected string id = "";
    protected string SQL = "";

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

        TYPE = Request.QueryString["type"];
        id = Request.QueryString["id"];

        if (Session["USER"] != null)
        {
            if (!Page.IsPostBack)
            {
                VatLidOnPhim.DAL.ResetToken(hiddenToken);
                BindListCommandAdd();

                switch (TYPE)
                {
                    case "edit":
                        Label1.Text = "CẬP NHẬT FilmGroup.";
                        BindData();
                        break;
                    default:
                        Label1.Text = "THÊM MỚI FilmGroup.";
                        hiddenTime.Value = "";
                        //txtUserName.Text = Session["USER"].ToString().ToUpper();
                        ddlCategory.SelectedValue = "0";
                        break;
                }
            }
        }
        else
            Response.Redirect(VatLidOnPhim.Variables.sWebRoot + "logout.aspx");
    }

    private void BindListCommandAdd()
    {
        SQL = "Categories_ItemMovie WHERE Status=2 and ParentID in (0) order by id asc";
        VatLidOnPhim.DAL.FillDataToDropdownList(ddlCategory, SQL, "ID,CategoryName");
        ListItem liVideoCate = new ListItem("Chuyên mục", "0");
        ddlCategory.Items.Add(liVideoCate);

        SQL = "Partner_Keeng WHERE status=2 order by id asc";
        VatLidOnPhim.DAL.FillDataToDropdownList(ddlPartnerID, SQL, "ID,namePartner");
        ListItem liPartnerID = new ListItem("Đối tác", "0");
        ddlPartnerID.Items.Add(liPartnerID);
    }
    protected void BindData()
    {
        DataSet ds = null;

        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@id", id);

        ds = VatLidOnPhim.DAL.GetDataSet("CMS_filmGroups_getByID", cmd);

        if (ds.Tables[0].Rows.Count > 0)
        {
            txtName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
            txtTotalFilm.Text = ds.Tables[0].Rows[0]["total"].ToString();
            txtDesc.Text = ds.Tables[0].Rows[0]["ItemDesc"].ToString();
            ddlPartnerID.SelectedValue = ds.Tables[0].Rows[0]["partnerid"].ToString();
            ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["categoryid"].ToString();
            HiddenField1.Value = ds.Tables[0].Rows[0]["DateCreate"].ToString();
            inputDatetime.Value = ds.Tables[0].Rows[0]["DatePublish"].ToString();

            imagedd.ImageUrl = (VatLidOnPhim.Variables.sVideoPathPlay + "/" + ds.Tables[0].Rows[0]["FolderName"].ToString() + "/" + ds.Tables[0].Rows[0]["ItemImage"].ToString()).ToString();
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
        if (ddlCategory.SelectedValue == "0")
        {
            lblError.Text = "Bạn phải nhập chuyên mục";
            return;
        }
        //if ((TYPE != "edit") && getTitle(txtName.Text.Trim()) == "1")
        //{
        //    lblError.Text = "Video đã nhập trùng. Mời bạn nhập tiêu đề khác";
        //    return;
        //}

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
            cmd.Parameters.AddWithValue("@ItemName", VatLidOnPhim.Utils.safeString(txtName.Text.Trim()));
            cmd.Parameters.AddWithValue("@total", VatLidOnPhim.Utils.safeString(txtTotalFilm.Text.Trim()));
            cmd.Parameters.AddWithValue("@ItemDesc", VatLidOnPhim.Utils.safeString(txtDesc.Text.Trim()));
            cmd.Parameters.AddWithValue("@partnerID", VatLidOnPhim.Utils.safeString(ddlPartnerID.SelectedValue));
            cmd.Parameters.AddWithValue("@categoryID", VatLidOnPhim.Utils.safeString(ddlCategory.SelectedValue));
            cmd.Parameters.AddWithValue("@ID", VatLidOnPhim.Utils.safeString(id));
            cmd.Parameters.AddWithValue("@datePublish", inputDatetime.Value.Trim());

            if (ImageFileImage.HasFile)
            {
                //parameters[8].Value = 1;
                cmd.Parameters.AddWithValue("@status", 2);
                byte[] pic1 = new byte[ImageFileImage.PostedFile.ContentLength];
                ImageFileImage.PostedFile.InputStream.Read(pic1, 0, ImageFileImage.PostedFile.ContentLength);
                addImageVideoToFileUpdate(id + ".jpg", pic1, Convert.ToDateTime(HiddenField1.Value));
            }
            else
            {
                cmd.Parameters.AddWithValue("@status", 0);
            }
            VatLidOnPhim.DAL.GetDataSet("CMS_update_FilmGroups", VatLidOnPhim.DAL.getConnectionStringOnPhim(), cmd);

            lblError.Text = "update success.";
        }
        else  //Them moi
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@ItemName", VatLidOnPhim.Utils.safeString(txtName.Text.Trim()));
            cmd.Parameters.AddWithValue("@total", VatLidOnPhim.Utils.safeString(txtTotalFilm.Text.Trim()));
            cmd.Parameters.AddWithValue("@ItemDesc", VatLidOnPhim.Utils.safeString(txtDesc.Text.Trim()));
            cmd.Parameters.AddWithValue("@partnerID", VatLidOnPhim.Utils.safeString(ddlPartnerID.SelectedValue));
            cmd.Parameters.AddWithValue("@categoryID", VatLidOnPhim.Utils.safeString(ddlCategory.SelectedValue));
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 10));
            cmd.Parameters["@ID"].Direction = ParameterDirection.Output;
            cmd.Parameters["@ID"].Value = 0;
            cmd.Parameters.AddWithValue("@datePublish", inputDatetime.Value.Trim());

            VatLidOnPhim.DAL.GetDataSet("CMS_insert_FilmGroups", VatLidOnPhim.DAL.getConnectionStringOnPhim(), cmd);

            id = cmd.Parameters["@ID"].Value.ToString();

            //VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUserId(Session).ToString(), VatLidOnPhim.LogType.ADD.ToString(), VatLidOnPhim.DAL.getCategoryID(FileName), "OK", id.ToString(), VatLidOnPhim.Utils.GetIP());

            lblError.Text = "Insert " + txtName.Text.Trim() + " succeed.";

            if (ImageFileImage.PostedFile != null && ImageFileImage.PostedFile.ContentLength > 0)
            {
                byte[] pic1 = new byte[ImageFileImage.PostedFile.ContentLength];
                ImageFileImage.PostedFile.InputStream.Read(pic1, 0, ImageFileImage.PostedFile.ContentLength);
                addImageVideo(id + ".jpg", pic1);

            }
        }
    }

    private void addImageVideo(string id, byte[] imgdata)
    {
        string imdDBLocal = ConfigurationSettings.AppSettings["FilmPhotoDBPath"];
        //string imghis = imdDBLocal + itemdate + "/" + id + ".jpg";
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
        //if (File.Exists(imghis))
        //{
        //    File.Delete(imghis);
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

    public void addImageVideoToFileUpdate(string id, byte[] imgdata, DateTime itemDate)
    {
        string imdDBLocal = ConfigurationSettings.AppSettings["FilmPhotoDBPath"];
        string imghis = imdDBLocal + itemDate.ToString("yyyy/MM/dd").Replace("/", "") + "/" + id;
        string folderDay = imdDBLocal + itemDate.ToString("yyyy/MM/dd").Replace("/", "");
        string filepath = folderDay + "/" + id.ToString();
        //test += "3. " + filepath + " - ";
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
