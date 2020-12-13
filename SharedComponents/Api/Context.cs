using SharedComponents.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace SharedComponents.Api
{
    public class Context
    {

        public string Locale { get; set; }

        public string LoginHint { get; set; }

        public string SessionId { get; set; }

        public string Theme { get; set; }

        public string TeamSiteDomain { get; set; }

        public string Tid { get; set; }

        public string UserObjectId { get; set; }

        public string UserPrincipalName { get; set; }

    }
}
