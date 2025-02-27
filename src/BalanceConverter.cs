
namespace SupportBank {
    public static class ConvertBalance {
        public static string SafeConvertBalance(int balance){
            double doubleBalance = balance;
            return $"£{doubleBalance/100}";
        }
    }
}