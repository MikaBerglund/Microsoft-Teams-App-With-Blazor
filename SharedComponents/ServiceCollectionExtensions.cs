using SharedComponents.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddBlazorTeamsApp(this IServiceCollection services, Action<BlazorTeamsAppOptions> configure = null)
        {
            BlazorTeamsAppOptions options = new BlazorTeamsAppOptions();
            configure?.Invoke(options);
            return services
                .AddSingleton(provider =>
                {
                    if(null != configure)
                    {
                        return options;
                    }

                    return provider.GetService<BlazorTeamsAppOptions>() ?? options;
                })
                ;
        }

    }
}
