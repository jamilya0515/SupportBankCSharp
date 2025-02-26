using System;
using System.Globalization;
using System.IO;
using System.Transactions;
using CsvHelper;
using CsvHelper.Configuration;

namespace SupportBank {
    public class PersonAccount (string name, int initialBalance = 0)
    {
        public string Name { get; set; }  = name;
        public int Balance { get; set; }  = initialBalance;    
        public List<Transaction> Transactions {get; }= new List<Transaction>();
        
        public void AddTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
        }
        public int getBalance(){
            return Balance;
        }
    }
}

