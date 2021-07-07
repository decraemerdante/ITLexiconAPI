using AutoMapper;
using ITLexiconAPI.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLexiconAPI.BusinessLayer.DTO.Profiles
{
    public class ArticleProfile:  Profile
    {
        public ArticleProfile()
        {
            CreateMap<ArticleDto, Article>();
            CreateMap<Article, ArticleDto>();
            CreateMap<Changelog, ChangeLogDto>();
            CreateMap<ChangeLogDto, Changelog>();
        }
    }
}
