using HackerNews.StoriesApi.Entities;
using HackerNews.StoriesApi.Repository.Contract;
using Newtonsoft.Json;

namespace HackerNews.StoriesApi.Repository.Implementation
{
   
    public class StoriesNumbersRepository : IStoriesNumberRepository
    {
        private readonly HttpClient _httpClient;

        public StoriesNumbersRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetDataFromJsonUrlAsync(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonContent = await response.Content.ReadAsStringAsync();
                   // var data = 
                   
                    return jsonContent;
                }
                else
                {
                    // Handle non-success status codes
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request errors
                throw ex;
            }
        }
    }

}
