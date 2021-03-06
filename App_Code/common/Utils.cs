using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.SessionState;
using System.Collections;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Runtime.InteropServices;
//

namespace VatLid
{
    /// <summary>
    /// Summary description for Utils
    /// </summary>
    public class Utils
    {
        public Utils()
        {
            //
            // TODO: Add constructor logic here
            //
        }
		

		public static string GetFileWithParam(HttpRequest request)
        {
            return request.RawUrl.Substring(request.RawUrl.LastIndexOf("/") + 1);
        }
       
        public static string ValidateXSS(string strWords)
        {
            StringBuilder sb = new StringBuilder(HttpUtility.HtmlEncode(strWords));

            string[] badChars = { "&lt;", "&gt;", "/", "script", "iframe" };
            string newChars = sb.ToString();
            foreach (string str in badChars)
            {
                //newChars = newChars.Replace(str, "");
                newChars = Regex.Replace(newChars, str, "", RegexOptions.IgnoreCase);
            }

            return newChars;
        }
        // xoa cac ki tu dac biet trong input dau vao
        public static string KillChars(string strWords)
        {
            string[] badChars = { "select", "drop", ";", "--", "insert", "delete", "xp_", "'" };
            string newChars = strWords;
            foreach (string str in badChars)
            {
                //newChars = newChars.Replace(str, "");
                newChars = Regex.Replace(newChars, str, "", RegexOptions.IgnoreCase);
            }
            newChars = newChars.Replace("[", "");

            return HttpUtility.HtmlDecode(newChars);
        }

        public static bool KillChars3(string imput)
        {
            int count = 0;

            string[] badChars = new string[] { "select", "drop", ";", "--", "insert", "delete", "xp_", "script", "alert", "'", "null", "cr", "lf", "prompt" };

            //string[] badChars = array("select", "drop", ";", "--", "insert", "delete", "xp_");
            string newChars = imput.ToLower();
            for (int i = 0; i < badChars.Length; i++)
            {
                if (newChars.IndexOf(badChars[i].ToString()) > 0)
                {
                    count++;
                }
                //newChars = newChars.Replace(badChars[i].ToString(), "");
            }
            if (count >= 1)
                return false;
            else
                return true;
        }


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
		
        public static void createForderUpload(string userid)
        {

            string path = ConfigurationSettings.AppSettings["UploadFile"];
            path += userid + "/";
            if (!System.IO.File.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            path = path + "/" + DateTime.Now.Year.ToString();
            if (!System.IO.File.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            if (DateTime.Now.Month < 10)
            {
                path = path + "/0" + DateTime.Now.Month.ToString();
            }
            else
            {
                path = path + "/" + DateTime.Now.Month.ToString();
            }
            if (!System.IO.File.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
        }

        public static string viewStringInsert(string strInsert, int countView)
        {
            string result = "";

            int count = (strInsert.Length + 1) / countView;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    result += strInsert.Substring(i * countView, countView) + "\r\n";
                }
            }
            else
            {
                result = strInsert;
            }
            return result;
        }

        public static string getCountInsert(int str, int count)
        {
            string result = "";            
            result = str.ToString() + "/" + (count - 2).ToString();
            return result;
        }

        public static System.DateTime getSqlDateTime(string date)
        {

            DateTime myResult = new DateTime();

            string[] myFormat = new string[] { "dd/MM/yyyy", "d/M/yyyy", "d/M/yy" };
            CultureInfo myProvider = CultureInfo.InvariantCulture;
            myProvider = new CultureInfo("fr-FR");

            try
            {
                if (date.IndexOf(" ") > 0)
                {
                    date = date.Substring(0, date.IndexOf(" "));
                }
                myResult = DateTime.ParseExact(date, myFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

            }
            catch (Exception ex)
            {
                //Console.WriteLine("" + ex.ToString());
            }

            return myResult;


        }


        public static System.DateTime getSqlDateTimeLBS(string date)
        {

            DateTime myResult = new DateTime();

            string[] myFormat = new string[] { "dd/MM/yyyy HH:mm:ss", "dd/MM/yyyy", "M/d/yyyy" };
            CultureInfo myProvider = CultureInfo.InvariantCulture;
            myProvider = new CultureInfo("fr-FR");

            try
            {
                if (date.IndexOf(" ") > 0)
                {
                    date = date.Substring(0, date.IndexOf(" "));
                }
                myResult = DateTime.ParseExact(date, myFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

            }
            catch (Exception ex)
            {
                //Console.WriteLine("" + ex.ToString());
            }

            return myResult;


        }


       
       
        public static string getCategoryID1Name(string id)
        {
            try
            {
              

                SqlParameter[] parameters = { 
                    new SqlParameter("@ID", SqlDbType.Int),																																					
                   
																								
				};
                parameters[0].Value = Convert.ToInt32(id);


                DataSet ds;
                ds = VatLid.SqlHelper.ExecuteDataset(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "Select_Category_ByID", parameters);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0]["CategoryName"].ToString().Trim();
                }
                else
                {
                    return "";
                }

            }
            catch (Exception exp)
            {
                return "";
            }
        }

        public static string getCategoryID2Name(string id)
        {
            try
            {


                SqlParameter[] parameters = { 
                    new SqlParameter("@ID", SqlDbType.Int),																																					
                   
																								
				};
                parameters[0].Value = Convert.ToInt32(id);


                DataSet ds;
                ds = VatLid.SqlHelper.ExecuteDataset(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "Select_Category2_ByID", parameters);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0]["CategoryName"].ToString().Trim();
                }
                else
                {
                    return "";
                }

            }
            catch (Exception exp)
            {
                return "";
            }
        }

