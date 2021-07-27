using ITLexiconAPI.BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ITLexiconAPI.BusinessLayer.Repositories
{
    public interface IArticleBLRepo
    {
        Task<string> Add(ArticleDto article);
        Task<ArticleDto> Get(string maskId);
        Task<List<ArticleDto>> Get();
        Task Delete(string maskId);
        Task Update(ArticleDto articleNew);
        Task<List<ArticleDto>> GetByCategory(string maskId);
    }
}
