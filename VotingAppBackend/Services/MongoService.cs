using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using VotingAppBackend.Models;

namespace VotingAppBackend.Services
{
    public class MongoService
    {
        IMongoDatabase? database;
        MongoClient? client;
        public MongoService(IOptions<MongoSettings> mongoSettings)
        {

             client = new MongoClient(mongoSettings.Value.ConnectionUri);
             database = client.GetDatabase(mongoSettings.Value.DatabaseName);

            if (database != null) {
                Console.WriteLine("Database's connection established");
                System.Environment.Exit(1);
            }
        }

        public IMongoCollection<BsonDocument> GetCollection(string collectionName)
        {
            return database!.GetCollection<BsonDocument>(collectionName);
        }

        public List<BsonDocument> ListAllDocuments(string collectionName) {
            var collection = GetCollection(collectionName);
            var docs = collection.Find(new BsonDocument()).ToList();
            return docs;
        }
    }
}
