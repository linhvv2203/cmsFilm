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
using MediaInfoLib;

public partial class OnKeeng_ItemMovieGroup_addPartByitemovieID : System.Web.UI.Page
{
    protected string id = "";
    protected string itemFile = "";
    protected string SQL = "";
    protected string id_group = "";

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

        if (!Page.IsPostBack)
        {
            txtUserName.Text = Session["USER"].ToString().ToUpper();
            txtUserName.Enabled = false;
            ddlMovie.Enabled = false;

            load_ddlMovie();
            BindDataGrid();

            if (id != "")
            {
                ddlMovie.ClearSelection();
                ddlMovie.Items.FindByValue(id).Selected = true;
            }
        }

    }

    protected void cmdSave_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@Userupload", txtUserName.Text.Trim().ToUpper());
        cmd.Parameters.AddWithValue("@ItemName", txtNamePart.Text.Trim().ToUpper());
        cmd.Parameters.AddWithValue("@Episode", txtepisode.Text.Trim());
        cmd.Parameters.AddWithValue("@GroupID", ddlMovie.SelectedValue);
        cmd.Parameters.Add(new SqlParameter("@ItemFile1", SqlDbType.NVarChar, 100));
        cmd.Parameters["@ItemFile1"].Direction = ParameterDirection.Output;
        cmd.Parameters["@ItemFile1"].Value = 100;
        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.NVarChar, 10));
        cmd.Parameters["@ID"].Direction = ParameterDirection.Output;
        cmd.Parameters["@ID"].Value = 10;

        VatLidOnPhim.DAL.GetDataSet("CMS_Itemmoviesgroup_Insert_CP", cmd);

        itemFile = cmd.Parameters["@ItemFile1"].Value.ToString();
        id_group = cmd.Parameters["@ID"].Value.ToString();

        if (itemFile != "" && ImageFileMp4.PostedFile != null && ImageFileMp4.PostedFile.ContentLength > 0)
        {
            byte[] mp4 = new byte[ImageFileMp4.PostedFile.ContentLength];
            ImageFileMp4.PostedFile.InputStream.Read(mp4, 0, ImageFileMp4.PostedFile.ContentLength);
            string folderDay = DateTime.Now.ToString("yyyy/MM/dd").Replace("/", ""); ;
            addMp4Phim(itemFile + ".mp4", mp4);

            //check file format video
            CheckBitrate(itemFile);
        }
        //lblError.Text = "Upload success";
    }

    private void load_ddlMovie()
    {
        VatLidOnPhim.DAL.FillDataToDropdownListStore(ddlMovie, "CMS_DropDownList_Itemmovie", "id,itemname", "1=1");
        ListItem liVideoCateLBS = new ListItem("==Tập Phim ==", "0");
        ddlMovie.Items.Add(liVideoCateLBS);
        ddlMovie.SelectedValue = "0";

        //ListItem lstLoaiLogo = new ListItem("---chọn logo---", "0");
        //ddlLogo.Items.Add(lstLoaiLogo);
        //lstLoaiLogo = new ListItem("Logo keeng", "http://media3.onbox.vn:8088/phimonbox/images/logo/keeng_logo.png");
        //ddlLogo.Items.Add(lstLoaiLogo);
        //lstLoaiLogo = new ListItem("Logo QPVN", "http://media3.onbox.vn:8088/phimonbox/images/logo/QPVN_logo.png");
        //ddlLogo.Items.Add(lstLoaiLogo);

        //ListItem lstPositionLogo = new ListItem("---Chọn vị trí logo---", "0");
        //ddlPosition.Items.Add(lstPositionLogo);
        //lstPositionLogo = new ListItem("Left", "Left");
        //ddlPosition.Items.Add(lstPositionLogo);
        //lstPositionLogo = new ListItem("Right", "Right");
        //ddlPosition.Items.Add(lstPositionLogo);
    }
    public void addMp4Phim(string id, byte[] imgdata)
    {

        string imdDBLocal = ConfigurationSettings.AppSettings["FilmDBPath"];
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

    public void CheckBitrate(string itemfile)
    {
        try
        {
            string imdDBLocal = ConfigurationSettings.AppSettings["FilmDBPath"];
            string folderDay = imdDBLocal + DateTime.Now.ToString("yyyy/MM/dd").Replace("/", ""); ;
            string filepath = folderDay + "/" + itemfile.ToString() + ".mp4";
            lblError.Text = "";

            if (File.Exists(filepath))
            {
                MediaInfo mi = new MediaInfo();
                mi.Open(filepath);

                string infoFile = mi.Inform();
                string[] infoFileArr = infoFile.Split(':');
                bool profile = false;

                for (int i = 0; i < infoFileArr.Length; i++)
                {
                    if (infoFileArr[i].ToString().IndexOf("High@") >= 0 || infoFileArr[i].ToString().IndexOf("Main@L") >= 0)
                    {
                        profile = true;
                        break;
                    }
                }

                VideoInfo videoInfo = new VideoInfo(mi);

                if (videoInfo.Bitrate / 1024 > 2000)
                {
                    lblError.Text = "Bitrate đã vượt quá 2000.";
                    update_status_ItemmovieGroup(id, "0", "4");
                    File.Delete(filepath);
                    return;
                }
                if (profile != true)
                {
                    lblError.Text = "Format profile không đúng.";
                    update_status_ItemmovieGroup(id, "0", "4");
                    File.Delete(filepath);
                    return;
                }
                if (videoInfo.Width < 720 || videoInfo.Width > 1280)
                {
                    lblError.Text = "Bạn cần nhập video tỉ lệ 16:9.";
                    update_status_ItemmovieGroup(id, "0", "4");
                    File.Delete(filepath);
                    return;
                }

                //dong bo phim len media, cdn
                Insert_sync(id_group, itemfile);

                lblError.Text = "Upload Success";
                mi.Close();
            }

        }
        catch (Exception)
        {
        }

    }
    #region Grid of page

    private void dgrCommon_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dgrCommon.CurrentPageIndex = e.NewPageIndex;
        BindDataGrid();
    }
    private void InitializeComponent()
    {
        this.dgrCommon.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgrCommon_PageIndexChanged);
    }
    protected void dgrCommon_SelectedIndexChanged(object sender, EventArgs e)
    {
        dgrCommon.CurrentPageIndex = 0;
        BindDataGrid();
    }
    private void BindDataGrid()
    {
        //SQL = BuildFilter();
        //lblError.Text = SQL;



        //dgrCommon.DataSource = BuildFilter().Tables[0];


        try
        {

            //dv = VatLidOnPhim.DAL.CreateDataView(SQL);
            if (BuildFilter().Tables[0].Rows.Count > 0)
            {
                dgrCommon.DataSource = BuildFilter().Tables[0];
                //dgrCommon.DataSource = dv;


                //String format4 = "<img  src='" + VatLidOnPhim.Variables.ImageLinks7 + "' onclick=\"window.open('" + VatLidOnPhim.Variables.sWebRoot + "CLIP/PlayDigital.aspx?ID={0}','','top=100,left=200,height=300,width=300,scrollbars=0,toolbar=0')\" />";
                //VatLidOnPhim.DAL.FetchDataGridColumnFormat(dgrCommon, "Xem", "ID", format4);

                dgrCommon.DataKeyField = "ID";
                dgrCommon.AutoGenerateColumns = false;
                dgrCommon.DataBind();
            }


            //lblTotalRecords.Text = "Số bản ghi: " + dgrCommon.Items.Count.ToString() + " trên tổng số " + dv.Count.ToString();
        }
        catch (Exception e)
        {
            VatLidOnPhim.DAL.ExceptionProcess(e);
        }

    }
    #endregion
    #region Web Form Designer generated code
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: This call is required by the ASP.NET Web Form Designer.
        //
        InitializeComponent();
        base.OnInit(e);
    }
    #endregion

    private DataSet BuildFilter()
    {
        DataSet ds = null;
        try
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@idvideo_group", id);
            ds = VatLidOnPhim.DAL.GetDataSet("cms_Itemmovies_select_0", cmd);
        }
        catch (Exception ex)
        {
            Response.Redirect("");
        }
        return ds;


        //SQL = "SELECT ID,ItemName,ItemDate,ItemStatus,DatePublish,Userupload  FROM ItemMovieGroup WHERE 1=1 and ItemStatus=1 order by ItemDate desc";

        //return SQL;
    }


    private void Insert_sync(string id, string itemfile)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@ID", id);
        cmd.Parameters.AddWithValue("@ItemFile1", itemfile);

        VatLidOnPhim.DAL.GetDataSet("sp_Insert_Sync", cmd);
    }
    private void update_status_ItemmovieGroup(string id, string status, string type)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@status", status);
        cmd.Parameters.AddWithValue("@type", type);

        VatLidOnPhim.DAL.GetDataSet("cms_ItemMovieGroup_update_status", cmd);
    }
    protected void cmdSetShowHome_Click(object sender, EventArgs e)
    {
        int i;
        string sTemp = "";
        for (i = 0; i < dgrCommon.Items.Count; i++)
        {

            if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == true)
            {

                if (sTemp != "") { sTemp += ","; }
                sTemp += dgrCommon.DataKeys[i];
                VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUser(Session).ToString(), VatLidOnPhim.LogType.Publish.ToString(), "", "OK", dgrCommon.DataKeys[i].ToString(), VatLidOnPhim.Utils.GetIP());
            }

        }

        if (sTemp != "")
        {
            string[] idList = sTemp.Split(',');

            for (int j = 0; j < idList.Length; j++)
            {

                update_status_ItemmovieGroup(idList[j], "2", "3");
            }

            lblError.Text = "Đã thực hiện thành công:" + sTemp;
        }
        else
        {
            VatLidOnPhim.MessageBox.Show("Bạn phải chọn một mục để thực hiện");
        }
        BindDataGrid();
    }

    protected void cmdDelete_Click(object sender, EventArgs e)
    {
        int i;
        string sTemp = "";
        for (i = 0; i < dgrCommon.Items.Count; i++)
        {

            if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == true)
            {

                if (sTemp != "") { sTemp += ","; }
                sTemp += dgrCommon.DataKeys[i];
                VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUser(Session).ToString(), VatLidOnPhim.LogType.Delete.ToString(), "", "OK", dgrCommon.DataKeys[i].ToString(), VatLidOnPhim.Utils.GetIP());
            }

        }
        if (sTemp != "")
        {
            string[] idList = sTemp.Split(',');

            for (int j = 0; j < idList.Length; j++)
            {
                update_status_ItemmovieGroup(idList[j], "0", "1");
            }

            lblError.Text = "Đã thực hiện thành công:" + sTemp;
        }
        else
        {
            VatLidOnPhim.MessageBox.Show("Bạn phải chọn một mục để thực hiện");
        }
        BindDataGrid();
    }
}
