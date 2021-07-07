using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLexiconAPI.BusinessLayer.DTO
{
    public class LinkedArticlesDto
    {
        public Guid ArticleId { get; set; }
        public Guid LinkedArticleId { get; set; }
    }
}
