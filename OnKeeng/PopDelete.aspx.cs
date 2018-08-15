using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VatLidOnPhim;

public partial class OnKeeng_PopDelete : System.Web.UI.Page
{
    protected string sItemName = "";
    protected string id = "";
    protected string SQL = "";
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

        id = (Request.QueryString["id"] == null) ? "0" : Request.QueryString["id"];

        if (Session["USER"] != null)
        {
            sItemName = getName(id);
            if (!Page.IsPostBack)
            {
                DAL.ResetToken(hiddenToken);
                VatLidOnPhim.DAL.FillDataToDropdownListStore(ddlCategory, "CMS_DropDownList_CategoriesStatus", "id,Name", "");
                ListItem liCategoriesStatus = new ListItem("===Lựa chọn lý do từ chối===", "0");
                ddlCategory.Items.Add(liCategoriesStatus);
                ddlCategory.SelectedValue = "0";
            }
        }
    }

    public static string getName(string id)
    {
        DataSet ds = null;
        string result = "";
        try
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id));

            ds = VatLidOnPhim.DAL.GetDataSet("cms_itemmovie_getname", cmd);

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
    protected void cmdSave_Click(object sender, EventArgs e)
    {

        //if (ddlCategory.SelectedValue == "0")
        //{
        //    lblError.Text = "Bạn phải chọn loại trạng thái xóa";
        //    return;
        //}
        string ItemName = getName(id);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@status", 0);
            cmd.Parameters.AddWithValue("@type", 1);

            VatLidOnPhim.DAL.GetDataSet("cms_itemmovie_update_status", cmd);

            lblError.Text = Server.HtmlEncode("Bạn đã xóa thành công: " + ItemName);
        }
        catch (Exception ex)
        {

            lblError.Text = Server.HtmlEncode("Bạn xóa lỗi: " + ItemName);
        }
        

        //SqlCommand cmd = new SqlCommand();
        //cmd.Parameters.AddWithValue("@idvideo", "(" + VatLid.Utils.safeString(id) + ")");
        //cmd.Parameters.AddWithValue("@status", 1);
        //VatLidOnVTV.DAL.ExecuteNonQuery_pro("cms_itemvideo_delete", cmd);
        //VatLidOnVTV.DAL.INSERT_USER_LOG_NEW(VatLid.Utils.getUserId(Session).ToString(), VatLid.LogType.Delete.ToString(), ItemName, "OK", id, VatLid.Utils.GetIP());
        //VatLidOnVTV.DAL.INSERT_ItemVideoHistory(Convert.ToInt32(id), Convert.ToInt32(ddlCategory.SelectedItem.Value), Session["USER"].ToString().ToUpper(), txtDesc.Text.Trim());

    }
}