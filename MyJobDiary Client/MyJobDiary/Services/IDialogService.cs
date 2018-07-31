using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobDiary.Services
{
    public interface IDialogService
    {
        void ShowDialog(string title, string message);

        bool ShowYesNoDialog(string title, string message);
    }
}
