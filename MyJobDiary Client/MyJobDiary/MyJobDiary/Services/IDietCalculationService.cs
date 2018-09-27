using MyJobDiary.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyJobDiary.Services
{
    public interface IDietCalculationService
    {
        Task RecalculateDiets(IEnumerable<Shift> shifts);

        Task<Dictionary<string, double>> GetMonthDiets(int year, int month);
    }
}
