using AutoMapper;
using Data;
using Data.DTOs;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Operations;

public interface IMovieOperations
{
    public Task<Movie> AddMovieAsync(NewMovieDto movie);
    public Task<Movie?> DeleteMovieAsync(Guid id);
    public Task<Movie?> GetMovieAsync(Guid id);
    public Task<Movie?> UpdateMovieAsync(Guid id, UpdateMovieDto updateMovie);
    public Task<IEnumerable<Movie>> GetMoviesAsync();
}

public class MovieOperations : IMovieOperations
{
    private readonly MovieCollectionContext _context;
    private readonly IMapper _mapper;

    public MovieOperations(MovieCollectionContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Movie> AddMovieAsync(NewMovieDto newMovie)
    {
        var movie = _mapper.Map<Movie>(newMovie);
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

    public async Task<Movie?> UpdateMovieAsync(Guid id, UpdateMovieDto updatedMovie)
    {
        var originalMovie = await _context.Movies.FindAsync(id);

        if (originalMovie is null)
            return null;

        _mapper.Map(updatedMovie, originalMovie);
        await _context.SaveChangesAsync();

        return originalMovie;
    }

    public async Task<Movie?> GetMovieAsync(Guid id)
    {
        return await _context.Movies.FindAsync(id) is Movie movie ? movie : null;
    }

    public async Task<IEnumerable<Movie>> GetMoviesAsync()
    {
        return await _context.Movies.ToArrayAsync();
    }
}
