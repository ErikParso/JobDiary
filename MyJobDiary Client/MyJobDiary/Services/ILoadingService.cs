using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobDiary.Services
{
    public interface ILoadingService
    {
        void StartLoading(string message);

        void StopLoading();
    }
}
