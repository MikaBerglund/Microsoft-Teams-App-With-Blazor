using SharedComponents.Extensions;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Text.Json;

namespace SharedComponents.Api
{
    public class ClientConfig
    {
        public ClientConfig() { }

        public ClientConfig(JsonElement element)
        {
            this.ClientId = element.TryGetPropertyAsString("clientId");
            this.RedirectUrl = element.TryGetPropertyAsString("redirectUrl");
            this.LoginUrl = element.TryGetPropertyAsString("loginUrl");
            this.Scopes = element.TryGetPropertyAsStringArray("scopes");
        }

        public string ClientId { get; set; }

        public string RedirectUrl { get; set; }

        public string LoginUrl { get; set; }

        public IEnumerable<string> Scopes { get; set; }

    }
}
