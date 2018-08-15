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
using VatLidOnPhim;
using System.Data.SqlClient;

public partial class OnPhim_Actor_Data : System.Web.UI.Page
{
    protected string FileName = "";
    protected string itemfile = "";
    protected string SQL = "";
    protected string id = "";
    protected string TYPE = "";
    protected string strCategoryID = "";
    protected string status = "";
    public static string file_image = "";
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
        if (Utils.IsNumeric(id))
        {
            id = id;
        }
        else
        {
            id = "0";
        }

        if (Session["USER"] != null)
        {
            if (!Page.IsPostBack)
            {
                DAL.ResetToken(hiddenToken);
                BinData();
                switch (id)
                {
                    case "0":
                        Label1.Text = "THÊM MỚI ACTOR.";
                        hiddenTime.Value = "";
                        //txtUserName.Text = Session["USER"].ToString().ToUpper();
                        break;
                    default:
                        Label1.Text = "CẬP NHẬT ACTOR.";
                        Load_Data(id);
                        ddlStatus.Enabled = false;
                        break;
                }
            }
        }
        else
            Response.Redirect(VatLidOnPhim.Variables.sWebRoot + "logout.aspx");


    }
    protected void BinData()
    {
        //VatLidOnPhim.DAL.FillDataToDropdownListStore(ddldt, "CMS_DropDownList_Partner", "id,namepartner", Session["PartnerID"].ToString());
        ListItem liShow = new ListItem("--Đã duyệt --", "2");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Chờ duyệt--", "1");
        ddlStatus.Items.Add(liShow);
        liShow = new ListItem("--Xoá--", "0");
        ddlStatus.Items.Add(liShow);
        ddlStatus.SelectedValue = "2";
    }
    protected void Load_Data(string id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@ID", id);

        DataSet ds;
        ds = DAL.GetDataSet("cms_Categories&Select", DAL.getConnectionStringOnPhim(), cmd);

        try
        {
            txtName.Text = ds.Tables[0].Rows[0]["CategoryName"].ToString();
            txtDesc.Text = ds.Tables[0].Rows[0]["description"].ToString();
            file_image = ds.Tables[0].Rows[0]["url_images"].ToString();    
        }
        catch (Exception e)
        {
            lblError.Text = e.Message;
        }

    }

    protected void cmdSave_Click(object sender, EventArgs e)
    {
        if (!VatLidOnPhim.DAL.CheckPermissionByToken(hiddenToken))
        {
            VatLidOnPhim.DAL.ResetToken(hiddenToken);
            return;
        }
        if (txtName.Text.Length <= 1)
        {
            lblError.Text = "Bạn phải nhập tiêu đề category";
            return;
        }

        //if (id == "0" && getTitle(txtName.Text.Trim()) == "1")
        //{
        //    lblError.Text = "Category đã nhập trùng. Mời bạn nhập tiêu đề khác";
        //    return;
        //}
      
        if (id != "0")
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@id", VatLidOnPhim.Utils.safeString(id));
            cmd.Parameters.AddWithValue("@ParentID", 0);
            cmd.Parameters.AddWithValue("@CategoryName", VatLidOnPhim.Utils.safeString(txtName.Text.Trim()));
            cmd.Parameters.AddWithValue("@CategoryID", 0);
            cmd.Parameters.AddWithValue("@Order", 1);

            cmd.Parameters.AddWithValue("@Description", VatLidOnPhim.Utils.safeString(txtDesc.Text));
            cmd.Parameters.AddWithValue("@Status", 1);

            cmd.Parameters.AddWithValue("@idCP", 0);

         
            DAL.GetDataSet("cms_Categories&Sync", VatLidOnPhim.DAL.getConnectionStringOnPhim(), cmd);
            VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUserId(Session).ToString(), VatLidOnPhim.LogType.UPDATE.ToString(), VatLidOnPhim.DAL.getCategoryID(FileName), "OK", id.ToString(), VatLidOnPhim.Utils.GetIP());
            lblError.Text = "ĐÃ SỬA THÀNH CÔNG!";
            Load_Data(id);
        }

        else
        {
           
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@Name", VatLidOnPhim.Utils.safeString(txtName.Text.Trim()));
            cmd.Parameters.AddWithValue("@Desc", VatLidOnPhim.Utils.safeString(txtDesc.Text));
            cmd.Parameters.AddWithValue("@Status", 2);
            VatLidOnPhim.DAL.GetDataSet("cms_Actor$Sync", VatLidOnPhim.DAL.getConnectionStringOnPhim(), cmd);
            //VatLidOnPhim.SqlHelper.ExecuteNonQuery(VatLidOnPhim.DAL.getConnectionStringOnPhim(), CommandType.StoredProcedure, "insert_ItemVideo_new", cmd);
            //cmd.Parameters.AddWithValue("@idCP", Convert.ToInt32(VatLidOnPhim.Utils.safeString(ddlStatus.SelectedValue)));


            //VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUserId(Session).ToString(), VatLidOnPhim.LogType.ADD.ToString(), VatLidOnPhim.DAL.getCategoryID(FileName), "OK", id.ToString(), VatLidOnPhim.Utils.GetIP());
            //DataSet ds = DAL.ExecuteQuery(;//("cms_Actor$Sync", DAL.getConnectionStringOnPhim(), cmd);
          
            lblError.Text = "ĐÃ THÊM THÀNH CÔNG!";
        }
    }
   
    private string getTitle(string Title)
    {
        string status = "";
        DataSet ds = null;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@title", Title.Trim());
            cmd.Parameters.AddWithValue("@TYPE", 3);
            ds = VatLidOnPhim.DAL.GetDataSet("[cms_get_title]", cmd);

            if (ds.Tables[0].Rows.Count > 0)
            {
                status = "1";
            }
            else
            {
                status = "0";
            }


        }
        catch (Exception e)
        {
            VatLidOnPhim.DAL.ExceptionProcess(e);
        }

        return status;
    }



}
