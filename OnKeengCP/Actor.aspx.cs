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


public partial class OnPhim_Actor : System.Web.UI.Page
{
    #region declare variable
    protected string sFilterCategory = "";
    protected string SQL = "";
    protected string strCategoryID = "";

    protected string FileName = "";
    protected DataView dv = null;
    protected string Status = "2";
    protected string ListMID = "";
    #endregion

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

        if (Session["USER"] != null)
        {

            if (!Page.IsPostBack)
            {
                VatLid.DAL.ResetToken(hiddenToken);
                ViewState["IDs"] = ""; //Xoa viewstate
                BindData();
                ddlStatus.SelectedValue = Status;
                BindDataGrid();
            }
        }
        else
            Response.Redirect(VatLid.Variables.sWebRoot + "logout.aspx");



    }
    private void BindDataGrid()
    {
        try
        {
            dgrCommon.DataSource = BuildFilter().Tables[0];
            dgrCommon.DataKeyField = "ID";
            dgrCommon.AutoGenerateColumns = false;
            dgrCommon.DataBind();
            int a = dgrCommon.Items.Count;
            lblTotalRecords.Text = Server.HtmlEncode("Tổng số bản ghi: " + dgrCommon.Items.Count.ToString() + " trên tổng số " + BuildFilter().Tables[0].Rows.Count);
        }
        catch (Exception e)
        {
            VatLid.DAL.ExceptionProcess(e);
        }

    }
    #region filter all SQL
    private DataSet BuildFilter()
    {
        DataSet ds = null;
        try
        {

            string Keyword = txtKeyword.Text.Trim();
            Keyword = VatLid.Utils.safeString(Keyword);
            if (Keyword == "")
            {
                Keyword = " ";
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@Keyword", Keyword);
            cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue);
            ds = VatLidOnPhim.DAL.GetDataSet("cms_Actor_select", cmd);
        }
        catch
        {
            return null;
        }
        return ds;
    }
    #endregion

    private void BindData()
    {


        ListItem liShow = new ListItem("--Đã duyệt --", "2");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Chờ duyệt--", "1");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Xoá--", "0");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Tất cả--", "3");
        ddlStatus.Items.Add(liShow);
    }
    protected void cmdAdd_Click(object sender, EventArgs e)
    {
        int i;
        //ListMID = "(";
        for (i = 0; i < dgrCommon.Items.Count; i++)
        {

            if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == true)
            {
                if (ListMID.Length > 0) { ListMID += ","; }
                ListMID += dgrCommon.DataKeys[i];
            }
        }
        //ListMID += ")";

        //BindData();
    }
    protected void cmdDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (!VatLid.DAL.CheckPermissionByToken(hiddenToken))
            {
                VatLid.DAL.ResetToken(hiddenToken);
                return;
            }
            int i;
            string sTemp = "(";
            for (i = 0; i < dgrCommon.Items.Count; i++)
            {

                if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == true)
                {
                    if (sTemp != "(") { sTemp += ","; }
                    sTemp += dgrCommon.DataKeys[i];
                }
            }
            sTemp += ")";
            if (sTemp != "()")
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@idcate", sTemp);
                cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedItem.Value);
                VatLid.DAL.ExecuteNonQuery_pro("cms_wapinlinebox_delete", cmd);
                //VatLid.DAL.INSERT_USER_LOG_NEW(VatLid.Utils.getUserId(Session).ToString(), VatLid.LogType.Delete.ToString(), VatLid.DAL.getCategoryID(FileName), "OK", sTemp, VatLid.Utils.GetIP());
                //}
                lblError.Text = Server.HtmlEncode("Delete succeed:" + sTemp);

            }
            else
            {
                VatLid.MessageBox.Show("Bạn phải chọn một mục để thực hiện");
            }
            BindDataGrid();
        }
        catch
        {

        }

    }
    protected void dgrCommon_SelectedIndexChanged(object sender, EventArgs e)
    {
        dgrCommon.CurrentPageIndex = 0;
        BindDataGrid();
    }
    protected void dgrCommon_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgrCommon.CurrentPageIndex = e.NewPageIndex;
        BindDataGrid();
    }

    protected void cmdSearch_Click1(object sender, EventArgs e)
    {
        if (!VatLid.DAL.CheckPermissionByToken(hiddenToken))
        {
            VatLid.DAL.ResetToken(hiddenToken);
            return;
        }
        BindDataGrid();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void cmdSetShowHome_Click1(object sender, EventArgs e)
    {

    }
    protected void cmdRemove_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
    }
}
