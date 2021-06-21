using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ITLexiconAPI.DataAccessLayer.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MaskId { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<LinkedArticles> LinkedArticles { get; set; }       
    }
}
