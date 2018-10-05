using MyJobDiary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyJobDiary.ViewModel
{
    public class MainPageViewModel : ObservableObject
    {
        private readonly IDietCalculationService _dietCalculationService;

        private string _thisMonthReward;

        public MainPageViewModel(IDietCalculationService dietCalculationService)
        {
            _dietCalculationService = dietCalculationService;
        }

        public async Task Reload()
        {
            ThisMonthReward =
                BuildRewardString(await _dietCalculationService.GetMonthDiets(DateTime.Now.Year, DateTime.Now.Month));
        }

        public string ThisMonthReward
        {
            get => _thisMonthReward;
            set => SetField(ref _thisMonthReward, value);
        }

        private string BuildRewardString(Dictionary<string, double> diets)
            => "Diéty za tento mesiac: " + string.Join(", ", diets.Select(d => $"{d.Value} {d.Key}"));
    }
}
