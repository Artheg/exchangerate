using ER_SPA.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Nager.Date;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ER_SPA.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly IMongoDatabase database;

        public ExchangeRateService()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            database = mongoClient.GetDatabase("rates");
        }

        public IActionResult Get(string date)
        {
            string[] dateArr = date.Split(' ');
            string year = dateArr.Length > 1 ? dateArr[1] : "";
            if (year.Length == 0)
                return new BadRequestResult();

            DateTime dateTime = Convert.ToDateTime(date);
            while (DateSystem.IsPublicHoliday(dateTime, CountryCode.CZ) || DateSystem.IsWeekend(dateTime, CountryCode.CZ))
            {
                dateTime = dateTime.AddDays(-1);
            }
            date = dateTime.ToString("dd.MMM yyyy"); //Convert back for comparison
            List<ExchangeRate> result = database.GetCollection<ExchangeRate>(dateTime.Year.ToString()).Find(a => a.date == date).ToList();
            if (result.Count > 0)
                return new OkObjectResult(result[0]);
            else
                return new NotFoundResult();
        }
    }
}

public interface IExchangeRateService
{
    IActionResult Get(string date);
}