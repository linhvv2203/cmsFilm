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
using VatLid;

public partial class SYS_PartnerData : System.Web.UI.Page
{
    protected string id = "";
    protected string TYPE = "";
    protected string SQL;
    protected string sRandom = "";
    protected string fileimages;
    protected string FileName;

    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"];
        if (Utils.IsNumeric(id))
        {
            id = id;
        }
        else
        {
            id = "0";
        }

        TYPE = Request.QueryString["type"];

        cmdSave.Attributes.Add("onclick", "javascript: return checkImageSize();");


        string P_I = Request.ServerVariables["PATH_INFO"];
        string[] aPI = P_I.Split('/');
        int iLength = aPI.Length;
        FileName = aPI[iLength - 1];

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
                switch (TYPE)
                {

                    case "edit":
                        BindData();
                        Label1.Text = "Cập nhật thông tin đối tác";
                        break;
                    default:
                        Label1.Text = "Thêm thông tin đối tác";
                        break;
                }
            }
            }
        }
        else
            Response.Redirect(VatLid.Variables.sWebRoot + "logout.aspx");
    }
    protected void cmdSave_Click(object sender, EventArgs e)
    {
        try
        {
            Random rnd = new Random();
            string IDImageRandom = rnd.Next(999999999).ToString();

            if (TYPE == "edit")
            {
                SqlParameter[] parameters = 
                    { 
                        new SqlParameter("@namePartner", SqlDbType.NVarChar ), 
                        new SqlParameter("@description", SqlDbType.NVarChar),
                        new SqlParameter("@phone", SqlDbType.NVarChar ),
                        new SqlParameter("@address", SqlDbType.NVarChar),
                        new SqlParameter("@email", SqlDbType.NVarChar),
                        new SqlParameter("@fax", SqlDbType.NVarChar ), 
                        new SqlParameter("@status", SqlDbType.Int),
                        new SqlParameter("@id", SqlDbType.Int),
                        new SqlParameter("@popup", SqlDbType.NVarChar),
                        new SqlParameter("@link", SqlDbType.NVarChar),
                    };


                parameters[0].Value = Utils.safeString(txtnamePartner.Text.Trim());
                parameters[1].Value = Utils.safeString(txtdescription.Text.Trim());
                parameters[2].Value = Utils.safeString(txtphone.Text.Trim());
                parameters[3].Value = Utils.safeString(txtaddress.Text.Trim());
                parameters[4].Value = Utils.safeString(txtemail.Text.Trim());
                parameters[5].Value = Utils.safeString(txtfax.Text.Trim());
                parameters[6].Value = Utils.safeString(ddlStatus.SelectedValue.ToString());
                parameters[7].Value = Convert.ToInt32(id);
                parameters[8].Value = txtpopup.Text;
                parameters[9].Value = txtlink.Text;

                VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "WAPMEDIA_cms_updateOrInsertDataToPartner", parameters);
                VatLid.DAL.INSERT_USER_LOG_NEW(VatLid.Utils.getUserId(Session).ToString(), VatLid.LogType.UPDATE.ToString(), VatLid.DAL.getCategoryID(FileName), "OK", id, VatLid.Utils.GetIP());
            }
            else
            {
                SqlParameter[] parameters = 
                    { 
                        new SqlParameter("@namePartner", SqlDbType.NVarChar ), 
                        new SqlParameter("@description", SqlDbType.NVarChar),
                        new SqlParameter("@phone", SqlDbType.NVarChar ),
                        new SqlParameter("@address", SqlDbType.NVarChar),
                        new SqlParameter("@email", SqlDbType.NVarChar),
                        new SqlParameter("@fax", SqlDbType.NVarChar ), 
                        new SqlParameter("@status", SqlDbType.Int),
                        new SqlParameter("@id", SqlDbType.Int),
                          new SqlParameter("@popup", SqlDbType.NVarChar),
                        new SqlParameter("@link", SqlDbType.NVarChar),
                    };


                parameters[0].Value = Utils.safeString(txtnamePartner.Text.Trim());
                parameters[1].Value = Utils.safeString(txtdescription.Text.Trim());
                parameters[2].Value = Utils.safeString(txtphone.Text.Trim());
                parameters[3].Value = Utils.safeString(txtaddress.Text.Trim());
                parameters[4].Value = Utils.safeString(txtemail.Text.Trim());
                parameters[5].Value = Utils.safeString(txtfax.Text.Trim());
                parameters[6].Value = Utils.safeString(ddlStatus.SelectedValue.ToString());
                parameters[7].Value = Convert.ToInt32(id);
                parameters[8].Value = txtpopup.Text;
                parameters[9].Value = txtlink.Text;

                VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "WAPMEDIA_cms_updateOrInsertDataToPartner", parameters);
                VatLid.DAL.INSERT_USER_LOG_NEW(VatLid.Utils.getUserId(Session).ToString(), VatLid.LogType.UPDATE.ToString(), VatLid.DAL.getCategoryID(FileName), "OK", id, VatLid.Utils.GetIP());
            }

            Response.Redirect("PartnerList.aspx");

        }
        catch (Exception ex)
        {
            VatLid.DAL.ExceptionProcess(ex);
        }

    }
    protected void cmdDelete_Click(object sender, EventArgs e)
    {
    }
    private void BindData()
    {
        SqlParameter[] parameters = { 
											new SqlParameter("@ID", SqlDbType.Int),																																					
			};
        parameters[0].Value = Convert.ToInt32(id);
        DataSet ds;
        ds = VatLid.SqlHelper.ExecuteDataset(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "onvideo_cms_getdetailPartnerById", parameters);
        try
        {
            txtnamePartner.Text = ds.Tables[0].Rows[0]["namePartner"].ToString();
            txtphone.Text = ds.Tables[0].Rows[0]["phone"].ToString();
            txtaddress.Text = ds.Tables[0].Rows[0]["address"].ToString();
            txtemail.Text = ds.Tables[0].Rows[0]["email"].ToString();
            txtfax.Text = ds.Tables[0].Rows[0]["fax"].ToString();
            txtdescription.Text = ds.Tables[0].Rows[0]["description"].ToString();
        }
        catch (Exception e)
        {
            VatLid.DAL.ExceptionProcess(e);
        }

    }
    private void BindDataControl()
    {
        ListItem liCa = new ListItem("Hiển thị", "2");
        ddlStatus.Items.Add(liCa);
        liCa = new ListItem("Ẩn", "1");
        ddlStatus.Items.Add(liCa);
        liCa = new ListItem("Xóa", "0");
        ddlStatus.Items.Add(liCa);

    }
}
