using Microsoft.WindowsAzure.MobileServices;
using MyJobDiary.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyJobDiary.Services
{
    public class AccountInformationService : IAccountInformationService
    {
        private readonly MobileServiceClient _mobileServiceClient;

        public AccountInformationService(MobileServiceClient mobileServiceClient)
        {
            _mobileServiceClient = mobileServiceClient;
        }

        public string Email { get; private set; }

        public string PhotoUrl { get; private set; }

        public async Task LoadInformation()
        {
            var info = (await _mobileServiceClient.InvokeApiAsync<List<AppServiceIdentity>>("/.auth/me")).First();
            Email = info.UserClaims[4].Value;
            PhotoUrl = info.UserClaims[8].Value;
        }
    }
}
