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

namespace MarketVN.Sysadmin
{

	public partial class usergroup_permission : System.Web.UI.Page
	{
		protected string sID="";
		protected string sFilterCategory = "";
        protected string SQL = "";
        protected DataView dv = null;
		protected ArrayList al=new ArrayList();

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
                        BindDataControl();
                        BindData();
                        BindDataSysFunc();
                        getAllChecked();
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


        private void BindDataControl()
        {

            SQL = "CategoriesMenu WHERE CategoryStatus=2 and ParentID=0 Order by CategoryOrder desc";
            VatLid.DAL.FillDataToDropdownList(ddlCategory, SQL, "ID,CategoryName");
            ListItem liCategory = new ListItem("--Chọn menu--", "0");
            ddlCategory.Items.Add(liCategory);
            ddlCategory.Items.FindByValue("0").Selected = true;

            ListItem litype = new ListItem("--Tất cả--", "0");
            ddlSearch.Items.Add(litype);
            litype = new ListItem("Tên chức năng", "1");
            ddlSearch.Items.Add(litype);
            litype = new ListItem("Nhóm chức năng", "2");
            ddlSearch.Items.Add(litype);
            litype = new ListItem("Miêu tả", "3");
            ddlSearch.Items.Add(litype);
          

        }
        private string BuildFilter()
        {
            string sFilterCategory = "";
            SQL = "SELECT * FROM SysFuncs  where 1=1 ";


            #region lay du lieu Menu
            if (Convert.ToInt32(ddlCategory.SelectedItem.Value) == 0)
                sFilterCategory = "";
            else
                sFilterCategory = " AND MenuID=" + Convert.ToInt32(ddlCategory.SelectedItem.Value);
            SQL += sFilterCategory;
            #endregion


            string Keyword = VatLid.Utils.safeString(txtKeyword.Text.Trim());
            #region Du lieu lay theo Keyword
            if (txtKeyword.Text != "")
            {
                switch (Convert.ToInt32(ddlSearch.SelectedItem.Value))
                {
                    case 0:
                        SQL += " AND (CHARINDEX(N'" + Keyword + "',FuncName)<>0";
                        SQL += " OR CHARINDEX(N'" + Keyword + "',FuncGroup)<>0";
                        SQL += " OR CHARINDEX(N'" + Keyword + "',FuncDesc)<>0)";
                        break;
                    case 1:
                        SQL += " AND CHARINDEX(N'" + Keyword + "',FuncName)<>0";
                        break;
                    case 2:
                        SQL += " AND CHARINDEX(N'" + Keyword + "',FuncGroup)<>0";
                        break;
                    case 3:
                        SQL += " AND CHARINDEX(N'" + Keyword + "',FuncDesc)<>0";
                        break;
                  
                }
            }
            #endregion


            SQL += " ORDER BY FuncName asc";
            return SQL;
        }

		private void BindData()
		{

			string sql = "SELECT * FROM Users WHERE UserGroupID="+sID + " ORDER BY UserName";
			try
			{

				ArrayList al=DAL.GetDataReaderToArrayList("Select UserGroupName from UserGroups Where ID=" + sID);
				string[][] arrReturn=(string[][]) al.ToArray(typeof(string[]));
				lblGroup.Text = "Phân quyền cho nhóm : " + arrReturn[0][0].ToString();
				dgrNguoiDung.DataSource=VatLid.DAL.CreateDataView(sql)  ;


				VatLid.DAL.FetchDataGridColumn(dgrNguoiDung,"Tài khoản","UserName");
				VatLid.DAL.FetchDataGridColumn(dgrNguoiDung,"Họ tên","UserRealName");
				VatLid.DAL.FetchDataGridColumn(dgrNguoiDung,"Thuộc nhóm","UserGroupID");			
				VatLid.DAL.FetchDataGridColumn(dgrNguoiDung,"Trạng thái","UserStatus");


				dgrNguoiDung.DataKeyField="ID";
				dgrNguoiDung.AutoGenerateColumns=false;				
				dgrNguoiDung.DataBind();
			}
			catch(Exception e)
			{
				VatLid.DAL.ExceptionProcess(e);
			}
		
		}

