using System;

namespace ObserverPattern
{
    class MainClass
    {
        public static void Main (string[] args)
        {
            Console.WriteLine ("Observer/Subject pattern.");

            Test test = new Test();
            test.Run();
        }
    }
}
