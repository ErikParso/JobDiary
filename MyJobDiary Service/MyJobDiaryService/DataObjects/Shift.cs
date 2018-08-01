using Microsoft.Azure.Mobile.Server;
using System;

namespace MyJobDiaryService.DataObjects
{
    public class Shift : EntityData
    {
        public string UserId { get; set; }

        public DateTime TimeFrom { get; set; }

        public DateTime TimeTo { get; set; }

        public string Location { get; set; }

        public bool IsNightShift { get; set; }

        public string Job { get; set; }

        public bool IsClosed { get; set; }
    }
}