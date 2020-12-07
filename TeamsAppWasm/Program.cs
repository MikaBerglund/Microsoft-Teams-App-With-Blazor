using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SharedComponents.Configuration;
using System.IO;

namespace TeamsAppWasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            ConfigureServices(builder.Services);

            await builder.Build().RunAsync();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton(provider =>
                {
                    var config = provider.GetService<IConfiguration>();
                    var root = config.Get<RootConfigurationSection>();
                    return root;
                })
                .AddSingleton(provider =>
                {
                    var root = provider.GetService<RootConfigurationSection>();
                    return root.BlazorTeamsApp;
                })
                .AddBlazorTeamsApp()
                ;
        }
    }
}
