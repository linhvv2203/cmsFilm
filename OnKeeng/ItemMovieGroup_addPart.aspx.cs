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

public partial class OnKeeng_ItemMovieGroup_addPart : System.Web.UI.Page
{
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

        id = Request.QueryString["id"];

        if (!Page.IsPostBack)
        {
            loadData();
            load_ddlFilmGroups();

            if (id != "")
            {
                ddlItemMovie.ClearSelection();
                ddlItemMovie.Items.FindByValue(id).Selected = true;
            }
        }

    }

    private void loadData()
    {
        string txtKeyword = txtSearch.Text.Trim();
        DataSet ds = null;

        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@itemName", txtKeyword);

        ds = VatLidOnPhim.DAL.GetDataSet("CMS_getItemMovieGroup_Itemmovie", cmd);

        dgrCommon.DataSource = ds;

        dgrCommon.DataKeyField = "ID";
        dgrCommon.AutoGenerateColumns = false;
        dgrCommon.DataBind();
    }

    private void load_ddlFilmGroups()
    {
        VatLidOnPhim.DAL.FillDataToDropdownListStore(ddlItemMovie, "CMS_DropDownList_Itemmovie", "id,itemname", "1=1");
        ListItem liVideoCateLBS = new ListItem("== Tập Phim ==", "0");
        ddlItemMovie.Items.Add(liVideoCateLBS);
        ddlItemMovie.SelectedValue = "0";
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        loadData();
    }

    protected void btnAddPart_Click(object sender, EventArgs e)
    {
        int i;
        string sTemp = "";
        string id = ddlItemMovie.SelectedValue;

        if (id == "0")
        {
            lblMessage.Text = "Bạn cần chọn part.";
            return;
        }

        for (i = 0; i < dgrCommon.Items.Count; i++)
        {
            if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == true)
            {

                if (sTemp != "") { sTemp += ","; }
                sTemp += dgrCommon.DataKeys[i];
            }
        }

        if (sTemp == "")
        {
            lblMessage.Text = "Bạn cần chọn part cho tập.";
        }
        else
        {
            string[] idList = sTemp.Split(',');

            for (int j = 0; j < idList.Length; j++)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@idItemMovie", id);
                cmd.Parameters.AddWithValue("@idItemMovieGroup", idList[j]);

                VatLidOnPhim.DAL.GetDataSet("CMS_update_itemMovieGroup_AddPart_v2", cmd);
            }

            lblMessage.Text = "Thêm thành công";
        }
    }

    protected void dgrCommon_SelectedIndexChanged(object sender, EventArgs e)
    {
        dgrCommon.CurrentPageIndex = 0;
        loadData();
    }
    private void dgrCommon_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dgrCommon.CurrentPageIndex = e.NewPageIndex;
        loadData();
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
