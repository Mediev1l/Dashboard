using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoard.Models.Email
{
    public interface IEmailService
    {
        public Task SendAsync(EmailMessage emailMessage);
        //List<EmailMessage> ReceiveEmail(int maxCount = 10);
    }
}
