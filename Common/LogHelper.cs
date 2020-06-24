using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace TD.Common
{
    public class LogHelper
    {
        private static ILog log;
        public static void Error(string name, string msg)
        {

        }
        public static void Info(string name, string msg)
        {

        }
        public static void Error<T>(Exception ex)
        {
            log = LogManager.GetLogger("Api", typeof(T));
            log.Error("", ex);
        }
        public static void Error<T>(string msg)
        {
            log = LogManager.GetLogger("Api", typeof(T));
            log.Error(msg);
        }
    }
}
