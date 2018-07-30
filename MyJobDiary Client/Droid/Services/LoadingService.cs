using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyJobDiary.Services;
using Xamarin.Forms.Platform.Android;

namespace MyJobDiary.Droid.Services
{
    public class LoadingService : ILoadingService
    {
        private ProgressDialog progress;

        public LoadingService(FormsApplicationActivity mainActivity)
        {
            progress = new ProgressDialog(mainActivity);
            progress.Indeterminate = true;
            progress.SetProgressStyle(ProgressDialogStyle.Spinner);
            progress.SetCancelable(false);
        }

        public void StartLoading(string message)
        {
            progress.SetMessage(message);
            progress.Show();
        }

        public void StopLoading()
        {
            progress.Hide();
        }
    }
}