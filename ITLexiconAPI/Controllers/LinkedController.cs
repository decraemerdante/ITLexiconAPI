using AutoMapper;
using ITLexiconAPI.BusinessLayer.DTO;
using ITLexiconAPI.BusinessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLexiconAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkedController : ControllerBase
    {

        private IArticleBLRepo articleRepo; 
      
        private ILinkedBLRepo linkedRepo;

        public LinkedController(IArticleBLRepo articleRepo, ILinkedBLRepo linkedRepo)
        {
            this.articleRepo = articleRepo;           
            this.linkedRepo = linkedRepo;
        }
        [Route("{maskId}")]
        public async Task<ActionResult<List<ArticleDto>>> GetLinkedArticles(Guid maskId)
        {
            try
            {
                List<ArticleDto> articles = await linkedRepo.GetLinkedItems(maskId);
                return Ok(articles);
            }
            catch (Exception e) { }

            return BadRequest("Something went wrong");
        }

        [HttpPost]
        [Route("Linked")]
        public async Task<ActionResult<string>> AddLinkedArticle([FromBody] LinkedArticlesDto linkedArticles)
        {
            try
            {
                await this.linkedRepo.HandleLinkedArticleAction(linkedArticles.ArticleId, linkedArticles.LinkedArticleId);
                return Ok("Linked article added");
            }
            catch (Exception e) { }

            return BadRequest("Something went wrong");
        }

        [Route("Overview/{maskId}")]
        public async Task<ActionResult<LinkedArticleOverviewDto>> GetLinkedArticlesOverview(Guid maskId)
        {
            try
            {
                List<ArticleDto> allArticles = await articleRepo.Get();
                LinkedArticleOverviewDto overviewDto = new LinkedArticleOverviewDto()
                {
                    MainArticle = allArticles.FirstOrDefault(m => m.MaskId == maskId),
                    LinkedArticles = await linkedRepo.GetLinkedItems(maskId),
                    AllArticles = allArticles
                };
                return Ok(overviewDto);
            }
            catch (Exception e) { }

            return BadRequest("Something went wrong");
        }

        [HttpDelete]
        [Route("Linked")]

        public async Task<ActionResult> DeleteLinkedArticle([FromBody] LinkedArticlesDto linkedArticles)
        {
            try
            {
                await linkedRepo.HandleLinkedArticleAction(linkedArticles.ArticleId, linkedArticles.LinkedArticleId, true);
                return Ok("Linked article has been deleted");
            }
            catch (Exception e) { }

            return BadRequest("Something went wrong");

        }
    }
}
