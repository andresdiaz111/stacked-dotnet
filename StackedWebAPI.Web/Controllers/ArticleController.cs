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

    [HttpGet("/article/{id}")]
    public async Task <ActionResult> GetArticle(string id)
    {
        try
        {
            var guid = Guid.Parse(id);
            var article = await _articleService.GetById(guid);

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
