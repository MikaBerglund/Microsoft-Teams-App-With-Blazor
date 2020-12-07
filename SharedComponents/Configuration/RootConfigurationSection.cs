using System;
using System.Collections.Generic;
using System.Text;

namespace SharedComponents.Configuration
{
    public class RootConfigurationSection
    {
        public RootConfigurationSection()
        {
            this.BlazorTeamsApp = new BlazorTeamsAppOptions();
        }


        public BlazorTeamsAppOptions BlazorTeamsApp { get; set; }

    }
}
