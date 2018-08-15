using System.Diagnostics;
using System.Xml.Serialization;
using System;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Web.Services;

namespace VatLid
{



    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "getContentSoap", Namespace = "http://tempuri.org/")]
    public class getContent : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        /// <remarks/>
        public getContent()
        {
            this.Url = "http://192.168.228.99:7777/getContent.asmx";
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/getTextMessages", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string getTextMessages(string ServiceID, string CommandCode, string Info)
        {
            object[] results = this.Invoke("getTextMessages", new object[] {
                        ServiceID,
                        CommandCode,
                        Info});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegingetTextMessages(string ServiceID, string CommandCode, string Info, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("getTextMessages", new object[] {
                        ServiceID,
                        CommandCode,
                        Info}, callback, asyncState);
        }

        /// <remarks/>
        public string EndgetTextMessages(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/getDownloadMessages", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string getDownloadMessages(string ServiceID, string CommandCode, string Info, string UserID)
        {
            object[] results = this.Invoke("getDownloadMessages", new object[] {
                        ServiceID,
                        CommandCode,
                        Info,
                        UserID});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegingetDownloadMessages(string ServiceID, string CommandCode, string Info, string UserID, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("getDownloadMessages", new object[] {
                        ServiceID,
                        CommandCode,
                        Info,
                        UserID}, callback, asyncState);
        }

        /// <remarks/>
        public string EndgetDownloadMessages(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
    }
}
