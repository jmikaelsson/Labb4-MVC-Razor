using Microsoft.AspNetCore.Identity.UI.Services;

namespace Labb4_MVC_Razor.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMassage)
        {
            return Task.CompletedTask;
        }
    }
}
