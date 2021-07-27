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
    public class ArticleBLRepo : IArticleBLRepo
    {
        private IArticleRepo articleRepo;
        private IMapper mapper;
        private ICategoryRepo categoryRepo;
       

        public ArticleBLRepo(IArticleRepo articleRepo, IMapper mapper, ICategoryRepo categoryRepo)
        {
            this.articleRepo = articleRepo;
            this.mapper = mapper;
            this.categoryRepo = categoryRepo;            
        }
        public async Task<string> Add(ArticleDto article)
        {

            if (article.CategoryId != null && !string.IsNullOrEmpty(article.CategoryId))
            {
                Category category = await categoryRepo.Get(article.CategoryId);

                if (category != null)
                {
                    article.CategoryId = category.Id;
                }
            }

            Article createdArticle = await articleRepo.Add(mapper.Map<Article>(article));          

            return createdArticle.Id;
        }

        public async Task Delete(string maskId)
        {
            Article articleToDelete = await articleRepo.Get(maskId);
            if(articleToDelete != null)
            {               
                await articleRepo.Delete(articleToDelete);
            }            
        }

        public async Task<ArticleDto> Get(string maskId)
        {
            return mapper.Map<ArticleDto>(await articleRepo.Get(maskId));
        }

        public async Task<List<ArticleDto>> Get()
        {
           return mapper.Map<List<ArticleDto>>(await articleRepo.Get());
        }

        public async Task<List<ArticleDto>> GetByCategory(string maskId)
        {
            return mapper.Map<List<ArticleDto>>(await articleRepo.GetByCategory(maskId));
        }

        public async Task Update(ArticleDto articleNew)
        {
            Article articleOld = await articleRepo.Get(articleNew.Id);

            if(articleOld != null)
            {
                await articleRepo.Update(
                     articleOld,
                     mapper.Map<Article>(articleNew));               
            }         
        }
    }
}
