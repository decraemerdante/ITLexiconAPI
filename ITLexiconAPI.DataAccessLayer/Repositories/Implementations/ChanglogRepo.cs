using ITLexiconAPI.DataAccessLayer.DB;
using ITLexiconAPI.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITLexiconAPI.DataAccessLayer.Repositories.Implementations
{
    public class ChanglogRepo : IChangelogRepo
    {
        private LexiconContext context;

        public ChanglogRepo(LexiconContext context){
            this.context = context;
        }

        public async Task Add(Article article, Changelog.LogItemEnum logItemEnum)
        {
            Changelog changelogToAdd = new Changelog()
            {
                Type = logItemEnum,
                Date = DateTime.Now,
                Article = article
            };

            await this.context.Changelogs.AddAsync(changelogToAdd);

            await this.context.SaveChangesAsync();
        }

        public async Task DeleteChangelogsOfArticle(Article articleToDelete)
        {
            List<Changelog> changelogsToRemove = await this.context.Changelogs.Include(m => m.Article).Where(m => m.Article.Id == articleToDelete.Id).ToListAsync();

            this.context.Changelogs.RemoveRange(changelogsToRemove);
            await this.context.SaveChangesAsync();
        }
    }
}
