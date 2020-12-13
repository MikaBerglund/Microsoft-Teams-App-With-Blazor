using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharedComponents.JsInterop
{
    internal class CallbackDefinition
    {

        public static CallbackDefinition Create(object target, string methodName)
        {
            return new CallbackDefinition
            {
                Target = DotNetObjectReference.Create(target),
                MethodName = methodName
            };
        }

        public DotNetObjectReference<object> Target { get; set; }

        public string MethodName { get; set; }

    }
}
