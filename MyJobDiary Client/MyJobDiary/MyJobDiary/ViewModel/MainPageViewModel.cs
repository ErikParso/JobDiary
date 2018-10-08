using MyJobDiary.Model;
using MyJobDiary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyJobDiary.ViewModel
{
    public class MainPageViewModel : ObservableObject
    {
        private readonly IDietCalculationService _dietCalculationService;
        private readonly IFastInsertService _fastInsertService;
        private readonly IDialogService _dialogService;

        private string _thisMonthReward;

        public MainPageViewModel(
            IDietCalculationService dietCalculationService,
            IFastInsertService fastInsertService,
            IDialogService dialogService)
        {
            _dietCalculationService = dietCalculationService;
            _fastInsertService = fastInsertService;
            _dialogService = dialogService;

            FastInsertCommand = new Command(FastInsert);
        }

        public async Task Reload()
        {
            ThisMonthReward =
                BuildRewardString(await _dietCalculationService.GetMonthDiets(DateTime.Now.Year, DateTime.Now.Month));
        }


        #region Reward sum

        public string ThisMonthReward
        {
            get => _thisMonthReward;
            set => SetField(ref _thisMonthReward, value);
        }
        private string BuildRewardString(Dictionary<string, double> diets)
            => "Diéty za tento mesiac: " + string.Join(", ", diets.Select(d => $"{d.Value} {d.Key}"));

        #endregion


        #region Fast insert

        public ICommand FastInsertCommand { get; set; }

        private async void FastInsert()
        {
            var insertInfo = await _fastInsertService.InsertFast();
            _dialogService.ShowDialog(GetFastInsertCaption(insertInfo.Item1),
                GetFastInsertDetail(insertInfo.Item1, insertInfo.Item2));
        }

        private string GetFastInsertDetail(Insertion insertion, Shift shift)
        {
            var sb = new StringBuilder();
            switch (insertion)
            {
                case Insertion.Departure:
                    sb.AppendLine($"čas odchodu: {shift.DepartureTime.ToString("H:mm")}");
                    sb.AppendLine($"miesto odchodu: {shift.DepartureLocation}");
                    break;
                case Insertion.WorkStart:
                    sb.AppendLine($"začiatok práce: {shift.TimeFrom.ToString("H:mm")}");
                    sb.AppendLine($"miesto práce: {shift.Location}");
                    sb.AppendLine($"krajina: {shift.Country}");
                    break;
                case Insertion.WorkEnd:
                    sb.AppendLine($"koniec práce: {shift.TimeTo.ToString("H:mm")}");
                    sb.AppendLine($"miesto práce: {shift.Location}");
                    sb.AppendLine($"krajina: {shift.Country}");
                    break;
                case Insertion.Arrival:
                    sb.AppendLine($"čas príchodu: {shift.ArrivalTime.ToString("H:mm")}");
                    sb.AppendLine($"miesto príchodu: {shift.ArrivalLocation}");
                    break;
                default: throw new NotImplementedException();
            }
            return sb.ToString();
        }

        private string GetFastInsertCaption(Insertion insertion)
        {
            switch (insertion)
            {
                case Insertion.Departure: return "Odchod do práce";
                case Insertion.WorkStart: return "Príchod do práce";
                case Insertion.WorkEnd: return "Odchod z práce";
                case Insertion.Arrival: return "Príchod domov";
                default: throw new NotImplementedException();
            }
        }

        #endregion
    }
}
