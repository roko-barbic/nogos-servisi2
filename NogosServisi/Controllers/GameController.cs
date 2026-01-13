using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NogosServisi.Data;
using NogosServisi.DTO;
using NogosServisi.Entities;
using Microsoft.AspNetCore.SignalR;
using NogosServisi.Mappers;



[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly DataContext _context;
    private readonly IHubContext<ScoreHub> _hubContext;


    public GameController(DataContext context, IHubContext<ScoreHub> hubContext)
    {
        _context = context;
        _hubContext = hubContext;
    }

    [HttpGet("getPlayers")]
    public async Task<IActionResult> Players()
    {
        var players = await _context.Players.ToListAsync();
        
        return Ok(players);
    }

    [HttpGet("fetchGames")]
    public async Task<IActionResult> Games()
    {
        var games = await _context.Games.Include(a => a.awayPlayers).Include(a => a.homePlayers).ToListAsync();
        
        return Ok(games);
    }

    [HttpPost("createGame")]
    public async Task<IActionResult> CreateGame([FromBody] GameCreateDTO gameDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var homePlayers = await _context.Players
            .Where(p => gameDTO.HomePlayersId.Contains(p.Id))
            .ToListAsync();

        var awayPlayers = await _context.Players
            .Where(p => gameDTO.AwayPlayersId.Contains(p.Id))
            .ToListAsync();

        var game = new Game
        {
            Club_Home_Score = 0,
            Club_Away_Score = 0,
            homePlayers = homePlayers,
            awayPlayers = awayPlayers
        };

        _context.Games.Add(game);
        await _context.SaveChangesAsync();

        return Ok(game);
    }

    [HttpPost("addEvent")]
    public async Task<IActionResult> AddNewEvent(AddNewEventDTO addNewEventDTO)
    {
        var game =  await _context.Games.Include(b => b.Events).Include(c => c.homePlayers).Include(c => c.awayPlayers).FirstOrDefaultAsync(a => a.Id == addNewEventDTO.GameId);
        if(game == null) return BadRequest("Game not found");

        Events newEvent = new Events {
            Player_OneId = addNewEventDTO.Player_One, 
            Player_TwoId = addNewEventDTO.Player_Two, 
            Type = addNewEventDTO.Type
        };
        // newEvent.Player_One = await _context.Players.FirstOrDefaultAsync(a => a.Id == addNewEventDTO.Player_One);
        // newEvent.Player_Two = await _context.Players.FirstOrDefaultAsync(a => a.Id == addNewEventDTO.Player_Two);
        newEvent.Type = addNewEventDTO.Type;
        var eventList = game.Events.ToList();
        eventList.Add(newEvent);  
        game.Events = eventList;
        game = ScoreHelper.UpdateScore(game, newEvent);
        //_context.Events.Add(newEvent);
        await _context.SaveChangesAsync();

        await _hubContext.Clients.All.SendAsync("ScoreUpdated", new
        {
            score1 = game.Club_Home_Score,
            score2 = game.Club_Away_Score,
            scorer = newEvent?.Player_One?.LastName ?? "",    
            assist = newEvent?.Player_Two?.LastName ?? "",  
        });

        return Ok(game);
    }

    [HttpGet("fetchGame/{id}")]
    public async Task<IActionResult> FetchGame(int id)
    {
        var game =  await _context.Games.Include(b => b.Events)
                                        .Include(c => c.homePlayers)
                                        .Include(c => c.awayPlayers)
                                        .FirstOrDefaultAsync(a => a.Id == id);

        if(game == null) return BadRequest();

        var homePlayerDtos = game.homePlayers.Select(p => p.ToDto())
                                             .ToList();

        var awayPlayerDtos = game.awayPlayers.Select(p => p.ToDto())
                                             .ToList();

        GameStatusDTO gameStatusDTO = new GameStatusDTO
        {
            GameId = game.Id,
            HomePlayers = homePlayerDtos,
            AwayPlayers = awayPlayerDtos,
            HomeScore = game.Club_Home_Score,
            AwayScore = game.Club_Away_Score
        };

        return Ok(gameStatusDTO);
    }

}