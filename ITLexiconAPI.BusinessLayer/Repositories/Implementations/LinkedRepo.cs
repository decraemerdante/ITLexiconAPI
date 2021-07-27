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

        public Task<List<ArticleDto>> GetLinkedItems(Guid maskId)
        {
            throw new NotImplementedException();
        }

        public Task HandleLinkedArticleAction(Guid articleId, Guid linkedArticleId, bool isDelete = false)
        {
            throw new NotImplementedException();
        }
        //public async Task<List<ArticleDto>> GetLinkedItems(Guid maskId)
        //{
        //    List<Article> linkedArticles = new List<Article>();

        //    Article article = await articleRepo.GetArticleWithLinkedArticles(maskId);

        //    if (article != null && article.LinkedArticles != null && article.LinkedArticles.Any())
        //    {
        //        List<int> linkedArticleIds = article.LinkedArticles.Select(m => m.LinkedArticleId).ToList();

        //        linkedArticles = await articleRepo.GetArticlesByIds(linkedArticleIds);
        //    }

        //    return mapper.Map<List<ArticleDto>>(linkedArticles);
        //}        

        //public async Task HandleLinkedArticleAction(Guid articleId, Guid linkedArticleId, bool isDelete = false)
        //{
        //    List<Article> articles = await GetLinkedArticleArticles(articleId, linkedArticleId);

        //    if (articles != null && articles.Any() && articles.Count == 2)
        //    {
        //        Article firstArticle = articles.FirstOrDefault();
        //        Article lastArticle = articles.LastOrDefault();

        //        if (isDelete)
        //        {
        //            await linkedRepo.Delete(firstArticle, lastArticle);
        //            await linkedRepo.Delete(lastArticle, firstArticle);
        //        }
        //        else
        //        {
        //            await linkedRepo.Add(firstArticle, lastArticle);
        //            await linkedRepo.Add(lastArticle, firstArticle);
        //        }
        //    }
        //}

        //private async Task<List<Article>> GetLinkedArticleArticles(Guid articleId, Guid linkedArticleId)
        //{
        //    List<Guid> neededArticles = new List<Guid>()
        //    {
        //        articleId,
        //        linkedArticleId
        //    };

        //    return await articleRepo.GetArticlesByIds(neededArticles);
        //}
    }
}
