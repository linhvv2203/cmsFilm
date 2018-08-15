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


public partial class CLIP_ItemVideo_Group : System.Web.UI.Page
{
    protected string sFilterCategory = "";
    protected string SQL = "";
    protected string strCategoryID = "";

    protected string FileName = "";
    protected DataView dv = null;
    protected string Status = "2";
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

        Status = Request.QueryString["status"];

        if (Session["USER"] != null)
        {

            if (!Page.IsPostBack)
            {
                VatLidOnPhim.DAL.ResetToken(hiddenToken);
                ViewState["IDs"] = ""; //Xoa viewstate
                BindData();
                ddlStatus.SelectedValue = Status;
                BindDataGrid();
            }
        }
        else
            Response.Redirect(VatLidOnPhim.Variables.sWebRoot + "logout.aspx");



    }
    private void BindDataGrid()
    {
        try
        {
            dgrCommon.DataSource = BuildFilter().Tables[0];
            String format4 = "<img  src='" + VatLidOnPhim.Variables.ImageLinks7 + "' onclick=\"window.open('" + VatLidOnPhim.Variables.sWebRoot + "CLIP//PlayDigital_Group.aspx?id_group=&ID={1}','','top=100,left=200,height=300,width=300,scrollbars=0,toolbar=0')\" />";
            //VatLidOnPhim.DAL.FetchDataGridColumnFormatNew(dgrCommon, "Xem","id_groups" ,"ID", format4);
            dgrCommon.DataKeyField = "ID";
            dgrCommon.AutoGenerateColumns = false;
            dgrCommon.DataBind();
            int a = dgrCommon.Items.Count;
            lblTotalRecords.Text = Server.HtmlEncode("Tổng số bản ghi: " + dgrCommon.Items.Count.ToString() + " trên tổng số " + BuildFilter().Tables[0].Rows.Count);
        }
        catch (Exception e)
        {
            VatLidOnPhim.DAL.ExceptionProcess(e);
        }

    }
    #region filter all SQL
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
            cmd.Parameters.AddWithValue("@type", ddlgroups.SelectedValue);
            cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue);
            cmd.Parameters.AddWithValue("@id_group", ddlItemgroup.SelectedValue);
            ds = VatLidOnPhim.DAL.GetDataSet("cms_Itemgroup_select", cmd);
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
        VatLidOnPhim.DAL.FillDataToDropdownListStore(ddlItemgroup, "CMS_DropDownList_Groups", "ID,ItemName", " ItemStatus=2 and ItemOrder=1");
        ListItem liVideoCate = new ListItem("Groups Video", "0");
        ddlItemgroup.Items.Add(liVideoCate);
        ddlItemgroup.SelectedValue = "0";

        ListItem liImages = new ListItem("--Tất cả--", "0");
        ddlgroups.Items.Add(liImages);
        liImages = new ListItem("ID", "1");
        ddlgroups.Items.Add(liImages);
        liImages = new ListItem("Itemname", "2");
        ddlgroups.Items.Add(liImages);
        liImages = new ListItem("Id_group", "3");
        ddlgroups.Items.Add(liImages);

        ListItem liShow = new ListItem("--Đã duyệt --", "2");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Chờ duyệt--", "1");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Xoá--", "0");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Tất cả--", "3");
        ddlStatus.Items.Add(liShow);
    }

    protected void ddlgroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDataGrid();


    }
    protected void cmdSearch_Click1(object sender, EventArgs e)
    {
        if (!VatLidOnPhim.DAL.CheckPermissionByToken(hiddenToken))
        {
            VatLidOnPhim.DAL.ResetToken(hiddenToken);
            return;
        }
        BindDataGrid();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDataGrid();

    }
    protected void cmdAdd_Click(object sender, EventArgs e)
    {

    }
    protected void cmdDelete_Click(object sender, EventArgs e)
    {


    }
    protected void cmdSetShowHome_Click(object sender, EventArgs e)
    {
        try
        {
            if (!VatLidOnPhim.DAL.CheckPermissionByToken(hiddenToken))
            {
                VatLidOnPhim.DAL.ResetToken(hiddenToken);
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
                cmd.Parameters.AddWithValue("@status", 2);
                VatLidOnPhim.DAL.ExecuteNonQuery_pro("cms_videogroup_Publish", cmd);
                //VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUserId(Session).ToString(), VatLidOnPhim.LogType.Delete.ToString(), VatLidOnPhim.DAL.getCategoryID(FileName), "OK", sTemp, VatLidOnPhim.Utils.GetIP());
                //}
                lblError.Text = Server.HtmlEncode("Publish succeed:" + sTemp);

            }
            else
            {
                VatLidOnPhim.MessageBox.Show("Bạn phải chọn một mục để thực hiện");
            }
            BindDataGrid();
        }
        catch
        {

        }


    }
    protected void cmdRemove_Click(object sender, EventArgs e)
    {
        try
        {
            if (!VatLidOnPhim.DAL.CheckPermissionByToken(hiddenToken))
            {
                VatLidOnPhim.DAL.ResetToken(hiddenToken);
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
                cmd.Parameters.AddWithValue("@status", 1);
                VatLidOnPhim.DAL.ExecuteNonQuery_pro("cms_videogroup_Publish", cmd);
                //VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUserId(Session).ToString(), VatLidOnPhim.LogType.Delete.ToString(), VatLidOnPhim.DAL.getCategoryID(FileName), "OK", sTemp, VatLidOnPhim.Utils.GetIP());
                //}
                lblError.Text = Server.HtmlEncode("UnPublish succeed:" + sTemp);

            }
            else
            {
                VatLidOnPhim.MessageBox.Show("Bạn phải chọn một mục để thực hiện");
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
        //BindDataGrid();
    }
    protected void dgrCommon_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        //try
        //{
        //    string id = dgrCommon.DataKeys[e.Item.ItemIndex].ToString();

        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Parameters.AddWithValue("@ID", id);

        //    DataSet ds;
        //    ds = DAL.GetDataSet("Categories&Delete", DAL.getConnectionString1(), cmd);

        //    VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUserId(Session).ToString(), VatLidOnPhim.LogType.Delete.ToString(), VatLidOnPhim.DAL.getCategoryID("FileName"), "OK", id, VatLidOnPhim.Utils.GetIP());
        //    //BindDataGrid();
        //    lbError.Text = "Thành Công";
        //}
        //catch (Exception ex)
        //{
        //    lbError.Text = ex.ToString();
        //}
    }
    protected void dgrCommon_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgrCommon.CurrentPageIndex = e.NewPageIndex;
        BindDataGrid();
    }
    protected void ddlItemgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDataGrid();

    }
}
