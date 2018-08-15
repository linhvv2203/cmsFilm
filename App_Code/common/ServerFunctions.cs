using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;


namespace VatLid
{
	/// <summary>
	/// Summary description for ServerFunctions.
	/// </summary>
	//
	public class VAT
	{
		static public void loadComboBox(DropDownList myComboBox, DataSet myDataSet, string strDataField, string strDataValue)
		{
			myComboBox.Items.Clear();
			myComboBox.DataSource = myDataSet;
			myComboBox.DataTextField = strDataField;
			myComboBox.DataValueField = strDataValue;
			myComboBox.DataBind();
		}
		//txtTenBoPhan.Text = myDataRow["TenBoPhan"].ToString() ;

		public string ConvertDate(string NgayChuyen)
		{
			string strNgayChuyen;
			strNgayChuyen=Convert.ToDateTime(NgayChuyen).ToString("dd/MM/yyyy");
			if (strNgayChuyen=="01/01/1998")
				strNgayChuyen="Chưa biết";
			return strNgayChuyen;
		}
		static public bool IsValidEmail(string email)
		{
			return Regex.IsMatch(email,@"^.+\@(\[?)[a-zA-Z0-9\-\.]+\.([a-zA-Z]{2,3}|[0-9]{1,3})(\]?)$");
		}
		static public bool IsValidURL(string url)
		{
			return Regex.IsMatch(url,@"^(http|https|ftp)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&%\$#\=~])*[^\.\,\)\(\s]$");
		}
		static public bool IsValidInt(string val)
		{
			return Regex.IsMatch(val,@"^[1-9]\d*\.?[0]*$");
		}
		static private bool IsValidTag(string tag,string[] AllowedTags) 
		{
			if (tag.IndexOf("javascript") >= 0) return false;
			if (tag.IndexOf("vbscript") >= 0) return false;
			if (tag.IndexOf("onclick") >= 0)	return false;

			char[] endchars = new char[]{' ','>','/','\t'};
			
			int pos = tag.IndexOfAny(endchars,1);
			if (pos > 0) tag = tag.Substring(0,pos);
			if (tag[0] == '/') tag = tag.Substring(1);

			// check if it's a valid tag
			foreach (string aTag in AllowedTags)
			{
				if (tag == aTag) return true;
			}

			return false;
		}
		
		static public string SafeHtml(string html) 
		{
			html = html.Replace("<","&lt;");
			html = html.Replace(">","&gt;");
			return html;
		}

		static private string FixCode(string html) 
		{
			html = html.Replace("  ","&nbsp; ");
			html = html.Replace("  "," &nbsp;");
			html = html.Replace("\t","&nbsp; &nbsp;&nbsp;");
			html = html.Replace("[","&#91;");
			html = html.Replace("]","&#93;");
			html = html.Replace("<","&lt;");
			html = html.Replace(">","&gt;");
			html = html.Replace("\r\n","<br/>");
			return html;
		}
		public bool IsNumeric(object source, string value) 
		{
			try
			{
				Decimal temp=Convert.ToDecimal(value);
				return true;
			}
			catch 
			{
				return false;
			}
		}
		public static string GetValue(DataRow row, string field) 
		{
			if (row[field].ToString() == null)
				return "";
			else
				return row[field].ToString();
		}
		
		public void buildListBox(ListItemCollection Items,string[] values, string CustomInitialDisplayValue,string CustomInitialSubmitValue)
		{	
			Items.Clear();
			if(CustomInitialDisplayValue!=null) Items.Add(new ListItem(CustomInitialDisplayValue,CustomInitialSubmitValue));
			for(int i=0;i<values.Length;i+=2)Items.Add(new ListItem(values[i+1],values[i]));
		}
		
		
	}
}
