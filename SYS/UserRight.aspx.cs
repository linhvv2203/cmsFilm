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
	public partial class UserRight : System.Web.UI.Page
	{
		protected string sID="";
        protected string sFilterCategory = "";
        protected string SQL = "";
        protected DataView dv = null;
		string sKey = "";
		
		string[] arrKey;
		private void Page_Load(object sender, System.EventArgs e)
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

			if(!Page.IsPostBack)
			{
				BindData();
                BindDataControl();
				BindDataSysFunc();
				BindDataSysFuncSelected();
				
			}
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
		private void BindData()
		{

			string sql = "SELECT *  FROM viwUsers WHERE ID="+ sID;
			try
			{

				dgrNguoiDung.DataSource=VatLid.DAL.CreateDataView(sql)  ;
				VatLid.DAL.FetchDataGridColumn(dgrNguoiDung,"Tài khoản","UserName");
				VatLid.DAL.FetchDataGridColumn(dgrNguoiDung,"Họ tên","UserRealName");
				VatLid.DAL.FetchDataGridColumn(dgrNguoiDung,"Thuộc nhóm","UserGroupName");			
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
                        SQL += " OR CHARINDEX(N'" + Keyword + "',FuncFile)<>0";
                        SQL += " OR CHARINDEX(N'" + Keyword + "',FuncGroup)<>0";
                        SQL += " OR CHARINDEX(N'" + Keyword + "',FuncDesc)<>0)";
                        break;
                    case 1:
                        SQL += " AND CHARINDEX(N'" + Keyword + "',FuncName)<>0";
                        break;
                    case 2:
                        SQL += " AND CHARINDEX(N'" + Keyword + "',FuncFile)<>0";
                        break;
                    case 3:
                        SQL += " AND CHARINDEX(N'" + Keyword + "',FuncGroup)<>0";
                        break;
                    case 4:
                        SQL += " AND CHARINDEX(N'" + Keyword + "',FuncDesc)<>0";
                        break;

                }
            }
            #endregion

            SQL += " ORDER BY FuncName asc";
            return SQL;
        }


		private void BindDataSysFunc()
		{

			try
			{

                SQL = BuildFilter();

                dgrCommon.DataSource = DAL.CreateDataView(SQL);



                VatLid.DAL.FetchDataGridColumn(dgrCommon, "ID", "ID");
                VatLid.DAL.FetchDataGridColumn(dgrCommon, "Chức năng", "FuncName");
                VatLid.DAL.FetchDataGridColumn(dgrCommon, "Tên file", "FuncFile");
                VatLid.DAL.FetchDataGridColumn(dgrCommon, "Nhóm", "FuncGroup");
                VatLid.DAL.FetchDataGridColumn(dgrCommon, "Mô tả", "FuncDesc");		
	

                dgrCommon.DataKeyField = "ID";
				dgrCommon.AutoGenerateColumns=false;		
				dgrCommon.DataBind();
			}
			catch(Exception e)
			{
				VatLid.DAL.ExceptionProcess(e);
			}
		
		}


		private void BindDataSysFuncSelected()
		{

			try
			{

                SQL = "SELECT USER_FUNCTION_ACTION.ID as ID,FuncName,FuncFile,isAdded,isDeleted,isEdited,isUpdated,isViewed,isAllViewed FROM USER_FUNCTION_ACTION,SysFuncs where USER_FUNCTION_ACTION.FileNameID=SysFuncs.ID and UserID=" + sID;
				dgrCommon1.DataSource=DAL.CreateDataView(SQL)  ;
				
				DAL.FetchDataGridColumn(dgrCommon1,"Tên chức năng","FuncName");
				DAL.FetchDataGridColumn(dgrCommon1,"Tệp xử lý","FuncFile");
				DAL.FetchDataGridColumn(dgrCommon1,"isAdded","isAdded");
				DAL.FetchDataGridColumn(dgrCommon1,"isDeleted","isDeleted");
				DAL.FetchDataGridColumn(dgrCommon1,"isEdited","isEdited");
				DAL.FetchDataGridColumn(dgrCommon1,"isUpdated","isUpdated");
				DAL.FetchDataGridColumn(dgrCommon1,"isViewed","isViewed");
                DAL.FetchDataGridColumn(dgrCommon1, "isAllViewed", "isAllViewed");
				
				dgrCommon1.DataKeyField="ID";
				dgrCommon1.AutoGenerateColumns=false;		
				dgrCommon1.DataBind();
			}
			catch(Exception e)
			{
				VatLid.DAL.ExceptionProcess(e);
			}
	
		}


     

		private void BindDataChecked()
		{
			string SQLchecked="";
            SQLchecked = "SELECT isDeleted,isEdited,isUpdated,isViewed,isAllViewed FROM Users WHERE ID=" + sID;
			try
			{
				ArrayList al=DAL.GetDataReaderToArrayList(SQLchecked);
				string[][] arrReturn=(string[][]) al.ToArray(typeof(string[]));

				chkIsDeleted.Checked=(arrReturn[0][0].ToString()=="1" ? true:false);
				chkisEdited.Checked=(arrReturn[0][1].ToString()=="1" ? true:false);
				chkisUpdated.Checked=(arrReturn[0][2].ToString()=="1" ? true:false);
				chkisViewed.Checked=(arrReturn[0][3].ToString()=="1" ? true:false);
                chkIsXemAll.Checked = (arrReturn[0][4].ToString() == "1" ? true : false);
					
			}
			catch(Exception e)
			{
				VatLid.DAL.ExceptionProcess(e);
			}
		}
		
		private void dgrCommon_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e) 
		{
			dgrCommon.CurrentPageIndex= e.NewPageIndex;
			BindData();
			BindDataSysFunc();
			BindDataSysFuncSelected();

		}
		private void dgrCommon1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e) 
		{
			dgrCommon1.CurrentPageIndex= e.NewPageIndex;
			BindData();
			BindDataSysFunc();
			BindDataSysFuncSelected();

		}
		private void getAllChecked()
		{

			string SQL = "SELECT FunctionNew from Users WHERE ID=" + sID ;
			ArrayList al=VatLid.DAL.GetDataReaderToArrayList(SQL);
			string[][] arrReturn=(string[][]) al.ToArray(typeof(string[]));
			sKey=arrReturn[0][0];
			arrKey = sKey.Split(',');
			for (int i = 0; i < arrKey.Length; i++)
			{
				((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked = true;
			}
		}
		

	
        protected void cmdThem_Click(object sender, EventArgs e)
        {
            //Them quyen Add
            if (chkIsAdd.Checked == true)
            {
                int i;
                string sTemp = "";
                for (i = 0; i < dgrCommon1.Items.Count; i++)
                {
                    if (((CheckBox)dgrCommon1.Items[i].FindControl("chkAllDelete1")).Checked == true)
                    {
                        if (sTemp != "") { sTemp += ","; }
                        sTemp += dgrCommon1.DataKeys[i];
                    }

                }
                if (sTemp != "")
                {
                    SQL = "Update USER_FUNCTION_ACTION set isAdded = 1 WHERE ID in (" + sTemp + ")";
                    VatLid.DAL.ExecuteQuery(SQL);
                }
                else
                {
                    VatLid.MessageBox.Show("Bạn phải chọn danh mục để thực hiện");
                }
            }
            // Delete

            if (chkIsDeleted.Checked == true)
            {
                int i;
                string sTemp = "";
                for (i = 0; i < dgrCommon1.Items.Count; i++)
                {
                    if (((CheckBox)dgrCommon1.Items[i].FindControl("chkAllDelete1")).Checked == true)
                    {
                        if (sTemp != "") { sTemp += ","; }
                        sTemp += dgrCommon1.DataKeys[i];
                    }

                }
                if (sTemp != "")
                {
                    SQL = "Update USER_FUNCTION_ACTION set isDeleted = 1 WHERE ID in (" + sTemp + ")";
                    VatLid.DAL.ExecuteQuery(SQL);
                }
                else
                {
                    VatLid.MessageBox.Show("Bạn phải chọn danh mục để thực hiện");
                }
            }

            // Update
            if (chkisUpdated.Checked == true)
            {
                int i;
                string sTemp = "";
                for (i = 0; i < dgrCommon1.Items.Count; i++)
                {
                    if (((CheckBox)dgrCommon1.Items[i].FindControl("chkAllDelete1")).Checked == true)
                    {
                        if (sTemp != "") { sTemp += ","; }
                        sTemp += dgrCommon1.DataKeys[i];
                    }

                }
                if (sTemp != "")
                {
                    SQL = "Update USER_FUNCTION_ACTION set isUpdated = 1 WHERE ID in (" + sTemp + ")";
                    VatLid.DAL.ExecuteQuery(SQL);
                }
                else
                {
                    VatLid.MessageBox.Show("Bạn phải chọn danh mục để thực hiện");
                }
            }

            // Delete
            if (chkisEdited.Checked == true)
            {
                int i;
                string sTemp = "";
                for (i = 0; i < dgrCommon1.Items.Count; i++)
                {
                    if (((CheckBox)dgrCommon1.Items[i].FindControl("chkAllDelete1")).Checked == true)
                    {
                        if (sTemp != "") { sTemp += ","; }
                        sTemp += dgrCommon1.DataKeys[i];
                    }

                }
                if (sTemp != "")
                {
                    SQL = "Update USER_FUNCTION_ACTION set isEdited = 1 WHERE ID in (" + sTemp + ")";
                    VatLid.DAL.ExecuteQuery(SQL);
                }
                else
                {
                    VatLid.MessageBox.Show("Bạn phải chọn danh mục để thực hiện");
                }
            }

            // View

            if (chkisViewed.Checked == true)
            {
                int i;
                string sTemp = "";
                for (i = 0; i < dgrCommon1.Items.Count; i++)
                {
                    if (((CheckBox)dgrCommon1.Items[i].FindControl("chkAllDelete1")).Checked == true)
                    {
                        if (sTemp != "") { sTemp += ","; }
                        sTemp += dgrCommon1.DataKeys[i];
                    }

                }
                if (sTemp != "")
                {
                    SQL = "Update USER_FUNCTION_ACTION set isViewed = 1 WHERE ID in (" + sTemp + ")";
                    VatLid.DAL.ExecuteQuery(SQL);
                }
                else
                {
                    VatLid.MessageBox.Show("Bạn phải chọn danh mục để thực hiện");
                }
            }

            if (chkIsXemAll.Checked == true)
            {
                int i;
                string sTemp = "";
                for (i = 0; i < dgrCommon1.Items.Count; i++)
                {
                    if (((CheckBox)dgrCommon1.Items[i].FindControl("chkAllDelete1")).Checked == true)
                    {
                        if (sTemp != "") { sTemp += ","; }
                        sTemp += dgrCommon1.DataKeys[i];
                    }

                }
                if (sTemp != "")
                {
                    SQL = "Update USER_FUNCTION_ACTION set isAllViewed = 1 WHERE ID in (" + sTemp + ")";
                    VatLid.DAL.ExecuteQuery(SQL);
                }
                else
                {
                    VatLid.MessageBox.Show("Bạn phải chọn danh mục để thực hiện");
                }
            }

            BindData();
            BindDataSysFunc();
            BindDataSysFuncSelected();
        }
        protected void cmdXoa_Click(object sender, EventArgs e)
        {
            int i;
            string sTemp = "";
            for (i = 0; i < dgrCommon1.Items.Count; i++)
            {

                if (((CheckBox)dgrCommon1.Items[i].FindControl("chkAllDelete1")).Checked == true)
                {

                    if (sTemp != "") { sTemp += ","; }
                    sTemp += dgrCommon1.DataKeys[i];

                }

            }
            if (sTemp != "")
            {

                SQL = "DELETE USER_FUNCTION_ACTION WHERE ID in (" + sTemp + ")";
                VatLid.DAL.ExecuteQuery(SQL);
                lblError.Text = "Bạn xoá thành công files:" + sTemp;
            }
            else
            {
                VatLid.MessageBox.Show("Bạn phải chọn danh mục để thực hiện");
            }


            BindData();
            BindDataSysFunc();
            BindDataSysFuncSelected();
        }
       

        protected void cmdGo_Click(object sender, EventArgs e)
        {
            if (chkIsAdd.Checked == true)
            {
                int i;
                string sTemp = "";
                for (i = 0; i < dgrCommon1.Items.Count; i++)
                {
                    if (((CheckBox)dgrCommon1.Items[i].FindControl("chkAllDelete1")).Checked == true)
                    {
                        if (sTemp != "") { sTemp += ","; }
                        sTemp += dgrCommon1.DataKeys[i];
                    }

                }
                if (sTemp != "")
                {
                    SQL = "Update USER_FUNCTION_ACTION set isAdded = 0 WHERE ID in (" + sTemp + ")";
                    VatLid.DAL.ExecuteQuery(SQL);
                }
                else
                {
                    VatLid.MessageBox.Show("Bạn phải chọn danh mục để thực hiện");
                }
            }
            // Delete

            if (chkIsDeleted.Checked == true)
            {
                int i;
                string sTemp = "";
                for (i = 0; i < dgrCommon1.Items.Count; i++)
                {
                    if (((CheckBox)dgrCommon1.Items[i].FindControl("chkAllDelete1")).Checked == true)
                    {
                        if (sTemp != "") { sTemp += ","; }
                        sTemp += dgrCommon1.DataKeys[i];
                    }

                }
                if (sTemp != "")
                {
                    SQL = "Update USER_FUNCTION_ACTION set isDeleted = 0 WHERE ID in (" + sTemp + ")";
                    VatLid.DAL.ExecuteQuery(SQL);
                }
                else
                {
                    VatLid.MessageBox.Show("Bạn phải chọn danh mục để thực hiện");
                }
            }

            // Update
            if (chkisUpdated.Checked == true)
            {
                int i;
                string sTemp = "";
                for (i = 0; i < dgrCommon1.Items.Count; i++)
                {
                    if (((CheckBox)dgrCommon1.Items[i].FindControl("chkAllDelete1")).Checked == true)
                    {
                        if (sTemp != "") { sTemp += ","; }
                        sTemp += dgrCommon1.DataKeys[i];
                    }

                }
                if (sTemp != "")
                {
                    SQL = "Update USER_FUNCTION_ACTION set isUpdated = 0 WHERE ID in (" + sTemp + ")";
                    VatLid.DAL.ExecuteQuery(SQL);
                }
                else
                {
                    VatLid.MessageBox.Show("Bạn phải chọn danh mục để thực hiện");
                }
            }

            // Delete
            if (chkisEdited.Checked == true)
            {
                int i;
                string sTemp = "";
                for (i = 0; i < dgrCommon1.Items.Count; i++)
                {
                    if (((CheckBox)dgrCommon1.Items[i].FindControl("chkAllDelete1")).Checked == true)
                    {
                        if (sTemp != "") { sTemp += ","; }
                        sTemp += dgrCommon1.DataKeys[i];
                    }

                }
                if (sTemp != "")
                {
                    SQL = "Update USER_FUNCTION_ACTION set isEdited = 0 WHERE ID in (" + sTemp + ")";
                    VatLid.DAL.ExecuteQuery(SQL);
                }
                else
                {
                    VatLid.MessageBox.Show("Bạn phải chọn danh mục để thực hiện");
                }
            }

            // View

            if (chkisViewed.Checked == true)
            {
                int i;
                string sTemp = "";
                for (i = 0; i < dgrCommon1.Items.Count; i++)
                {
                    if (((CheckBox)dgrCommon1.Items[i].FindControl("chkAllDelete1")).Checked == true)
                    {
                        if (sTemp != "") { sTemp += ","; }
                        sTemp += dgrCommon1.DataKeys[i];
                    }

                }
                if (sTemp != "")
                {
                    SQL = "Update USER_FUNCTION_ACTION set isViewed = 0 WHERE ID in (" + sTemp + ")";
                    VatLid.DAL.ExecuteQuery(SQL);
                }
                else
                {
                    VatLid.MessageBox.Show("Bạn phải chọn danh mục để thực hiện");
                }
            }

            if (chkIsXemAll.Checked == true)
            {
                int i;
                string sTemp = "";
                for (i = 0; i < dgrCommon1.Items.Count; i++)
                {
                    if (((CheckBox)dgrCommon1.Items[i].FindControl("chkAllDelete1")).Checked == true)
                    {
                        if (sTemp != "") { sTemp += ","; }
                        sTemp += dgrCommon1.DataKeys[i];
                    }

                }
                if (sTemp != "")
                {
                    SQL = "Update USER_FUNCTION_ACTION set isAllViewed = 0 WHERE ID in (" + sTemp + ")";
                    VatLid.DAL.ExecuteQuery(SQL);
                }
                else
                {
                    VatLid.MessageBox.Show("Bạn phải chọn danh mục để thực hiện");
                }
            }

            BindData();
            BindDataSysFunc();
            BindDataSysFuncSelected();
        }
        protected void cmdAllow_Click(object sender, EventArgs e)
        {
            int intAdd = Convert.ToInt32((chkIsAdd.Checked == true) ? "1" : "0");
            int intDelete = Convert.ToInt32((chkIsDeleted.Checked == true) ? "1" : "0");
            int intEdit = Convert.ToInt32((chkisEdited.Checked == true) ? "1" : "0");
            int intUpdate = Convert.ToInt32((chkisUpdated.Checked == true) ? "1" : "0");
            int intView = Convert.ToInt32((chkisViewed.Checked == true) ? "1" : "0");

            int intXemAll = Convert.ToInt32((chkIsXemAll.Checked == true) ? "1" : "0");

            int i;
            string sTemp = "";
            for (i = 0; i < dgrCommon.Items.Count; i++)
            {

                if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == true)
                {

                    if (sTemp != "") { sTemp += ","; }
                    sTemp += dgrCommon.DataKeys[i];

                    int page = Convert.ToInt32(dgrCommon.DataKeys[i]);

                    SQL = "INSERT INTO USER_FUNCTION_ACTION(UserID,FileNameID,isAdded,isDeleted,isEdited,isUpdated,isViewed,isAllViewed)";
                    SQL = SQL + " values(" + sID + "," + page + "," + intAdd + "," + intDelete + "," + intEdit + "," + intUpdate + "," + intView + "," + intXemAll + ")";
                    VatLid.DAL.ExecuteQuery(SQL);
                }

            }
            if (sTemp != "")
            {
                lblError.Text = "Thực hiên thành công:" + sTemp;
            }
            else
            {
                VatLid.MessageBox.Show("Bạn phải chọn danh mục để thực hiện");
            }

            //lblError.Text = SQL;

            BindData();
            BindDataSysFunc();
            BindDataSysFuncSelected();
        }
      
        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            BindData();
            BindDataSysFunc();
            BindDataSysFuncSelected();
        }
        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.dgrCommon.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgrCommon_PageIndexChanged);
            this.dgrCommon1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgrCommon1_PageIndexChanged);
        }
        #endregion
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
            BindDataSysFunc();
            BindDataSysFuncSelected();
        }
}
}

