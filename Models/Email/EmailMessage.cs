using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoard.Models.Email
{
    public class EmailAddress
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
    public class EmailMessage
    {
        public EmailAddress ToAddress { get; set; }
        public EmailAddress FromAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