		private void BindDataSysFunc()
		{

            SqlConnection conn = null;
			try
			{
                SQL = BuildFilter();

                dgrCommon.DataSource = VatLid.DAL.CreateDataView(SQL);

                VatLid.DAL.FetchDataGridColumn(dgrCommon, "ID", "ID");
                VatLid.DAL.FetchDataGridColumn(dgrCommon, "Chức năng", "FuncName");
                VatLid.DAL.FetchDataGridColumn(dgrCommon, "Tên file", "FuncFile");
                VatLid.DAL.FetchDataGridColumn(dgrCommon, "Nhóm", "FuncGroup");
                VatLid.DAL.FetchDataGridColumn(dgrCommon, "Mô tả", "FuncDesc");		
	
				//VatLid.DAL.FetchDataGridColumn(dgrCommon,"Sửa","ID","function_data.aspx?id={0}&type=edit",VatLid.Variables.EditTitle,0);
				//VatLid.DAL.FetchDataGridColumn(dgrCommon,"Xoá","ID","function_data.aspx?id={0}&type=delete",VatLid.Variables.DeleteTitle,0);

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

		private void getAllChecked()
		{
			int i;
			string sTemp="";
			for (i=0; i < dgrCommon.Items.Count; i++) 
			{
				sTemp= "SELECT UserGroupID from viwRightsUserGroups WHERE FuncID="+dgrCommon.DataKeys[i]+ " AND UserGroupID=" + sID + "AND RightsStatus=1" ;
				al=DAL.GetDataReaderToArrayList(sTemp);
				if(al.Count!=0)
					((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked = true;
			}
		}
		private void dgrCommon_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e) 
		{
			dgrCommon.CurrentPageIndex= e.NewPageIndex;
			BindData();
			BindDataSysFunc();
			getAllChecked();
		}
		private void dgrNguoiDung_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e) 
		{
			dgrNguoiDung.CurrentPageIndex= e.NewPageIndex;
			BindData();
			BindDataSysFunc();
			getAllChecked();
		}
		protected void cmdSave_Click(object sender, System.EventArgs e)
		{
			//begin of updating Rights
			int i;
			string sTemp="";
			int skey;
			//INSERT
			for (i=0; i < dgrCommon.Items.Count; i++) 
			{
    
				if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == true) 
				{
					
					skey = Convert.ToInt32(dgrCommon.DataKeys[i]);

					string SQL = "SELECT FuncID from Rights WHERE UserGroupID=" + sID + " And FuncID=" + skey;
					ArrayList al=VatLid.DAL.GetDataReaderToArrayList(SQL);
					string[][] arrReturn=(string[][]) al.ToArray(typeof(string[]));
					

					if(al.Count==0)
					{
						string mSQL = "INSERT INTO Rights(UserGroupID,FuncID,RightsStatus,RightsType) VALUES("+sID+","+skey+",1,'group')";
						VatLid.DAL.ExecuteQuery(mSQL);	
						
					}
					else
					{
						if (sTemp != "") { sTemp += ","; }
						sTemp+= dgrCommon.DataKeys[i];
					}
				}
			}
			if(sTemp!="")
			{
				SQL="UPDATE Rights SET RightsStatus=1 WHERE UserGroupID=" + sID + " AND FuncID in (" + sTemp + ")";
				DAL.ExecuteQuery(SQL);
			}

			sTemp="";
			for (i=0; i < dgrCommon.Items.Count; i++) 
			{
    
				if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == false) 
				{
       
					if (sTemp != "") { sTemp += ","; }
					sTemp+= dgrCommon.DataKeys[i];
    
				}
    
			}
			if(sTemp!="")
			{
				SQL="UPDATE Rights SET RightsStatus=0 WHERE UserGroupID=" + sID + " AND FuncID in (" + sTemp + ")";
				DAL.ExecuteQuery(SQL);
			}			
			BindData();
			BindDataSysFunc();
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
			this.dgrNguoiDung.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgrNguoiDung_PageIndexChanged);
			this.dgrCommon.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgrCommon_PageIndexChanged);

		}
		#endregion
        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            BindData();
            BindDataSysFunc();
            getAllChecked();
        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
            BindDataSysFunc();
            getAllChecked();
        }
}
}
