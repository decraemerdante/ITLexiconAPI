using ITLexiconAPI.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static ITLexiconAPI.DataAccessLayer.Models.Changelog;

namespace ITLexiconAPI.DataAccessLayer.Repositories
{
   public interface IChangelogRepo
    {
        Task Add(Article article, LogItemEnum logItemEnum);
        Task DeleteChangelogsOfArticle(Article articleToDelete);
    }
}
