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
using System.Collections.Generic;
using MediaInfoLib;

public partial class MOVIE_ItemMovie_Data : System.Web.UI.Page
{
    protected string test = "";
    protected string id = "";
    protected string PhimID = "";
    protected string strCategoryID = "";
    protected string TYPE = "";
    protected int iStatus = 1;
    protected int status1 = 0;
    protected int status2 = 0;
    protected string SQL = "";
    protected DataView dv = null;
    protected string FileName = "";
    protected string strPartnerID = "";
    public static DataSet ds = null;
    protected string Status = "2";
    protected string itemcode = "";
    public static string folder = "";
    public static string itemdate = "";
    public static string itemfile = "";
    public static string fileTrailer = "";
    protected string UserLogin = "";
    protected string listCate = "";
    protected string type_film_name = "NORMAL";
    protected string TYPE_FILM_TVOD = "TVOD_ODD";
    protected string TYPE_FILM_TVOD_DRM = "TVOD_DRM";
    protected string TYPE_FILM_BIGSIX = "BIGSIX";
    protected bool hasFileTrailer = false;

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
        //dateName.Style["display"] = "none";
        //datetimepicker.Style["display"] = "none";
        //inputDatetime.Style["display"] = "none";
        infoTvod.Visible = false;
        infoBigsix.Visible = false;

        if (Utils.IsNumeric(id))
        {
            id = id;
        }
        else
        {
            id = "0";
        }

        TYPE = Request.QueryString["type"];

        strCategoryID = (Request.QueryString["CategoryID"] == null) ? "0" : Request.QueryString["CategoryID"];
        Status = (Request.QueryString["status"] == null) ? "0" : Request.QueryString["status"];

        if (Utils.IsNumeric(strCategoryID))
        {
            strCategoryID = strCategoryID;
        }
        else
        {
            strCategoryID = "0";
        }

        strPartnerID = (Request.QueryString["PartnerID"] == null) ? "0" : Request.QueryString["PartnerID"];
        if (Utils.IsNumeric(strPartnerID))
        {
            strPartnerID = strPartnerID;
        }
        else
        {
            strPartnerID = "0";
        }

