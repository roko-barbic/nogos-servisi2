using System.Text.Json;


public class GameCreateDTO
{
    public GameCreateDTO()
    {
    }

    public List<int> HomePlayersId
    {
        get; set;
    }
    public List<int> AwayPlayersId
    {
        get; set;
    }
}