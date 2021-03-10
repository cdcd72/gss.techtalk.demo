using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Utility.Extensions.Middleware;

namespace Middleware.Demo
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) => services.AddControllersWithViews();

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Home/Error");

            app.UseStaticFiles();

            // 例外處理中介程序
            app.UseExceptionHandleMiddleware()
               // 使用 Content Security Policy 中介程序
               .UseCsp(options =>
               {
                   options.Frames.Disallow();
                   options.FrameAncestors.Disallow();
                   options.Styles.AllowSelf();
                   options.Scripts.AllowSelf();
                   // options.Styles.AllowSelf().Allow("'unsafe-inline'"); -> 若要允許 css inline 程式碼，可以寫這樣...
                   // options.Scripts.AllowSelf().Allow("'unsafe-inline'").Allow("'unsafe-eval'"); -> 若要允許 js inline 程式碼或讓它可執行，可以寫這樣...
               })
               // 使用 X-Frame-Options 中介程序
               .UseXFrame(options => options.XFrame.Deny());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
