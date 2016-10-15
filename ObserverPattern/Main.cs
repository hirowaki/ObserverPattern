using System;

namespace ObserverPattern
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");
			Test test = new Test();
            test.Run();
		}
	}
}
