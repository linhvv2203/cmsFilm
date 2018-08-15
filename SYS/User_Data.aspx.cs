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
using VatLid;
using System.Text.RegularExpressions;

namespace MarketVN.Sysadmin
{

	public partial class User_Data : System.Web.UI.Page
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
                            
				            VatLid.DAL.FillDataToDropdownList(ddlUserGroup,"usergroups","ID,UserGroupName");
                            VatLid.DAL.FillDataToDropdownListStatusCP(ddlDoiTacCP, "Partner_Keeng", "id,namePartner");
				            switch(TYPE)
				            {
                              
					            case "edit":					
						            BindData();
						            break;
					            default:
						            Label1.Text="Thêm Người dùng";
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


       

        public bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);


        }


        protected bool checkMumber(string str)
        {
            bool result = false;
            for (int i = 0; i < str.Length; i++)
            {
                if (IsNumber(str.Substring(i, 1)))
                    result = true;

            }
            return result;
        }

		private void BindData()
		{
			SQL="SELECT ID,UserName,UserPw,UserRealName,UserStatus,UserDesc,UserDept,ExpiredDate,UserGroupID,StartAllowedTime,EndAllowedTime,AllowedIPs,Phone,Email,CP FROM Users WHERE ID=" + sID;
			try
			{
				ArrayList al=DAL.GetDataReaderToArrayList(SQL);

                if (al.Count > 0)
                {
                    string[][] arrReturn = (string[][])al.ToArray(typeof(string[]));
                    txtUserName.Text = arrReturn[0][1].ToString();
                    txtUserPw.Text = arrReturn[0][2].ToString();
                    txtConfirmPw.Text = txtUserPw.Text;
                    txtUserRealName.Text = arrReturn[0][3].ToString();
                    txtUserDesc.Text = arrReturn[0][5].ToString();
                    txtUserDept.Text = arrReturn[0][6].ToString();
                 
                    ddlUserGroup.Items.FindByValue(arrReturn[0][8].ToString()).Selected = true;
                 
                    txtPhone.Text = arrReturn[0][12].ToString();
                    txtEmail.Text = arrReturn[0][13].ToString();
                  
                }

			}
			catch(Exception e)
			{
				VatLid.DAL.ExceptionProcess(e);
			}
			
		}
		protected void cmdSave_Click(object sender, System.EventArgs e)
		{
			

			if(TYPE == "edit")
			{
				SQL = "UPDATE Users SET UserName=N'" + Utils.safeString(txtUserName.Text.Trim())  + "'";
				if(chkIsUpdatePw.Checked!=true)
					SQL += ",UserPw=N'" + VatLid.SaltedHash.EncodeMD5(txtUserPw.Text.Trim()) + "'";
				SQL += ",UserRealName=N'" + Utils.safeString(txtUserRealName.Text) + "'";
				//SQL += ",UserStatus=" + ddlUserStatus.SelectedItem.Value + "";
				SQL += ",UserDesc=N'" + Utils.safeString(txtUserDesc.Text) + "'";
				SQL += ",UserDept=N'" + Utils.safeString(txtUserDept.Text) + "'";
				SQL += ",ExpiredDate='" + "" + "'";
				SQL += ",UserGroupID=" + ddlUserGroup.SelectedItem.Value + "";
				SQL += ",StartAllowedTime=N'" + "" + "'";
				SQL += ",EndAllowedTime=N'" + "" + "'";
                SQL += ",AllowedIPs=N'" + "" + "'";
                SQL += ",PHONE=N'" + Utils.safeString(txtPhone.Text.Trim()) + "'";
                SQL += ",Email=N'" + Utils.safeString(txtEmail.Text.Trim()) + "'";
                SQL += ",CP=0";
                SQL += ",isNew=1";
                SQL += ",partnerid="+ddlDoiTacCP.SelectedValue;
				SQL += " WHERE ID=" + sID;
				VatLid.DAL.ExecuteQuery(SQL);
                VatLid.DAL.INSERT_USER_LOG_NEW(VatLid.Utils.getUserId(Session).ToString(), VatLid.LogType.UPDATE.ToString(), VatLid.DAL.getCategoryID(FileName), "OK", sID, VatLid.Utils.GetIP());
				string strData = "Cập nhật người dùng:" + txtUserName.Text;
		
			}
			else
			{
                SQL = "INSERT INTO Users(UserName,UserPw,UserRealName,UserStatus,UserDesc,UserDept,UserGroupID,Phone,Email,CP,isNew,partnerid) VALUES(N'" + Utils.safeString(txtUserName.Text);
                SQL += "',N'" + VatLid.SaltedHash.EncodeMD5(txtUserPw.Text.Trim());
                SQL += "',N'" + Utils.safeString(txtUserRealName.Text.Trim());
				SQL += "'," + 2 ;
				SQL += ",N'" + Utils.safeString(txtUserDesc.Text);
				SQL += "',N'" + Utils.safeString(txtUserDept.Text);
				SQL += "'," + ddlUserGroup.SelectedItem.Value;
                SQL += ",N'" + Utils.safeString(txtPhone.Text.Trim());
                SQL += "',N'" + Utils.safeString(txtEmail.Text.Trim());
                SQL += "'," + 0;
                SQL += "," + 1;
                SQL += "," + ddlDoiTacCP.SelectedValue;
				SQL += ")";

                //lblError.Text = SQL;

                VatLid.DAL.ExecuteQuery(SQL);
                VatLid.DAL.INSERT_USER_LOG_NEW(VatLid.Utils.getUserId(Session).ToString(), VatLid.LogType.ADD.ToString(), VatLid.DAL.getCategoryID(FileName), "OK", "0", VatLid.Utils.GetIP());
	
				
			}
            Response.Redirect("User_List.aspx");
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
