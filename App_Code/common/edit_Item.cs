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

public enum Edit_Item_Type
{ 
    Dropdown_Chooser = 1,
    Dropdown_Chooser_2 = 2,
    TextBox = 3,
    TextArea = 4,
    FileInput = 5,
    EditAction = 6,
    AddAction = 7,
    CheckBox = 8
}          
public class edit_Item
{
    private string _id;
    private string _label;
    private string _width;
    private string _field;
    private string _fieldType;
    private string _error_no_input;
    private string _connStr; 
    private Edit_Item_Type _tyle;
    private edit_Item _child;  

    public edit_Item()
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
    public Edit_Item_Type Type
    {
        get { return _tyle; }
        set { this._tyle = value; }
    }
    public string Field
    {
        get { return _field; }
        set { this._field = value; }
    }
    public string Width
    {
        get { return _width; }
        set { this._width = value; }
    }
    public string FieldType
    {
        get { return _fieldType; }
        set { this._fieldType = value; }
    }
    public string ErrorNoInput
    {
        get { return _error_no_input; }
        set { this._error_no_input = value; }
    }
    public string ConnStr
    {
        get { return _connStr; }
        set { this._connStr = value; }
    }
    public edit_Item Child
    {
        get { return _child; }
        set { this._child = value; }
    }                                    
}

public class edit_Item_TextInput : edit_Item
{
    private string validate;
    private string height;
    private TextBox textBox;
    public TextBox TextBox
    {
        get { return textBox; }
        set { this.textBox = value; }
    }
    public string Validate
    {
        get { return validate; }
        set { this.validate = value; }
    }
    public string Height
    {
        get { return height; }
        set { this.height = value; }
    }
}

public class edit_Item_CheckBox : edit_Item
{  
    private CheckBox checkBox;
    public CheckBox Check
    {
        get { return checkBox; }
        set { this.checkBox = value; }
    }
    public bool Checked
    {
        get { return checkBox.Checked; }
        set { this.checkBox.Checked = value; }
    }
}

public class edit_Item_FileInput : edit_Item
{
    private int maxsize = 10204000;
    private string storepath;
    private string extention;
    private FileUpload fileUpdate;
    private string currentValue = "";


    public FileUpload FileUpload
    {
        get { return fileUpdate; }
        set { this.fileUpdate = value; }
    }
    public string Extention
    {
        get { return extention; }
        set { this.extention = value; }
    }
    public string StorePath
    {
        get { return storepath; }
        set { this.storepath = value; }
    }
    public string CurrentValue
    {
        get { return currentValue; }
        set { this.currentValue = value; }
    }
    public int MaxSize
    {
        get { return maxsize; }
        set { this.maxsize = value; }
    }
}

public class edit_Item_Dropdown_Chooser : edit_Item
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

public class edit_Item_ButtonAction : edit_Item
{
    private string _actionType;
    private string procedure;
    private Button _button;
    private string backlink = "";

    public Button Button
    {
        get { return _button; }
        set { this._button = value; }
    }
    public string ActionType
    {
        get { return _actionType; }
        set { this._actionType = value; }
    }
    public string Procedure
    {
        get { return procedure; }
        set { this.procedure = value; }
    }
    public string BackLink
    {
        get { return backlink; }
        set { this.backlink = value; }
    }
}

