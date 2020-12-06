using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SharedComponents.Api;
using SharedComponents.Extensions;
using SharedComponents.Model;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SharedComponents
{
    partial class TeamsApplication
    {
        public TeamsApplication()
        {
            this.ApplicationFacade = new TeamsApplicationFacade();
        }

        protected TeamsApplicationFacade ApplicationFacade
        {
            get;
            private set;
        }

        protected bool ShowAuthentication { get; set; }

        [Parameter]
        public RenderFragment<TeamsApplicationFacade> ApplicationTemplate { get; set; }

        [Parameter]
        public RenderFragment SignInTemplate { get; set; }

        [Parameter]
        public bool RequireAuthentication { get; set; }



        public async Task SignInAsync()
        {
            await this.JsInterop.AuthenticateAsync(this, nameof(OnSignedInAsync));
        }

        public async Task SignOutAsync()
        {
            await this.JsInterop.ClearTokensAsync();
            await this.GetContextAsync();
        }

        [JSInvokable]
        public async Task OnAppInitializedAsync()
        {
            await this.JsInterop.AppInitializationNotifyAppLoadedAsync();
            await this.GetContextAsync();
        }

        [JSInvokable]
        public async Task OnSignedInAsync()
        {
            await this.HandleTokensAsync();
            this.StateHasChanged();
        }

        [JSInvokable]
        public async Task OnGotContextAsync(JsonElement args)
        {
            this.ApplicationFacade.Context = new Context(args);

            await this.HandleTokensAsync();

            await this.JsInterop.AppInitializationNotifySuccessAsync();

            this.StateHasChanged();
        }



        [Inject]
        protected IJSRuntime JsInterop { get; set; }



        protected async override Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            this.ShowAuthentication = this.RequireAuthentication;
        }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if(firstRender)
            {
                await this.InitializeAsync();
            }
        }


        private async Task GetContextAsync()
        {
            await this.JsInterop.InvokeVoidAsync("blazorTeams.getContext", DotNetObjectReference.Create(this), nameof(OnGotContextAsync));
        }

        private async Task HandleTokensAsync()
        {
            this.ApplicationFacade.TokenCache = null;
            if (this.RequireAuthentication)
            {
                var accessToken = await this.JsInterop.GetAccessTokenAsync();
                var idToken = await this.JsInterop.GetIdTokenAsync();
                var expires = await this.JsInterop.GetTokenExpiresUtcAsync();

                var isTokenValid = expires.HasValue && expires.Value > DateTime.UtcNow && accessToken?.Length > 0 && idToken?.Length > 0;
                this.ShowAuthentication = !isTokenValid;
                if (isTokenValid)
                {
                    this.ApplicationFacade.TokenCache = new TokenCache
                    {
                        AccessToken = accessToken,
                        IdToken = idToken,
                        ExpireUtc = expires.Value
                    };
                }
            }
            else
            {
                this.ShowAuthentication = false;
            }
        }

        private async Task InitializeAsync()
        {
            await this.JsInterop.InvokeVoidAsync("blazorTeams.initialize", DotNetObjectReference.Create(this), nameof(OnAppInitializedAsync));
        }

    }
}
