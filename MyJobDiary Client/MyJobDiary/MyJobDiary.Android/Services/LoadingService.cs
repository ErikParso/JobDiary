using Android.App;
using MyJobDiary.Services;
using Xamarin.Forms.Platform.Android;

namespace MyJobDiary.Droid.Services
{
    public class LoadingService : ILoadingService
    {
        private ProgressDialog progress;

        public LoadingService(FormsAppCompatActivity mainActivity)
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