namespace GenericTypeAsAFactory.Demo.Services
{
    public class DemoService : IDemoService
    {
        public string GetValue()
        {
            string value = "哈哈~ 是我啦!";
            return value;
        }
    }
}
