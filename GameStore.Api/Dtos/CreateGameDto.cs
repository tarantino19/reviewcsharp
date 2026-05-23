namespace GameStore.Api.Dtos;
using System.ComponentModel.DataAnnotations;

public record class CreateGameDto (
    [Required][StringLength(50)] string Name, 
    [Required][StringLength(50)] string Genre, 
    [Required][Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")] decimal Price, 
    [Required] DateOnly ReleaseDate);
    