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


public partial class SYS_User_Log : System.Web.UI.Page
{
    protected string strStatus = "";
    protected string sFilterCategory = "";
    protected string SQL = "";
    protected DataView dv = null;
    protected string strToday = "";
    protected string sMonth = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        strStatus = (Request.QueryString["Status"] == null) ? "2" : Request.QueryString["Status"];
        if (Utils.IsNumeric(strStatus))
        {
            strStatus = strStatus;
        }
        else
        {
            strStatus = "0";
        }

        #region Check Permission and Load Data
        string P_I = Request.ServerVariables["PATH_INFO"];
        string[] aPI = P_I.Split('/');
        int iLength = aPI.Length;
        string FileName = aPI[iLength - 1];


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

                    DateTime dtNow = DateTime.Today;

                    string ngay = "";
                    string thang;
                    if (dtNow.Day.ToString().Length == 1)
                    {
                        ngay = "0" + dtNow.Day;
                    }
                    else
                    {
                        ngay = dtNow.Day + "";
                    }
                    if (dtNow.Month.ToString().Length == 1)
                    {
                        thang = "0" + dtNow.Month;
                    }
                    else
                    {
                        thang = dtNow.Month + "";
                    }
                    strToday = ngay + "/" + thang + "/" + dtNow.Year;

                    txtDateStart.Text = ngay + "/" + thang + "/" + dtNow.Year;
                    txtDateEnd.Text = ngay + "/" + thang + "/" + dtNow.Year;


                    BindData();

                    BindGridData();
                }
            }
        }
        else
            Response.Redirect(VatLid.Variables.sWebRoot + "logout.aspx");
        #endregion
    }
    private string getType_Date(string txtDate)
    {
        string sDate = txtDate.Trim().Replace("/", "");
        return sDate.Substring(sDate.Length - 4) + sDate.Substring(2, 2) + sDate.Substring(0, 2);
    }
    private void dgrCommon_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dgrCommon.CurrentPageIndex = e.NewPageIndex;
        BindGridData();
    }
    private void BindData()
    {

        ListItem liTypesearch = new ListItem("--All--", "0");
        ddlTypeSearch.Items.Add(liTypesearch);
        liTypesearch = new ListItem("Tài khoản", "1");
        ddlTypeSearch.Items.Add(liTypesearch);
        liTypesearch = new ListItem("Tên Log", "2");
        ddlTypeSearch.Items.Add(liTypesearch);
        liTypesearch = new ListItem("Miêu tả", "3");
        ddlTypeSearch.Items.Add(liTypesearch);
        liTypesearch = new ListItem("Trạng thái", "4");
        ddlTypeSearch.Items.Add(liTypesearch);

    }
    private void BindGridData()
    {
        SQL = BuildFilter();
        try
        {

            dv = VatLid.DAL.CreateDataView(SQL);
            dgrCommon.DataSource = dv;

            VatLid.DAL.FetchDataGridColumn(dgrCommon, "Tài khoản", "UserID");
            VatLid.DAL.FetchDataGridColumn(dgrCommon, "Tên Log", "ActionName");
            VatLid.DAL.FetchDataGridColumn(dgrCommon, "Miêu tả", "ActionDesc");
            VatLid.DAL.FetchDataGridColumn(dgrCommon, "Trạng thái", "ActionStatus");
            VatLid.DAL.FetchDataGridColumn(dgrCommon, "Ngày gửi", "ActionDate");



            dgrCommon.DataKeyField = "ID";
            dgrCommon.AutoGenerateColumns = false;
            dgrCommon.DataBind();

            lblTotalRecords.Text = "Tổng số bản ghi đang xem: " + dgrCommon.Items.Count.ToString() + " trên tổng số " + dv.Count.ToString();
        }
        catch (Exception e)
        {
            VatLid.DAL.ExceptionProcess(e);
        }



    }
    private string BuildFilter()
    {

        string sFilterCategory = "";
        string start = getType_Date(txtDateStart.Text);
        string end = getType_Date(txtDateEnd.Text);
        sMonth = start.Substring(0, 6);
        SQL = "SELECT * from User_Log where 1=1 ";


        if (start != "" && end != "")
        {
            SQL = SQL + " and Replace(Replace(Replace(Left(Convert(nvarchar, ActionDate,120),10),'-',''),' ',''),':','')   Between '" + start + "' AND '" + end + "'";
        }
        string Keyword = VatLid.Utils.safeString(txtKeyword.Text.Trim());

        #region Du lieu lay theo Keyword
        if (txtKeyword.Text != "")
        {
            switch (Convert.ToInt32(ddlTypeSearch.SelectedItem.Value))
            {
                case 0:
                    SQL += " AND (CHARINDEX(N'" + Keyword + "',UserID)<>0";
                    SQL += " OR CHARINDEX(N'" + Keyword + "',ActionName)<>0";
                    SQL += " OR CHARINDEX(N'" + Keyword + "',ActionStatus)<>0";
                    SQL += " OR CHARINDEX(N'" + Keyword + "',ActionDesc)<>0)";
                    break;
                case 1:
                    SQL += " AND CHARINDEX(N'" + Keyword + "',UserID)<>0";
                    break;
                case 2:
                    SQL += " AND CHARINDEX(N'" + Keyword + "',ActionName)<>0";
                    break;
                case 3:
                    SQL += " AND CHARINDEX(N'" + Keyword + "',ActionStatus)<>0";
                    break;
                case 4:
                    SQL += " AND CHARINDEX(N'" + Keyword + "',ActionDesc)<>0";
                    break;
            }
        }
        #endregion


        SQL += " ORDER BY ActionDate DESC";
        return SQL;
    }

    protected void cmdSearch_Click(object sender, EventArgs e)
    {
        dgrCommon.CurrentPageIndex = 0;
        BindGridData();
        //lblError.Text = SQL;
    }
    #region Web Form Designer generated code
    override protected void OnInit(EventArgs e)
    {

        InitializeComponent();
        base.OnInit(e);
    }

    private void InitializeComponent()
    {

        this.dgrCommon.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgrCommon_PageIndexChanged);


    }
    #endregion
}
