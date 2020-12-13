using Microsoft.JSInterop;
using SharedComponents.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedComponents.JsInterop
{
    public abstract class MicrosoftTeamsInteropModule
    {
        protected MicrosoftTeamsInteropModule(IJSRuntime interopRuntime, BlazorTeamsAppOptions options)
        {
            this.Interop = interopRuntime ?? throw new ArgumentNullException(nameof(interopRuntime));
            this.Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        protected IJSRuntime Interop { get; }

        protected BlazorTeamsAppOptions Options { get; }

    }
}
