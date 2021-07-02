using AutoMapper;
using ITLexiconAPI.BusinessLayer.DTO.Profiles;
using ITLexiconAPI.BusinessLayer.Repositories;
using ITLexiconAPI.BusinessLayer.Repositories.Implementations;
using ITLexiconAPI.DataAccessLayer.DB;
using ITLexiconAPI.DataAccessLayer.Repositories;
using ITLexiconAPI.DataAccessLayer.Repositories.Implementations;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLexiconAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
             builder =>
             {
                 builder.WithOrigins("http://localhost:4200", "https://it-lexicon-app.azurewebsites.net").AllowAnyHeader().AllowAnyMethod();

             });

            });
            services.AddControllers();
            services.AddSwaggerDocument();
            services.AddDbContext<LexiconContext>(item => item.UseSqlServer(Configuration.GetConnectionString("ITLexiconDB")));
            services.AddTransient<ICategoryRepo, CategoryRepo>();
            services.AddTransient<IArticleRepo, ArticleRepo>();
            services.AddTransient<ILinkedRepo, LinkedRepo>();
            services.AddTransient<ICategoryBLRepo, CategoryBLRepo>();
            services.AddTransient<IArticleBLRepo, ArticleBLRepo>();
            services.AddTransient<ILinkedBLRepo, LinkedBLRepo>();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ArticleProfile());
                mc.AddProfile(new CategoryProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);                  

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();
            app.UseHttpsRedirection();

            app.UseRouting();
           
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

          
            app.UseOpenApi();
            app.UseSwaggerUi3();

        }
    }
}
