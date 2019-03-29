using ExchangeRate.Models;
using System.Collections.Generic;

namespace ExchangeRate.Parsers
{
    static public class StringParser
    {
        public static IEnumerable<CurrencyData> ParseMultipleCurrencyData(string[] str)
        {
            for (int i = 1; i < str.Length; i++)
            {
                CurrencyData currencyData = new CurrencyData();
                currencyData.order = i;
                currencyData.name = str[i];
                yield return currencyData;
            }
        }

        public static IEnumerable<ExchangeRateData> ParseMultipleExchangeRateData(string[] str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                yield return ParseExchangeRate(str[i]);
            }
        }

        public static ExchangeRateData ParseExchangeRate(string str)
        {
            string[] arr = str.Split('|');
            string date = "";
            Dictionary<string, float> rates = new Dictionary<string, float>();
            for (int i = 0; i < arr.Length; i++)
            {
                string data = arr[i];
             
                if (i == 0)
                    date = data;
                else
                    rates[i.ToString()] = float.Parse(data);
            }

            ExchangeRateData exchangeRate = new ExchangeRateData();
            exchangeRate.date = date;
            exchangeRate.ratesByID = rates;
            return exchangeRate;
        }
    }
}
