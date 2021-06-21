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
    public class CategoryRepo : ICategoryRepo
    {
        private LexiconContext context;

        public CategoryRepo(LexiconContext context)
        {
            this.context = context;
        }


        public async Task Add(Category category)
        {
            await this.context.Categories.AddAsync(category);
            await this.context.SaveChangesAsync();
        }

        public async Task Delete(Category category)
        {
            List<Article> articlesOfCategory = await this.context.Articles.Where(m => m.CategoryId == category.Id).ToListAsync();

            foreach (var article in articlesOfCategory)
            {
                article.CategoryId = null;
            }
            this.context.Categories.Remove(category);
            await this.context.SaveChangesAsync();
        }

        public async Task<Category> Get(Guid maskId)
        {
            return await this.context.Categories.Include(c => c.Articles).FirstOrDefaultAsync(c => c.MaskId == maskId);
        }

        public async Task<List<Category>> Get()
        {
            return await this.context.Categories.Include(c => c.Articles).ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
            return await this.context.Categories.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Update(Category category, string name)
        {
            category.Name = name;

            await this.context.SaveChangesAsync();
        }
    }
}
