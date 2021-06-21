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

        public async Task HandleLinkedArticleAction(Guid articleId, Guid linkedArticleId, bool isDelete = false)
        {
            List<Article> articles = await GetLinkedArticleArticles(articleId, linkedArticleId);

            if(articles != null && articles.Any() && articles.Count == 2)
            {
                Article firstArticle = articles.FirstOrDefault();
                Article lastArticle = articles.LastOrDefault();

                if (isDelete)
                {
                    await DeleteLinkedArticle(firstArticle, lastArticle);
                    await DeleteLinkedArticle(lastArticle, firstArticle);
                }
                else
                {
                    await AddLinkedArticleToArticle(firstArticle, lastArticle);
                    await AddLinkedArticleToArticle(lastArticle, firstArticle);
                }            
            }
        }

        private async Task DeleteLinkedArticle(Article article, Article linkedArticle)
        {
            LinkedArticles linkedArticles = await this.context.LinkedArticles.FirstOrDefaultAsync(m => m.ArticleId == article.Id && m.LinkedArticleId == linkedArticle.Id);

            if(linkedArticles != null)
            {
                this.context.LinkedArticles.Remove(linkedArticles);
                await this.context.SaveChangesAsync();
            }
        }

        private async Task AddLinkedArticleToArticle(Article article, Article LinkedArticle)
        {
            article.LinkedArticles.Add(new LinkedArticles()
            {
                LinkedArticleId = LinkedArticle.Id
            });
            await this.context.SaveChangesAsync();
        }

        private async Task<List<Article>> GetArticlesByIds(List<Guid> articleIds)
        {
            return await this.context.Articles.Include(m => m.LinkedArticles).Where(m => articleIds.Contains(m.MaskId)).ToListAsync();
        }

        private async Task<List<Article>> GetLinkedArticleArticles(Guid articleId, Guid linkedArticleId)
        {
            List<Guid> neededArticles = new List<Guid>()
            {
                articleId,
                linkedArticleId
            };

            return await GetArticlesByIds(neededArticles);
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

        public async Task<List<Article>> GetLinkedItems(Guid maskId)
        {
            List<Article> linkedArticles = new List<Article>();

            Article article = await this.context.Articles.Include(a => a.LinkedArticles).FirstOrDefaultAsync(a => a.MaskId == maskId);

            if(article != null && article.LinkedArticles != null && article.LinkedArticles.Any())
            {
                List<int> linkedArticleIds = article.LinkedArticles.Select(m => m.LinkedArticleId).ToList();

                linkedArticles = await GetArticlesByIds(linkedArticleIds);
            }              
          

            return linkedArticles;
        }

        private async Task<List<Article>> GetArticlesByIds(List<int> articleIds)
        {
            return await this.context.Articles.Include(m => m.LinkedArticles).Where(m => articleIds.Contains(m.Id)).ToListAsync();
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
