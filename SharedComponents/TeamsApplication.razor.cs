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
        }

        protected BlazorAppContext ApplicationContext { get; private set; }

        protected bool ShowAuthentication { get; set; }

        [Parameter]
        public RenderFragment<BlazorAppContext> ApplicationTemplate { get; set; }

        [Parameter]
        public RenderFragment<BlazorAppContext> SignInTemplate { get; set; }

        [Parameter]
        public bool RequireAuthentication { get; set; }



        [Inject]
        protected IJSRuntime JsInterop { get; set; }

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
            await this.JsInterop.ClearTokensAsync();
            this.ShowAuthentication = this.RequireAuthentication;

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
            var tokensValid = await this.JsInterop.IsAuthTokenValidAsync();
            this.ShowAuthentication = this.RequireAuthentication && !tokensValid;

            this.StateHasChanged();
        }

        [JSInvokable]
        public async Task OnGotContextAsync(Context context)
        {
            this.ApplicationContext.Context = context;

            if(this.RequireAuthentication)
            {

                await this.TeamsInterop.AppInitialization.NotifySuccessAsync();
            }
            else
            {
                await this.TeamsInterop.AppInitialization.NotifySuccessAsync();
            }

            this.StateHasChanged();
        }







        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if(firstRender)
            {
                await this.TeamsInterop.InitializeAsync(this.OnAppInitializedAsync);
            }

        }

    }
}
