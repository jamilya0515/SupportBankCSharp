using System;
using System.Globalization;
using System.IO;
using System.Transactions;
using CsvHelper;
using CsvHelper.Configuration;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace SupportBank {    
   public static class ReadFile 
        {   
            private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
            public static List<Transaction> ReadCsvFile (string filePath) {
                 List<Transaction> transactions = new List<Transaction>();

            Logger.Debug($"ReadCsvFile called with filePath: {filePath}");
            if(CsvValidator.ValidateCsvFormat(filePath)){
            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    PrepareHeaderForMatch = args => args.Header.ToLower(),
                };
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, config))
                {
                    transactions = csv.GetRecords<Transaction>().ToList();
                }
                    Logger.Info("Data processed successfully.");
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "An error occurred in ReadCsvFile.");
            }
            } 
            return transactions;
        }
    }
}
    
