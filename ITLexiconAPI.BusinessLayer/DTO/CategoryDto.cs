using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLexiconAPI.BusinessLayer.DTO
{
    public class CategoryDto
    {
        public string Name { get; set; }
      
        public Guid MaskId { get; set; }

        public List<ArticleDto> Articles { get; set; }
    }
}
