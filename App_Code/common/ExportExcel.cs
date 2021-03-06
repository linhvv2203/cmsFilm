using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
/// <summary>
/// Summary description for ExportExcel
/// </summary>
/// 
namespace ExportExcel
{
    public class ExportExcel
    {
        public ExportExcel()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static bool print_as_excel(DataView dv,String path,String name)
        {
            
            String url = path + "\\" + name;
            try
            {
                StreamWriter file = new StreamWriter(url, false, Encoding.Unicode);

                int col;
                bool first_column;

                // column names
                first_column = true;
                file.Write("<table>");
                file.Write("<tr>");
                for (col = 0; col < dv.Table.Columns.Count; col++)
                {
                    if (dv.Table.Columns[col].ColumnName == "$FLAG") continue;

                    if (!first_column)
                    {
                        file.Write("\t");
                    }
                    file.Write("<td style=\"border-style:inset;font-weight:bold;background-color:yellow\">");
                    file.Write(dv.Table.Columns[col].ColumnName);
                    file.Write("</td>");
                    first_column = false;
                }
                file.Write("</tr>");
                file.Write("\n");

                int index = 0;
                foreach (DataRowView drv in dv)
                {
                    index++;
                    first_column = true;
                    file.Write("<tr>");
                    for (col = 0; col < dv.Table.Columns.Count; col++)
                    {
                        if (dv.Table.Columns[col].ColumnName == "$FLAG") continue;

                        if (!first_column)
                        {
                            file.Write("\t");
                        }
                        String value = drv[col].ToString();
                        if(index % 2 == 0)
                            file.Write("<td style='text-align:left;border-bottom:1px;background-color:Gainsboro'>");
                        else
                            file.Write("<td style='text-align:left;border-bottom:1px;'>");
                        file.Write(value);
                        file.Write("</td>");
                        first_column = false;
                    }
                    file.Write("</tr>");
                    file.Write("\n");
                }
                file.Close();
                return true;
            }
            catch (Exception aa) {
                return false;
            }

        }
        public static bool print_as_excel1(DataSet ds, String path, String name)
        {

            String url = path + "\\" + name;
            try
            {
                StreamWriter file = new StreamWriter(url, false, Encoding.Unicode);
                file.Write("<table>");
                file.Write("<tr>");
                file.Write("<th style=\"border-style:inset;font-weight:bold;background-color:yellow\">STT</th>");
                file.Write("<th style=\"border-style:inset;font-weight:bold;background-color:yellow\">NỘI DUNG</th>");
                for (int i= 0; i < ds.Tables.Count; i++)
                {
                    file.Write("<th style=\"border-style:inset;font-weight:bold;background-color:yellow\">");
                    file.Write(ds.Tables[i].Rows[0]["DateTime"].ToString());
                    file.Write("</th>");
                }
                file.Write("</tr>");
                file.Write("\n");
                file.Write("<tr><td>1</td><td>Số thuê bao đăng ký mới trong ngày</td>");
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    file.Write("<td>"+ds.Tables[i].Rows[0]["SuBNgayDK"].ToString()+"</td>");
                }
                file.Write("</tr>");

                file.Write("\n");
                file.Write("<tr><td>2</td><td>Số thuê bao hủy trong ngày</td>");
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    file.Write("<td>" + ds.Tables[i].Rows[0]["SuBNgayHuy"].ToString() + "</td>");
                }
                file.Write("</tr>");

                file.Write("\n");
                file.Write("<tr><td>3</td><td>Tổng thuê bao lũy kế ngày</td>");
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    file.Write("<td>" + ds.Tables[i].Rows[0]["SuBNgayDKLuyKe"].ToString() + "</td>");
                }
                file.Write("</tr>");

