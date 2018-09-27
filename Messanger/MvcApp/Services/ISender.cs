using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApp.Services
{
    public interface ISender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
