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
    }
}
