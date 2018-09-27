using MyJobDiary.Managers;
using MyJobDiary.Model;
using MyJobDiary.Services;
using System.Collections.Generic;

namespace MyJobDiary.ViewModel
{
    public class AttendanceListViewModel : ObservableObject
    {
        private readonly CachedTableManager<Shift> _shiftManager;

        public AttendanceListViewModel(CachedTableManager<Shift> shiftManager)
        {
            _shiftManager = shiftManager;
            MonthNavigationViewModel = new MonthNavigationViewModel();
            AddHandlers();
        }

        private void AddHandlers()
        {
            MonthNavigationViewModel.MonthChanged += Reload;
        }

        public MonthNavigationViewModel MonthNavigationViewModel { get; private set; }

        public IEnumerable<AttendanceItem> Days { get; private set; }

        public async void Reload()
        {
            Days = AttendanceBuilder.BuildAttendance(
                MonthNavigationViewModel.YearPicker.Value,
                MonthNavigationViewModel.MonthPicker.Value,
                await _shiftManager.GetAsync());
            RaisePropertyChanged(nameof(Days));
        }
    }
}
