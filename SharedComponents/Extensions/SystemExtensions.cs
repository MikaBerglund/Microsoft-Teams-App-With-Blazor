using System;
using System.Collections.Generic;
using System.Text;

namespace SharedComponents.Extensions
{
    public static class SystemExtensions
    {

        public static IDictionary<string, string> ParseFragment(this Uri uri)
        {
            var dict = new Dictionary<string, string>();

            if(uri?.Fragment?.Length > 0)
            {
                var arr = uri.Fragment.Substring(1).Split('&');
                foreach(var item in arr)
                {
                    var ix = item.IndexOf('=');
                    if(ix > 0)
                    {
                        var name = item.Substring(0, ix);
                        var value = item.Substring(ix + 1);

                        dict[name] = value;
                    }
                }
            }

            return dict;
        }
    }
}