        if (Session["USER"] != null)
        {
            if (!Page.IsPostBack)
            {
                BindListCommandAdd();
                BindListbox();
                switch (TYPE)
                {
                    case "edit":
                        Label1.Text = "CẬP NHẬT PHIM.";
                        ddlProductInfo.SelectedValue = "0";
                        BindData();
                        //dateName.Style["display"] = "nomal";
                        //datetimepicker.Style["display"] = "nomal";
                        //inputDatetime.Style["display"] = "nomal";
                        BindDataGrid();
                        break;
                    default:
                        Label1.Text = "THÊM MỚI PHIM.";
                        txtUserName.Text = Session["USER"].ToString().ToUpper();
                        //ddlCategory.SelectedValue = strCategoryID;
                        ddlPartnerID.SelectedValue = Session["PartnerID"].ToString();
                        ddlProductInfo.SelectedValue = "0";
                        ddlIntroFilm.SelectedValue = "0";
                        break;
                }
            }
        }
        else
            Response.Redirect(VatLidOnPhim.Variables.sWebRoot + "logout.aspx");
    }

    private void BindListbox()
    {
        SQL = "Categories_ItemMovie WHERE Status in (1,2) and ParentID in (0) order by id asc";
        VatLidOnPhim.DAL.FillDataToCheckboxlist(chkbCategory, SQL, "ID,CategoryName");
        //ListItem liVideoCate = new ListItem("Chuyên mục", "0");
        //ddlCategory.Items.Add(liVideoCate);


        SQL = "ProductInfo";
        VatLidOnPhim.DAL.FillDataToDropdownList(ddlProductInfo, SQL, "ID,ProductInfo");
        ListItem liProductinfo = new ListItem("--- thông tin sản phẩm ---", "0");
        ddlProductInfo.Items.Add(liProductinfo);
    }

    private void BindListCommandAdd()
    {
        //SQL = "Categories_ItemMovie WHERE Status in (1,2) and ParentID in (0) order by id asc";
        //VatLidOnPhim.DAL.FillDataToDropdownList(ddlCategory, SQL, "ID,CategoryName");
        //ListItem liVideoCate = new ListItem("Chuyên mục", "0");
        //ddlCategory.Items.Add(liVideoCate);

        SQL = "Partner_Keeng WHERE status=2 order by id asc";
        VatLidOnPhim.DAL.FillDataToDropdownList(ddlPartnerID, SQL, "ID,namePartner");
        ListItem liPartnerID = new ListItem("Đối tác", "0");
        ddlPartnerID.Items.Add(liPartnerID);

        SQL = "itemmovie_intro WHERE status=2";
        VatLidOnPhim.DAL.FillDataToDropdownList(ddlIntroFilm, SQL, "ID,Name");
        ListItem liIntro = new ListItem("-- Intro Film --", "0");
        ddlIntroFilm.Items.Add(liIntro);

        ListItem lstLoaiPhim = new ListItem("--Tất cả--", "0");
        ddLoaiPhim.Items.Add(lstLoaiPhim);
        lstLoaiPhim = new ListItem("Phim lẻ", "1");
        ddLoaiPhim.Items.Add(lstLoaiPhim);
        lstLoaiPhim = new ListItem("Phim bộ", "2");
        ddLoaiPhim.Items.Add(lstLoaiPhim);

        ListItem lstTypePhim = new ListItem("NORMAL", "2");
        ddlTypePhim.Items.Add(lstTypePhim);
        lstTypePhim = new ListItem("TVOD_ODD", "1");
        ddlTypePhim.Items.Add(lstTypePhim);
        lstTypePhim = new ListItem("TVOD_DRM", "3");
        ddlTypePhim.Items.Add(lstTypePhim);
        lstTypePhim = new ListItem("BIGSIX", "4");
        ddlTypePhim.Items.Add(lstTypePhim);

        ListItem lstLoaiLogo = new ListItem("----Chọn logo---", "0");
        ddlLogo.Items.Add(lstLoaiLogo);
        lstLoaiLogo = new ListItem("Logo keeng", "http://media3.onbox.vn:8088/phimonbox/images/logo/keeng_logo.png");
        ddlLogo.Items.Add(lstLoaiLogo);
        lstLoaiLogo = new ListItem("Logo QPVN", "http://media3.onbox.vn:8088/phimonbox/images/logo/QPVN_logo.png");
        ddlLogo.Items.Add(lstLoaiLogo);

        ListItem lstPositionLogo = new ListItem("----chọn vị trí logo----", "0");
        ddlPosition.Items.Add(lstPositionLogo);
        lstPositionLogo = new ListItem("Left", "Left");
        ddlPosition.Items.Add(lstPositionLogo);
        lstPositionLogo = new ListItem("Right", "Right");
        ddlPosition.Items.Add(lstPositionLogo);

        ListItem lstPhanLoaiVong = new ListItem("--Tất cả--", "0");
        ddlPhanLoaiVong.Items.Add(lstPhanLoaiVong);
        lstPhanLoaiVong = new ListItem("Current", "1");
        ddlPhanLoaiVong.Items.Add(lstPhanLoaiVong);
        lstPhanLoaiVong = new ListItem("2nd cycle", "2");
        ddlPhanLoaiVong.Items.Add(lstPhanLoaiVong);
        lstPhanLoaiVong = new ListItem("Library", "3");
        ddlPhanLoaiVong.Items.Add(lstPhanLoaiVong);

        if (Session["USERNAME"].ToString().ToUpper() == "ADMINISTRATOR")
        {
            ddlPartnerID.SelectedValue = strPartnerID;
        }
        else
        {
            ddlPartnerID.Enabled = false;
            ddlPartnerID.SelectedValue = Session["PartnerID"].ToString().ToUpper();
        }

        txtUserName.Enabled = false;


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

    private void BindData()
    {
        SqlParameter[] parameters = {
            new SqlParameter("@ID", SqlDbType.Int),
        };
        parameters[0].Value = Convert.ToInt32(id);
        DataSet ds;
        ds = VatLidOnPhim.SqlHelper.ExecuteDataset(VatLidOnPhim.DAL.getConnectionStringOnPhim(), CommandType.StoredProcedure, "cms_select_ItemMovie_ByID", parameters);

        try
        {
            //ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["ChuyenMuc"].ToString();//chuyen muc
            ddlPartnerID.SelectedValue = ds.Tables[0].Rows[0]["PartnerID"].ToString();//Đối tác:
            ddLoaiPhim.SelectedValue = ds.Tables[0].Rows[0]["Object_ID"].ToString();// loại phim
            txtName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();// Tiêu đề
            txtNameEn.Text = ds.Tables[0].Rows[0]["ItemNameEn"].ToString();// Tiêu đề
            txtItemProductYear.Text = ds.Tables[0].Rows[0]["ItemProductYear"].ToString();//Năm
            txtItemCountry.Text = ds.Tables[0].Rows[0]["ItemCountry"].ToString();//Quốc gia
            txtDesc.Text = ds.Tables[0].Rows[0]["ItemDesc"].ToString();// Miêu tả
            txtUserName.Text = ds.Tables[0].Rows[0]["UserLogin"].ToString();//Người Upload
            inputDatetime.Value = ds.Tables[0].Rows[0]["DatePublish"].ToString();
            HiddenField1.Value = ds.Tables[0].Rows[0]["ItemDate"].ToString();
            txtIsView.Text = ds.Tables[0].Rows[0]["isView"].ToString();// luot view
            imageDoc.ImageUrl = ds.Tables[0].Rows[0]["ItemImage"].ToString();// anh doc
            imageNgang.ImageUrl = ds.Tables[0].Rows[0]["ItemImageBanner"].ToString();// anh ngang
            imageFlash.ImageUrl = ds.Tables[0].Rows[0]["ItemImageFlash"].ToString();// anh flash 
            imageFlashWeb.ImageUrl = ds.Tables[0].Rows[0]["ImageFlashWeb"].ToString();// anh flash web 
            txtContract.Text = ds.Tables[0].Rows[0]["contract"].ToString();//Contract number
            txtExpired.Value = ds.Tables[0].Rows[0]["expiredlicense"].ToString();// Expired license
            bool ckinter = ds.Tables[0].Rows[0]["IsInternational"].ToString() == "1" ? false : true;
            chkInternational.Checked = ckinter; //quốc tế
            chkIsmonopoly.Checked = ds.Tables[0].Rows[0]["isMonopoly"].ToString() == "1" ? true : false;// bản quyền
            chkIsControls.Checked = ds.Tables[0].Rows[0]["isControls"].ToString() == "1" ? true : false;//đói soát
            //chkIsFree.Checked = ds.Tables[0].Rows[0]["isFree"].ToString() == "1" ? true : false;//film free
            chkFree_content.Checked = ds.Tables[0].Rows[0]["free_content"].ToString() == "1" ? true : false;// free content
            chkFree_data.Checked = ds.Tables[0].Rows[0]["free_data"].ToString() == "1" ? true : false;// free data
            chkFree_bigsix.Checked = ds.Tables[0].Rows[0]["free_bigsix"].ToString() == "1" ? true : false;// free movie bigsix
            chkFree_content_all.Checked = ds.Tables[0].Rows[0]["free_content_all"].ToString() == "1" ? true : false;
            chkFree_data_all.Checked = ds.Tables[0].Rows[0]["free_data_all"].ToString() == "1" ? true : false;
            txtIMDB.Text = ds.Tables[0].Rows[0]["IMBD"].ToString();
            txtPublisher.Text = ds.Tables[0].Rows[0]["Publisher"].ToString();

            string drm_content_id = ds.Tables[0].Rows[0]["drm_content_id"].ToString();
            string isTvod_odd = ds.Tables[0].Rows[0]["istvod_odd"].ToString();
            string type_film = ds.Tables[0].Rows[0]["Type_Film"].ToString();
            txtStartLicense.Value = ds.Tables[0].Rows[0]["StartLicense"].ToString();
            ddlIntroFilm.SelectedValue = ds.Tables[0].Rows[0]["codeIntro"].ToString();

            ddlTypePhim.ClearSelection();
            ddlTypePhim.Items.FindByText(type_film.ToUpper()).Selected = true;

            if (ds.Tables[0].Rows[0]["productInfo"].ToString() != "")
            {
                ddlProductInfo.ClearSelection();
                ddlProductInfo.Items.FindByText(ds.Tables[0].Rows[0]["productInfo"].ToString()).Selected = true;
            }
            if (type_film.ToUpper() == TYPE_FILM_TVOD || type_film.ToUpper() == TYPE_FILM_TVOD_DRM)
            {
                try
                {
                    //ddlTypePhim.SelectedValue = "tvod_drm";
                    txtRatioShare.Text = ds.Tables[0].Rows[0]["ratioshare"].ToString();
                    ddlPhanLoaiVong.SelectedValue = ds.Tables[0].Rows[0]["typesCircle"].ToString();



                    infoTvod.Visible = true;
                }
                catch (Exception ex)
                {
                }

            }
            else if (type_film.ToUpper() == TYPE_FILM_BIGSIX)
            {

                infoBigsix.Visible = true;
                if (ds.Tables[7].Rows.Count > 0)
                {
                    txtFoxIdBigsix.Text = ds.Tables[7].Rows[0]["FoxID"].ToString();//
                }
            }

            foreach (DataRow dr in ds.Tables[4].Rows)
            {
                ListItem listItem = chkbCategory.Items.FindByValue(dr["categoryid"].ToString());
                if (listItem != null) listItem.Selected = true;
            }
            //Tags Videos
            if (ds.Tables.Count > 1)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    string actor = "";
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        actor += dr["id_actor"].ToString() + ",";
                    }

                    if (actor.Length > 0)
                        txtActor.Text = actor.Substring(0, actor.Length - 1);
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    string director = "";
                    foreach (DataRow dr in ds.Tables[2].Rows)
                    {
                        director += dr["id_director"].ToString() + ",";
                    }

                    if (director.Length > 0)
                        txtDirector.Text = director.Substring(0, director.Length - 1);
                }
                if (ds.Tables[3].Rows.Count > 0)
                {
                    string country = "";
                    foreach (DataRow dr in ds.Tables[3].Rows)
                    {
                        country += dr["id_country"].ToString() + ",";
                    }

                    if (country.Length > 0)
                        txtItemCountry.Text = country.Substring(0, country.Length - 1);
                }

                if (ds.Tables[5].Rows.Count > 0)
                {
                    if (!ds.Tables[5].Rows[0]["logo_path"].ToString().Equals(""))
                    {
                        ddlLogo.SelectedValue = ds.Tables[5].Rows[0]["logo_path"].ToString();
                        ddlPosition.SelectedValue = ds.Tables[5].Rows[0]["logo_position"].ToString();
                    }
                }

                if (ds.Tables[6].Rows.Count > 0)
                {
                    txtPriceHD.Text = ds.Tables[6].Rows[0]["HD_Price"].ToString();
                    txtPriceHDDATA.Text = ds.Tables[6].Rows[0]["HD_Price_FreeData"].ToString();
                    txtPriceHDMin.Text = ds.Tables[6].Rows[0]["HD_Price_Min"].ToString();
                    txtPriceSD.Text = ds.Tables[6].Rows[0]["SD_Price"].ToString();
                    txtPriceSDDATA.Text = ds.Tables[6].Rows[0]["SD_Price_FreeData"].ToString();
                    txtPriceSDMin.Text = ds.Tables[6].Rows[0]["SD_Price_Min"].ToString();//
                    ddlPhanLoaiVong.SelectedValue = ds.Tables[6].Rows[0]["TypesCircle"].ToString();//
                    txtRatioShare.Text = ds.Tables[6].Rows[0]["RatioShare"].ToString();//
                    txtFoxID.Text = ds.Tables[6].Rows[0]["FoxID"].ToString();//
                    txtAvailYear.Text = ds.Tables[6].Rows[0]["AvailYear"].ToString();//
                }
            }

            //itemcode = ds.Tables[0].Rows[0]["ItemCode"].ToString();
            //itemfile = ds.Tables[0].Rows[0]["ItemFile"].ToString();
            Status = ds.Tables[0].Rows[0]["ItemStatus"].ToString();
            folder = ds.Tables[0].Rows[0]["FolderName"].ToString();

        }
        catch (Exception e)
        {
            lblError.Text = e.Message;
        }

    }
    protected void cmdSave_Click(object sender, EventArgs e)
    {
        try
        {
            listCate = "";
            foreach (ListItem item in chkbCategory.Items)
            {
                if (item.Selected)
                {
                    if (listCate != "") { listCate += ","; }
                    listCate += item.Value;
                }
            }

            if (FUFileTrailer.HasFile && !FUFileTrailer.PostedFile.FileName.Contains(".mp4"))
            {

                lblError.Text = "Bạn phải nhập trailer film (mp4)";
                return;
            }

            if (txtName.Text.Length <= 1)
            {
                lblError.Text = "Bạn phải nhập tiêu đề video";
                return;
            }

            //if ((TYPE != "edit") && txtName.Text.Trim() == "1")
            //{
            //    lblError.Text = "Video đã nhập trùng. Mời bạn nhập tiêu đề khác";
            //    return;
            //}

            if (listCate.Trim().Length < 1)
            {
                lblError.Text = "Bạn phải chọn chuyên mục";
                return;
            }
            if (ddLoaiPhim.SelectedValue == "0")
            {
                lblError.Text = "Bạn phải chọn loại phim";
                return;
            }

            int isview = 0;
            int.TryParse(txtIsView.Text.Trim(), out isview);

            if (ddlTypePhim.SelectedItem.Text.ToUpper() == TYPE_FILM_TVOD || ddlTypePhim.SelectedItem.Text.ToUpper() == TYPE_FILM_TVOD_DRM)
            {
                ddlTypePhim.Visible = true;
            }

            if (TYPE == "edit")
            {
                //dateName.Visible = true;
                //datetimepicker.Visible = true;
                //inputDatetime.Visible = true;

                SqlParameter[] parameters =
                {

                new SqlParameter("@listCate", SqlDbType.NVarChar,200),
                new SqlParameter("@PartnerID", SqlDbType.Int),
                new SqlParameter("@ItemName", SqlDbType.NVarChar ),
                new SqlParameter("@ItemDirector", SqlDbType.NVarChar ),//Đạo diễn
                new SqlParameter("@ItemProductYear", SqlDbType.NVarChar ),//Năm XS
                new SqlParameter("@ItemCountry", SqlDbType.NVarChar ),// Quốc gia
                new SqlParameter("@ItemActor", SqlDbType.NVarChar ),// Diễn viên
				new SqlParameter("@ItemDesc", SqlDbType.NVarChar),
                new SqlParameter("@UserLogin", SqlDbType.NVarChar),
                new SqlParameter("@ID", SqlDbType.Int),
                new SqlParameter("@status1", SqlDbType.Int),
                new SqlParameter("@status2", SqlDbType.Int),
                new SqlParameter("@status3", SqlDbType.Int),
                new SqlParameter("@datePublish", SqlDbType.VarChar),
                new SqlParameter("@isInternational",SqlDbType.Int),
                new SqlParameter("@isView",SqlDbType.Int),
                new SqlParameter("@typePhim", SqlDbType.NVarChar),
                new SqlParameter("@contract",SqlDbType.VarChar),
                new SqlParameter("@ExpiredLicense",SqlDbType.VarChar),
                new SqlParameter("@isMonopoly",SqlDbType.Int),
                new SqlParameter("@isControls",SqlDbType.Int),
                new SqlParameter("@isFree",SqlDbType.Int),
                new SqlParameter("@IMBD",SqlDbType.NVarChar,10),
                new SqlParameter("@Logo",SqlDbType.NVarChar,100),
                new SqlParameter("@Position",SqlDbType.NVarChar,100),
                new SqlParameter("@free_content",SqlDbType.Int),
                new SqlParameter("@free_data",SqlDbType.Int),
                new SqlParameter("@SD_Price",SqlDbType.Int),
                new SqlParameter("@SD_Price_Data",SqlDbType.Int),
                new SqlParameter("@SD_Price_MIN",SqlDbType.NVarChar,20),
                new SqlParameter("@HD_Price",SqlDbType.Int),
                new SqlParameter("@HD_Price_Data",SqlDbType.Int),
                new SqlParameter("@HD_Price_MIN",SqlDbType.NVarChar,20),
                new SqlParameter("@Start_license",SqlDbType.NVarChar,50),
                new SqlParameter("@Ratio_Share",SqlDbType.NVarChar,50),
                new SqlParameter("@PhanLoaiVong",SqlDbType.NVarChar,50),
                new SqlParameter("@isTVOD",SqlDbType.NVarChar,20),
                new SqlParameter("@FoxID",SqlDbType.NVarChar,100),
                new SqlParameter("@AvailYear",SqlDbType.NVarChar,100),
                new SqlParameter("@FoxIdBigsix",SqlDbType.NVarChar,15),
                new SqlParameter("@Publisher",SqlDbType.NVarChar,100),
                new SqlParameter("@status_flashweb",SqlDbType.Int),
                new SqlParameter("@productInfo",SqlDbType.NVarChar,200),
                new SqlParameter("@introFilm",SqlDbType.Int),
                new SqlParameter("@free_bigsix",SqlDbType.Int),
                new SqlParameter("@ItemNameEn",SqlDbType.NVarChar,300),
                new SqlParameter("@FileTrailer",SqlDbType.VarChar,60),
                new SqlParameter("@status_FileTrailer",SqlDbType.Int),
                new SqlParameter("@free_content_all",SqlDbType.Int),
                new SqlParameter("@free_data_all",SqlDbType.Int)
            };

                parameters[0].Value = listCate.Trim();
                parameters[1].Value = Convert.ToInt32(ddlPartnerID.SelectedItem.Value);
                parameters[2].Value = txtName.Text.Trim();
                parameters[3].Value = txtDirector.Text.Trim();//Đạo diễn
                parameters[4].Value = txtItemProductYear.Text.Trim();//Năm XS
                parameters[5].Value = txtItemCountry.Text.Trim();// Quốc gia
                parameters[6].Value = txtActor.Text.Trim();// Diễn viên
                parameters[7].Value = txtDesc.Text.Trim();//ItemDesc
                parameters[8].Value = Session["USER"].ToString().ToUpper();
                parameters[9].Value = Convert.ToInt32(id);
                parameters[13].Value = inputDatetime.Value;
                parameters[14].Value = chkInternational.Checked == true ? 0 : 1;
                parameters[15].Value = isview;
                parameters[16].Value = ddLoaiPhim.SelectedValue;
                parameters[17].Value = txtContract.Text.Trim();
                parameters[18].Value = txtExpired.Value;
                parameters[19].Value = chkIsmonopoly.Checked == true ? 1 : 0;
                parameters[20].Value = chkIsControls.Checked == true ? 1 : 0;
                parameters[21].Value = 0;
                parameters[22].Value = txtIMDB.Text.Trim();
                parameters[23].Value = ddlLogo.SelectedValue;
                parameters[24].Value = ddlPosition.SelectedValue;
                parameters[25].Value = chkFree_content.Checked == true ? 1 : 0;
                parameters[26].Value = chkFree_data.Checked == true ? 1 : 0;
                parameters[27].Value = txtPriceSD.Text.Trim() == "" ? "0" : txtPriceSD.Text.Trim();
                parameters[28].Value = txtPriceSDDATA.Text.Trim() == "" ? "0" : txtPriceSDDATA.Text.Trim();
                parameters[29].Value = txtPriceSDMin.Text.Trim() == "" ? "0" : txtPriceSDMin.Text.Trim();
                parameters[30].Value = txtPriceHD.Text.Trim() == "" ? "0" : txtPriceHD.Text.Trim();
                parameters[31].Value = txtPriceHDDATA.Text.Trim() == "" ? "0" : txtPriceHDDATA.Text.Trim();
                parameters[32].Value = txtPriceHDMin.Text.Trim() == "" ? "0" : txtPriceHDMin.Text.Trim();
                parameters[33].Value = txtStartLicense.Value.Trim();
                parameters[34].Value = txtRatioShare.Text.Trim();
                parameters[35].Value = ddlPhanLoaiVong.SelectedIndex;
                parameters[36].Value = ddlTypePhim.SelectedItem.Text.Trim();
                parameters[37].Value = txtFoxID.Text.Trim();
                parameters[38].Value = txtAvailYear.Text.Trim();
                parameters[39].Value = txtFoxIdBigsix.Text.Trim();
                parameters[40].Value = txtPublisher.Text.Trim();
                parameters[42].Value = ddlProductInfo.SelectedValue == "0" ? "" : ddlProductInfo.SelectedItem.Text.Trim();
                parameters[43].Value = ddlIntroFilm.SelectedValue;
                parameters[44].Value = chkFree_bigsix.Checked == true ? 1 : 0;
                parameters[45].Value = txtNameEn.Text.Trim();
                parameters[46].Value = "";
                parameters[46].Direction = ParameterDirection.Output;
                parameters[48].Value = chkFree_content_all.Checked == true ? 1 : 0;
                parameters[49].Value = chkFree_data_all.Checked == true ? 1 : 0;
                //itemfile = getFileName(id);
                //UserLogin = getUserLogin(id);

                if (ImageFileImage.HasFile)
                {
                    if (ImageFileImage.PostedFile != null && ImageFileImage.PostedFile.ContentLength > 0)
                    {
                        parameters[10].Value = 1;
                        byte[] pic1 = new byte[ImageFileImage.PostedFile.ContentLength];
                        ImageFileImage.PostedFile.InputStream.Read(pic1, 0, ImageFileImage.PostedFile.ContentLength);
                        addImageVideoToFileUpdate(id + ".jpg", pic1, Convert.ToDateTime(HiddenField1.Value));
                    }
                }
                else
                {
                    parameters[10].Value = 0;
                }
                if (ImageFileImageBanner.HasFile)
                {
                    if (ImageFileImageBanner.PostedFile != null && ImageFileImageBanner.PostedFile.ContentLength > 0)
                    {
                        parameters[11].Value = 1;
                        byte[] pic2 = new byte[ImageFileImageBanner.PostedFile.ContentLength];
                        ImageFileImageBanner.PostedFile.InputStream.Read(pic2, 0, ImageFileImageBanner.PostedFile.ContentLength);
                        addImageVideoToFileUpdate(id + "_banner.jpg", pic2, Convert.ToDateTime(HiddenField1.Value));
                    }
                }
                else
                {
                    parameters[11].Value = 0;

                }
                if (ImageFileImageFlash.HasFile)
                {
                    if (ImageFileImageFlash.PostedFile != null && ImageFileImageFlash.PostedFile.ContentLength > 0)
                    {
                        parameters[12].Value = 1;
                        byte[] pic3 = new byte[ImageFileImageFlash.PostedFile.ContentLength];
                        ImageFileImageFlash.PostedFile.InputStream.Read(pic3, 0, ImageFileImageFlash.PostedFile.ContentLength);
                        addImageVideoToFileUpdate(id + "_flash.jpg", pic3, Convert.ToDateTime(HiddenField1.Value));
                    }
                }
                else
                {
                    parameters[12].Value = 0;

                }

                if (FiUploadFlashHotWeb.HasFile)
                {
                    if (FiUploadFlashHotWeb.PostedFile != null && FiUploadFlashHotWeb.PostedFile.ContentLength > 0)
                    {
                        parameters[41].Value = 1;
                        byte[] pic3 = new byte[FiUploadFlashHotWeb.PostedFile.ContentLength];
                        FiUploadFlashHotWeb.PostedFile.InputStream.Read(pic3, 0, FiUploadFlashHotWeb.PostedFile.ContentLength);
                        addImageVideoToFileUpdate(id + "_flash_web.jpg", pic3, Convert.ToDateTime(HiddenField1.Value));
                    }
                }
                else
                {
                    parameters[41].Value = 0;
                }

                if (FUFileTrailer.HasFile)
                {
                    if (FUFileTrailer.PostedFile != null && FUFileTrailer.PostedFile.ContentLength > 0)
                    {
                        parameters[47].Value = 1;

                        hasFileTrailer = true;
                    }
                }
                else
                {
                    parameters[47].Value = 0;
                    lblError.Text = "update success";
                }

                VatLidOnPhim.SqlHelper.ExecuteNonQuery(VatLidOnPhim.DAL.getConnectionStringOnPhim(), CommandType.StoredProcedure, "cms_update_ItemMovie", parameters);
                VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUser(Session).ToString(), VatLidOnPhim.LogType.UPDATE.ToString(), "", "OK", id.ToString(), VatLidOnPhim.Utils.GetIP());

                if (hasFileTrailer)
                {
                    fileTrailer = parameters[46].Value.ToString();
                    byte[] mp4 = new byte[FUFileTrailer.PostedFile.ContentLength];
                    FUFileTrailer.PostedFile.InputStream.Read(mp4, 0, FUFileTrailer.PostedFile.ContentLength);
                    string folderDay = DateTime.Now.ToString("yyyy/MM/dd").Replace("/", "");
                    addMp4Phim(fileTrailer + ".mp4", mp4);

                    //check file format video
                    CheckBitrate(fileTrailer);
                }

            }
            else  //Them moi
            {
                SqlParameter[] parameters =
                {
                new SqlParameter("@listCate", SqlDbType.NVarChar,200),
                new SqlParameter("@PartnerID", SqlDbType.Int),
                new SqlParameter("@ItemName", SqlDbType.NVarChar ),
                new SqlParameter("@ItemDirector", SqlDbType.NVarChar ),//Đạo diễn
                new SqlParameter("@ItemProductYear", SqlDbType.NVarChar ),//Năm XS
                new SqlParameter("@ItemCountry", SqlDbType.NVarChar ),// Quốc gia
                new SqlParameter("@ItemActor", SqlDbType.NVarChar ),// Diễn viên
				new SqlParameter("@ItemDesc", SqlDbType.NVarChar),
                new SqlParameter("@UserLogin", SqlDbType.NVarChar),
                new SqlParameter("@ID", SqlDbType.Int),
                new SqlParameter("@ID_group", SqlDbType.Int),
                new SqlParameter("@isInternational",SqlDbType.Int),
                new SqlParameter("@isView",SqlDbType.Int),
                new SqlParameter("@typePhim",SqlDbType.NVarChar),
                new SqlParameter("@contract",SqlDbType.VarChar),
                new SqlParameter("@ExpiredLicense",SqlDbType.VarChar),// expired license
                new SqlParameter("@isMonopoly",SqlDbType.Int),
                new SqlParameter("@isControls",SqlDbType.Int),
                new SqlParameter("@isFree",SqlDbType.Int),
                new SqlParameter("@status1", SqlDbType.Int),
                new SqlParameter("@status2", SqlDbType.Int),
                new SqlParameter("@status3", SqlDbType.Int),
                new SqlParameter("@IMBD",SqlDbType.NVarChar,10),
                new SqlParameter("@Logo",SqlDbType.NVarChar,100),
                new SqlParameter("@Position",SqlDbType.NVarChar,100),
                new SqlParameter("@free_content",SqlDbType.Int),
                new SqlParameter("@free_data",SqlDbType.Int),
                new SqlParameter("@SD_Price",SqlDbType.Int),
                new SqlParameter("@SD_Price_Data",SqlDbType.Int),
                new SqlParameter("@SD_Price_MIN",SqlDbType.NVarChar,20),
                new SqlParameter("@HD_Price",SqlDbType.Int),
                new SqlParameter("@HD_Price_Data",SqlDbType.Int),
                new SqlParameter("@HD_Price_MIN",SqlDbType.NVarChar,20),
                new SqlParameter("@Start_license",SqlDbType.NVarChar,50),
                new SqlParameter("@Ratio_Share",SqlDbType.NVarChar,50),
                new SqlParameter("@PhanLoaiVong",SqlDbType.NVarChar,50),
                new SqlParameter("@isTVOD",SqlDbType.NVarChar,20),
                new SqlParameter("@FoxID",SqlDbType.NVarChar,100),
                new SqlParameter("@AvailYear",SqlDbType.NVarChar,100),
                new SqlParameter("@Publisher",SqlDbType.NVarChar,100),
                new SqlParameter("@status_flashweb",SqlDbType.Int),
                new SqlParameter("@productInfo",SqlDbType.NVarChar,200),
                new SqlParameter("@datePublish",SqlDbType.NVarChar),
                new SqlParameter("@introFilm",SqlDbType.Int),
                new SqlParameter("@ItemNameEn",SqlDbType.NVarChar,300),
                new SqlParameter("@FileTrailer",SqlDbType.VarChar,60),
                new SqlParameter("@status_FileTrailer",SqlDbType.Int),
                new SqlParameter("@free_content_all",SqlDbType.Int),
                new SqlParameter("@free_data_all",SqlDbType.Int)
            };

                parameters[0].Value = listCate.Trim();
                parameters[1].Value = Convert.ToInt32(ddlPartnerID.SelectedItem.Value);
                parameters[2].Value = txtName.Text.Trim();
                parameters[3].Value = txtDirector.Text.Trim();//Đạo diễn
                parameters[4].Value = txtItemProductYear.Text.Trim();//Năm XS
                parameters[5].Value = txtItemCountry.Text.Trim();// Quốc gia
                parameters[6].Value = txtActor.Text.Trim();// Diễn viên
                parameters[7].Value = txtDesc.Text.Trim();//ItemDesc
                parameters[8].Value = Session["USER"].ToString().ToUpper();
                parameters[9].Value = 0;
                parameters[9].Direction = ParameterDirection.Output;
                parameters[10].Value = 0;
                parameters[10].Direction = ParameterDirection.Output;
                parameters[11].Value = chkInternational.Checked == true ? 0 : 1;
                parameters[12].Value = isview;// luot view
                parameters[13].Value = ddLoaiPhim.SelectedValue;// loai phim
                parameters[14].Value = txtContract.Text.Trim();//contract number
                parameters[15].Value = txtExpired.Value.Trim(); // expired license
                parameters[16].Value = chkIsmonopoly.Checked == true ? 1 : 0;
                parameters[17].Value = chkIsControls.Checked == true ? 1 : 0;
                parameters[18].Value = 0;
                parameters[19].Value = 1;
                parameters[20].Value = 1;
                parameters[21].Value = 1;
                parameters[22].Value = txtIMDB.Text.Trim();
                parameters[23].Value = ddlLogo.SelectedValue;
                parameters[24].Value = ddlPosition.SelectedValue;
                parameters[25].Value = chkFree_content.Checked == true ? 1 : 0;
                parameters[26].Value = chkFree_data.Checked == true ? 1 : 0;
                parameters[27].Value = txtPriceSD.Text.Trim() == "" ? "0" : txtPriceSD.Text.Trim();
                parameters[28].Value = txtPriceSDDATA.Text.Trim() == "" ? "0" : txtPriceSDDATA.Text.Trim();
                parameters[29].Value = txtPriceSDMin.Text.Trim() == "" ? "0" : txtPriceSDMin.Text.Trim();
                parameters[30].Value = txtPriceHD.Text.Trim() == "" ? "0" : txtPriceHD.Text.Trim();
                parameters[31].Value = txtPriceHDDATA.Text.Trim() == "" ? "0" : txtPriceHDDATA.Text.Trim();
                parameters[32].Value = txtPriceHDMin.Text.Trim() == "" ? "0" : txtPriceHDMin.Text.Trim();
                parameters[33].Value = txtStartLicense.Value.Trim();
                parameters[34].Value = txtRatioShare.Text.Trim();
                parameters[35].Value = ddlPhanLoaiVong.SelectedValue;
                parameters[36].Value = ddlTypePhim.SelectedItem.Text.Trim();
                parameters[37].Value = txtFoxID.Text.Trim();
                parameters[38].Value = txtAvailYear.Text.Trim();
                parameters[39].Value = txtPublisher.Text.Trim();
                parameters[41].Value = ddlProductInfo.SelectedValue == "0" ? "" : ddlProductInfo.SelectedItem.Text.Trim();
                parameters[42].Value = inputDatetime.Value;
                parameters[43].Value = ddlIntroFilm.SelectedValue;
                parameters[44].Value = txtNameEn.Text.Trim();
                parameters[45].Value = "";
                parameters[45].Direction = ParameterDirection.Output;
                parameters[47].Value = chkFree_content_all.Checked == true ? 1 : 0;
                parameters[48].Value = chkFree_data_all.Checked == true ? 1 : 0;

                if (FiUploadFlashHotWeb.HasFile)
                {
                    if (FiUploadFlashHotWeb.PostedFile != null && FiUploadFlashHotWeb.PostedFile.ContentLength > 0)
                    {
                        parameters[40].Value = 1;
                    }
                }
                else
                {
                    parameters[40].Value = 0;
                }

                if (FUFileTrailer.HasFile)
                {
                    if (FUFileTrailer.PostedFile != null && FUFileTrailer.PostedFile.ContentLength > 0)
                    {
                        parameters[46].Value = 1;
                    }
                }
                else
                {
                    parameters[46].Value = 0;
                }

                VatLidOnPhim.SqlHelper.ExecuteNonQuery(VatLidOnPhim.DAL.getConnectionStringOnPhim(), CommandType.StoredProcedure, "cms_insert_ItemMovie", parameters);

                itemfile = parameters[10].Value.ToString();
                id = parameters[9].Value.ToString();
                fileTrailer = parameters[45].Value.ToString();

                if (ImageFileImage.PostedFile != null && ImageFileImage.PostedFile.ContentLength > 0)
                {

                    byte[] pic1 = new byte[ImageFileImage.PostedFile.ContentLength];
                    ImageFileImage.PostedFile.InputStream.Read(pic1, 0, ImageFileImage.PostedFile.ContentLength);
                    addImagePhim(id + ".jpg", pic1);

                }
                if (ImageFileImageBanner.HasFile)
                {
                    if (ImageFileImageBanner.PostedFile != null && ImageFileImageBanner.PostedFile.ContentLength > 0)
                    {

                        byte[] pic2 = new byte[ImageFileImageBanner.PostedFile.ContentLength];
                        ImageFileImageBanner.PostedFile.InputStream.Read(pic2, 0, ImageFileImageBanner.PostedFile.ContentLength);
                        addImagePhim(id + "_banner.jpg", pic2);
                    }
                }
                if (ImageFileImageFlash.HasFile)
                {
                    if (ImageFileImageFlash.PostedFile != null && ImageFileImageFlash.PostedFile.ContentLength > 0)
                    {

                        byte[] pic3 = new byte[ImageFileImageFlash.PostedFile.ContentLength];
                        ImageFileImageFlash.PostedFile.InputStream.Read(pic3, 0, ImageFileImageFlash.PostedFile.ContentLength);
                        addImagePhim(id + "_flash.jpg", pic3);
                    }
                }

                if (FiUploadFlashHotWeb.HasFile)
                {
                    if (FiUploadFlashHotWeb.PostedFile != null && FiUploadFlashHotWeb.PostedFile.ContentLength > 0)
                    {
                        byte[] pic3 = new byte[FiUploadFlashHotWeb.PostedFile.ContentLength];
                        FiUploadFlashHotWeb.PostedFile.InputStream.Read(pic3, 0, FiUploadFlashHotWeb.PostedFile.ContentLength);
                        addImageVideoToFileUpdate(id + "_flash_web.jpg", pic3, Convert.ToDateTime(HiddenField1.Value));
                    }
                }

                if (FUFileTrailer.HasFile)
                {
                    if (FUFileTrailer.PostedFile != null && FUFileTrailer.PostedFile.ContentLength > 0)
                    {
                        byte[] mp4 = new byte[FUFileTrailer.PostedFile.ContentLength];
                        FUFileTrailer.PostedFile.InputStream.Read(mp4, 0, FUFileTrailer.PostedFile.ContentLength);
                        string folderDay = DateTime.Now.ToString("yyyy/MM/dd").Replace("/", "");
                        addMp4Phim(fileTrailer + ".mp4", mp4);

                        //check file format video
                        CheckBitrate(fileTrailer);
                    }
                }
                else
                {
                    lblError.Text = "Đã nhập thành công Film";
                }

                VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUser(Session).ToString(), VatLidOnPhim.LogType.ADD.ToString(), "", "OK", id.ToString(), VatLidOnPhim.Utils.GetIP());

                //lblError.Text = "Đã nhập thành công Film:  " + txtName.Text.Trim() + ".";

            }

            BindDataGrid();
            BindData();
        }
        catch (Exception ex)
        {
        }
    }

    #region check bitrate file
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
                    if (infoFileArr[i].ToString().IndexOf("High@") >= 0)
                    {
                        profile = true;
                        break;
                    }
                }

                VideoInfo videoInfo = new VideoInfo(mi);

                if (videoInfo.Bitrate / 1024 > 5000)
                {
                    lblError.Text = "Bitrate đã vượt quá 5000.";
                    // xóa file vượt quá bitrate
                    File.Delete(filepath);
                    return;
                }
                //if (profile != true)
                //{
                //    lblError.Text = "Format profile không đúng.";
                //    update_status_ItemmovieGroup(id, "0", "4");
                //    File.Delete(filepath);
                //    return;
                //}
                //if (videoInfo.Width < 720 || videoInfo.Width > 1280)
                //{
                //    lblError.Text = "Bạn cần nhập video tỉ lệ 16:9.";
                //    update_status_ItemmovieGroup(id, "0", "4");
                //    File.Delete(filepath);
                //    return;
                //}

                //dong bo phim len media, cdn
                Insert_sync(id, itemfile);

                lblError.Text = "Upload Success";
                mi.Close();
            }

        }
        catch (Exception)
        {
        }

    }

    private void Insert_sync(string id, string itemfile)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@ID", id);
        cmd.Parameters.AddWithValue("@ItemFile1", itemfile);

        VatLidOnPhim.DAL.GetDataSet("sp_Insert_Sync_trailer", cmd);
    }
    #endregion


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
    public static void WriteLogFile(string strLogText)
    {
        string filePath = "E:\\out.txt";
        try
        {
            StreamWriter writer;
            if (!File.Exists(filePath))
            {
                writer = new StreamWriter(filePath);
            }
            else
            {
                writer = File.AppendText(filePath);
            }
            writer.WriteLine(strLogText);
            writer.Close();
            writer.Dispose();
        }
        catch (Exception)
        {
        }
        finally
        {

        }
    }

    public void addImagePhim(string id, byte[] imgdata)
    {
        string imdDBLocal = ConfigurationSettings.AppSettings["FilmPhotoDBPath"];
        string imghis = imdDBLocal + itemdate + "/" + id + ".jpg";
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

    public void addImageVideoToFileUpdate(string id, byte[] imgdata, DateTime itemDate)
    {
        string imdDBLocal = ConfigurationSettings.AppSettings["FilmPhotoDBPath"];
        string imghis = imdDBLocal + itemDate.ToString("yyyy/MM/dd").Replace("/", "") + "/" + id;
        string folderDay = imdDBLocal + itemDate.ToString("yyyy/MM/dd").Replace("/", "");
        string filepath = folderDay + "/" + id.ToString();
        test += "3. " + filepath + " - ";
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

    protected void ddlTypePhim_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTypePhim.SelectedItem.Text.ToUpper() == TYPE_FILM_TVOD || ddlTypePhim.SelectedItem.Text.ToUpper() == TYPE_FILM_TVOD_DRM)
        {
            infoTvod.Visible = true;

        }
        else if (ddlTypePhim.SelectedItem.Text.ToUpper() == TYPE_FILM_BIGSIX)
        {
            infoBigsix.Visible = true;
        }
    }
    protected void ddlProductInfo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlIntroFilm_SelectedIndexChanged(object sender, EventArgs e)
    {

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

            SQL = "UPDATE itemmoviegroup SET ItemStatus=2 , DatePublish = getdate() WHERE ID in (" + sTemp + ")";
            VatLidOnPhim.DAL.ExecuteQuery(SQL);
            lblError.Text = "Thực hiện thành công:" + sTemp;

        }
        else
        {
            VatLidOnPhim.MessageBox.Show("Bạn phải chọn một mục để thực hiện");
        }
        BindDataGrid();
    }

    protected void cmdAddLogo_Click(object sender, EventArgs e)
    {
        int i;
        string sTemp = "";
        for (i = 0; i < dgrCommon.Items.Count; i++)
        {

            if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == true)
            {

                if (sTemp != "") { sTemp += ","; }
                sTemp += dgrCommon.DataKeys[i];
                //VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUser(Session).ToString(), VatLidOnPhim.LogType.Publish.ToString(), "", "OK", dgrCommon.DataKeys[i].ToString(), VatLidOnPhim.Utils.GetIP());
            }

        }
        if (ddlLogo.SelectedValue != "0" && ddlPosition.SelectedValue != "0")
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@logo", ddlLogo.SelectedValue);
            cmd.Parameters.AddWithValue("@logoposition", ddlPosition.SelectedValue);
            cmd.Parameters.AddWithValue("@id", id);

            VatLidOnPhim.DAL.GetDataSet("sp_addlogo_itemmoviegroup", cmd);

            //SQL = "UPDATE itemmoviegroup SET ItemStatus=2 , DatePublish = getdate(),logo_path='" + ddlLogo.SelectedValue + "',logo_position='" + ddlPosition.SelectedValue + "' WHERE id_group=" + id;
            //VatLidOnPhim.DAL.ExecuteQuery(SQL);
            lblError.Text = "Thực hiện thành công:" + id;

        }
        else
        {
            VatLidOnPhim.MessageBox.Show("Bạn phải chọn logo hoặc vị trí logo");
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

            //SQL = "DELETE ItemMovie WHERE ID in (" + sTemp + ")";
            SQL = "UPDATE ItemMovieGroup SET ItemStatus=0 , DatePublish = getdate() WHERE id in (" + sTemp + ")";
            VatLidOnPhim.DAL.ExecuteQuery(SQL);
            lblError.Text = "Thực hiện thành công:" + sTemp;

        }
        else
        {
            VatLidOnPhim.MessageBox.Show("Bạn phải chọn một mục để thực hiện");
        }
        BindDataGrid();
    }

}
