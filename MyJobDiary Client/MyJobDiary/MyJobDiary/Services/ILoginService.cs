﻿using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace MyJobDiary.Services
{
    public interface ILoginService
    {
        Task<bool> Login(MobileServiceClient client);
    }
}
