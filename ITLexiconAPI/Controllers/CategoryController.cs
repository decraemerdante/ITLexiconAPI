using AutoMapper;
using ITLexiconAPI.BusinessLayer.DTO;
using ITLexiconAPI.BusinessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITLexiconAPI.Controllers
{
    [Route("api/[controller]")]
  
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryBLRepo categoryRepo;
        private IMapper mapper;

        public CategoryController(ICategoryBLRepo categoryRepo, IMapper mapper)
        {
            this.categoryRepo = categoryRepo;
            this.mapper = mapper;
        }

        public async Task<ActionResult<List<CategoryDto>>> Get()
        {
            try
            {
                List<CategoryDto> categories = await categoryRepo.Get();
                return Ok(categories);
            }
            catch (Exception e) { }

            return BadRequest("Something went wrong");
        }

        [Route("{maskId}")]
        public async Task<ActionResult<CategoryDto>> Get(string maskId)
        {
            try
            {
                CategoryDto category = await categoryRepo.Get(maskId);

                if (category != null)
                    return Ok(category);

                return NotFound("Category does not exist");
            }
            catch (Exception e) { }

            return BadRequest("Something went wrong");
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] string name)
        {
            try
            {
                await categoryRepo.Add(new CategoryDto() { Name = name });
                return Ok("Category has been added");
            }
            catch (Exception e) { }

            return BadRequest("Something went wrong");
        }       
        
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] CategoryDto category)
        {
            try
            {
               await categoryRepo.Update(category);
               return Ok("Category has been changed");               
            }
            catch (Exception e) { }

            return BadRequest("Something went wrong");
        }
        

        [HttpDelete]
        public async Task<ActionResult> Delete(string maskId)
        {
            try
            {
               await categoryRepo.Delete(maskId);
               return Ok("Category has been deleted");                
            }
            catch (Exception e) { }

            return BadRequest("Something went wrong");
        }
    }
}
