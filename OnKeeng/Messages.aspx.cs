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
using System.Text;
using System.Configuration;
using VatLidOnPhim;

public partial class NEW_Messages : System.Web.UI.Page
{
    protected string iSupplierID = "";
    protected string FileName;
    protected DataView dv = null;
    protected string intParentID;
    protected string SQL = "";
    protected string intStatus;
    protected int catnew;

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
        intParentID = (Request.QueryString["ParentID"] == null) ? "0" : Request.QueryString["ParentID"];
        if (Utils.IsNumeric(intParentID))
        {
            intParentID = intParentID;
        }
        else
        {
            intParentID = "0";
        }

 
        if (Session["USER"] != null)
        {
            
                if (!Page.IsPostBack)
                {
                    DAL.ResetToken(hiddenToken);
                    BindDataControl();
                    
                    BindData();
                }
            
        }
        else
            Response.Redirect(VatLidOnPhim.Variables.sWebRoot + "login.aspx");
 
    }
    private void dgrCommon_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dgrCommon.CurrentPageIndex = e.NewPageIndex;
        BindData();
    }
    private void BindDataControl()
    {
      

        ListItem litype = new ListItem("--All--", "0");
        ddlType.Items.Add(litype);
        litype = new ListItem("Title", "1");
        ddlType.Items.Add(litype);
        litype = new ListItem("Content", "2");
        ddlType.Items.Add(litype);


        ListItem liShow = new ListItem("--Show --", "2");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Hide--", "1");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Delete--", "0");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--All--", "3");
        ddlStatus.Items.Add(liShow);
    }
    private void BindData()
    {
        try
        {
            dgrCommon.DataSource = BuildFilter().Tables[0];

            VatLidOnPhim.DAL.FetchDataGridColumn(dgrCommon, "ID", "ID");
            VatLidOnPhim.DAL.FetchDataGridColumn(dgrCommon, "Content", "MsgContent");
            VatLidOnPhim.DAL.FetchDataGridColumn(dgrCommon, "CPName", "cpname");
            VatLidOnPhim.DAL.FetchDataGridColumn(dgrCommon, "Date Pub", "MsgPostDate");
            VatLidOnPhim.DAL.FetchDataGridColumn(dgrCommon, "Status", "Status");



            VatLidOnPhim.DAL.FetchDataGridColumn(dgrCommon, "Edit", "ID", "Messages_data.aspx?module=773&id={0}&type=edit", VatLidOnPhim.Variables.EditTitle, 0);

            dgrCommon.DataKeyField = "ID";
            dgrCommon.AutoGenerateColumns = false;
            dgrCommon.DataBind();

            lblTotalRecords.Text = "Number of Record: " + dgrCommon.Items.Count.ToString() + " on total " + BuildFilter().Tables[0].Rows.Count;
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
            string Keyword = txtKeyword.Text.Trim();
            Keyword = VatLidOnPhim.Utils.safeString(Keyword);
            if (Keyword == "")
            {
                Keyword = " ";
            }
            SqlCommand cmd = new SqlCommand();

            cmd.Parameters.AddWithValue("@Keyword", VatLidOnPhim.Utils.safeString(Keyword));
            cmd.Parameters.AddWithValue("@type",VatLidOnPhim.Utils.safeString( ddlType.SelectedValue));
            cmd.Parameters.AddWithValue("@status", VatLidOnPhim.Utils.safeString(ddlStatus.SelectedItem.Value));
            cmd.Parameters.AddWithValue("@CateID", Convert.ToInt32(0));
            ds = VatLidOnPhim.DAL.GetDataSet("cms_Messages_OnVideo_select", DAL.getConnectionStringOnPhim(), cmd);
        }

        catch (Exception ex)
        {
            Response.Redirect("");
        }
        return ds;

    }
    protected void cmdAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Messages_data.aspx?module=773");
    }
    protected void cmdDetele_Click(object sender, EventArgs e)
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
                VatLidOnPhim.DAL.GetDataSet("cms_Messages_OnVideo_delete", DAL.getConnectionStringOnPhim(), cmd);
                VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUserId(Session).ToString(), VatLidOnPhim.LogType.Delete.ToString(), VatLidOnPhim.DAL.getCategoryID(FileName), "OK", sTemp, VatLidOnPhim.Utils.GetIP());
                lblError.Text = Server.HtmlEncode("Delete succeed:" + sTemp);
            }
            else
            {
                VatLidOnPhim.MessageBox.Show("Bạn phải chọn một mục để thực hiện");
            }
            BindData();
        }
        catch
        {

        }
    }
    
    protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void cmdSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
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

    protected void cmdPublish_Click(object sender, EventArgs e)
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
                if (ddlStatus.SelectedValue != "2")
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@id", VatLidOnPhim.Utils.safeString(sTemp));
                    VatLidOnPhim.DAL.GetDataSet("cms_Messages_OnVideo_Show", DAL.getConnectionStringOnPhim(), cmd);
                    VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUserId(Session).ToString(), VatLidOnPhim.LogType.Remove.ToString(), VatLidOnPhim.DAL.getCategoryID(FileName), "OK", sTemp, VatLidOnPhim.Utils.GetIP());

                    lblError.Text = Server.HtmlEncode("Thực hiện thành công:" + sTemp);
                }
                else
                {
                    VatLidOnPhim.MessageBox.Show("Mục Bạn chọn đã được duyệt!");
                }
            }
            else
            {
                VatLidOnPhim.MessageBox.Show("Bạn phải chọn một mục để thực hiện");
            }
            BindData();
        }
        catch
        {

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
                if (ddlStatus.SelectedValue != "1")
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@id", VatLidOnPhim.Utils.safeString(sTemp));
                    VatLidOnPhim.DAL.GetDataSet("cms_Messages_OnVideo_Remove", DAL.getConnectionStringOnPhim(), cmd);
                    VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUserId(Session).ToString(), VatLidOnPhim.LogType.Remove.ToString(), VatLidOnPhim.DAL.getCategoryID(FileName), "OK", sTemp, VatLidOnPhim.Utils.GetIP());

                    lblError.Text = Server.HtmlEncode("Thực hiện thành công:" + sTemp);
                }
                else
                {
                    VatLidOnPhim.MessageBox.Show("Mục Bạn chọn đã được duyệt!");
                }
            }
            else
            {
                VatLidOnPhim.MessageBox.Show("Bạn phải chọn một mục để thực hiện");
            }
            BindData();
        }
        catch
        {

        }
    }

    protected void ddlParentID_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

 
}
