using brevo_csharp.Api;
using brevo_csharp.Model;
using brevo_csharp.Client;
using PortfolioApi.Interfaces;
using PortfolioApi.Entities;

namespace PortfolioApi.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuraion;
        public EmailService(IConfiguration configuration) 
        { 
            _configuraion = configuration;
        }
        public bool SendEmail(Message message)
        {
            var apiKey = _configuraion["ApiKeys:BrevoApiKey"];
            var senderName = _configuraion["EmailList:SenderName"];
            var senderEmail = _configuraion["EmailList:SenderEmail"];
            var recipientEmail = _configuraion["EmailList:RecipientEmail"];
            var recipientName = _configuraion["EmailList:RecipientName"];

            //Saves my Brevo account details to the Brevo API config to be able to send emails from my account
            brevo_csharp.Client.Configuration.Default.ApiKey.TryAdd("api-key", apiKey);

            var apiInstance = new TransactionalEmailsApi();
            var sendSmtpEmail = new SendSmtpEmail(); // SendSmtpEmail | Values to send a transactional email
            //Sender
            sendSmtpEmail.Sender = new SendSmtpEmailSender(senderName, senderEmail);

            //Recipient
            SendSmtpEmailTo recipient = new SendSmtpEmailTo(recipientEmail, recipientName);
            sendSmtpEmail.To = new List<SendSmtpEmailTo> { recipient };

            //Set email content
            sendSmtpEmail.Subject = $"Contact {message.SenderName} at {message.SenderEmail}";
            sendSmtpEmail.HtmlContent = $"<html><head></head><body><p>Hello,</p>{message.Content}</p></body></html>";

            try
            {
                // Send a transactional email
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
               
                return true;
            }
            catch (Exception e)
            {
                // Should add error handling here at some point
                var errorMessage = e.Message;
                return false;
            }
        }
    }
}
