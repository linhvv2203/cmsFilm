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
using VatLid;


public partial class SYS_user_Menu : System.Web.UI.Page
{
    protected string sID = "";
    protected string sFilterCategory = "";
    protected string SQL = "";
    protected DataView dv = null;
    protected ArrayList al = new ArrayList();
    protected string FileName = "";
    protected void Page_Load(object sender, System.EventArgs e)
    {
        sID = Request.QueryString["id"];
        if (Utils.IsNumeric(sID))
        {
            sID = sID;
        }
        else
        {
            sID = "0";
        }

        #region:"check permission to access system and also permission to access system function"
        string P_I = Request.ServerVariables["PATH_INFO"];
        string[] aPI = P_I.Split('/');
        int iLength = aPI.Length;
        FileName = aPI[iLength - 1];
        if (Session["USER"] != null)
        {
            if (DAL.GetRights(Session["USER"].ToString(), FileName) == false)
            {
                Response.Redirect(VatLid.Variables.sWebRoot + "error_info.aspx?err=21");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    BindDataControl();
                    BindData();
                    BindDataSysFunc();
                    getAllChecked();
                }
            }
        }
        else
            Response.Redirect(VatLid.Variables.sWebRoot + "login.aspx");
        #endregion


    }

    protected override void OnError(EventArgs e)
    {

        if (typeof(System.Web.HttpRequestValidationException) == Server.GetLastError().GetType())
        {
            string strErrMsg = Server.GetLastError().Message;

            VatLid.DAL.ExceptionProcess1(strErrMsg);

            Response.Redirect(VatLid.Variables.sWebRoot + "error_info.aspx?err=33");

        }


        if (typeof(System.Web.HttpException) == Server.GetLastError().GetType())
        {
            string strErrMsg = Server.GetLastError().Message;

            VatLid.DAL.ExceptionProcess1(strErrMsg);

            Response.Redirect(VatLid.Variables.sWebRoot + "error_info.aspx?err=34");

        }


        base.OnError(e);
    }

    private void BindDataControl()
    {

        ListItem litype = new ListItem("--Tất cả--", "0");
        ddlSearch.Items.Add(litype);
        litype = new ListItem("Tên Menu", "1");
        ddlSearch.Items.Add(litype);
        litype = new ListItem("Nhóm Menu", "2");
        ddlSearch.Items.Add(litype);
        litype = new ListItem("Miêu tả", "3");
        ddlSearch.Items.Add(litype);

        SQL = "CategoriesMenu WHERE ParentID=0 and CategoryStatus=2 order by CategoryOrder desc";
        VatLid.DAL.FillDataToDropdownList(ddlCategory, SQL, "ID,CategoryName");
        ListItem liCategory1 = new ListItem("==Danh mục gốc==", "0");
        ddlCategory.Items.Add(liCategory1);
        ddlCategory.SelectedValue = "0" ;


    }
    private string BuildFilter()
    {
        string sFilterCategory = "";
        SQL = "SELECT * FROM CategoriesMenu WHERE 1=1 ";

        #region lay du lieu ParentID
        if (Convert.ToInt32(ddlCategory.SelectedItem.Value) == 0)
            sFilterCategory = " and ParentID = 0";
        else
            sFilterCategory = " AND ParentID=" + Convert.ToInt32(ddlCategory.SelectedItem.Value);
        SQL += sFilterCategory;
        #endregion

        string Keyword = VatLid.Utils.safeString(txtKeyword.Text.Trim());

        #region Du lieu lay theo Keyword
        if (txtKeyword.Text != "")
        {
            switch (Convert.ToInt32(ddlSearch.SelectedItem.Value))
            {
                case 0:
                    SQL += " AND (CHARINDEX(N'" + Keyword + "',CategoryName)<>0";
                    SQL += " OR CHARINDEX(N'" + Keyword + "',CategoryForder)<>0";
                    SQL += " OR CHARINDEX(N'" + Keyword + "',CategoryLink)<>0)";
                    break;
                case 1:
                    SQL += " AND CHARINDEX(N'" + Keyword + "',CategoryName)<>0";
                    break;
                case 2:
                    SQL += " AND CHARINDEX(N'" + Keyword + "',CategoryForder)<>0";
                    break;
                case 3:
                    SQL += " AND CHARINDEX(N'" + Keyword + "',CategoryLink)<>0";
                    break;
            }
        }
        #endregion

        SQL += " ORDER BY CategoryOrder DESC";
        return SQL;
    }

    private void BindData()
    {
        SQL = "SELECT top(1) * FROM Users WHERE ID=" + sID;
        try
        {
            dgrNguoiDung.DataSource = VatLid.DAL.CreateDataView(SQL);

            VatLid.DAL.FetchDataGridColumn(dgrNguoiDung, "Tài khoản", "UserName");
            VatLid.DAL.FetchDataGridColumn(dgrNguoiDung, "Họ tên", "UserRealName");
            VatLid.DAL.FetchDataGridColumn(dgrNguoiDung, "Thuộc nhóm", "UserGroupID");
            VatLid.DAL.FetchDataGridColumn(dgrNguoiDung, "Trạng thái", "UserStatus");

            dgrNguoiDung.DataKeyField = "ID";
            dgrNguoiDung.AutoGenerateColumns = false;
            dgrNguoiDung.DataBind();
        }
        catch (Exception e)
        {
            VatLid.DAL.ExceptionProcess(e);
        }
     
    }

    private void BindDataSysFunc()
    {

        try
        {
            SQL = BuildFilter();

            dgrCommon.DataSource = VatLid.DAL.CreateDataView(SQL);

            VatLid.DAL.FetchDataGridColumn(dgrCommon, "Danh mục menu", "CategoryName");
            VatLid.DAL.FetchDataGridColumn(dgrCommon, "Nhóm", "CategoryForder");
            VatLid.DAL.FetchDataGridColumn(dgrCommon, "Mô tả", "CategoryLink");

            VatLid.DAL.FetchDataGridColumn(dgrCommon, "Sửa", "ID", "CategoriesMenu_data.aspx?id={0}&type=edit", VatLid.Variables.EditTitle, 0);
            VatLid.DAL.FetchDataGridColumn(dgrCommon, "Xoá", "ID", "CategoriesMenu_data.aspx?id={0}&type=delete", VatLid.Variables.DeleteTitle, 0);

            dgrCommon.DataKeyField = "ID";
            dgrCommon.AutoGenerateColumns = false;
            dgrCommon.DataBind();
            lblTotalRecords.Text = "Tổng số bản ghi: " + dgrCommon.Items.Count.ToString() + " ";
        }
        catch (Exception e)
        {
            VatLid.DAL.ExceptionProcess(e);
        }

    }

    private void getAllChecked()
    {
        int i;
        string sTemp = "";
        for (i = 0; i < dgrCommon.Items.Count; i++)
        {
            sTemp = "SELECT UserID from viwRightsMenuUserGroups WHERE MenuID=" + dgrCommon.DataKeys[i] + " AND UserID=" + sID + "AND RightsStatus=1";
            al = DAL.GetDataReaderToArrayList(sTemp);
            if (al.Count != 0)
                ((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked = true;
        }
    }

    private void dgrCommon_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dgrCommon.CurrentPageIndex = e.NewPageIndex;
        BindData();
        BindDataSysFunc();
        getAllChecked();
    }
    private void dgrNguoiDung_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dgrNguoiDung.CurrentPageIndex = e.NewPageIndex;
        BindData();
        BindDataSysFunc();
        getAllChecked();
    }
    public static string getGroupID(string id)
    {
        String sql = "select usergroupid from Users where id=" + id;
        String[] dl = VatLid.DAL.GetDataReaderToStringList(sql);
        if (dl != null)
        {
            return dl[0];
        }
        return "Chưa xác định";
    }
    protected void cmdSave_Click(object sender, System.EventArgs e)
    {
        string sUserGroupID = getGroupID(sID);
        int i;
        string sTemp = "";
        int skey;
        for (i = 0; i < dgrCommon.Items.Count; i++)
        {

            if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == true)
            {

                skey = Convert.ToInt32(dgrCommon.DataKeys[i]);

                SQL = "SELECT MenuID from RightsMenu WHERE UserID=" + sID + " And MenuID=" + skey;
                ArrayList al = VatLid.DAL.GetDataReaderToArrayList(SQL);
                string[][] arrReturn = (string[][])al.ToArray(typeof(string[]));


                if (al.Count == 0)
                {
                    SQL = "INSERT INTO RightsMenu(UserGroupID,UserID,MenuID,RightsStatus) VALUES(" + sUserGroupID + "," + sID + "," + skey + ",1)";
                    VatLid.DAL.ExecuteQuery(SQL);
                    VatLid.DAL.INSERT_USER_LOG_NEW(VatLid.Utils.getUserId(Session).ToString(), VatLid.LogType.ADD.ToString(), VatLid.DAL.getCategoryID(FileName), "OK", sTemp, VatLid.Utils.GetIP());
                }
                else
                {
                    if (sTemp != "") { sTemp += ","; }
                    sTemp += dgrCommon.DataKeys[i];
                }
            }
        }
        if (sTemp != "")
        {
            SQL = "UPDATE RightsMenu SET RightsStatus=1 WHERE UserID=" + sID + " AND MenuID in (" + sTemp + ")";
            DAL.ExecuteQuery(SQL);
        }

        sTemp = "";
        for (i = 0; i < dgrCommon.Items.Count; i++)
        {

            if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == false)
            {

                if (sTemp != "") { sTemp += ","; }
                sTemp += dgrCommon.DataKeys[i];

            }

        }
        if (sTemp != "")
        {
            SQL = "UPDATE RightsMenu SET RightsStatus=0 WHERE UserID=" + sID + " AND MenuID in (" + sTemp + ")";
            DAL.ExecuteQuery(SQL);
            VatLid.DAL.INSERT_USER_LOG_NEW(VatLid.Utils.getUserId(Session).ToString(), VatLid.LogType.UPDATE.ToString(), VatLid.DAL.getCategoryID(FileName), "OK", sTemp, VatLid.Utils.GetIP());
        }
        BindData();
        BindDataSysFunc();
        getAllChecked();


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

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.dgrNguoiDung.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgrNguoiDung_PageIndexChanged);
        this.dgrCommon.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgrCommon_PageIndexChanged);

    }
    #endregion
    protected void cmdSearch_Click(object sender, EventArgs e)
    {
        BindData();
        BindDataSysFunc();
        getAllChecked();
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindData();
        BindDataSysFunc();
        getAllChecked();
    }
}
