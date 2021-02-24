using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ResolvingServicesWhenUsingIOptions.Demo.Model;
using ResolvingServicesWhenUsingIOptions.Demo.Services;

namespace ResolvingServicesWhenUsingIOptions.Demo
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // 註冊 IAnotherService (實際注入 AnotherService)
            services.AddTransient<IAnotherService, AnotherService>();

            // 除了一般將組態做強型別化之外...
            services.Configure<SomeOptions>(Configuration.GetSection(SomeOptions.Some));

            // 還可以透過一些額外的服務來配置 Options 物件本身...
            services.AddOptions<SomeOptions>()
                    .Configure<IAnotherService>((options, anotherService) =>
                        options.AnotherSetting = anotherService.GetAnotherSetting());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
