using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLexiconAPI.DTO
{
    public class LinkedArticleOverviewDto
    {
      public ArticleDto MainArticle { get; set; }
      public List<ArticleDto> LinkedArticles { get; set; }
      public List<ArticleDto> AllArticles { get; set; }
    }
}