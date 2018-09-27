using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MyJobDiary.Services
{
    public interface ILocationService
    {
        Task<Placemark> GetLocation();
    }
}
