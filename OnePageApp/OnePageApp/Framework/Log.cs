using System;
using log4net;

namespace Logs
{
    public static class Log
    {
        private static readonly ILog Logger = LogManager.GetLogger("One page app");

        public static void Add(Exception ex)
        {
            Logger.Error(ex);
        }

        public static void Add(string message)
        {
            Logger.Error(message);
        }
    }
}
