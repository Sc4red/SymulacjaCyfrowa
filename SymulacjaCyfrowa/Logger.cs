using NLog;

namespace DigitalSimulation
{
    public static class Log
    {
        private static ILogger logger = LogManager.GetLogger("");

        public static void Write(string message)
        {
            logger.Debug(message);
        }
    }
   
}

