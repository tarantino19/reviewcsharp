namespace GameStore.Api.Dtos;
using System.ComponentModel.DataAnnotations;

public record class CreateGameDto (
    string Name, 
    string Genre, 
    decimal Price, 
    DateOnly ReleaseDate);
    