
namespace SupportBank {
     public class Bank
    {
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
        if (string.IsNullOrWhiteSpace(userinput))
        {
            throw new ArgumentException("Account name cannot be empty.");
        }

        if(UserAccounts.ContainsKey(userinput))
        {
            PersonAccount person = UserAccounts[userinput];
            List<Transaction> UserTransactions = person.Transactions;

            foreach (var transaction in UserTransactions)
            {
                Console.WriteLine($"{transaction.Date}, {transaction.From}, {transaction.To}, {transaction.Narrative}, {ConvertBalance.SafeConvertBalance(transaction.Amount)}");   
            }
        }
        else
        {
            Console.WriteLine($"No account found for {userinput}.");
        }
    }    
    }
}