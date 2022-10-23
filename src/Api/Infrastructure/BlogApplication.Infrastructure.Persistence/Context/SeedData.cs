using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Api.Domain.Models;
using BlogApplication.Common.Infrastructure;
using BlogApplication.Common.Models;
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
                    .RuleFor(i => i.Password, i => PasswordEncryptor.Encrypt(i.Internet.Password()))
                    .RuleFor(i => i.EmailConfirmed, i => i.PickRandom(true, false))
                    .Generate(50);

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

            foreach (var user in users)
            {
                var result = new Faker<User>("tr").RuleFor(i => i.EmailAddress, i => i.Internet.Email(user.FirstName, user.LastName))
                    .RuleFor(i => i.UserName, i => i.Internet.UserName(user.FirstName, user.LastName))
                    .Generate();

                user.EmailAddress = result.EmailAddress;
                user.UserName = result.UserName;
            }

            var userIds = users.Select(i => i.Id);

            await context.Users.AddRangeAsync(users);

            var guids = Enumerable.Range(0, 1000).Select(i => Guid.NewGuid()).ToList();
            var tagGuids = Enumerable.Range(0, 50).Select(i => Guid.NewGuid()).ToList();
            var categoryGuids = Enumerable.Range(0, 50).Select(i => Guid.NewGuid()).ToList();
            List<int> likedStatusList = new List<int>
            {
                1,0,-1
            };

            int counter = 0;
            int tagCounter = 0;
            int categoryCounter = 0;
            int postCommentCounter = 0;

            //Posts
            var posts = new Faker<Post>("tr")
                .RuleFor(i => i.Id, i => guids[counter++])
                .RuleFor(i => i.CreateDate, i => i.Date.Between(DateTime.Now.AddDays(-200), DateTime.Now))
                .RuleFor(i => i.MetaTitle, i => i.Lorem.Sentence(4, 4))
                .RuleFor(i => i.Title, i => i.Lorem.Sentence(8, 8))
                .RuleFor(i => i.Summary, i => i.Lorem.Paragraph(1))
                .RuleFor(i => i.Content, i => i.Lorem.Paragraph(5))
                .RuleFor(i => i.Published, i => i.PickRandom(true, false))
                .RuleFor(i => i.CreatedById, i => i.PickRandom(userIds))
                .Generate(1000);

            await context.Posts.AddRangeAsync(posts);

            //Comments
            var comments = new Faker<PostComment>("tr")
                    .RuleFor(i => i.Id, i => Guid.NewGuid())
                    .RuleFor(i => i.CreateDate, i => i.Date.Between(DateTime.Now.AddDays(-200), DateTime.Now))
                    .RuleFor(i => i.Title, i => i.Lorem.Sentence(7, 7))
                    .RuleFor(i => i.Content, i => i.Lorem.Paragraph(2))
                    .RuleFor(i => i.CreatedById, i => i.PickRandom(userIds))
                    .RuleFor(i => i.PostId, i => i.PickRandom(guids))
                    .Generate(1000);

            await context.PostComments.AddRangeAsync(comments);



            //Favorites
            var favorites = new Faker<PostFavorite>("tr")
                .RuleFor(i => i.Id, i => Guid.NewGuid())
                .RuleFor(i => i.CreateDate, i => i.Date.Between(DateTime.UtcNow.AddDays(-200), DateTime.UtcNow))
                .RuleFor(i => i.CreatedById, i => i.PickRandom(userIds))
                .RuleFor(i => i.PostId, i => i.PickRandom(guids))
                .Generate(1000);
            await context.PostFavorites.AddRangeAsync(favorites);


            //Tags
            var tags = new Faker<Tag>("tr")
                .RuleFor(i => i.Id, i => tagGuids[tagCounter++])
                .RuleFor(i => i.Name, i => i.Lorem.Word())
                .Generate(20);
            await context.Tags.AddRangeAsync(tags);
            

            //Categories
            var categories = new Faker<Category>("tr")
                .RuleFor(i => i.Id, i => categoryGuids[categoryCounter++])
                .RuleFor(i => i.Name, i => i.Lorem.Word())
                .Generate(20);
            await context.Categories.AddRangeAsync(categories);

            //PostLikes
            var postLikes = new Faker<PostLikes>("tr")
                .RuleFor(i => i.Id, i => Guid.NewGuid())
                .RuleFor(i => i.PostId, i => i.PickRandom(guids))
                .RuleFor(i => i.CreatedById, i => i.PickRandom(userIds))
                .RuleFor(i => i.LikedStatus, i => (LikedStatus)i.PickRandom(likedStatusList))
                .Generate(1000);
            await context.PostLikes.AddRangeAsync(postLikes);




            await context.SaveChangesAsync();
        }
    }
}
