using System;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace SupportBank {
    public class Program {
        static void Main(string[] args)
        {   string filepath = "./Transactions2014.csv";
            var transactions = ReadCsvFile(filepath);
            foreach (var transaction in transactions)
            {
                Console.WriteLine($"{transaction.Date}, {transaction.From}, {transaction.To}, {transaction.Narrative}, {transaction.Amount}");
            }
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

    }
}
