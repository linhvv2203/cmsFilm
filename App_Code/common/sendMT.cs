﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.2407
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by wsdl, Version=1.1.4322.2407.
// 
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
public class sendMT : System.Web.Services.Protocols.SoapHttpClientProtocol {
    
    /// <remarks/>
    public sendMT() 
    {
        this.Url = libConst.mdAddress ;
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/InsertCP", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public string InsertCP(string CPCode, string RequestID, string UserID, string ReceiverID, string ServiceID, string CommandCode, string Content, string ContentType) {
        object[] results = this.Invoke("InsertCP", new object[] {
                    CPCode,
                    RequestID,
                    UserID,
                    ReceiverID,
                    ServiceID,
                    CommandCode,
                    Content,
                    ContentType});
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginInsertCP(string CPCode, string RequestID, string UserID, string ReceiverID, string ServiceID, string CommandCode, string Content, string ContentType, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("InsertCP", new object[] {
                    CPCode,
                    RequestID,
                    UserID,
                    ReceiverID,
                    ServiceID,
                    CommandCode,
                    Content,
                    ContentType}, callback, asyncState);
    }
    
    /// <remarks/>
    public string EndInsertCP(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/InsertFMO", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public string InsertFMO(string ContentType, string UserID, string ServiceID, string Content) 
	{
		object[] results = this.Invoke("InsertFMO", new object[] {
																	 ContentType,
																	 UserID,
																	 ServiceID,
																	 Content});
		return ((string)(results[0]));
	}
    
	/// <remarks/>
	public System.IAsyncResult BeginInsertFMO(string ContentType, string UserID, string ServiceID, string Content, System.AsyncCallback callback, object asyncState) 
	{
		return this.BeginInvoke("InsertFMO", new object[] {
															  ContentType,
															  UserID,
															  ServiceID,
															  Content}, callback, asyncState);
	}
    
	/// <remarks/>
	public string EndInsertFMO(System.IAsyncResult asyncResult) 
	{
		object[] results = this.EndInvoke(asyncResult);
		return ((string)(results[0]));
	}
}
