using System;
using Microsoft.Extensions.Options;
using WorkerService.Demo.Core;

namespace WorkerService.Demo.Intermediaries
{
    /// <summary>
    /// 橋接 WorkerSettings
    /// </summary>
    public class WorkerSettingsBridge : IWorkerSettingsResolved
    {
        private WorkerSettings workerSettings;

        public WorkerSettingsBridge(IOptionsMonitor<WorkerSettings> workerSettings)
        {
            this.workerSettings = workerSettings.CurrentValue ?? throw new ArgumentNullException(nameof(workerSettings));

            // 當設定組態改變時觸發
            workerSettings.OnChange((WorkerSettings workerSettings) => this.workerSettings = workerSettings);
        }

        public GithubWatcherOption GithubWatcher => workerSettings.GithubWatcher;

        public OtherWorkerOption OtherWorker => workerSettings.OtherWorker;
    }
}
