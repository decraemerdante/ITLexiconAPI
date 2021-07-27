using ITLexiconAPI.DataAccessLayer.DB;
using ITLexiconAPI.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITLexiconAPI.DataAccessLayer.Repositories.Implementations
{
    public class LinkedRepo : ILinkedRepo
    {
        public Task Add(Article article, Article LinkedArticle)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Article article, Article linkedArticle)
        {
            throw new NotImplementedException();
        }
    }
}
