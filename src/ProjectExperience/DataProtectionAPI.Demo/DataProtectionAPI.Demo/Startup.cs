using System.IO;
using DataProtectionAPI.Demo.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DataProtectionAPI.Demo
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // 資料庫設定強型別化
            services.Configure<DbSettings>(Configuration.GetSection(DbSettings.SectionName));
            // 註冊資料庫設定驗證器
            services.AddSingleton<ISettingsValidator<DbSettings>, DbSettingsValidator>();
            // 註冊解析後的資料庫設定(暴露給 App 使用，而不是直接注入 DbSettings 去使用...)
            services.AddScoped<IDbSettingsResolved, DbSettingsBridge>();
            // 註冊 DPAPI
            services.AddDataProtection()
                    .SetApplicationName("Demo")
                    // 保存私鑰至本機
                    .PersistKeysToFileSystem(new DirectoryInfo(Configuration.GetValue<string>("Security:KeyPath")));
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
