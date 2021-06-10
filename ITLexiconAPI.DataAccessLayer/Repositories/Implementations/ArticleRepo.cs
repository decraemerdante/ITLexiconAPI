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
    public class ArticleRepo : IArticleRepo
    {
        private LexiconContext context;

        public ArticleRepo(LexiconContext context)
        {
            this.context = context;
        }
        public async Task<Guid> Add(Article article)
        {
            await this.context.AddAsync(article);
            await this.context.SaveChangesAsync();

            return article.MaskId;
        }

        public async Task Delete(Article article)
        {
            this.context.Remove(article);
            await this.context.SaveChangesAsync();
        }

        public async Task<Article> Get(Guid maskId)
        {
            return await this.context.Articles.FirstOrDefaultAsync(a => a.MaskId == maskId);
        }

        public async Task<List<Article>> Get()
        {
            return await this.context.Articles.ToListAsync();
        }

        public async Task<List<Article>> GetByCategory(Guid maskId)
        {
            return await this.context.Articles.Include(a => a.Category).Where(a => a.CategoryId.HasValue && a.Category.MaskId == maskId).ToListAsync();
        }

        public async Task Update(Article article, Article articleNew)
        {
            article.Title = articleNew.Title;
            article.Content = articleNew.Content;
            article.CategoryId = articleNew.CategoryId;

            await this.context.SaveChangesAsync();
        }
    }
}
