﻿using AutoMapper;
using ITLexiconAPI.DataAccessLayer.Models;
using ITLexiconAPI.DataAccessLayer.Repositories;
using ITLexiconAPI.DTO;
using Microsoft.AspNetCore.Cors;
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
        private ICategoryRepo categoryRepo;
        private IMapper mapper;

        public ArticleController(IArticleRepo articleRepo, IMapper mapper, ICategoryRepo categoryRepo)
        {
            this.articleRepo = articleRepo;
            this.mapper = mapper;
            this.categoryRepo = categoryRepo;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Add([FromBody] ArticleDto article)
        {
            try
            {
                if (article.CategoryMaskId.HasValue && article.CategoryMaskId != Guid.Empty)
                {
                    Category category = await categoryRepo.Get(article.CategoryMaskId.Value);

                    if (category != null)
                    {
                        article.CategoryId = category.Id;
                    }
                }
                Guid maskId = await articleRepo.Add(mapper.Map<Article>(article));
                return Ok(maskId.ToString());
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
                {
                    ArticleDto articleDto = mapper.Map<ArticleDto>(article);
                    Category cat = new Category();

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
