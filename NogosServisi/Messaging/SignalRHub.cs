using Microsoft.AspNetCore.SignalR;
using NogosServisi.Data;

public class ScoreHub : Hub
{
    private readonly DataContext _context;

    public ScoreHub(DataContext context)
    {
        _context = context;
    }

    public async Task SendCurrentScore(int gameId)
    {
        var game = await _context.Games.FindAsync(gameId);
        await Clients.Caller.SendAsync("ScoreUpdated", new
        {
            HomeScore = game.Club_Home_Score,
            AwayScore = game.Club_Away_Score
        });
    }
}