using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class MovieCollectionContext : DbContext
{
    public MovieCollectionContext(DbContextOptions<MovieCollectionContext> options) : base(options) { }

    public DbSet<Movie> Movies => Set<Movie>();
}