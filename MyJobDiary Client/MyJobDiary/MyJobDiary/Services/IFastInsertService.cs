using MyJobDiary.Model;
using System.Threading.Tasks;

namespace MyJobDiary.Services
{
    public interface IFastInsertService
    {
        Task<(Insertion, Shift)> InsertFast();
    }
}
