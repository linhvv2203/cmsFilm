using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using VatLid;

public partial class Left : System.Web.UI.Page
{
    protected string SQL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["USER"] != null)
            {
                BindData("0");
            }
         }
        catch (Exception ex)
        {
            VatLid.DAL.ExceptionProcess(ex);
            Response.Redirect(VatLid.Variables.sWebRoot + "logout.aspx");
        }
    }
    private void BindDataAll()
    {
        SQL = "select ID,ParentID,CategoryForder,CategoryName,CategoryImage,CategoryLink from CategoriesMenu where ParentID=0 ";
        SQL = SQL + " and CategoryStatus=2 order by CategoryOrder desc";
        DataView view = VatLid.DAL.CreateDataView(SQL);
        rpCenterMenu.DataSource = view.Table;
        rpCenterMenu.DataBind();
    }
  
    private void BindData(String parentid)
    {
        if (Session["USERGROUPID"].ToString().ToUpper() == "1")
        {
            SQL = "select ID,ParentID,CategoryForder,CategoryName,CategoryImage,CategoryLink from CategoriesMenu where ParentID=0 ";
            SQL = SQL + " and CategoryStatus=2 order by CategoryOrder desc";

        }
        else
        {

            SQL = "select ID,ParentID,CategoryForder,CategoryName,CategoryImage,CategoryLink from CategoriesMenu where ParentID=0 and ID in (" + "select MenuID from RightsMenu where RightsStatus=1 AND UserID=" + Utils.safeString(Session["USERID"].ToString()) + ")";
            SQL = SQL + " and CategoryStatus=2 order by CategoryOrder desc";
        }


        DataSet ds = DAL.GetMenuLeft(Convert.ToInt32(Session["USERGROUPID"].ToString()), Convert.ToInt32(Session["USERID"].ToString()));

        DataView view = ds.Tables[0].DefaultView;// VatLid.DAL.CreateDataView(SQL);
        rpCenterMenu.DataSource = view.Table;
        rpCenterMenu.DataBind();
    }
    protected void rpCenterMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem item = e.Item;

        if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)item.DataItem;
            String catid = drv.Row[0].ToString();

            Repeater rpSubMenu = (Repeater)item.FindControl("rpSubMenu");

            if (Session["USERGROUPID"].ToString().ToUpper() == "1")
            {
                SQL = "select ID,ParentID,CategoryForder,CategoryName,CategoryImage,CategoryLink from CategoriesMenu where  Parentid=" + catid + " and CategoryStatus=2 order by CategoryOrder desc";

            }
            else
            {
                SQL = "select ID,ParentID,CategoryForder,CategoryName,CategoryImage,CategoryLink from CategoriesMenu where  Parentid=" + catid + " and ID in (" + "select MenuID from RightsMenu where RightsStatus=1 AND UserID=" + Utils.safeString(Session["USERID"].ToString()) + ")";
                SQL = SQL + " and CategoryStatus=2 order by CategoryOrder desc";
            }

            DataSet ds = DAL.GetMenuChildLeft(Convert.ToInt32(Session["USERGROUPID"].ToString()), Convert.ToInt32(Session["USERID"].ToString()), Convert.ToInt32(catid));


            //DataView view = VatLid.DAL.CreateDataView(SQL);
            rpSubMenu.DataSource = ds.Tables[0];// view.Table;
            rpSubMenu.DataBind();
        }
    }
}
