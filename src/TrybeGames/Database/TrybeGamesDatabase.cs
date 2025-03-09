namespace TrybeGames;

public class TrybeGamesDatabase
{
    public List<Game> Games = new List<Game>();

    public List<GameStudio> GameStudios = new List<GameStudio>();

    public List<Player> Players = new List<Player>();

    // 4. Crie a funcionalidade de buscar jogos desenvolvidos por um estúdio de jogos
    public List<Game> GetGamesDevelopedBy(GameStudio gameStudio)
    {
        return Games.Where(game => gameStudio.Id == game.DeveloperStudio).ToList();
    }

    // 5. Crie a funcionalidade de buscar jogos jogados por uma pessoa jogadora
    public List<Game> GetGamesPlayedBy(Player player)
    {
        return Games.Where(game => game.Players.Contains(player.Id)).ToList();  
    }

    // 6. Crie a funcionalidade de buscar jogos comprados por uma pessoa jogadora
    public List<Game> GetGamesOwnedBy(Player playerEntry)
    {
        return Games.Where(game => game.Players.Contains(playerEntry.Id)).ToList();
    }


    // 7. Crie a funcionalidade de buscar todos os jogos junto do nome do estúdio desenvolvedor
    public List<GameWithStudio> GetGamesWithStudio()
    {
        return Games.Select(
            game => new GameWithStudio
            {
                GameName = game.Name,
                StudioName = GameStudios.First(studio => studio.Id == game.DeveloperStudio).Name,
                NumberOfPlayers = game.Players.Count,
            }
        ).ToList();                      
    }
    
    // 8. Crie a funcionalidade de buscar todos os diferentes Tipos de jogos dentre os jogos cadastrados
    public List<GameType> GetGameTypes()
    {
        return Games.Select(game => game.GameType).Distinct().ToList();
    }

    // 9. Crie a funcionalidade de buscar todos os estúdios de jogos junto dos seus jogos desenvolvidos com suas pessoas jogadoras
    public List<StudioGamesPlayers> GetStudiosWithGamesAndPlayers()
    {
        return Games.GroupBy(game => game.DeveloperStudio)
        .Select(studio => new StudioGamesPlayers
        {
            GameStudioName = GameStudios.First(games => games.Id == studio.Key).Name,
            Games = studio.Select(g => new GamePlayer
            {
                GameName = g.Name,
                Players = Players.Where(player => g.Players.Contains(player.Id)).ToList(),
            }).ToList(),
        }).ToList();
    }

}
