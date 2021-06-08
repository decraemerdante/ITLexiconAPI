using AutoMapper;
using ITLexiconAPI.DataAccessLayer.Models;
using ITLexiconAPI.DataAccessLayer.Repositories;
using ITLexiconAPI.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLexiconAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepo categoryRepo;
        private IMapper mapper;

        public CategoryController(ICategoryRepo categoryRepo, IMapper mapper)
        {
            this.categoryRepo = categoryRepo;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] string name)
        {
            try
            {
                await categoryRepo.Add(new Category() { Name = name });
                return Ok("Category has been added");
            }
            catch (Exception e) { }

            return BadRequest("Something went wrong");
        }
        public async Task<ActionResult<List<CategoryDto>>> Get()
        {            
            try
            {
                List<Category> categories = await categoryRepo.Get();
                return Ok(mapper.Map<List<CategoryDto>>(categories));
            }
            catch(Exception e) { }

            return BadRequest("Something went wrong");
        }

        [Route("{maskId}")]
        public async Task<ActionResult<CategoryDto>> Get(Guid maskId)
        {
            try
            {
                Category category = await categoryRepo.Get(maskId);

                if(category != null)
                    return Ok(mapper.Map<CategoryDto>(category));

                return NotFound("Category does not exist");
            }
            catch (Exception e) { }

            return BadRequest("Something went wrong");            
        }
        
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] CategoryDto category)
        {
            try
            {
                Category categoryOld = await categoryRepo.Get(category.MaskId);

                if (categoryOld != null)
                {
                    await categoryRepo.Update(categoryOld, category.Name);
                    return Ok("Category has been changed");
                }

                return NotFound("Category does not exist");
            }
            catch (Exception e) { }

            return BadRequest("Something went wrong");
        }
        

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid maskId)
        {
            try
            {
                Category category = await categoryRepo.Get(maskId);

                if(category != null)
                {
                    await categoryRepo.Delete(category);
                    return Ok("Category has been deleted");
                }

                return NotFound("Category does not exist");
            }
            catch (Exception e) { }

            return BadRequest("Something went wrong");
        }
    }
}
