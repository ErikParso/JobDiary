using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobDiary.Services
{
    public interface ILoginService
    {
        MobileServiceUser User { get; }

        Task<bool> Authenticate();
    }
}
