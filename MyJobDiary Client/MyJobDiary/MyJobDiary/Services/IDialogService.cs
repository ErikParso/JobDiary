namespace MyJobDiary.Services
{
    public interface IDialogService
    {
        void ShowDialog(string title, string message);

        bool ShowYesNoDialog(string title, string message);
    }
}
