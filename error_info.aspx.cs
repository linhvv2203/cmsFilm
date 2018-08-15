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
namespace MarketVN.Sysadmin
{
	/// <summary>
	/// Summary description for error_info.
	/// </summary>
	public partial class error_info : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string ErrorInfo=Request.QueryString["err"];
			switch(ErrorInfo)
			{
				case "21": lblErrorInfo.Text="Bạn không có quyền sử dụng chức năng này"; break;
				case "22": lblErrorInfo.Text="Chức năng này chưa cài đặt"; break;
				case "23": lblErrorInfo.Text="Đổi mật khẩu thành công"; break;
				case "24": lblErrorInfo.Text="Không tìm thấy người dùng"; break;
				case "25": lblErrorInfo.Text="Đã cập nhật thành công"; break;
				case "26": lblErrorInfo.Text="Hai mật khẩu không giống nhau"; break;
				case "27": lblErrorInfo.Text="Đánh không đúng mật khẩu"; break;
				case "28": lblErrorInfo.Text="Thao tác của bạn bị lỗi ! Thực hiện lại"; break;
                case "29": lblErrorInfo.Text = "Dữ liệu đầu vào không hợp lệ"; break;
                case "30": lblErrorInfo.Text = "Xin lỗi bạn. Mật khẩu bạn nhập không an toàn <br/> Mật khẩu bạn nhập phải bao gồm ký tự và số <br/> Liên hệ gửi mail ducnm17@viettel.com.vn để được cấp lại"; break;
                case "31": lblErrorInfo.Text = "Xin lỗi bạn. Mật khẩu bạn nhập không an toàn <br/> Mật khẩu bạn nhập phải lớn hơn 8 ký tự, bao gồm ký tự, số và ký tự đặc biệt <br/> Liên hệ gửi mail ducnm17@viettel.com.vn để biết thêm thông tin"; break;
                
                case "32": lblErrorInfo.Text = "Xin lỗi bạn. Mật khẩu bạn nhập không an toàn <br/> Mật khẩu bạn nhập phải bao gồm ký tự và số <br/> Liên hệ gửi mail ducnm17@viettel.com.vn để được cấp lại"; break;
                case "33": lblErrorInfo.Text = "Dữ liệu nhập vào bị lỗi ! ValidateRequest"; break;
                case "34": lblErrorInfo.Text = "Dữ liệu nhập vào bị lỗi ! Exception"; break;
                case "35": lblErrorInfo.Text = "Dữ liệu nhập vào bị lỗi ! Exception"; break;
                case "36": lblErrorInfo.Text = "Số lần đăng nhập không thành công liên tiếp tối đa vượt quá 5 <br/> Hệ thống thực hiện khóa tài khoản trong vòng 15 phút <br/> "; break;
                case "37": lblErrorInfo.Text = "Mật khẩu cũ trùng với mật khẩu mơi <br/> Xin nhập lại mật khẩu khác <br/> "; break;
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
