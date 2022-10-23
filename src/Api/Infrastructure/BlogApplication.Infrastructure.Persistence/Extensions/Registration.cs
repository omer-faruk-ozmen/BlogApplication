using BlogApplication.Api.Application.Interfaces.Repositories.Category;
using BlogApplication.Api.Application.Interfaces.Repositories.EmailConfirmation;
using BlogApplication.Api.Application.Interfaces.Repositories.Post;
using BlogApplication.Api.Application.Interfaces.Repositories.PostComment;
using BlogApplication.Api.Application.Interfaces.Repositories.Tag;
using BlogApplication.Api.Application.Interfaces.Repositories.User;
using BlogApplication.Api.Application.Interfaces.Services;
using BlogApplication.Infrastructure.Persistence.Context;
using BlogApplication.Infrastructure.Persistence.Repositories.Category;
using BlogApplication.Infrastructure.Persistence.Repositories.EmailConfirmation;
using BlogApplication.Infrastructure.Persistence.Repositories.Post;
using BlogApplication.Infrastructure.Persistence.Repositories.PostComment;
using BlogApplication.Infrastructure.Persistence.Repositories.Tag;
using BlogApplication.Infrastructure.Persistence.Repositories.User;
using BlogApplication.Infrastructure.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApplication.Infrastructure.Persistence.Extensions;

public static class Registration
{
    public static IServiceCollection AddPersistenceRegistration(this IServiceCollection services)
    {
        services.AddDbContext<BlogApplicationContext>(options =>
        {

            options.UseNpgsql(Configuration.ConnectionString);
        });

        var seedData = new SeedData();
        seedData.SeedAsync().GetAwaiter().GetResult();

        services.AddScoped<IUserReadRepository, UserReadRepository>();
        services.AddScoped<IUserWriteRepository, UserWriteRepository>();

        services.AddScoped<IPostReadRepository, PostReadRepository>();
        services.AddScoped<IPostWriteRepository, PostWriteRepository>();

        services.AddScoped<IPostCommentReadRepository, PostCommentReadRepository>();
        services.AddScoped<IPostCommentWriteRepository, PostCommentWriteRepository>();

        services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
        services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();

        services.AddScoped<ITagReadRepository, TagReadRepository>();
        services.AddScoped<ITagWriteRepository, TagWriteRepository>();

        services.AddScoped<IEmailConfirmationReadRepository, EmailConfirmationReadRepository>();
        services.AddScoped<IEmailConfirmationWriteRepository, EmailConfirmationWriteRepository>();

        services.AddScoped<IPostService, PostService>();




        return services;
    }
}