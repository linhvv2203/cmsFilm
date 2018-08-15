using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Configuration;
using VatLid;

public partial class SYS_CategoriesMenu_data : System.Web.UI.Page
{
    protected string id = "";
    protected string TYPE = "";
    protected string SQL;
    protected string sRandom = "";
    protected string intCategoryID;
    protected string fileimages;
    protected string FileName;


    protected void Page_Load(object sender, EventArgs e)
    {
        intCategoryID = (Request.QueryString["categoryID"] == null) ? "0" : Request.QueryString["categoryID"];
        if (Utils.IsNumeric(intCategoryID))
        {
            intCategoryID = intCategoryID;
        }
        else
        {
            intCategoryID = "0";
        }

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

        cmdDelete.Attributes.Add("onclick", "javascript: return confirm('Bạn có thực sự muốn xóa danh mục này không?');");
        cmdSave.Attributes.Add("onclick", "javascript: return checkImageSize();");
        Response.Write("<script type='text/javascript'> var ImgDataID ='" + ImageView.ClientID + "';</script>");


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
                            Label1.Text = "Cập nhật danh mục tin";
                            break;
                        default:
                            ddlCategory.SelectedValue = intCategoryID;//.Items.FindByValue(intCategoryID).Selected = true;
                            Label1.Text = "Thêm danh mục tin";
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

                if (txtCategoryName.Text.Length == 0)
                {
                    lblError.Text = "Bạn phải nhập tên chuyên mục!";
                    return;
                }

                if ((ImageFile.PostedFile != null) && (ImageFile.PostedFile.ContentLength != 0))
                {
                    int intContentLength = Convert.ToInt32(ConfigurationSettings.AppSettings["ContentLength"]);

                   
                    if (ImageFile.PostedFile.ContentLength > intContentLength)
                    {
                        lblError.Text = "File upload file maxsize 100Kb";
                        return;
                    }
                }



                SqlParameter[] parameters = 
					{ 
						new SqlParameter("@ParentID", SqlDbType.Int ), 
						new SqlParameter("@CategoryForder", SqlDbType.NVarChar),
						new SqlParameter("@CategoryName", SqlDbType.NVarChar ),
						new SqlParameter("@CategoryLink", SqlDbType.NVarChar),
						new SqlParameter("@CategoryImage", SqlDbType.NVarChar),
                        new SqlParameter("@CategoryOrder", SqlDbType.Int ), 
						new SqlParameter("@ID", SqlDbType.Int),
						
					};

                string filename = ImageFile.PostedFile.FileName;
                int pos = filename.LastIndexOfAny(new char[] { '/', '\\' });
                if (pos >= 0) filename = filename.Substring(pos + 1);

                byte[] pic = new byte[ImageFile.PostedFile.ContentLength];
                ImageFile.PostedFile.InputStream.Read(pic, 0, ImageFile.PostedFile.ContentLength);

                if ((ImageFile.PostedFile != null) && (ImageFile.PostedFile.ContentLength != 0))
                {
                    fileimages = IDImageRandom + filename.Substring(filename.LastIndexOf("."));

                }
                else
                {
                    string cSQL = "SELECT CategoryImage FROM CategoriesMenu WHERE  ID=" + id;
                    ArrayList al = VatLid.DAL.GetDataReaderToArrayList(cSQL);
                    string[][] arrReturn = (string[][])al.ToArray(typeof(string[]));
                    if (al.Count > 0)
                    {
                        fileimages = arrReturn[0][0].Trim();
                    }
                }

                parameters[0].Value = ddlCategory.SelectedItem.Value;
                //parameters[1].Value = txtCategoryForder.Text.Trim();
                //parameters[2].Value = txtCategoryName.Text.Trim();
                //parameters[3].Value = txtCategoryLink.Text.Trim();


                parameters[1].Value = txtCategoryForder.Text.Trim();
                parameters[2].Value = txtCategoryName.Text.Trim();
                parameters[3].Value = txtCategoryLink.Text.Trim();
                parameters[4].Value = fileimages;
                parameters[5].Value = Convert.ToInt32(txtCategoryOrder.Text.Trim());
                parameters[6].Value = Convert.ToInt32(id);

                if ((ImageFile.PostedFile != null) && (ImageFile.PostedFile.ContentLength != 0))
                {
                    addImageToFile(fileimages, pic);
                }

                VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "update_CategoriesMenu", parameters);
                VatLid.DAL.INSERT_USER_LOG_NEW(VatLid.Utils.getUserId(Session).ToString(), VatLid.LogType.UPDATE.ToString(), VatLid.DAL.getCategoryID(FileName), "OK", id, VatLid.Utils.GetIP());
            }
            else
            {
                if (txtCategoryName.Text.Length == 0)
                {
                    lblError.Text = "Bạn phải nhập tên danh mục!";
                    return;
                }
                SqlParameter[] parameters2 = { 
													 new SqlParameter("@CategoryName", SqlDbType.NVarChar),																																					
													 new SqlParameter("@ID", SqlDbType.Int),																																					
					};
                parameters2[0].Value =  Utils.safeString(txtCategoryName.Text.Trim());
                parameters2[1].Value = Convert.ToInt32(id);
                DataSet ds;
                ds = VatLid.SqlHelper.ExecuteDataset(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "select_CategoriesMenu_ByName", parameters2);


                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblError.Text = "CategoryName is exist";
                    return;
                }

                if ((ImageFile.PostedFile != null) && (ImageFile.PostedFile.ContentLength != 0))
                {
                    int intContentLength = Convert.ToInt32(ConfigurationSettings.AppSettings["ContentLength"]);

                   
                    if (ImageFile.PostedFile.ContentLength > intContentLength)
                    {
                        lblError.Text = "File upload file maxsize 100Kb";
                        return;
                    }
                }


                SqlParameter[] parameters = 
					{ 
						new SqlParameter("@ParentID", SqlDbType.Int ), 
						new SqlParameter("@CategoryForder", SqlDbType.NVarChar),
						new SqlParameter("@CategoryName", SqlDbType.NVarChar ),
						new SqlParameter("@CategoryLink", SqlDbType.NVarChar ),
						new SqlParameter("@CategoryImage", SqlDbType.NVarChar ),
                        new SqlParameter("@CategoryOrder", SqlDbType.Int ),
						
					};


                string filename = ImageFile.PostedFile.FileName;
                int pos = filename.LastIndexOfAny(new char[] { '/', '\\' });
                if (pos >= 0) filename = filename.Substring(pos + 1);

                byte[] pic = new byte[ImageFile.PostedFile.ContentLength];
                ImageFile.PostedFile.InputStream.Read(pic, 0, ImageFile.PostedFile.ContentLength);

                if ((ImageFile.PostedFile != null) && (ImageFile.PostedFile.ContentLength != 0))
                {
                    fileimages = IDImageRandom + filename.Substring(filename.LastIndexOf("."));

                }
                else
                {
                    fileimages = "Template.jpg";
                }



                parameters[0].Value = ddlCategory.SelectedItem.Value;
                parameters[1].Value =  txtCategoryForder.Text.Trim();
                parameters[2].Value =  txtCategoryName.Text.Trim();
                parameters[3].Value =  txtCategoryLink.Text.Trim();
                parameters[4].Value = fileimages;
                parameters[5].Value = Convert.ToInt32(txtCategoryOrder.Text.Trim());


                if ((ImageFile.PostedFile != null) && (ImageFile.PostedFile.ContentLength != 0))
                {
                    addImageToFile(fileimages, pic);
                }


                VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "insert_CategoriesMenu", parameters);
                VatLid.DAL.INSERT_USER_LOG_NEW(VatLid.Utils.getUserId(Session).ToString(), VatLid.LogType.ADD.ToString(), VatLid.DAL.getCategoryID(FileName), "OK", "0", VatLid.Utils.GetIP());
            }

            Response.Redirect("CategoriesMenu.aspx?CategoryID=" + ddlCategory.SelectedItem.Value);

        }
        catch (Exception ex)
        {
            VatLid.DAL.ExceptionProcess(ex);
        }

    }
    public void addImageToFile(string id, byte[] imgdata)
    {
        string imdDBLocal = ConfigurationSettings.AppSettings["ImageCate"];
        string filepath = imdDBLocal + id.ToString();
        if (File.Exists(filepath))
        {
            File.Delete(filepath);
        }
        FileStream fStream = new FileStream(filepath, FileMode.CreateNew);
        BinaryWriter bw = new BinaryWriter(fStream);
        bw.Write(imgdata);
        bw.Close();
        fStream.Close();
        fStream.Dispose();
        if (bw is IDisposable)
            ((IDisposable)bw).Dispose();
    }
    protected void cmdDelete_Click(object sender, EventArgs e)
    {
        SqlParameter[] parameters = 
					{ 					
						new SqlParameter("@ID", SqlDbType.Int ),
						new SqlParameter("@CategoryStatus", SqlDbType.Int ),
			};
        parameters[0].Value = Convert.ToInt32(id);
        parameters[1].Value = 0;
        VatLid.SqlHelper.ExecuteNonQuery(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "Update_CategoriesMenu_Status", parameters);
        Response.Redirect("CategoriesMenu.aspx?CategoryID=" + ddlCategory.SelectedItem.Value);
    }
    private void BindData()
    {
        SqlParameter[] parameters = { 
											new SqlParameter("@ID", SqlDbType.Int),																																					
			};
        parameters[0].Value = Convert.ToInt32(id);
        DataSet ds;
        ds = VatLid.SqlHelper.ExecuteDataset(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "select_CategoriesMenu_ByID", parameters);
        try
        {
            ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["ParentID"].ToString();
            txtCategoryForder.Text = ds.Tables[0].Rows[0]["CategoryForder"].ToString();
            txtCategoryName.Text = ds.Tables[0].Rows[0]["CategoryName"].ToString();
            txtCategoryLink.Text = ds.Tables[0].Rows[0]["CategoryLink"].ToString();
            txtCategoryOrder.Text = ds.Tables[0].Rows[0]["CategoryOrder"].ToString();

        }
        catch (Exception e)
        {
            VatLid.DAL.ExceptionProcess(e);
        }

    }
    private void BindDataControl()
    {
        VatLid.DAL.FillDataToDropdownList(ddlCategory, "CategoriesMenu WHERE ParentID=0 and CategoryStatus=2 ORDER BY CategoryOrder desc", "ID,CategoryName");
        ListItem liCa = new ListItem("==Danh mục gốc==", "0");
        ddlCategory.Items.Add(liCa);


    }
}
