using HackerNews.StoriesApi.Entities;

namespace HackerNews.StoriesApi.Repository.Contract
{
    public interface INewsStoriesRepository
    {
        IEnumerable<NewsStories> GetAllNewsStories(int numberOfTopNews);
    }
  
}
