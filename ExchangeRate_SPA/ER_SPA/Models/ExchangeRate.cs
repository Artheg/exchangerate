using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ER_SPA.Models
{
    [Serializable]
    public class ExchangeRate
    {
        [BsonId]
        public string date;
        public Dictionary<string, float> ratesByID = new Dictionary<string, float>();
    }
}
