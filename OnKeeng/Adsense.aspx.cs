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
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using VatLidOnPhim;
using System.Text.RegularExpressions;
using ICSharpCode.SharpZipLib.Zip;
using System.Configuration;

public partial class OnKeeng_Adsense : System.Web.UI.Page
{
    #region Declarre  variable for all page

    protected string id = "";
    protected string sFilterCategory = "";
    protected string SQL = "";
    protected string strCategoryID = "";
    protected string strAlbumID = "";
    protected string strPartnerID = "";

    protected string FileName;
    protected DataView dv = null;
    protected string statusSMS = "";
    protected int statusWEB = 0;
    protected string Status = "2";


    #endregion

    protected override void OnError(EventArgs e)
    {

        if (typeof(System.Web.HttpRequestValidationException) == Server.GetLastError().GetType())
        {
            string strErrMsg = Server.GetLastError().Message;

            VatLidOnPhim.DAL.ExceptionProcess1(strErrMsg);

            Response.Redirect(VatLidOnPhim.Variables.sWebRoot + "error_info.aspx?err=33");

        }


        if (typeof(System.Web.HttpException) == Server.GetLastError().GetType())
        {
            string strErrMsg = Server.GetLastError().Message;

            VatLidOnPhim.DAL.ExceptionProcess1(strErrMsg);

            Response.Redirect(VatLidOnPhim.Variables.sWebRoot + "error_info.aspx?err=34");
        }
        base.OnError(e);
    }

    #region pageLoad for page
    private void Page_Load(object sender, System.EventArgs e)
    {

        #region:"check permission to access system and also permission to access system function"
        string P_I = Request.ServerVariables["PATH_INFO"];
        string[] aPI = P_I.Split('/');
        int iLength = aPI.Length;
        string FileName = aPI[iLength - 1];
        if (Session["USER"] != null)
        {
            if (VatLid.DAL.GetRights(Session["USER"].ToString(), FileName, 16) == false)
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

        //lblError.Text=
        ///cmdDelete.Attributes.Add("onclick", "javascript: return confirm('You are confirm delete?');");

        if (Session["USER"] != null)
        {
            if (!Page.IsPostBack)
            {
                BindData();
                //ddlStatus.SelectedValue = Status;
                ddlPositionAdsense.SelectedValue = "0";
                ddlCategory.SelectedValue = "0";
                ddlResponsive.SelectedValue = "0";
                ViewState["IDs"] = ""; //Xoa viewstate
                BindDataGrid();

            }
        }
        else
            Response.Redirect(VatLidOnPhim.Variables.sWebRoot + "logout.aspx");
    }
    #endregion

    #region Grid of page
    private void BindDataGrid()
    {
        try
        {
            DataSet ds = Builfilter();
            dgrCommon.DataSource = ds;

            dgrCommon.DataKeyField = "ID";
            dgrCommon.AutoGenerateColumns = false;
            dgrCommon.DataBind();
            int a = dgrCommon.Items.Count;
            lblTotalRecords.Text = "Tổng số bản ghi: " + dgrCommon.Items.Count.ToString() + " trên tổng số " + dv.Count.ToString();
        }
        catch (Exception e)
        {
            VatLidOnPhim.DAL.ExceptionProcess(e);
        }

    }
    #endregion

    #region bin data load page

    private DataSet Builfilter()
    {
        DataSet ds = null;
        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@keyword", txtKeyword.Text.Trim());
        cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue);
        cmd.Parameters.AddWithValue("@type", ddlImages.SelectedValue);
        cmd.Parameters.AddWithValue("@categoryID", ddlCategory.SelectedValue);
        cmd.Parameters.AddWithValue("@positionID", ddlPositionAdsense.SelectedValue);
        cmd.Parameters.AddWithValue("@responsiveID", ddlPositionAdsense.SelectedValue);

        ds = VatLidOnPhim.DAL.GetDataSet("cms_getAdsense", cmd);

        return ds;
    }
    private void BindData()
    {
        ListItem liImages = new ListItem("--Tất cả--", "0");
        ddlImages.Items.Add(liImages);
        liImages = new ListItem("ID", "1");
        ddlImages.Items.Add(liImages);
        liImages = new ListItem("Name", "2");
        ddlImages.Items.Add(liImages);
        liImages = new ListItem("Description", "3");
        ddlImages.Items.Add(liImages);

        ListItem liShow = new ListItem("--Đã duyệt --", "2");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Chờ duyệt--", "1");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Xoá--", "0");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Tất cả--", "3");
        ddlStatus.Items.Add(liShow);

        ListItem lstResponsive = new ListItem("--Chọn responsive--", "0");
        ddlResponsive.Items.Add(lstResponsive);
        lstResponsive = new ListItem("--Web--", "1");
        ddlResponsive.Items.Add(lstResponsive);
        lstResponsive = new ListItem("--Wap--", "2");
        ddlResponsive.Items.Add(lstResponsive);
        lstResponsive = new ListItem("--App--", "3");
        ddlResponsive.Items.Add(lstResponsive);

