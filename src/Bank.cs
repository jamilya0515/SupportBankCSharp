using NLog;
using NLog.Config;
using NLog.Targets;

namespace SupportBank {
     public class Bank
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public Dictionary<string, PersonAccount> UserAccounts { get; set; } = new Dictionary<string, PersonAccount>();

        public void BankAddTransaction(List<Transaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                if (!UserAccounts.ContainsKey(transaction.From))
                {
                    UserAccounts[transaction.From] = new PersonAccount (transaction.From);
                }
                if (!UserAccounts.ContainsKey(transaction.To))
                {
                    UserAccounts[transaction.To] = new PersonAccount (transaction.To)  ;
                }  
                UserAccounts[transaction.From].AddTransaction(transaction);
                UserAccounts[transaction.To].AddTransaction(transaction);
            }
        }

    public void ListAllAccounts()
    {
        foreach (var account in UserAccounts.Values)
        {
            Console.WriteLine($"{account.Name}: {ConvertBalance.SafeConvertBalance(account.Balance)}"); 
        }
    }  

    public void ListAccount()
    {       
        Console.WriteLine("Please enter name of account:");
        string? userinput = Console.ReadLine();
        
        Logger.Debug("ListAccount called with input: {Input}", userinput);
        
        try 
        {   
            if (string.IsNullOrWhiteSpace(userinput))
            {
                throw new ArgumentException("Account name cannot be empty.");
            }
            
            else if (!UserAccounts.ContainsKey(userinput)) 
            {
                throw new ArgumentException($"No account found for {userinput}.");
            }

            else 
            {
                PersonAccount person = UserAccounts[userinput];
                List<Transaction> UserTransactions = person.Transactions;

                foreach (var transaction in UserTransactions)
                {
                    Console.WriteLine(transaction.ToString());
                }
                Logger.Info("Data processed successfully.");
            }
        }
        catch (ArgumentNullException ex)
        {
            Logger.Error(ex, "An error occurred in ListAccount."); 
        }
    }    
    }
}



