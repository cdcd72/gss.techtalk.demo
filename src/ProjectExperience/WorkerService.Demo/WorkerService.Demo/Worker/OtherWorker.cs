using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WorkerService.Demo.Core;

namespace WorkerService.Worker
{
    public class OtherWorker : BackgroundService
    {
        private readonly ILogger<OtherWorker> logger;
        private readonly IOtherWorkerSettings otherWorkerSettings;

        public OtherWorker(ILogger<OtherWorker> logger, IOtherWorkerSettings otherWorkerSettings)
        {
            this.logger = logger;
            this.otherWorkerSettings = otherWorkerSettings;
        }

        /// <summary>
        /// 覆寫 BackgroundService.StartAsync
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("OtherWorker starting at: {time}", DateTimeOffset.Now);

            // 下行代碼為必要，因為還有繼承類需要處理的事情
            await base.StartAsync(cancellationToken);
        }

        /// <summary>
        /// 覆寫 BackgroundService.ExecuteAsync
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("OtherWorker running at: {time}", DateTimeOffset.Now);

                // do your think...

                await Task.Delay(otherWorkerSettings.OtherWorker.DelayTime, stoppingToken);
            }
        }

        /// <summary>
        /// 覆寫 BackgroundService.StopAsync
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("OtherWorker stopping at: {time}", DateTimeOffset.Now);

            // 下行代碼為必要，因為還有繼承類需要處理的事情
            await base.StopAsync(cancellationToken);
        }

        #region Private Method

        #endregion
    }
}
