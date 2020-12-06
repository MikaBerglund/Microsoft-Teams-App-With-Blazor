using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SharedComponents.Api;
using SharedComponents.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SharedComponents
{
    partial class LoginDialogHandler
    {

        [Parameter]
        public virtual RenderFragment ChildContent { get; set; }

        [Parameter]
        public Context Context { get; set; }

        [Inject]
        protected IJSRuntime JsInterop { get; set; }

        [Inject]
        protected NavigationManager NavMan { get; set; }



        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if(firstRender)
            {
                var uri = new Uri(this.NavMan.Uri);
                
                if(string.IsNullOrEmpty(uri.Fragment))
                {
                    await this.JsInterop.InvokeVoidAsync("blazorTeams.redirectToAuthority", Guid.NewGuid(), Guid.NewGuid());
                }
                else
                {
                    var parameters = uri.ParseFragment();
                    await this.JsInterop.InvokeVoidAsync("microsoftTeams.authentication.notifySuccess");
                }
            }
        }
    }
}
