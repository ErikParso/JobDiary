﻿using Newtonsoft.Json;
using System;

namespace MyJobDiary.Model
{
    public class Shift
    {
        public string Id { get; set; }

        public DateTime TimeFrom { get; set; }

        public DateTime TimeTo { get; set; }

        public string Location { get; set; }

        public bool IsNightShift { get; set; }

        public string Job { get; set; }

        public bool IsClosed { get; set; }

        [JsonIgnore]
        public TimeSpan TimeWorked { get => TimeTo - TimeFrom; }
    }
}