        SQL = "Adsense_position_phimkeeng WHERE status=2 order by id asc";
        VatLidOnPhim.DAL.FillDataToDropdownList(ddlPositionAdsense, SQL, "ID,name");
        ListItem lstAdsense = new ListItem("Vị trí Banner", "0");
        ddlPositionAdsense.Items.Add(lstAdsense);

        SQL = "Categories_ItemMovie WHERE 1=1 order by id asc";
        VatLidOnPhim.DAL.FillDataToDropdownList(ddlCategory, SQL, "id,CategoryName");
        ListItem lstCategory = new ListItem("---chọn chuyên mục---", "0");
        ddlCategory.Items.Add(lstCategory);

    }
    #endregion

    #region all action this page
    private void dgrCommon_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dgrCommon.CurrentPageIndex = e.NewPageIndex;
        BindDataGrid();
    }

    private void cmdSearch_Click(object sender, System.EventArgs e)
    {
        dgrCommon.CurrentPageIndex = 0;
        BindDataGrid();
    }



    private void InitializeComponent()
    {
        this.dgrCommon.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgrCommon_PageIndexChanged);
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

    protected void btnDuyet_Click(object sender, EventArgs e)
    {
        int i;
        string sTemp = "";
        for (i = 0; i < dgrCommon.Items.Count; i++)
        {

            if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == true)
            {

                if (sTemp != "") { sTemp += ","; }
                sTemp += dgrCommon.DataKeys[i];

            }

        }
        if (sTemp != "")
        {
            SQL = "UPDATE ADV SET Status=2  WHERE ID in (" + sTemp + ")";
            VatLidOnPhim.DAL.ExecuteQuery(SQL);
            lblError.Text = "Duyệt thành công:" + sTemp;
        }
        else
        {

            VatLidOnPhim.MessageBox.Show("You must choose one item to do");

        }
        BindDataGrid();
    }
    protected void btnGoDuyet_Click(object sender, EventArgs e)
    {
        int i;
        string sTemp = "";
        for (i = 0; i < dgrCommon.Items.Count; i++)
        {

            if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == true)
            {

                if (sTemp != "") { sTemp += ","; }
                sTemp += dgrCommon.DataKeys[i];

            }

        }
        if (sTemp != "")
        {
            SQL = "UPDATE ADV SET Status=1  WHERE ID in (" + sTemp + ")";
            VatLidOnPhim.DAL.ExecuteQuery(SQL);
            lblError.Text = "Gỡ Duyệt thành công:" + sTemp;
        }
        else
        {

            VatLidOnPhim.MessageBox.Show("You must choose one item to do");

        }
        BindDataGrid();
    }
    protected void ddlWorking_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDataGrid();
    }
    protected void ddlImages_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDataGrid();

    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDataGrid();
    }

    protected void cmdSearch_Click1(object sender, EventArgs e)
    {
        id = "0";
        BindDataGrid();
    }


    protected void dgrCommon_SelectedIndexChanged(object sender, EventArgs e)
    {
        dgrCommon.CurrentPageIndex = 0;
        BindDataGrid();
    }
    protected void ddlcp_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDataGrid();
    }

    private string getTitle(int id)
    {
        DataSet ds = null;
        SqlParameter[] parameters = 
			{ 
                new SqlParameter("@ID", SqlDbType.Int),
            };

        parameters[0].Value = id;
        try
        {

            ds = VatLidOnPhim.SqlHelper.ExecuteDataset(VatLidOnPhim.DAL.getConnectionStringOnPhim(), CommandType.StoredProcedure, "select_ItemVideo_GetNambyID", parameters);

        }
        catch
        {

        }
        return ds.Tables[0].Rows[0][0].ToString();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        for (int i = 0; i < dgrCommon.Items.Count; i++)
        {
            string txtCode = ((Literal)dgrCommon.Items[i].FindControl("txtCode")).Text;
            string txtOrder = ((TextBox)dgrCommon.Items[i].FindControl("txtOrder")).Text;


            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@order", txtOrder);
            cmd.Parameters.AddWithValue("@id", txtCode);

            VatLidOnPhim.DAL.GetDataSet("cms_Update_Order_ItemMovieTop", cmd);

        }

    }
    protected void dgrCommon_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            string id = dgrCommon.DataKeys[e.Item.ItemIndex].ToString();
            SqlParameter[] parameters = { 
			new SqlParameter("@ID", SqlDbType.Int),																																					
		};

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@ID", id);
            parameters[0].Value = Convert.ToInt32(VatLidOnPhim.Utils.safeString(id));
            DataSet ds;
            ds = VatLidOnPhim.DAL.GetDataSet("cms_delete_adv", cmd);
            //ds = VatLidOnPhim.SqlHelper.ExecuteDataset(VatLidOnPhim.DAL.getConnectionStringOnPhim(), CommandType.StoredProcedure, "[Categories&Delete]", parameters);
            //VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUserId(Session).ToString(), VatLidOnPhim.LogType.Delete.ToString(), VatLidOnPhim.DAL.getCategoryID(FileName), "OK", id, VatLidOnPhim.Utils.GetIP());
            BindDataGrid();
            lblError.Text = "Thành Công";
        }
        catch (Exception ex)
        {
            lblError.Text = ex.ToString();
        }

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Adsense_Data.aspx?id=" + id);
    }
}