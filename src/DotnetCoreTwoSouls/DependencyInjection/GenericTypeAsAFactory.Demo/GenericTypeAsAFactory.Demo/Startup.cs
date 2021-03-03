using GenericTypeAsAFactory.Demo.Services;
using GenericTypeAsAFactory.Demo.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GenericTypeAsAFactory.Demo
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // 註冊 IDemoService (實際注入 DemoService)
            services.AddTransient<IDemoService, DemoService>();

            // 註冊 IServiceFactory
            services.AddTransient(typeof(IServiceFactory<>), typeof(ServiceFactory<>));

            // 1. 以往寫 Factory 可能都是開一個類別寫 Create 方法，但若搭配 DI 機制可以寫一個較通用的 Factory，而且還不用寫到方法...
            // 2. 此 Factory 利用 IServiceProvider 可取得已註冊至 DI 容器的 TService 之外，若不存在於 DI 容器的 TService 也能夠創建新實例...
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
