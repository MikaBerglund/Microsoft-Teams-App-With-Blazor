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
        public MicrosoftTeamsAuthenticationInterop(IJSRuntime interopRuntime, BlazorTeamsAppOptions options, TokenStorageInterop tokenStorage) : base(interopRuntime, options)
        {
            this.TokenStorage = tokenStorage ?? throw new ArgumentNullException(nameof(tokenStorage));
        }

        private TokenStorageInterop TokenStorage { get; }

        public async Task AuthenticateAsync(BlazorTeamsAppOptions options, Func<Task> callback)
        {
            await this.Interop.InvokeVoidAsync("blazorTeams.authentication.authenticate", options, CallbackDefinition.Create(callback.Target, callback.Method.Name));
        }

        public async Task NotifySuccessAsync()
        {
            await this.Interop.InvokeVoidAsync("microsoftTeams.authentication.notifySuccess");
        }

        public async Task NofityFailureAsync()
        {
            await this.Interop.InvokeVoidAsync("microsoftTeams.authentication.notifyFailure");
        }

        public async Task RedirectToAuthorityAsync(BlazorTeamsAppOptions options)
        {
            string state = Guid.NewGuid().ToString(), nonce = Guid.NewGuid().ToString();
            await this.TokenStorage.SetAuthStateAsync(state);
            await this.Interop.InvokeVoidAsync("blazorTeams.authentication.redirectToAuthority", options, nonce, state);
        }

    }
}
