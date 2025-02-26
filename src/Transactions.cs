using System;
using System.Globalization;
using System.IO;
using System.Transactions;
using CsvHelper;
using CsvHelper.Configuration;

namespace SupportBank {
    public class Transaction
    {
        public Transaction(string date, string from, string to, string narrative, string amount) {
            this.Date = date;
            this.From = from;
            this.To = to;
            this.Narrative = narrative;
            this.Amount = (int)float.Parse(amount)*100;
        }
        public string Date { get; }
        public string From { get; } 
        public string To { get; } 
        public string Narrative { get; } 
        public int Amount { get; }  
    }
}