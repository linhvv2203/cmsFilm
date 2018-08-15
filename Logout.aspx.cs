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
using VatLid;

namespace MarketVN.Sysadmin
{

	public partial class Logout : System.Web.UI.Page
	{
        protected string FileName;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Session.Clear();
			Session.RemoveAll();
			//Session.Abandon();
            string P_I = Request.ServerVariables["PATH_INFO"];
            string[] aPI = P_I.Split('/');
            int iLength = aPI.Length;
            FileName = aPI[iLength - 1];

			if(!IsPostBack)
			{
                VatLid.DAL.INSERT_USER_LOG_NEW(VatLid.Utils.getUserId(Session).ToString(), VatLid.LogType.LogOut.ToString(), VatLid.DAL.getCategoryID(FileName), "NOK", "0", VatLid.Utils.GetIP());
                
				Response.Redirect(VatLid.Variables.sWebRoot +  "" +  "login.aspx");
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
