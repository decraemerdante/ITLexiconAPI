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
    public class ArticleRepo : IArticleRepo
    {
        private readonly IMongoCollection<Article> articles;
        public ArticleRepo(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            articles = database.GetCollection<Article>(settings.ArticleCollectionName);
        }
        public async Task<Article> Add(Article article)
        {
           await articles.InsertOneAsync(article);
           return article;
        }

        public async Task Delete(Article articleToDelete)
        {
           await articles.DeleteOneAsync(article => article.Id == articleToDelete.Id);
        }

        public async Task<Article> Get(string maskId)
        {
            return await articles.Find<Article>(article => article.Id == maskId).FirstOrDefaultAsync();
        }

        public async Task<List<Article>> Get()
        {
           return await articles.Find(article => true).ToListAsync();
        }

        public async Task<List<Article>> GetArticlesByIds(List<string> articleIds)
        {
            return await articles.Find(article => articleIds.Contains(article.Id)).ToListAsync();
        }      

        public Task<Article> GetArticleWithLinkedArticles(string maskId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Article>> GetByCategory(string maskId)
        {
            return await articles.Find(article => article.CategoryId == maskId).ToListAsync();
        }

        public async Task Update(Article articleOld, Article articleNew)
        {
            await articles.ReplaceOneAsync(article => article.Id == articleOld.Id, articleNew);
        }
    }
}
