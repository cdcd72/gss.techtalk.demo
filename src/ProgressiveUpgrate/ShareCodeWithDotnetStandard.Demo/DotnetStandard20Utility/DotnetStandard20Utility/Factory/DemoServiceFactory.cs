using DotnetStandard20Utility.Enum;
using DotnetStandard20Utility.Service;

namespace DotnetStandard20Utility.Factory
{
    public class DemoServiceFactory : IDemoServiceFactory
    {
        private readonly DemoType demoType;

        public DemoServiceFactory(DemoType demoType)
        {
            this.demoType = demoType;
        }

        public IDemoService Create()
        {
            IDemoService demoService;

            switch (demoType)
            {
                case DemoType.Main:
                    demoService = new DemoService();
                    break;
                case DemoType.Another:
                    demoService = new AnotherDemoService();
                    break;
                default:
                    demoService = null;
                    break;
            }

            return demoService;
        }
    }
}
