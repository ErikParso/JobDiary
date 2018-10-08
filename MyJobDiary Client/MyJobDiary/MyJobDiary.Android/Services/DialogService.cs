using Android.App;
using Android.Graphics.Drawables;
using MyJobDiary.Services;
using System;
using Xamarin.Forms.Platform.Android;

namespace MyJobDiary.Droid.Services
{
    public class DialogService : IDialogService
    {
        private FormsAppCompatActivity _mainActivity;

        public DialogService(FormsAppCompatActivity mainActivity)
        {
            _mainActivity = mainActivity;
        }

        public void ShowDialog(string title, string message, string icon = null)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(_mainActivity);
            builder.SetMessage(message);
            builder.SetTitle(title);
            if (!string.IsNullOrEmpty(icon))
                builder.SetIcon(_mainActivity.GetDrawable(icon));
            builder.Create().Show();
        }
    }
}