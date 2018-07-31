using System;
using Android.App;
using MyJobDiary.Services;
using Xamarin.Forms.Platform.Android;

namespace MyJobDiary.Droid.Services
{
    public class DialogService: IDialogService
    {
        private FormsApplicationActivity _mainActivity;

        public DialogService(FormsApplicationActivity mainActivity)
        {
            _mainActivity = mainActivity;
        }

        public void ShowDialog(string title, string message)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(_mainActivity);
            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.Create().Show();
        }

        public bool ShowYesNoDialog(string title, string message)
        {
            throw new NotImplementedException();
        }
    }
}