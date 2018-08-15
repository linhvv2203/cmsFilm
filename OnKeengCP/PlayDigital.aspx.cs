using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using VatLid;

public partial class POPUP_PlayDigital : System.Web.UI.Page
{
    protected string sNgheThu = "";
    protected string sAnhNgheThu = "";
    protected string id = "";
    protected string SQL = "";
    protected string resultVip = "1";
    protected string ch = "";
    protected string type = "";
    protected string code = "";
    protected string cid = "";
    protected string view = "";
    protected string encode = "";
    protected string cate_name = "";
    protected string description = "";
    protected string descriptionShort = "";
    protected string ItemName = "";
    protected string Image = "";

    protected string itemfile = "";
    protected string UserID = "";
    protected string UserName = "";
    protected string FolderName = "";
    private string userAgent = "";
    protected string CategoryName = "";
    protected string CategoryURL = "";
    protected string CategoryImg = "";
    protected string URLMP4 = "";
    protected string URLIMG = "";
    protected string URLVIDEO = "";
    protected string URLIMAGE = "";

    protected string url = "";
    protected string categoryurl = "";
    protected string URLChannel = "";
    protected string URLCate = "";
    protected int IsVip;
    protected int line30;
    protected string username;
    protected string channel = "";
    protected string ip = "";
    protected string idcp = "";
    protected string cpid;
    protected string ChannelSale = "";
    protected string Cateid = "";
    protected string Sub = "";
    protected int _OS = 0;
    protected string RefreshToken = "";
    protected string ItemLength1 = "";
    protected string TimePublish = "";
    protected string ItemDirector = "";
    protected string YearProduct = "";
    protected string ItemActor = "";
    protected string Country = "";
    public string sWebRoot = "#";
    protected string Userlogin = "";
    protected int Stt = 0;
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

        id = (Request.QueryString["id"] == null) ? "0" : Request.QueryString["id"];
        SqlParameter[] parameters = { 
			new SqlParameter("@ItemCode", SqlDbType.Int),																																					
		};
        parameters[0].Value = Convert.ToInt32(id);

        DataSet ds;
        ds = VatLidOnPhim.SqlHelper.ExecuteDataset(VatLidOnPhim.DAL.getConnectionStringOnPhim(), CommandType.StoredProcedure, "wap_movie_getMovieDetail_cms", parameters);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            id = ds.Tables[0].Rows[0]["ID"].ToString();

            cate_name = ds.Tables[0].Rows[0]["cate_name"].ToString();

            //cate_url = ds.Tables[0].Rows[0]["cate_url"].ToString();

            cid = ds.Tables[0].Rows[0]["CategoryID"].ToString();
            description = ds.Tables[0].Rows[0]["ItemDesc"].ToString();
            ItemName = ds.Tables[0].Rows[0]["ItemName"].ToString();
            Image = ds.Tables[0].Rows[0]["ItemImage"].ToString();
            itemfile = ds.Tables[0].Rows[0]["ItemFile"].ToString();
            view = ds.Tables[0].Rows[0]["isview"].ToString();
            TimePublish = ds.Tables[0].Rows[0]["TimePublish"].ToString();
            cpid = ds.Tables[0].Rows[0]["PartnerID"].ToString();
            Cateid = ds.Tables[0].Rows[0]["CategoryID"].ToString();
            Userlogin = ds.Tables[0].Rows[0]["UserLogin"].ToString();
            Country = ds.Tables[0].Rows[0]["Country"].ToString();
            //YearProduct = ds.Tables[0].Rows[0]["YearProduct"].ToString();

            //URLVIDEO = VatLid.Security.Encrypt(VatLid.DAL.ReplaceLinkMediaVip(itemfile), "vutoan1912");

            URLVIDEO = itemfile;
            URLIMAGE = Image;

            //Episode
            if (ds.Tables[1].Rows.Count > 0)
            {
                RepeaterEpisode.DataSource = ds.Tables[1];
                RepeaterEpisode.DataBind();
            }
            if (ds.Tables[2].Rows.Count > 0)
            {
                RptActor.DataSource = ds.Tables[2];
                RptActor.DataBind();
            }

            if (ds.Tables[3].Rows.Count > 0)
            {
                RptDirector.DataSource = ds.Tables[3];
                RptDirector.DataBind();
            }
        }


    }
}
