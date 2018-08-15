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
using System.Web.Services;

public partial class DOWNLOAD_ItemVideo : System.Web.UI.Page
{
    #region Declarre  variable for all page

    protected string id = "";
    protected string sFilterCategory = "";
    protected string SQL = "";
    protected string strCategoryID = "0";
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

        //lblError.Text=
        //cmdDelete.Attributes.Add("onclick", "javascript: return confirm('You are confirm delete?');");

        if (Session["USER"] != null)
        {
            if (!Page.IsPostBack)
            {
                BindData();
                ddlStatus.SelectedValue = Status;
                ddlCateogry.SelectedValue = strCategoryID;
                ViewState["IDs"] = ""; //Xoa viewstate
                if (ddlStatus.SelectedValue == "3")
                {
                    //cmdRemove.Enabled = false;
                    //cmdSetShowHome.Enabled = false;
                    //cmdDelete.Enabled = false;
                }
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
        //SQL = BuildFilter();
        try
        {
            //dv = VatLidOnPhim.DAL.CreateDataView(SQL);
            dgrCommon.DataSource = getDataItemMovie();

            String format4 = "<img  src='" + VatLidOnPhim.Variables.ImageLinks7 + "' onclick=\"window.open('" + VatLidOnPhim.Variables.sWebRoot + "OnKeeng/PlayDigital.aspx?ID={0}','','top=100,left=200,height=400,width=400,scrollbars=0,toolbar=0')\" />";
            VatLidOnPhim.DAL.FetchDataGridColumnFormatLast(dgrCommon, "Xem", "ID", format4);

            String format5 = "<img  src='" + VatLidOnPhim.Variables.ImageLinks1 + "' onclick=\"window.open('" + VatLidOnPhim.Variables.sWebRoot + "OnKeeng/PopDelete.aspx?ID={0}','','top=100,left=200,height=300,width=300,scrollbars=0,toolbar=0')\" />";

            VatLidOnPhim.DAL.FetchDataGridColumnFormat(dgrCommon, "Xóa", "ID", format5);

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

    private DataSet getDataItemMovie()
    {
        try
        {
            DataSet ds = null;
            SqlCommand cmd = null;
            string cpCode = "";

            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@partnerName", Session["USER"].ToString());
            ds = VatLidOnPhim.DAL.GetDataSet("sp_getPartnerID", cmd);
            cpCode = ds.Tables[0].Rows[0]["id"].ToString();

            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@Keyword", txtKeyword.Text.Trim());
            cmd.Parameters.AddWithValue("@type", ddlImages.SelectedValue);
            cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue);
            cmd.Parameters.AddWithValue("@categoryID", ddlCateogry.SelectedValue);
            cmd.Parameters.AddWithValue("@partnerID", cpCode);
            cmd.Parameters.AddWithValue("@typeFilm", ddTypePhim.SelectedValue);

            ds = VatLidOnPhim.DAL.GetDataSet("CMS_getItemmvie_CP", cmd);

            return ds;
        }
        catch (Exception)
        {
            return null;
        }
        
    }

    #region bin data load page
    private void BindData()
    {
        VatLidOnPhim.DAL.FillDataToDropdownListStore(ddlCateogry, "CMS_DropDownList_Categories", "id,CategoryName", " status=2 and parentid=0");
        ListItem liVideoCate = new ListItem("==Thể loại==", "0");
        ddlCateogry.Items.Add(liVideoCate);

        string a = Session["PartnerID"].ToString();
        if (a == "1")
        {
            SQL = "Partner_Keeng ";
        }
        else
        {
            SQL = "Partner_Keeng where id=" + Session["PartnerID"].ToString() + "";
        }
        //VatLidOnPhim.DAL.FillDataToDropdownList(ddlcp, SQL, "id,namepartner");
        //ListItem itemcp = new ListItem("--Chọn CP--", "0");
        //ddlcp.Items.Add(itemcp);

        //ddTypePhim.Items.Clear();
        ListItem lstTypePhim = new ListItem("== Tất cả ==", "0");
        ddTypePhim.Items.Add(lstTypePhim);
        lstTypePhim = new ListItem("Phim lẻ", "1");
        ddTypePhim.Items.Add(lstTypePhim);
        lstTypePhim = new ListItem("Phim bộ", "2");
        ddTypePhim.Items.Add(lstTypePhim);

        ListItem liImages = new ListItem("--Tất cả--", "0");
        ddlImages.Items.Add(liImages);
        liImages = new ListItem("ItemCode", "1");
        ddlImages.Items.Add(liImages);
        liImages = new ListItem("ItemName", "2");
        ddlImages.Items.Add(liImages);
        liImages = new ListItem("Description", "3");
        ddlImages.Items.Add(liImages);

        ListItem liShow = new ListItem("--Đã duyệt --", "2");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Chờ duyệt--", "1");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Sync fail--", "3");
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
                VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUser(Session).ToString(), VatLidOnPhim.LogType.Delete.ToString(), getName(dgrCommon.DataKeys[i].ToString()), "OK", dgrCommon.DataKeys[i].ToString(), VatLidOnPhim.Utils.GetIP());
            }

        }

        if (sTemp != "")
        {
            string[] idList = sTemp.Split(',');

            for (int j = 0; j < idList.Length; j++)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@id", idList[j]);
                cmd.Parameters.AddWithValue("@status", 0);
                cmd.Parameters.AddWithValue("@type", 1);

                VatLidOnPhim.DAL.GetDataSet("cms_itemmovie_update_status", cmd);
            }

            lblError.Text = "Đã thực hiện thành công:" + sTemp;
        }
        else
        {
            VatLidOnPhim.MessageBox.Show("Bạn phải chọn một mục để thực hiện");
        }
        BindDataGrid();
    }

    protected void cmdAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("ItemMovie_data.aspx?CategoryID=" + ddlCateogry.SelectedItem.Value);
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        id = "0";
        if (ddlStatus.SelectedValue != "3")
        {

            //cmdRemove.Enabled = true;
            //cmdSetShowHome.Enabled = true;
            //cmdDelete.Enabled = true;
        }

        BindDataGrid();
    }

    protected void cmdSearch_Click1(object sender, EventArgs e)
    {
        id = "0";
        BindDataGrid();
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
                if (Session["USERID"].ToString() != null && Session["USERID"].ToString() != "")
                {
                    VatLidOnPhim.DAL.UserLogs_InsertBatch(Convert.ToInt32(Session["USERID"].ToString()), VatLidOnPhim.Utils.getUser(Session).ToString(), dgrCommon.DataKeys[i].ToString(), VatLidOnPhim.LogTypeNew.PUBLISH);

                }
                //VatLidOnPhim.DAL.UserLogs_InsertBatch(Convert.ToInt32(Session["USERID"].ToString()), VatLidOnPhim.Utils.getUser(Session).ToString(), sTemp, VatLidOnPhim.LogTypeNew.PUBLISH);
                VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUser(Session).ToString(), VatLidOnPhim.LogType.Publish.ToString(), getName(dgrCommon.DataKeys[i].ToString()), "OK", dgrCommon.DataKeys[i].ToString(), VatLidOnPhim.Utils.GetIP());
            }

        }
        if (sTemp != "")
        {
            string[] idList = sTemp.Split(',');

            for (int j = 0; j < idList.Length; j++)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@id", idList[j]);
                cmd.Parameters.AddWithValue("@status", 2);
                cmd.Parameters.AddWithValue("@type", 3);

                VatLidOnPhim.DAL.GetDataSet("cms_itemmovie_update_status", cmd);
            }

            lblError.Text = "Đã thực hiện thành công:" + sTemp;
        }
        else
        {
            VatLidOnPhim.MessageBox.Show("Bạn phải chọn một mục để thực hiện");
        }
        BindDataGrid();
    }

    protected void ddlCateogry_SelectedIndexChanged(object sender, EventArgs e)
    {
        id = "0";

        if (ddlCateogry.SelectedValue.ToString() != "0")
        {


        }
        else
        {

        }
        BindDataGrid();
    }

    protected void dgrCommon_SelectedIndexChanged(object sender, EventArgs e)
    {
        dgrCommon.CurrentPageIndex = 0;
        BindDataGrid();
    }

    public string ExtractAlphaNumericCharacters(string source)
    {
        return Regex.Replace(source, @"[^a-zA-Z0-9 ]", string.Empty, RegexOptions.Compiled);
    }

    protected void cmdIsHome_Click(object sender, EventArgs e)
    {
        string sTemp = "";
        for (int i = 0; i < dgrCommon.Items.Count; i++)
        {
            if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == true)
            {
                if (sTemp != "") { sTemp += ","; }
                sTemp += dgrCommon.DataKeys[i];
            }
        }

        if (sTemp != "")
        {
            string[] idList = sTemp.Split(',');

            for (int j = 0; j < idList.Length; j++)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@id", idList[j]);
                cmd.Parameters.AddWithValue("@status", 1);
                cmd.Parameters.AddWithValue("@type", 2);

                VatLidOnPhim.DAL.GetDataSet("cms_itemmovie_update_status", cmd);
            }

            lblError.Text = "Đã thực hiện thành công:" + sTemp;
        }
        else
        {
            VatLidOnPhim.MessageBox.Show("Bạn phải chọn một mục để thực hiện");
        }
        BindDataGrid();
    }

    protected void cmdUnIsHome_Click(object sender, EventArgs e)
    {
        string sTemp = "";
        for (int i = 0; i < dgrCommon.Items.Count; i++)
        {
            if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == true)
            {
                if (sTemp != "") { sTemp += ","; }
                sTemp += dgrCommon.DataKeys[i];
            }
        }
        if (sTemp != "")
        {
            string[] idList = sTemp.Split(',');

            for (int j = 0; j < idList.Length; j++)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@id", idList[j]);
                cmd.Parameters.AddWithValue("@status", 0);
                cmd.Parameters.AddWithValue("@type", 2);

                VatLidOnPhim.DAL.GetDataSet("cms_itemmovie_update_status", cmd);
            }

            lblError.Text = "Đã thực hiện thành công:" + sTemp;
        }
        else
        {
            VatLidOnPhim.MessageBox.Show("Bạn phải chọn một mục để thực hiện");
        }
        BindDataGrid();
    }

    protected void cmdRemove_Click(object sender, EventArgs e)
    {
        int i;
        string sTemp = "";
        for (i = 0; i < dgrCommon.Items.Count; i++)
        {
            if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == true)
            {
                if (sTemp != "") { sTemp += ","; }
                sTemp += dgrCommon.DataKeys[i];
                VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUser(Session).ToString(), VatLidOnPhim.LogType.Remove.ToString(), getName(dgrCommon.DataKeys[i].ToString()), "OK", dgrCommon.DataKeys[i].ToString(), VatLidOnPhim.Utils.GetIP());
            }
        }

        if (sTemp != "")
        {
            string[] idList = sTemp.Split(',');

            for (int j = 0; j < idList.Length; j++)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@id", idList[j]);
                cmd.Parameters.AddWithValue("@status", 1);
                cmd.Parameters.AddWithValue("@type", 1);

                VatLidOnPhim.DAL.GetDataSet("cms_itemmovie_update_status", cmd);
            }

            lblError.Text = "Đã thực hiện thành công:" + sTemp;
        }
        else
        {
            VatLidOnPhim.MessageBox.Show("Bạn phải chọn một mục để thực hiện");
        }
        BindDataGrid();
    }

    public static string getName(string id)
    {
        DataSet ds = null;
        string result = "";
        try
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id));

            ds = VatLidOnPhim.DAL.GetDataSet("cms_itemmovie_getname", cmd);

            if (ds != null)
            {
                result = ds.Tables[0].Rows[0]["ItemName"].ToString();
            }
            else
            {
                result = "0";
            }
        }
        catch (Exception ex)
        {
        }
        return result;
    }

    protected void ddlcp_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDataGrid();
    }

    protected void ddTypePhim_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDataGrid();
    }

    protected void btnSetTop_Click(object sender, EventArgs e)
    {
        string sTemp = "";
        string checkSetTop = "";
        for (int i = 0; i < dgrCommon.Items.Count; i++)
        {
            if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == true)
            {
                if (sTemp != "") { sTemp += ","; }
                sTemp += dgrCommon.DataKeys[i];
            }
        }

        if (sTemp != "")
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@listID", sTemp);

            DataSet ds = null;
            string imagePath = "";
            string code = "";
            string name = "";

            ds = VatLidOnPhim.DAL.GetDataSet("cms_getMovieTop", cmd);

            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    imagePath = ds.Tables[0].Rows[i]["ItemImage"].ToString();
                    code = ds.Tables[0].Rows[i]["ID"].ToString();
                    name = ds.Tables[0].Rows[i]["ItemName"].ToString();

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Parameters.AddWithValue("@code", code);
                    cmd1.Parameters.AddWithValue("@imagePath", imagePath);
                    cmd1.Parameters.AddWithValue("@name", name);

                    VatLidOnPhim.DAL.GetDataSet("cms_ItemMovieTop_Insert", cmd1);

                }
            }
        }
    }
    protected void dgrCommon_addPart(Object sender, DataGridCommandEventArgs e)
    {
        int Id = (int)dgrCommon.DataKeys[(int)e.Item.ItemIndex];

        Response.Redirect("~/onkeengcp/ItemMovieGroup_addPart.aspx?id=" + Id);

    }
    protected void dgrCommon_createPart(Object sender, DataGridCommandEventArgs e)
    {
        int Id = (int)dgrCommon.DataKeys[(int)e.Item.ItemIndex];

        Response.Redirect("~/onkeengcp/ItemMovieGroup_addPartByitemovieID.aspx?id=" + Id);

    }
    protected void dgrCommon_Delete(Object sender, DataGridCommandEventArgs e)
    {
        int Id = (int)dgrCommon.DataKeys[(int)e.Item.ItemIndex];
    }

}