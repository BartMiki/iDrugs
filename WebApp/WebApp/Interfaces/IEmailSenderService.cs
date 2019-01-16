using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Interfaces
{
    public interface IEmailSenderService
    {
        Result SendEmail(string sendTo, string topic, string content);
    }
}
