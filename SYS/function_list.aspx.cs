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
	public partial class function_list : System.Web.UI.Page
	{

        protected string sFilterCategory = "";
        protected string SQL = "";
        protected DataView dv = null;
        protected string intCategoryID;

		protected void Page_Load(object sender, System.EventArgs e)
		{
            intCategoryID = (Request.QueryString["CategoryID"] == null) ? "0" : Request.QueryString["CategoryID"];
            if (Utils.IsNumeric(intCategoryID))
            {
                intCategoryID = intCategoryID;
            }
            else
            {
                intCategoryID = "0";
            }

			#region:"check permission to access system and also permission to access system function"
			string P_I= Request.ServerVariables["PATH_INFO"];
			string[] aPI=P_I.Split('/');
			int iLength=aPI.Length;
			string FileName=aPI[iLength-1];
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
                        ddlCategory.SelectedValue = intCategoryID;
                        BindData();
                    }
                }
            }
            else
            {
                Response.Redirect(VatLid.Variables.sWebRoot + "logout.aspx");
            }
			#endregion
			cmdReGen.Enabled = true;
		}
        private void BindDataControl()
        {

            SQL = "CategoriesMenu WHERE CategoryStatus=2 and ParentID=0 Order by CategoryOrder desc";
            VatLid.DAL.FillDataToDropdownList(ddlCategory, SQL, "ID,CategoryName");
            ListItem liCategory = new ListItem("--Chọn menu--", "0");
            ddlCategory.Items.Add(liCategory);

            SQL = "CategoriesMenu WHERE CategoryStatus=2 and ParentID=0 Order by CategoryOrder desc";
            VatLid.DAL.FillDataToDropdownList(ddlCategoryC, SQL, "ID,CategoryName");
            ListItem liCategory1 = new ListItem("--Chọn menu--", "0");
            ddlCategoryC.Items.Add(liCategory1);
            ddlCategoryC.Items.FindByValue("0").Selected = true;

            ListItem litype = new ListItem("--Tất cả--", "0");
            ddlSearch.Items.Add(litype);
            litype = new ListItem("Tên chức năng", "1");
            ddlSearch.Items.Add(litype);
            litype = new ListItem("Tên File", "2");
            ddlSearch.Items.Add(litype);
            litype = new ListItem("Nhóm chức năng", "3");
            ddlSearch.Items.Add(litype);
            litype = new ListItem("Miêu tả", "4");
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

            string Keyword = txtKeyword.Text.Trim();
            Keyword = VatLid.Utils.safeString(Keyword);

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

		private void BindData()
		{
			try
			{
                SQL = BuildFilter();

                dgrCommon.DataSource = VatLid.DAL.CreateDataView(SQL);

                VatLid.DAL.FetchDataGridColumn(dgrCommon, "ID", "ID");
				VatLid.DAL.FetchDataGridColumn(dgrCommon,"Chức năng","FuncName");
                VatLid.DAL.FetchDataGridColumn(dgrCommon,"Tên file", "FuncFile");
                VatLid.DAL.FetchDataGridColumn(dgrCommon,"Nhóm","FuncGroup");
				VatLid.DAL.FetchDataGridColumn(dgrCommon,"Mô tả","FuncDesc");		
	
				VatLid.DAL.FetchDataGridColumn(dgrCommon,"Sửa","ID","function_data.aspx?id={0}&type=edit",VatLid.Variables.EditTitle,0);
				VatLid.DAL.FetchDataGridColumn(dgrCommon,"Xoá","ID","function_data.aspx?id={0}&type=delete",VatLid.Variables.DeleteTitle,0);

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
                string SQL = "DELETE FROM sysfuncs WHERE ID in (" + sTemp + ")";
                VatLid.DAL.ExecuteQuery(SQL);
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
            Response.Redirect("function_data.aspx?CategoryID=" + intCategoryID);
		}
		protected void cmdReGen_Click(object sender, System.EventArgs e)
		{
            VatLid.DAL.ReGenerateRightsFromUserGroupsAndSysFuncs();
            VatLid.DAL.ExecuteQuery("UPDATE Rights Set RightsStatus=1 Where UserGroupID=1 AND RightsType='group'");
            BindData();

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
        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
        protected void cmdConvert4_Click(object sender, EventArgs e)
        {
            int i;
            string sTemp = "";
            for (i = 0; i < dgrCommon.Items.Count; i++)
            {

                if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == true)
                {

                    if (sTemp != "") { sTemp += ","; }
                    sTemp += dgrCommon.DataKeys[i];

                }

            }
            if (sTemp != "")
            {

                SQL = "UPDATE SysFuncs SET MenuID=" + ddlCategoryC.SelectedItem.Value + " WHERE ID in (" + sTemp + ")";
                VatLid.DAL.ExecuteQuery(SQL);
                lblError.Text = "Show succeed:" + sTemp;

            }
            else
            {
                VatLid.MessageBox.Show("You must choose one item to do");
            }
            BindData();
        }
        protected void cmdRight_Click(object sender, EventArgs e)
        {

            int i;
            string sTemp = "";
            for (i = 0; i < dgrCommon.Items.Count; i++)
            {

                if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == true)
                {

                    if (sTemp != "") { sTemp += ","; }
                    sTemp += dgrCommon.DataKeys[i];

                }

            }
            if (sTemp != "")
            {

                SQL = "INSERT INTO Rights(UserGroupID,FuncID,RightsStatus,RightsType) values(5," + sTemp + ",1,'group')";
                VatLid.DAL.ExecuteQuery(SQL);
                lblError.Text = "Show succeed:" + sTemp;

            }
            else
            {
                VatLid.MessageBox.Show("You must choose one item to do");
            }
            BindData();

        }
}
}
