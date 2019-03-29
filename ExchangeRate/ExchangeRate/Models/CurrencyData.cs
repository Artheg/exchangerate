using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ExchangeRate.Models
{
    [Serializable]
    public class CurrencyData
    {
        [BsonId]
        public string name;
        public int order;
    }
}
