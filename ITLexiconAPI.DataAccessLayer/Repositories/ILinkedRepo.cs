using ITLexiconAPI.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ITLexiconAPI.DataAccessLayer.Repositories
{
    public interface ILinkedRepo
    {
        Task Delete(Article article, Article linkedArticle);
        Task Add(Article article, Article LinkedArticle);
        Task<List<LinkedArticles>> GetLinkedArticles(string maskId);
    }
}
