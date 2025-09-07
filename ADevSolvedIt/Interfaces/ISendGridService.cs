namespace ADevSolvedIt.Interfaces
{
    public interface ISendGridService
    {
        Task SendEmailAsync(string emailBody, string postTitle);
    }
}
