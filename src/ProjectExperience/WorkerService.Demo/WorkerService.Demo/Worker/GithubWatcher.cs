using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WorkerService.Demo.Core;

namespace WorkerService.Worker
{
    public class GithubWatcher : BackgroundService
    {
        private readonly ILogger<GithubWatcher> logger;
        private readonly IGithubWatcherSettings githubWatcherSettings;
        private readonly IHttpClientFactory clientFactory;

        #region Constructor

        public GithubWatcher(ILogger<GithubWatcher> logger, IGithubWatcherSettings githubWatcherSettings, IHttpClientFactory clientFactory)
        {
            this.logger = logger;
            this.githubWatcherSettings = githubWatcherSettings;
            this.clientFactory = clientFactory;
        }

        #endregion

        /// <summary>
        /// 覆寫 BackgroundService.ExecuteAsync
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("GithubWatcher running at: {time}", DateTimeOffset.Now);

                var userInfo = await GetUserInfo(githubWatcherSettings.GithubWatcher.User);

                logger.LogInformation(userInfo);

                await Task.Delay(githubWatcherSettings.GithubWatcher.DelayTime, stoppingToken);
            }
        }

        #region Private Method

        /// <summary>
        /// 取得 Github 使用者資訊
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> GetUserInfo(string user)
        {
            var client = clientFactory.CreateClient("Github");

            var response = await client.GetAsync(string.Format("users/{0}", user));

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        #endregion
    }
}
