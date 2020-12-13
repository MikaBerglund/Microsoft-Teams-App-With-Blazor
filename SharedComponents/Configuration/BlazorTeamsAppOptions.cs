using System;
using System.Collections.Generic;
using System.Text;

namespace SharedComponents.Configuration
{
    public class BlazorTeamsAppOptions
    {
        public BlazorTeamsAppOptions()
        {
            this.ClientId = Guid.Empty.ToString();
            this.LoginUrl = "/login";
            this.Scopes = new string[] { "user.read", "openid" };
            this.TenantId = "common";
        }

        public string ClientId { get; set; }

        public string LoginUrl { get; set; }

        public ICollection<string> Scopes { get; set; }

        public string TenantId { get; set; }

    }
}
