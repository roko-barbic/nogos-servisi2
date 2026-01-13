using NogosServisi.DTO;

class GameStatusDTO{
    public int GameId { get; set; }
    public List<PlayerIdDto>? HomePlayers { get; set; }
    public List<PlayerIdDto>? AwayPlayers { get; set; }
    public int HomeScore { get; set; }
    public int AwayScore { get; set; }

}