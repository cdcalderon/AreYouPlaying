using System;
using System.Collections.Generic;
using System.Text;

namespace WhosPlayingSports.Models
{
    public class EmailDetails
    {
        public DateTime EventDateAndTime { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ResponseUrl { get; set; }
    }
}
