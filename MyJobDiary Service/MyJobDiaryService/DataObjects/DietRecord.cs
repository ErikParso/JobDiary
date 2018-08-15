using Microsoft.Azure.Mobile.Server;
using System;

namespace MyJobDiaryService.DataObjects
{
    public class DietRecord : EntityData
    {
        public string UserId { get; set; }

        public string StartLocation1 { get; set; }

        public string DestinationLocation1 { get; set; }

        public DateTime ArrivalTime1 { get; set; }

        public string StartLocation2 { get; set; }

        public string DestinationLocation2 { get; set; }

        public DateTime ArrivalTime2 { get; set; }
    }
}