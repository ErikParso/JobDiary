namespace MyJobDiary.Services
{
    public interface IDialogService
    {
        void ShowDialog(string title, string message, string icon = null);
    }
}
