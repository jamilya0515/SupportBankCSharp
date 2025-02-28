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
    public static class CsvValidator
    {
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

    public static bool ValidateCsvFormat(string filePath)
    {   
        string[] expectedHeaders = ["date","from", "to", "narrative", "amount"];
        Logger.Debug($"ValidateCsvFormat called with filePath: {filePath}");

        if (!File.Exists(filePath))
        {
            Logger.Error($"File not found: {filePath}");
            return false;
        }

        try
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower(),
                HasHeaderRecord = true
            };
            
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Read();
                csv.ReadHeader();
                if(csv.HeaderRecord != null){


                // Header Validation
                for (int i = 0; i < expectedHeaders.Length; i++)
                {
                    if (csv.HeaderRecord[i].ToLower() != expectedHeaders[i].ToLower())
                    {
                        Logger.Error($"Header mismatch: Expected '{expectedHeaders[i]}', found '{csv.HeaderRecord[i]}'");
                        return false;
                    }
                }

                // Data Type Validation and Row/Column Count Validation
                while (csv.Read())
                {   
                    
                    if (csv.HeaderRecord.Length != expectedHeaders.Length)
                    {
                        Logger.Error($"Incorrect column count in row {csv.Context.Parser.Row}.");
                        return false;
                    }
                    // Validate a date column
                    try
                    {
                        if (!string.IsNullOrEmpty(csv.GetField(0)))
                        {
                            DateTime.Parse(csv.GetField(0));
                        }
                    }
                    catch (FormatException)
                    {
                        Logger.Error($"Invalid date format in row {csv.Context.Parser.Row}.");
                        return false;
                    }
                    // Validate the Amount columnt
                    try 
                    {
                        if (!string.IsNullOrEmpty(csv.GetField(4))) 
                        {
                            double.Parse(csv.GetField(4));
                        }
                    }
            
                    catch (FormatException)
                    {
                        Logger.Error($"Invalid amount format in row {csv.Context.Parser.Row}.");
                        return false;
                    }
                }
                }

                Logger.Info($"CSV file format validated successfully: {filePath}");
                return true;
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, $"An error occurred during CSV validation: {filePath}");
            return false;
        }
    }
    }
}

