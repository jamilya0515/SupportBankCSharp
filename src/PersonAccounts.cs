using NLog;
using NLog.Config;
using NLog.Targets;

namespace SupportBank {
    public class PersonAccount 
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public PersonAccount(string name, int initialBalance) {
            this.Name = name;
            this.Balance = initialBalance;
            this.Transactions = new List<Transaction>();
        }

        public PersonAccount(string name) {
            this.Name = name;
            this.Balance = 0;
            this.Transactions = new List<Transaction>();
        }
        public string Name { get; set; } 
        public int Balance { get; private set; }    
        public List<Transaction> Transactions {get; }
        
        public void AddTransaction(Transaction transaction)
        { 
            Transactions.Add(transaction);
            if(transaction.From == Name){
                Balance -= transaction.Amount;
            } else if(transaction.To == Name){
                Balance += transaction.Amount;
            }
        }
        public string getBalance(){
            return ConvertBalance.SafeConvertBalance(Balance);
        }
    }
}

