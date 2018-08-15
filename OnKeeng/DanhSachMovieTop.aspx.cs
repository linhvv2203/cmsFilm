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
using VatLidOnPhim;
using System.Text.RegularExpressions;
using ICSharpCode.SharpZipLib.Zip;
using System.Configuration;

public partial class OnPhim_DanhSachMovieTop : System.Web.UI.Page
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


        if (!IsPostBack)
        {
            BindData();
            BindDataGrid();
            //ddlTypeShow.SelectedValue = "1";
        }
    }
    #endregion

    #region Grid of page
    private void BindDataGrid()
    {
        try
        {
            DataSet ds = null;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@type", 1);
            cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue);
            cmd.Parameters.AddWithValue("@keyword", txtKeyword.Text.Trim());

            ds = VatLidOnPhim.DAL.GetDataSet("[cms_getMovieTop_New]", cmd);
            dgrCommon.DataSource = ds;

            dgrCommon.DataKeyField = "ID";
            dgrCommon.AutoGenerateColumns = false;
            dgrCommon.DataBind();

            lblTotalRecords.Text = "Tổng số bản ghi: " + dgrCommon.Items.Count.ToString() + " trên tổng số " + dv.Count.ToString();
        }
        catch (Exception e)
        {
            VatLidOnPhim.DAL.ExceptionProcess(e);
        }

    }
    #endregion

    #region bin data load page
    private void BindData()
    {

        //ListItem lstTypeShow = new ListItem("--Tất cả--", "0");
        //ddlTypeShow.Items.Add(lstTypeShow);
        //lstTypeShow = new ListItem("WAP-APP", "1");
        //ddlTypeShow.Items.Add(lstTypeShow);
        //lstTypeShow = new ListItem("APP TV", "2");
        //ddlTypeShow.Items.Add(lstTypeShow);

        ListItem liShow = new ListItem("--Đã duyệt --", "2");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Chờ duyệt--", "1");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Xoá--", "0");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("Sysn Fail", "3");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Tất cả--", "4");
        ddlStatus.Items.Add(liShow);

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

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        //id = "0";
        BindDataGrid();
    }
    protected void cmdSearch_Click1(object sender, EventArgs e)
    {
        id = "0";
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

    protected void ddlTypeShow_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindDataGrid();
    }
    protected void dgrCommon_SelectedIndexChanged(object sender, EventArgs e)
    {
        dgrCommon.CurrentPageIndex = 0;
        BindDataGrid();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/onkeeng/ItemmovieTop_data.aspx", false);
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string lstCode = "";

        for (int i = 0; i < dgrCommon.Items.Count; i++)
        {
            if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == true)
            {
                //dgrCommon.DataKeys[i]
                string txtOrder = ((TextBox)dgrCommon.Items[i].FindControl("txtOrder")).Text;

                lstCode = lstCode + dgrCommon.DataKeys[i] + ",";

                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@order", txtOrder);
                cmd.Parameters.AddWithValue("@id", dgrCommon.DataKeys[i]);

                VatLidOnPhim.DAL.GetDataSet("cms_Update_Order_ItemMovieTop", cmd);
            }
        }

        if (lstCode != "")
        {
            lblError.Text = "Cập nhật thành công :" + lstCode;
            BindDataGrid();
        }
    }
    protected void btnPublish_Click(object sender, EventArgs e)
    {
        string lstCode = "";

        for (int i = 0; i < dgrCommon.Items.Count; i++)
        {
            if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == true)
            {

                lstCode = lstCode + dgrCommon.DataKeys[i] + ",";

                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@id", dgrCommon.DataKeys[i]);
                cmd.Parameters.AddWithValue("@status", 2);

                VatLidOnPhim.DAL.GetDataSet("cms_ItemmovieTop_Update_Status", cmd);
            }
        }

        if (lstCode != "")
        {
            lblError.Text = "Cập nhật thành công :" + lstCode;
            BindDataGrid();
        }
    }
    protected void btnUnPublish_Click(object sender, EventArgs e)
    {
        string lstCode = "";

        for (int i = 0; i < dgrCommon.Items.Count; i++)
        {
            if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == true)
            {
                lstCode = lstCode + dgrCommon.DataKeys[i] + ",";

                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@id", dgrCommon.DataKeys[i]);
                cmd.Parameters.AddWithValue("@status", 1);

                VatLidOnPhim.DAL.GetDataSet("cms_ItemmovieTop_Update_Status", cmd);
            }
        }

        if (lstCode != "")
        {
            lblError.Text = "Cập nhật thành công :" + lstCode;
            BindDataGrid();
        }
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        int i;
        string listID = "";
        for (i = 0; i < dgrCommon.Items.Count; i++)
        {

            if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == true)
            {
                if (listID != "") { listID += ","; }
                listID += dgrCommon.DataKeys[i];
            }
        }

        if (listID != "")
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@listID", listID);

            VatLidOnPhim.DAL.GetDataSet("cms_Del_ItemMovieTop", cmd);

            Label1.Text = "Xóa thành công :" + listID;
            BindDataGrid();
        }

    }
}
