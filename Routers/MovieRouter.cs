using Components;
using Data.Models;
using Operations;

namespace Routers;

public class MovieRouter : RouterBase
{
    private IMovieOperations _operations;

    public MovieRouter(IMovieOperations operations)
    {
        UrlFragment = "movies";
        _operations = operations;
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
        return TypedResults.Ok(await _operations.GetMovieAsync(id));
    }

    protected async virtual Task<IResult> GetAllMovies()
    {
        return TypedResults.Ok(await _operations.GetMoviesAsync());
    }

    protected async virtual Task<IResult> AddMovie(Movie movie)
    {
        await _operations.AddMovieAsync(movie);

        return TypedResults.Created($"/movies/{movie.Id}", movie);
    }

    protected async virtual Task<IResult> UpdateMovie(Movie movie)
    {
        var updatedMovie = await _operations.UpdateMovieAsync(movie);

        return updatedMovie is Movie ? TypedResults.NoContent() : TypedResults.NotFound();
    }

    protected async virtual Task<IResult> DeleteMovie(Guid id)
    {
        var deletedMovie = await _operations.DeleteMovieAsync(id);

        return deletedMovie is Movie ? TypedResults.Ok(deletedMovie) : TypedResults.NotFound();
    }
}