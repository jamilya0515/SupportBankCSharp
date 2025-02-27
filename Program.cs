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
            myBank.ListAllAccounts();
            myBank.ListAccount();
        }
    
        public static List<Transaction> ReadCsvFile(string filePath)
        {   List<Transaction> transactions;
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
            PrepareHeaderForMatch = args => args.Header.ToLower(),
            };
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                transactions = csv.GetRecords<Transaction>().ToList();
            }
            return transactions;
        }
    }
}

