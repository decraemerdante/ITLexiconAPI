using ITLexiconAPI.BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ITLexiconAPI.BusinessLayer.Repositories
{
    public interface ILinkedBLRepo
    {
        Task<List<ArticleDto>> GetLinkedItems(Guid maskId);
        Task HandleLinkedArticleAction(Guid articleId, Guid linkedArticleId, bool isDelete = false);
    }
}
