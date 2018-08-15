using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OnKeeng_ItemMovie_Upload_Excel : System.Web.UI.Page
{
    private string partner = "";//doi tac
    private string category = "";// the loai
    private string namevideo = "";// name video
    private string image = "";// image
    private string filemp4 = "";// file mp4
    private string director = "";// director
    private string actor = "";// actor
    private string country = "";// country
    private string year = "";// year
    private string description = "";//description
    private string typeFilm = "";// type (bo,le)
    private string isInternational = "";// international
    private string contract = "";//contract
    private string expiredLicense = ""; // expired license
    private string isMonopoly = "";// is monopoly
    private string isControls = "";// iscontrols
    private string IMDB = "";// imdb
    private string freeContent = "";//free content
    private string freeData = "";// free data
    private string logoPath = "";// logo path
    private string logoPosition = "";// logo position
    private string filmFree = "";// filmfree
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
        string[] validFileTypes = { "xls", "xlsx" };

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
                partner = dr[0].ToString().Trim();//doi tac
                category = dr[1].ToString().Trim();// the loai
                namevideo = dr[2].ToString().Trim();// name video
                image = dr[3].ToString().Trim();// image
                filemp4 = dr[4].ToString().Trim();// file mp4
                director = dr[5].ToString().Trim();// director
                actor = dr[6].ToString().Trim();// actor
                country = dr[7].ToString().Trim();// country
                year = dr[8].ToString().Trim();// year
                description = dr[9].ToString().Trim();//description
                typeFilm = dr[10].ToString().Trim();// type (bo,le)
                isInternational = dr[11].ToString().Trim();// international
                contract = dr[12].ToString().Trim();//contract
                expiredLicense = dr[13].ToString().Trim(); // expired license
                isMonopoly = dr[14].ToString().Trim();// is monopoly
                isControls = dr[15].ToString().Trim();// iscontrols
                IMDB = dr[16].ToString().Trim();// imdb
                freeContent = dr[17].ToString().Trim();//free content
                freeData = dr[18].ToString().Trim();// free data
                logoPath = dr[19].ToString().Trim();// logo path
                logoPosition = dr[20].ToString().Trim();// logo position
                filmFree = dr[21].ToString().Trim();// filmFree

                #region validate input
                // check partner
                if (!string.IsNullOrEmpty(VatLidOnPhim.Utils.KillChars(partner)))
                {
                    partner = VatLidOnPhim.DAL.Validate_input_values(partner, "1");
                    if (partner == "0")
                    {
                        lbError.Text = "Bạn nhập sai Tên đối tác, vui lòng nhập lại.";
                        return;
                    }

                }

                // check category
                if (!string.IsNullOrEmpty(VatLidOnPhim.Utils.KillChars(category)))
                {
                    //if (VatLidOnPhim.DAL.Validate_input_values(category, "2") == "0")
                    //{
                    //    lbError.Text = "Bạn nhập sai Tên thể loại, vui lòng nhập lại.";
                    //    return;
                    //}

                    string[] categoryArr = category.Split(',');
                    string result = "";
                    category = "";
                    int stt = 1;
                    foreach (string item in categoryArr)
                    {
                        result = VatLidOnPhim.DAL.getId_fromName(item, "sp_getIdCategory_fromName");

                        if (result == "0")
                        {
                            lbError.Text = "Bạn nhập sai Tên thể loại " + item + ", vui lòng nhập lại.";
                            return;
                        }
                        else
                        {
                            category += result;
                            if (stt < categoryArr.Length)
                                category += ",";
                        }
                        stt++;
                    }

                }




                // check đạo diễn
                if (!string.IsNullOrEmpty(VatLidOnPhim.Utils.KillChars(director)))
                {
                    string[] directorArr = director.Split(',');
                    string result = "";
                    director = "";
                    int stt = 1;
                    foreach (string item in directorArr)
                    {
                        result = VatLidOnPhim.DAL.getId_fromName(item, "sp_getIdDirector_fromName");

                        if (result == "0")
                        {
                            lbError.Text = "Bạn nhập sai Tên đạo diễn " + item + ", vui lòng nhập lại.";
                            return;
                        }
                        else
                        {
                            director += result;
                            if (stt < directorArr.Length)
                                director += ",";
                        }
                        stt++;
                    }

                }
                // check diễn viên
                if (!string.IsNullOrEmpty(VatLidOnPhim.Utils.KillChars(actor)))
                {
                    string[] actorArr = actor.Split(',');
                    string result = "";
                    actor = "";
                    int stt = 1;
                    foreach (string item in actorArr)
                    {
                        result = VatLidOnPhim.DAL.getId_fromName(item, "sp_getIdActor_fromName");

                        if (result == "0")
                        {
                            lbError.Text = "Bạn nhập sai Tên diễn viên " + item + ", vui lòng nhập lại.";
                            return;
                        }
                        else
                        {
                            actor += result;
                            if (stt < actorArr.Length)
                                actor += ",";
                        }
                        stt++;
                    }

                }
                // check quốc gia
                if (!string.IsNullOrEmpty(VatLidOnPhim.Utils.KillChars(country)))
                {
                    string[] countryArr = country.Split(',');
                    string result = "";
                    country = "";
                    int stt = 1;
                    foreach (string item in countryArr)
                    {
                        result = VatLidOnPhim.DAL.getId_fromName(item, "sp_getIdCountry_fromName");

                        if (result == "0")
                        {
                            lbError.Text = "Bạn nhập sai Tên quốc gia " + item + ", vui lòng nhập lại.";
                            return;
                        }
                        else
                        {
                            country += result;
                            if (stt < countryArr.Length)
                                country += ",";
                        }
                        stt++;
                    }

                }


                #endregion end validate input

                //validate
                //if (ItemName == "" || ItemName.Length > 100 - 1)
                //{
                //    lbError.Text = "Thông tin ItemName sai dữ liệu";
                //    return;
                //}
                //if (ItemFile == "" || ItemFile.Length > 300 - 1)
                //{
                //    lbError.Text = "Thông tin ItemFile sai dữ liệu";
                //    return;
                //}
                //if (Episode == "")
                //{
                //    lbError.Text = "Episode not null";
                //    return;
                //}
                //if (ItemDate == "" || ItemDate.Length > 50 - 1)
                //{
                //    lbError.Text = "Thông tin ItemDate sai dữ liệu";
                //    return;
                //}
            }

            //insert DATA
            foreach (DataRow dr in dt.Rows)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@partner", partner);
                cmd.Parameters.AddWithValue("@category", category);
                cmd.Parameters.AddWithValue("@namevideo", namevideo);
                cmd.Parameters.AddWithValue("@image", image);
                cmd.Parameters.AddWithValue("@filemp4", filemp4);
                cmd.Parameters.AddWithValue("@director", director);
                cmd.Parameters.AddWithValue("@actor", actor);
                cmd.Parameters.AddWithValue("@country", country);
                cmd.Parameters.AddWithValue("@year", year);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@typeFilm", typeFilm == "" ? "0" : typeFilm);
                cmd.Parameters.AddWithValue("@isInternational", isInternational == "" ? "0" : isInternational);
                cmd.Parameters.AddWithValue("@contract", contract);
                cmd.Parameters.AddWithValue("@expiredLicense", expiredLicense);
                cmd.Parameters.AddWithValue("@isMonopoly", isMonopoly);
                cmd.Parameters.AddWithValue("@isControls", isControls == "" ? "0" : isControls);
                cmd.Parameters.AddWithValue("@IMDB", IMDB == "" ? "0" : IMDB);
                cmd.Parameters.AddWithValue("@freeContent", freeContent == "" ? "0" : freeContent);
                cmd.Parameters.AddWithValue("@freeData", freeData == "" ? "0" : freeData);
                cmd.Parameters.AddWithValue("@logoPath", logoPath);
                cmd.Parameters.AddWithValue("@logoPosition", logoPosition);
                cmd.Parameters.AddWithValue("@userLogin", Session["USER"].ToString().ToUpper());
                cmd.Parameters.AddWithValue("@filmFree", filmFree == "" ? "0" : filmFree);

                VatLidOnPhim.DAL.GetDataSet("sp_Upload_batch_Itemmovie", cmd);

                //SqlCommand cmd = new SqlCommand();
                //cmd.Parameters.AddWithValue("@itemName", ItemName);
                //cmd.Parameters.AddWithValue("@itemFile", ItemFile);
                //cmd.Parameters.AddWithValue("@ePisode", Episode);
                //cmd.Parameters.AddWithValue("@itemDate", ItemDate);

                //dsResult = VatLidOnPhim.DAL.GetDataSet("ItemMovieGroup_ImprortExcel_v2", cmd);

                //if (Convert.ToInt32(dsResult.Tables[0].Rows[0][0].ToString()) == 1)
                //{
                //    lbError.Text = "Upload success";
                //}
                //else
                //{
                //    lbError.Text = "Upload error";
                //    return;
                //}



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