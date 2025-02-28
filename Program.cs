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
    public class Program {

        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {   
            var config = new LoggingConfiguration();
            var target = new FileTarget { FileName = @"C:\Users\JamAit\Projects\SupportBankC#\logs\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}", CreateDirs = true };
            config.AddTarget("File Logger", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
            LogManager.Configuration = config;

            string filepath = "./DodgyTransactions2015.csv";
            var transactions = ReadFile.ReadCsvFile(filepath);
            Bank myBank = new Bank();
            myBank.BankAddTransaction(transactions);
            myBank.ListAllAccounts();
            myBank.ListAccount();
        }
    }
}

