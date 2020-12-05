using Microsoft.JSInterop;
using SharedComponents.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharedComponents.JsInterop
{
    public class AppInitializationModule : InteropModule
    {
        public async Task NotifyAppLoadedAsync()
        {
            await this.JsInterop.InvokeVoidAsync("microsoftTeams.appInitialization.notifyAppLoaded");
        }

        public async Task NotifyFailureAsync(string message = null)
        {
            await this.NotifyFailureAsync(new FailedRequest { message = message });
        }

        public async Task NotifyFailureAsync(FailedRequest reason)
        {
            await this.JsInterop.InvokeVoidAsync("microsoftTeams.appInitialization.notifyFailure", reason);
        }

        public async Task NotifySuccessAsync()
        {
            await this.JsInterop.InvokeVoidAsync("microsoftTeams.appInitialization.notifySuccess");
        }

    }
}
