using Components;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Routers;

public class MovieRouter : RouterBase
{
    private MovieCollectionContext _context;

    public MovieRouter(MovieCollectionContext context)
    {
        UrlFragment = "movies";
        _context = context;
    }

    public override void AddRoutes(WebApplication app)
    {
        app.MapGet($"/{UrlFragment}", () => GetAllMovies());
        app.MapGet($"/{UrlFragment}/{{id:guid}}", (Guid id) => GetMovie(id));
        app.MapPost($"/{UrlFragment}", (Movie movie) => AddMovie(movie));
        app.MapPut($"/{UrlFragment}", (Movie movie) => UpdateMovie(movie));
        app.MapDelete($"/{UrlFragment}/{{id:guid}}", (Guid id) => DeleteMovie(id));
    }

    protected async virtual Task<IResult> GetMovie(Guid id)
    {
        return TypedResults.Ok(await _context.Movies.FindAsync(id));
    }

    protected async virtual Task<IResult> GetAllMovies()
    {
        return TypedResults.Ok(await _context.Movies.ToListAsync());
    }

    protected async virtual Task<IResult> AddMovie(Movie movie)
    {
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        return TypedResults.Created($"/movies/{movie.Id}", movie);
    }

    protected async virtual Task<IResult> UpdateMovie(Movie movie)
    {
        var originalMovie = await _context.Movies.FindAsync(movie.Id);

        if (originalMovie is null) return TypedResults.NotFound();

        originalMovie.Title = movie.Title;
        originalMovie.Description = movie.Description;
        originalMovie.ReleaseDate = movie.ReleaseDate;
        originalMovie.Rating = movie.Rating;
        originalMovie.Description = movie.Description;
        originalMovie.InventoryDate = movie.InventoryDate;

        await _context.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    protected async virtual Task<IResult> DeleteMovie(Guid id)
    {
        if (await _context.Movies.FindAsync(id) is Movie movie)
        {
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return TypedResults.Ok(movie);
        }

        return TypedResults.NotFound();
    }
}