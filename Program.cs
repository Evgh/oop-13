using System;
using System.IO;

namespace oop_13
{
    class Program
    {
        delegate void log(string str);
        event log Notify = KENLogger.Log;

        class KENLogger
        {
            public static void Log(string message)
            {
                using(StreamWriter sw = new StreamWriter("LogFile.txt", true))
                {
                    sw.Write(DateTime.Now);
                    sw.Write(' ');
                    sw.Write(message);
                    sw.WriteLine();
                }
            }




        }




        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
