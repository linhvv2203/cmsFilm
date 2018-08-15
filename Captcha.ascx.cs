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
using MSCaptcha;

public partial class Detail_Captcha : System.Web.UI.UserControl
{    
    private bool _userValidated;
    private int _textLength;
    private CaptchaImage.LineNoiseLevel _captchaLineNoise;
    public bool UserValidated
    {
        get
        {
            return this._userValidated;
        }
    }
    
    public CaptchaImage.LineNoiseLevel CaptchaLineNoise
    {
        get
        {
            return this._captchaLineNoise;
        }
        set
        {
            this._captchaLineNoise = value;
        }
    }

    public int CaptchaLength
    {
        get
        {
            return this._textLength;
        }
        set
        {
            this._textLength = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //this.capcha.CaptchaLineNoise = this._captchaLineNoise;
        //this.capcha.CaptchaLength = this._textLength;
                
        //if (Session["ramdomStr"] != null)
        //{
        //    HttpCookie aCookie = new HttpCookie("randomStr");
        //    aCookie.Domain = ".dailyinfo.vn";
        //    aCookie.Values["value"] = Session["randomStr"].ToString();
        //    Response.Cookies.Add(aCookie);
        //}
    }
    public void ValidateCaptcha(string textvalidate)
    {
        try
        {
            if (Request.Cookies["randomStr"]["value"].ToString().ToLower() == textvalidate.ToLower())
            //if(Session["randomStr"].ToString() == textvalidate)
                _userValidated = true;
            else
                _userValidated = false;
        }
        catch
        {
            _userValidated = false;
        }
    }
     
}
