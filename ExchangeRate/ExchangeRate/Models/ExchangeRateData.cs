using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ExchangeRate.Models
{
    [Serializable]
    public class ExchangeRateData
    {
        [BsonId]
        public string date;
        public Dictionary<string, float> ratesByID = new Dictionary<string, float>();
    }
}
