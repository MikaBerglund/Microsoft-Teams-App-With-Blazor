using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SharedComponents.Model;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace SharedComponents
{
    partial class TeamsApplication
    {

        [Parameter]
        public bool AutoInitialize { get; set; }


        [Inject]
        protected IJSRuntime JsInterop { get; set; }


        public async Task InitializeAsync()
        {
            await this.JsInterop.InvokeVoidAsync("microsoftTeams.initialize");
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

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if(firstRender && this.AutoInitialize)
            {
                await this.InitializeAsync();
                await this.NotifySuccessAsync();
            }
        }
    }
}
