using MyJobDiary.Model;
using System.Collections.Generic;

namespace MyJobDiary.ViewModel
{
    public class AttendanceListViewModel
    {
        public IEnumerable<AttendanceItem> Days { get; set; }
    }
}
