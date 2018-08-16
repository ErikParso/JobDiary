using System.Windows.Input;

namespace MyJobDiary.ViewModel
{
    public class SidePickerViewModel: ObservableObject
    {
        public ICommand LeftCommand { get; set; }

        public ICommand RightCommand { get; set; }

        private int _value;
        public int Value
        {
            get => _value;
            set => SetField(ref _value, value);
        }
    }
}
