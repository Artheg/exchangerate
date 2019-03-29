using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace ExchangeRate.Database
{
    class DatabaseManager
    {
        private MongoClient mongoClient;
        private string connectionString;

        public DatabaseManager(string connectionString)
        {
            this.connectionString = connectionString;
            mongoClient = new MongoClient(this.connectionString);
        }

        public IMongoDatabase GetDatabase(string name)
        {
            return mongoClient.GetDatabase(name);
        }

        public void InsertMany<T>(List<T> list, string db, string collectionName)
        {
            if (list.Count < 1)
            {
                Console.WriteLine("Warning: cannot write empty list " + list.GetType() + " to collection " + collectionName + " of db" + db);
                return;
            }
            var database = mongoClient.GetDatabase(db);
            var collection = database.GetCollection<T>(collectionName);

            try
            {
                collection.InsertMany(list, new InsertManyOptions() { IsOrdered = false });
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }
    }
}
