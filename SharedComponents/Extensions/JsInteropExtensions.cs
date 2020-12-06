using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SharedComponents.Extensions
{
    public static class JsInteropExtensions
    {

        private const string ExpiresUtcKey = "expiresUtc";
        private const string AccessTokenKey = "accessToken";
        private const string IdTokenKey = "idToken";
        private const string AuthStateKey = "authState";


        public static async Task<string> GetAuthStateAsync(this IJSRuntime interop)
        {
            return await interop.GetLocalStorageItemAsync(AuthStateKey);
        }

        public static async Task SetAuthStateAsync(this IJSRuntime interop, string state)
        {
            await interop.SetLocalStorageItemAsync(AuthStateKey, state);
        }

        public static async Task<DateTime?> GetTokenExpiresUtcAsync(this IJSRuntime interop)
        {
            DateTime? expires = null;

            if(DateTime.TryParse(await interop.GetLocalStorageItemAsync(ExpiresUtcKey), out DateTime dt))
            {
                expires = dt;
            }

            return expires;
        }

        public static async Task SetTokenExpiresInAsync(this IJSRuntime interop, string expiresInSeconds)
        {
            if(int.TryParse(expiresInSeconds, out int i))
            {
                var expires = DateTime.UtcNow.AddSeconds(i).AddSeconds(-10);
                await interop.SetLocalStorageItemAsync(ExpiresUtcKey, expires.ToString("o"));
            }
            else
            {
                await interop.RemoveLocalStorageItemAsync(ExpiresUtcKey);
            }
        }

        public static async Task<string> GetAccessTokenAsync(this IJSRuntime interop)
        {
            return await interop.GetLocalStorageItemAsync(AccessTokenKey);
        }

        public static async Task SetAccessTokenAsync(this IJSRuntime interop, string token)
        {
            await interop.SetLocalStorageItemAsync(AccessTokenKey, token);
        }

        public static async Task<string> GetIdTokenAsync(this IJSRuntime interop)
        {
            return await interop.GetLocalStorageItemAsync(IdTokenKey);
        }

        public static async Task SetIdTokenAsync(this IJSRuntime interop, string token)
        {
            await interop.SetLocalStorageItemAsync(IdTokenKey, token);
        }

        public static async Task<string> GetLocalStorageItemAsync(this IJSRuntime interop, string key)
        {
            return await interop.InvokeAsync<string>("localStorage.getItem", key);
        }

        public static async Task RemoveLocalStorageItemAsync(this IJSRuntime interop, string key)
        {
            await interop.InvokeVoidAsync("localStorage.removeItem", key);
        }

        public static async Task SetLocalStorageItemAsync(this IJSRuntime interop, string key, string value)
        {
            if(!string.IsNullOrEmpty(value))
            {
                await interop.InvokeVoidAsync("localStorage.setItem", key, value);
            }
            else
            {
                await interop.RemoveLocalStorageItemAsync(key);
            }
        }

    }
}
