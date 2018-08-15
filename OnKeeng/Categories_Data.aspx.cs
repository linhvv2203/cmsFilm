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
using System.IO;
using VatLidOnPhim;

public partial class CATEGORIES_CHANELS_Categories_Data : System.Web.UI.Page
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
                        Label1.Text = "THÊM MỚI CATEGORIES.";
                        hiddenTime.Value = "";
                        //txtUserName.Text = Session["USER"].ToString().ToUpper();
                        break;
                    default:
                        Label1.Text = "CẬP NHẬT CATEGORIES.";
                        Load_Data(id);
                        //ddldt.Enabled = false;
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
        //ddldt.SelectedValue = "1";
    }
    protected void Load_Data(string id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@ID",id);
        
        DataSet ds;
        ds = DAL.GetDataSet("cms_Categories&Select", DAL.getConnectionStringOnPhim(), cmd);

        try
        {
            txtName.Text = ds.Tables[0].Rows[0]["CategoryName"].ToString();
            txtDesc.Text = ds.Tables[0].Rows[0]["description"].ToString();
            file_image = ds.Tables[0].Rows[0]["url_images"].ToString();
            imagedd.ImageUrl = VatLidOnPhim.Variables.ImageUrl_Categories + file_image;
            //ddldt.SelectedValue = ds.Tables[0].Rows[0]["idcp"].ToString();
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

        if (id == "0"&& getTitle(txtName.Text.Trim()) == "1")
        {
            lblError.Text = "Category đã nhập trùng. Mời bạn nhập tiêu đề khác";
            return;
        }
        //if (((ImageFileImage.PostedFile != null) && (ImageFileImage.PostedFile.ContentLength != 0)))
        //{
        //    if (ImageFileImage.PostedFile.ContentType != "image/jpeg" && ImageFileImage.PostedFile.ContentType != "image/pjpeg" && ImageFileImage.PostedFile.ContentType != "image/gif")
        //    {
        //        lblError.Text =Server.HtmlEncode( "You don't upload type of file \"" + ImageFileImage.PostedFile.ContentType + "\". Only upload type of file .jpg in in File 1.");
        //        return;
        //    }
        //    string filename1 = ImageFileImage.PostedFile.FileName;
        //    int pos1 = filename1.LastIndexOfAny(new char[] { '/', '\\' });
        //    if (pos1 >= 0) filename1 = filename1.Substring(pos1 + 1);
        //}
        if (id != "0")
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@id", VatLidOnPhim.Utils.safeString(id));
            //cmd.Parameters.AddWithValue("@ParentID", 0);
            cmd.Parameters.AddWithValue("@CategoryName", VatLidOnPhim.Utils.safeString(txtName.Text.Trim()));
            //cmd.Parameters.AddWithValue("@URL",VatLidOnPhim.Utils.safeString( txturl.Text));
            //cmd.Parameters.AddWithValue("@CategoryID", 0);
            //cmd.Parameters.AddWithValue("@Order", 1);
            cmd.Parameters.AddWithValue("@Status", 1);
            cmd.Parameters.AddWithValue("@Description", VatLidOnPhim.Utils.safeString(txtDesc.Text));
            cmd.Parameters.AddWithValue("@setTop", 0);
          
            //if (ImageFileImage.HasFile)
            //{
            //    cmd.Parameters.AddWithValue("@url_images", DateTime.Now.ToString("yyyy/MM/dd").Replace("/", "") + "/" + id + ".jpg");

            //    if (VatLidOnPhim.Utils.checkImageType(ImageFileImage, lblError, DateTime.Now.ToString("yyyy/MM/dd").Replace("/", "") + "/" + id + ".jpg", 2, 1, DateTime.Now) == true)
            //    {
            //        return;
            //    }
            //}
            //else
            //{

            //    cmd.Parameters.AddWithValue("@url_images",VatLidOnPhim.Utils.safeString( file_image));
            //}
            DAL.GetDataSet("cms_Categories&Sync", VatLidOnPhim.DAL.getConnectionStringOnPhim(), cmd);
            VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUserId(Session).ToString(), VatLidOnPhim.LogType.UPDATE.ToString(), VatLidOnPhim.DAL.getCategoryID(FileName), "OK", id.ToString(), VatLidOnPhim.Utils.GetIP());
            lblError.Text = "ĐÃ SỬA THÀNH CÔNG!";
            Load_Data(id);
        }

        else //them moi
        {
            //if (!ImageFileImage.HasFile)
            //{
            //    lblError.Text = "Bạn phải chọn ảnh đại diện";
            //    return;
            //}
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@id", VatLidOnPhim.Utils.safeString(id));
            cmd.Parameters.AddWithValue("@CategoryName",VatLidOnPhim.Utils.safeString( txtName.Text.Trim()));
            cmd.Parameters.AddWithValue("@SetTop", 0);
            cmd.Parameters.AddWithValue("@Description", VatLidOnPhim.Utils.safeString(txtDesc.Text));
            cmd.Parameters.AddWithValue("@Status", 1);
          

            VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUserId(Session).ToString(), VatLidOnPhim.LogType.ADD.ToString(), VatLidOnPhim.DAL.getCategoryID(FileName), "OK", id.ToString(), VatLidOnPhim.Utils.GetIP());
            DataSet ds = DAL.GetDataSet("cms_Categories_ItemMovie&Sync", DAL.getConnectionStringOnPhim(), cmd);
            string IDNewItem = ds.Tables[0].Rows[0]["IDNewItem"].ToString().Trim();
            VatLidOnPhim.DAL.INSERT_USER_LOG_NEW(VatLidOnPhim.Utils.getUserId(Session).ToString(), VatLidOnPhim.LogType.ADD.ToString(), VatLidOnPhim.DAL.getCategoryID(FileName), "OK", id.ToString(), VatLidOnPhim.Utils.GetIP());
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@id", VatLidOnPhim.Utils.safeString(IDNewItem));
            cmd.Parameters.AddWithValue("@img", DateTime.Now.ToString("yyyy/MM/dd").Replace("/", "") + "/" + IDNewItem + ".jpg");
            DAL.GetDataSet("cms_cate_updateimg",DAL.getConnectionStringOnPhim(),cmd);

            string folder = DateTime.Now.ToString("yyyy/MM/dd").Replace("/", "") + "/" + IDNewItem + ".jpg";
            //if (ImageFileImage.HasFile)
            //{
            //    if (VatLidOnPhim.Utils.checkImageType(ImageFileImage, lblError, folder, 2, 1, DateTime.Now) == true)
            //    {
            //        return;
            //    }
            //}
            lblError.Text = "ĐÃ THÊM THÀNH CÔNG!";
        }
    }
    public void addimageToFile(string id, byte[] imgdata)
    {
        string imdDBLocal = ConfigurationSettings.AppSettings["Image_Categories"];
        string folderDay = imdDBLocal + "/" + DateTime.Now.ToString("yyyy/MM/dd").Replace("/", "");
        string filepath = imdDBLocal + "/" + id.ToString();

        if (!Directory.Exists(folderDay))
        {
            Directory.CreateDirectory(folderDay);
        }
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
    public void Delete_file()
    {
        string imdDBLocal = ConfigurationSettings.AppSettings["ImageUrl_Categories"];
        string filepath = imdDBLocal + "/" + file_image;
        if (File.Exists(filepath))
        {
            File.Delete(filepath);
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
#region file
class MP3Header
{
    // Public variables for storing the information about the MP3
    public int intBitRate;
    public string strFileName;
    public long lngFileSize;
    public int intFrequency;
    public string strMode;
    public int intLength;
    public string strLengthFormatted;

    // Private variables used in the process of reading in the MP3 files
    private ulong bithdr;
    private bool boolVBitRate;
    private int intVFrames;

    public bool ReadMP3Information(string FileName)
    {
        FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
        // Set the filename not including the path information
        strFileName = @fs.Name;
        char[] chrSeparators = new char[] { '\\', '/' };
        string[] strSeparator = strFileName.Split(chrSeparators);
        int intUpper = strSeparator.GetUpperBound(0);
        strFileName = strSeparator[intUpper];

        // Replace ' with '' for the SQL INSERT statement
        strFileName = strFileName.Replace("'", "''");

        // Set the file size
        lngFileSize = fs.Length;

        byte[] bytHeader = new byte[4];
        byte[] bytVBitRate = new byte[12];
        int intPos = 0;

        // Keep reading 4 bytes from the header until we know for sure that in
        // fact it's an MP3
        do
        {
            fs.Position = intPos;
            fs.Read(bytHeader, 0, 4);
            intPos++;
            LoadMP3Header(bytHeader);
        }
        while (!IsValidHeader() && (fs.Position != fs.Length));

        // If the current file stream position is equal to the length,
        // that means that we've read the entire file and it's not a valid MP3 file
        if (fs.Position != fs.Length)
        {
            intPos += 3;

            if (getVersionIndex() == 3)    // MPEG Version 1
            {
                if (getModeIndex() == 3)    // Single Channel
                {
                    intPos += 17;
                }
                else
                {
                    intPos += 32;
                }
            }
            else                        // MPEG Version 2.0 or 2.5
            {
                if (getModeIndex() == 3)    // Single Channel
                {
                    intPos += 9;
                }
                else
                {
                    intPos += 17;
                }
            }

            // Check to see if the MP3 has a variable bitrate
            fs.Position = intPos;
            fs.Read(bytVBitRate, 0, 12);
            boolVBitRate = LoadVBRHeader(bytVBitRate);

            // Once the file's read in, then assign the properties of the file to the public variables
            intBitRate = getBitrate();
            intFrequency = getFrequency();
            strMode = getMode();
            intLength = getLengthInSeconds();
            strLengthFormatted = getFormattedLength();
            fs.Close();
            fs.Dispose();
            return true;
        }
        return false;
    }

    private void LoadMP3Header(byte[] c)
    {
        // this thing is quite interesting, it works like the following
        // c[0] = 00000011
        // c[1] = 00001100
        // c[2] = 00110000
        // c[3] = 11000000
        // the operator << means that we'll move the bits in that direction
        // 00000011 << 24 = 00000011000000000000000000000000
        // 00001100 << 16 =         000011000000000000000000
        // 00110000 << 24 =                 0011000000000000
        // 11000000       =                         11000000
        //                +_________________________________
        //                  00000011000011000011000011000000
        bithdr = (ulong)(((c[0] & 255) << 24) | ((c[1] & 255) << 16) | ((c[2] & 255) << 8) | ((c[3] & 255)));
    }

    private bool LoadVBRHeader(byte[] inputheader)
    {
        // If it's a variable bitrate MP3, the first 4 bytes will read 'Xing'
        // since they're the ones who added variable bitrate-edness to MP3s
        if (inputheader[0] == 88 && inputheader[1] == 105 &&
            inputheader[2] == 110 && inputheader[3] == 103)
        {
            int flags = (int)(((inputheader[4] & 255) << 24) | ((inputheader[5] & 255) << 16) | ((inputheader[6] & 255) << 8) | ((inputheader[7] & 255)));
            if ((flags & 0x0001) == 1)
            {
                intVFrames = (int)(((inputheader[8] & 255) << 24) | ((inputheader[9] & 255) << 16) | ((inputheader[10] & 255) << 8) | ((inputheader[11] & 255)));
                return true;
            }
            else
            {
                intVFrames = -1;
                return true;
            }
        }
        return false;
    }

    private bool IsValidHeader()
    {
        return (((getFrameSync() & 2047) == 2047) &&
                ((getVersionIndex() & 3) != 1) &&
                ((getLayerIndex() & 3) != 0) &&
                ((getBitrateIndex() & 15) != 0) &&
                ((getBitrateIndex() & 15) != 15) &&
                ((getFrequencyIndex() & 3) != 3) &&
                ((getEmphasisIndex() & 3) != 2));
    }

    private int getFrameSync()
    {
        return (int)((bithdr >> 21) & 2047);
    }

    private int getVersionIndex()
    {
        return (int)((bithdr >> 19) & 3);
    }

    private int getLayerIndex()
    {
        return (int)((bithdr >> 17) & 3);
    }

    private int getProtectionBit()
    {
        return (int)((bithdr >> 16) & 1);
    }

    private int getBitrateIndex()
    {
        return (int)((bithdr >> 12) & 15);
    }

    private int getFrequencyIndex()
    {
        return (int)((bithdr >> 10) & 3);
    }

    private int getPaddingBit()
    {
        return (int)((bithdr >> 9) & 1);
    }

    private int getPrivateBit()
    {
        return (int)((bithdr >> 8) & 1);
    }

    private int getModeIndex()
    {
        return (int)((bithdr >> 6) & 3);
    }

    private int getModeExtIndex()
    {
        return (int)((bithdr >> 4) & 3);
    }

    private int getCoprightBit()
    {
        return (int)((bithdr >> 3) & 1);
    }

    private int getOrginalBit()
    {
        return (int)((bithdr >> 2) & 1);
    }

    private int getEmphasisIndex()
    {
        return (int)(bithdr & 3);
    }

    private double getVersion()
    {
        double[] table = { 2.5, 0.0, 2.0, 1.0 };
        return table[getVersionIndex()];
    }

    private int getLayer()
    {
        return (int)(4 - getLayerIndex());
    }

    private int getBitrate()
    {
        // If the file has a variable bitrate, then we return an integer average bitrate,
        // otherwise, we use a lookup table to return the bitrate
        if (boolVBitRate)
        {
            double medFrameSize = (double)lngFileSize / (double)getNumberOfFrames();
            return (int)((medFrameSize * (double)getFrequency()) / (1000.0 * ((getLayerIndex() == 3) ? 12.0 : 144.0)));
        }
        else
        {
            int[, ,] table =        {
                                { // MPEG 2 & 2.5
                                    {0,  8, 16, 24, 32, 40, 48, 56, 64, 80, 96,112,128,144,160,0}, // Layer III
                                    {0,  8, 16, 24, 32, 40, 48, 56, 64, 80, 96,112,128,144,160,0}, // Layer II
                                    {0, 32, 48, 56, 64, 80, 96,112,128,144,160,176,192,224,256,0}  // Layer I
                                },
                                { // MPEG 1
                                    {0, 32, 40, 48, 56, 64, 80, 96,112,128,160,192,224,256,320,0}, // Layer III
                                    {0, 32, 48, 56, 64, 80, 96,112,128,160,192,224,256,320,384,0}, // Layer II
                                    {0, 32, 64, 96,128,160,192,224,256,288,320,352,384,416,448,0}  // Layer I
                                }
                                };

            return table[getVersionIndex() & 1, getLayerIndex() - 1, getBitrateIndex()];
        }
    }

    private int getFrequency()
    {
        int[,] table =    {  
                            {32000, 16000,  8000}, // MPEG 2.5
                            {    0,     0,     0}, // reserved
                            {22050, 24000, 16000}, // MPEG 2
                            {44100, 48000, 32000}  // MPEG 1
                        };

        return table[getVersionIndex(), getFrequencyIndex()];
    }

    private string getMode()
    {
        switch (getModeIndex())
        {
            default:
                return "Stereo";
            case 1:
                return "Joint Stereo";
            case 2:
                return "Dual Channel";
            case 3:
                return "Single Channel";
        }
    }

    private int getLengthInSeconds()
    {
        // "intKilBitFileSize" made by dividing by 1000 in order to match the "Kilobits/second"
        int intKiloBitFileSize = (int)((8 * lngFileSize) / 1000);
        return (int)(intKiloBitFileSize / getBitrate());
    }

    private string getFormattedLength()
    {
        // Complete number of seconds
        int s = getLengthInSeconds();

        // Seconds to display
        int ss = s % 60;

        // Complete number of minutes
        int m = (s - ss) / 60;

        // Minutes to display
        int mm = m % 60;

        // Complete number of hours
        int h = (m - mm) / 60;

        // Make "hh:mm:ss"
        return h.ToString("D2") + ":" + mm.ToString("D2") + ":" + ss.ToString("D2");
    }

    private int getNumberOfFrames()
    {
        // Again, the number of MPEG frames is dependant on whether it's a variable bitrate MP3 or not
        if (!boolVBitRate)
        {
            double medFrameSize = (double)(((getLayerIndex() == 3) ? 12 : 144) * ((1000.0 * (float)getBitrate()) / (float)getFrequency()));
            return (int)(lngFileSize / medFrameSize);
        }
        else
            return intVFrames;
    }

}
#endregion





