using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ITLexiconAPI.DataAccessLayer.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MaskId { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}
