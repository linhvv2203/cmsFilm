using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for view_Item
/// </summary>


public class view_Item
{
    private string _id;
    private string _label;
    private string _tyle;
    private view_Item _child;
    private string _connStr;
	public view_Item()
	{

	}
    public string ID
    {
        get{ return _id;}
        set { this._id = value; }
    }
    public string Label
    {
        get { return _label; }
        set { this._label = value; }
    }
    public string Type
    {
        get { return _tyle; }
        set { this._tyle = value; }
    }
    public string ConnStr
    {
        get { return _connStr; }
        set { this._connStr = value; }
    }
    public view_Item Child
    {
        get { return _child; }
        set { this._child = value; }
    }                                    
}
public class view_Column
{
    private string _field;
    private string _header;

    public string Field
    {
        get { return _field; }
        set { this._field = value; }
    }
    public string Header
    {
        get { return _header; }
        set { this._header = value; }
    }

}
public class view_Item_Dropdown_Filter : view_Item
{
    private string _sql;
    private string _fieldList;
    private string _filterField;
    private DropDownList _ddl;

    public string FilterField
    {
        get { return _filterField; }
        set { this._filterField = value; }
    }
    public string SQL
    {
        get { return _sql; }
        set { this._sql = value; }
    }
    public string FieldList
    {
        get { return _fieldList; }
        set { this._fieldList = value; }
    }
    public DropDownList DDL
    {
        get { return _ddl; }
        set { this._ddl = value; }
    }                    
}

public class view_Item_DateSearch : view_Item
{
    private string _labelStartDate;
    private string _labelEndDate;
    private int _initDateStart; // = -1 -> set init date = yesterday
    private int _initDateEnd; // = -1 -> set init date = yesterday
    private string _filterField;
    private TextBox _txtStart;
    private TextBox _txtEnd;
    private CheckBox check;
    public CheckBox CheckBox
    {
        get { return check; }
        set { this.check = value; }
    }
    public int InitDateStart
    {
        get { return _initDateStart; }
        set { this._initDateStart = value; }
    }
    public int InitDateEnd
    {
        get { return _initDateEnd; }
        set { this._initDateEnd = value; }
    }
    public string FilterField
    {
        get { return _filterField; }
        set { this._filterField = value; }
    }
    public TextBox TextBoxStart
    {
        get { return _txtStart; }
        set { this._txtStart = value; }
    }
    public TextBox TextBoxEnd
    {
        get { return _txtEnd; }
        set { this._txtEnd = value; }
    }
    public string LabelEndDate
    {
        get { return _labelEndDate; }
        set { this._labelEndDate = value; }
    }
    public string LabelStartDate
    {
        get { return _labelStartDate; }
        set { this._labelStartDate = value; }
    }
}

public class view_Item_Search : view_Item_Dropdown_Filter
{
    private string _labelsearch;
    private string _labelbutton;
    private string _values;
    private DropDownList _ddl;
    private TextBox _txtKeyword;
    public DropDownList DDL
    {
        get { return _ddl; }
        set { this._ddl = value; }
    }
    public TextBox TxtKeyword
    {
        get { return _txtKeyword; }
        set { this._txtKeyword = value; }
    }
    public string LabelSearch
    {
        get { return _labelsearch; }
        set { this._labelsearch = value; }
    }
    public string LabelButton
    {
        get { return _labelbutton; }
        set { this._labelbutton = value; }
    }
    public string Values
    {
        get { return _values; }
        set { this._values = value; }
    }
}


public class view_Item_ButtonAction : view_Item
{
    private string _alert;
    private string _error_mes ;
    private string _sql;
    private Button _button;

    public Button Button
    {
        get { return _button; }
        set { this._button = value; }
    }
    
    public string Error_Mes
    {
        get { return _error_mes; }
        set { this._error_mes = value; }
    }

    public string Alert
    {
        get { return _alert; }
        set { this._alert = value; }
    }

    public string SQL
    {
        get { return _sql; }
        set { this._sql = value; }
    }
}

public class dgrConfig
{
    // example: 1,2,3 -> column 1,2,3 se co row tong o cuoi cung
    private string row_sum = "";
    public string Row_Sum
    {
        get { return row_sum; }
        set { this.row_sum = value; }
    }
}