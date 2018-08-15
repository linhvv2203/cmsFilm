using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using VatLid;

public partial class SYS_Useralert_Data : System.Web.UI.Page
{
    protected string sID = "";
    protected string TYPE = "";
    protected string SQL;

    protected void Page_Load(object sender, EventArgs e)
    {
        sID = Request.QueryString["id"];
        if (Utils.IsNumeric(sID))
        {
            sID = sID;
        }
        else
        {
            sID = "0";
        }

        TYPE = Request.QueryString["type"];
     

        string P_I = Request.ServerVariables["PATH_INFO"];
        string[] aPI = P_I.Split('/');
        int iLength = aPI.Length;
        string FileName = aPI[iLength - 1];

        if (Session["USER"] != null)
        {
            if (DAL.GetRights(Session["USER"].ToString(), FileName) == false)
            {
                Response.Redirect(VatLid.Variables.sWebRoot + "error_info.aspx?err=21");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    
                    switch (TYPE)
                    {
                        //case "delete":
                      
                        //    break;
                        case "edit":
                            BindData();
                            break;
                        default:
                            Label1.Text = "Thông tin";
                            break;
                    }
                }
            }
        }
        else
            Response.Redirect(VatLid.Variables.sWebRoot + "logout.aspx");
    }
    private void BindData()
    {
        SQL = "SELECT ID,UserName FROM Users WHERE ID=" + sID;
        try
        {
            ArrayList al = DAL.GetDataReaderToArrayList(SQL);
            string[][] arrReturn = (string[][])al.ToArray(typeof(string[]));
            if (al.Count > 0)
            {
                txtUserName.Text = arrReturn[0][1].ToString();
            }
            else
            {
                txtUserName.Text = "ALL";
            }
           

        }
        catch (Exception e)
        {
            VatLid.DAL.ExceptionProcess(e);
        }

    }
   
    protected void cmdSave_Click(object sender, System.EventArgs e)
    {

        SQL = "INSERT INTO User_alert(UserID,AlertContent) VALUES(N'" + Utils.safeString(txtUserName.Text);
        SQL += "',N'" + txtContent.Html.Trim().Replace("'", "''");
        SQL += "')";
        VatLid.DAL.ExecuteQuery(SQL);
        Response.Redirect(VatLid.Variables.sWebRoot + "SYS/User_List.aspx");
    }



}
