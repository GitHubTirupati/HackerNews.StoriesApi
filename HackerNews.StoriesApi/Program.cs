using HackerNews.StoriesApi.Entities;
using HackerNews.StoriesApi.Repository.Contract;
using HackerNews.StoriesApi.Repository.Implementation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<INewsStoriesRepository, NewsStoriesRepository>();
builder.Services.AddHttpClient<IStoriesNumberRepository, StoriesNumbersRepository>(client =>
{
    client.BaseAddress = new Uri("https://hacker-news.firebaseio.com");
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
