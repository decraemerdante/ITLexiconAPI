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
    public class LinkedRepo : ILinkedRepo
    {

        private LexiconContext context;

        public LinkedRepo(LexiconContext context)
        {
            this.context = context;
        }     

        public async Task Delete(Article article, Article linkedArticle)
        {
            LinkedArticles linkedArticles = await this.context.LinkedArticles.FirstOrDefaultAsync(m => m.ArticleId == article.Id && m.LinkedArticleId == linkedArticle.Id);

            if (linkedArticles != null)
            {
                this.context.LinkedArticles.Remove(linkedArticles);
                await this.context.SaveChangesAsync();
            }
        }

        public async Task Add(Article article, Article LinkedArticle)
        {
            article.LinkedArticles.Add(new LinkedArticles()
            {
                LinkedArticleId = LinkedArticle.Id
            });
            await this.context.SaveChangesAsync();
        }      
       
    }
}
