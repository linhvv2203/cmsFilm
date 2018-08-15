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
using VatLid;

public partial class SYS_PartnerList : System.Web.UI.Page
{
    protected string sFilterCategory = "";
    protected string SQL = "";
    protected DataView dv = null;
    protected string intCategoryID;
    protected string FileName;

    protected void Page_Load(object sender, EventArgs e)
    {
        string P_I = Request.ServerVariables["PATH_INFO"];
        string[] aPI = P_I.Split('/');
        int iLength = aPI.Length;
        FileName = aPI[iLength - 1];
        intCategoryID = (Request.QueryString["CategoryID"] == null) ? "0" : Request.QueryString["CategoryID"];
        if (Utils.IsNumeric(intCategoryID))
        {
            intCategoryID = intCategoryID;
        }
        else
        {
            intCategoryID = "0";
        }

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
                    BindDataGrid();
                }
            }
        }
        else
            Response.Redirect(VatLid.Variables.sWebRoot + "logout.aspx");
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

    private string BuildFilter()
    {
        string sFilterCategory = "";
        SQL = "SELECT * FROM dbo.Partner WHERE 1=1 ";

        #region lay du lieu trang thai
        if (Convert.ToInt32(ddlStatus.SelectedItem.Value) == 3)
            sFilterCategory = " ";
        else
            sFilterCategory = " AND status=" + Convert.ToInt32(ddlStatus.SelectedItem.Value);
        SQL += sFilterCategory;
        #endregion

        string Keyword = txtKeyword.Text.Trim();
        Keyword = VatLid.Utils.safeString(Keyword);

        #region Du lieu lay theo Keyword
        if (txtKeyword.Text != "")
        {
            switch (Convert.ToInt32(ddlSearch.SelectedItem.Value))
            {
                case 0:
                    SQL += " AND (CHARINDEX(N'" + Keyword + "',namePartner)<>0";
                    SQL += " OR CHARINDEX(N'" + Keyword + "',phone)<>0";
                    SQL += " OR CHARINDEX(N'" + Keyword + "',address)<>0";
                    SQL += " OR CHARINDEX(N'" + Keyword + "',email)<>0";
                    SQL += " OR CHARINDEX(N'" + Keyword + "',fax)<>0)";
                    break;
                case 1:
                    SQL += " AND CHARINDEX(N'" + Keyword + "',namePartner)<>0";
                    break;
                case 2:
                    SQL += " AND CHARINDEX(N'" + Keyword + "',phone)<>0";
                    break;
                case 3:
                    SQL += " AND CHARINDEX(N'" + Keyword + "',address)<>0";
                    break;
                case 4:
                    SQL += " AND CHARINDEX(N'" + Keyword + "',email)<>0";
                    break;
                case 5:
                    SQL += " AND CHARINDEX(N'" + Keyword + "',fax)<>0";
                    break;
            }
        }
        #endregion

        SQL += " ORDER BY id desc";
        return SQL;
    }
    private void BindDataGrid()
    {
        SQL = BuildFilter();
        try
        {
            dv = VatLid.DAL.CreateDataView(SQL);
            dgrCommon.DataSource = dv;
            dgrCommon.DataKeyField = "ID";
            dgrCommon.AutoGenerateColumns = false;
            dgrCommon.DataBind();
            lblTotalRecords.Text = "Số bản ghi đang xem: " + dgrCommon.Items.Count.ToString() + " trên tổng số " + dv.Count.ToString();
        }
        catch (Exception e)
        {
            VatLid.DAL.ExceptionProcess(e);
        }
    }

    private void dgrCommon_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dgrCommon.CurrentPageIndex = e.NewPageIndex;
        BindDataGrid();
    }
    private void BindDataControl()
    {
        ListItem litype = new ListItem("--All--", "0");
        ddlSearch.Items.Add(litype);
        litype = new ListItem("Tên công ty", "1");
        ddlSearch.Items.Add(litype);
        litype = new ListItem("Số điện thoại", "2");
        ddlSearch.Items.Add(litype);
        litype = new ListItem("Địa chỉ", "3");
        ddlSearch.Items.Add(litype);
        litype = new ListItem("Email", "4");
        ddlSearch.Items.Add(litype);
        litype = new ListItem("Fax", "5");
        ddlSearch.Items.Add(litype);

        ListItem liShow = new ListItem("--Duyệt--", "2");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Gỡ bỏ--", "1");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Xóa--", "0");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Tất cả--", "3");
        ddlStatus.Items.Add(liShow);

    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDataGrid();
    }

    protected void ddlTypeMusic_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDataGrid();
    }
    protected void cmdSearch_Click(object sender, EventArgs e)
    {
        BindDataGrid();
    }
    protected void cmdAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("PartnerData.aspx");
    }
    protected void cmdDetele_Click(object sender, EventArgs e)
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
            if (ddlStatus.SelectedItem.Value == "0")
            {
                SQL = "DELETE Partner WHERE ID in (" + sTemp + ")";
                VatLid.DAL.ExecuteQuery(SQL);
                VatLid.DAL.INSERT_USER_LOG_NEW(VatLid.Utils.getUserId(Session).ToString(), VatLid.LogType.DeleteAll.ToString(), VatLid.DAL.getCategoryID(FileName), "OK", sTemp, VatLid.Utils.GetIP());
                lblError.Text = "Thực hiện thành công: " + sTemp;
            }
            else
            {

                string SQL = "UPDATE Partner SET status=0 WHERE ID in (" + sTemp + ")";
                VatLid.DAL.ExecuteQuery(SQL);
                VatLid.DAL.INSERT_USER_LOG_NEW(VatLid.Utils.getUserId(Session).ToString(), VatLid.LogType.Delete.ToString(), VatLid.DAL.getCategoryID(FileName), "OK", sTemp, VatLid.Utils.GetIP());
                lblError.Text = "Thực hiện thành công: " + sTemp;
            }
        }
        else
        {
            VatLid.MessageBox.Show("Bạn phải chọn để thực hiện.");

        }
        BindDataGrid();
    }
    protected void cmdPublish_Click(object sender, EventArgs e)
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
            VatLid.DAL.UpdateStatus("Partner", "status", 2, sTemp);
            VatLid.DAL.INSERT_USER_LOG_NEW(VatLid.Utils.getUserId(Session).ToString(), VatLid.LogType.Publish.ToString(), VatLid.DAL.getCategoryID(FileName), "OK", sTemp, VatLid.Utils.GetIP());
            lblError.Text = "Thực hiện thành công.";
        }
        else
        {
            VatLid.MessageBox.Show("Bạn phải chọn để thực hiện.");

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
            }

        }
        if (sTemp != "")
        {
            VatLid.DAL.UpdateStatus("Partner", "status", 1, sTemp);
            VatLid.DAL.INSERT_USER_LOG_NEW(VatLid.Utils.getUserId(Session).ToString(), VatLid.LogType.Remove.ToString(), VatLid.DAL.getCategoryID(FileName), "OK", sTemp, VatLid.Utils.GetIP());
            lblError.Text = "Thực hiện thành công.";
        }
        else
        {
            VatLid.MessageBox.Show("Bạn phải chọn để thực hiện.");

        }
        BindDataGrid();
    }
    #region Web Form Designer generated code

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
    #endregion
}
