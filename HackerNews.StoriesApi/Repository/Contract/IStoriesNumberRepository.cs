using HackerNews.StoriesApi.Entities;

namespace HackerNews.StoriesApi.Repository.Contract
{
    public interface IStoriesNumberRepository
    {
        Task<string> GetDataFromJsonUrlAsync(string url);
    }
}
