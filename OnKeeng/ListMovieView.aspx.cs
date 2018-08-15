using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OnKeeng_ListMovieView : System.Web.UI.Page
{
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

        if (!Page.IsPostBack)
        {
            loadCP();
            loadCate();
            ddlPartnerID.SelectedValue = "0";
            ddlCategory.SelectedValue = "0";
            BindDataGrid();
            btnExportExcel.Enabled = false;
        }
    }

    private void loadCP()
    {
        SQL = "Partner_Keeng WHERE status=2 order by id asc";
        VatLidOnPhim.DAL.FillDataToDropdownList(ddlPartnerID, SQL, "ID,namePartner");
        ListItem liPartnerID = new ListItem("Đối tác", "0");
        ddlPartnerID.Items.Add(liPartnerID);

        
    }
    private void loadCate()
    {
        SQL = "Categories_ItemMovie WHERE status=2 order by id asc";
        VatLidOnPhim.DAL.FillDataToDropdownList(ddlCategory, SQL, "ID,CategoryName");
        ListItem liCategory = new ListItem("Thể loại", "0");
        ddlCategory.Items.Add(liCategory);
    }
    private void BindDataGrid()
    {
        try
        {
            lblTotalRecords.Text = "";

            dgrCommon.DataSource = BuildFilter().Tables[0];
            //dgrCommon.DataKeyField = "ID";
            dgrCommon.AutoGenerateColumns = false;
            dgrCommon.DataBind();
            int a = dgrCommon.Items.Count;
            //lblTotalRecords.Text = Server.HtmlEncode("Tổng số bản ghi: " + dgrCommon.Items.Count.ToString() + " trên tổng số " + BuildFilter().Tables[0].Rows.Count);
            btnExportExcel.Enabled = true;
        }
        catch (Exception e)
        {
            VatLidOnPhim.DAL.ExceptionProcess(e);
        }

    }
    private DataSet BuildFilter()
    {
        DataSet ds = null;
        try
        {

            string id = ddlPartnerID.SelectedValue;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@IdCP", ddlPartnerID.SelectedValue);
            cmd.Parameters.AddWithValue("@IdCat", ddlCategory.SelectedValue);
            cmd.Parameters.AddWithValue("@DateStart", txtDateStart.Value.Trim());
            cmd.Parameters.AddWithValue("@DateEnd", txtDateEnd.Value.Trim());
            ds = VatLidOnPhim.DAL.GetDataSet("cms_Itemmovie_getView", cmd);
        }
        catch
        {
            return null;
        }
        return ds;
    }

    protected void btnSeach_Click(object sender, EventArgs e)
    {
        BindDataGrid();
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ddlPartnerID.SelectedValue = "0";
        ddlCategory.SelectedValue = "0";
        txtDateStart.Value = "";
        txtDateEnd.Value = "";
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        ExportToExcel(BuildFilter(), "ListMovieView.xls");
    }
    protected void ExportToExcel(DataSet ds, string filename)
    {
        HttpResponse response = HttpContext.Current.Response;

        // first let's clean up the response.object
        response.Clear();
        response.Charset = "";

        // set the response mime type for excel
        response.ContentType = "application/vnd.ms-excel";
        response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");

        // create a string writer
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            {
                // instantiate a datagrid
                DataGrid dg = new DataGrid();
                dg.DataSource = ds.Tables[0];
                dg.DataBind();
                dg.RenderControl(htw);
                response.Write(sw.ToString());
                response.End();
            }
        }
    }
    protected void dgrCommon_SelectedIndexChanged(object sender, EventArgs e)
    {
        //dgrCommon.CurrentPageIndex = 0;
        BindDataGrid();
    }
    protected void dgrCommon_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgrCommon.CurrentPageIndex = e.NewPageIndex;
        BindDataGrid();
    }
    protected void ddlPartnerID_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDataGrid();
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDataGrid();
    }
         
}