using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
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
            await this.JsInterop.InvokeVoidAsync("teamsBlazor.initialize");
            await this.JsInterop.InvokeVoidAsync("teamsBlazor.notifySuccess");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if(firstRender && this.AutoInitialize)
            {
                await this.InitializeAsync();
            }
        }
    }
}
