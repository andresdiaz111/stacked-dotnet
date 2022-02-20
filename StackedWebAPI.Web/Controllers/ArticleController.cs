using Microsoft.AspNetCore.Mvc;

namespace StackedWebAPI.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ArticleController : ControllerBase
{

    private readonly ILogger<ArticleController> _logger;

    public ArticleController(ILogger<ArticleController> logger)
    {
        _logger = logger;
    }

    [HttpGet("/article")]
    public ActionResult GetPaginatedArticles()
    {
        return Ok("Hello from get");
    }
}
