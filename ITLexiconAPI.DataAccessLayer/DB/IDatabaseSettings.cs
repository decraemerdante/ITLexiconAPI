using System;
using System.Collections.Generic;
using System.Text;

namespace ITLexiconAPI.DataAccessLayer.DB
{
     public interface IDatabaseSettings
    {
        string ArticleCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string CategoryCollectionName { get; set; }
        string LinkedArticlesCollectionName { get; set; }
    }

    public class DatabaseSettings : IDatabaseSettings
    {
        public string ArticleCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CategoryCollectionName { get ; set ; }
        public string LinkedArticlesCollectionName { get ; set ; }
    }
}
