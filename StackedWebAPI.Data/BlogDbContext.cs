using Microsoft.EntityFrameworkCore;
using StackedWebAPI.Data.Models;

namespace StackedWebAPI.Data;
public class BlogDbContext : DbContext
{
    public BlogDbContext() { }
    public BlogDbContext(DbContextOptions options) : base(options) { }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Article> Articles { get; set; }
    public virtual DbSet<Comment> Comments { get; set; }
    public virtual DbSet<Tag> Tags { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Un Article tiene solo un id de Author, pero el Author tiene varios id de Articles
        modelBuilder.Entity<Article>()
                    .HasOne(article => article.Author)
                    .WithMany(author => author.Articles);

        // Set Foreign key

        modelBuilder.Entity<Comment>()
                    .HasOne(comment => comment.Article)
                    .WithMany(article => article.Comments)
                    .HasForeignKey(comment => comment.ArticleId);

        // Articles <=> Tags, Many to Many relationship
        modelBuilder.Entity<ArticleTag>()
                    .HasKey(at => new { at.ArticleId, at.TagId });


        modelBuilder.Entity<ArticleTag>()
                    .HasOne(at => at.Article)
                    .WithMany(a => a.ArticleTags)
                    .HasForeignKey(at => at.ArticleId);
    }
}
