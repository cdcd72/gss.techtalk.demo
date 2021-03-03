using Microsoft.Extensions.Logging;

namespace CreatingInstancesFromIServiceProvider.Demo.Services
{
    public class AnotherService
    {
        private readonly ILogger<AnotherService> logger;

        /// <summary>
        /// AnotherService 建構子 (即使該服務並未先行被註冊在 DI 容器中，但還是可以透過相依工廠幫忙注入相依...)
        /// </summary>
        /// <param name="logger"></param>
        public AnotherService(ILogger<AnotherService> logger) => this.logger = logger;

        public string GetValue()
        {
            var value = "_(:3 」∠ )_";
            // 因為有成功注入相依，所以 logger 可以正常運行...
            logger.LogInformation(value);
            return value;
        }
    }
}
