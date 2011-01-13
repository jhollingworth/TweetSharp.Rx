using Dimebrain.TweetSharp.UnitTests.Fluent.Twitter;

namespace TestConsole
{
    class Program
    {
        static void Main()
        {
            var tests = new FluentTwitterTests();

            var methods = tests.GetType().GetMethods();
            foreach(var method in methods)
            {
                method.Invoke(tests, null);
            }
        }
    }
}
