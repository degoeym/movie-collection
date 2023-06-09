using System.Runtime.Serialization;

namespace Data.Models;

public record Movie
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Rating Rating { get; set; }
    public DateTime ReleaseDate { get; set; }
    public DateTime InventoryDate { get; set; }
}

public enum Rating
{
    [EnumMember(Value = "N/R")]
    Unknown = -1,
    G = 1,
    PG = 2,

    [EnumMember(Value = "PG-13")]
    PG13 = 3,
    R = 4,

    [EnumMember(Value = "NC-17")]
    NC17 = 5,
    X = 6,
}
