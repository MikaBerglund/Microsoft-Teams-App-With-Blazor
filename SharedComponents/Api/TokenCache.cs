using System;
using System.Collections.Generic;
using System.Text;

namespace SharedComponents.Api
{
    public class TokenCache
    {

        public string AccessToken { get; set; }

        public string IdToken { get; set; }

        public DateTime ExpireUtc { get; set; }

    }
}
