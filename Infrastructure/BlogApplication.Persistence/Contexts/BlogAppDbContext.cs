using BlogApplication.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Domain.Entities;
using BlogApplication.Domain.Entities.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Persistence.Contexts
{
    public class BlogAppDbContext : IdentityDbContext<AppUser,AppRole,string>
    {
        public BlogAppDbContext(DbContextOptions options):base(options){}

        public DbSet<Post>? Posts { get; set; }
        public DbSet<PostComment>? PostComments { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<PostMeta>? PostMetas { get; set; }
        public DbSet<Tag>? Tags { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.Now,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.Now,
                    _ => DateTime.UtcNow
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
