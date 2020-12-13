using Microsoft.JSInterop;
using SharedComponents.Configuration;
using SharedComponents.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharedComponents.JsInterop
{
    public class MicrosoftTeamsAppInitializationInterop : MicrosoftTeamsInteropModule
    {
        public MicrosoftTeamsAppInitializationInterop(IJSRuntime interopRuntime, BlazorTeamsAppOptions options) : base(interopRuntime, options)
        {
        }


        public async Task NotifyAppLoadedAsync()
        {
            await this.Interop.InvokeVoidAsync("microsoftTeams.appInitialization.notifyAppLoaded");
        }

        public async Task NotifyFailureAsync(FailedRequest appInitializationFailedRequest = null)
        {
            await this.Interop.InvokeVoidAsync("microsoftTeams.appInitialization.notifyFailure", appInitializationFailedRequest ?? new FailedRequest { });
        }

        public async Task NotifySuccessAsync()
        {
            await this.Interop.InvokeVoidAsync("microsoftTeams.appInitialization.notifySuccess");
        }

    }
}
