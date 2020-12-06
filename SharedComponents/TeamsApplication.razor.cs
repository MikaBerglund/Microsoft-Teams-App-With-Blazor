using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SharedComponents.Api;
using SharedComponents.Model;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SharedComponents
{
    partial class TeamsApplication
    {
        public TeamsApplication()
        {
        }

        private TeamsApplicationFacade FacadeBuilder = new TeamsApplicationFacade();

        protected TeamsApplicationFacade ApplicationFacade
        {
            get;
            private set;
        }

        [Parameter]
        public EventCallback AppInitialized { get; set; }

        [Parameter]
        public EventCallback<Context> GotContext { get; set; }

        [Parameter]
        public RenderFragment<TeamsApplicationFacade> ApplicationTemplate { get; set; }



        [JSInvokable]
        public async Task OnAppInitializedAsync()
        {
            await this.JsInterop.InvokeVoidAsync("microsoftTeams.appInitialization.notifySuccess");
            await this.AppInitialized.InvokeAsync(null);

            await this.GetContextAsync();
        }

        [JSInvokable]
        public async Task OnGotContextAsync(JsonElement args)
        {
            this.FacadeBuilder.Context = new Context(args);
            await this.GotContext.InvokeAsync(this.FacadeBuilder.Context);

            this.FinalizeApplicationFacade();

            await Task.CompletedTask;
        }



        [Inject]
        protected IJSRuntime JsInterop { get; set; }




        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if(firstRender)
            {
                await this.InitializeAsync();
            }
        }


        private void FinalizeApplicationFacade()
        {
            this.ApplicationFacade = this.FacadeBuilder;
            this.StateHasChanged();
        }

        private async Task GetContextAsync()
        {
            await this.JsInterop.InvokeVoidAsync("blazorTeams.getContext", DotNetObjectReference.Create(this), nameof(OnGotContextAsync));
        }

        private async Task InitializeAsync()
        {
            await this.JsInterop.InvokeVoidAsync("blazorTeams.initialize", DotNetObjectReference.Create(this), nameof(OnAppInitializedAsync));
        }

    }
}
