namespace HackerNews.StoriesApi.Entities
{
    public class NewsStories
    {
        public string? title { get; set; }
        public string? uri { get; set; }
        public string? postedBy { get; set; }
        public DateTime time { get; set; }
        public int score { get; set; }
        public int commentCount { get; set; }

    }
}