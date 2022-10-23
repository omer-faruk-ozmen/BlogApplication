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
    public DbSet<PostCategory> PostCategories { get; set; }
    public DbSet<PostTag> PostTags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(Configuration.ConnectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PostCategory>()
            .HasKey(p => new
            {
                p.CategoryId,
                p.PostId
            });

        modelBuilder.Entity<PostCategory>()
            .HasOne(p => p.Category)
            .WithMany(p => p.PostCategories)
            .HasForeignKey(p => p.CategoryId).IsRequired();

        modelBuilder.Entity<PostCategory>()
            .HasOne(p => p.Post)
            .WithMany(p => p.PostCategories)
            .HasForeignKey(p => p.PostId).IsRequired();
        


        modelBuilder.Entity<PostTag>()
            .HasKey(p => new { p.PostId, p.TagId });

        modelBuilder.Entity<PostTag>()
            .HasOne(p => p.Tag)
            .WithMany(p => p.PostTags)
            .HasForeignKey(p => p.TagId);

        modelBuilder.Entity<PostTag>()
            .HasOne(p => p.Post)
            .WithMany(p => p.PostTags)
            .HasForeignKey(p => p.PostId);
        



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
        foreach (var entityEntry in ChangeTracker.Entries())
        {
            if (entityEntry.Entity is BaseEntity)
            {
                if (entityEntry.State is EntityState.Added)
                {

                    ((BaseEntity)entityEntry.Entity).CreateDate = DateTime.UtcNow.AddHours(3);
                    ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.UtcNow.AddHours(3);
                }

                if (entityEntry.State is EntityState.Modified)
                {
                    ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.UtcNow.AddHours(3);
                }

            }

        }


    }


}