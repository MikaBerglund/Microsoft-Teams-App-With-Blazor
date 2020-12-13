using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharedComponents.JsInterop
{
    public class LocalStorageInterop
    {
        public LocalStorageInterop(IJSRuntime interopRuntime)
        {
            this.Interop = interopRuntime ?? throw new ArgumentNullException(nameof(interopRuntime));
        }

        private IJSRuntime Interop { get; }


        public async Task<string> GetItemAsync(string key)
        {
            return await this.Interop.InvokeAsync<string>("localStorage.getItem", key);
        }

        public async Task RemoveItemAsync(string key)
        {
            await this.Interop.InvokeVoidAsync("localStorage.removeItem", key);
        }

        public async Task SetItemAsync(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                await this.Interop.InvokeVoidAsync("localStorage.setItem", key, value);
            }
            else
            {
                await this.RemoveItemAsync(key);
            }
        }

    }
}
