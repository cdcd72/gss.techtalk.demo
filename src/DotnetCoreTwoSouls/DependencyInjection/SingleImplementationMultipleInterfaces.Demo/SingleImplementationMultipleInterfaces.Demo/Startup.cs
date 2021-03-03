using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SingleImplementationMultipleInterfaces.Demo.Utility;

namespace SingleImplementationMultipleInterfaces.Demo
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // 試想你有一個工具類別且同時實作多個介面，但你又想要透過 DI 容器來決定暴露的程度...
            // PS. 但 IServiceCollection 目前又沒有內建類似實作可以做到這件事，所以可以透過以下方式來近似模擬...
            services.AddSingleton<Tool>();
            // ISecurityTool & IHashTool 皆會取到同一個實例，但暴露出來的方法會如預期的不同！
            services.AddSingleton<ISecurityTool>(sp => sp.GetRequiredService<Tool>());
            services.AddSingleton<IHashTool>(sp => sp.GetRequiredService<Tool>());
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
