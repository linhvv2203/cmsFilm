using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using VatLid;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace MarketVN.Sysadmin
{
	/// <summary>
	/// Summary description for usergroups.
	/// </summary>
	public partial class usergroups : System.Web.UI.Page
	{
        protected string FileName = "";
		protected void Page_Load(object sender, System.EventArgs e)
		{
			#region:"check permission to access system and also permission to access system function"
			string P_I= Request.ServerVariables["PATH_INFO"];
			string[] aPI=P_I.Split('/');
			int iLength=aPI.Length;
			FileName=aPI[iLength-1];
			if(Session["USER"]!=null)
			{
				if(DAL.GetRights(Session["USER"].ToString(),FileName)==false)
				{
                    Response.Redirect(VatLid.Variables.sWebRoot + "error_info.aspx?err=21");
				}
				else
				{
					if(!Page.IsPostBack)
					{
						BindData();
					}
				}
			}
			else
                Response.Redirect(VatLid.Variables.sWebRoot + "login.aspx");
			#endregion
		}

        protected override void OnError(EventArgs e)
        {

            if (typeof(System.Web.HttpRequestValidationException) == Server.GetLastError().GetType())
            {
                string strErrMsg = Server.GetLastError().Message;

                VatLid.DAL.ExceptionProcess1(strErrMsg);

                Response.Redirect(VatLid.Variables.sWebRoot + "error_info.aspx?err=33");

            }


            if (typeof(System.Web.HttpException) == Server.GetLastError().GetType())
            {
                string strErrMsg = Server.GetLastError().Message;

                VatLid.DAL.ExceptionProcess1(strErrMsg);

                Response.Redirect(VatLid.Variables.sWebRoot + "error_info.aspx?err=34");

            }


            base.OnError(e);
        }


		private void BindData()
		{
			string sql = "SELECT * FROM Usergroups";
			try
			{

				dgrCommon.DataSource=VatLid.DAL.CreateDataView(sql)  ;
				VatLid.DAL.FetchDataGridColumn(dgrCommon,"Tên nhóm","UserGroupName");
				VatLid.DAL.FetchDataGridColumn(dgrCommon,"Mô tả nhóm","UserGroupDesc");		
				VatLid.DAL.FetchDataGridColumn(dgrCommon,"Trạng thái","UserGroupStatus");		
				VatLid.DAL.FetchDataGridColumn(dgrCommon,"Sửa","ID","usergroup_data.aspx?id={0}&type=edit",VatLid.Variables.EditTitle,0);
				//VatLid.DAL.FetchDataGridColumn(dgrCommon,"Xoá","ID","usergroup_data.aspx?id={0}&type=delete",VatLid.Variables.DeleteTitle,0);
				VatLid.DAL.FetchDataGridColumn(dgrCommon,"Quyền","ID","usergroup_permission.aspx?id={0}&type=assigned",VatLid.Variables.PermissionTitle,0);
				dgrCommon.DataKeyField="ID";
				dgrCommon.AutoGenerateColumns=false;				
				dgrCommon.DataBind();
				lblTotalRecords.Text="Tổng số bản ghi: "+dgrCommon.Items.Count.ToString()+" ";
			}
			catch(Exception e)
			{
				VatLid.DAL.ExceptionProcess(e);
			}
		
		}
		protected void cmdDetele_Click(object sender, System.EventArgs e)
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
            if (sTemp != "")
            {
			    string SQL="DELETE FROM Users WHERE UserGroupID in (" + sTemp + ")";
			    VatLid.DAL.ExecuteQuery(SQL);
			    SQL="DELETE FROM Rights WHERE UserGroupID in (" + sTemp + ")";
			    VatLid.DAL.ExecuteQuery(SQL);
			    SQL="DELETE FROM usergroups WHERE ID in ("+sTemp+")";
			    VatLid.DAL.ExecuteQuery(SQL);
                VatLid.DAL.INSERT_USER_LOG_NEW(VatLid.Utils.getUserId(Session).ToString(), VatLid.LogType.DeleteAll.ToString(), VatLid.DAL.getCategoryID(FileName), "OK", sTemp, VatLid.Utils.GetIP());
            }
            else
            {
                VatLid.MessageBox.Show("Chọn một danh sách để thực hiện.");

            }

			BindData();
		}
		private void dgrCommon_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e) 
		{
			dgrCommon.CurrentPageIndex= e.NewPageIndex;
			BindData();
		}
		protected void cmdAdd_Click(object sender, System.EventArgs e)
		{

			Response.Redirect("usergroup_data.aspx");
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
