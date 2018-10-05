using Microsoft.WindowsAzure.MobileServices;
using MyJobDiary.Model;
using System.Threading.Tasks;

namespace MyJobDiary.Services
{
    public interface ILoginService
    {
        Task<bool> Login(MobileServiceClient client);

        Task Logout(MobileServiceClient client);

        Task<AppServiceIdentity> GetUserInformation(MobileServiceClient client);

        string Log { get; }
    }
}
