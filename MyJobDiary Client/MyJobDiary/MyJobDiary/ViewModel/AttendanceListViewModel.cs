using MyJobDiary.Model;
using System.Collections.Generic;

namespace MyJobDiary.ViewModel
{
    public class AttendanceListViewModel
    {
        public IEnumerable<AttendanceDayModel> Days { get; set; }
    }
}
