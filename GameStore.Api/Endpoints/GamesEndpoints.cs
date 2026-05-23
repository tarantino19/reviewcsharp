using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGameById";

private static readonly List<GameDto> games = [
    new (
        1,
        "The Legend of Zelda: Breath of the Wild",
        "Action-adventure",
        19.99M,
        new DateOnly(2017, 3, 3)),
    new (
        2,
        "Super Mario Odyssey",
        "Platform",
        59.99M,
        new DateOnly(2017, 10, 27)),
    new (
        3,
        "Red Dead Redemption 2",
        "Action-adventure",
        39.99M,
        new DateOnly(2018, 10, 26))
];


    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {

        var group = app.MapGroup("/games")
            .WithParameterValidation();
        //get all games.  /games
        group.MapGet("/", () => games); //returns the games list as JSON

        //get game by id. /games/1
        group.MapGet("/{id}", (int id) =>
        {
            GameDto? game = games.Find(g => g.Id == id);

            return game != null
                ? Results.Ok(game)
                : Results.NotFound(new
                {
                    message = "Game not found."
                });
        })
        .WithName(GetGameEndpointName);
        // “I just created item #5. You can find it at /games/5.”

        group.MapPost("/", (CreateGameDto createGameDto) =>
        {
            var newGame = new GameDto(
                games.Count + 1,
                createGameDto.Name,
                createGameDto.Genre,
                createGameDto.Price,
                createGameDto.ReleaseDate
            );

            games.Add(newGame);


        //“I just created item #5. You can find it at /games/5.”
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = newGame.Id }, newGame);
        });

        //PUT /games/5
        group.MapPut("/{id}", (int id, UpdateGameDto updateGameDto) =>
        {
            var gameIndex = games.FindIndex(g => g.Id == id);

            if (gameIndex == -1)
            {
                return Results.NotFound(new
                {
                    message = "Record does not exist, record not updated."
                });
            }

            games[gameIndex] = new GameDto(
                id,
                updateGameDto.Name,
                updateGameDto.Genre,
                updateGameDto.Price,
                updateGameDto.ReleaseDate
            );

            return Results.Ok(new
            {
                message = "Game updated successfully"
            });
        });

        //DELETE /games/4
        group.MapDelete("/{id}", (int id) =>
        {
            var gameIndex = games.RemoveAll(g => g.Id == id);

            if (gameIndex == -1)
            {
                return Results.NotFound(new
                {
                    message = "Record does not exist."
                });
            }

            return Results.Ok(new
            {
                message = "Game deleted successfully"
            });

        });

        return group; 
        }
    }