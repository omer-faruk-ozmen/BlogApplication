using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Infrastructure.Persistence.Context;

public class BlogApplicationContext : DbContext
{

    public const string DEFAULT_SCHEMA = "dbo";

    public BlogApplicationContext()
    {

    }

    public BlogApplicationContext(DbContextOptions options) : base(options)
    {
        
    }


    public DbSet<User> Users { get; set; }
    public DbSet<EmailConfirmation> EmailConfirmations { get; set; }

    public DbSet<Post> Posts { get; set; }
    public DbSet<PostLikes> PostLikes { get; set; }
    public DbSet<PostFavorite> PostFavorites { get; set; }

    public DbSet<PostComment> PostComments { get; set; }
    public DbSet<PostCommentLikes> PostCommentLikes { get; set; }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(Configuration.ConnectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override int SaveChanges()
    {
        OnBeforeSave();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSave();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        OnBeforeSave();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        OnBeforeSave();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void OnBeforeSave()
    {
        var addedEntites = ChangeTracker.Entries()
            .Where(i => i.State == EntityState.Added)
            .Select(i => (BaseEntity)i.Entity);

        PrepareAddedEntities(addedEntites);

        var modifiedEntities = ChangeTracker.Entries()
            .Where(i => i.State == EntityState.Modified)
            .Select(i => (BaseEntity)i.Entity);

        PrepareModifiedEntities(modifiedEntities);
    }



    private void PrepareAddedEntities(IEnumerable<BaseEntity> entities)
    {

        foreach (var entity in entities)
        {
            entity.CreateDate = DateTime.UtcNow.AddHours(3);
            entity.UpdatedDate = DateTime.UtcNow.AddHours(3);
        }
    }

    private void PrepareModifiedEntities(IEnumerable<BaseEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.UpdatedDate = DateTime.UtcNow.AddHours(3);
        }
    }
}