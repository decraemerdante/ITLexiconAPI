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
    public class ArticleController : ControllerBase
    {
      
            private IArticleRepo articleRepo;
            private IMapper mapper;

            public ArticleController(IArticleRepo articleRepo, IMapper mapper)
            {
                this.articleRepo = articleRepo;
                this.mapper = mapper;
            }

            [HttpPost]
            public async Task<ActionResult> Add([FromBody] ArticleDto article)
            {
                try
                {
                    await articleRepo.Add(mapper.Map<Article>(article));
                    return Ok("Article has been added");
                }
                catch (Exception e) { }

                return BadRequest("Something went wrong");
            }
            public async Task<ActionResult<List<ArticleDto>>> Get()
            {
                try
                {
                    List<Article> articles = await articleRepo.Get();
                    return Ok(mapper.Map<List<ArticleDto>>(articles));
                }
                catch (Exception e) { }

                return BadRequest("Something went wrong");
            }

        [Route("Category/{maskId}")]
        public async Task<ActionResult<List<ArticleDto>>> GetByCategory(Guid maskId)
        {
            try
            {
                List<Article> articles = await articleRepo.GetByCategory(maskId);               
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
                    Article article = await articleRepo.Get(maskId);

                    if (article != null)
                        return Ok(mapper.Map<ArticleDto>(article));

                    return NotFound("Article does not exist");
                }
                catch (Exception e) { }

                return BadRequest("Something went wrong");
            }

            [HttpPut]
            public async Task<ActionResult> Update([FromBody] ArticleDto article)
            {
                try
                {
                    Article articleOld = await articleRepo.Get(article.MaskId);

                    if (articleOld != null)
                    {
                        await articleRepo.Update(articleOld, mapper.Map<Article>(article));
                        return Ok("Article has been changed");
                    }

                    return NotFound("Article does not exist");
                }
                catch (Exception e) { }

                return BadRequest("Something went wrong");
            }


            [HttpDelete]
            public async Task<ActionResult> Delete(Guid maskId)
            {
                try
                {
                    Article article = await articleRepo.Get(maskId);

                    if (article != null)
                    {
                        await articleRepo.Delete(article);
                        return Ok("Article has been deleted");
                    }

                    return NotFound("Article does not exist");
                }
                catch (Exception e) { }

                return BadRequest("Something went wrong");
            }
        }
}
