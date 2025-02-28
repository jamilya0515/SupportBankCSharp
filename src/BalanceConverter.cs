using NLog;
using NLog.Config;
using NLog.Targets;

namespace SupportBank {
    public static class ConvertBalance {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public static string SafeConvertBalance(int balance){
            double doubleBalance = balance;
            return $"£{doubleBalance/100}";
        }
    }
}