using NogosServisi.Entities;

public static class ScoreHelper
{
    public static Game UpdateScore(Game game, Events newEvent){
        if (game.awayPlayers.Any(p => p.Id == newEvent.Player_OneId))
        {
            game.Club_Away_Score++;
        }
        else
        {
            game.Club_Home_Score++;;
        }
        return game;
    }
}