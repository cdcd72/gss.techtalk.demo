using System.IO;
using System.Text;
using JwtAuth.Demo.Configuration;
using JwtAuth.Demo.Helpers;
using JwtAuth.Demo.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuth.Demo
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

            // 使用 MemoryCache
            services.AddMemoryCache();
            // 註冊 UserService
            services.AddScoped<IUserService, UserService>();
            // JWT 設定強型別化
            services.Configure<JwtSettings>(Configuration.GetSection(JwtSettings.SectionName));
            // 註冊解析後的 JWT 設定(暴露給 App 使用，而不是直接注入 JwtSettings 去使用...)
            services.AddScoped<IJwtSettingsResolved, JwtSettingsBridge>();
            // JWT Helper
            services.AddScoped<IJwtHelper, JwtHelper>();
            var jwtSettings = GetJwtSettings(services);
            // 註冊認證機制 (預設使用 JWT 做驗證...)
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer, // JWT Issuer
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience, // JWT Consumer
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    ValidateLifetime = true
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting();

            // 先驗證
            app.UseAuthentication();

            // 再授權
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        private IJwtSettingsResolved GetJwtSettings(IServiceCollection services)
            => services.BuildServiceProvider().GetService<IJwtSettingsResolved>();
    }
}
