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

public partial class OnKeeng_FilmGroups : System.Web.UI.Page
{
    protected string SQL = "";
    protected string id = "";

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
            if (VatLid.DAL.GetRights(Session["USER"].ToString(), FileName) == false)
            {
                Response.Redirect(VatLid.Variables.sWebRoot + "error_info.aspx?err=21");
            }
        }
        else
        {
            Response.Redirect(VatLid.Variables.sWebRoot + "logout.aspx");
        }

        if (Session["USER"] != null)
        {
            if (!Page.IsPostBack)
            {
                VatLidOnPhim.DAL.ResetToken(hiddenToken);
                BindListddl();
                ddlSearch.SelectedValue = "0";
                ddlStatus.SelectedValue = "2";
                dataGrid();


            }
        }
        else
            Response.Redirect(VatLidOnPhim.Variables.sWebRoot + "logout.aspx");
    }

    private void BindListddl()
    {
        ListItem lstStatus = new ListItem("--Tất cả--", "4");
        ddlStatus.Items.Add(lstStatus);
        lstStatus = new ListItem("--Đã duyệt--", "2");
        ddlStatus.Items.Add(lstStatus);
        lstStatus = new ListItem("--Chờ duyệt--", "1");
        ddlStatus.Items.Add(lstStatus);
        lstStatus = new ListItem("--xóa--", "0");
        ddlStatus.Items.Add(lstStatus);
        lstStatus = new ListItem("--Syn Fail--", "3");
        ddlStatus.Items.Add(lstStatus);

        ListItem liSearch = new ListItem("--Tất cả--", "0");
        ddlSearch.Items.Add(liSearch);
        liSearch = new ListItem("ItemCode", "1");
        ddlSearch.Items.Add(liSearch);
        liSearch = new ListItem("ItemName", "2");
        ddlSearch.Items.Add(liSearch);
        liSearch = new ListItem("Description", "3");
        ddlSearch.Items.Add(liSearch);
    }
    private void dataGrid()
    {
        try
        {
            DataSet ds = null;

            ds = BuildFilter();

            dgrCommon.DataSource = ds;

            dgrCommon.DataKeyField = "ID";
            dgrCommon.AutoGenerateColumns = false;
            dgrCommon.DataBind();
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
            cmd.Parameters.AddWithValue("@type", ddlSearch.SelectedValue);
            cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue);
            ds = VatLidOnPhim.DAL.GetDataSet("CMS_getFilmGroups_v2", cmd);
        }
        catch
        {
            return null;
        }
        return ds;
    }
    #endregion

    public static string getName(string id)
    {
        DataSet ds = null;
        string result = "";
        try
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id));

            ds = VatLidOnPhim.DAL.GetDataSet("cms_filmgroups_getname", cmd);

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
    protected void cmdSearch_Click1(object sender, EventArgs e)
    {
        id = "";
        dataGrid();
    }
    protected void btnCmdAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/onkeeng/ItemMovie_Groups_Data.aspx");
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

                VatLidOnPhim.DAL.GetDataSet("cms_FilmGroups_update_status", cmd);
            }

            lbError.Text = "Đã thực hiện thành công:" + sTemp;
        }
        else
        {
            VatLidOnPhim.MessageBox.Show("Bạn phải chọn một mục để thực hiện");
        }
        dataGrid();
    }

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

                VatLidOnPhim.DAL.GetDataSet("cms_FilmGroups_update_status", cmd);
            }

            lbError.Text = "Đã thực hiện thành công:" + sTemp;
        }
        else
        {
            VatLidOnPhim.MessageBox.Show("Bạn phải chọn một mục để thực hiện");
        }
        dataGrid();
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

                VatLidOnPhim.DAL.GetDataSet("cms_FilmGroups_update_status", cmd);
            }

            lbError.Text = "Đã thực hiện thành công:" + sTemp;
        }
        else
        {
            VatLidOnPhim.MessageBox.Show("Bạn phải chọn một mục để thực hiện");
        }
        dataGrid();
    }

    protected void btnSetHome_Click(object sender, EventArgs e)
    {
        int i;
        string sTemp = "";
        for (i = 0; i < dgrCommon.Items.Count; i++)
        {

            if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == true)
            {

                if (sTemp != "") { sTemp += ","; }
                sTemp += dgrCommon.DataKeys[i];
                //if (Session["USERID"].ToString() != null && Session["USERID"].ToString() != "")
                //{
                //    VatLidOnPhim.DAL.UserLogs_InsertBatch(Convert.ToInt32(Session["USERID"].ToString()), VatLidOnPhim.Utils.getUser(Session).ToString(), dgrCommon.DataKeys[i].ToString(), VatLidOnPhim.LogTypeNew.PUBLISH);

                //}
                //VatLidOnPhim.DAL.UserLogs_InsertBatch(Convert.ToInt32(Session["USERID"].ToString()), VatLidOnPhim.Utils.getUser(Session).ToString(), sTemp, VatLidOnPhim.LogTypeNew.PUBLISH);
                VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUser(Session).ToString(), VatLidOnPhim.LogType.ShowHome.ToString(), getName(dgrCommon.DataKeys[i].ToString()), "OK", dgrCommon.DataKeys[i].ToString(), VatLidOnPhim.Utils.GetIP());
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

                VatLidOnPhim.DAL.GetDataSet("cms_FilmGroups_update_status", cmd);
            }

            lbError.Text = "Đã thực hiện thành công:" + sTemp;
        }
        else
        {
            VatLidOnPhim.MessageBox.Show("Bạn phải chọn một mục để thực hiện");
        }
        dataGrid();
    }

    protected void btnHideHome_Click(object sender, EventArgs e)
    {
        int i;
        string sTemp = "";
        for (i = 0; i < dgrCommon.Items.Count; i++)
        {

            if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == true)
            {

                if (sTemp != "") { sTemp += ","; }
                sTemp += dgrCommon.DataKeys[i];
                //if (Session["USERID"].ToString() != null && Session["USERID"].ToString() != "")
                //{
                //    VatLidOnPhim.DAL.UserLogs_InsertBatch(Convert.ToInt32(Session["USERID"].ToString()), VatLidOnPhim.Utils.getUser(Session).ToString(), dgrCommon.DataKeys[i].ToString(), VatLidOnPhim.LogTypeNew.PUBLISH);

                //}
                //VatLidOnPhim.DAL.UserLogs_InsertBatch(Convert.ToInt32(Session["USERID"].ToString()), VatLidOnPhim.Utils.getUser(Session).ToString(), sTemp, VatLidOnPhim.LogTypeNew.PUBLISH);
                VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUser(Session).ToString(), VatLidOnPhim.LogType.HideHome.ToString(), getName(dgrCommon.DataKeys[i].ToString()), "OK", dgrCommon.DataKeys[i].ToString(), VatLidOnPhim.Utils.GetIP());
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

                VatLidOnPhim.DAL.GetDataSet("cms_FilmGroups_update_status", cmd);
            }

            lbError.Text = "Đã thực hiện thành công:" + sTemp;
        }
        else
        {
            VatLidOnPhim.MessageBox.Show("Bạn phải chọn một mục để thực hiện");
        }
        dataGrid();
    }

    protected void ItemsGrid_TapPhim(Object sender, DataGridCommandEventArgs e)
    {

        // Set the EditItemIndex property to the index of the item clicked 
        // in the DataGrid control to enable editing for that item. Be sure
        // to rebind the DateGrid to the data source to refresh the control.
        //ItemsGrid.EditItemIndex = e.Item.ItemIndex;
        //BindGrid();
        int Id = (int)dgrCommon.DataKeys[(int)e.Item.ItemIndex];

        Response.Redirect("~/onkeeng/FilmGroups_listMovie.aspx?idFilmGroup=" + Id);

    }

    protected void Grid_AddPhim(object source, DataGridCommandEventArgs e)
    {

        int Id = (int)dgrCommon.DataKeys[(int)e.Item.ItemIndex];

        Response.Redirect("~/onkeeng/ItemMovie_Groups.aspx?id=" + Id);

    }
    protected void dgrCommon_Cancel(object source, DataGridCommandEventArgs e)
    {

        int Id = (int)dgrCommon.DataKeys[(int)e.Item.ItemIndex];

        Response.Redirect("~/onkeeng/ItemMovie_Groups_Data.aspx?type=edit&id=" + Id);

    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        dataGrid();
    }
    protected void dgrCommon_SelectedIndexChanged(object sender, EventArgs e)
    {
        dgrCommon.CurrentPageIndex = 0;
        dataGrid();
    }
    private void dgrCommon_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dgrCommon.CurrentPageIndex = e.NewPageIndex;

        dataGrid();
    }
    private void InitializeComponent()
    {
        this.dgrCommon.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgrCommon_PageIndexChanged);
    }

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
}
