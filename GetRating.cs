using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace RatingsApp
{
    public static class GetRating
    {
  
        [FunctionName("GetRating")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "ratings/{id}")] HttpRequest req,
            [CosmosDB("ratings", "ratings",
                ConnectionStringSetting = "CosmosDBConnection",
                SqlQuery = "select * from ratings r where r.id = {id}")]
                IEnumerable<RatingItem> ratingItems,
            ILogger log)
        {
            var json = "";
            foreach (var ratingItem in ratingItems)
            {
                json = JsonConvert.SerializeObject(ratingItem);
                
                break;
            }
            return new OkObjectResult(json);
        }
    }

    public class RatingItem 
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string LocationName { get; set; }
        public string ProductId { get; set; }
        public int Rating { get; set; }
        public string UserNotes { get; set; }
        public string TimeStamp { get; set; }
    }
}
