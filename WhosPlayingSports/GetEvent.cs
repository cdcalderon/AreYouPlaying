
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
using TicTacTec.TA.Library;

namespace WhosPlayingSports
{
    public static class GetEvent
    {
        [FunctionName("GetEvent")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req,
            ILogger log)
        {
            int outBegIndex = 0;
            int outNbElement = 0;
           
            var closePrices = new float[] {1,2,3,47,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23 };
            var outMovingAverages = new double[closePrices.Length];
            var resultState = TicTacTec.TA.Library.Core.MovingAverage(
                0,
                closePrices.Length - 1,
                closePrices, 10,
                Core.MAType.Ema,
                out outBegIndex,
                out outNbElement,
                outMovingAverages);

            var response = req.GetQueryParameterDictionary();

            if (response.TryGetValue("responseCode", out var responseCode))
            {
                if(responseCode == "hello_world")
                {
                    return new OkObjectResult(new
                    {
                        EventDateAndTime = DateTime.Now,
                        Location = "Here " + outMovingAverages[0],
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
