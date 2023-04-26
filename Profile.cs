using AutoMapper;
using Data.DTOs;
using Data.Models;

public class MovieCollectionProfile : Profile
{
    public MovieCollectionProfile()
    {
        CreateMap<NewMovieDto, Movie>()
            .AfterMap(
                (src, dest) =>
                {
                    dest.InventoryDate = DateTime.SpecifyKind(src.InventoryDate, DateTimeKind.Utc);
                    dest.ReleaseDate = DateTime.SpecifyKind(src.ReleaseDate, DateTimeKind.Utc);
                }
            );

        CreateMap<UpdateMovieDto, Movie>()
            .AfterMap(
                (src, dest) =>
                {
                    dest.InventoryDate = DateTime.SpecifyKind(src.InventoryDate, DateTimeKind.Utc);
                    dest.ReleaseDate = DateTime.SpecifyKind(src.ReleaseDate, DateTimeKind.Utc);
                }
            );
    }
}
