using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;
using WhosPlayingSports.Models;

namespace WhosPlayingSports
{
    public static class SendInvitationEmail
    {
        [FunctionName("SendInvitationEmail")]
        public static void Run([QueueTrigger("emails", Connection = "AzureWebJobsStorage")]EmailDetails myQueueItem,
            [SendGrid(ApiKey = "SendGridApiKey")] out SendGridMessage message,
            ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            message = new SendGridMessage();
            message.AddTo("cdcalderon@gmail.com");
            message.AddContent("text/html", myQueueItem.Name + myQueueItem.Email + "exito");
            message.SetFrom(new EmailAddress("cdcalderon@gmail.com"));
            message.SetSubject("TEST");
        }

        public class OutgoingEmail
        {
            public string To { get; set; }
            public string From { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
        }
    }
}

