using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ITLexiconAPI.DataAccessLayer.Models
{
    public class LinkedArticles
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public int LinkedArticleId { get; set; }
        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }       

    }
}
