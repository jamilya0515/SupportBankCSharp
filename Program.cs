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
            // foreach (var transaction in transactions)
            // {
            //     Console.WriteLine($"{transaction.Date}, {transaction.From}, {transaction.To}, {transaction.Narrative}, {transaction.Amount}");
            // }
            Bank myBank = new Bank();
            myBank.BankAddTransaction(transactions);
            myBank.ListAllAccounts();
      
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

//Edit your program so that it creates an account for each person, and then keeps track of how much each person owes / is owed.
//Print out the names of each person, along with the total amount they owe, or are owed.//

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
            // Create accounts if they don't exist
                if (!UserAccounts.ContainsKey(transaction.From))
                {
                    UserAccounts[transaction.From] = new PersonAccounts { Name = transaction.From };
                }
                if (!UserAccounts.ContainsKey(transaction.To))
                {
                    UserAccounts[transaction.To] = new PersonAccounts { Name = transaction.To };
                }  

            // Update balances
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
                Console.WriteLine($"{account.Name}: {account.Balance:C}"); // "C" for currency format
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
