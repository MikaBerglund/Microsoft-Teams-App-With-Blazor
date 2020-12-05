using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SharedComponents.JsInterop;
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
            this.AutoInitialize = true;
        }



        [Parameter]
        public EventCallback<EventArgs> Initialized { get; set; }

        [Parameter]
        public bool AutoInitialize { get; set; }



        public async Task InitializeAsync()
        {
            await this.JsInterop.InvokeVoidAsync("blazorTeams.initialize", DotNetObjectReference.Create(this), nameof(OnAppInitializedAsync));
        }

        [JSInvokable]
        public async Task OnAppInitializedAsync()
        {
            await this.JsInterop.InvokeVoidAsync("microsoftTeams.appInitialization.notifySuccess");
            await this.Initialized.InvokeAsync(EventArgs.Empty);
        }



        [Inject]
        protected IJSRuntime JsInterop { get; set; }





        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if(firstRender && this.AutoInitialize)
            {
                await this.InitializeAsync();
                //await this.MicrosoftTeams.AppInitialization.NotifySuccessAsync();
            }
        }
    }
}
