﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLexiconAPI.DTO
{
    public class ArticleDto
    {
        public string Title { get; set; }
        public string Content { get; set; }        
        public Guid MaskId { get; set; }
        public int? CategoryId { get; set; }
    }
}