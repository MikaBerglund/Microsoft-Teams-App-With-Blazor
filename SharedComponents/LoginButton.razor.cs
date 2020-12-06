using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharedComponents
{
    partial class LoginButton
    {

        [Parameter]
        public virtual RenderFragment ChildContent { get; set; }

        [Parameter]
        public string LoginHint { get; set; }

        [Inject]
        protected IJSRuntime JsInterop { get; set; }


        protected async Task LoginAsync()
        {
            await this.JsInterop.InvokeVoidAsync("blazorTeams.aad.login");
        }
    }
}
