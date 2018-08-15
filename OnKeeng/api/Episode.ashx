<%@ WebHandler Language="C#" Class="ShareAction" %>

using System;
using System.Web;

public class ShareAction : IHttpHandler, System.Web.SessionState.IRequiresSessionState 
{
    protected string token = "";
    protected string Episode = "";
    protected string ID = "";

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            Episode = context.Request.Params[0].ToString();
            ID = context.Request.Params[1].ToString();
            //token = context.Request.Params[2].ToString();

            //if (token.Length > 10 && token == System.Web.HttpContext.Current.Session["TokenEpisode"].ToString())
            //{
                if (Episode.Length > 0)
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(VatLidOnPhim.DAL.EpisodeAction_OnKeeng(ID, Episode));
                }
                else
                    context.Response.Write("404");
            //}
            //else
            //    context.Response.Write("403");
        }
        catch
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("");
        }
    }

    #region IHttpHandler Members

    public bool IsReusable
    {
        get { throw new NotImplementedException(); }
    }

    #endregion
}