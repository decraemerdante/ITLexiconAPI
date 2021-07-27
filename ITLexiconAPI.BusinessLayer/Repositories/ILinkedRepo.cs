using ITLexiconAPI.BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ITLexiconAPI.BusinessLayer.Repositories
{
    public interface ILinkedBLRepo
    {
        Task<List<ArticleDto>> GetLinkedItems(string maskId);
        Task HandleLinkedArticleAction(string articleId, string linkedArticleId, bool isDelete = false);
    }
}
