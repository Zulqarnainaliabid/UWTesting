using System.Threading.Tasks;

namespace Upwork.Testing.Data.Abstractions
{
    public interface IEmailService
    {
        Task SendEmailAsync(string from, string to, string subject, string body);
    }
}
