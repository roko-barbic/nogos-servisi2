using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
namespace NogosServisi.Entities;
public class Game
{
    public Game()
    {
        Club_Home_Score = 0;
        Club_Away_Score = 0;
        DateTimeOfMatch = DateTime.UtcNow;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int Club_Home_Score {get; set;}
    public int Club_Away_Score {get; set;}
    public List<Player> homePlayers{ get; set;}
    public List<Player> awayPlayers { get; set; }
    public IEnumerable<Events> Events {get; set;}
    public DateTime? DateTimeOfMatch { get; set; }

}