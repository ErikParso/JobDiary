using Newtonsoft.Json;
using System;

namespace MyJobDiary.Model
{
    public class DietPaymentItem : ICachedTableItem
    {
        public string Id { get; set; }

        public string Location { get; set; }

        public TimeSpan Time { get; set; }

        public double Payment { get; set; }

        [JsonIgnore]
        public string TimeString { get => Time.ToString(@"hh\:mm"); }
    }
}
