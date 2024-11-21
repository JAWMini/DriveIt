namespace DriveIt.EmailSenders
{
    public interface IGeneralEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
