using ITLexiconAPI.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ITLexiconAPI.DataAccessLayer.Repositories
{
    public interface ICategoryRepo
    {
        Task Add(Category category);
        Task<Category> Get(string maskId);
        Task<List<Category>> Get();
        Task Delete(Category category);
        Task Update(Category category, string name);
    }
}
