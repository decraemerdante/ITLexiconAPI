using ITLexiconAPI.BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ITLexiconAPI.BusinessLayer.Repositories
{
    public interface ICategoryBLRepo
    {
        Task Add(CategoryDto category);
        Task<CategoryDto> Get(string maskId);
        Task<List<CategoryDto>> Get();
        Task Delete(string maskId);
        Task Update(CategoryDto newCategory);        
    }
}
