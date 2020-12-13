using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SharedComponents.Api;
using SharedComponents.Configuration;
using SharedComponents.Extensions;
using SharedComponents.JsInterop;
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
        protected MicrosoftTeamsInterop TeamsInterop { get; set; }

        [Inject]
        protected LocalStorageInterop LocalStorage { get; set; }

        [Inject]
        protected TokenStorageInterop TokenStorage { get; set; }

        [Inject]
        protected BlazorTeamsAppOptions Options { get; set; }

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
                    await this.TeamsInterop.Authentication.RedirectToAuthorityAsync(this.Options);// .JsInterop.RedirectToAuthorityAsync(this.Options, nonce, state);
                }
                else
                {
                    var parameters = uri.ParseFragment();
                    bool success = false;
                    bool displayError = false;

                    var storedState = await this.TokenStorage.GetAuthStateAsync();
                    if(storedState == parameters.GetValue("state"))
                    {
                        if (!parameters.ContainsKey("error"))
                        {
                            await this.TokenStorage.SetTokenExpiresInAsync(parameters.GetValue("expires_in"));
                            await this.TokenStorage.SetAccessTokenAsync(parameters.GetValue("access_token"));
                            await this.TokenStorage.SetIdTokenAsync(parameters.GetValue("id_token"));
                            
                            success = true;
                        }
                        else
                        {
                            this.ErrorCode = parameters.GetValue("error");
                            this.ErrorDescription = parameters.GetValue("error_description");
                            this.ErrorUrl = parameters.GetValue("error_uri");

                            displayError = true;
                        }

                        await this.TokenStorage.SetAuthStateAsync(null);
                    }

                    if(success)
                    {
                        await this.TeamsInterop.Authentication.NotifySuccessAsync();
                    }
                    else if(!displayError)
                    {
                        await this.TeamsInterop.Authentication.NofityFailureAsync();
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
