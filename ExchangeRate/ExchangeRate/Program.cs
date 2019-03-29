using ExchangeRate.Database;
using ExchangeRate.Helpers;
using ExchangeRate.Models;
using ExchangeRate.Network;
using ExchangeRate.Parsers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRate.Root
{
    class MainClass
    {
        private static string uri = "https://www.cnb.cz/en/financial_markets/foreign_exchange_market/exchange_rate_fixing/year.txt?year=";
        private static string year = "";
        private static string response = null;
        private static DatabaseManager databaseManager;

        public static void Main(string[] args)
        {
            Start();
        }

        private static void DisplayWarning()
        {
            string warning = "Warning \n" + "This app will try to connect to mongodb with mongodb://localhost:27017;\n";
            warning += "It will then try to create two databases: 'ids' and 'rates' and populate them with objects which ";
            warning += "are represented by \n" + "models inside Models folder. I hope you're ok with that.";
            Console.WriteLine(warning);
            Console.WriteLine("");
            Console.WriteLine("Hit Enter to continue");
            Console.ReadLine();
        }

        private static void Start()
        {
            DisplayWarning();

            databaseManager = new DatabaseManager("mongodb://localhost:27017");

            GetYearParameter();

            GetData().Wait();

            ParseAndWriteData();

            Console.WriteLine("DONE");
            Console.ReadLine();
        }

        private static async Task GetData()
        {
            Console.WriteLine("Trying to get data from " + uri + year);
            response = await RequestHandler.GetData(uri, year);
        }

        private static void GetYearParameter()
        {
            Console.WriteLine("Enter year:");
            year = Console.ReadLine();
            if (!year.All(char.IsDigit) || year.Length == 0)
            {
                Console.WriteLine("Invalid year parameter: " + year);
                GetYearParameter();
            }
        }

        private static void TryRestart()
        {
            Console.WriteLine("Invalid response : " + response);
            Console.WriteLine("Should we try again? yes/no");
            string userInput = Console.ReadLine();
            if (userInput.Contains('y'))
                Start();
        }

        private static void ParseAndWriteData()
        {
            string[] arr = StringHelper.SplitByNewLine(response);

            if (arr.Length < 2)
            {
                TryRestart();
                return;
            }

            Console.WriteLine("Parsing Currency Data...");
            List<CurrencyData> currencyData = StringParser.ParseMultipleCurrencyData(arr[0].Split('|')).ToList();
            Console.WriteLine("Parsing Exchange Rate Data...");
            List<ExchangeRateData> exchangeRates = StringParser.ParseMultipleExchangeRateData(arr.Skip(1).ToArray()).ToList();

            databaseManager.InsertMany(currencyData, "ids", year);
            databaseManager.InsertMany(exchangeRates, "rates", year);
        }
 
    }
}
