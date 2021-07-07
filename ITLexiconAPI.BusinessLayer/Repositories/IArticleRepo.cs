using ITLexiconAPI.BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ITLexiconAPI.BusinessLayer.Repositories
{
    public interface IArticleBLRepo
    {
        Task<Guid> Add(ArticleDto article);
        Task<ArticleDto> Get(Guid maskId);
        Task<List<ArticleDto>> Get();
        Task Delete(Guid maskId);
        Task Update(ArticleDto articleNew);
        Task<List<ArticleDto>> GetByCategory(Guid maskId);
    }
}
