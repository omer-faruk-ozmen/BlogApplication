using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Application.Repositories.Category;
using BlogApplication.Application.Repositories.Post;
using BlogApplication.Application.Repositories.PostComment;
using BlogApplication.Application.Repositories.PostMeta;
using BlogApplication.Application.Repositories.Tag;
using BlogApplication.Domain.Entities.Identity;
using BlogApplication.Persistence.Contexts;
using BlogApplication.Persistence.Repositories.Category;
using BlogApplication.Persistence.Repositories.Post;
using BlogApplication.Persistence.Repositories.PostComment;
using BlogApplication.Persistence.Repositories.PostMeta;
using BlogApplication.Persistence.Repositories.Tag;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApplication.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<BlogAppDbContext>(options => options.UseNpgsql(Configuration.ConnectionString));


            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<BlogAppDbContext>();

            services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
            services.AddScoped<IPostReadRepository, PostReadRepository>();
            services.AddScoped<IPostWriteRepository, PostWriteRepository>();
            services.AddScoped<IPostCommentReadRepository, PostCommentReadRepository>();
            services.AddScoped<IPostCommentWriteRepository, PostCommentWriteRepository>();
            services.AddScoped<IPostMetaReadRepository, PostMetaReadRepository>();
            services.AddScoped<IPostMetaWriteRepository, PostMetaWriteRepository>();
            services.AddScoped<ITagReadRepository, TagReadRepository>();
            services.AddScoped<ITagWriteRepository, TagWriteRepository>();
        }
    }
}
