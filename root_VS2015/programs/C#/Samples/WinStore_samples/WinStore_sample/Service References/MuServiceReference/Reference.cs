﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.18033
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.VisualStudio.ServiceReference.Platforms, version 11.0.50727.1
// 
namespace WinStore_sample.MuServiceReference {
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="ArrayOfString", Namespace="http://tempuri.org/", ItemName="string")]
    public class ArrayOfString : System.Collections.ObjectModel.ObservableCollection<string> {
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MuServiceReference.ServiceForMuSoap")]
    public interface ServiceForMuSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Call", ReplyAction="*")]
        System.Threading.Tasks.Task<WinStore_sample.MuServiceReference.CallResponse> CallAsync(WinStore_sample.MuServiceReference.CallRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CallRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Call", Namespace="http://tempuri.org/", Order=0)]
        public WinStore_sample.MuServiceReference.CallRequestBody Body;
        
        public CallRequest() {
        }
        
        public CallRequest(WinStore_sample.MuServiceReference.CallRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class CallRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string context;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string serviceName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public WinStore_sample.MuServiceReference.ArrayOfString cmnParameterValue;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string parameterValue;
        
        public CallRequestBody() {
        }
        
        public CallRequestBody(string context, string serviceName, WinStore_sample.MuServiceReference.ArrayOfString cmnParameterValue, string parameterValue) {
            this.context = context;
            this.serviceName = serviceName;
            this.cmnParameterValue = cmnParameterValue;
            this.parameterValue = parameterValue;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CallResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="CallResponse", Namespace="http://tempuri.org/", Order=0)]
        public WinStore_sample.MuServiceReference.CallResponseBody Body;
        
        public CallResponse() {
        }
        
        public CallResponse(WinStore_sample.MuServiceReference.CallResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class CallResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string CallResult;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string context;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string returnValue;
        
        public CallResponseBody() {
        }
        
        public CallResponseBody(string CallResult, string context, string returnValue) {
            this.CallResult = CallResult;
            this.context = context;
            this.returnValue = returnValue;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ServiceForMuSoapChannel : WinStore_sample.MuServiceReference.ServiceForMuSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceForMuSoapClient : System.ServiceModel.ClientBase<WinStore_sample.MuServiceReference.ServiceForMuSoap>, WinStore_sample.MuServiceReference.ServiceForMuSoap {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public ServiceForMuSoapClient() : 
                base(ServiceForMuSoapClient.GetDefaultBinding(), ServiceForMuSoapClient.GetDefaultEndpointAddress()) {
            this.Endpoint.Name = EndpointConfiguration.ServiceForMuSoap.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ServiceForMuSoapClient(EndpointConfiguration endpointConfiguration) : 
                base(ServiceForMuSoapClient.GetBindingForEndpoint(endpointConfiguration), ServiceForMuSoapClient.GetEndpointAddress(endpointConfiguration)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ServiceForMuSoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(ServiceForMuSoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ServiceForMuSoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(ServiceForMuSoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ServiceForMuSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WinStore_sample.MuServiceReference.CallResponse> WinStore_sample.MuServiceReference.ServiceForMuSoap.CallAsync(WinStore_sample.MuServiceReference.CallRequest request) {
            return base.Channel.CallAsync(request);
        }
        
        public System.Threading.Tasks.Task<WinStore_sample.MuServiceReference.CallResponse> CallAsync(string context, string serviceName, WinStore_sample.MuServiceReference.ArrayOfString cmnParameterValue, string parameterValue) {
            WinStore_sample.MuServiceReference.CallRequest inValue = new WinStore_sample.MuServiceReference.CallRequest();
            inValue.Body = new WinStore_sample.MuServiceReference.CallRequestBody();
            inValue.Body.context = context;
            inValue.Body.serviceName = serviceName;
            inValue.Body.cmnParameterValue = cmnParameterValue;
            inValue.Body.parameterValue = parameterValue;
            return ((WinStore_sample.MuServiceReference.ServiceForMuSoap)(this)).CallAsync(inValue);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.ServiceForMuSoap)) {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.ServiceForMuSoap)) {
                return new System.ServiceModel.EndpointAddress("http://localhost:9996/WinStore_sample/WebService/ServiceForMu.asmx");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding() {
            return ServiceForMuSoapClient.GetBindingForEndpoint(EndpointConfiguration.ServiceForMuSoap);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress() {
            return ServiceForMuSoapClient.GetEndpointAddress(EndpointConfiguration.ServiceForMuSoap);
        }
        
        public enum EndpointConfiguration {
            
            ServiceForMuSoap,
        }
    }
}
