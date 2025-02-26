using System;
using System.Globalization;
using System.IO;
using System.Transactions;
using CsvHelper;
using CsvHelper.Configuration;

namespace SupportBank {
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