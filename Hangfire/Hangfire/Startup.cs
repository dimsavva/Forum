using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;


namespace Hangfire
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHangfire(config => config.UseMemoryStorage()); // This should work if Hangfire is correctly installed
            services.AddHangfireServer();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHangfireDashboard("/hangfire");
        }
    }
}
