using Microsoft.JSInterop;
using SharedComponents.Api;
using SharedComponents.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharedComponents.JsInterop
{
    public class MicrosoftTeamsInterop : MicrosoftTeamsInteropModule
    {
        public MicrosoftTeamsInterop(IJSRuntime interopRuntime, BlazorTeamsAppOptions options, TokenStorageInterop tokenStorage) : base(interopRuntime, options)
        {
            this.TokenStorage = tokenStorage ?? throw new ArgumentNullException(nameof(tokenStorage));
        }

        private TokenStorageInterop TokenStorage { get; }

        private MicrosoftTeamsAppInitializationInterop _AppInitialization;

        public MicrosoftTeamsAppInitializationInterop AppInitialization
        {
            get
            {
                if(null == _AppInitialization)
                {
                    _AppInitialization = new MicrosoftTeamsAppInitializationInterop(this.Interop, this.Options);
                }
                return _AppInitialization;
            }
        }

        private MicrosoftTeamsAuthenticationInterop _Authentication;

        public MicrosoftTeamsAuthenticationInterop Authentication
        {
            get
            {
                if(null == _Authentication)
                {
                    _Authentication = new MicrosoftTeamsAuthenticationInterop(this.Interop, this.Options, this.TokenStorage);
                }
                return _Authentication;
            }
        }

        public async Task GetContextAsync(Func<Context, Task> callback)
        {
            
            this.AssertCallbackMethod(callback.Method);
            await this.Interop.InvokeVoidAsync("blazorTeams.getContext", CallbackDefinition.Create(callback.Target, callback.Method.Name));
        }

        public async Task InitializeAsync(Func<Task> callback)
        {
            this.AssertCallbackMethod(callback.Method);
            await this.Interop.InvokeVoidAsync("blazorTeams.initialize", CallbackDefinition.Create(callback.Target, callback.Method.Name));
        }



        private void AssertCallbackMethod(MethodInfo method)
        {
            var attribute = method.GetCustomAttribute<JSInvokableAttribute>();
            if (null == attribute)
            {
                throw new ArgumentException($"The given callback must be a defined method decorate with the '{typeof(JSInvokableAttribute).FullName}' attribute.", nameof(method));
            }
        }

    }
}
