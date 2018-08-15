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
using VatLid;
using System.Text.RegularExpressions;

namespace MarketVN.Sysadmin
{
	/// <summary>
	/// Summary description for user_permission.
	/// </summary>
	public partial class user_permission : System.Web.UI.Page
	{
		protected string sID="";
		protected string SQL="";
		string[] arrKey;
		string sKey = "";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			sID=Request.QueryString["id"];
            if (Utils.IsNumeric(sID))
            {
                sID = sID;
            }
            else
            {
                sID = "0";
            }

            #region:"check permission to access system and also permission to access system function"
            string P_I = Request.ServerVariables["PATH_INFO"];
            string[] aPI = P_I.Split('/');
            int iLength = aPI.Length;
            string FileName = aPI[iLength - 1];
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
                        BindData();
                        getAllChecked();
                    }
                }
            }
            else
                Response.Redirect(VatLid.Variables.sWebRoot + "login.aspx");
            #endregion

			
		}
		private void BindData()
		{
			string sql = "SELECT ID,CategoryName,CategoryStatus FROM categories WHERE CategoryStatus =2 ORDER BY CategoryName";
			try
			{

				dgrCommon.DataSource=DAL.CreateDataView(sql )  ;
				dgrCommon.DataKeyField="ID";
				DAL.FetchDataGridColumn(dgrCommon,"Tên chức năng","CategoryName");
				DAL.FetchDataGridColumn(dgrCommon,"Trạng thái","CategoryStatus");
				dgrCommon.AutoGenerateColumns=false;		
				dgrCommon.DataBind();
			}
			catch(Exception e)
			{
				VatLid.DAL.ExceptionProcess(e);
			}
		
		}
		private void getAllChecked()
		{

			string SQL = "SELECT FunctionNew from Users WHERE ID=" + sID ;
			ArrayList al=VatLid.DAL.GetDataReaderToArrayList(SQL);
			string[][] arrReturn=(string[][]) al.ToArray(typeof(string[]));
			sKey=arrReturn[0][0];
			arrKey = sKey.Split(',');
			int i;
			for (i=0; i < dgrCommon.Items.Count; i++) 
			{
				for(int j=0;j<arrKey.Length;j++)
					if(arrKey[j].Equals(dgrCommon.DataKeys[i].ToString()))
						((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked = true;
			}
		}
		private void dgrCommon_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e) 
		{
			dgrCommon.CurrentPageIndex= e.NewPageIndex;
			BindData();
			getAllChecked();
		}
		protected void cmdSave_Click(object sender, System.EventArgs e)
		{	
			int i;
			string sTemp="";
			for (i=0; i < dgrCommon.Items.Count; i++) 
			{
    
				if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == true) 
				{
       
					if (sTemp != "") { sTemp += ","; }
					sTemp+= dgrCommon.DataKeys[i];
    
				}
		
    
			}
			SQL="UPDATE Users SET FunctionNew=" + "'" + sTemp + "'" + " WHERE ID=" + sID;
			DAL.ExecuteQuery(SQL);
			BindData();
			getAllChecked();

			
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
			this.dgrCommon.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgrCommon_PageIndexChanged);

		}
		#endregion
	}
}
