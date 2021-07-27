using AutoMapper;
using ITLexiconAPI.BusinessLayer.DTO;
using ITLexiconAPI.DataAccessLayer.Models;
using ITLexiconAPI.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITLexiconAPI.BusinessLayer.Repositories.Implementations
{
    public class LinkedBLRepo : ILinkedBLRepo
    {
        private IArticleRepo articleRepo;
        private IMapper mapper;
        private ILinkedRepo linkedRepo;

        public LinkedBLRepo(IArticleRepo articleRepo, IMapper mapper, ILinkedRepo linkedRepo) {
            this.articleRepo = articleRepo;
            this.mapper = mapper;
            this.linkedRepo = linkedRepo;
        }

        public async Task<List<ArticleDto>> GetLinkedItems(string maskId)
        {
            List<Article> linkedArticles = new List<Article>();

            List<LinkedArticles> linkedArticlesObjects = await linkedRepo.GetLinkedArticles(maskId);

            linkedArticles = await articleRepo.GetArticlesByIds(linkedArticlesObjects.Select(m => m.LinkedArticleId).ToList());

            return mapper.Map<List<ArticleDto>>(linkedArticles);
        }
       
        public async Task HandleLinkedArticleAction(string articleId, string linkedArticleId, bool isDelete = false)
        {
            List<Article> articles = await GetLinkedArticleArticles(articleId, linkedArticleId);

            if (articles != null && articles.Any() && articles.Count == 2)
            {
                Article firstArticle = articles.FirstOrDefault();
                Article lastArticle = articles.LastOrDefault();

                if (isDelete)
                {
                    await linkedRepo.Delete(firstArticle, lastArticle);
                    await linkedRepo.Delete(lastArticle, firstArticle);
                }
                else
                {
                    await linkedRepo.Add(firstArticle, lastArticle);
                    await linkedRepo.Add(lastArticle, firstArticle);
                }
            }
        }

        private async Task<List<Article>> GetLinkedArticleArticles(string articleId, string linkedArticleId)
        {
            List<string> neededArticles = new List<string>()
            {
                articleId,
                linkedArticleId
            };

            return await articleRepo.GetArticlesByIds(neededArticles);
        }
    }
}
