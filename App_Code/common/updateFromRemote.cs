﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.2379
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 1.1.4322.2379.
// 
namespace VatLid {
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Web.Services;
    
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="sendMTSoap", Namespace="http://tempuri.org/")]
    public class updateWS : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        /// <remarks/>
        public updateWS() {
            this.Url = "http://viettelvas.vn:7778/sendMT.asmx";
			this.Credentials = new System.Net.NetworkCredential("sms","sms123#$");
        }       
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/InsertUMT", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string InsertUMT(string UserID, string ServiceID, string Content) {
            object[] results = this.Invoke("InsertUMT", new object[] {
                        UserID,
                        ServiceID,
                        Content});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginInsertUMT(string UserID, string ServiceID, string Content, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("InsertUMT", new object[] {
                        UserID,
                        ServiceID,
                        Content}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndInsertUMT(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
    }
}
