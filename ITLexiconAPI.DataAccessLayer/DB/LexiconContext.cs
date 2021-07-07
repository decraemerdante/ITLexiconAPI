using ITLexiconAPI.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITLexiconAPI.DataAccessLayer.DB
{
   public class LexiconContext: DbContext
    {
        public LexiconContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<LinkedArticles> LinkedArticles { get; set; }

        public DbSet<Changelog> Changelogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Article>().Property(x => x.MaskId).HasDefaultValueSql("newsequentialid()");
            modelBuilder.Entity<Category>().Property(x => x.MaskId).HasDefaultValueSql("newsequentialid()");

        }
    }
}
