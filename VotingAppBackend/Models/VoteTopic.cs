using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Net.Sockets;

namespace VotingAppBackend.Models
{
    public class Option
    {
        public string? optionName { get; set; }
        public int? voteCount {  get; set; }
    }
    public class Topic
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("topicName")]
        public string? topicName { get; set; }

        [BsonElement("description")]
        public string? description { get; set; }

        [BsonElement("options")]
        public List<Option>? options {  get; set; }    
    }



    public class PostTopicBody
    {
        [BsonElement("topicName")]
        public string? topicName { get; set; }

        [BsonElement("description")]
        public string? description { get; set; }

        [BsonElement("options")]
        public List<Option>? options {  get; set; }
    }

    public class PatchVoteBody {
        public int? optionIndex { get; set; } 
    }

}
