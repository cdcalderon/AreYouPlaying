
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
using System.Collections.Generic;
using System.Net.Http;

namespace WhosPlayingSports
{
    public static class CreateEvent
    {
        [FunctionName("CreateEvent")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", 
            Route = null)]HttpRequestMessage req, ILogger log, IAsyncCollector<EmailDetails> emailsQueue)
        {
            var eventDetails = await req.Content.ReadAsAsync<EventDetails>();
            foreach(var invitee in eventDetails.Invitees)
            {
                log.LogInformation($"Inviting {invitee.Name} ({invitee.Email})");
                var accessCode = Guid.NewGuid().ToString("n");
                var amailDetails = new EmailDetails
                {
                    EventDateAndTime = eventDetails.EventDateAndTime,
                    Location = eventDetails.Location,
                    Name = invitee.Name,
                    Email = invitee.Email,
                    ResponseUrl = $"https://whosplayingsportfuncs.azurewebsites.net"
                };
            }

            return req.CreateResponse(System.Net.HttpStatusCode.OK);
        }
    }

    public class EventDetails
    {
        public DateTime EventDateAndTime { get; set; }
        public string Location { get; set; }
        public List<InviteeDetails> Invitees { get; set; }
    }

    public class InviteeDetails
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class EmailDetails
    {
        public DateTime EventDateAndTime { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ResponseUrl { get; set; }
    }
}

