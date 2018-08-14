using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace MyJobDiary.Services
{
    public interface ILoginService
    {
        MobileServiceUser User { get; }

        Task<bool> Login();

        Task Logout();
    }
}
