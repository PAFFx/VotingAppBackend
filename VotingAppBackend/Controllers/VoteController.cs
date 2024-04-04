using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VotingAppBackend.Services;


namespace VotingAppBackend.Controllers
{
    public class VoteController : Controller
    {
        private readonly MongoService _mongoService;

        public VoteController(MongoService mongoService) {
            _mongoService = mongoService;
        }
     }
}
