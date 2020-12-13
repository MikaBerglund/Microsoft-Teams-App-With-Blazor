using SharedComponents.Configuration;
using SharedComponents.JsInterop;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddBlazorTeamsApp(this IServiceCollection services)
        {
            return services
                .AddScoped<MicrosoftTeamsInterop>()
                .AddScoped<LocalStorageInterop>()
                .AddScoped<TokenStorageInterop>()
                ;
        }

        public static IServiceCollection AddBlazorTeamsApp(this IServiceCollection services, Action<BlazorTeamsAppOptions> configure)
        {
            BlazorTeamsAppOptions options = new BlazorTeamsAppOptions();
            configure?.Invoke(options);
            return services
                .AddSingleton(options)
                .AddBlazorTeamsApp()
                ;
        }

    }
}
