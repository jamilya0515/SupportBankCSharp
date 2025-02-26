using System;
using System.Globalization;
using System.IO;
using System.Transactions;
using CsvHelper;
using CsvHelper.Configuration;

namespace SupportBank {
    public class Program {
        static void Main(string[] args)
        {   string filepath = "./Transactions2014.csv";
            var transactions = ReadCsvFile(filepath);
            Bank myBank = new Bank();
            myBank.BankAddTransaction(transactions);
            // myBank.ListAllAccounts();
            myBank.ListAccount();
        }
    
        public static List<Transactions> ReadCsvFile(string filePath)
        {   List<Transactions> transactions;

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                transactions = csv.GetRecords<Transactions>().ToList();
            }
            return transactions;
        }
 
    public class Transactions 
    {
        public string Date { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;  
        public string Narrative { get; set; } = string.Empty;
        public string Amount { get; set; } = string.Empty;       
    }

    public class PersonAccounts 
    {
        public string Name { get; set; } = string.Empty;      
        public int Balance { get; set; }       
        public List<Transactions> transactions {get; set;} = new List<Transactions>();
        
        public void AddTransaction(Transactions transaction)
        {
        transactions.Add(transaction);
        }
        public int getBalance(){
            return Balance;
        }
    }

    public class Bank
    {
        public Dictionary<string, PersonAccounts> UserAccounts { get; set; } = new Dictionary<string, PersonAccounts>();

            public void BankAddTransaction(List<Transactions> transactions)
            {
                foreach (var transaction in transactions)
            {
                if (!UserAccounts.ContainsKey(transaction.From))
                {
                    UserAccounts[transaction.From] = new PersonAccounts { Name = transaction.From };
                }
                if (!UserAccounts.ContainsKey(transaction.To))
                {
                    UserAccounts[transaction.To] = new PersonAccounts { Name = transaction.To };
                }  

            UserAccounts[transaction.From].Balance -= StringConverters.SafeParseInt(transaction.Amount);
            UserAccounts[transaction.To].Balance += StringConverters.SafeParseInt(transaction.Amount);
            UserAccounts[transaction.From].AddTransaction(transaction);
            UserAccounts[transaction.To].AddTransaction(transaction);
            }
        }

        public void ListAllAccounts()
        {
            foreach (var account in UserAccounts.Values)
            {
                Console.WriteLine($"{account.Name}: {account.Balance:C}"); 
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

            if(UserAccounts.ContainsKey(userinput)){
                PersonAccounts person = UserAccounts[userinput];
                List<Transactions> UserTransactions = person.transactions;

            foreach (var transaction in UserTransactions)
            {
                Console.WriteLine($"{transaction.Date}, {transaction.From}, {transaction.To}, {transaction.Narrative}, {transaction.Amount}");   
            }
            }
            else
            {
                Console.WriteLine($"No account found for {userinput}.");
            }
        }   

        
    }

    public static class StringConverters{

        public static int SafeParseInt(string input, int DefaultValue=0){
            int result;
            if(int.TryParse(input, out result)){
                return result;
            }
            else{
                return DefaultValue;
            }
        }
    } 
}
}
