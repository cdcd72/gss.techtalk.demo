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

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // ���U IDemoService (��ڪ`�J DemoService)
            services.AddTransient<IDemoService, DemoService>();

            // ���U IServiceFactory
            services.AddTransient(typeof(IServiceFactory<>), typeof(ServiceFactory<>));

            // 1. �H���g Factory �i�ೣ�O�}�@�����O�g Create ��k�A���Y�f�t DI ����i�H�g�@�Ӹ��q�Ϊ� Factory�A�ӥB�٤��μg���k...
            // 2. �� Factory �Q�� IServiceProvider �i���o�w���U�� DI �e���� TService ���~�A�Y���s�b�� DI �e���� TService �]����Ыطs���...
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
