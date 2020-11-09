using System;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace DigitalSimulation
{
    class Program
        //Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ConfigureLogger();

            Log.Write("Program uruchomiony");
 

           // Window4 wyk2 = new Window4();
          //  wyk2.ShowDialog();

          //  Window4 wyk3 = new Window4(5);
         //   wyk3.ShowDialog();

            GC.Collect();
            Restaurant restaurant = new Restaurant();
            restaurant.Simulation();
            Console.ReadKey();


        }


        private static void ConfigureLogger()
        {
            var config = new LoggingConfiguration();

            var fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);

            fileTarget.FileName = "${basedir}/Log/log.txt";
            fileTarget.Layout = @"[${date:format=yyyy-MM-dd HH\:mm\:ss.fff}] ${message}";
            fileTarget.ArchiveAboveSize = 1000000;

            var rule = new LoggingRule("*", LogLevel.Debug, fileTarget);
            config.LoggingRules.Add(rule);

            LogManager.Configuration = config;
        }

      
    }
}
