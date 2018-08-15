using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OnKeeng_IntroManage : System.Web.UI.Page
{
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
                BindData();
                ViewState["IDs"] = ""; //Xoa viewstate
                BindDataGrid();

            }
        }
        else
            Response.Redirect(VatLidOnPhim.Variables.sWebRoot + "logout.aspx");
    }
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
    private void BindDataGrid()
    {
        //SQL = BuildFilter();
        try
        {
            //dv = VatLidOnPhim.DAL.CreateDataView(SQL);
            //dgrCommon.DataSource = dv;
            DataSet ds = Builfilter();
            dgrCommon.DataSource = ds;

            String format4 = "<img  src='" + VatLidOnPhim.Variables.ImageLinks7 + "' onclick=\"window.open('" + VatLidOnPhim.Variables.sWebRoot + "OnKeeng/PlayVideoIntro.aspx?ID={0}','','top=100,left=200,height=400,width=400,scrollbars=0,toolbar=0')\" />";
            VatLidOnPhim.DAL.FetchDataGridColumnFormatLast(dgrCommon, "Xem", "ID", format4);

            dgrCommon.DataKeyField = "ID";
            dgrCommon.AutoGenerateColumns = false;
            dgrCommon.DataBind();
            int a = dgrCommon.Items.Count;
        }
        catch (Exception e)
        {
            VatLidOnPhim.DAL.ExceptionProcess(e);
        }

    }
    private DataSet Builfilter()
    {
        DataSet ds = null;
        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@keyword", "");
        cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue);
        cmd.Parameters.AddWithValue("@type", 1);

        ds = VatLidOnPhim.DAL.GetDataSet("cms_getItemmovie_intro", cmd);

        return ds;
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
            //lblError.Text = "Thành Công";
        }
        catch (Exception ex)
        {
            //lblError.Text = ex.ToString();
        }

    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDataGrid();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("ItemmovieIntro_Add.aspx");
    }
}