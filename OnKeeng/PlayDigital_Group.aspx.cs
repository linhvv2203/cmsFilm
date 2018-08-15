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
using VatLidOnPhim;
using VatLidOnBox;

public partial class CLIP_PlayDigital_Group : System.Web.UI.Page
{
    protected string sNgheThu = "";
    protected string sAnhNgheThu = "";
    protected string id = "";
    protected string id_group = "";
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

        id = (Request.QueryString["id"] == null) ? "0" : Request.QueryString["id"];
        id_group = (Request.QueryString["id_group"] == null) ? "0" : Request.QueryString["id_group"];
        SqlParameter[] parameters = { 
			new SqlParameter("@ID", SqlDbType.Int),																																					
		};
        parameters[0].Value = Convert.ToInt32(id_group);
        DataSet ds;
        ds = VatLidOnPhim.SqlHelper.ExecuteDataset(VatLidOnPhim.DAL.getConnectionStringOnPhim(), CommandType.StoredProcedure, "select_ItemVideo_ByID_v2", parameters);

        lblDichVu.Text = "Xem video: " + ds.Tables[0].Rows[0]["ItemName"].ToString();
        lbsapo.Text = "Mô tả: " + ds.Tables[0].Rows[0]["ItemDesc"].ToString();
        //if (ds.Tables.Count > 1)
        //{
        //    if (ds.Tables[1].Rows.Count > 0)
        //    {
        //        string tags = "";
        //        foreach (DataRow dr in ds.Tables[1].Rows)
        //        {
        //            tags += dr["name"].ToString() + ",";
        //        }

        //        if (tags.Length > 0)
        //            lbtag.Text = "Từ khóa: " + tags.Substring(0, tags.Length - 1);
        //    }
        //}
        //string IPClient = GetUser_IP();// System.Web.HttpContext.Current.Request.UserHostAddress.ToString().Trim();
        sNgheThu = getPath(id); //+ "?e=" + SaltedHash.EncodeMD5("vip.phim.onbox" + GetUser_IP() + id_group.Trim()) + "&expires=" + id_group.Trim() + "&ip=" + IPClient.Trim();
        // System.Threading.Thread.Sleep(3000);
        // Response.Redirect(sNgheThu);
        sAnhNgheThu = getPathItemCode(id);
        //Response.Write(getPath(id) + "- " + getPathItemCode(id);

    }
    protected static void GetUser_IP()
    {
        string VisitorsIPAddr = string.Empty;
        if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
        {
            VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
        }
        else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
        {
            VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
        }
        //uip.Text = "Your IP is: " + VisitorsIPAddr;
    }
   




    public static string getPath(string id)
    {
        string sql = "";
        //if (Convert.ToInt32(id) >= 838693)
        //{
        //    sql = "select top(1) 'xemdi/mp4/' + left(dbo.fn_DateTime_To_String(itemdate),8) + '/' + ItemFile from ItemVideo where ID=" + id;
        //}

        //sql = "select top(1) dbo.fn_folder_video_group_cms(ID,id_group)  + ItemFile + '.mp4'  from ItemGroupVideo where ID=" + id;


        String[] dl = VatLidOnPhim.DAL.GetDataReaderToStringList(sql);
        if (dl != null)
        {
            return dl[0];
        }
        else
        {
            return "0";
        }
    }
    public static string getPathItemCode(string id)
    {
        string sql = "";// "select top(1) dbo.fn_folder(ID) +'/'  + ItemImage  from ItemVideo where ID=" + id;
        String[] dl = VatLidOnPhim.DAL.GetDataReaderToStringList(sql);
        if (dl != null)
        {
            return dl[0];
        }
        else
        {
            return "0";
        }
    }

}
