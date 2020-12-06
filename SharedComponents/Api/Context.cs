using SharedComponents.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace SharedComponents.Api
{
    public class Context
    {
        public Context() { }

        public Context(JsonElement json)
        {
            this.Locale = json.TryGetPropertyAsString("locale");
            this.LoginHint = json.TryGetPropertyAsString("loginHint");
            this.SessionId = json.TryGetPropertyAsString("sessionId");
            this.Theme = json.TryGetPropertyAsString("theme");
            this.TeamSiteDomain = json.TryGetPropertyAsString("teamSiteDomain");
            this.TenantId = json.TryGetPropertyAsString("tid");
            this.UserObjectId = json.TryGetPropertyAsString("userObjectId");
            this.UserPrincipalName = json.TryGetPropertyAsString("userPrincipalName");
        }

        public string Locale { get; set; }

        public string LoginHint { get; set; }

        public string SessionId { get; set; }

        public string Theme { get; set; }

        public string TeamSiteDomain { get; set; }

        public string TenantId { get; set; }

        public string UserObjectId { get; set; }

        public string UserPrincipalName { get; set; }

    }
}
