namespace RoslynCodeGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ClassGenerator classGenerator = new ClassGenerator(@"using System;
namespace Igloo
{
    [MessageInfoData((uint)12345)]
    public class MyMessage : MessageInfoBase
    {
        public override void ExecuteMessageCore(IMessageHandler handler)
        {
            handler.HandleMyMessage(this);
        }
    }
}");
            var n = classGenerator.ClassName;
        }
    }
}