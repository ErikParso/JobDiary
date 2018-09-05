namespace MyJobDiary.Model
{
    public class DietPaymentItem : ICachedTableItem
    {
        public string Id { get; set; }

        public string Country { get; set; }

        public double Hours { get; set; }

        public double Reward { get; set; }

        public string Currency { get; set; }
    }
}
