using Microsoft.Extensions.Configuration.EnvironmentVariables;
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
            }
        }


        public List<Topic> ListTopics() {
            var collection = database!.GetCollection<Topic>("VoteTopic");


            var docs = collection.Find(_=> true).ToList();

            return docs;
        }

        public Topic? GetTopic(string topicId)
        {
            var collection = database!.GetCollection<Topic>("VoteTopic");

            var filter = Builders<Topic>.Filter.Eq("_id", new ObjectId(topicId));

            var doc = collection.Find(filter).ToList().FirstOrDefault();

            return doc;

        }

        public Boolean CreateTopic(PostTopicBody topic)
        {

            try
            {
                var collection = database!.GetCollection<PostTopicBody>("VoteTopic");
                collection.InsertOne(topic);
                return true;
            }
            catch( Exception ex) 
            {
                Console.WriteLine($"Error posting topic: {ex.Message}");
                return false;
            }
        }

        public Boolean EditVote(string topicId,PatchVoteBody vote)
        {
            try
            {

                var collection = database!.GetCollection<Topic>("VoteTopic");
                int optionIndex = (int)vote.optionIndex;

                var filter = Builders<Topic>.Filter.And(
                    Builders<Topic>.Filter.Eq("_id", new ObjectId(topicId)),
                    Builders<Topic>.Filter.Exists($"options.{optionIndex}")
                   );

                var update = Builders<Topic>.Update.Inc($"options.{optionIndex}.voteCount", 1);

                var result = collection.UpdateOne(filter, update);
                if (result.ModifiedCount > 0) return true;
                else return false;
            }
            catch( Exception ex) 
            {
                Console.WriteLine($"Error posting topic: {ex.Message}");
                return false;
            }
        }

    }
}
