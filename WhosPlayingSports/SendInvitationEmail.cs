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
        public static void Run([QueueTrigger("myqueue-items", Connection = "AzureWebJobsStorage")]EmailDetails myQueueItem,
            [SendGrid(ApiKey = "SendGridApiKey")] out SendGridMessage message,
            OutgoingEmail email,
            ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            message = new SendGridMessage();
            message.AddTo("cdcalderon@gmail.com");
            message.AddContent("text/html", myQueueItem.Name + "exito");
            message.SetFrom(new EmailAddress("cdcalderon@gmail.com"));
            message.SetSubject(email.Subject);
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
