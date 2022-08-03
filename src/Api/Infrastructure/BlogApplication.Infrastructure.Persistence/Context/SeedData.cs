using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Api.Domain.Models;
using BlogApplication.Common.Infrastructure;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Infrastructure.Persistence.Context
{
    internal class SeedData
    {
        private static List<User> getUsers()
        {
            var result = new Faker<User>("tr")
                    .RuleFor(i => i.Id, i => Guid.NewGuid())
                    .RuleFor(i => i.CreateDate, i => i.Date.Between(DateTime.Now.AddDays(-200), DateTime.Now))
                    .RuleFor(i => i.FirstName, i => i.Person.FirstName)
                    .RuleFor(i => i.LastName, i => i.Person.LastName)
                    .RuleFor(i => i.EmailAddress, i => i.Internet.Email())
                    .RuleFor(i => i.UserName, i => i.Internet.UserName())
                    .RuleFor(i => i.Password, i => PasswordEncryptor.Encrypt(i.Internet.Password()))
                    .RuleFor(i => i.EmailConfirmed, i => i.PickRandom(true, false))
                    .Generate(500);

            return result;
        }

        public async Task SeedAsync()
        {
            var dbContextBuilder = new DbContextOptionsBuilder();
            dbContextBuilder.UseNpgsql(Configuration.ConnectionString);

            var context = new BlogApplicationContext(dbContextBuilder.Options);

            if (context.Users.Any())
            {
                await Task.CompletedTask;
                return;
            }

            var users = getUsers();

            var userIds = users.Select(i => i.Id);

            await context.Users.AddRangeAsync(users);

            var guids = Enumerable.Range(0, 150).Select(i => Guid.NewGuid()).ToList();
            int counter = 0;

            var posts = new Faker<Post>("tr")
                .RuleFor(i => i.Id,i=> guids[counter++])
                .RuleFor(i => i.CreateDate, i => i.Date.Between(DateTime.Now.AddDays(-200), DateTime.Now))
                .RuleFor(i => i.MetaTitle, i => i.Lorem.Sentence(4, 4))
                .RuleFor(i => i.Title, i => i.Lorem.Sentence(8, 8))
                .RuleFor(i => i.Summary, i => i.Lorem.Paragraph(1))
                .RuleFor(i => i.Content, i => i.Lorem.Paragraph(5))
                .RuleFor(i => i.Published, i => i.PickRandom(true, false))
                .RuleFor(i => i.CreatedById, i => i.PickRandom(userIds))
                .Generate(150);

            await context.Posts.AddRangeAsync(posts);

            var comments = new Faker<PostComment>("tr")
                    .RuleFor(i => i.Id,i=> Guid.NewGuid())
                    .RuleFor(i => i.CreateDate, i => i.Date.Between(DateTime.Now.AddDays(-200), DateTime.Now))
                    .RuleFor(i => i.Title, i => i.Lorem.Sentence(7, 7))
                    .RuleFor(i => i.Content, i => i.Lorem.Paragraph(2))
                    .RuleFor(i => i.CreatedById, i => i.PickRandom(userIds))
                    .RuleFor(i => i.PostId, i => i.PickRandom(guids))
                    .Generate(1000);

            await context.PostComments.AddRangeAsync(comments);

            await context.SaveChangesAsync();
        }
    }
}
