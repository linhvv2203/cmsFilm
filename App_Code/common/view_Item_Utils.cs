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
/// Summary description for view_Item_Utils
/// </summary>
public class view_Item_Utils
{
    private string title = "";
    private string conn = "";
    private bool _isDebug = false;
    private string tableFilter = "";
    private ArrayList listColumns = null;
    private ArrayList listItems = null;
    private dgrConfig dgrConfiguration;
    public string Title
    {
        get { return title; }
        set { title = value; }
    }
    public string TableFilter
    {
        get { return tableFilter; }
        set { tableFilter = value; }
    }
    public bool isDebug
    {
        get { return _isDebug; }
        set { _isDebug = value; }
    }
    public string Conn
    {
        get { return conn; }
        set { conn = value; }
    }
    public dgrConfig DataGridConfig
    {
        get { return dgrConfiguration; }
        set { dgrConfiguration = value; }
    }

    public ArrayList ListItems
    {
        get { return listItems; }
        set { listItems = value; }
    }
    public ArrayList ListColumns
    {
        get { return listColumns; }
        set { listColumns = value; }
    }

	public view_Item_Utils()
	{

	}                          

   
    private string getChild(XmlNode node, string child, string defaultValue)
    {
        return node.SelectSingleNode(".//" + child) == null ? defaultValue : node.SelectSingleNode(".//" + child).InnerText;
    }                                                                          

    private view_Item_Dropdown_Filter getFilter(XmlNode node)
    {
        string id = node.Attributes["id"].Value;//.SelectSingleNode(".//id").InnerText;
        string label = node.Attributes["label"].Value;//.SelectSingleNode(".//label").InnerText;
        string type = node.Attributes["type"].Value;//.SelectSingleNode(".//type").InnerText;

        view_Item_Dropdown_Filter filter = new view_Item_Dropdown_Filter();
        filter.ConnStr = node.SelectSingleNode(".//conn") == null ? VatLid.DAL.getConnectionString1() : node.SelectSingleNode(".//conn").InnerText;
        filter.ConnStr = SecureConnection.GetCnxString(filter.ConnStr);
        filter.ID = id;
        filter.Label = label;
        filter.Type = type;
        if (type != "3") //<!-- filter, type = 3: Filter info load from list of different kind of filter. Ex: {Canceled,IsPause=1};{Free,IsPause=0 AND NextCharge>0};{==All status==,} -->
        {
            filter.FieldList = node.SelectSingleNode(".//fieldlist").InnerText;
            filter.FilterField = node.SelectSingleNode(".//filterField").InnerText;
        }
        filter.SQL = node.SelectSingleNode(".//sql").InnerText;
        return filter;
    }
}
