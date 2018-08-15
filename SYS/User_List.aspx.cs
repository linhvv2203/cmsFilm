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
using Idunno.AntiCsrf.Configuration;

namespace MarketVN.Sysadmin
{

	public partial class User_List : System.Web.UI.Page
	{

            protected string sFilterCategory = "";
            protected string SQL = "";
            protected DataView dv = null;
            protected string intCategoryID;
            protected string FileName = "";

		    protected void Page_Load(object sender, System.EventArgs e)
		    {
			    #region:"check permission to access system and also permission to access system function"
                intCategoryID = (Request.QueryString["CategoryID"] == null) ? "0" : Request.QueryString["CategoryID"];
                if (Utils.IsNumeric(intCategoryID))
                {
                    intCategoryID = intCategoryID;
                }
                else
                {
                    intCategoryID = "0";
                }

			    string P_I= Request.ServerVariables["PATH_INFO"];
			    string[] aPI=P_I.Split('/');
			    int iLength=aPI.Length;
			    string FileName=aPI[iLength-1];
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
                            BindDataControl();
                            ddlCategory.SelectedValue = intCategoryID;
                            BindData();
					    }
				    }
			    }
			    else
                    Response.Redirect(VatLid.Variables.sWebRoot + "logout.aspx");
			    #endregion
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
            private void BindDataControl()
            {
                ListItem liShow = new ListItem("--Published --", "2");
                ddlStatus.Items.Add(liShow);
                liShow = new ListItem("--Pending--", "1");
                ddlStatus.Items.Add(liShow);
                liShow = new ListItem("--Deleted--", "0");
                ddlStatus.Items.Add(liShow);
                liShow = new ListItem("--All--", "3");
                ddlStatus.Items.Add(liShow);


                SQL = "UserGroups WHERE UserGroupStatus=1 Order by ID asc";
                VatLid.DAL.FillDataToDropdownList(ddlCategory, SQL, "ID,UserGroupName");
                ListItem liCategory = new ListItem("--Chọn Group--", "0");
                ddlCategory.Items.Add(liCategory);


                ListItem litype = new ListItem("--Tất cả--", "0");
                ddlSearch.Items.Add(litype);
                litype = new ListItem("Tên tài khoản", "1");
                ddlSearch.Items.Add(litype);
                litype = new ListItem("THọ và tên", "2");
                ddlSearch.Items.Add(litype);
                litype = new ListItem("Miêu tả", "3");
                ddlSearch.Items.Add(litype);
                litype = new ListItem("Phòng ban", "4");
                ddlSearch.Items.Add(litype);
                litype = new ListItem("Email", "5");
                ddlSearch.Items.Add(litype);
                litype = new ListItem("Mobile Phone", "6");
                ddlSearch.Items.Add(litype);
            }
            private string BuildFilter()
            {
                string sFilterCategory = "";
                SQL = "SELECT * FROM Users where 1=1 ";

                #region lay du lieu UserGroupID
                if (Convert.ToInt32(ddlCategory.SelectedItem.Value) == 0)
                    sFilterCategory = "";
                else
                    sFilterCategory = " AND UserGroupID=" + Convert.ToInt32(ddlCategory.SelectedItem.Value);
                SQL += sFilterCategory;
                #endregion

                #region lay du lieu trang thai Status
                if (Convert.ToInt32(ddlStatus.SelectedItem.Value) == 3)
                    sFilterCategory = "";
                else
                    sFilterCategory = " AND UserStatus=" + Convert.ToInt32(ddlStatus.SelectedItem.Value);
                SQL += sFilterCategory;
                #endregion

                string Keyword = VatLid.Utils.safeString(txtKeyword.Text.Trim());

                #region Du lieu lay theo Keyword
                if (txtKeyword.Text != "")
                {
                    switch (Convert.ToInt32(ddlSearch.SelectedItem.Value))
                    {
                        case 0:
                            SQL += " AND (CHARINDEX(N'" + Keyword + "',UserName)<>0";
                            SQL += " OR CHARINDEX(N'" + Keyword + "',UserRealName)<>0";
                            SQL += " OR CHARINDEX(N'" + Keyword + "',UserDesc)<>0";
                            SQL += " OR CHARINDEX(N'" + Keyword + "',UserDept)<>0";
                            SQL += " OR CHARINDEX(N'" + Keyword + "',Email)<>0";
                            SQL += " OR CHARINDEX(N'" + Keyword + "',Phone)<>0)";
                            break;
                        case 1:
                            SQL += " AND CHARINDEX(N'" + Keyword + "',UserName)<>0";
                            break;
                        case 2:
                            SQL += " AND CHARINDEX(N'" + Keyword + "',UserRealName)<>0";
                            break;
                        case 3:
                            SQL += " AND CHARINDEX(N'" + Keyword + "',UserDesc)<>0";
                            break;
                        case 4:
                            SQL += " AND CHARINDEX(N'" + Keyword + "',UserDept)<>0";
                            break;
                        case 5:
                            SQL += " AND CHARINDEX(N'" + Keyword + "',Email)<>0";
                            break;
                        case 6:
                            SQL += " AND CHARINDEX(N'" + Keyword + "',Phone)<>0";
                            break;

                    }
                }
                #endregion

                SQL += " ORDER BY UserName asc";
                return SQL;
            }

		    private void BindData()
		    {

                SQL = BuildFilter();

			    try 
			    {
                  
                  

                    dv = VatLid.DAL.CreateDataView(SQL);
                    dgrCommon.DataSource = dv;


				    VatLid.DAL.FetchDataGridColumn(dgrCommon,"Tài khoản","UserName");
				    VatLid.DAL.FetchDataGridColumn(dgrCommon,"Họ tên","UserRealName");
                    VatLid.DAL.FetchDataGridColumn(dgrCommon, "Phòng ban", "UserDept");
                    VatLid.DAL.FetchDataGridColumn(dgrCommon, "Email", "Email");
                    VatLid.DAL.FetchDataGridColumn(dgrCommon, "Phone", "Phone");

				    VatLid.DAL.FetchDataGridColumn(dgrCommon,"Sửa","ID","user_data.aspx?id={0}&type=edit",VatLid.Variables.EditTitle,0);

                    //VatLid.DAL.FetchDataGridColumn(dgrCommon, "Quyền Cate", "ID", "user_permission.aspx?id={0}&type=assigned", VatLid.Variables.PermissionTitle, 0);
                    VatLid.DAL.FetchDataGridColumn(dgrCommon, "Menu", "ID", "user_Menu.aspx?id={0}&type=assigned", VatLid.Variables.PermissionTitle, 0);
                    VatLid.DAL.FetchDataGridColumn(dgrCommon, "Action", "ID", "UserRight.aspx?id={0}&type=assigned", VatLid.Variables.ActionTitle, 0);
                    VatLid.DAL.FetchDataGridColumn(dgrCommon, "Alert", "ID", "Useralert_Data.aspx?id={0}&type=edit", VatLid.Variables.ActionTitle, 0);


				    dgrCommon.DataKeyField="ID";
				    dgrCommon.AutoGenerateColumns=false;				
				    dgrCommon.DataBind();
                    //lblTotalRecords.Text = "Tổng số bản ghi: " + dgrCommon.Items.Count.ToString() + " ";
                    lblTotalRecords.Text = "Tổng số bản ghi đăng xem: " + dgrCommon.Items.Count.ToString() + " trên tổng số " + dv.Count.ToString();

			    }
			    catch (Exception e) 
			    {
				    VatLid.DAL.ExceptionProcess(e);
			    }
    			
		    }
		    private void dgrCommon_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e) 
		    {
    			
			    dgrCommon.CurrentPageIndex= e.NewPageIndex;
			    BindData();
		    }

		    protected void cmdDetele_Click(object sender, System.EventArgs e)
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
                    if (Convert.ToInt32(ddlStatus.SelectedItem.Value) != 0)
                    {
                        VatLid.DAL.UpdateStatus("users", "UserStatus", 0, sTemp);
                        lblError.Text = "DELETE succeed";
                        VatLid.DAL.INSERT_USER_LOG_NEW(VatLid.Utils.getUserId(Session).ToString(), VatLid.LogType.Delete.ToString(), VatLid.DAL.getCategoryID(FileName), "OK", sTemp, VatLid.Utils.GetIP());
                    }
                    else
                    {
                        SQL = "delete users where ID in (" + sTemp + ")";
                        VatLid.DAL.ExecuteQuery(SQL);
                        VatLid.DAL.INSERT_USER_LOG_NEW(VatLid.Utils.getUserId(Session).ToString(), VatLid.LogType.DeleteAll.ToString(), VatLid.DAL.getCategoryID(FileName), "OK", sTemp, VatLid.Utils.GetIP());
                    }
                }
                else
                {
                    VatLid.MessageBox.Show("select one to do.");

                }
                BindData();
		    }

		    protected void cmdAdd_Click(object sender, System.EventArgs e)
		    {
                Response.Redirect("user_data.aspx?CategoryID=" + ddlCategory.SelectedItem.Value);
		    }
    		
            protected void cmdAlert_Click(object sender, EventArgs e)
            {
                Response.Redirect("Useralert_Data.aspx?id=0&type=edit");
            }
            protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
            {
                BindData();
            }
            protected void cmdSearch_Click(object sender, EventArgs e)
            {
                BindData();
            }
            protected void cmdPublish_Click(object sender, EventArgs e)
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

                    SQL = "UPDATE users SET UserStatus=2 WHERE ID in (" + sTemp + ")";
                    VatLid.DAL.ExecuteQuery(SQL);
                    VatLid.DAL.INSERT_USER_LOG_NEW(VatLid.Utils.getUserId(Session).ToString(), VatLid.LogType.Publish.ToString(), VatLid.DAL.getCategoryID(FileName), "OK", sTemp, VatLid.Utils.GetIP());
                    lblError.Text = "Show succeed:" + sTemp;

                }
                else
                {
                    VatLid.MessageBox.Show("You must choose one item to do");
                }
                BindData();
            }
            protected void cmdRemove_Click(object sender, EventArgs e)
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

                    SQL = "UPDATE users SET UserStatus=1 WHERE ID in (" + sTemp + ")";
                    VatLid.DAL.ExecuteQuery(SQL);
                    VatLid.DAL.INSERT_USER_LOG_NEW(VatLid.Utils.getUserId(Session).ToString(), VatLid.LogType.Remove.ToString(), VatLid.DAL.getCategoryID(FileName), "OK", sTemp, VatLid.Utils.GetIP());
                    lblError.Text = "Show succeed:" + sTemp;

                }
                else
                {
                    VatLid.MessageBox.Show("You must choose one item to do");
                }
                BindData();
            }
            protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
            {
                BindData();
            }
    }
}
