using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Common.Events.Post;
using Dapper;
using Npgsql;

namespace BlogApplication.Projections.FavoriteService.Services
{
    public class FavoriteService
    {
        private readonly string connectionString;

        public FavoriteService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task CreatePostFavorite(CreatePostFavoriteEvent @event)
        {
            using var connection = new NpgsqlConnection(connectionString);


            await connection.ExecuteAsync("INSERT INTO PostFavorites (Id, PostId, CreatedById, CreateDate) VALUES(@Id, @PostId, @CreatedById, GETDATE())",
                new
            {
                    Id=Guid.NewGuid(),
                    PostId=@event.PostId,
                    CreatedById=@event.CreatedBy,
            });
        }
    }
}
