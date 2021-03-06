using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Collections;
using VatLid;

/// <summary>
/// Summary description for edit_Item_Utils
/// </summary>
public class edit_Item_Utils
{
    private string title_edit = "";
    private string title_add = "";
    private string title_width = "100";
    private string select = "";
    private string conn = "";
    private string table = "";
    private bool getsql = false;
    private bool _isDebug = false;

    private ArrayList listItems = null;

    public ArrayList ListItems
    {
        get { return listItems; }
        set { listItems = value; }
    }
    public string Select
    {
        get { return select; }
        set { select = value; }
    }
    public string Table
    {
        get { return table; }
        set { table = value; }
    }
    public bool GetSQL
    {
        get { return getsql; }
        set { getsql = value; }
    }
    public bool isDebug
    {
        get { return _isDebug; }
        set { _isDebug = value; }
    }
    public string ConnStr
    {
        get { return conn; }
        set { conn = value; }
    }
    public string TitleEdit
    {
        get { return title_edit; }
        set { title_edit = value; }  
    }
    public string TitleAdd
    {
        get { return title_add; }
        set { title_add = value; }
    }
    public string TitleWidth
    {
        get { return title_width; }
        set { title_width = value; }
    }

	public edit_Item_Utils()
	{

	}
    
 
    
    private void travelXML(XmlNode doc)
    {
        listItems = new ArrayList();
        foreach (XmlNode value in doc.SelectNodes(".//text"))
        {                                                 
            string type = value.Attributes["type"].Value;     
             
            edit_Item_TextInput textinput = new edit_Item_TextInput();
            textinput.ID = value.Attributes["id"].Value;
            textinput.Label = value.Attributes["label"].Value;
            textinput.Width = value.Attributes["width"].Value;
            textinput.Field = value.SelectSingleNode(".//field").InnerText;
            textinput.FieldType = value.SelectSingleNode(".//fieldtype").InnerText;
            textinput.Validate = value.SelectSingleNode(".//validate").InnerText;
            textinput.ErrorNoInput = getChild(value, "error_no_input", "");  
            if (type == "2") // text area
            {
                textinput.Type = Edit_Item_Type.TextArea;
                textinput.Height = value.SelectSingleNode(".//height").InnerText;
            }
            else
                textinput.Type = Edit_Item_Type.TextBox;

            listItems.Add(textinput);    
        }

        //foreach (XmlNode value in doc.SelectNodes(".//check"))
        //{
        //    string type = value.Attributes["type"].Value;

        //    edit_Item_CheckBox textinput = new edit_Item_CheckBox();
        //    textinput.ID = value.Attributes["id"].Value;
        //    textinput.Label = value.Attributes["label"].Value;
        //    textinput.Width = value.Attributes["width"].Value;
        //    textinput.Field = value.SelectSingleNode(".//field").InnerText;
        //    textinput.FieldType = value.SelectSingleNode(".//fieldtype").InnerText;
        //    textinput.ErrorNoInput = getChild(value, "error_no_input", "");
           
        //    textinput.Type = Edit_Item_Type.TextBox;

        //    listItems.Add(textinput);
        //}


        foreach (XmlNode value in doc.SelectNodes(".//file"))
        {
            edit_Item_FileInput fileinput = new edit_Item_FileInput();
            fileinput.ID = value.Attributes["id"].Value;
            fileinput.Label = value.Attributes["label"].Value;
            fileinput.Width = value.Attributes["width"].Value;
            fileinput.Field = value.SelectSingleNode(".//field").InnerText;
            fileinput.FieldType = value.SelectSingleNode(".//fieldtype").InnerText;
            fileinput.Extention = value.SelectSingleNode(".//extension").InnerText;
            fileinput.StorePath = ConfigurationSettings.AppSettings[value.SelectSingleNode(".//storepath").InnerText];            
            fileinput.MaxSize = Int32.Parse(getChild(value, "maxsize", "10204000"));
            fileinput.ErrorNoInput = getChild(value, "error_no_input", "");
            fileinput.CurrentValue = "";

            listItems.Add(fileinput);
        }
        foreach (XmlNode value in doc.SelectNodes(".//filter"))
        {
            edit_Item_Dropdown_Chooser filter = getFilter(value);
            listItems.Add(filter);

            XmlNode n = value.SelectSingleNode(".//filter_sub");
            if (n != null)
            {
                edit_Item_Dropdown_Chooser sub = getFilter(n);
                sub.Type = Edit_Item_Type.Dropdown_Chooser_2;
                filter.Child = sub;
                listItems.Add(sub);
            }                  
        }
        foreach (XmlNode value in doc.SelectNodes(".//action"))
        {
            string type = value.Attributes["type"].Value;         
            
            edit_Item_ButtonAction action = new edit_Item_ButtonAction();
            action.ConnStr = getChild(value,"conn","ConnStr1");
            action.ConnStr = SecureConnection.GetCnxString(action.ConnStr);
            action.ID = value.Attributes["id"].Value;
            action.Label = value.Attributes["label"].Value;
            if (type == "1")
                action.Type = Edit_Item_Type.AddAction;
            else
                action.Type = Edit_Item_Type.EditAction;
            action.Procedure = value.SelectSingleNode(".//procedure").InnerText;
            action.BackLink = getChild(value,"backlink","");

            listItems.Add(action);    
             
        }
    }
    private string getChild(XmlNode node, string child, string defaultValue)
    { 
         return node.SelectSingleNode(".//" + child) == null ? defaultValue :  node.SelectSingleNode(".//" + child).InnerText;
    }
    private edit_Item_Dropdown_Chooser getFilter(XmlNode node)
    {                    
        edit_Item_Dropdown_Chooser filter = new edit_Item_Dropdown_Chooser();
        filter.ConnStr = getChild(node, "conn", "ConnStr1");
        filter.ConnStr = SecureConnection.GetCnxString(filter.ConnStr);
        filter.ID = node.Attributes["id"].Value;
        filter.Label = node.Attributes["label"].Value;
        filter.Width = node.Attributes["width"].Value;
        filter.Type = Edit_Item_Type.Dropdown_Chooser;
        filter.Field = node.SelectSingleNode(".//field").InnerText;
        filter.FieldType = node.SelectSingleNode(".//fieldtype").InnerText;
        filter.FieldList = node.SelectSingleNode(".//fieldlist").InnerText;
        filter.FilterField = node.SelectSingleNode(".//filterField").InnerText;
        filter.SQL = node.SelectSingleNode(".//sql").InnerText;
        filter.ErrorNoInput = getChild(node, "error_no_input", ""); 
        return filter;
    }
}
