namespace MyWebtoonWebProject.Data.Models
{
    public class WebtoonsSubscribers
    {
        public string SubscriberId { get; set; }

        public ApplicationUser Subscriber { get; set; }

        public string WebtoonId { get; set; }

        public Webtoon Webtoon { get; set; }
    }
}
