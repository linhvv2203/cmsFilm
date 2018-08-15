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
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using VatLidOnPhim;
public partial class NEW_Messages_data : System.Web.UI.Page
{
    protected string SQL = "";
    protected string intParentID;
    protected string id = "";
    protected string TYPE = "";
    protected string AnhLead;
    protected string fileimages;
    protected int catnew;
    protected string FileName;

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
        cmdSave.Attributes.Add("onclick", "javascript: return checkImageSize();");

        intParentID = (Request.QueryString["ParentID"] == null) ? "0" : Request.QueryString["ParentID"];


        

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

 
        if (Session["USER"] != null)
        {
           
                if (!Page.IsPostBack)
                {
                    DAL.ResetToken(hiddenToken);
                    BinData();
                    switch (TYPE)
                    {

                        case "edit":
                            BindData();
                            lblError.Text = "";
                            break;
                        default:
                           
                            cmdDelete.Enabled = false;
                            lblError.Text = "";
                            break;
                    }                
            }
        }
        else
            Response.Redirect(VatLidOnPhim.Variables.sWebRoot + "logout.aspx");
 
    }
    private void BindData()
    {
        SqlParameter[] parameters = { 
                                        new SqlParameter("@ID", SqlDbType.Int),																																					
			};
        parameters[0].Value = Convert.ToInt32(id);
        DataSet ds;
        ds = VatLidOnPhim.SqlHelper.ExecuteDataset(VatLidOnPhim.DAL.getConnectionStringOnPhim(), CommandType.StoredProcedure, "select_Messages_Onvideo_ByID", parameters);
        try
        {

            txtTitle.Text = ds.Tables[0].Rows[0]["MsgSubject"].ToString();
            txtMsgContent.Text = ds.Tables[0].Rows[0]["MsgContent"].ToString();
            ddldt.SelectedValue = ds.Tables[0].Rows[0]["idcp"].ToString();
            datetimepicker.Text = ds.Tables[0].Rows[0]["MsgPostDate"].ToString();
        }
        catch (Exception e)
        {
            VatLidOnPhim.DAL.ExceptionProcess(e);
        }

    }
    protected void BinData()
    {
        VatLidOnPhim.DAL.FillDataToDropdownListStore(ddldt, "CMS_DropDownList_Partner", "id,namepartner", Session["PartnerID"].ToString());
        ddldt.SelectedValue = "1";
    }
    protected void cmdSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!VatLidOnPhim.DAL.CheckPermissionByToken(hiddenToken))
            {
                VatLidOnPhim.DAL.ResetToken(hiddenToken);
                return;
            }
            Random rnd = new Random();
            string IDImageRandom = rnd.Next(999999999).ToString();
         
            if (TYPE == "edit")
            {
                if (txtTitle.Text.Length == 0)
                {
                    lblError.Text = "Ban phai nhap tieu de!";
                    return;
                }

                if (txtMsgContent.Text.Length == 0)
                {
                    lblError.Text = "Ban phai nhap noi dung!";
                    return;
                }
                if (ddldt.SelectedValue == "0")
                {
                    lblError.Text = "Ban phai chọn CP!";
                    return;
                }       
                
                SqlParameter[] parameters = 
						{ 					
							new SqlParameter("@MsgSubject", SqlDbType.NVarChar ),
							new SqlParameter("@MsgContent", SqlDbType.NVarChar ),
							new SqlParameter("@ID", SqlDbType.Int),
                            new SqlParameter("@idcp", SqlDbType.Int ),					        
                            new SqlParameter("@DatePub",SqlDbType.DateTime2)
					};

            
               
                parameters[0].Value = VatLidOnPhim.Utils.safeString(txtTitle.Text.Trim());
                parameters[1].Value = VatLidOnPhim.Utils.safeString(txtMsgContent.Text.Trim());
                parameters[2].Value = Convert.ToInt32(VatLidOnPhim.Utils.safeString(id));
                parameters[3].Value = Convert.ToInt32(VatLidOnPhim.Utils.safeString(ddldt.SelectedValue));
                parameters[4].Value = VatLidOnPhim.Utils.safeString(datetimepicker.Text);
                VatLidOnPhim.SqlHelper.ExecuteNonQuery(VatLidOnPhim.DAL.getConnectionStringOnPhim(), CommandType.StoredProcedure, "update_Messages_Onvideo", parameters);

                Response.Redirect("Messages.aspx?module=773");

            }
            else
            {

                if (txtTitle.Text.Length == 0)
                {
                    lblError.Text = "Ban phai nhap tieu de";
                    return;
                }

                if (txtMsgContent.Text.Length == 0)
                {
                    lblError.Text = "Ban phai nhap noi dung!";
                    return;
                }
                
            


                SqlParameter[] parameters = 
									{ 
					new SqlParameter("@MsgSubject", SqlDbType.NVarChar ),
                    new SqlParameter("@MsgContent", SqlDbType.NVarChar ),
                    new SqlParameter("@Poster", SqlDbType.NVarChar ),
				    new SqlParameter("@idcp", SqlDbType.Int ),	
                    new SqlParameter("@DatePub",SqlDbType.DateTime2)
									
				};

                

             
                parameters[0].Value = VatLidOnPhim.Utils.safeString(txtTitle.Text);
                parameters[1].Value = VatLidOnPhim.Utils.safeString(txtMsgContent.Text);
                parameters[2].Value = VatLidOnPhim.Utils.safeString(Session["USER"].ToString());
                parameters[3].Value = Convert.ToInt32(VatLidOnPhim.Utils.safeString(ddldt.SelectedValue));
                parameters[4].Value = VatLidOnPhim.Utils.safeString(datetimepicker.Text);

                VatLidOnPhim.SqlHelper.ExecuteNonQuery(VatLidOnPhim.DAL.getConnectionStringOnPhim(), CommandType.StoredProcedure, "insert_Messages_Onvideo", parameters);
                
                lblError.Text = "Ban nhap thanh cong";
                Response.Redirect("Messages.aspx?module=773");


            }
        }

        catch (Exception ex)
        {
            //VatLidOnPhim.DAL.ExceptionProcess(ex);
            lblError.Text = ex.Message;
        }
    }
    

    protected void cmdDelete_Click(object sender, EventArgs e)
    {
        Response.Redirect("Messages.aspx?module=773");
    }

}
