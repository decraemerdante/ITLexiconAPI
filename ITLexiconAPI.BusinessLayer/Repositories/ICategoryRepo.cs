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
        Task<CategoryDto> Get(Guid maskId);
        Task<List<CategoryDto>> Get();
        Task Delete(Guid maskId);
        Task Update(CategoryDto newCategory);
        Task<CategoryDto> GetById(int id);
    }
}
