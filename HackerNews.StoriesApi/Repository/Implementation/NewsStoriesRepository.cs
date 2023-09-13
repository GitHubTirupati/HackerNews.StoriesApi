using HackerNews.StoriesApi.Controllers;
using HackerNews.StoriesApi.Entities;
using HackerNews.StoriesApi.Repository.Contract;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace HackerNews.StoriesApi.Repository.Implementation
{
    public class NewsStoriesRepository : INewsStoriesRepository
    {
        private readonly IStoriesNumberRepository _storiesNumberRepository;
        private readonly IConfiguration  _configuration;
        public NewsStoriesRepository(IStoriesNumberRepository storiesNumberRepository, IConfiguration configuration)
        {
            _storiesNumberRepository = storiesNumberRepository;
            _configuration = configuration;
        }

        public IEnumerable<NewsStories> GetAllNewsStories(int numberOfTopNews)
        {
            List<NewsStories> bestStories = new List<NewsStories>();
            List<Task<string>> apiTasks = new List<Task<string>>();

            string jsonContent = _storiesNumberRepository.GetDataFromJsonUrlAsync(_configuration.GetValue<string>("AppSettings:StoriesApi")).Result;
            int[] storyList = JsonConvert.DeserializeObject<int[]>(jsonContent);
            string storiesDetailsApi =  _configuration.GetValue<string>("AppSettings:StoriesDetailsApi");
            Parallel.ForEach(storyList, item =>
            {
                apiTasks.Add(_storiesNumberRepository.GetDataFromJsonUrlAsync($"{storiesDetailsApi}{item}.json"));
                
            });
            

            Task.WhenAll(apiTasks);
            Parallel.ForEach(apiTasks, task =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                {
                    string apiResponse = task.Result;
                    // Process the API response as needed
                    NewsStory newsStory = JsonConvert.DeserializeObject<NewsStory>(apiResponse);
                    bestStories.Add(new NewsStories() { title = newsStory.title, uri = newsStory.url, time = DateTimeOffset.FromUnixTimeSeconds(newsStory.time).UtcDateTime, postedBy = newsStory.by, score = newsStory.score, commentCount = 0 });
                }
                else if (task.IsFaulted)
                {
                    // Handle API call error

                }
            });

            return bestStories;
        }

    }
}
