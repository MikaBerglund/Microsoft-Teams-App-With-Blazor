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

        [Parameter]
        public string ErrorCode { get; set; }

        [Parameter]
        public string ErrorDescription { get; set; }

        [Parameter]
        public string ErrorUrl { get; set; }

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
                    var state = Guid.NewGuid().ToString();
                    var nonce = Guid.NewGuid().ToString();
                    await this.JsInterop.SetAuthStateAsync(state);
                    await this.JsInterop.InvokeVoidAsync("blazorTeams.redirectToAuthority", nonce, state);
                }
                else
                {
                    var parameters = uri.ParseFragment();
                    bool success = false;
                    bool displayError = false;

                    var storedState = await this.JsInterop.GetAuthStateAsync();
                    if(storedState == parameters.GetValue("state"))
                    {
                        if (!parameters.ContainsKey("error"))
                        {
                            await this.JsInterop.SetTokenExpiresInAsync(parameters.GetValue("expires_in"));
                            await this.JsInterop.SetAccessTokenAsync(parameters.GetValue("access_token"));
                            await this.JsInterop.SetIdTokenAsync(parameters.GetValue("id_token"));

                            
                            success = true;
                        }
                        else
                        {
                            this.ErrorCode = parameters.GetValue("error");
                            this.ErrorDescription = parameters.GetValue("error_description");
                            this.ErrorUrl = parameters.GetValue("error_uri");

                            displayError = true;
                        }

                        await this.JsInterop.SetAuthStateAsync(null);
                    }

                    if(success)
                    {
                        await this.JsInterop.InvokeVoidAsync("microsoftTeams.authentication.notifySuccess");
                    }
                    else if(!displayError)
                    {
                        await this.JsInterop.InvokeVoidAsync("microsoftTeams.authentication.notifyFailure");
                    }
                    else
                    {
                        this.StateHasChanged();
                    }
                }
            }
        }


        
    }
}
