using MyJobDiary.Model;
using MyJobDiary.Services;
using System;
using System.Collections.Generic;

namespace MyJobDiary.ViewModel
{
    public class AttendanceListViewModel : ObservableObject
    {
        private readonly IEnumerable<Shift> _shifts;

        public AttendanceListViewModel(IEnumerable<Shift> shifts)
        {
            _shifts = shifts;
            MonthNavigationViewModel = new MonthNavigationViewModel();
            MonthNavigationViewModel.MonthChanged += Reload;
            Reload();
        }

        public MonthNavigationViewModel MonthNavigationViewModel { get; private set; }

        public IEnumerable<AttendanceItem> Days { get; private set; }

        private void Reload()
        {
            Days = AttendanceBuilder.BuildAttendance(
                MonthNavigationViewModel.YearPicker.Value,
                MonthNavigationViewModel.MonthPicker.Value,
                _shifts);
            RaisePropertyChanged(nameof(Days));
        }

    }
}
