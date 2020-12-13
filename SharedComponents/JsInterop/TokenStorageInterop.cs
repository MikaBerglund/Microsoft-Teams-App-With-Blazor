using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharedComponents.JsInterop
{
    public class TokenStorageInterop
    {
        public TokenStorageInterop(LocalStorageInterop localStorage)
        {
            this.LocalStorage = localStorage ?? throw new ArgumentNullException(nameof(localStorage));
        }

        private const string ExpiresUtcKey = "expiresUtc";
        private const string AccessTokenKey = "accessToken";
        private const string IdTokenKey = "idToken";
        private const string AuthStateKey = "authState";


        private LocalStorageInterop LocalStorage { get; }

        public async Task ClearTokensAsync()
        {
            await this.SetAccessTokenAsync(null);
            await this.SetIdTokenAsync(null);
            await this.SetTokenExpiresInAsync(null);
        }

        public async Task<string> GetAccessTokenAsync()
        {
            return await this.LocalStorage.GetItemAsync(AccessTokenKey);
        }

        public async Task<string> GetAuthStateAsync()
        {
            return await this.LocalStorage.GetItemAsync(AuthStateKey);
        }

        public async Task<string> GetIdTokenAsync()
        {
            return await this.LocalStorage.GetItemAsync(IdTokenKey);
        }

        public async Task<DateTime?> GetTokenExpiresUtcAsync()
        {
            DateTime? expires = null;

            if (DateTime.TryParse(await this.LocalStorage.GetItemAsync(ExpiresUtcKey), out DateTime dt))
            {
                expires = dt;
            }

            return expires;
        }

        public async Task<bool> IsAuthTokenValidAsync()
        {
            bool result = false;

            var accessToken = await this.GetAccessTokenAsync();
            var idToken = await this.GetIdTokenAsync();
            var expires = await this.GetTokenExpiresUtcAsync();

            result = expires.HasValue && expires.Value > DateTime.UtcNow && accessToken?.Length > 0 && idToken?.Length > 0;

            return result;
        }

        public async Task SetAccessTokenAsync(string token)
        {
            await this.LocalStorage.SetItemAsync(AccessTokenKey, token);
        }

        public async Task SetAuthStateAsync(string state)
        {
            await this.LocalStorage.SetItemAsync(AuthStateKey, state);
        }

        public async Task SetTokenExpiresInAsync(string expiresInSeconds)
        {
            if (int.TryParse(expiresInSeconds, out int i))
            {
                var expires = DateTime.UtcNow.AddSeconds(i).AddSeconds(-10);
                await this.LocalStorage.SetItemAsync(ExpiresUtcKey, expires.ToString("o"));
            }
            else
            {
                await this.LocalStorage.RemoveItemAsync(ExpiresUtcKey);
            }
        }

        public async Task SetIdTokenAsync(string token)
        {
            await this.LocalStorage.SetItemAsync(IdTokenKey, token);
        }

    }
}
