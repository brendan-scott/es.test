using Emergent.Code.Test.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Emergent.Code.Test
{
    public class Startup
    {
        private const string ERROR_HANDLING_PATH = "/Home/Error";
        private const string DEFAULT = "default";
        private const string DEFAULT_ROUTE = "{controller=Home}/{action=Index}/{id?}";

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            // Manager
            _ = services.AddScoped<ISoftwareManager, SoftwareManager>();

            IMvcBuilder mvcBuilder = services.AddControllersWithViews();

#if DEBUG
            _ = mvcBuilder.AddRazorRuntimeCompilation();
#endif
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                _ = app
                    .UseDeveloperExceptionPage();
            }
            else
            {
                _ = app
                    .UseExceptionHandler(ERROR_HANDLING_PATH)
                    .UseHsts();
            }

            _ = app
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints.MapControllerRoute(DEFAULT, DEFAULT_ROUTE));
        }
    }
}