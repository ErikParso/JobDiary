using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace MyJobDiary.Services
{
    public interface ILoginService
    {
        Task<bool> Login(MobileServiceClient client);

        Task Logout(MobileServiceClient client);

        string Log { get; }
    }
}