                file.Write("\n");
                file.Write("<tr><td>4</td><td>Tổng thuê bao đem đi trừ cước</td>");
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    file.Write("<td>" + ds.Tables[i].Rows[0]["SubTruCuoc"].ToString() + "</td>");
                }
                file.Write("</tr>");

                file.Write("\n");
                file.Write("<tr><td>5</td><td>Tổng thuê bao trừ cước thành công</td>");
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    file.Write("<td>" + ds.Tables[i].Rows[0]["SubTruCuocTC"].ToString() + "</td>");
                }
                file.Write("</tr>");

                file.Write("\n");
                file.Write("<tr><td>6</td><td>Tổng doanh thu thuê bao ngày</td>");
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    file.Write("<td>" + ds.Tables[i].Rows[0]["DTSub"].ToString() + "</td>");
                }
                file.Write("</tr>");

                file.Write("\n");
                file.Write("<tr><td>7</td><td>Tổng lượt view của đối tác</td>");
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    file.Write("<td>" + ds.Tables[i].Rows[0]["ViewCP"].ToString() + "</td>");
                }
                file.Write("</tr>");

                file.Write("\n");
                file.Write("<tr><td>8</td><td>Tổng lượt view 3G của đối tác</td>");
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    file.Write("<td>" + ds.Tables[i].Rows[0]["ViewCP3gVT"].ToString() + "</td>");
                }
                file.Write("</tr>");

                file.Write("\n");
                file.Write("<tr><td>9</td><td>Tổng lượt view không 3G của đối tác</td>");
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    file.Write("<td>" + ds.Tables[i].Rows[0]["ViewCPNot3g"].ToString() + "</td>");
                }
                file.Write("</tr>");
                file.Write("</table>");
                file.Close();
                return true;
            }
            catch (Exception aa)
            {
                return false;
            }

        }

        public static bool print_as_excel_OnKid(DataSet ds, DataSet ds1, int tong, String path, String name)
        {

            String url = path + "\\" + name;
            try
            {
                StreamWriter file = new StreamWriter(url, false, Encoding.Unicode);
                file.Write("<table>");
                file.Write("<tr>");
                file.Write("<th style=\"border-style:inset;font-weight:bold;background-color:yellow\">STT</th>");
                file.Write("<th style=\"border-style:inset;font-weight:bold;background-color:yellow\">THÔNG TIN THUÊ BAO</th>");
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    file.Write("<th style=\"border-style:inset;font-weight:bold;background-color:yellow\">");
                    file.Write(ds.Tables[i].Rows[0]["DateTime"].ToString());
                    file.Write("</th>");
                }
                file.Write("</tr>");
                file.Write("\n");
                file.Write("<tr><td>1</td><td>Tổng thuê bao.</td>");
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    file.Write("<td>" + ds.Tables[i].Rows[0]["TongThueBao"].ToString() + "</td>");
                }
                file.Write("</tr>");

                file.Write("\n");
                file.Write("<tr><td>2</td><td>Tổng số thuê bao đang kích hoạt.</td>");
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    file.Write("<td>" + ds.Tables[i].Rows[0]["ThueBaoActive"].ToString() + "</td>");
                }
                file.Write("</tr>");

                file.Write("\n");
                file.Write("<tr><td>3</td><td>Tổng số thuê bao gói ngày đang kích hoạt</td>");
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    file.Write("<td>" + ds.Tables[i].Rows[0]["ThueBaoNgayActive"].ToString() + "</td>");
                }
                file.Write("</tr>");

                file.Write("\n");
                file.Write("<tr><td>4</td><td>Tổng số thuê bao gói tuần đang kích hoạt</td>");
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    file.Write("<td>" + ds.Tables[i].Rows[0]["ThueBaoTuanActive"].ToString() + "</td>");
                }
                file.Write("</tr>");

                file.Write("\n");
                file.Write("<tr><td>5</td><td>Tổng số thuê bao gói tháng đang kích hoạt</td>");
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    file.Write("<td>" + ds.Tables[i].Rows[0]["ThueBaoThangActive"].ToString() + "</td>");
                }
                file.Write("</tr>");

                file.Write("\n");
                file.Write("<tr><td>6</td><td>Số thuê bao hủy dịch vụ trong ngày</td>");
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    file.Write("<td>" + ds.Tables[i].Rows[0]["ThueBaoHuy"].ToString() + "</td>");
                }
                file.Write("</tr>");

                file.Write("\n");
                file.Write("<tr><td>7</td><td>Số thuê bao đăng ký mới trong ngày</td>");
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    file.Write("<td>" + ds.Tables[i].Rows[0]["ThueBaoDangKy"].ToString() + "</td>");
                }
                file.Write("</tr>");

                file.Write("\n");
                file.Write("<tr><td>8</td><td>Số thuê bao đăng ký mới qua wap</td>");
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    file.Write("<td>" + ds.Tables[i].Rows[0]["ThueBaoDKWAP"].ToString() + "</td>");
                }
                file.Write("</tr>");

                file.Write("\n");
                file.Write("<tr><td>9</td><td>Số thuê bao đăng ký mới qua sms</td>");
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    file.Write("<td>" + ds.Tables[i].Rows[0]["ThueBaoDKSMS"].ToString() + "</td>");
                }
                file.Write("</tr>");

                file.Write("\n");
                file.Write("<tr><td>10</td><td>Số thuê bao đăng ký mới gói ngày</td>");
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    file.Write("<td>" + ds.Tables[i].Rows[0]["ThueBaoDKNgay"].ToString() + "</td>");
                }
                file.Write("</tr>");

                file.Write("\n");
                file.Write("<tr><td>11</td><td>Số thuê bao đăng ký mới gói tuần</td>");
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    file.Write("<td>" + ds.Tables[i].Rows[0]["ThueBaoDKTuan"].ToString() + "</td>");
                }
                file.Write("</tr>");

                file.Write("\n");
                file.Write("<tr><td>12</td><td>Số thuê bao đăng ký mới gói tháng</td>");
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    file.Write("<td>" + ds.Tables[i].Rows[0]["ThueBaoDKThang"].ToString() + "</td>");
                }
                file.Write("</tr>");

                file.Write("\n");
                file.Write("<tr><td>13</td><td>Số TB gói ngày đem đi trừ cước</td>");
                for (int i = 0; i < ds1.Tables.Count; i++)
                {
                    file.Write("<td>" + ds1.Tables[i].Rows[0]["ThueBaoNgayTruCuoc"].ToString() + "</td>");
                }
                file.Write("</tr>");

                file.Write("\n");
                file.Write("<tr><td>14</td><td>Số TB gói ngày trừ cước thành công</td>");
                for (int i = 0; i < ds1.Tables.Count; i++)
                {
                    file.Write("<td>" + ds1.Tables[i].Rows[0]["ThueBaoNgayTC"].ToString() + "</td>");
                }
                file.Write("</tr>");

                file.Write("\n");
                file.Write("<tr><td>15</td><td>Số TB gói tuần đem đi trừ cước</td>");
                for (int i = 0; i < ds1.Tables.Count; i++)
                {
                    file.Write("<td>" + ds1.Tables[i].Rows[0]["ThueBaoTuanTruCuoc"].ToString() + "</td>");
                }
                file.Write("</tr>");


                file.Write("\n");
                file.Write("<tr><td>16</td><td>Số TB gói tuần trừ cước thành công</td>");
                for (int i = 0; i < ds1.Tables.Count; i++)
                {
                    file.Write("<td>" + ds1.Tables[i].Rows[0]["ThueBaoTuanTC"].ToString() + "</td>");
                }
                file.Write("</tr>");


                file.Write("\n");
                file.Write("<tr><td>17</td><td>Số TB gói tháng đem đi trừ cước</td>");
                for (int i = 0; i < ds1.Tables.Count; i++)
                {
                    file.Write("<td>" + ds1.Tables[i].Rows[0]["ThueBaoThangTruCuoc"].ToString() + "</td>");
                }
                file.Write("</tr>");


                file.Write("\n");
                file.Write("<tr><td>18</td><td>Số TB gói tháng trừ cước thành công</td>");
                for (int i = 0; i < ds1.Tables.Count; i++)
                {
                    file.Write("<td>" + ds1.Tables[i].Rows[0]["ThueBaoThangTC"].ToString() + "</td>");
                }
                file.Write("</tr>");

                file.Write("</table>");

                file.Write("<br/>");

                file.Write("<table>");
                file.Write("<tr>");
                file.Write("<th style=\"border-style:inset;font-weight:bold;background-color:yellow\">STT</th>");
                file.Write("<th style=\"border-style:inset;font-weight:bold;background-color:yellow\">THÔNG TIN DOANH THU</th>");
                for (int i = 0; i < ds1.Tables.Count; i++)
                {
                    file.Write("<th style=\"border-style:inset;font-weight:bold;background-color:yellow\">");
                    file.Write(ds1.Tables[i].Rows[0]["DateTime"].ToString());
                    file.Write("</th>");
                }
                file.Write("</tr>");
                file.Write("\n");

                file.Write("<tr><td>1</td><td>Doanh thu trong ngày</td>");
                for (int i = 0; i < ds1.Tables.Count; i++)
                {
                    file.Write("<td>" + ds1.Tables[i].Rows[0]["DoanhThu"].ToString() + "</td>");
                }
                file.Write("</tr>");

                file.Write("\n");
                file.Write("<tr><td>2</td><td>Doanh thu gói ngày</td>");
                for (int i = 0; i < ds1.Tables.Count; i++)
                {
                    file.Write("<td>" + ds1.Tables[i].Rows[0]["DoanhThuGoiNgay"].ToString() + "</td>");
                }
                file.Write("</tr>");


                file.Write("\n");
                file.Write("<tr><td>3</td><td>Số TB gói tuần trừ cước thành công</td>");
                for (int i = 0; i < ds1.Tables.Count; i++)
                {
                    file.Write("<td>" + ds1.Tables[i].Rows[0]["DoanhThuGoiTuan"].ToString() + "</td>");
                }
                file.Write("</tr>");


                file.Write("\n");
                file.Write("<tr><td>3</td><td>Doanh thu gói tuần</td>");
                for (int i = 0; i < ds1.Tables.Count; i++)
                {
                    file.Write("<td>" + ds1.Tables[i].Rows[0]["DoanhThuGoiThang"].ToString() + "</td>");
                }
                file.Write("</tr>");


                file.Write("\n");
                file.Write("<tr><td>4</td><td>  Doanh thu gói tháng</td>");
                for (int i = 0; i < ds1.Tables.Count; i++)
                {
                    file.Write("<td >" + ds1.Tables[i].Rows[0]["DoanhThuGoiThang"].ToString() + "</td>");
                }
                file.Write("</tr>");

                file.Write("\n");
                file.Write("<tr><td>5</td><td colspan=" + ds1.Tables.Count + "'>Tổng doanh thu</td>");
                file.Write("<td>" + tong + "</td>");
                file.Write("</tr>");

                file.Write("</table>");
                file.Close();
                return true;
            }
            catch (Exception aa)
            {
                return false;
            }

        }

        public static bool print_as_excel_view(DataSet ds, String path, String name)
        {

            String url = path + "\\" + name;
            try
            {
                StreamWriter file = new StreamWriter(url, false, Encoding.Unicode);
                file.Write("<table>");
                file.Write("<tr>");              
                    file.Write("<th style=\"border-style:inset;font-weight:bold;background-color:yellow\">");
                    file.Write("Ngày");
                    file.Write("</th>");

                    file.Write("<th style=\"border-style:inset;font-weight:bold;background-color:yellow\">");
                    file.Write("Thời sự");
                    file.Write("</th>");

                    file.Write("<th style=\"border-style:inset;font-weight:bold;background-color:yellow\">");
                    file.Write("Giải trí");
                    file.Write("</th>");

                    file.Write("<th style=\"border-style:inset;font-weight:bold;background-color:yellow\">");
                    file.Write("Thể thao");
                    file.Write("</th>");

                    file.Write("<th style=\"border-style:inset;font-weight:bold;background-color:yellow\">");
                    file.Write("Âm nhạc");
                    file.Write("</th>");

                    file.Write("<th style=\"border-style:inset;font-weight:bold;background-color:yellow\">");
                    file.Write("Gia đình");
                    file.Write("</th>");

                    file.Write("<th style=\"border-style:inset;font-weight:bold;background-color:yellow\">");
                    file.Write("Phim");
                    file.Write("</th>");

                    file.Write("<th style=\"border-style:inset;font-weight:bold;background-color:yellow\">");
                    file.Write("Hài");
                    file.Write("</th>");

                    file.Write("<th style=\"border-style:inset;font-weight:bold;background-color:yellow\">");
                    file.Write("Thiếu nhi");
                    file.Write("</th>");

                    file.Write("<th style=\"border-style:inset;font-weight:bold;background-color:yellow\">");
                    file.Write("Game");
                    file.Write("</th>");

                    file.Write("<th style=\"border-style:inset;font-weight:bold;background-color:yellow\">");
                    file.Write("Độc lạ");
                    file.Write("</th>");

                    file.Write("<th style=\"border-style:inset;font-weight:bold;background-color:yellow\">");
                    file.Write("Nghe đi");
                    file.Write("</th>");
                file.Write("</tr>");
                file.Write("\n");
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    file.Write("<tr>");
                    for (int j = 0; j < ds.Tables[i].Columns.Count; j++)
                    {
                        file.Write("<td>" + ds.Tables[i].Rows[0][j].ToString() + "</td>");
                    }
                    file.Write("</tr>");
                    file.Write("\n");
                }
                file.Write("</table>");
                file.Close();
                return true;
            }
            catch (Exception aa)
            {
                return false;
            }

        }
    }
}
