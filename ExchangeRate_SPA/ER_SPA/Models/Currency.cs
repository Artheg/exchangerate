using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ER_SPA.Models
{
    [Serializable]
    public class Currency
    {
        [BsonId]
        public string name;
        public int order;
    }
}
