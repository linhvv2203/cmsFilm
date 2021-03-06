using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using Microsoft.Office.Core;
using System.Reflection;
using Excel;
using System.IO;
using System.Text;
/// <summary>
/// Summary description for CreateExcelDoc
/// </summary>
/// 
namespace VatLidOnPhim
{
    public class CreateExcelDoc
    {        
        System.IO.StreamWriter excelDoc = null;
        int numColumns;
        private string firstLineStyle = @"style='font-weight: bold; text-align: center; font-size: 18;font-family: Times New Roman; font-style: italic; background:yellow'";
        private string headerStyle = @"style='font-weight: nomal; text-align: left; font-size: 14;font-family: Times New Roman; font-style: italic; background:white'";
        private string columnStyle = @"style='font-weight: bold; text-align: middle; font-size: 14; font-family: Times New Roman;background:#cccccc'";
        private string cellStyle = @"style='text-align: middle; font-size: 12; font-family: Times New Roman'";
        public CreateExcelDoc()
        {
            
        }
        public void createHeaders(string header)
        {
            excelDoc.WriteLine("<tr>");
            excelDoc.WriteLine("<td align='center' colspan='" + numColumns + "' " + headerStyle + ">");
            excelDoc.WriteLine(header);
            excelDoc.WriteLine("</td></tr>");
        }

        public void fillData(string filename, System.Data.DataTable dt, string[] headers,string timeReport, string creater, string[] columns)
        {
            String url = filename;            
            try
            {
                excelDoc = new StreamWriter(url, false, Encoding.Unicode);
            }
            catch{}
            if (excelDoc == null)
                throw new Exception("Không tạo được file " + url);
            //Chinh sua hom 25/02/2010
            //numColumns = dt.Columns.Count;
            numColumns = columns.Length;

            string xml = @"<table cellpadding='3' width='100%' border='1' bordercolor='#cccccc'>";
            excelDoc.WriteLine(xml); 
            // headers
            foreach(string header in headers)
            {
                excelDoc.WriteLine("<tr>");
                excelDoc.WriteLine("<td align='center' colspan='" + numColumns + "' " + firstLineStyle + ">");
                excelDoc.WriteLine(header);
                excelDoc.WriteLine("</td></tr>");
            }

            createHeaders("Thời gian báo cáo: <b>" + timeReport + "</b>");
            createHeaders("Người lập báo cáo: <b>" + creater + "</b>");
            createHeaders("Thời gian lập báo cáo: <b>" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "</b>"); 
                      
            // columns
            excelDoc.WriteLine("<tr>");
            foreach (string column in columns)
            {                
                excelDoc.WriteLine("<td " + columnStyle + ">");
                excelDoc.WriteLine(column);
                excelDoc.WriteLine("</td>");
            }
            excelDoc.WriteLine("</tr>");

            // datas
            
            foreach (DataRow row in dt.Rows)
            {
                excelDoc.WriteLine("<tr>");
                for(int i=0; i < numColumns; i++)
                {
                    excelDoc.WriteLine("<td " + cellStyle + ">");
                    excelDoc.WriteLine(row[i].ToString());
                    excelDoc.WriteLine("</td>");
                }
                excelDoc.WriteLine("</tr>");
            }            
            excelDoc.WriteLine("</table>");
            excelDoc.Close();
        }

        //Create by doan
       public void fillDataExcell(string filename, System.Data.DataTable dt, string[] headers,string timeReport, string creater, string[] columns)
        {
            String url = filename;            
            try
            {
                excelDoc = new StreamWriter(url, false, Encoding.Unicode);
            }
            catch{}
            if (excelDoc == null)
                throw new Exception("Không tạo được file " + url);
            //Chinh sua hom 25/02/2010
            //numColumns = dt.Columns.Count;
            numColumns = columns.Length;

            string xml = @"<table cellpadding='3' width='100%' border='1' bordercolor='#cccccc'>";
            excelDoc.WriteLine(xml); 
            // headers
            foreach(string header in headers)
            {
                excelDoc.WriteLine("<tr>");
                excelDoc.WriteLine("<td align='center' colspan='" + numColumns + "' " + firstLineStyle + ">");
                excelDoc.WriteLine(header);
                excelDoc.WriteLine("</td></tr>");
            }

            createHeaders("Thời gian báo cáo: <b>" + timeReport + "</b>");
            createHeaders("Người lập báo cáo: <b>" + creater + "</b>");
            createHeaders("Thời gian lập báo cáo: <b>" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "</b>"); 
                      
            // columns
            excelDoc.WriteLine("<tr>");
            foreach (string column in columns)
            {                
                excelDoc.WriteLine("<td " + columnStyle + ">");
                excelDoc.WriteLine(column);
                excelDoc.WriteLine("</td>");
            }
            excelDoc.WriteLine("</tr>");

            // datas
            
            foreach (DataRow row in dt.Rows)
            {
                excelDoc.WriteLine("<tr>");
                for(int i=0; i < numColumns; i++)
                {
                    excelDoc.WriteLine("<td " + cellStyle + ">");
                    excelDoc.WriteLine(row[i].ToString().Replace("<br>", "\n"));
                    excelDoc.WriteLine("</td>");
                }
                excelDoc.WriteLine("</tr>");
            }            
            excelDoc.WriteLine("</table>");
            excelDoc.Close();
        }

       


    }

     
   // }

}