using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Operations;

public interface IMovieOperations
{
    public Task<Movie> AddMovie(Movie movie);
    public Task<Movie?> DeleteMovie(Guid id);
    public Task<Movie?> GetMovie(Guid id);
    public Task<IEnumerable<Movie>> GetMovies();
}

public class MovieOperations : IMovieOperations
{
    private readonly MovieCollectionContext _context;

    public MovieOperations(MovieCollectionContext context)
    {
        _context = context;
    }

    public async Task<Movie> AddMovie(Movie movie)
    {
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        return movie;
    }

    public async Task<Movie?> DeleteMovie(Guid id)
    {
        if (await _context.Movies.FindAsync(id) is Movie movie)
        {
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return movie;
        }

        return null;
    }

    public async Task<Movie?> GetMovie(Guid id)
    {
        return await _context.Movies.FindAsync(id)
            is Movie movie
                ? movie
                : null;
    }

    public async Task<IEnumerable<Movie>> GetMovies()
    {
        return await _context.Movies.ToArrayAsync();
    }
}