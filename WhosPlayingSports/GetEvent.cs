
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WhosPlayingSports
{
    public static class GetEvent
    {
        [FunctionName("GetEvent")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req,
            ILogger log)
        {
            var response = req.GetQueryParameterDictionary();

            if (response.TryGetValue("responseCode", out var responseCode))
            {
                if(responseCode == "hello_world")
                {
                    return new OkObjectResult(new
                    {
                        EventDateAndTime = DateTime.Now,
                        Location = "Here",
                        MyResponse = "Yes",
                        Responses = new[] {
                        new { Name= "Carlos", Response = "Playing"},
                         new { Name= "Lorena", Response = "NotPlaying"}
                        }
                    });
                }
               
            }
            return new BadRequestResult();
        }
        
        public class EventTableEntity
        {
            public string PartitionKey { get; set; }
            public string RowKey { get; set; }
            public DateTime EventDateAndTime { get; set; }
            public string Location { get; set; }
            public string ResponsesJson { get; set; }
        }

        public class Response
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string IsPlaying { get; set; }
            public string ResponseCode { get; set; }
        }
    }
}
