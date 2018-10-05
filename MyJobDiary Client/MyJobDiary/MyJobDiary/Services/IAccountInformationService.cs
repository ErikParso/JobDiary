using System.Threading.Tasks;

namespace MyJobDiary.Services
{
    public interface IAccountInformationService
    {
        string Email { get; }

        string PhotoUrl { get; }

        Task LoadInformation();
    }
}
