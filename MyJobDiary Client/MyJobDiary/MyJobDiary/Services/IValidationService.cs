using MyJobDiary.Model;
using System.Threading.Tasks;

namespace MyJobDiary.Services
{
    public interface IValidationService
    {
        Task<bool> IsShiftOverlapped(Shift shift);
    }
}
