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
    public class CategoryRepo : ICategoryRepo
    {
        private readonly IMongoCollection<Category> categories;
        public CategoryRepo(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            categories = database.GetCollection<Category>(settings.CategoryCollectionName);
        }

        public async Task Add(Category category)
        {
            await categories.InsertOneAsync(category);
        }

        public async Task Delete(Category categoryToDelete)
        {
            await categories.DeleteOneAsync(category => category.Id == categoryToDelete.Id);
        }

        public async Task<Category> Get(string maskId)
        {
            return await categories.Find<Category>(category => category.Id == maskId).FirstOrDefaultAsync();
        }

        public async Task<List<Category>> Get()
        {
            return await categories.Find(category => true).ToListAsync();
        }

        public async Task Update(Category categoryOld, string name)
        {
            categoryOld.Name = name;
            await categories.ReplaceOneAsync(category => category.Id == categoryOld.Id, categoryOld);
        }
    }
}
