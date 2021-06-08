using AutoMapper;
using ITLexiconAPI.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLexiconAPI.DTO.Profiles
{
    public class CategoryProfile :  Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
        }      
    }
}
