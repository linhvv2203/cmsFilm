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
using VatLid;


namespace MarketVN.Sysadmin
{
	

	public partial class Center : System.Web.UI.Page
	{
        protected string SQL = "";
        protected string TITLE = "";
        protected string TEXT = "";
        protected string id = "0";

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (Session["USER"] != null)
            {
               // BindData();
            }
            else
                Response.Redirect(VatLid.Variables.sWebRoot + "logout.aspx");
		}
        private void BindData()
        {
            SqlParameter[] parameters = { 
											new SqlParameter("@ID", SqlDbType.Int),																																					
			};
            parameters[0].Value = Convert.ToInt32(id);
            DataSet ds;
            ds = VatLid.SqlHelper.ExecuteDataset(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "select_Messages_ByID_TB", parameters);
            try
            {
                TITLE = ds.Tables[0].Rows[0]["Title"].ToString();
                TEXT = ds.Tables[0].Rows[0]["MsgContent"].ToString();



            }
            catch (Exception e)
            {
                VatLid.DAL.ExceptionProcess(e);
            }


        }
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion
	}
    
}
