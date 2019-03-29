using ER_SPA.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Nager.Date;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ER_SPA.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IMongoDatabase database;

        public CurrencyService()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            database = mongoClient.GetDatabase("ids");
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

            List<Currency> result = database.GetCollection<Currency>(dateTime.Year.ToString()).Find(a => true).ToList();

            if (result.Count > 0)
                return new OkObjectResult(result);
            else
                return new NotFoundResult();
        }
    }

    public interface ICurrencyService
    {
        IActionResult Get(string date);
    }
}
