using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using VotingAppBackend.Models;
using VotingAppBackend.Services;

namespace VotingAppBackend.Controllers
{
    [Route("/Vote")]
    [ApiController]
    public class VoteController : Controller
    {
        private readonly MongoService _mongoService;

        public VoteController(MongoService mongoService)
        {
            _mongoService = mongoService;
        }

        private void ConvertObjectIdToString(BsonDocument document)
        {
            if (document.TryGetValue("_id", out BsonValue idValue) && idValue.IsObjectId) {
                document["_id"] = idValue.AsObjectId.ToString();
                    }
        }

        [HttpGet("Topics")]
        public ActionResult<List<Topic>> ListTopics()
        {
            var docs = _mongoService.ListTopics();
            return Json(docs);
        }

        [HttpPost("Topics")]
        public ActionResult PostTopic([FromBody]PostTopicBody input)
        {
            var result = _mongoService.CreateTopic(input);
            if (result) return Json("OK");
            else return Json("Sad");

        }

        [HttpGet("Topics/{topicId}")]
        public ActionResult<Topic> GetTopic(string topicId)
        {
            var doc = _mongoService.GetTopic(topicId);
            if (doc == null)
            {
                return Json("sad");
            }
            return Json(doc);

        }

        [HttpPatch("Topics/{topicId}")]
        public ActionResult PatchVote(string topicId, [FromBody]PatchVoteBody input)
        {
            var result = _mongoService.EditVote(topicId, input);
            if (result) return Json("OK");
            else return Json("Not Ok");
        }

    }
}
