using Microsoft.WindowsAzure.MobileServices;
using MyJobDiary.Managers;
using MyJobDiary.Services;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.Android;

namespace MyJobDiary.Droid.Services
{
    public class LoginService : ILoginService
    {
        private FormsApplicationActivity _mainActivity;

        public MobileServiceUser User { get; private set; }

        public LoginService(FormsApplicationActivity mainActivity)
        {
            _mainActivity = mainActivity;
        }

        public async Task<bool> Authenticate()
        {
            // Sign in with Facebook login using a server-managed flow.
            User = await ShiftItemManager.Current.Value.CurrentClient.LoginAsync(_mainActivity,
                MobileServiceAuthenticationProvider.Google, "myjobdiary");
            return (User != null);
        }
    }
}