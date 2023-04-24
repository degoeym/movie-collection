using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Operations;

public interface IMovieOperations
{
    public Task<Movie> AddMovieAsync(Movie movie);
    public Task<Movie?> DeleteMovieAsync(Guid id);
    public Task<Movie?> GetMovieAsync(Guid id);
    public Task<Movie?> UpdateMovieAsync(Movie movie);
    public Task<IEnumerable<Movie>> GetMoviesAsync();
}

public class MovieOperations : IMovieOperations
{
    private readonly MovieCollectionContext _context;

    public MovieOperations(MovieCollectionContext context)
    {
        _context = context;
    }

    public async Task<Movie> AddMovieAsync(Movie movie)
    {
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        return movie;
    }

    public async Task<Movie?> DeleteMovieAsync(Guid id)
    {
        if (await _context.Movies.FindAsync(id) is Movie movie)
        {
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return movie;
        }

        return null;
    }

    public async Task<Movie?> UpdateMovieAsync(Movie movie)
    {
         var originalMovie = await _context.Movies.FindAsync(movie.Id);

        if (originalMovie is null) return null;

        originalMovie.Title = movie.Title;
        originalMovie.Description = movie.Description;
        originalMovie.ReleaseDate = movie.ReleaseDate;
        originalMovie.Rating = movie.Rating;
        originalMovie.Description = movie.Description;
        originalMovie.InventoryDate = movie.InventoryDate;

        await _context.SaveChangesAsync();

        return movie;
    }

    public async Task<Movie?> GetMovieAsync(Guid id)
    {
        return await _context.Movies.FindAsync(id)
            is Movie movie
                ? movie
                : null;
    }

    public async Task<IEnumerable<Movie>> GetMoviesAsync()
    {
        return await _context.Movies.ToArrayAsync();
    }
}