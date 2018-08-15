using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OnKeeng_ListFilm_Synchronous : System.Web.UI.Page
{
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

        btnDelete.Attributes.Add("onclick", "javascript: return confirm('You are confirm delete?');");

        if (Session["USER"] != null)
        {
            if (!Page.IsPostBack)
            {
                BindData();
                ddlStatus.SelectedValue = "0";
                btnDownload.Visible = false;
                ViewState["IDs"] = ""; //Xoa viewstate

                //ddlcpuserlogin.SelectedValue = "0";
                BindDataGrid();

            }
        }
        else
            Response.Redirect(VatLidOnPhim.Variables.sWebRoot + "logout.aspx");
    }

    private void BindData()
    {
        ListItem liShow = new ListItem("--chờ duyệt để đồng bộ thông tin từ VAS --", "0");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Trạng thái chuyển để bắt đầu đồng bộ thông tin từ VAS--", "1");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Trạng thái đang được xử lý đồng bộ ruột phim--", "2");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Trang thái đã xử lý đồng bộ phim về xong--", "3");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Download fail--", "5");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("Xóa", "4");
        ddlStatus.Items.Add(liShow);

        ListItem liType = new ListItem("--- tất cả ---","0");
        ddlType.Items.Add(liType);
        liType = new ListItem("--- phim lẻ ---", "2");
        ddlType.Items.Add(liType);
        liType = new ListItem("--- phim bộ ---", "3");
        ddlType.Items.Add(liType);
        liType = new ListItem("--- trailer ---", "7");
        ddlType.Items.Add(liType);

    }
    private void BindDataGrid()
    {
        try
        {
            dgrCommon.DataSource = BuildFilter().Tables[0];

            dgrCommon.DataKeyField = "ID";
            dgrCommon.AutoGenerateColumns = false;
            dgrCommon.DataBind();

            //lblTotalRecords.Text = "Tổng số bản ghi: " + dgrCommon.Items.Count.ToString() + " trên tổng số " + dv.Count.ToString();
        }
        catch (Exception e)
        {
            VatLidOnPhim.DAL.ExceptionProcess(e);
        }

    }
    private DataSet BuildFilter()
    {
        DataSet ds = null;
        try
        {

            string Keyword = txtKeyword.Text.Trim();
            Keyword = VatLidOnPhim.Utils.safeString(Keyword);
            if (Keyword == "")
            {
                Keyword = " ";
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@Keyword", Keyword);
            cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue);
            cmd.Parameters.AddWithValue("@type", ddlType.SelectedValue);
            ds = VatLidOnPhim.DAL.GetDataSet("cms_CrawlerMedia_select", cmd);
        }
        catch
        {
            return null;
        }
        return ds;
    }
    protected void cmdSearch_Click(object sender, EventArgs e)
    {
        BindDataGrid();
    }
    protected void btnSynchronous_Click(object sender, EventArgs e)
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
            SQL = "UPDATE [OnboxPhim].[dbo].[CrawlerMedia]  SET process_status =1  WHERE ID in (" + sTemp + ")";
            VatLidOnPhim.DAL.ExecuteQuery(SQL);
            //VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUser(Session).ToString(), VatLidOnPhim.LogType.HideHome.ToString(), VatLidOnPhim.DAL.getCategoryID(FileName), "OK", sTemp, VatLidOnPhim.Utils.GetIP());
            lblError.Text = "Update succeed:" + sTemp;
        }
        else
        {

            VatLidOnPhim.MessageBox.Show("You must choose one item to do");

        }
        BindDataGrid();
    }

    protected void btnDownload_Click(object sender, EventArgs e)
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
            SQL = "UPDATE [OnboxPhim].[dbo].[CrawlerMedia]  SET process_status =100  WHERE ID in (" + sTemp + ")";
            VatLidOnPhim.DAL.ExecuteQuery(SQL);
            //VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUser(Session).ToString(), VatLidOnPhim.LogType.HideHome.ToString(), VatLidOnPhim.DAL.getCategoryID(FileName), "OK", sTemp, VatLidOnPhim.Utils.GetIP());
            lblError.Text = "Update succeed:" + sTemp;
        }
        else
        {

            VatLidOnPhim.MessageBox.Show("You must choose one item to do");

        }
        BindDataGrid();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
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
            SQL = "UPDATE [OnboxPhim].[dbo].[CrawlerMedia]  SET process_status =4  WHERE ID in (" + sTemp + ")";
            VatLidOnPhim.DAL.ExecuteQuery(SQL);
            //VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUser(Session).ToString(), VatLidOnPhim.LogType.HideHome.ToString(), VatLidOnPhim.DAL.getCategoryID(FileName), "OK", sTemp, VatLidOnPhim.Utils.GetIP());
            lblError.Text = "Xóa thành công: " + sTemp;
        }
        else
        {

            VatLidOnPhim.MessageBox.Show("You must choose one item to do");

        }
        BindDataGrid();
    }
    protected void dgrCommon_SelectedIndexChanged(object sender, EventArgs e)
    {
        dgrCommon.CurrentPageIndex = 0;
        BindDataGrid();
    }
    protected void ddlStatus_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStatus.SelectedValue != "0")
        {
            btnSynchronous.Visible = false;
        }
        else
        {
            btnSynchronous.Visible = true;
        }
        if (ddlStatus.SelectedValue == "5")
        {
            btnDownload.Visible = true;
        }
        else
        {
            btnDownload.Visible = false;
        }
        BindDataGrid();
    }
    protected void ddlType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindDataGrid();
    }
    private void dgrCommon_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dgrCommon.CurrentPageIndex = e.NewPageIndex;
        BindDataGrid();
    }
    private void InitializeComponent()
    {
        this.dgrCommon.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgrCommon_PageIndexChanged);
    }
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: This call is required by the ASP.NET Web Form Designer.
        //
        InitializeComponent();
        base.OnInit(e);
    }
}