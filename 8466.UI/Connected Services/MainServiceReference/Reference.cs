﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConcProg_CW1_8466.MainServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MainServiceReference.IMainService")]
    public interface IMainService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMainService/StartCollectingSwipes", ReplyAction="http://tempuri.org/IMainService/StartCollectingSwipesResponse")]
        _8466.Domain.Entities.Operation[] StartCollectingSwipes();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMainService/StartCollectingSwipes", ReplyAction="http://tempuri.org/IMainService/StartCollectingSwipesResponse")]
        System.Threading.Tasks.Task<_8466.Domain.Entities.Operation[]> StartCollectingSwipesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMainService/GetStatus", ReplyAction="http://tempuri.org/IMainService/GetStatusResponse")]
        _8466.Domain.Entities.Operation[] GetStatus();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMainService/GetStatus", ReplyAction="http://tempuri.org/IMainService/GetStatusResponse")]
        System.Threading.Tasks.Task<_8466.Domain.Entities.Operation[]> GetStatusAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMainServiceChannel : ConcProg_CW1_8466.MainServiceReference.IMainService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MainServiceClient : System.ServiceModel.ClientBase<ConcProg_CW1_8466.MainServiceReference.IMainService>, ConcProg_CW1_8466.MainServiceReference.IMainService {
        
        public MainServiceClient() {
        }
        
        public MainServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MainServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MainServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MainServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public _8466.Domain.Entities.Operation[] StartCollectingSwipes() {
            return base.Channel.StartCollectingSwipes();
        }
        
        public System.Threading.Tasks.Task<_8466.Domain.Entities.Operation[]> StartCollectingSwipesAsync() {
            return base.Channel.StartCollectingSwipesAsync();
        }
        
        public _8466.Domain.Entities.Operation[] GetStatus() {
            return base.Channel.GetStatus();
        }
        
        public System.Threading.Tasks.Task<_8466.Domain.Entities.Operation[]> GetStatusAsync() {
            return base.Channel.GetStatusAsync();
        }
    }
}