using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OnKeeng_PlayVideoTrailer : System.Web.UI.Page
{
    protected string video_path = "";
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

        string id = (Request.QueryString["id"] == null) ? "0" : Request.QueryString["id"];

        if (!IsPostBack)
        {
            getData(id);
        }
    }

    private void getData(string id)
    {
        try
        {
            SqlParameter[] parameters = {
            new SqlParameter("@ItemCode", SqlDbType.VarChar),
            };
            parameters[0].Value = id;

            DataSet ds;
            ds = VatLidOnPhim.SqlHelper.ExecuteDataset(VatLidOnPhim.DAL.getConnectionStringOnPhim(), CommandType.StoredProcedure, "getItemmovie_trailer", parameters);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                video_path = ds.Tables[0].Rows[0]["video_path"].ToString();

            }
        }
        catch
        {
        }
    }
}