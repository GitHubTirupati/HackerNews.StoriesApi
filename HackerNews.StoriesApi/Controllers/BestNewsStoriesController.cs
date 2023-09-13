using HackerNews.StoriesApi.Entities;
using HackerNews.StoriesApi.Repository.Contract;
using HackerNews.StoriesApi.Repository.Implementation;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace HackerNews.StoriesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BestNewsStoriesController : ControllerBase
    {
        

        private readonly ILogger<BestNewsStoriesController> _logger;
        private readonly INewsStoriesRepository _newsStoriesRepository;

        public BestNewsStoriesController(ILogger<BestNewsStoriesController> logger, INewsStoriesRepository newsStories)
        {
            _logger = logger;
            _newsStoriesRepository = newsStories;
        }

        [HttpGet("{n}")]
        public async Task<ActionResult<IEnumerable<NewsStories>>> getBestStories(int n)
        {
            try
            {
               
                return _newsStoriesRepository.GetAllNewsStories(n).OrderByDescending(x => x.score).Take(n).ToArray();
                
            }
            catch (Exception ex)
            {
                //log exception 
                return BadRequest();
            }

        }
        

    }
}