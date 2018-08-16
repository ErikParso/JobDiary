namespace MyJobDiary.Services
{
    public interface ILoadingService
    {
        void StartLoading(string message);

        void StopLoading();
    }
}
