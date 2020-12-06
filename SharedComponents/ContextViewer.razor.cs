using Microsoft.AspNetCore.Components;
using SharedComponents.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedComponents
{
    partial class ContextViewer
    {

        [Parameter]
        public Context Context { get; set; }

    }
}
