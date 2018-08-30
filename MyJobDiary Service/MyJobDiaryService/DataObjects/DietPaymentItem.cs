using Microsoft.Azure.Mobile.Server;
using System;

namespace MyJobDiaryService.DataObjects
{
    public class DietPaymentItem : EntityData
    {
        public string UserId { get; set; }

        public string Location { get; set; }

        public TimeSpan Time { get; set; }

        public double Payment { get; set; }

    }
}