        public static string getCategoryID3Name(string id)
        {
            try
            {


                SqlParameter[] parameters = { 
                    new SqlParameter("@ID", SqlDbType.Int),																																					
                   
																								
				};
                parameters[0].Value = Convert.ToInt32(id);


                DataSet ds;
                ds = VatLid.SqlHelper.ExecuteDataset(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "Select_Category3_ByID", parameters);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0]["CategoryName"].ToString().Trim();
                }
                else
                {
                    return "";
                }

            }
            catch (Exception exp)
            {
                return "";
            }
        }


        public static string getCategoryID4Name(string id)
        {
            try
            {


                SqlParameter[] parameters = { 
                    new SqlParameter("@ID", SqlDbType.Int),																																					
                   
																								
				};
                parameters[0].Value = Convert.ToInt32(id);


                DataSet ds;
                ds = VatLid.SqlHelper.ExecuteDataset(VatLid.DAL.getConnectionString1(), CommandType.StoredProcedure, "Select_Category4_ByID", parameters);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0]["CategoryName"].ToString().Trim();
                }
                else
                {
                    return "";
                }

            }
            catch (Exception exp)
            {
                return "";
            }
        }


        public static string exportExcel(System.Data.DataView dv, string[] header, string timeReport, string[] column, string dict, string filename)
        {
            try
            {
                if (HttpContext.Current.Session["USER"] == null)
                    HttpContext.Current.Response.Redirect(VatLid.Variables.sWebRoot + "logout.aspx");
                CreateExcelDoc export = new CreateExcelDoc();

               // string kk = dv.Table.Rows.Count.ToString();
                export.fillData(dict + "\\" + filename + ".xls", dv.Table, header, timeReport, HttpContext.Current.Session["USER"].ToString(), column);

                HttpContext.Current.Response.Redirect("~\\doc" + "\\" + filename + ".xls");
                return "";
            }
            catch (Exception ex)
            {
                return "";

            }
        }

        //public static System.DateTime getSqlDateTime(string date)
        //{

        //    DateTime myResult = new DateTime();

        //    string[] myFormat = new string[] { "dd/MM/yyyy", "d/M/yyyy", "d/M/yy" };
        //    CultureInfo myProvider = CultureInfo.InvariantCulture;
        //    myProvider = new CultureInfo("fr-FR");

        //    try
        //    {
        //        if (date.IndexOf(" ") > 0)
        //        {
        //            date = date.Substring(0, date.IndexOf(" "));
        //        }
        //        myResult = DateTime.ParseExact(date, myFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("" + ex.ToString());
        //    }

        //    return myResult;


        //}

        public static string getType_Date(string txtDate)
        {
            string sDate = txtDate.Trim().Replace("/", "");
            return sDate.Substring(sDate.Length - 4) + sDate.Substring(2, 2) + sDate.Substring(0, 2);
        }


        public static string getType1_Date(string txtDate)
        {
            string sDate = txtDate.Trim().Replace("/", "");
            return sDate.Substring(sDate.Length - 4) + sDate.Substring(0, 2) + sDate.Substring(2, 2);
        }

        public static string GetIP()
        {
            try
            {
                HttpRequest currentRequest = HttpContext.Current.Request;
                string ipAddress = currentRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipAddress == null || ipAddress.ToLower() == "unknown")
                    ipAddress = currentRequest.ServerVariables["REMOTE_ADDR"];
                return ipAddress;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static int getUserId(HttpSessionState session)
        {
            if ((session["USERID"] == null) || (session["USERID"] == "0"))
                return 0;
            else
                return Convert.ToInt32(session["USERID"]);
        }
        public static string getUser(HttpSessionState session)
        {
            if ((session["USER"] == null) || (session["USER"] == "0"))
                return "";
            else
                return session["USER"].ToString();
        }

        public static string StripHTML(string htmlString)
        {
            string pattern = @"<(.|\n)*?>";
            return Regex.Replace(htmlString, pattern, string.Empty);
        }

        //public static string getType_Date(string txtDate)
        //{
        //    string sDate = txtDate.Trim().Replace("/", "");
        //    return sDate.Substring(sDate.Length - 4) + sDate.Substring(0, 2) + sDate.Substring(2, 2);
        //}

        public static bool IsNumeric(string inputString)
        {
            try
            {
                return Regex.IsMatch(inputString, "^[0-9]+$");

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool checkImageTypeNew(FileUpload fileImage, Label lblError, string id, int typeimage, int update_or_insert, DateTime itemdate)
        {
            bool result = false;
            int intContentLength = Convert.ToInt32(ConfigurationSettings.AppSettings["ContentLength"]);


            try
            {

                if ((fileImage.PostedFile != null) && (fileImage.PostedFile.ContentLength != 0))
                {
                    //byte[] data = null;

                    string name = fileImage.PostedFile.FileName.ToLower();
                    if (!name.EndsWith("gif") && !name.EndsWith("jpg") && !name.EndsWith("jpeg") && !name.EndsWith("jpg") && !name.EndsWith("png"))
                    {
                        lblError.Text = "Không upload được file \"" + fileImage.PostedFile.ContentType + "\". Chỉ upload được file  .jpeg .gif .png";
                        result = true;
                    }


                    string type = fileImage.PostedFile.ContentType.ToLower();

                    if (!type.Contains("gif") && !type.Contains("jpg") && !type.Contains("jpeg") && !type.Contains("png"))
                    {
                        lblError.Text = "Không upload được file \"" + fileImage.PostedFile.ContentType + "\". Chỉ upload được file  .jpeg .gif .png";
                        result = true;
                    }

                    //  if(fileImage.n

                    byte[] data = VatLid.Utils.getByteFileNew(fileImage);

                    bool b = IsImage(data);

                    if (b == false)
                    {

                        lblError.Text = "Không upload được loại file \"" + fileImage.PostedFile.ContentType + "\". Chỉ upload loại file .jpg, .gif or .png.";
                        result = true;
                    }
                    else
                    {
                        if (typeimage == 1)
                        {
                            if (update_or_insert == 1)
                            {
                                addImageVideoToFileInsert(id + ".jpg", data);
                            }
                            else
                            {
                                addImageVideoToFileUpdate(id + ".jpg", data, itemdate);
                            }
                        }
                        else
                            if (typeimage == 2)
                                addimageToFile(id, data, ConfigurationSettings.AppSettings["Image_Categories"]);
                            else if (typeimage == 3)
                                addimageToFile(id, data, ConfigurationSettings.AppSettings["ImageVideoPath_Chanel"]);
                            else if (typeimage == 4)
                                addimageToFile(id, data, ConfigurationSettings.AppSettings["ImageVideoPath_Chanel_Banner"]);
                    }
                    if (fileImage.PostedFile.ContentLength > intContentLength)
                    {
                        lblError.Text = "File upload không quá maxsize 10Mb";
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {

                return false;
            }
            return result;
        }


        public static void addImageVideoToFileInsert(string id, byte[] imgdata)
        {
            string imdDBLocal = ConfigurationSettings.AppSettings["IMAGE_VIDEO_Path"];
            //string imghis = imdDBLocal +Convert.ToDateTime( hiddenTime.Value).ToString("yyyy/MM/dd") + "/" + id ;
            string folderDay = imdDBLocal + DateTime.Now.ToString("yyyy/MM/dd").Replace("/", ""); ;
            string filepath = folderDay + "/" + id.ToString();

            if (!Directory.Exists(folderDay))
            {
                Directory.CreateDirectory(folderDay);
            }
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            FileStream fStream = new FileStream(filepath, FileMode.CreateNew);
            BinaryWriter bw = new BinaryWriter(fStream);
            bw.Write(imgdata);
            bw.Close();
            fStream.Close();
            fStream.Dispose();
            if (bw is IDisposable)
                ((IDisposable)bw).Dispose();
        }
        public static void addimageToFile(string id, byte[] imgdata, string url)
        {

            string imdDBLocal = url;
            string folderDay = imdDBLocal + "/" + DateTime.Now.ToString("yyyy/MM/dd").Replace("/", "");
            string filepath = imdDBLocal + "/" + id.ToString();

            if (!Directory.Exists(folderDay))
            {
                Directory.CreateDirectory(folderDay);
            }


            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            FileStream fStream = new FileStream(filepath, FileMode.CreateNew);
            BinaryWriter bw = new BinaryWriter(fStream);
            bw.Write(imgdata);
            bw.Close();
            fStream.Close();
            fStream.Dispose();
            if (bw is IDisposable)
                ((IDisposable)bw).Dispose();

        }

        public static void addImageVideoToFileUpdate(string id, byte[] imgdata, DateTime itemDate)
        {
            string imdDBLocal = ConfigurationSettings.AppSettings["IMAGE_VIDEO_Path"];
            string folderDay = imdDBLocal + itemDate.ToString("yyyy/MM/dd").Replace("/", "");
            string filepath = folderDay + "/" + id.ToString();
            if (!Directory.Exists(folderDay))
            {
                Directory.CreateDirectory(folderDay);
            }
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            FileStream fStream = new FileStream(filepath, FileMode.CreateNew);
            BinaryWriter bw = new BinaryWriter(fStream);
            bw.Write(imgdata);
            bw.Close();
            fStream.Close();
            fStream.Dispose();
            if (bw is IDisposable)
                ((IDisposable)bw).Dispose();
        }


        public static byte[] getByteFileNew(FileUpload fileImage)
        {
            byte[] result = null;

            result = new byte[fileImage.PostedFile.ContentLength];
            fileImage.PostedFile.InputStream.Read(result, 0, fileImage.PostedFile.ContentLength);

            return result;

        }


        public static bool checkImageType(HtmlInputFile fileImage, Label lblError)
        {
            bool result = false;
            int intContentLength = Convert.ToInt32(ConfigurationSettings.AppSettings["ContentLength"]);


            try
            {


                // fileImage.PostedFile.


                if ((fileImage.PostedFile != null) && (fileImage.PostedFile.ContentLength != 0))
                {
                   
                    if (fileImage.PostedFile.ContentLength > intContentLength)
                    {
                        lblError.Text = "File upload không quá maxsize 10Mb";
                        result = true;
                    }
                }

            }
            catch (OutOfMemoryException)
            {

                return false;
            }


            return result;

        }

        public static bool checkImageTypeVTT(HtmlInputFile fileImage, Label lblError, string mumber, string Random, string Thang, string Ngay)
        {
            bool result = false;
            int intContentLength = Convert.ToInt32(ConfigurationSettings.AppSettings["ContentLength"]);


            try
            {

                if ((fileImage.PostedFile != null) && (fileImage.PostedFile.ContentLength != 0))
                {
                    //byte[] data = null;


                    byte[] data = VatLid.Utils.getByteFile(fileImage);

                    bool b = IsImage(data);

                    if (b == false)
                    {

                        lblError.Text = "Không upload được loại file \"" + fileImage.PostedFile.ContentType + "\". Chỉ upload loại file .jpg, .gif or .png.";
                        result = true;
                    }
                    else
                    {
                        addImageToFileVTT(VatLid.Utils.getFileImages(fileImage, Random, mumber), data, Thang, Ngay);
                    }

                    if (fileImage.PostedFile.ContentLength > intContentLength)
                    {
                        lblError.Text = "File upload không quá maxsize 10Mb";
                        result = true;
                    }
                }

            }
            catch (OutOfMemoryException)
            {

                return false;
            }


            return result;

        }

        public static void addImageToFileVTT(string id, byte[] imgdata, string thang, string ngay)
        {
            try
            {
                string imdDBLocal = VatLid.Variables.sPathToResource + @"archive\imageslead\VTT\";
                imdDBLocal += thang + "\\" + ngay + "\\";

                if (!System.IO.Directory.Exists(imdDBLocal))
                    Directory.CreateDirectory(imdDBLocal);

                string filepath = imdDBLocal + id.ToString();
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
                FileStream fStream = new FileStream(filepath, FileMode.CreateNew);
                BinaryWriter bw = new BinaryWriter(fStream);
                bw.Write(imgdata);
                bw.Close();
                fStream.Close();
                fStream.Dispose();
                if (bw is IDisposable)
                    ((IDisposable)bw).Dispose();
            }
            catch (Exception ex)
            {
                //  lblError.Text = ex.ToString();
            }
        }
      
        public static void addImageToFile(string id, byte[] imgdata, string thang, string ngay)
        {
            try
            {
                string imdDBLocal = VatLid.Variables.sPathToResource + @"archive\photo\";
                imdDBLocal +=   thang + "\\" + ngay + "\\";

                if (!System.IO.Directory.Exists(imdDBLocal))
                    Directory.CreateDirectory(imdDBLocal);

                string filepath = imdDBLocal + id.ToString();
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
                FileStream fStream = new FileStream(filepath, FileMode.CreateNew);
                BinaryWriter bw = new BinaryWriter(fStream);
                bw.Write(imgdata);
                bw.Close();
                fStream.Close();
                fStream.Dispose();
                if (bw is IDisposable)
                    ((IDisposable)bw).Dispose();
            }
            catch (Exception ex)
            {
                //  lblError.Text = ex.ToString();
            }
        }

        public static bool checkImageTypeThiAnh(HtmlInputFile fileImage, Label lblError, string mumber, string Random, string Thang, string Ngay)
        {
            bool result = false;
            int intContentLength = Convert.ToInt32(ConfigurationSettings.AppSettings["ContentLength"]);


            try
            {

                if ((fileImage.PostedFile != null) && (fileImage.PostedFile.ContentLength != 0))
                {
                    //byte[] data = null;


                    byte[] data = VatLid.Utils.getByteFile(fileImage);

                    bool b = IsImage(data);

                    if (b == false)
                    {

                        lblError.Text = "Không upload được loại file \"" + fileImage.PostedFile.ContentType + "\". Chỉ upload loại file .jpg, .gif or .png.";
                        result = true;
                    }
                    else
                    {
                        addImageToFileThiAnh(VatLid.Utils.getFileImages(fileImage, Random, mumber), data, Thang, Ngay);
                    }

                    if (fileImage.PostedFile.ContentLength > intContentLength)
                    {
                        lblError.Text = "File upload không quá maxsize 10Mb";
                        result = true;
                    }
                }

            }
            catch (OutOfMemoryException)
            {

                return false;
            }


            return result;

        }

        public static void addImageToFileThiAnh(string id, byte[] imgdata, string thang, string ngay)
        {
            try
            {
                string imdDBLocal = VatLid.Variables.sPathToResource + @"archive\ThiAnh\";
                imdDBLocal += thang + "\\" + ngay + "\\";

                if (!System.IO.Directory.Exists(imdDBLocal))
                    Directory.CreateDirectory(imdDBLocal);

                string filepath = imdDBLocal + id.ToString();
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
                FileStream fStream = new FileStream(filepath, FileMode.CreateNew);
                BinaryWriter bw = new BinaryWriter(fStream);
                bw.Write(imgdata);
                bw.Close();
                fStream.Close();
                fStream.Dispose();
                if (bw is IDisposable)
                    ((IDisposable)bw).Dispose();
            }
            catch (Exception ex)
            {
                //  lblError.Text = ex.ToString();
            }
        }

        public static bool IsImage(byte[] data)
        {
            //read 64 bytes of the stream only to determine the type
            string myStr = System.Text.Encoding.ASCII.GetString(data).Substring(0, 16);
            //check if its definately an image. 
            if (myStr.Substring(8, 2).ToString().ToLower() != "if")
            {
                //its not a jpeg
                if (myStr.Substring(0, 3).ToString().ToLower() != "gif")
                {
                    //its not a gif
                    if (myStr.Substring(1, 3).ToString().ToLower() != "png")
                    {
                        myStr = null;
                        return false;

                        ////its not a .png
                        //if (myStr.Substring(0, 2).ToString().ToLower() != "bm")
                        //{                           
                        //    myStr = null;
                        //    return false;
                        //}
                    }
                }
            }
            myStr = null;
            return true;
        }

        public static string getFileImages(HtmlInputFile fileImage, string IDImageRandom, string mumber)
        {

          
            string fileImages = "";

            string filename = fileImage.PostedFile.FileName;

            int pos = filename.LastIndexOfAny(new char[] { '/', '\\' });
            if (pos >= 0) filename = filename.Substring(pos + 1);

            if ((fileImage.PostedFile != null) && (fileImage.PostedFile.ContentLength != 0))
            {
                fileImages = "thumb" + mumber + "_" + IDImageRandom + filename.Substring(filename.LastIndexOf("."));                
            }

            return fileImages;

        }
        public static byte[] getByteFile(HtmlInputFile fileImage)
        {
            byte[] result = null;

            result = new byte[fileImage.PostedFile.ContentLength];
            fileImage.PostedFile.InputStream.Read(result, 0, fileImage.PostedFile.ContentLength);

            return result;

        }

        public static string getPathFileName(HtmlInputFile fileImage, string IDImageRandom, string mumber)
        {
            string pathResult = "";

            string path1 = @"archive/ThiAnh/";
            string fileimages = getFileImages(fileImage, IDImageRandom, mumber);

            if (fileimages.Length > 1)
            {
                pathResult = path1 + fileimages;
                
            }
            else
            {
                pathResult = path1 + "template.jpg";

            }

            return pathResult;

        }

        public static string getPathFileName(HtmlInputFile fileImage, string album, string IDImageRandom, string mumber)
        {
            string pathResult = "";

            string path1 = @"archive/imageslead/" + album + "/";
            string fileimages = getFileImages(fileImage, IDImageRandom, mumber);

            if (fileimages.Length > 1)
            {
                pathResult = path1 + fileimages;

            }
            else
            {
                pathResult = path1 + "template.jpg";

            }

            return pathResult;

        }


        public static string KillChars1(string imput)
        {
            string[] badChars = new string[] { "select", "drop", ";", "--", "insert", "delete", "xp_", "script", "alert", "'", "null", "cr", "lf" };

            //string[] badChars = array("select", "drop", ";", "--", "insert", "delete", "xp_");
            string newChars = imput.ToLower();
            for (int i = 0; i < badChars.Length; i++)
            {
                newChars = newChars.Replace(badChars[i].ToString(), "");
            }

            return newChars;
        }

        public static string GetListSelected(DataGrid dgrCommon)
        {
            string sTemp = "";
            int i;
            for (i = 0; i < dgrCommon.Items.Count; i++)
            {
                if (((CheckBox)dgrCommon.Items[i].FindControl("chkAllDelete")).Checked == true)
                {
                    if (sTemp != "") { sTemp += ","; }
                    sTemp += dgrCommon.DataKeys[i];
                }
            }
            return sTemp;
        }

        public static string UpdateStatus(string connStrSport, DataGrid dgrCommon, string sTableName, string sFieldName, int intvalue, string logMessage)
        {
            string sTemp = GetListSelected(dgrCommon);
            if (sTemp != "")
            {
                logMessage = "Update bảng " + sTableName + ":" + sTemp + " sang trạng thái " + intvalue;
                string SQL = "";
                try
                {
                    SQL = "UPDATE " + sTableName + " SET " + sFieldName + " = " + intvalue + " WHERE ID in (" + sTemp + ")";
                    VatLid.DAL.ExecuteQuery(connStrSport, SQL);
                    //if (logMessage != "")
                    //{
                    //    VatLid.DAL.INSERT_USER_LOG(HttpContext.Current.Session["USER"].ToString(), logMessage, sTemp, "OK");
                    //}
                    return "Thực hiện thành công: " + sTemp;
                }
                catch (Exception e)
                {
                    return "Thực hiện không thành công: " + sTemp;
                }
            }
            else
            {
                VatLid.MessageBox.Show("Bạn phải chọn để thực hiện.");
                return "";
            }
        }

        public static string UpdateDate(string connStrSport, DataGrid dgrCommon, string tableName, string fieldName)
        {
            string IDS = "";
            try
            {
                IDS = GetListSelected(dgrCommon);
                SqlParameter[] parameters = 
                    { 
                        new SqlParameter("@TableName", SqlDbType.NVarChar ),//0
                        new SqlParameter("@FieldName", SqlDbType.NVarChar ),//0
                        new SqlParameter("@IDS", SqlDbType.NVarChar ),//0
                    };
                parameters[0].Value = tableName;
                parameters[1].Value = fieldName;
                parameters[2].Value = IDS;
                VatLid.SqlHelper.ExecuteNonQuery(connStrSport, CommandType.StoredProcedure, "update_Date", parameters);
                return "";
            }
            catch (Exception ex)
            {
                return "Thực hiện không thành công: " + IDS;
            }
        }

        public static string GetKeyWordFilter(int index, string keyword, string listField)
        {
            string keywordSearch = CleanInput(keyword);
            // index = index - 1;
            if (keywordSearch.Trim().Length > 0)
            {
                string[] fields = listField.Split(',');
                if (fields.Length < 1)
                    return " ";
                string filter = "";
                switch (index)
                {
                    case 0: // tat ca
                        if (fields.Length == 1)
                            filter = " AND CHARINDEX(N'" + keywordSearch.Trim() + "'," + fields[0] + ")<>0 ";
                        else
                        {
                            filter = " AND (CHARINDEX(N'" + keywordSearch.Trim() + "'," + fields[0] + ")<>0 ";
                            for (int i = 1; i < fields.Length; i++)
                                filter += " OR CHARINDEX(N'" + keywordSearch.Trim() + "'," + fields[i] + ")<>0";
                            filter += ") ";
                        }
                        break;
                    default:
                        filter = " AND CHARINDEX(N'" + keywordSearch.Trim() + "'," + fields[index - 1] + ")<>0 ";
                        break;
                }
                return filter;
            }
            else
                return " ";
        }

        public static String CleanInput(string strIn)
        {
            // Replace invalid characters with empty strings.
            string result = Regex.Replace(strIn.Trim(), @"[^\w\.@-]", " ");
            result = Regex.Replace(result, " {2,}", " ");
            return result;
        }

        public static void UpdateStatus(string connStrSport, string sTableName, string sFieldName, int intvalue, string sID)
        {
            string SQL = "";
            try
            {
                SQL = "UPDATE " + sTableName + " SET " + sFieldName + " = " + intvalue + " WHERE ID in (" + sID + ")";
                VatLid.DAL.ExecuteQuery(connStrSport, SQL);
            }
            catch (Exception e)
            {
                VatLid.DAL.ExceptionProcess(e);
            }
        }


        private static string removeStr(string strRaw, string[] badStr)
        {
            //try
            //{
                if (string.IsNullOrEmpty(strRaw)) return "";
                int i ;
                string tmp = strRaw.ToLower();
                foreach (string remove in badStr)
                {
                    i = tmp.IndexOf(remove);
                    if (i > -1)
                    {
                        int n = tmp.Length;
                        if (n > 1)
                        {
                            int j = tmp.IndexOfAny(new char[] { ';', '-', ' ' }, i);
                            if (j - i > n) n = j - i;
                        }
                        tmp = tmp.Remove(i, n);
                        strRaw = strRaw.Remove(i, n);
                    }
                }
                return strRaw.Trim();
            //} catch 
            //{
            //    return "";// strRaw.Trim();
            //}
        }

        public static string safeString(string unSafe)
        {
            return removeStr(unSafe, Bad_String);
        }
        private static string[] Bad_String = { "xp_", ";", "--", "<", ">", "script", "iframe", "delete", "drop", "exec", "insert", 
									"--",";--",";",
									"char","nchar","varchar","nvarchar",
									"alter","begin","cast","create","cursor","declare","delete","drop","end","exec","execute",
									"fetch","insert","kill","open",
									"select", "sys","sysobjects","syscolumns",
									"table","update"
			};

        //Doàn Vũ Check ký tự đặc biệt
        private static string[] Special_Characters = { "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "~", "?", ">", "<" };

        public static bool isSpecial_Characters(string strRaw)
        {
            if (string.IsNullOrEmpty(strRaw)) return false;
            int i;
            string tmp = strRaw.ToLower();
            foreach (string remove in Special_Characters)
            {
                i = tmp.IndexOf(remove);
                if (i > -1)
                {
                    return true;
                }
            }
            return false;
        }

        public static string getFileImages(HtmlInputFile fileImage, string IDImageRandom, string mumber, string thang, string ngay)
        {

            string imdDBLocal = "archive\\photo\\";
            imdDBLocal += thang + "\\" + ngay + "\\";

            string fileImages = "";

            string filename = fileImage.PostedFile.FileName;

            int pos = filename.LastIndexOfAny(new char[] { '/', '\\' });
            if (pos >= 0) filename = filename.Substring(pos + 1);

            if ((fileImage.PostedFile != null) && (fileImage.PostedFile.ContentLength != 0))
            {
                fileImages = "thumb" + mumber + "_" + IDImageRandom + filename.Substring(filename.LastIndexOf("."));
                fileImages = imdDBLocal + fileImages;
            }

            return fileImages;

        }
        //public static string getPathFileName(HtmlInputFile fileImage, string Thang, string Ngay, List<string> list_images, string IDImageRandom, string mumber, string UserID)
        //{
        //    string pathResult = "";

        //    string path1 = @"archive/imageslead/";
        //    string fileimages = getFileImages(fileImage, IDImageRandom, mumber);

        //    if (fileimages.Length > 1)
        //    {
        //        pathResult = path1 + UserID + "/" + Thang + "/" + Ngay + "/" + fileimages;


        //        list_images.Add(@"http://images.dailyinfo.vn/" + pathResult);



        //        //Image ima =   Image.FromFile(Variables.sPathToResource + pathResult);

        //        if (mumber == "00")
        //        {
        //            byte[] data = null;
        //            FileInfo fInfo = new FileInfo(Variables.sPathToResource + pathResult);
        //            long numBytes = fInfo.Length;
        //            FileStream fStream = new FileStream(Variables.sPathToResource + pathResult, FileMode.Open, FileAccess.Read);
        //            BinaryReader br = new BinaryReader(fStream);
        //            data = br.ReadBytes((int)numBytes);


        //            MemoryStream img = new MemoryStream(data);
        //            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(img);


        //            list_images.Add(Variables.sLinkToResource + resizeImage(returnImage, 146, pathResult));
        //            list_images.Add(Variables.sLinkToResource + resizeImage(returnImage, 162, pathResult));
        //            list_images.Add(Variables.sLinkToResource + resizeImage(returnImage, 100, pathResult));
        //            // cat anh cho ipad
        //            list_images.Add(Variables.sLinkToResource + resizeImage(returnImage, 320, pathResult));
        //            list_images.Add(Variables.sLinkToResource + resizeImage(returnImage, 240, pathResult));

        //            // resizeImage(returnImage, 100, 100, pathResult);

        //        }
        //        if (mumber == "4")
        //        {
        //            byte[] data = null;
        //            FileInfo fInfo = new FileInfo(Variables.sPathToResource + pathResult);
        //            long numBytes = fInfo.Length;
        //            FileStream fStream = new FileStream(Variables.sPathToResource + pathResult, FileMode.Open, FileAccess.Read);
        //            BinaryReader br = new BinaryReader(fStream);
        //            data = br.ReadBytes((int)numBytes);

        //            MemoryStream img = new MemoryStream(data);
        //            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(img);

        //            list_images.Add(Variables.sLinkToResource + resizeImage(returnImage, 320, pathResult));
        //            list_images.Add(Variables.sLinkToResource + resizeImage(returnImage, 260, pathResult));
        //            list_images.Add(Variables.sLinkToResource + resizeImage(returnImage, 250, pathResult));
        //            list_images.Add(Variables.sLinkToResource + resizeImage(returnImage, 280, pathResult));
        //            list_images.Add(Variables.sLinkToResource + resizeImage(returnImage, 240, pathResult));
        //        }

        //        if (mumber == "3")
        //        {
        //            byte[] data = null;
        //            FileInfo fInfo = new FileInfo(Variables.sPathToResource + pathResult);
        //            long numBytes = fInfo.Length;
        //            FileStream fStream = new FileStream(Variables.sPathToResource + pathResult, FileMode.Open, FileAccess.Read);
        //            BinaryReader br = new BinaryReader(fStream);
        //            data = br.ReadBytes((int)numBytes);

        //            MemoryStream img = new MemoryStream(data);
        //            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(img);

        //            list_images.Add(Variables.sLinkToResource + resizeImage(returnImage, 75, pathResult));

        //        }

        //        if (mumber == "11")
        //        {
        //            byte[] data = null;
        //            FileInfo fInfo = new FileInfo(Variables.sPathToResource + pathResult);
        //            long numBytes = fInfo.Length;
        //            FileStream fStream = new FileStream(Variables.sPathToResource + pathResult, FileMode.Open, FileAccess.Read);
        //            BinaryReader br = new BinaryReader(fStream);
        //            data = br.ReadBytes((int)numBytes);

        //            MemoryStream img = new MemoryStream(data);
        //            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(img);

        //            list_images.Add(Variables.sLinkToResource + resizeImage(returnImage, 320, pathResult));
        //        }

        //        if (mumber == "7")
        //        {
        //            byte[] data = null;
        //            FileInfo fInfo = new FileInfo(Variables.sPathToResource + pathResult);
        //            long numBytes = fInfo.Length;
        //            FileStream fStream = new FileStream(Variables.sPathToResource + pathResult, FileMode.Open, FileAccess.Read);
        //            BinaryReader br = new BinaryReader(fStream);
        //            data = br.ReadBytes((int)numBytes);

        //            MemoryStream img = new MemoryStream(data);
        //            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(img);

        //            list_images.Add(Variables.sLinkToResource + resizeImage(returnImage, 75, pathResult));
        //        }

        //        if (mumber == "6")
        //        {
        //            byte[] data = null;
        //            FileInfo fInfo = new FileInfo(Variables.sPathToResource + pathResult);
        //            long numBytes = fInfo.Length;
        //            FileStream fStream = new FileStream(Variables.sPathToResource + pathResult, FileMode.Open, FileAccess.Read);
        //            BinaryReader br = new BinaryReader(fStream);
        //            data = br.ReadBytes((int)numBytes);

        //            MemoryStream img = new MemoryStream(data);
        //            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(img);

        //            list_images.Add(Variables.sLinkToResource + resizeImage(returnImage, 320, pathResult));
        //            list_images.Add(Variables.sLinkToResource + resizeImage(returnImage, 240, pathResult));
        //        }
        //    }
        //    else
        //    {
        //        pathResult = path1 + "/" + "template.jpg";

        //    }
        //    return pathResult;
        //}

        public static string resizeImage(System.Drawing.Image ima, int widths, string fileName)
        {
            try
            {
                int maxWidth = widths;
                decimal dHeight, dWidth, dNewHeight, dNewWidth;
                if (widths == 320)
                    maxWidth = widths;



                if (fileName.ToLower().Contains(".jpg"))
                {
                    fileName = fileName.ToLower().Replace(".jpg", "wap_" + maxWidth.ToString() + ".jpg");
                }
                else if (fileName.ToLower().Contains(".jpeg"))
                {
                    fileName = fileName.ToLower().Replace(".jpeg", "wap_" + maxWidth.ToString() + ".jpeg");
                }
                else if (fileName.ToLower().Contains(".gif"))
                {
                    fileName = fileName.ToLower().Replace(".gif", "wap_" + maxWidth.ToString() + ".gif");
                }
                else if (fileName.ToLower().Contains(".png"))
                {
                    fileName = fileName.ToLower().Replace(".png", "wap_" + maxWidth.ToString() + ".png");
                }
                fileName = fileName.Replace("/", "\\");

                if (maxWidth == 75) maxWidth = 100;


                //if (widths == 320)
                //    maxWidth = 400;

                int width = ima.Width;
                int height = ima.Height;

                if (width > maxWidth)
                {
                    Decimal divider = Math.Abs((Decimal)width / (Decimal)maxWidth);
                    height = (int)Math.Round((Decimal)(height / divider));
                }
                else
                {
                    maxWidth = width;
                }

                Size newSize = new Size(maxWidth, height);
                // Bitmap dstImg = new Bitmap(newOrigWidth, newOrigHeight);
                // dstImg.SetResolution(72, 72);
                //Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);
                using (Bitmap thumb = new Bitmap(maxWidth, height, PixelFormat.Format64bppArgb))
                {
                    //thumb.SetResolution(500, 500);
                    using (Graphics g = Graphics.FromImage(thumb)) // Create Graphics object from original Image
                    {
                        g.CompositingQuality = CompositingQuality.HighQuality;
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.DrawImage(ima, new Rectangle(0, 0, thumb.Width, thumb.Height));

                        string path_to_folder = System.IO.Path.GetDirectoryName(Variables.sPathToResource + fileName);
                        if (!System.IO.Directory.Exists(path_to_folder))
                            Directory.CreateDirectory(path_to_folder);

                        EncoderParameters encoderParameters = new EncoderParameters(1);
                        encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 95L);
                        thumb.Save(Variables.sPathToResource + fileName, ImageCodecInfo.GetImageEncoders()[1], encoderParameters);

                    }

                }

                return fileName;
            }
            catch (Exception ex) { return ""; }
        }
        public static void addMusicToFileInsert(string id, byte[] imgdata)
        {

            string imdDBLocal = ConfigurationSettings.AppSettings["VideoDBPath"];
            string folderDay = imdDBLocal + DateTime.Now.ToString("yyyy/MM/dd").Replace("/", ""); ;
            string filepath = folderDay + "/" + id.ToString();

            if (!Directory.Exists(folderDay))
            {
                Directory.CreateDirectory(folderDay);
            }


            if (File.Exists(filepath))
            {

                File.Delete(filepath);
            }
            FileStream fStream = new FileStream(filepath, FileMode.CreateNew);
            BinaryWriter bw = new BinaryWriter(fStream);
            bw.Write(imgdata);
            bw.Close();
            fStream.Close();
            fStream.Dispose();
            if (bw is IDisposable)
                ((IDisposable)bw).Dispose();
        }

        public static void addMusicToFileUpdate(string id, byte[] imgdata, DateTime datetime)
        {
            string imdDBLocal = ConfigurationSettings.AppSettings["VideoDBPath"];
            //  string videohis = imdDBLocal + datetime.ToString("yyyy/MM/dd").Replace("/", "") + "/" + itemfile;
            string folderDay = imdDBLocal + datetime.ToString("yyyy/MM/dd").Replace("/", "");
            string filepath = folderDay + "/" + id.ToString();
            if (!Directory.Exists(folderDay))
            {
                Directory.CreateDirectory(folderDay);
            }


            if (File.Exists(filepath))
            {

                File.Delete(filepath);
            }
            //if (File.Exists(videohis))
            //{
            //    File.Delete(videohis);
            //}
            FileStream fStream = new FileStream(filepath, FileMode.CreateNew);
            BinaryWriter bw = new BinaryWriter(fStream);
            bw.Write(imgdata);
            bw.Close();
            fStream.Close();
            fStream.Dispose();
            if (bw is IDisposable)
                ((IDisposable)bw).Dispose();
        }

       

        public static bool checkFLVType(FileUpload fileImage, Label lblError, string itemfile, int type, int update_or_insert, DateTime itemdate)
        {
            bool result = false;
            int intContentLength = Convert.ToInt32(ConfigurationSettings.AppSettings["ContentLength"]);
            try
            {

                if ((fileImage.PostedFile != null) && (fileImage.PostedFile.ContentLength != 0))
                {
                    byte[] data = VatLid.Utils.getByteFileNew(fileImage);
                    byte[] buffer = new byte[256];

                    for (int i = 0; i < 256; i++)
                    {
                        buffer[i] = data[i];
                    }

                    System.UInt32 mimetype;
                    FindMimeFromData(0, null, buffer, 256, null, 0, out mimetype, 0);
                    System.IntPtr mimeTypePtr = new IntPtr(mimetype);
                    string mime = Marshal.PtrToStringUni(mimeTypePtr);
                    Marshal.FreeCoTaskMem(mimeTypePtr);

                    if (mime == "application/octet-stream")
                    {
                        if (type == 2)//mp3
                        {
                            // addRadioToFile(VatLid.Utils.getFileRadio(fileImage, Random, "12"), data, Thang, Ngay, UserID);
                        }
                        else if (type == 1)
                        {
                            // addVideoToFile(VatLid.Utils.getFileVideo(fileImage, Random, "9"), data, Thang, Ngay, UserID);
                        }
                        else if (type == 3)
                        {
                            if (update_or_insert == 1)
                                addMusicToFileInsert(itemfile, data);
                            else
                                addMusicToFileUpdate(itemfile, data, itemdate);
                        }
                        else if (type == 4)
                        {
                            // addVideoToFile(VatLid.Utils.getFileVideo(fileImage, Random, "16"), data, Thang, Ngay, UserID);
                        }
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {

                return true;
            }


            return result;

        }

        [DllImport(@"urlmon.dll", CharSet = CharSet.Auto)]
        private extern static System.UInt32 FindMimeFromData(
            System.UInt32 pBC,
            [MarshalAs(UnmanagedType.LPStr)] System.String pwzUrl,
            [MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer,
            System.UInt32 cbSize,
            [MarshalAs(UnmanagedType.LPStr)] System.String pwzMimeProposed,
            System.UInt32 dwMimeFlags,
            out System.UInt32 ppwzMimeOut,
            System.UInt32 dwReserverd)
        ;
    }
}
