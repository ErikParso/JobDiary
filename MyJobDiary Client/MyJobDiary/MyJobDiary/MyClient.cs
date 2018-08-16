using Microsoft.WindowsAzure.MobileServices;
using System;

namespace MyJobDiary
{
    public class MyClient : MobileServiceClient
    {
        public static Lazy<MyClient> Current = new Lazy<MyClient>(() => new MyClient());

        private MyClient() : base(Constants.ApplicationURL)
        {

        }
    }
}
