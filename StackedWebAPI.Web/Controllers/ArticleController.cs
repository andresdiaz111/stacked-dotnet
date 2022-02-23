using Microsoft.AspNetCore.Mvc;
using StackedWebAPI.Models;
using StackedWebAPI.Services.Interfaces;
using StackedWebAPI.Web.Models;

namespace StackedWebAPI.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ArticleController : ControllerBase
{

    private readonly ILogger<ArticleController> _logger;
    private readonly IArticleService _articleService;

    public ArticleController(ILogger<ArticleController> logger, IArticleService articleService)
    {
        _logger = logger;
        _articleService = articleService;
    }


    /// <summary>
    /// Create an article given a body 
    /// </summary>
    /// <param name="article"></param>
    /// <returns></returns>
    [HttpPost("/article")]
    public async Task<ActionResult> CreateArticle([FromBody] ArticleDto article)
    {
        _logger.LogDebug($"Creating a new Article: {article.Title}");
        var newArticle = await _articleService.Create(article);

        if (newArticle.Error != null)
        {
            _logger.LogError($"Service layer error creating article: {newArticle.Error}");
            return StatusCode(500, "Error creating Articles.");
        }

        _logger.LogDebug($"Created Article: {article.Title} ({newArticle.Data})");
        return StatusCode(201, new { id = newArticle.Data });
    }

    /// <summary>
    /// Return a article given a id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("/article/{id}")]
    public async Task <ActionResult> GetArticle(string id)
    {
        try
        {
            var guid = Guid.Parse(id);
            var article = await _articleService.GetById(guid);

            if (article.Data == null && article.Error == null)
            {
                _logger.LogWarning($"Requested Article not found: {id}");
                return NotFound("Article not found");
            }
            if (article.Error != null)
            {
                _logger.LogError($"Error retrieving paginated articles: {id}, {article.Error}");
                return StatusCode(500, "Error retrieving Article.");
            }
            _logger.LogDebug($"Retrivied Article : {id}");
            return Ok(article.Data);
        }
        catch (FormatException e)
        {
            _logger.LogWarning($"There was GUID  format error for article {id}");
            return BadRequest(id);
        }
    }

    /// <summary>
    /// Return a paginated listo articles given a query
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("/article")]
    public async Task<ActionResult> GetPaginatedArticles([FromQuery] ManyArticlesRequest query)
    {
        var page = query.Page == 0 ? 1 : query.Page;
        var perPage = query.PerPage == 0 ? 3 : query.PerPage;
        var articles = await _articleService.GetAll(page, perPage);

        if (articles.Error != null)
        {
            _logger.LogError($"Error retrieving paginated articles: {articles.Error}");
            return StatusCode(500, "Error retrieving Articles.");
        }
        _logger.LogDebug($"Retrieved Articles Total: {articles.Data.TotalCount}");
        return Ok(articles.Data);
    }
}
