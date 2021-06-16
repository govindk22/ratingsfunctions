using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace RatingsApp
{

    public static class GetRatings
    { 

        [FunctionName("GetRatings")]
        public static IActionResult Run(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ratings")] HttpRequest req,
           [CosmosDB("ratings", "ratings",
                ConnectionStringSetting = "CosmosDBConnection",
                SqlQuery = "select * from ratings")]
                IReadOnlyList<RatingItem> ratings,
           TraceWriter log)
        {
            if (ratings != null && ratings.Count > 0)
            {
                log.Info($"{ratings.Count} ratings found.");
            }
            else
            {
                log.Info($"No ratings found.");
            }
           
            return new OkObjectResult(ratings);
        }


    }
}
