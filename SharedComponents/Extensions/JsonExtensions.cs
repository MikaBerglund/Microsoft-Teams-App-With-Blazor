using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace SharedComponents.Extensions
{
    public static class JsonExtensions
    {

        public static string TryGetPropertyAsString(this JsonElement element, string propertyName, string defaultValue = null)
        {
            string result = defaultValue;

            if(element.TryGetProperty(propertyName, out JsonElement p))
            {
                result = p.GetString();
            }

            return result;
        }

        public static IEnumerable<string> TryGetPropertyAsStringArray(this JsonElement element, string propertyName, IEnumerable<string> defaultValue = null)
        {
            var result = defaultValue;

            if (element.TryGetProperty(propertyName, out JsonElement p))
            {
                var list = new List<string>();
                foreach(var item in p.EnumerateArray())
                {
                    var s = item.GetString();
                    if (!string.IsNullOrEmpty(s)) list.Add(s);
                }

                result = list;
            }

            return result;
        }
    }
}
