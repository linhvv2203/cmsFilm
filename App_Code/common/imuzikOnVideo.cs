using System.Diagnostics;
using System.Web.Services;
using System.ComponentModel;
using System.Web.Services.Protocols;
using System;
using System.Xml.Serialization;


namespace VatLidOnPhim
{

    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="dlSoap", Namespace="http://tempuri.org/")]
    public partial class imuzik : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback DownloadOperationCompleted;
        
        private System.Threading.SendOrPostCallback PresentGiftOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public imuzik() 
        {
            this.Url = "http://cms.imuzik.com.vn/dl.asmx";
            
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event DownloadCompletedEventHandler DownloadCompleted;
        
        /// <remarks/>
        public event PresentGiftCompletedEventHandler PresentGiftCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Download", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Download(string UserId, string Code) {
            object[] results = this.Invoke("Download", new object[] {
                        UserId,
                        Code});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void DownloadAsync(string UserId, string Code) {
            this.DownloadAsync(UserId, Code, null);
        }
        
        /// <remarks/>
        public void DownloadAsync(string UserId, string Code, object userState) {
            if ((this.DownloadOperationCompleted == null)) {
                this.DownloadOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDownloadOperationCompleted);
            }
            this.InvokeAsync("Download", new object[] {
                        UserId,
                        Code}, this.DownloadOperationCompleted, userState);
        }
        
        private void OnDownloadOperationCompleted(object arg) {
            if ((this.DownloadCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DownloadCompleted(this, new DownloadCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/PresentGift", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string PresentGift(string UserId, string RecieverId, string Code) {
            object[] results = this.Invoke("PresentGift", new object[] {
                        UserId,
                        RecieverId,
                        Code});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void PresentGiftAsync(string UserId, string RecieverId, string Code) {
            this.PresentGiftAsync(UserId, RecieverId, Code, null);
        }
        
        /// <remarks/>
        public void PresentGiftAsync(string UserId, string RecieverId, string Code, object userState) {
            if ((this.PresentGiftOperationCompleted == null)) {
                this.PresentGiftOperationCompleted = new System.Threading.SendOrPostCallback(this.OnPresentGiftOperationCompleted);
            }
            this.InvokeAsync("PresentGift", new object[] {
                        UserId,
                        RecieverId,
                        Code}, this.PresentGiftOperationCompleted, userState);
        }
        
        private void OnPresentGiftOperationCompleted(object arg) {
            if ((this.PresentGiftCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.PresentGiftCompleted(this, new PresentGiftCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void DownloadCompletedEventHandler(object sender, DownloadCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DownloadCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DownloadCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void PresentGiftCompletedEventHandler(object sender, PresentGiftCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class PresentGiftCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal PresentGiftCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}
