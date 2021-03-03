namespace GenericTypeAsAFactory.Demo.Services
{
    public class DemoService : IDemoService
    {
        public string GetValue()
        {
            var value = "哈哈~ 是我啦!";
            return value;
        }
    }
}
