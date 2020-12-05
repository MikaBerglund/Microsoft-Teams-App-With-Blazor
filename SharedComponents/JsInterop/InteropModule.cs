using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.JSInterop;

namespace SharedComponents.JsInterop
{
    public abstract class InteropModule
    {

        protected InteropModule() { }

        protected InteropModule(IJSRuntime jsInterop)
        {
            this.JsInterop = jsInterop;
        }

        private IJSRuntime _JsInterop;
        public IJSRuntime JsInterop
        {
            get => _JsInterop;
            set
            {
                _JsInterop = value;
                this.OnJsInteropChanged(value);
            }
        }



        protected virtual void OnJsInteropChanged(IJSRuntime jsInterop) { }

    }
}
