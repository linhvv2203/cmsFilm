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
using System.Text;
using VatLid;

namespace MarketVN.Sysadmin
{

	public partial class usergroup_data : System.Web.UI.Page
	{
		protected string sID="";
		protected string TYPE="";
		protected string SQL;
        protected string FileName = "";

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

			TYPE=Request.QueryString["type"];

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
							switch(TYPE)
							{
                                //case "delete":
                                //    break;
								case "edit":					
									BindData();
									break;
								default:
									Label1.Text="Thêm nhóm người dùng";
									break;
							}
						}
				}
			}
			else
                Response.Redirect(VatLid.Variables.sWebRoot + "logout.aspx");
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
			SQL="SELECT * FROM UserGroups WHERE ID=" + sID;
			try
			{
				ArrayList al=DAL.GetDataReaderToArrayList(SQL);
				string [][] arrReturn =(string[][]) al.ToArray(typeof(string[]));

				txtUserGroupName.Text=arrReturn[0][1].ToString();
				txtUserGroupDesc.Text=arrReturn[0][2].ToString();
				ddlUserGroupStatus.Items.FindByValue(arrReturn[0][3].ToString()).Selected=true;
			}
			catch(Exception e)
			{
				VatLid.DAL.ExceptionProcess(e);
			}
			
		}
		protected void cmdSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(TYPE == "edit")
				{
					SQL = "UPDATE UserGroups SET UserGroupName=N'" + Utils.safeString(txtUserGroupName.Text)  + "'";
					SQL += ",UserGroupDesc=N'" + Utils.safeString(txtUserGroupDesc.Text) + "'";
					SQL += ",UserGroupStatus=" + ddlUserGroupStatus.SelectedItem.Value + "";
					SQL += " WHERE ID=" + sID;
					VatLid.DAL.ExecuteQuery(SQL);
                    VatLid.DAL.INSERT_USER_LOG_NEW(VatLid.Utils.getUserId(Session).ToString(), VatLid.LogType.UPDATE.ToString(), VatLid.DAL.getCategoryID(FileName), "OK", sID, VatLid.Utils.GetIP());
					string strData = "Cập nhật nhóm người dùng:" + txtUserGroupName.Text;
				
				}
				else
				{
					SQL = "INSERT INTO UserGroups(UserGroupName,UserGroupDesc,UserGroupStatus) VALUES(N'" + txtUserGroupName.Text + "'";
					SQL += ",N'" + Utils.safeString(txtUserGroupDesc.Text);
					SQL += "'," + ddlUserGroupStatus.SelectedItem.Value + ")";
					VatLid.DAL.ExecuteQuery(SQL);
                    VatLid.DAL.INSERT_USER_LOG_NEW(VatLid.Utils.getUserId(Session).ToString(), VatLid.LogType.ADD.ToString(), VatLid.DAL.getCategoryID(FileName), "OK", "0", VatLid.Utils.GetIP());
					string UserGroupID;
					//Lay ID vua insert
					SQL="SELECT TOP 1 ID FROM UserGroups ORDER BY ID DESC";
					ArrayList al=VatLid.DAL.GetDataReaderToArrayList(SQL);
					string[][] arrReturn=(string[][]) al.ToArray(typeof(string[]));
					UserGroupID=arrReturn[0][0].ToString();
					//lay danh sach chuc nang he thong de insert vao bang Rights
					al=VatLid.DAL.GetDataReaderToArrayList("Select ID from SysFuncs");
					arrReturn=(string[][]) al.ToArray(typeof(string[]));
					int iCount=al.Count;
					for(int i=0;i<iCount;i++)
					{
						SQL="INSERT INTO Rights(UserGroupID,FuncID,RightsStatus) VALUES("+ UserGroupID+ ","+ arrReturn[i][0] +",0)";
						VatLid.DAL.ExecuteQuery(SQL);
					}

					
				}
				Response.Redirect("usergroups.aspx");
			}
			catch(Exception err)
			{
				VatLid.DAL.ExceptionProcess(err);
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
