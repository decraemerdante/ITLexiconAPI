using AutoMapper;
using ITLexiconAPI.BusinessLayer.DTO;
using ITLexiconAPI.DataAccessLayer.Models;
using ITLexiconAPI.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ITLexiconAPI.BusinessLayer.Repositories.Implementations
{
    public class CategoryBLRepo : ICategoryBLRepo
    {
        private ICategoryRepo categoryRepo;
        private IMapper mapper;

        public CategoryBLRepo(ICategoryRepo catergoryRepo, IMapper mapper)
        {
            this.categoryRepo = catergoryRepo;
            this.mapper = mapper;
        }

        public async Task Add(CategoryDto category)
        {
            await categoryRepo.Add(mapper.Map<Category>(category));
        }

        public async Task Delete(Guid maskId)
        {
            Category category = await categoryRepo.Get(maskId);
            await categoryRepo.Delete(category);
        }

        public async Task<CategoryDto> Get(Guid maskId)
        {
            return mapper.Map<CategoryDto>(await categoryRepo.Get(maskId));
        }

        public async Task<List<CategoryDto>> Get()
        {
            return mapper.Map<List<CategoryDto>>(await categoryRepo.Get());
        }

        public async Task<CategoryDto> GetById(int id)
        {
            return mapper.Map<CategoryDto>(await categoryRepo.GetById(id));
        }

        public async Task Update(CategoryDto newCategory)
        {
            Category category = await categoryRepo.Get(newCategory.MaskId);

            if(category != null)
            {
                await categoryRepo.Update(
               category,
               newCategory.Name);
            }
           
        }
    }
}
