namespace SignalPlus.DTOs
{
    public class HomePageDTO
    {
        public int TotalSignals { get; set; }

        public int InProgressSignals { get; set; }

        public int CompletedSignals { get; set; }

        public List<FaqItem> faq { get; set; } = new();
    }
}
