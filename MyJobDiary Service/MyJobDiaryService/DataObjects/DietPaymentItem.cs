using Microsoft.Azure.Mobile.Server;

namespace MyJobDiaryService.DataObjects
{
    public class DietPaymentItem : EntityData
    {
        public string UserId { get; set; }

        public string Country { get; set; }

        public double Hours { get; set; }

        public double Reward { get; set; }

        public string Currency { get; set; }

    }
}