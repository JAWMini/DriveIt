using SendGrid;
using SendGrid.Helpers.Mail;

namespace DriveIt.EmailSenders
{
    public class SendGridEmailSender : IGeneralEmailSender
    {
        private readonly string _sendGridApiKey;

        public SendGridEmailSender(string sendGridApiKey)
        {
            _sendGridApiKey = sendGridApiKey ?? throw new ArgumentNullException(nameof(sendGridApiKey));
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SendGridClient(_sendGridApiKey);
            // TODO: Ustawienie adresu nadawcy
            var from = new EmailAddress("no-reply@driveit.com", "DriveIt");
            var to = new EmailAddress(email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);

            var response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"SendGrid error: {response.StatusCode}");
            }
        }
    }
}
