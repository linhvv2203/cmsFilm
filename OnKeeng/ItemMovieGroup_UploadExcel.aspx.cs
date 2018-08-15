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
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

public partial class OnKeeng_ItemMovieGroup_UploadExcel : System.Web.UI.Page
{
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
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string[] validFileTypes = { "xls","xlsx" };

        string ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);

        bool isValidFile = false;

        for (int i = 0; i < validFileTypes.Length; i++)
        {

            if (ext == "." + validFileTypes[i])
            {

                isValidFile = true;

                break;

            }

        }

        if (FileUpload1.HasFile && isValidFile)
        {
            string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);

            string FilePath = Server.MapPath(@"~\\doc\" + FileName);

            FileUpload1.SaveAs(FilePath);

            Import_To_Grid(FilePath, Extension, rbHDR.SelectedItem.Text);
        }
        else
        {
            lbError.Text = "Bạn cần chọn file upload.";
        }
    }


    private void Import_To_Grid(string FilePath, string Extension, string isHDR)
    {
        string conStr = "";
        switch (Extension)
        {
            case ".xls": //Excel 97-03
                conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
                break;
            case ".xlsx": //Excel 07
                conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
                break;
        }

        conStr = String.Format(conStr, FilePath, isHDR);
        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        cmdExcel.Connection = connExcel;

        //Get the name of First Sheet
        connExcel.Open();

        DataTable dtExcelSchema;
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
        connExcel.Close();

        //Read Data from First Sheet
        connExcel.Open();
        cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();

        //Bind Data to GridView
        GridView1.Caption = Path.GetFileName(FilePath);
        GridView1.DataSource = dt;
        GridView1.DataBind();

        //Kiem tra trung
        bool kt = false;
        string note = "";
        DataSet dsResult = null;
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                string ItemName = dr[0].ToString().Trim();
                string ItemFile = dr[1].ToString().Trim();
                string Episode = dr[2].ToString().Trim();
                string ItemDate = dr[3].ToString().Trim();

                if (ItemName == "" || ItemName.Length > 100 - 1)
                {
                    lbError.Text = "Thông tin ItemName sai dữ liệu";
                    return;
                }
                if (ItemFile == "" || ItemFile.Length > 300 - 1)
                {
                    lbError.Text = "Thông tin ItemFile sai dữ liệu";
                    return;
                }
                if (Episode == "")
                {
                    lbError.Text = "Episode not null";
                    return;
                }
                if (ItemDate == "" || ItemDate.Length > 50 - 1)
                {
                    lbError.Text = "Thông tin ItemDate sai dữ liệu";
                    return;
                }
            }

            //insert DATA
            foreach (DataRow dr in dt.Rows)
            {
                string ItemName = dr[0].ToString().Trim();
                string ItemFile = dr[1].ToString().Trim();
                string Episode = dr[2].ToString().Trim();
                string ItemDate = dr[3].ToString().Trim();

                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@itemName", ItemName);
                cmd.Parameters.AddWithValue("@itemFile", ItemFile);
                cmd.Parameters.AddWithValue("@ePisode", Episode);
                cmd.Parameters.AddWithValue("@itemDate", ItemDate);

                dsResult = VatLidOnPhim.DAL.GetDataSet("ItemMovieGroup_ImprortExcel_v2", cmd);

                if (Convert.ToInt32(dsResult.Tables[0].Rows[0][0].ToString()) == 1)
                {
                    lbError.Text = "Upload success";
                }
                else
                {
                    lbError.Text = "Upload error";
                    return;
                }

                //gia_cuoc = gia_cuoc.Replace(".", "");
                //gia_cuoc = gia_cuoc.Replace(",", "");

                //DataSet ds = ImportData(1, hop_dong, doi_tac, dich_vu, huong_dich_vu, command_code, packet_type, ma_phan_kenh, CMD, int.Parse(gia_cuoc), int.Parse(Telco), int.Parse(VTM), int.Parse(vtm_sau_telco), int.Parse(doi_tac_1), int.Parse(Telco_1), int.Parse(Media), int.Parse(media_sau_telco), int.Parse(doi_tac_2));
                //if (ds.Tables.Count > 0)
                //{
                //    if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        if (ds.Tables[0].Rows[0]["note"].ToString().Length > 0)
                //        {
                //            note += ds.Tables[0].Rows[0]["note"].ToString() + "<br/>";
                //        }
                //    }
                //}
            }
        }

        if (note.Length > 0)
        {
            //lbError.Text = note;
        }
        else
        {
            foreach (DataRow dr in dt.Rows)
            {
                //string hop_dong = dr[0].ToString().Trim();
                //string doi_tac = dr[1].ToString().Trim();
                //string dich_vu = dr[2].ToString().Trim();
                //string huong_dich_vu = dr[3].ToString().Trim();
                //string command_code = dr[4].ToString().Trim();
                //string packet_type = dr[5].ToString().Trim();
                //string ma_phan_kenh = dr[6].ToString().Trim();
                //string CMD = dr[7].ToString().Trim();
                //string gia_cuoc = dr[8].ToString().Trim();
                //string Telco = dr[9].ToString().Trim();
                //string VTM = dr[10].ToString().Trim();
                //string vtm_sau_telco = dr[11].ToString().Trim();
                //string doi_tac_1 = dr[12].ToString().Trim();
                //string Telco_1 = dr[13].ToString().Trim();
                //string Media = dr[14].ToString().Trim();
                //string media_sau_telco = dr[15].ToString().Trim();
                //string doi_tac_2 = dr[16].ToString().Trim();

                //gia_cuoc = gia_cuoc.Replace(".", "");
                //gia_cuoc = gia_cuoc.Replace(",", "");

                //DataSet ds = ImportData(0, hop_dong, doi_tac, dich_vu, huong_dich_vu, command_code, packet_type, ma_phan_kenh, CMD, int.Parse(gia_cuoc), int.Parse(Telco), int.Parse(VTM), int.Parse(vtm_sau_telco), int.Parse(doi_tac_1), int.Parse(Telco_1), int.Parse(Media), int.Parse(media_sau_telco), int.Parse(doi_tac_2));
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type">-- 0 Insert, 1: check</param>
    /// <param name="hop_dong"></param>
    /// <param name="doi_tac"></param>
    /// <param name="dich_vu"></param>
    /// <param name="huong_dich_vu"></param>
    /// <param name="command_code"></param>
    /// <param name="packet_type"></param>
    /// <param name="ma_phan_kenh"></param>
    /// <param name="CMD"></param>
    /// <param name="gia_cuoc"></param>
    /// <param name="Telco"></param>
    /// <param name="VTM"></param>
    /// <param name="vtm_sau_telco"></param>
    /// <param name="doi_tac_1"></param>
    /// <param name="Telco_1"></param>
    /// <param name="Media"></param>
    /// <param name="media_sau_telco"></param>
    /// <param name="doi_tac_2"></param>
    public DataSet ImportData(int type, string hop_dong, string doi_tac, string dich_vu, string huong_dich_vu, string command_code, string packet_type, string ma_phan_kenh, string CMD, int gia_cuoc, int Telco, int VTM, int vtm_sau_telco, int doi_tac_1, int Telco_1, int Media, int media_sau_telco, int doi_tac_2)
    {
        DataSet ds = new DataSet();

        try
        {
            SqlParameter[] parameters = 
            { 
                new SqlParameter("@type", SqlDbType.Int),
                new SqlParameter("@hop_dong", SqlDbType.NVarChar),
                new SqlParameter("@doi_tac", SqlDbType.NVarChar),
                new SqlParameter("@dich_vu", SqlDbType.NVarChar),
                new SqlParameter("@huong_dich_vu", SqlDbType.NVarChar),
                new SqlParameter("@command_code", SqlDbType.NVarChar),
                new SqlParameter("@packet_type", SqlDbType.NVarChar),
                new SqlParameter("@ma_phan_kenh", SqlDbType.NVarChar),
                new SqlParameter("@CMD", SqlDbType.NVarChar),
                new SqlParameter("@gia_cuoc", SqlDbType.Int),
                new SqlParameter("@Telco", SqlDbType.Int),
                new SqlParameter("@VTM", SqlDbType.Int),
                new SqlParameter("@vtm_sau_telco", SqlDbType.Int),
                new SqlParameter("@doi_tac_1", SqlDbType.Int),
                new SqlParameter("@Telco_1", SqlDbType.Int),            
                new SqlParameter("@Media", SqlDbType.Int),
                new SqlParameter("@media_sau_telco", SqlDbType.Int),
                new SqlParameter("@doi_tac_2", SqlDbType.Int)
            };

            parameters[0].Value = type;
            parameters[1].Value = hop_dong;
            parameters[2].Value = doi_tac;
            parameters[3].Value = dich_vu;
            parameters[4].Value = huong_dich_vu;
            parameters[5].Value = command_code;
            parameters[6].Value = packet_type;
            parameters[7].Value = ma_phan_kenh;
            parameters[8].Value = CMD;
            parameters[9].Value = gia_cuoc;
            parameters[10].Value = Telco;
            parameters[11].Value = VTM;
            parameters[12].Value = vtm_sau_telco;
            parameters[13].Value = doi_tac_1;
            parameters[14].Value = Telco_1;
            parameters[15].Value = Media;
            parameters[16].Value = media_sau_telco;
            parameters[17].Value = doi_tac_2;

            ds = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "cms_sync_Control_Services_Partner", parameters);
        }
        catch
        {
        };

        return ds;
    }

    protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string FileName = GridView1.Caption;
        string Extension = Path.GetExtension(FileName);
        string FilePath = Server.MapPath(@"~\\doc\" + FileName);
        Import_To_Grid(FilePath, Extension, rbHDR.SelectedItem.Text);
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
    }
}
