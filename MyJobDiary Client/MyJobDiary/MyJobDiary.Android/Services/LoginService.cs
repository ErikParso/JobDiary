using Microsoft.WindowsAzure.MobileServices;
using MyJobDiary.Services;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.Android;

namespace MyJobDiary.Droid.Services
{
    public class LoginService : ILoginService
    {
        private FormsAppCompatActivity _mainActivity;

        public MobileServiceUser User { get; private set; }

        public LoginService(FormsAppCompatActivity mainActivity)
        {
            _mainActivity = mainActivity;
        }

        public async Task<bool> Login()
        {
            User = await MyClient.Current.Value.LoginAsync(_mainActivity,
                MobileServiceAuthenticationProvider.Google, "myjobdiary");
            return (User != null);
        }

        public async Task Logout()
        {
            await MyClient.Current.Value.LogoutAsync();
            User = null;
        }
    }
}