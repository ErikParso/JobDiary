using System;
using Xamarin.Forms;

namespace MyJobDiary.ViewModel
{
    public class MonthNavigationViewModel
    {
        public SidePickerViewModel YearPicker { get; private set; }

        public SidePickerViewModel MonthPicker { get; private set; }

        public Action MonthChanged;

        public MonthNavigationViewModel()
        {
            YearPicker = new SidePickerViewModel()
            {
                Value = DateTime.Now.Year,
                LeftCommand = new Command(YearLeft),
                RightCommand = new Command(YearRight)
            };
            MonthPicker = new SidePickerViewModel()
            {
                Value = DateTime.Now.Month,
                LeftCommand = new Command(MonthLeft),
                RightCommand = new Command(MonthRight)
            };
        }

        private void YearLeft()
        {
            YearPicker.Value--;
            MonthChanged?.Invoke();
        }

        private void MonthLeft()
        {
            if (MonthPicker.Value == 1)
            {
                MonthPicker.Value = 12;
                YearPicker.Value--;
            }
            else
            {
                MonthPicker.Value--;
            }
            MonthChanged?.Invoke();
        }

        private void YearRight()
        {
            YearPicker.Value++;
            MonthChanged?.Invoke();
        }

        private void MonthRight()
        {
            if (MonthPicker.Value == 12)
            {
                MonthPicker.Value = 1;
                YearPicker.Value++;
            }
            else
            {
                MonthPicker.Value++;
            }
            MonthChanged?.Invoke();
        }
    }
}
