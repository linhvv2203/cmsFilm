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

public partial class OnKeeng_FilmGroups_listMovie : System.Web.UI.Page
{
    protected string idFilmGroup = "";

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

        idFilmGroup = Request.QueryString["idFilmGroup"];

        if (!Page.IsPostBack)
        {
            loadData();
        }
    }

    private void loadData()
    {
        DataSet ds = null;

        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@idFilmGroup", idFilmGroup);

        ds = VatLidOnPhim.DAL.GetDataSet("CMS_getItemMovie_FilmGroups_Detail", cmd);

        dgrCommon.DataSource = ds;

        dgrCommon.DataKeyField = "ID";
        dgrCommon.AutoGenerateColumns = false;
        dgrCommon.DataBind();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int i;
        string sTemp = "";

        for (i = 0; i < dgrCommon.Items.Count; i++)
        {
            if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == true)
            {

                TextBox txtOrder = (TextBox)dgrCommon.Items[i].FindControl("txtOrderBy");

                string orderBy = txtOrder.Text;

                if (sTemp != "") { sTemp += ","; }
                sTemp += dgrCommon.DataKeys[i];

                if (sTemp != "")
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@id", dgrCommon.DataKeys[i]);
                    cmd.Parameters.AddWithValue("@orderBy", orderBy);

                    VatLidOnPhim.DAL.GetDataSet("CMS_update_itemMovie_filmGroup_orderBy", cmd);
                    lblMess.Text = "Cập nhật thành công.";
                }
                else
                {
                    lblMess.Text = "Bạn cần chọn tập cần update.";
                }


            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int i;
        string sTemp = "";

        for (i = 0; i < dgrCommon.Items.Count; i++)
        {
            if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == true)
            {

                TextBox txtOrder = (TextBox)dgrCommon.Items[i].FindControl("txtOrderBy");

                string orderBy = txtOrder.Text;

                if (sTemp != "") { sTemp += ","; }
                sTemp += dgrCommon.DataKeys[i];

                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@id", dgrCommon.DataKeys[i]);
                //cmd.Parameters.AddWithValue("@idFilmGroup", idFilmGroup);

                VatLidOnPhim.DAL.GetDataSet("CMS_delete_itemMovie_filmGroup", cmd);

            }
        }
        if (sTemp != "")
        {
            lblMess.Text = "Xóa thành công.";
        }
        else
        {
            lblMess.Text = "Bạn cần chọn tập cần xóa.";
        }
        loadData();
    }

    protected void btnSetTapInBo_Click(object sender, EventArgs e)
    {
        string sTemp = "";
        for (int i = 0; i < dgrCommon.Items.Count; i++)
        {
            if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == true)
            {
                if (sTemp != "") { sTemp += ","; }
                sTemp += dgrCommon.DataKeys[i];

                if (sTemp != "")
                {
                    //update film groups first name
                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Parameters.AddWithValue("@idFilmGroup", idFilmGroup);
                    cmd1.Parameters.AddWithValue("@id", dgrCommon.DataKeys[i]);

                    VatLidOnPhim.DAL.GetDataSet("CMS_update_filmGroups_link", cmd1);

                    lblMess.Text = "DA THUC HIEN THANH CONG:" + sTemp;
                }
                else
                {
                    lblMess.Text = "Chọn danh sách phim để set";
                }
                loadData();
            }
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
