using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SharedComponents.Api;
using SharedComponents.Configuration;
using SharedComponents.Extensions;
using SharedComponents.JsInterop;
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
            this.ApplicationContext = new BlazorAppContext();
            this.CheckHost = true;
        }

        protected BlazorAppContext ApplicationContext { get; private set; }

        protected bool ShowSignInTemplate { get; set; }

        protected bool IsHostFound { get; set; }

        [Parameter]
        public RenderFragment<BlazorAppContext> ApplicationTemplate { get; set; }

        [Parameter]
        public bool CheckHost { get; set; }

        [Parameter]
        public RenderFragment HostNotFoundTemplate { get; set; }

        [Parameter]
        public RenderFragment<BlazorAppContext> SignInTemplate { get; set; }

        [Parameter]
        public bool RequireAuthentication { get; set; }



        [Inject]
        protected TokenStorageInterop TokenStorage { get; set; }

        [Inject]
        protected BlazorTeamsAppOptions Options { get; set; }

        [Inject]
        protected MicrosoftTeamsInterop TeamsInterop { get; set; }



        public async Task SignInAsync()
        {
            await this.TeamsInterop.Authentication.AuthenticateAsync(this.Options, this.OnSignedInAsync);
        }

        public async Task SignOutAsync()
        {
            await this.TokenStorage.ClearTokensAsync();
            this.ShowSignInTemplate = this.RequireAuthentication;

            this.StateHasChanged();
        }



        [JSInvokable]
        public async Task OnAppInitializedAsync()
        {
            await this.TeamsInterop.GetContextAsync(this.OnGotContextAsync);
        }

        [JSInvokable]
        public async Task OnSignedInAsync()
        {
            await this.HandleSignInTemplateAsync();
        }

        [JSInvokable]
        public async Task OnGotContextAsync(Context context)
        {
            this.ApplicationContext.Context = context;

            await this.HandleSignInTemplateAsync();

            await this.TeamsInterop.AppInitialization.NotifyAppLoadedAsync();
        }



        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if(firstRender)
            {
                this.IsHostFound = !this.CheckHost || await this.TeamsInterop.IsHostFoundAsync();
                if(this.IsHostFound)
                {
                    await this.TeamsInterop.InitializeAsync(this.OnAppInitializedAsync);
                }
                else
                {
                    this.StateHasChanged();
                }
            }

        }



        private async Task HandleSignInTemplateAsync()
        {
            var tokensValid = await this.TokenStorage.IsAuthTokenValidAsync();
            this.ShowSignInTemplate = this.RequireAuthentication && !tokensValid;
            this.StateHasChanged();
        }

    }
}
