using ITLexiconAPI.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ITLexiconAPI.DataAccessLayer.Repositories
{
    public interface IArticleRepo
    {
        Task<Article> Add(Article article);
        Task<Article> Get(string maskId);
        Task<List<Article>> Get();
        Task Delete(Article article);
        Task Update(Article article, Article articleNew);
        Task<List<Article>> GetByCategory(string maskId);
        Task<Article> GetArticleWithLinkedArticles(string maskId);
        Task<List<Article>> GetArticlesByIds(List<string> articleIds);       
    }
}
