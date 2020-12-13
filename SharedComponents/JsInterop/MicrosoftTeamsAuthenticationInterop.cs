using Microsoft.JSInterop;
using SharedComponents.Configuration;
using SharedComponents.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharedComponents.JsInterop
{
    public class MicrosoftTeamsAuthenticationInterop : MicrosoftTeamsInteropModule
    {
        public MicrosoftTeamsAuthenticationInterop(IJSRuntime interopRuntime, BlazorTeamsAppOptions options) : base(interopRuntime, options) { }


        public async Task AuthenticateAsync(BlazorTeamsAppOptions options, Func<Task> callback)
        {
            await this.Interop.InvokeVoidAsync("blazorTeams.authentication.authenticate", options, CallbackDefinition.Create(callback.Target, callback.Method.Name));
        }

        public async Task RedirectToAuthorityAsync(BlazorTeamsAppOptions options)
        {
            string state = Guid.NewGuid().ToString(), nonce = Guid.NewGuid().ToString();
            await this.Interop.SetAuthStateAsync(state);
            await this.Interop.InvokeVoidAsync("blazorTeams.authentication.redirectToAuthority", options, nonce, state);
        }
    }
}
