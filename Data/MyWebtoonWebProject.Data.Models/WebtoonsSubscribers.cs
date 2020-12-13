namespace MyWebtoonWebProject.Data.Models
{
    public class WebtoonsSubscribers
    {
        public string SubscriberId { get; set; }

        public virtual ApplicationUser Subscriber { get; set; }

        public string WebtoonId { get; set; }

        public virtual Webtoon Webtoon { get; set; }
    }
}
