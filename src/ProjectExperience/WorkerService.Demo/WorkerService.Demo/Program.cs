using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkerService.Demo.Core;
using WorkerService.Demo.Intermediaries;
using WorkerService.Worker;

namespace WorkerService.Demo
{
    public static class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    #region 組態配置

                    var configuration = hostContext.Configuration;

                    // 註冊組態設定實例
                    services.Configure<WorkerSettings>(configuration.GetSection("WorkerSettings"));

                    // 注入經橋接後的 WorkerSettings
                    services.AddSingleton<IWorkerSettingsResolved, WorkerSettingsBridge>();

                    // 從已注入之 WorkerSettings 取得各自所需實例 (介面隔離原則)
                    services.AddSingleton<IGithubWatcherSettings>(provider => provider.GetService<IWorkerSettingsResolved>());
                    services.AddSingleton<IOtherWorkerSettings>(provider => provider.GetService<IWorkerSettingsResolved>());

                    #endregion

                    // 註冊 Github HttpClient
                    services.AddHttpClient("Github", client =>
                    {
                        client.BaseAddress = new Uri(configuration.GetSection("Url:Github").Value);
                        client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                        client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
                    });

                    // 配置委託服務
                    ConfigureHostedService(services);
                })
                .UseWindowsService();

        /// <summary>
        /// 配置委託服務
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigureHostedService(IServiceCollection services)
        {
            // GithubWatcher
            services.AddHostedService<GithubWatcher>();

            // OtherWorker
            services.AddHostedService<OtherWorker>();
        }
    }
}
