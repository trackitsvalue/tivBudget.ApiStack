using freebyTech.Common.Web.Logging.Interfaces;
using freebyTech.Common.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;
using tivBudget.Dal.Repositories.Interfaces;
using System;

namespace tivBudget.Api.Controllers
{
  /// <summary>
  /// Public Content API Controller.
  /// </summary>
  [Route("public")]
  [ApiController]
  public class PublicContentController : ControllerBase
  {
    private IQuoteRepository QuoteRepo { get; }
    private INewsRepository NewsRepo { get; }
    private IVideoRepository VideoRepo { get; }
    private IApiRequestLogger RequestLogger { get; }

    /// <summary>
    /// Standard constructor.
    /// </summary>
    /// <param name="quoteRepository">Repo for quote information.</param>
    /// <param name="videoRepository">Repo for video information.</param>
    /// <param name="newsRepository">Repo for news information.</param>
    /// <param name="requestLogger">Logger used to log information about request.</param>
    public PublicContentController(IQuoteRepository quoteRepository, IVideoRepository videoRepository, INewsRepository newsRepository, IApiRequestLogger requestLogger)
    {
      RequestLogger = requestLogger;
      QuoteRepo = quoteRepository;
      NewsRepo = newsRepository;
      VideoRepo = videoRepository;
    }

    /// <summary>
    /// Returns all unowned and owned budget categories for a particular ownerId.
    /// </summary>
    /// <returns>A list of all unowned and owned budget categories for a particular ownerId.</returns>
    [HttpGet("quotes/{count}")]
    public IActionResult GetQuotes(int count)
    {
      var quotes = QuoteRepo.FindAllQuotes();

      if (count > 0)
      {
        quotes = quotes.TakeRandom(count);
      }

      return Ok(quotes);
    }

    /// <summary>
    /// Returns all the published news items.
    /// </summary>
    /// <returns>A list of published news items.</returns>
    [HttpGet("news")]
    public IActionResult GetNews()
    {
      var news = NewsRepo.FindAllNews();

      return Ok(news);
    }

    /// <summary>
    /// Returns all the published videos.
    /// </summary>
    /// <returns>A list of published videos.</returns>
    [HttpGet("videos")]
    public IActionResult GetAllVideos()
    {
      var videos = VideoRepo.FindAllVideos();

      return Ok(videos);
    }

    /// <summary>
    /// Returns all the published videos of a category.
    /// </summary>
    /// <returns>A list of published videos of a category.</returns>
    [HttpGet("videos/{categoryId}")]
    public IActionResult GetCategory(Guid categoryId)
    {
      var category = VideoRepo.FindCategoryVideos(categoryId);

      if (category == null)
      {
        return NotFound($"Category with ID '{categoryId}' not found.");
      }

      return Ok(category);
    }

    /// <summary>
    /// Returns the published video of a category if both are found.
    /// </summary>
    /// <returns>The published video of a category if both are found.</returns>
    [HttpGet("videos/{categoryId}/{videoId}")]
    public IActionResult GetCategoryAndVideo(Guid categoryId, Guid videoId)
    {
      var category = VideoRepo.FindVideo(categoryId, videoId);

      if (category == null || category.Videos.Count == 0)
      {
        return NotFound($"Category with ID '{categoryId}' or Video with ID '{videoId}' not found.");
      }

      return Ok(category);
    }

  }
}