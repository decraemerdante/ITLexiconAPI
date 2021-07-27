using ITLexiconAPI.DataAccessLayer.DB;
using ITLexiconAPI.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITLexiconAPI.DataAccessLayer.Repositories.Implementations
{
    public class LinkedRepo : ILinkedRepo
    {
        private readonly IMongoCollection<LinkedArticles> linkedArticles;
        public LinkedRepo(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            linkedArticles = database.GetCollection<LinkedArticles>(settings.LinkedArticlesCollectionName);
        }

        public async Task Delete(Article articleToDelete, Article linkedArticleToDelete)
        {
            await linkedArticles.DeleteOneAsync(article => article.ArticleId == articleToDelete.Id && article.LinkedArticleId == linkedArticleToDelete.Id);
        }

        public async Task Add(Article article, Article LinkedArticle)
        {

          await linkedArticles.InsertOneAsync(new LinkedArticles()
            {
                ArticleId = article.Id,
                LinkedArticleId = LinkedArticle.Id
            });            
        }

        public async Task<List<LinkedArticles>> GetLinkedArticles(string maskId)
        {
            return await linkedArticles.Find(article => article.ArticleId == maskId).ToListAsync();
        }
    }
}
