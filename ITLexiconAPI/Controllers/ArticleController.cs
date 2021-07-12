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
    public class ArticleController : ControllerBase
    {

        private IArticleBLRepo articleRepo;
        private ICategoryBLRepo categoryRepo;
        private IMapper mapper;

        public ArticleController(IArticleBLRepo articleRepo, IMapper mapper, ICategoryBLRepo categoryRepo)
        {
            this.articleRepo = articleRepo;
            this.mapper = mapper;
            this.categoryRepo = categoryRepo;
        }
        #region Get
        public async Task<ActionResult<List<ArticleDto>>> Get()
        {
            try
            {
                List<ArticleDto> articles = await articleRepo.Get();
                return Ok(mapper.Map<List<ArticleDto>>(articles));
            }
            catch (Exception e) { }

            return BadRequest("Something went wrong");
        }

        [Route("{maskId}")]
        public async Task<ActionResult<ArticleDto>> Get(Guid maskId)
        {
            try
            {
                ArticleDto articleDto = await articleRepo.Get(maskId);

                if (articleDto != null)
                {
                   
                    CategoryDto cat = new CategoryDto();

                    if (articleDto.CategoryId.HasValue)
                        cat = await categoryRepo.GetById(articleDto.CategoryId.Value);

                    if (cat != null)
                        articleDto.CategoryMaskId = cat.MaskId;

                    return Ok(articleDto);
                }


                return NotFound("Article does not exist");
            }
            catch (Exception e) { }

            return BadRequest("Something went wrong");
        }

        [Route("Category/{maskId}")]
        public async Task<ActionResult<List<ArticleDto>>> GetByCategory(Guid maskId)
        {
            try
            {
                List<ArticleDto> articles = await articleRepo.GetByCategory(maskId);
                return Ok(mapper.Map<List<ArticleDto>>(articles));
            }
            catch (Exception e) { }

            return BadRequest("Something went wrong");
        }

       
        #endregion

        #region Add
        [HttpPost]
        public async Task<ActionResult<string>> Add([FromBody] ArticleDto article)
        {
            try
            {               
                Guid maskId = await articleRepo.Add(article);
                return Ok(maskId.ToString());
            }
            catch (Exception e) { }

            return BadRequest("Something went wrong");
        }     

        #endregion

        #region Edit
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] ArticleDto article)
        {
            try
            {
               
                    await articleRepo.Update(article);
                    return Ok("Article has been changed");
                
            }
            catch (Exception e) 
            {
            
            }

            return BadRequest("Something went wrong");
        }
        #endregion

        #region Delete
        [HttpDelete]
        public async Task<ActionResult> Delete(Guid maskId)
        {
            try
            {
                    await articleRepo.Delete(maskId);
                    return Ok("Article has been deleted");              

            }
            catch (Exception e) 
            { 


            }

            return BadRequest("Something went wrong");
        }       

            #endregion
        }
}
