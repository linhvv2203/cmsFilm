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
    protected string UserLogin = "";
    protected string listCate = "";
    protected string cpCode = ""; 
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
        dateName.Style["display"] = "none";
        datetimepicker.Style["display"] = "none";
        inputDatetime.Style["display"] = "none";

        if (Utils.IsNumeric(id))
        {
            id = id;
        }
        else
        {
            id = "0";
        }

        TYPE = Request.QueryString["type"];

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
                        BindData();
                        dateName.Style["display"] = "nomal";
                        datetimepicker.Style["display"] = "nomal";
                        inputDatetime.Style["display"] = "nomal";
                        BindDataGrid();
                        break;
                    default:
                        Label1.Text = "THÊM MỚI PHIM.";
                        txtUserName.Text = Session["USER"].ToString().ToUpper();
                        txtPartner.Text = Session["USER"].ToString();
                        //ddlCategory.SelectedValue = strCategoryID;
                        //ddlPartnerID.SelectedValue = Session["PartnerID"].ToString();
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
    }

    private void BindListCommandAdd()
    {
        //SQL = "Categories_ItemMovie WHERE Status in (1,2) and ParentID in (0) order by id asc";
        //VatLidOnPhim.DAL.FillDataToDropdownList(ddlCategory, SQL, "ID,CategoryName");
        //ListItem liVideoCate = new ListItem("Chuyên mục", "0");
        //ddlCategory.Items.Add(liVideoCate);

        //SQL = "Partner_Keeng WHERE status=2 order by id asc";
        //VatLidOnPhim.DAL.FillDataToDropdownList(ddlPartnerID, SQL, "ID,namePartner");
        //ListItem liPartnerID = new ListItem("Đối tác", "0");
        //ddlPartnerID.Items.Add(liPartnerID);

        ListItem lstLoaiPhim = new ListItem("--Tất cả--", "0");
        ddLoaiPhim.Items.Add(lstLoaiPhim);
        lstLoaiPhim = new ListItem("Phim lẻ", "1");
        ddLoaiPhim.Items.Add(lstLoaiPhim);
        lstLoaiPhim = new ListItem("Phim bộ", "2");
        ddLoaiPhim.Items.Add(lstLoaiPhim);

        //ListItem lstLoaiLogo = new ListItem("----Chọn logo---", "0");
        //ddlLogo.Items.Add(lstLoaiLogo);
        //lstLoaiLogo = new ListItem("Logo keeng", "http://media3.onbox.vn:8088/phimonbox/images/logo/keeng_logo.png");
        //ddlLogo.Items.Add(lstLoaiLogo);
        //lstLoaiLogo = new ListItem("Logo QPVN", "http://media3.onbox.vn:8088/phimonbox/images/logo/QPVN_logo.png");
        //ddlLogo.Items.Add(lstLoaiLogo);

        //ListItem lstPositionLogo = new ListItem("----chọn vị trí logo----", "0");
        //ddlPosition.Items.Add(lstPositionLogo);
        //lstPositionLogo = new ListItem("Left", "Left");
        //ddlPosition.Items.Add(lstPositionLogo);
        //lstPositionLogo = new ListItem("Right", "Right");
        //ddlPosition.Items.Add(lstPositionLogo);

        //if (Session["USERNAME"].ToString().ToUpper() == "ADMINISTRATOR")
        //{
        //    ddlPartnerID.SelectedValue = strPartnerID;
        //}
        //else
        //{
        //    ddlPartnerID.Enabled = false;
        //    ddlPartnerID.SelectedValue = Session["PartnerID"].ToString().ToUpper();
        //}

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
        ds = VatLidOnPhim.SqlHelper.ExecuteDataset(VatLidOnPhim.DAL.getConnectionStringOnPhim(), CommandType.StoredProcedure, "cms_select_ItemMovie_ByID_v5", parameters);

        try
        {
            //ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["ChuyenMuc"].ToString();//chuyen muc
            txtPartner.Text = Session["USER"].ToString(); //Đối tác:
            ddLoaiPhim.SelectedValue = ds.Tables[0].Rows[0]["Object_ID"].ToString();// loại phim
            txtName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();// Tiêu đề
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
            txtContract.Text = ds.Tables[0].Rows[0]["contract"].ToString();//Contract number
            txtExpired.Value = ds.Tables[0].Rows[0]["expiredlicense"].ToString();// Expired license
            bool ckinter = ds.Tables[0].Rows[0]["IsInternational"].ToString() == "1" ? false : true;
            //chkInternational.Checked = ckinter; //quốc tế
            //chkIsmonopoly.Checked = ds.Tables[0].Rows[0]["isMonopoly"].ToString() == "1" ? true : false;// bản quyền
            //chkIsControls.Checked = ds.Tables[0].Rows[0]["isControls"].ToString() == "1" ? true : false;//đói soát
            //chkIsFree.Checked = ds.Tables[0].Rows[0]["isFree"].ToString() == "1" ? true : false;//film free
            //chkFree_content.Checked = ds.Tables[0].Rows[0]["free_content"].ToString() == "1" ? true : false;// free content
            //chkFree_data.Checked = ds.Tables[0].Rows[0]["free_data"].ToString() == "1" ? true : false;// free data
            txtIMDB.Text = ds.Tables[0].Rows[0]["IMBD"].ToString();

            //txtPartNumber.Text = ds.Tables[0].Rows[0]["PartID"].ToString();//Số Tập

            //imagedd.ImageUrl = (VatLidOnPhim.Variables.sPhimPathImage + ds.Tables[0].Rows[0]["FolderName"].ToString() + "/" + ds.Tables[0].Rows[0]["ItemImage"].ToString()).ToString();

            //check box category

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
            }

            if (ds.Tables.Count > 2)
            {
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
            }
            if (ds.Tables.Count > 2)
            {
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
            }
            if (ds.Tables.Count > 2)
            {
                if (ds.Tables[5].Rows.Count > 0)
                {
                    //ddlLogo.SelectedValue = ds.Tables[5].Rows[0]["logo_path"].ToString();
                    //ddlPosition.SelectedValue = ds.Tables[5].Rows[0]["logo_position"].ToString();
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
        listCate = "";
        foreach (ListItem item in chkbCategory.Items)
        {
            if (item.Selected)
            {
                if (listCate != "") { listCate += ","; }
                listCate += item.Value;
            }
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

        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@partnerName", Session["USER"].ToString());
        ds = VatLidOnPhim.DAL.GetDataSet("sp_getPartnerID", cmd);
        cpCode = ds.Tables[0].Rows[0]["id"].ToString();

        if (TYPE == "edit")
        {
            dateName.Visible = true;
            datetimepicker.Visible = true;
            inputDatetime.Visible = true;

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
			};

            parameters[0].Value = listCate.Trim();
            parameters[1].Value = Convert.ToInt32(cpCode);
            parameters[2].Value = txtName.Text.Trim();
            parameters[3].Value = txtDirector.Text.Trim();//Đạo diễn
            parameters[4].Value = txtItemProductYear.Text.Trim();//Năm XS
            parameters[5].Value = txtItemCountry.Text.Trim();// Quốc gia
            parameters[6].Value = txtActor.Text.Trim();// Diễn viên
            parameters[7].Value = txtDesc.Text.Trim();//ItemDesc
            parameters[8].Value = Session["USER"].ToString().ToUpper();
            parameters[9].Value = Convert.ToInt32(id);
            parameters[13].Value = inputDatetime.Value;
            parameters[14].Value = 1;//is international
            parameters[15].Value = isview;
            parameters[16].Value = ddLoaiPhim.SelectedValue;
            parameters[17].Value = txtContract.Text.Trim();
            parameters[18].Value = txtExpired.Value;
            parameters[19].Value = 0;//is monopoly
            parameters[20].Value = 0;// iscontrols
            parameters[21].Value = 0; // isfilmfree
            parameters[22].Value = txtIMDB.Text.Trim();
            parameters[23].Value = "";// logo
            parameters[24].Value = ""; //position logo
            parameters[25].Value = 0;// is free content
            parameters[26].Value = 0;// is free data
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

            VatLidOnPhim.SqlHelper.ExecuteNonQuery(VatLidOnPhim.DAL.getConnectionStringOnPhim(), CommandType.StoredProcedure, "cms_update_ItemMovie_CP", parameters);
            VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUser(Session).ToString(), VatLidOnPhim.LogType.UPDATE.ToString(), "", "OK", id.ToString(), VatLidOnPhim.Utils.GetIP());
            lblError.Text = "Update " + itemfile + " succeed.";
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
			};

            parameters[0].Value = listCate.Trim();
            parameters[1].Value = Convert.ToInt32(cpCode);
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
            parameters[11].Value = 1; // is international
            parameters[12].Value = isview;// luot view
            parameters[13].Value = ddLoaiPhim.SelectedValue;// loai phim
            parameters[14].Value = txtContract.Text.Trim();//contract number
            parameters[15].Value = txtExpired.Value.Trim(); // expired license
            parameters[16].Value = 0; // is monopoly
            parameters[17].Value = 0; // is iscontrols
            parameters[18].Value = 0; // is film free
            parameters[19].Value = 1;
            parameters[20].Value = 1;
            parameters[21].Value = 1;
            parameters[22].Value = txtIMDB.Text.Trim();
            parameters[23].Value = ""; // logo
            parameters[24].Value = ""; // position logo
            parameters[25].Value = 0; // freecontent
            parameters[26].Value = 0; // free data
            VatLidOnPhim.SqlHelper.ExecuteNonQuery(VatLidOnPhim.DAL.getConnectionStringOnPhim(), CommandType.StoredProcedure, "cms_insert_ItemMovie_CP", parameters);

            itemfile = parameters[10].Value.ToString();
            id = parameters[9].Value.ToString();

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

            VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUser(Session).ToString(), VatLidOnPhim.LogType.ADD.ToString(), "", "OK", id.ToString(), VatLidOnPhim.Utils.GetIP());

            lblError.Text = "Đã nhập thành công Film:  " + txtName.Text.Trim() + ".";

        }

        BindDataGrid();
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
        //if (ddlLogo.SelectedValue !="0" && ddlPosition.SelectedValue !="0")
        //{

        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Parameters.AddWithValue("@logo", ddlLogo.SelectedValue);
        //    cmd.Parameters.AddWithValue("@logoposition", ddlPosition.SelectedValue);
        //    cmd.Parameters.AddWithValue("@id", id);

        //    VatLidOnPhim.DAL.GetDataSet("sp_addlogo_itemmoviegroup", cmd);

        //    //SQL = "UPDATE itemmoviegroup SET ItemStatus=2 , DatePublish = getdate(),logo_path='" + ddlLogo.SelectedValue + "',logo_position='" + ddlPosition.SelectedValue + "' WHERE id_group=" + id;
        //    //VatLidOnPhim.DAL.ExecuteQuery(SQL);
        //    lblError.Text = "Thực hiện thành công:" + id;

        //}
        //else
        //{
        //    VatLidOnPhim.MessageBox.Show("Bạn phải chọn logo hoặc vị trí logo");
        //}
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
