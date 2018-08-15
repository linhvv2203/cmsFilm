using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using VatLid;

namespace MarketVN.Sysadmin
{
	/// <summary>
	/// Summary description for function_data.
	/// </summary>
	public partial class function_data : System.Web.UI.Page
	{
		

		private string  sID;
		private string TYPE;
		private string SQL;
        protected string intCategoryID;
        protected string FileName = "";
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string P_I= Request.ServerVariables["PATH_INFO"];
			string[] aPI=P_I.Split('/');
			int iLength=aPI.Length;
			FileName=aPI[iLength-1];
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

            intCategoryID = (Request.QueryString["CategoryID"] == null) ? "0" : Request.QueryString["CategoryID"];
            if (Utils.IsNumeric(intCategoryID))
            {
                intCategoryID = intCategoryID;
            }
            else
            {
                intCategoryID = "0";
            }

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
                           
							switch(TYPE)
							{
		
								case "edit":					
									BindData();
									break;
								default:
									Label1.Text="Thêm Chức năng hệ thống";
                                    ddlCategory.SelectedValue = intCategoryID;
									break;
							}
						}
					}
				}
			else
                    Response.Redirect(VatLid.Variables.sWebRoot + "logout.aspx");
		}
		private void BindData()
		{
            SQL = "SELECT ID,FuncName,FuncFile,FuncDesc,FuncGroup,MenuID FROM SysFuncs WHERE ID=" + sID;
			ArrayList al=DAL.GetDataReaderToArrayList(SQL);
			string[][] arrReturn=(string[][]) al.ToArray(typeof(string[]));

            if (al.Count > 0)
            {
                txtFuncName.Text = arrReturn[0][1].ToString();
                txtFuncFile.Text = arrReturn[0][2].ToString();
                txtFuncDesc.Text = arrReturn[0][3].ToString();
                txtFuncGroup.Text = arrReturn[0][4].ToString();
                ddlCategory.Items.FindByValue(arrReturn[0][5]).Selected = true;
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
        }

		protected void cmdSave_Click(object sender, System.EventArgs e)
		{
			try
			{
                if (txtFuncName.Text.Length == 0)
                {
                    lblError.Text = "Bạn phải nhập tên chức năng!";
                    return;
                }

                if (txtFuncFile.Text.Length == 0)
                {
                    lblError.Text = "Bạn phải nhập tên file chức năng!";
                    return;
                }


				if(TYPE == "edit")
				{
					SQL = "UPDATE SysFuncs SET FuncName=N'" + Utils.safeString(txtFuncName.Text)  + "'";
                    SQL += ",FuncFile=N'" + Utils.safeString(txtFuncFile.Text.Trim()) + "'";
                    SQL += ",FuncDesc=N'" + Utils.safeString(txtFuncDesc.Text.Trim()) + "'";
                    SQL += ",FuncGroup=N'" + Utils.safeString(txtFuncGroup.Text.Trim()) + "'";
                    SQL += ",MenuID=" + Convert.ToInt32(ddlCategory.SelectedItem.Value) + "";
					SQL += " WHERE ID=" + sID;
					VatLid.DAL.ExecuteQuery(SQL);

					string strData = "Cập nhật chức hệ thống:" + Utils.safeString(txtFuncFile.Text);
				
				}
				else
				{
                    SQL = "INSERT INTO SysFuncs(FuncName,FuncFile,FuncDesc,FuncGroup,MenuID) VALUES(N'" + Utils.safeString(txtFuncName.Text) + "'";
					SQL += ",N'" + Utils.safeString(txtFuncFile.Text.Trim());
                    SQL += "',N'" + Utils.safeString(txtFuncDesc.Text.Trim());
                    SQL += "',N'" + Utils.safeString(txtFuncGroup.Text.Trim());
                    SQL += "'," + Convert.ToInt32(ddlCategory.SelectedItem.Value);
					SQL += ")";
					VatLid.DAL.ExecuteQuery(SQL);
					
				}

                Response.Redirect("function_list.aspx?CategoryID=" + ddlCategory.SelectedItem.Value);

			}
			catch(Exception err)
			{
				VatLid.DAL.ExceptionProcess(err);			
			}

			Response.Redirect(VatLid.Variables.sWebRoot+"error_info.aspx?err=25");
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
