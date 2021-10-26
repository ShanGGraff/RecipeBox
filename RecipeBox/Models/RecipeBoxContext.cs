using Microsoft.EntityFrameworkCore;

namespace RecipeBox.Models
{
  public class RecipeBoxContext : DbContext
  {
    public DbSet<Recipe> Recipe { get; set; }
    public DbSet<Tag> Tag { get; set; }
    public DbSet<RecipeTag> RecipeTag { get; set; }

    public RecipeBoxContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}