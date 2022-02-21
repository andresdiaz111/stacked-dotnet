using StackedWebAPI.Models;
using StackedWebAPI.Services.Interfaces;
using StackedWebAPI.Services.Models;

namespace StackedWebAPI.Services
{
    public class ArticleService : IArticleService
    {
        public Task<ServiceResult<Guid>> Create(ArticleDto article)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<Guid>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedServiceResult<ArticleDto>> GetAll(int page, int perPage)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<ArticleDto>> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<ArticleDto>> Update(Guid id, ArticleDto article)
        {
            throw new NotImplementedException();
        }
    }
}

