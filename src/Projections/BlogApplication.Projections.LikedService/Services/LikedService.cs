using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using BlogApplication.Common.Events.Post;

namespace BlogApplication.Projections.LikedService.Services
{
    public class LikedService
    {
        private readonly string connectionString;

        public LikedService(string connectionString)
        {
            this.connectionString = connectionString;
        }


        public async Task CreatePostLiked(CreatePostLikesEvent likes)
        {
            await DeletePostLiked(likes.PostId, likes.CreatedBy);

            using var connection = new NpgsqlConnection(connectionString);



            await connection.ExecuteAsync("INSERT INTO blogapplicationdb.public.PostLikes(Id, CreateDate, PostId, LikedStatus, CreatedById) VALUES (@Id, GetDate(), @PostId, @LikedStatus, @CreatedById)",
                new
                {
                    Id = Guid.NewGuid(),
                    EntryId = likes.PostId,
                    LikedStatus = (int)likes.LikedStatus,
                    CreatedById = likes.CreatedBy
                });
        }

        public async Task DeletePostLiked(Guid postId, Guid userId)
        {
            using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
var teswt = await connection.GetSchemaAsync();
            await connection.ExecuteAsync("DELETE FROM blogapplicationdb.public.PostLikes WHERE PostId = @PostId AND CREATEDBYID = @UserId",
                new
                {
                    PostId = postId,
                    UserId = userId
                });
            

        }
    }
}
