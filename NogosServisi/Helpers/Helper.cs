using NogosServisi.Entities;

public static class ScoreHelper
{
    public static Game UpdateScore(Game game, Events newEvent){
        if (game.awayPlayers.Contains(newEvent.Player_One))
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