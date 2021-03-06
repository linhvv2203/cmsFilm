	using System;
	using System.Data;
	using System.Configuration;
	//using System.Diagnostics ;
	//using System.Web;
	//using System.Web.Security;
	//using System.Web.UI;
	//using System.Web.UI.WebControls;
	//using System.Web.UI.WebControls.WebParts;
	//using System.Web.UI.HtmlControls;

	public class libConst
	{
		public static string wsAddress = ConfigurationSettings.AppSettings["wsAddress"];
		public static string wsAdmin = ConfigurationSettings.AppSettings["wsAdmin"];
		public static string wsPass = ConfigurationSettings.AppSettings["wsPass"];
		public static string wsPhoneViettel = ConfigurationSettings.AppSettings["wsPhoneViettel"];
		public static string wsPhone = ConfigurationSettings.AppSettings["wsPhoneExt"];
		public static string mdAddress = ConfigurationSettings.AppSettings["mdAddress"];
		public static string mdAdmin = ConfigurationSettings.AppSettings["mdAdmin"];
		public static string mdPass = ConfigurationSettings.AppSettings["mdPass"];
		public static string mdCPCode = ConfigurationSettings.AppSettings["mdCpCode"];
		public static string mdRBTCmd = ConfigurationSettings.AppSettings["mdRBTCmd"];

		/// <summary>
		/// After login, must set to true ;
		/// TimeOut: reset to false;
		/// </summary>
		public static Boolean isInSection = false;
		/// <summary>
		/// After login, must set userPhone = Phone login ;
		/// Timeout: must set to null;
		/// </summary>
		public static string userPhone="";
		public static string userPass="";
		public static string userMusicPhone="";
		public static string userMusicPass="";
		public static string userPersonID;
		/// <summary>
		/// User only Valid after call hwgetUserInfo and status=2;
		/// </summary>
		public static Boolean userValid = false;
		public static string phoneSyntax = "0123456789";
		public static string getErrorDescription(string errCode)
		{
			switch (errCode)
			{
				case "000000": return "Thành công";
				case "000001": return "Đã được chấp nhận nhưng đang trong quá trình xử lý.";
				case "000002": return "Không có thông tin về gói đang thêm mới hoặc sửa thành công. ";
				case "000003": return "Yêu cầu tải RBT được chấp nhận và RBTs sẽ được tải về sau khi thuê bao đăng kí dịch vụ RBT (Dịch vụ đang ký RBT không phải là thời gian thực, tức là hệ thống không chấp nhận đăng ký dịch vụ RBT ngay lập tức).";
				case "100001": return "Lỗi không xác đinh.";
				case "100002": return "Hệ thống đang bận xử lý.";
				case "100003": return "Thời gian chờ đợi xử lý hết.";
				case "100004": return "Lỗi đường truyền mạng.";
				case "100005": return "Lỗi xử lý database. ";
				case "100006": return "Lỗi xử lý database.";
				case "100007": return "Dịch vụ tạm dừng hoạt động.";
				case "100008": return "Lỗi thực thi cơ sở dữ liệu.";
				case "200001": return "Không vào giá trị các tham số bắt buộc.";
				case "200002": return "Sai định dạng tham số vào.";
				case "200003": return "Độ dài tham số vượt quá độ dài cho phép.";
				case "200004": return "Tất cả các tham số vào đều null.";
				case "200005": return "Sai định dạng thời gian của RBT.";
				case "200006": return "Vào giá trị của những tham số không yêu cầu.";
				case "201001": return "Sai định dạng số điện thoại.";
				case "201002": return "Sai mật khẩu.";
				case "201003": return "Không vào mật khẩu.";
				case "202001": return "Sai định dạng của hộp âm nhạc. ";
				case "202002": return "Sai định dạng của mã RBT. ";
				case "300001": return "Vượt quá số lượng thành viên cho phép.";
				case "300002": return "Không lấy được thông tin. ";
				case "301001": return "Thuê bao không tồn tại.";
				case "301002": return "Thuê bao đã đăng ký dịch vụ trước đó.";
				case "301003": return "Sai mật khẩu.";
				case "301004": return "Không gửi được SMS đến thuê bao.";
				case "301005": return "Vào sai mã kiểm tra.";
				case "301006": return "Thuê bao đang chờ đăng ký hoặc chờ được kích hoạt. ";
				case "301008": return "Không lấy được mã kiểm tra do thuê bao có lỗi.";
				case "301009": return "Không được phép định nghĩa thuê bao do thuê bao đang có lỗi.";
				case "301010": return "Không xóa được thuê bao do thuê bao đang có lỗi.";
				case "301011": return "Thuê bao nợ tiền.";
				case "301012": return "Mã kiểm tra không tồn tại.";
				case "301013": return "Không thể tải về trang thái dịch vụ thuê bao.";
				case "301014": return "Không thể thiết lập trạng thái dịch vụ thuê bao";
				case "301015": return "Dịch vụ không đáp ứng cho thuê bao do lỗi trạng thái dịch vụ của thuê bao.";
				case "301016": return "Thuê bao không cho phép các thuê bao khác sao chép RBTs.";
				case "301017": return "Không thể sao chép cấu hình hệ thống.";
				case "301018": return "Số điện thoại của thuê bao bị sao chép không tồn tại.";
				case "301019": return "Danh sách các RBT có chứa RBT không hợp lệ (trạng thái không xác định).";
				case "301020": return "RBT không thuộc một công ty nào.";
				case "301021": return "Đối tượng bị hạn chế lần tiêu thụ.";
				case "301022": return "Mã giới hạn tiêu dùng không tồn tại.";
				case "301023": return "Vượt quá giới hạn sử dụng.";
				case "301024": return "Loại RBT không tồn tại.";
				case "301025": return "Hệ thống tính tiền mới không thay đổi so với hệ thống cũ nên không cần thay đổi gì. ";
				case "309123": return "Số lần thuê bao đăng ký dịch vụ trong ngày đã vượt quá giới hạn.";
				case "301026": return "Một nhánh dịch vụ mới không thay đổi gì so với dịch vụ cũ.";
				case "301027": return "Mã ngôn ngữ không được Hệ thống hỗ trợ.";
				case "301029": return "Số thuê bao không thuộc phạm vi vùng.";
				case "302001": return "RBT đã tồn tại.";
				case "302002": return "Lỗi truyền file RBT.";
				case "302003": return "Thời gian cài đặt cho RBT trùng lặp.";
				case "302004": return "RBT đã bị yêu cầu xóa.";
				case "302005": return "RBT của CP yêu cầu xử lý không thuộc về CP đó.";
				case "302006": return "RBT đã được yêu cầu sửa chữa.";
				case "302007": return "Số lượng kết quả truy vấn bằng 0.";
				case "302008": return "RBT đang trong trạng thái được thay đổi.";
				case "302009": return "RBT trong thư để tải lên có lỗi.";
				case "302010": return "Lỗi tính cước thuê bao.";
				case "302011": return "RBT được tải lại.";
				case "302012": return "RBT đã được tải lên hoặc không tìm thấy chứng nhận.";
				case "302013": return "Định dạng file RBT lỗi. Hai khả năng lỗi: File không đúng định dạng file wav hoặc asf  File không đúng chuẩn mã hóa qui định 8K8bit (8,000 Hz 8 bit PCM , bitrate = 64 kbs, 1min=468.8 k , 8k8bitpcm.wav)";
				case "302014": return "Sai định dạng file uploaded. File phải là định dạng wav hoặc asf.";
				case "302015": return "RBT đã ở trạng thái ẩn (chưa kích hoạt).";
				case "302016": return "RBT đã ở trạng thái được phép hiển thị.";
				case "302017": return "Hộp âm nhạc không tồn tại.";
				case "302018": return "RBT chưa được tải về.";
				case "302019": return "RBT hết hạn sử dụng.";
				case "302020": return "Số lượng RBT trong hộp âm nhạc vượt quá giới hạn.";
				case "302021": return "Kiểu thư mục không tồn tại.";
				case "302022": return "Mã xác định trong của thư mục không tồn tại.";
				case "302023": return "Không tạo được thư mục.";
				case "302024": return "CP không sở hữu RBT. ";
				case "302025": return "CP không thể upload RBTs.";
				case "302026": return "Không ẩn được trạng thái của RBT.";
				case "302027": return "Không hiện được trạng thái của RBT.";
				case "302028": return "RBT đang đợi được chấp nhận.";
				case "302029": return "Lỗi sai thời hạn sử dụng RBT. Có hai trường hợp: Thời gian sử dụng lại đặt trước thời gian hiện thời hoặc Thời hạn sử dụng sai (bắt đầu và kết thúc)";
				case "302030": return "Thời gian chơi RBT bị đè lên nhau.";
				case "302031": return "Mã kiểu thư mục không tồn tại hoặc sai định dạng.";
				case "302032": return "Cấu trúc thư mục sai và không chứa được RBT.";
				case "302033": return "Thư mục mẹ không được chứa RBT (hệ thống chỉ cho phép thư mục con chứa RBT)";
				case "302034": return "Không được xóa thư mục chứa RBT.";
				case "302035": return "Không xóa được thư mục chứa thư mục con.";
				case "302037": return "Không sao chép được tất cả RBTs trong thư mục.";
				case "302038": return "Không sao chép được một vài RBTs trong thư mục.";
				case "302039": return "Các trường đợi được chấp nhận không thể sửa chữa. Các trường đang ở trong trạng thái bị xóa.";
				case "302040": return "Ngôn ngữ không tồn tại.";
				case "302041": return "Công ty không tồn tại.";
				case "302042": return "Mã vùng không tồn tại.";
				case "302043": return "Sai định dạng file.";
				case "302044": return "Không ẩn được bài hát trong hộp âm nhạc.";
				case "302045": return "Không thể thay đổi thuộc tính tài nguyên của thư mục.";
				case "302046": return "Thư mục mẹ không tồn tại.";
				case "302047": return "Thư mục RBT không tồn tại.";
				case "302048": return "Hộp âm nhạc đã tồn tại.";
				case "302049": return "Số lượng RBTs uploaded vượt quá số lượng cho phép của CP.";
				case "302050": return "Thuê bao không thể upload DIY RBTs.";
				case "302051": return "Số RBT của công ty vượt quá giới hạn cho phép.";
				case "302052": return "Hộp âm nhạc quá hạn. ";
				case "302053": return "Hộp âm nhạc đang trọng trạng thái chờ được chấp nhận.";
				case "302054": return "Không ẩn được trạng thái hiện thời của hộp âm nhạc.";
				case "302055": return "Không hiển thị được trạng thái hiện thời của hộp âm nhạc.";
				case "302056": return "Thư mục chứ RBT không tồn tại.";
				case "302057": return "Thư mục chứa RBT đang đợi được chấp nhận.";
				case "302058": return "Không ẩn được trạng thái hiện thời của thư mục chứa RBT.";
				case "302059": return "Không hiển thị được trạng thái hiện thời của thư mục RBT.";
				case "302060": return "Không thực hiện thay đổi một chuỗi RBT.";
				case "302061": return "Không thực hiện được thay đổi chuỗi";
				case "302062": return "Không tải được DIY RBTs sau khi RBT đã được thông qua.";
				case "302063": return "Không thể tính tiền bán RBT cho công ty sau khi RBT đã được chấp nhận.";
				case "302064": return "Số lượng hộp âm nhạc một CP upload đã vượt quá giới hạn.";
				case "302065": return "Ngày hết hạn sử dụng của hôm âm nhạc sớm hơn ngày hiện thời.";
				case "302066": return "Hộp âm nhạc đang đợi được chấp nhận đã bị xóa nên không thể thay đổi được.";
				case "302067": return "Hộp âm nhạc không thể thay đổi.";
				case "302068": return "RBT không thuộc một danh sách RBTs nào cả.";
				case "302069": return "Tất cả RBTs đã bao gồm trong danh sách RBT.";
				case "302070": return "Không lấy được thư mục hoạt động của file announcement trong hộp âm nhạc.";
				case "302071": return "Đã tồn tại cả hai trường ghi người gọi và nhận.";
				case "302072": return "Không tồn tại cả hai trường ghi người gọi và nhận.";
				case "302073": return "Thuê bao hoặc tài nguyên RBT không tồn tại.";
				case "302074": return "RBT không thể được sửa chữa.";
				case "302075": return "Số nhạc nền không tồn tại trong mục ghi cấu hình.";
				case "302076": return "Âm nhạc nền không tồn tại.";
				case "302078": return "Không soạn được RBT.";
				case "302079": return "Số người gọi đã đặt trước đó.";
				case "302080": return "Thời gian hiệu lực bị sai.";
				case "302081": return "File nghe thử và file đặt RBT khác nhau.";
				case "302082": return "Vượt quá hạn sử dụng của hộp âm nhạc.";
				case "302083": return "Kiểu thuê bao mới giống hệt kiểu thuê bao cũ.";
				case "302084": return "Không upload được kênh âm nhạc.";
				case "302085": return "Hạn sử dụng của hộp âm nhạc sai vượt quá hạn sử dụng của RBTs.";
				case "302086": return "Chưa cấu hình thiết bị hỗ trợ và kiểu.";
				case "302087": return "Thư mục chứa RBT nghe thử chưa được cấu hình.";
				case "302088": return "Thuê bao chưa tải RBT.";
				case "302089": return "IVR hoặc Web chưa cấu hình. ";
				case "302090": return "Không gia hạn được sử dụng của RBT.";
				case "302091": return "RBT và hộp âm nhạc  hết hạn sử dụng và không gia hạn được. ";
				case "302093": return "Hạn sử dụng của một gói sớm hơn thời gian hiện thời.";
				case "302094": return "CP không tồn tại.";
				case "302095": return "Một gói của CP đã tồn tại trước đó.";
				case "302096": return "Gói không tồn tại.";
				case "302097": return "Không cấu hình được thiết bị hỗ trợ.";
				case "302098": return "Gói không ở trạng thái bình thường hoặc ẩn.";
				case "302099": return "Operator không tồn tại.";
				case "302100": return "RBT đã tồn tại trước đó.";
				case "302101": return "Announcement file không tồn tại.";
				case "302102": return "Không sao chép được file announcement.";
				case "302103": return "Mã trường ghi RBTs ngầm định không tồn tại.";
				case "302104": return "Trạng thái hiện thời của gói RBT không ẩn được.";
				case "302105": return "Trạng thái hiện thời của gói RBT không hiển thị được.";
				case "302106": return "Nếu gói không phân biệt được các SPs khác, cờ CP sẽ không thể ghi ";
				case "302107": return "Khi gói phân biệt được các SPs khác, mã của CP cần phải cung cấp";
				case "302108": return "Người dùng đã tải tất cả các RBTs trong gói";
				case "302109": return "Thêm hoặc sửa chữa thành công, hoặc kích hoạt không thành công gói đang ở trạng thái uploaded";
				case "302110": return "Ngày bắt đầu sớm hơn ngày kết thúc thời hạn sử dụng.";
				case "302111": return "CP không tồn tại hoặc đang ở trạng thái không bình thường";
				case "302112": return "Sai lệch hạn sử dụng. ";
				case "302113": return "Gói không ở trạng thái uploaded";
				case "302114": return "Bản ghi ở trong bảng sắp phê duyệt đã bị chỉnh sửa";
				case "302115": return "Thông tin phụ của tài nguyên không tồn tại";
				case "302116": return "Thông tin phụ của tài nguyên có tồn tại";
				case "302117": return "Thông tin phụ của tài nguyên không ở trạng thái sắp phê duyệt";
				case "302118": return "Thông tin phụ của tài nguyên đang ở trạng thái sắp phê duyệt";
				case "302119": return "Trạng thái RBT bị lỗi.";
				case "302120": return "Kiểu tài nguyên không cùng kiểu với kiểu thư mục.";
				case "302121": return "Không được phép tạo thư mục con trong thư mục đang đợi được chấp thuận.";
				case "302122": return "Thư mục con không thể được chấp nhận khi thư mục mẹ được chờ được chấp nhận.";
				case "302123": return "RBT tồn tại trong thư viện cá nhân.";
				case "302124": return "Kiểu RBT khác nhau.";
				case "302125": return "Kiểu RBT đặt không trùng với kiểu RBT trong danh sách. Ví dụ kiểu RBT được đặt là PLUS RBT nhưng trong danh sách RBT lại là RBT thường.";
				case "302126": return "RBT thông thường không được cung cấp bởi CP.";
				case "302200": return "Sai tài khoản của người sở hữu thư mục.";
				case "303001": return "Thuê bao đã định nghĩa nhóm gọi đến (gồm cả tên và số thuê bao).";
				case "303002": return "Khi sửa chữa hoặc xóa một nhóm người gọi, trường ghi nhận báo nhóm đó không tồn tại.";
				case "303003": return "Nhóm người gọi không thể bị thêm bởi số người gọi trong nhóm vượt quá giới hạn cho phép.";
				case "303004": return "Nhóm người gọi bao gồm những thành viên không thể xóa được.";
				case "303011": return "Thuê bao đã tồn tại trong nhóm.";
				case "303012": return "Thuê bao không tồn tại trong nhóm.";
				case "303013": return "Số thuê bao trong nhóm vượt quá giới hạn.";
				case "303014": return "Không thể sửa được số của thuê bao bởi thuê bao này nằm trong nhóm khác.";
				case "303015": return "Số lượng thuê bao gọi vượt quá giới hạn cho phép.";
				case "303021": return "RBT đã tồn tại trong hộp âm nhạc.";
				case "303023": return "Số lượng RBT được tải về vượt quá giới hạn cho phép.";
				case "303024": return "SỐ lượng RBT vượt quá giới hạn cho phép. ";
				case "303025": return "Gói tải về không tồn tại. ";
				case "303026": return "Thuê bao vừa tải về một gói không xác định SPs";
				case "303027": return "The subscriber has downloaded a package distinguishing SPs, so he is not allowed to download a package without distinguishing SPs.";
				case "303028": return "Gói chuẩn bị tải về đã được tải trước đó.";
				case "303029": return "Thuê bao không tải gói về. ";
				case "303030": return "Thuê bao không được phép tải nhiều gói của một CP. ";
				case "303031": return "Hộp âm nhạc đã tồn tại.";
				case "303032": return "Hộp âm nhạc không tồn tại.";
				case "303033": return "Chủ sở hữu của hộp âm nhạc không tồn tại.";
				case "303041": return "RBT đã tồn tại trong hộp âm nhạc. ";
				case "303042": return "RBT không tồn tại trong hộp âm nhạc. ";
				case "303043": return "Nội dung của nhóm RBT không sửa chữa được. ";
				case "303051": return "Cài đặt RBT đã tồn tại. ";
				case "303052": return "Cài đặt cho RBT không tồn tại. ";
				case "303053": return "Nhóm hoặc bộ phận không tồn tại. ";
				case "303054": return "Tình trạng dịch vụ của nhóm hoặc bộ phận không bình thường";
				case "303055": return "Bạn không được phép sửa nhóm RBT khi thiết lập điều kiện tính cước";
				case "303056": return "Nguồn RBT không nằm trong thư viện nhạc cá nhân của người sử dụng, RBTs này sẽ được copied. ";
				case "303057": return "Thông tin cấu hình của hệ thống liên quan không tồn tại.";
				case "303058": return "Thuê bao không thể tải RBT của công ty vì không thuộc công ty đó.";
				case "304001": return "Không tìm thấy giá trị thống kê. ";
				case "306001": return "RBT ngầm định không tồn tại. ";
				case "306002": return "Hệ thống báo lỗi khi hiển thị mã RBT. ";
				case "306003": return "Hệ thống báo lỗi khi mã nhận dạng trong của RBT chuyển thành mã RBT. ";
				case "306004": return "Tham số vào sai. ";
				case "306005": return "Dải số trước là một dải số lá";
				case "306006": return "Dải số bị đè lên nhau. ";
				case "306007": return "Dải số không tồn tại. ";
				case "306008": return "Dải số chưa một dải số con khác. ";
				case "306009": return "Areas of one level up do not exist.";
				case "306010": return "Areas of one level up are leaf areas.";
				case "306011": return "Mã vùng đã tồn tại. ";
				case "306012": return "Mã vùng không tồn tại. ";
				case "306013": return "Areas containing sub-areas cannot be set to leaf areas.";
				case "306014": return "Areas containing sub-areas cannot be deleted.";
				case "307001": return "Department đã tồn tại. ";
				case "307002": return "Deparment không tồn tại. ";
				case "307003": return "Các thành viên nhóm không yêu cầu đúng dịch vụ. ";
				case "307004": return "Số lượng nhóm của công ty vượt quá giới hạn. ";
				case "307005": return "Thành viên không phải thuộc công ty. ";
				case "307006": return "RBT của công ty không thể thay đổi do trạng thái hiện thời của thành viên công ty. ";
				case "307007": return "Không tạo được một vài thành viên. ";
				case "307008": return "Không xóa được các thành viên. ";
				case "307009": return "Nhóm của công ty không tồn tại. ";
				case "307010": return "Đây là thành viên của một nhóm trong quản lý công ty. ";
				case "307011": return "The superior department of this one does not exist. ";
				case "307012": return "Thành viên thuộc department khác. ";
				case "307013": return "Phân đoạn thời gian đã tồn tại. ";
				case "307014": return "Lễ hội đã tồn tại";
				case "307015": return "Trạng thái công ty có lỗi. ";
				case "307016": return "Nhóm đã cài đặt rồi. ";
				case "307017": return "Dải số không tồn tại";
				case "307018": return "Ngày lễ hội đặc biết không tồn tại. ";
				case "307019": return "Không tạo mới được các thành viên của department. ";
				case "307020": return "Đã thêm được các thành viên của department. ";
				case "307021": return "Không xóa được các thành viên department. ";
				case "307022": return "Không xóa được các thành viên department. ";
				case "307023": return "Không kích hoạt hoặc tạm dừng dịch vụ RBT cho tất cả các thành viên trong công ty. ";
				case "307024": return "Đã kích hoạt hoặc tạm dừng dịch vụ RBT cho tất cả các thành viên trong công ty.. ";
				case "307025": return "Thông tin RBT không thể bị sửa chữa do trạng thái dịch vụ của công ty. ";
				case "307181": return "Công ty không tồn tại.";
				case "308001": return "Người nhận quà tặng RBT không đăng ký dịch vụ RBT";
				case "308002": return "Số lượng RBT của người nhận quà tặng RBT đã vượt quá giới hạn cho phép. ";
				case "308003": return "Người nhận quà tặng không phải thuộc vùng thuê bao cho phép. ";
				case "308004": return "Người nhận quà tặng đã có quà tặng này trong nhóm rồi. ";
				case "308005": return "Số tháng tặng quà vượt quá giá trị cho phép. ";
				case "308006": return "Người nhận quà tặng dịch vụ RBT cũng đã đăng kí dịch vụ RBT rồi";
				case "308007": return "Người được tặng không được là người tặng RBT";
				case "308008": return "Dịch vụ hết hạn sử dụng. ";
				case "308009": return "Một phần của gói đang download bị lỗi !";
				case "308010": return "Thuê bao đã có RBTs ngầm định, cài đặt không thực hiện được. ";
				case "308011": return "Một RBT không thể đặt được vào hộp âm nhạc. ";
				case "308012": return "RBT tồn tại trong danh sách RBT rồi. ";
				case "308013": return "Tất cả các lệnh tải chuỗi có lỗi. ";
				case "308014": return "All the batch presenting fails.";
				case "308015": return "All the batch setting fails.";
				case "308016": return "Part of batch presenting fails.";
				case "308017": return "Part of batch setting fails.";
				case "308019": return "Phím nghe thử bị lỗi. ";
				case "308020": return "Hệ thống không tạo được khóa nghe thử. ";
				case "308021": return "Khóa nghe thử không tồn tại. ";
				case "308023": return "Bài hát (RBT) đã tồn tại hoặc đang được gửi tặng.";
				case "309001": return "Vượt quá khả năng cung cấp của hệ thống. ";
				case "309002": return "Yêu cầu mã kiểm tra";
				case "309003": return "Chính sách đăng ký không tìm thấy. ";
				case "309004": return "Không xóa và tạo được thuê bao tại website. ";
				case "309005": return "Chưa đặt kiểu truy cập";
				case "309006": return "Số lượng thue bao định nghĩa trong ngày vượt quá giới hạn. ";
				case "309007": return "Không xóa hoặc tạo được thuê bao. ";
				case "309008": return "Thuê bao đang được tạo hoặc xóa. ";
				case "309009": return "Thành viên công ty chuẩn bị tạo không phải là một thuê bao dịch vụ RBT thông thường. ";
				case "309010": return "Số lượng các thuê bao của công ty đăng kí dịch vụ RBT vượt quá giới hạn cho phép. ";
				case "309011": return "Thành viên công ty không tồn tại ";
				case "309012": return "Thành viên công ty vừa đăng kí dịch vụ RBT. ";
				case "309113": return "Người dùng không dùng được dịch vụ RBT . ";
				case "309114": return "Thuê bao không thể xóa được. ";
				case "309115": return "Chưa đặt kiểu thuê bao. ";
				case "309116": return "Sai kiểu thuê bao hoặc kiểu truy cập. ";
				case "309117": return "Số lượng thuê bao hằng ngày dùng dịch vụ vượt quá giới hạn cho phép. ";
				case "309118": return "Thuê bao không được phép sử dụng dịch vụ dịch vụ RBT. ";
				case "309119": return "Vùng của thuê bao không được phép dùng dịch vụ. ";
				case "309120": return "Thời gian để tạo hoặc xóa một thuê bao năm trong khoảng thời gian không cho phép. ";
				case "309121": return "Thuê bao thuộc nhóm khác.";
				case "309122": return "Tạo được dịch vụ cho thuê bao nhưng không gửi được tin nhắn SMS. Nhắc thuê bao để lấy mật khẩu qua Webiste. ";
				case "309124": return "Không lấy được giấy chứng nhận cho công ty";
				case "309125": return "Số lượng các thành viên của công ty đăng ký đã vượt quá giới hạn cho phép của công ty. ";
				case "310001": return "Thuê bao không đủ tiền trong tài khoản. ";
				case "311001": return "Địa chỉ thiết bị thu tiền có lỗi. ";
				case "311002": return "Cổng vào ra của thiết bị tính tiền có lỗi. ";
				case "312002": return "Không thể xóa CP vẫn có RBTs. ";
				case "312003": return "Mã vùng không tồn tại. ";
				case "312004": return "Tài khoản của thuê bao không được phép thực hiện yêu cầu.";
				case "312005": return "Mã vai trò không tồn tại. ";
				case "312006": return "Số phân đoạn không tồn tại. ";
				case "312007": return "Chỉ mục của CP đã tồn tại trước đó. ";
				case "312008": return "Administrator không tồn tại. ";
				case "312009": return "Mã đăng nhập của tài khoản hệ thống đã tồn tại. ";
				case "312010": return "Mã đăng nhập của tài khoản hệ thống không tồn tại. ";
				case "312011": return "Mật khẩu cũ vào không đúng. ";
				case "312012": return "Tài khoản của công ty hoặc khoe không tồn tại. ";
				case "312013": return "Số trong của công ty hoặc phòng không tồn tại. ";
				case "312014": return "Số trong của The internal number of the corp operator or the department administrator does not exist. ";
				case "312015": return "Mã truy cập của CP đã tồn tại.";
				case "312016": return "The administrator’s modified number segment cannot contain the number segments of all his inferior administrators. ";
				case "312017": return "Quản trị hệ thống không có quyền điều khiển thuê bao này. ";
				case "313001": return "Mã trả về không tồn tại. ";
				case "313002": return "Dừng lại một vài tác vụ có lỗi. ";
				case "313003": return "Nhận lại một vài tác vụ có lỗi. ";
				case "313004": return "Sửa chưa các tác vụ tại thời điểm hoạt động bị lỗi. ";
				case "314001": return "Tên tham số không tồn tại. ";
				case "314002": return "Tên mới của biến không tuân theo yêu cầu đăng nhập. ";
				case "314003": return "Tham số cập nhật không tồn tại. ";
				case "314004": return "Kiểu tham số hàm gọi không tồn tại. ";
				case "314005": return "Lỗi xử lý database";
				case "314006": return "Mục cấu hình đã tồn tại";
				case "314007": return "Tham số vào không đúng hoặc không đúng với qui tắc cấu hình. ";
				case "314008": return "Hệ thống lỗi để cập nhật cấu hình";
				case "315001": return "Danh sách xếp hạng không tồn tại";
				case "315002": return "Vào vị trí của bảng xếp hạng sai. ";
				case "315003": return "Nội dung của một bảng xếp hạng bị sai. ";
				case "315004": return "Bảng xếp hạng bị trùng đè. ";
				case "315005": return "Bảng xếp hàng không thể bị xóa";
				case "315006": return "Một vài RBTs có chung một vị trí trong bảng xếp hạng. ";
				case "315007": return "Một bảng xếp hạng có hiệu lực sớm hơn thời gian hiện tại. ";
				case "316001": return "Một phiên yêu cầu tồn tại. ";
				case "316002": return "Không có RBT tương ứng trong bảng xếp hạng. ";
				case "316003": return "Mã RBT không tồn tại. ";
				case "316004": return "RBT được kích hoạt không tồn tại hoặc ở trạng thái lỗi. ";
				case "317001": return "Thuê bao không yêu cầu dịch vụ thêm";
				case "317002": return "Thuê bao bị lỗi không thể khởi tạo dịch vụ thêm. ";
				case "317003": return "Thuê bao chưa kích hoạt dịch vụ thêm. ";
				case "317004": return "Thuê bao đang ở trạng thái lỗi và không thể huy dịch vụ thêm";
				case "317005": return "CP không có quyền tải RBT có khả năng bỏ đi. ";
				case "317006": return "RBT không phải là RBT plus. ";
				case "317007": return "The personal library of the subscriber is full, so, you cannot upload the PLUS RBT to it.";
				case "317008": return "Thuê bao không được phép tải RBT – plus lên hệ thống. ";
				case "317009": return "The PLUS subscriber does not exist or his state is abnormal.";
				case "317010": return "Sau khi RBT plus được chấp thuận, RBT tải về lỗi. ";
				case "318001": return "The approval flow relevant to the operation does not exist.";
				case "318002": return "The flow procedure relevant to the operation is not in the procedure range.";
				case "318003": return "The person specified to approve the flow dose not exist.";
				case "318004": return "Bạn không thể xóa hoặc sửa các trường đang đợi chấp thuận. ";
				case "318005": return "The approval invoker relevant to the operation does not exist.";
				case "318006": return "The ID of the relation between the operation approval flow and the invoker does not exist.";
				case "318007": return "The approval flow exists (The information and procedure of the approval flow are the same as those of the previous one).";
				case "318008": return "The approval procedure is approval again (The same person approves the same flow procedure).";
				case "318009": return "The same operator corresponds to multiple flows. More exactly, the information of the flow is the same as that of other flows, but the flow procedure is different.";
				case "318010": return "The strings of the approval procedure contain the information of the procedure that is not needed to approve.";
				case "319003": return "Luật không thể áp dùng cho dịch vụ thông tin. ";
				case "319004": return "Luật này không thể áp dục cho hàm dịch vụ này. ";
				case "319005": return "Dịch vụ được yêu cầu nên không xóa được. ";
				case "319006": return "Dịch vụ chưa hoàn tất nên không thể tải về được. ";
				case "319007": return "Không xóa được do chính sách thu tiền năm trong luật dịch vụ ";
				case "319008": return "Luật phí dịch vụ đã tồn tại.";
				case "319009": return "Dịch vụ đã tồn tại";
				case "319010": return "Luật cho dịch vụ đã tồn tại. ";
				case "319011": return "Dịch vụ không tồn tại hoặc đang bị ẩn. ";
				case "319012": return "Luật cho dịch vụ không tồn tại. ";
				case "319013": return "Chính sách thu tiền không tồn tại. ";
				case "319014": return "Mô tả của chính sách thu tiền không tồn tại. ";
				case "319015": return "Thuê bao không dùng dịch vụ không tồn tại. ";
				case "319016": return "Thuê bao dùng dịch vụ không tồn tại. ";
				case "319017": return "Thuê bao vừa yêu cầu dùng dịch vụ. ";
				case "319018": return "Chức năng này đưuợc yêu cầu lại. ";
				case "319019": return "Cờ thời gian khác nhau. ";
				case "319020": return "Chính sách thu tiền đã tồn tại. ";
				case "319021": return "Số lượng thuê bao yêu cầu dịch vụ vượt quá giới hạn. ";
				case "319022": return "Thuê bao không thể gọi dịch vụ do sô shuê bao không nằm trong dải số cho phép. ";
				case "320001": return "Thuê bao yêu cầu dịch vụ gọi đến.";
				case "320002": return "Thuê bao không yêu cầu dịch vụ gọi đến. ";
				case "321001": return "Các cài đặt liên quan đến số thuê bao đã tồn tại. ";
				case "321002": return "Cài đặt RBT của thuê bao không tồn tại. ";
				case "321003": return "The number of set ordinary RBTs exceeds its default value.";

				default: break;
			}
			return "Lỗi không thể xác định.";
		}
		public static string getCategoryValue(string categoryName)
		{
			try
			{
				return ConfigurationSettings.AppSettings[categoryName];
			}
			catch
			{
				return "";
			}
		}

		public libConst()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
	public class Song
	{
		public static string SELECT_BY_SONG = "0";
		public static string SELECT_BY_SINGER = "1";
		public static string SELECT_BY_CODE = "2";
		public static string SELECT_BY_NAME_LETTER = "4";
		public static string SELECT_BY_PRICE = "5";
		public static string SELECT_BY_LANGUAGE = "6";
		public static string SELECT_BY_CATEGORY = "-1";
    
		public static string LANGUGE_VIETNAM = "1";
		public static string LANGUGE_ENLISH = "2";
		public static string LANGUGE_DEFAULT = "";

		public static string TAT_CA = "";
		public static string NHAC_TRE = "24";
		public static string NHAC_TRU_TINH = "25";
		public static string NHAC_CACH_MANG = "26";
		public static string NHAC_THIEU_NHI = "27";
		public static string DAN_CA = "28";
		public static string ROCK_VIET = "36";
		public static string POP = "29";
		public static string ROCK_N_DANCE = "30";
		public static string NHAC_PHIM = "32";
		public static string AM_THANH_CUOC_SONG = "33";
		public static string NHAC_THEO_TEN = "37";
		public static string NHAC_KHONG_LOI = "34";
		public static string NHAC_AUDITION = "44";
		public static string NHAC_QUANG_CAO = "46";


		public static string ORDER_TYPE_ASC = "1";
		public static string ORDER_TYPE_DESC = "2";

		public static string ORDER_BY_RBT_CODE = "1"; 
		public static string ORDER_BY_RBT_NAME = "2";
		public static string ORDER_BY_SINGER_NAME = "3";
		public static string ORDER_BY_RBT_PRICES = "6";
		public static string ORDER_BY_TOTAL_DOWNLOAD = "9";
		public static string ORDER_BY_RBT_NUMBERS = "10";

		public static string QUERY_TOTAL_RECORD = "1";
		public static string QUERY_NUM_RECORD = "2";

		public static string SETTONE_DEFAULT = "2";
		public static string SETTONE_GROUP = "3";
		public static string SETTONE_PERSONAL = "4";

		public static string SETTONE_LOOP_SEQ = "1";
		public static string SETTONE_LOOP_RAN = "2";
		public static string SETTONE_TIME_DAY = "1";
		public static string SETTONE_TIME_INDAY = "2";
		public static string SETTONE_TIME_WEEK = "3";
		public static string SETTONE_TIME_MONTH = "4";
		public static string SETTONE_TIME_YEAR = "5";
		public static string SETTONE_TIME_SPECIAL = "6";

		public static string USER_INFO_DOWNLOAD = "1";
		public static string USER_INFO_PRESENT = "2";
		public static string USER_INFO_COPY = "3";
		public static string USER_INFO_SETTING = "5";

	}
	public class hwRespone
	{
		/// <summary>
		/// resultCode = true: Action OK ;
		/// </summary>
		public Boolean resultCode;
		/// <summary>
		/// resultDes: Return Error Descrtion ;
		/// </summary>
		public string resultDes;
	}
	public class hwInfoDes
	{
		/// <summary>
		/// Any type: group code, member phone,...
		/// </summary>
		public string infoCode;
		/// <summary>
		/// Any type: group name, member name 
		/// </summary>
		public string infoName;
		/// <summary>
		/// Any description
		/// </summary>
		public string infoDes;
	}
	public class hwResponeTable
	{
		public hwRespone result;
		/// <summary>
		/// Total record found
		/// </summary>
		public string total = "0";
		/// <summary>
		/// DataTable depend on query statment
		/// </summary>
		public DataTable table = null;
	}
	//public class hwtonToneInfo
	//{
	//    /// <summary>
	//    /// result: Check status action
	//    /// </summary>
	//    public hwRespone result = new hwRespone();
	//    /// <summary>
	//    /// Total record found ;
	//    /// </summary>
	//    public string recordSum ;
	//    /// <summary>
	//    /// toneinfos: Array on ToneInfo;
	//    /// </summary>
	//    public ToneInfo[] toninfos = null;
	//}
	//public class hwToneInfo
	//{
	//    /// <summary>
	//    /// result: Check status action
	//    /// </summary>
	//    public hwRespone result = new hwRespone();
	//    /// <summary>
	//    /// Total record found ;
	//    /// </summary>
	//    public string totalRBT;
	//    /// <summary>
	//    /// toneinfos: Array on ToneInfo;
	//    /// </summary>
	//    //public hwRBTDetail[] tinfo = null ;
	//    public DataTable table = null;
	//}
	//public class hwToneDataTable
	//{
	//    /// <summary>
	//    /// result: Check status action
	//    /// </summary>
	//    public hwRespone result = new hwRespone();
	//    /// <summary>
	//    /// Total record found ;
	//    /// </summary>
	//    public string totalRBT;
	//    /// <summary>
	//    /// toneinfos: Array on ToneInfo;
	//    /// </summary>
	//    public DataTable table = null;
	//}
	public class hwgetTone
	{
		/// <summary>
		/// Default 0
		/// </summary>
		public string startRecord = "0";
		/// <summary>
		/// Total record return max : 50;
		/// </summary>
		public string endRecord = "10";
		/// <summary>
		/// queryType=1: Return total num RBTS, 2: RBTs Info by endRecord-startRecord
		/// </summary>
		public string queryType = "1";  // 1: return All, 2: record set
		/// <summary>
		/// toneCode used for get directly RBT info by tone code
		/// </summary>
		public string toneCode = "";    // RBT code
		/// <summary>
		/// toneName used for get directly RBT info by tone name
		/// </summary>
		public string toneName = "";     // RBT Name
		/// <summary>
		/// singerName used for get RBTs info by singer
		/// </summary>
		public string singerName = "";
		public string category = "";
		public string toneNameLetter = "";
		public string minPrice = "";
		public string maxPrice = "";
		public string orderBy = "";
		public string orderType = "";
		public string language = "";
	}
	public class hwRBTDetail
	{    
		public string MaSo;
		public string TenBaiHat;
		public string Casi;
		public string NgonNgu;
		public string Gia;
		public string NgayCapNhat;
		public string NgayHetHan;
		public string NhaCungCap;
		public string ChiDanThem;
		// Danh them cho download, setting
		public string cpCode;
		public string orderTimes;
		public string personID;
		public string setTimes;
		public string singerSex;
		public string tableType;
		public string tondCode;
		public string tonLongCode;
		public string toneID;    
		public string toneDownPath;
		public string toneListenPath;    
		//public string deviceAndUrl;
	}
	public class hwGroupInfo
	{
		public string phoneNumber;
		public string groupCode;
		public string groupName;
		public string groupDes;
	}
	//public class hwGroupInfos
	//{
	//    public hwRespone result ;
	//    /// <summary>
	//    /// Total Group
	//    /// </summary>
	//    public int total = 0;
	//    public hwGroupInfo[] gInfo =null;
	//}
	public class hwGroupMember
	{
		public string memberNumBer;
		public string memberName;
		public string memberDes;
	}
	//public class hwGroupMembers
	//{
	//    public hwRespone result;
	//    /// <summary>
	//    /// Total Member In Group
	//    /// </summary>
	//    public int total = 0;
	//    public hwGroupMember[] gInfo = null;
	//}
	public class hwCaller
	{
		public string callerNumBer;
		public string callerName;
		public string callerDes;
	}
	//public class hwCallers
	//{
	//    public hwRespone result;
	//    /// <summary>
	//    /// Total Callers
	//    /// </summary>
	//    public int total =0 ;
	//    public hwCaller[] gInfo = null;
	//}
	public class hwDownload
	{
		public string downloadDate;
		public string toneName;
		public string tonePrice;
	}
	//public class hwInfoDownload
	//{
	//    public hwRespone result;
	//    /// <summary>
	//    /// Total Download
	//    /// </summary>
	//    public int total = 0;
	//    public DataTable tb = null;
	//    //public hwDownload[] dInfo = null;
	//}
	public class hwPresent
	{
		public string presentDate;
		public string toneCode;
		public string toneName;
		public string tonePrice;
		public string phoneNume;
	}
	//public class hwInfoPresent
	//{
	//    public hwRespone result;
	//    /// <summary>
	//    /// Total Present
	//    /// </summary>
	//    public int total = 0;
	//    public DataTable tb = null;
	//    //public hwPresent[] pInfo = null;
	//}
	public class hwCopy
	{
		public string copyDate;
		public string toneCode;
		public string toneName;
		public string tonePrice;
		public string phoneNume;
	}
	//public class hwInfoCopy
	//{
	//    public hwRespone result;
	//    /// <summary>
	//    /// Total Copies
	//    /// </summary>
	//    public int total = 0;
	//    public DataTable tb = null;
	//    //public hwCopy[] pInfo = null;
	//}
	public class hwSetting
	{
		public string toneName;  
		/// <summary>
		/// Phone Number or Group Name
		/// </summary>
		public string phoneGroup;    
		public string startDate;
		public string endDate;
		public string setDate;
		/// <summary>
		/// Setting type
		/// </summary>
		public string setType;    
	}
	//public class hwInfoSetting
	//{
	//    public hwRespone result;
	//    /// <summary>
	//    /// Total Download
	//    /// </summary>
	//    public int total = 0;
	//    public DataTable tb = null;
	//    //public hwSetting[] pInfo = null;
	//}
	public class hwUserSet
	{
		public string number;
		/// <summary>
		/// Not invalid when setType=2,
		/// Display number of a calling group when setType=3,
		/// calling number when setType=4
		/// </summary>
		public string callerNumber;
		/// <summary>
		/// Name of an RBT group
		/// </summary>
		public string callerName;
		/// <summary>
		/// 2: default tone
		/// 3: group tone
		/// 4: personal tone
		/// </summary>
		public string setType;
		/// <summary>
		/// 1: Sequency, 2: Random
		/// </summary>
		public string loopType = "";
		/// <summary>
		/// 1: whole day
		/// 2: time segment of a day
		/// 3: time segment of a week
		/// 4: time segment of a month
		/// 5: time segment of a year
		/// 6: start and end time
		/// </summary>
		public string timeType = "";
		/// <summary>
		/// timeType=2 -> HH:mm:ss else yyyy-MM-dd HH:mm:ss
		/// </summary>
		public string startTime = "";
		/// <summary>
		/// timeType=2 -> HH:mm:ss else yyyy-MM-dd HH:mm:ss
		/// </summary>
		public string endTime = "";
		/// <summary>
		/// Not set if create new
		/// </summary>
		public string toneBoxID = "";
		public string toneType = "";
		public string status = "";
		public string settingID = "";
		public string description = "";
	}
	public class hwUserSets
	{
		public hwRespone result;
		/// <summary>
		/// Total Download
		/// </summary>
		public int total = 0;
		public hwUserSet[] pInfo = null;
	}
	public class hwUserInfo
	{
		/// <summary>
		///Max length: 40 charracter
		/// </summary>
		public string name;
		/// <summary>
		/// Max length: 40 charracter
		/// </summary>
		public string email;
		/// <summary>
		/// Max length: 20 charracter
		/// </summary>
		public string oldPwd="";
		/// <summary>
		/// For change password, oldPwd must not null if newPwd not null;
		/// Max length: 20 charracter
		/// </summary>
		public string newPwd="";
		public string status;
		/// <summary>
		/// Copy Music - Enabled: 1, Disabled: 0
		/// </summary>
		public string isCopy="";
		public string registerTime;
		public string isValidUser;
		/// <summary>
		/// Play RBT ground, Enbled/Disabled: 1/0
		/// </summary>
		public string bgTone="";
	}
	public class hwUserInfos
	{
		public hwRespone result;
		public hwUserInfo info = new hwUserInfo();
	}


