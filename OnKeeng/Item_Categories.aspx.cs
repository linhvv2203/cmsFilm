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
using VatLidOnPhim;

public partial class CATEGORIES_CHANELS_Item_Categories : System.Web.UI.Page
{
    protected string sFilterCategory = "";
    protected string SQL = "";
    protected string strCategoryID = "";

    protected string FileName="";
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
                DAL.ResetToken(hiddenToken);
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
            dgrCommon.DataKeyField = "ID";
            dgrCommon.AutoGenerateColumns = false;
            dgrCommon.DataBind();
          int a=  dgrCommon.Items.Count;
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
            cmd.Parameters.AddWithValue("@type", ddlImages.SelectedValue);
            cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue);
            ds = VatLidOnPhim.DAL.GetDataSet("cms_CategoriesParent_select", cmd);
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

        ListItem liImages = new ListItem("--Tất cả--", "0");
        ddlImages.Items.Add(liImages);
        liImages = new ListItem("CategoryID", "1");
        ddlImages.Items.Add(liImages);
        liImages = new ListItem("CategoryName", "2");
        ddlImages.Items.Add(liImages);
        liImages = new ListItem("Description", "3");
        ddlImages.Items.Add(liImages);

        ListItem liShow = new ListItem("--Đã duyệt --", "2");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Chờ duyệt--", "1");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Xoá--", "0");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Tất cả--", "3");
        ddlStatus.Items.Add(liShow);
    }
    protected void ddlPositions_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlImages_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDataGrid();

    }
    protected void ddlAlbum_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDataGrid();
    }
    protected void ddlCateogry_SelectedIndexChanged(object sender, EventArgs e)
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
    protected void dgrCommon_SelectedIndexChanged(object sender, EventArgs e)
    {
        dgrCommon.CurrentPageIndex = 0;
        BindDataGrid();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDataGrid();

    }
    protected void dgrCommon_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            string id = dgrCommon.DataKeys[e.Item.ItemIndex].ToString();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@status", 1);
            VatLidOnPhim.DAL.ExecuteNonQuery_pro("cms_CategoriesItemmovie_delete", cmd);
            //VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUserId(Session).ToString(), VatLidOnPhim.LogType.Delete.ToString(), VatLidOnPhim.DAL.getCategoryID(FileName), "OK", id, VatLidOnPhim.Utils.GetIP());
            BindDataGrid();
            lbError.Text = "Thành Công";
        }
        catch (Exception ex)
        {
            lbError.Text = ex.ToString();
        }
    }
    protected void cmdDelete_Click(object sender, EventArgs e)
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
                cmd.Parameters.AddWithValue("@id",VatLidOnPhim.Utils.safeString( sTemp));
                cmd.Parameters.AddWithValue("@status", VatLidOnPhim.Utils.safeString(ddlStatus.SelectedItem.Value));
                VatLidOnPhim.DAL.ExecuteNonQuery_pro("cms_CategoriesItemmovie_delete", cmd);
                VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUserId(Session).ToString(), VatLidOnPhim.LogType.Delete.ToString(), VatLidOnPhim.DAL.getCategoryID(FileName), "OK", sTemp, VatLidOnPhim.Utils.GetIP());
                lblError.Text = Server.HtmlEncode("Delete succeed:" + sTemp);

            }
            else
            {
                VatLidOnPhim.MessageBox.Show("Bạn phải chọn một mục để thực hiện");
            }
            BindDataGrid();
        }
        catch
        {
            Response.Redirect(VatLidOnPhim.Variables.sWebRoot + "error_info.aspx?err=29");
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
                if (ddlStatus.SelectedValue == "2")
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@id",VatLidOnPhim.Utils.safeString(sTemp));
                    VatLidOnPhim.DAL.ExecuteNonQuery_pro("cms_CategoriesItemmovie_removed", cmd);

                    VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUserId(Session).ToString(), VatLidOnPhim.LogType.Remove.ToString(), VatLidOnPhim.DAL.getCategoryID(FileName), "OK", sTemp, VatLidOnPhim.Utils.GetIP());

                    lblError.Text = Server.HtmlEncode("Thực hiện thành công:" + sTemp);
                }
                else
                {
                    VatLidOnPhim.MessageBox.Show("Audio Bạn chọn đã được gỡ bỏ!");
                }
            }
            else
            {
                VatLidOnPhim.MessageBox.Show("Bạn phải chọn một mục để thực hiện");
            }
            BindDataGrid();
        }
        catch
        {
            Response.Redirect(VatLidOnPhim.Variables.sWebRoot + "error_info.aspx?err=29");
        }
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
                cmd.Parameters.AddWithValue("@idcate",VatLidOnPhim.Utils.safeString( sTemp));
                VatLidOnPhim.DAL.ExecuteNonQuery_pro("cms_categoriesItemMovie_SetShowHome", cmd);
                VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUserId(Session).ToString(), VatLidOnPhim.LogType.Publish.ToString(), VatLidOnPhim.DAL.getCategoryID(FileName), "OK", sTemp, VatLidOnPhim.Utils.GetIP());
                lblError.Text = Server.HtmlEncode("Thực hiện thành công:" + sTemp);

            }
            else
            {
                VatLidOnPhim.MessageBox.Show("Bạn phải chọn một mục để thực hiện");
            }
            BindDataGrid();
        }
        catch
        {
            Response.Redirect(VatLidOnPhim.Variables.sWebRoot + "error_info.aspx?err=29");
        }
    }
    protected void dgrCommon_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgrCommon.CurrentPageIndex = e.NewPageIndex;
        BindDataGrid();
    }
    protected void cmdAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Categories_Data.aspx?module=837");
    }
    protected void btsettop_Click(object sender, EventArgs e)
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
                cmd.Parameters.AddWithValue("@idcate", VatLidOnPhim.Utils.safeString(sTemp));
                VatLidOnPhim.DAL.ExecuteNonQuery_pro("cms_categories_SetTop", cmd);
                VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUserId(Session).ToString(), VatLidOnPhim.LogType.Publish.ToString(), VatLidOnPhim.DAL.getCategoryID(FileName), "OK", sTemp, VatLidOnPhim.Utils.GetIP());
                lblError.Text = Server.HtmlEncode("Thực hiện thành công:" + sTemp);

            }
            else
            {
                VatLidOnPhim.MessageBox.Show("Bạn phải chọn một mục để thực hiện");
            }
            BindDataGrid();
        }
        catch
        {
            Response.Redirect(VatLidOnPhim.Variables.sWebRoot + "error_info.aspx?err=29");
        }
    }
    protected void ddldt_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDataGrid();
    }
    protected void btnSetTopMenu_Click(object sender, EventArgs e)
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

        if(sTemp!="")
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@listID", VatLidOnPhim.Utils.safeString(sTemp));

            VatLidOnPhim.DAL.GetDataSet("cms_Update_Category_SetTopMenu", cmd);
        }
    }

    protected void btnUnSetMenu_Click(object sender, EventArgs e)
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

        if(sTemp!="")
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@listID", VatLidOnPhim.Utils.safeString(sTemp));

            VatLidOnPhim.DAL.GetDataSet("cms_Update_Category_UnSetTopMenu", cmd);
        }
    }

    protected void btnSetOrder_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < dgrCommon.Items.Count; i++)
        {
            string txtID = ((Literal)dgrCommon.Items[i].FindControl("txtID")).Text;
            string txtOrder = ((TextBox)dgrCommon.Items[i].FindControl("txtOrder")).Text;


            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@order", txtOrder);
            cmd.Parameters.AddWithValue("@id", txtID);

            VatLidOnPhim.DAL.GetDataSet("cms_Update_Order_CategoryMenu", cmd);

        }
    }
}
