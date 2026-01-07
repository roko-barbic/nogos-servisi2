using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NogosServisi.Data;
using NogosServisi.Entities;


namespace NogosServisi.Seed;
public static class DefaultSeeds
{

    public static async Task<bool> SeedAsync(DataContext dataContext)
    {
        // // add clubs
        /*string clubsJSON = File.ReadAllText(@"Seed" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + Path.DirectorySeparatorChar + "clubs.json");
        List<Club> clubsList = JsonConvert.DeserializeObject<List<Club>>(clubsJSON);
        dataContext.Clubs.AddRange(clubsList);*/
         
        //OVO JE PRVI KORAK I NEMOJ ZAB NAPRAVITI DOCKER COMPOSE UP --BUILD 
        
        var seedFilePath = Path.Combine("DBSeed", "PlayerSeed.json");
        Console.WriteLine($"Attempting to read seed file from: {Path.GetFullPath(seedFilePath)}");

        if (!File.Exists(seedFilePath))
        {
            Console.WriteLine("Seed file not found at: " + Path.GetFullPath(seedFilePath));
        }
        else
        {
            string clubsJSON = File.ReadAllText(seedFilePath);
            List<Player> players = JsonConvert.DeserializeObject<List<Player>>(clubsJSON);
            dataContext.Players.AddRange(players);
            Console.WriteLine($"Seeded {players.Count} clubs.");
            foreach(var player in players)
            {
                player.DateOfBirth = DateTime.SpecifyKind(player.DateOfBirth, DateTimeKind.Utc);
            }
        }
        
        await dataContext.SaveChangesAsync();
        dataContext.ChangeTracker.Clear();


        
        // await dataContext.SaveChangesAsync();

        // add players
        /*string playersJSON = File.ReadAllText(@"Seed" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + Path.DirectorySeparatorChar + "playersDu.json");
        List<Player> playersList = JsonConvert.DeserializeObject<List<Player>>(playersJSON);
        
        
        int i = 22;
        foreach (var player in playersList)
        {
            player.DateOfBirth = player.DateOfBirth.ToUniversalTime();
            dataContext.Add(player);
            dataContext.Entry(player).Property("ClubId").CurrentValue = i;
        }*/
        /*for (int j = 1; j< 7; j++)
        {
            string playersJSON = "";
            if (j == 1)
            {
                playersJSON = File.ReadAllText(@"Seed" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + Path.DirectorySeparatorChar + "playersZg.json");
            }
            else if (j == 2)
            {
                playersJSON = File.ReadAllText(@"Seed" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + Path.DirectorySeparatorChar + "playersSt.json");
            }
            else if (j == 3)
            {
                playersJSON = File.ReadAllText(@"Seed" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + Path.DirectorySeparatorChar + "playersOs.json");
            }
            else if (j == 4)
            {
                playersJSON = File.ReadAllText(@"Seed" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + Path.DirectorySeparatorChar + "playersBn.json");
            }
            else if (j == 5)
            {
                playersJSON = File.ReadAllText(@"Seed" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + Path.DirectorySeparatorChar + "playersDu.json");
            }
            else if (j == 6)
            {
                playersJSON = File.ReadAllText(@"Seed" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + Path.DirectorySeparatorChar + "playersTu.json");
            }
            List<Player> playersList = JsonConvert.DeserializeObject<List<Player>>(playersJSON);
            
            foreach (var player in playersList)
            {
                player.DateOfBirth = player.DateOfBirth.ToUniversalTime();
                dataContext.Add(player);
                dataContext.Entry(player).Property("ClubId").CurrentValue = j;
            }   
        }
        */
        
        
        //DRUGI KORAK
        //Potencijalno jos bolj verzija
        
       /* for (int clubId = 1; clubId < 7; clubId++)
        {
            // Use a switch expression to choose the file name based on clubId.
            string fileName = clubId switch
            {
                1 => "playersZg.json",
                2 => "playersSt.json",
                3 => "playersOs.json",
                4 => "playersBn.json",
                5 => "playersDu.json",
                6 => "playersTu.json",
                _ => throw new ArgumentOutOfRangeException(nameof(clubId), "Invalid club id")
            };

            // Construct the file path in a platform-independent way.
            string filePath = Path.Combine("Seed", "Data", fileName);
            Console.WriteLine($"[DEBUG] ClubId {clubId}: Reading file {Path.GetFullPath(filePath)}");

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"[ERROR] File not found for clubId {clubId}: {Path.GetFullPath(filePath)}");
                continue;
            }

            // Read and deserialize the JSON.
            string playersJSON = File.ReadAllText(filePath);
            List<Player> playersList = JsonConvert.DeserializeObject<List<Player>>(playersJSON);
            Console.WriteLine($"[DEBUG] ClubId {clubId}: Loaded {playersList.Count} players from {fileName}");

            // Assign each player the current clubId.
            foreach (var player in playersList)
            {
                player.DateOfBirth = player.DateOfBirth.ToUniversalTime();
                dataContext.Players.Add(player);
                dataContext.Entry(player).Property("ClubId").CurrentValue = clubId;
            }
            await dataContext.SaveChangesAsync();
            dataContext.ChangeTracker.Clear();
        }

*/
        // await dataContext.SaveChangesAsync();
          
        
        
        //add tornaments
        // string tournamentsJSON = File.ReadAllText(@"Seed" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + Path.DirectorySeparatorChar + "tournaments.json");
        // List<Tournament> torunamentsList = JsonConvert.DeserializeObject<List<Tournament>>(tournamentsJSON);
        // dataContext.Tournaments.AddRange(torunamentsList);

        // await dataContext.SaveChangesAsync();

        //add clubs to tournaments
        // add 4 radnom clubs to every tournament
        // var clubs = await dataContext.Clubs.ToListAsync();
        // var tournaments = await dataContext.Tournaments.ToListAsync();
        //var rnd = new Random();
        // foreach (var tournament in tournaments)
        // {
        //     var clubsInTournament = new List<Club>();
        //     for (int i = 0; i < 4; i++)
        //     {
        //         var club = clubs[rnd.Next(clubs.Count)];
        //         clubsInTournament.Add(club);
        //     }
        //     tournament.Clubs = clubsInTournament;
        // }


        //add events
         /*var games = await dataContext.Games.Include(a => a.Club_Home).ThenInclude(b => b.Players).Include(c=> c.Club_Away).ThenInclude(d => d.Players).ToListAsync();
         foreach (var game in games)
         {
             var eventsInGame = new List<EventInGame>();
             var homeClub = game.Club_Home;
             var awayClub = game.Club_Away;
             var homePlayers = await dataContext.Players
                 .Where(player => player.Club == homeClub).ToListAsync();
             var awayPlayers = await dataContext.Players
                 .Where(player => player.Club == awayClub).ToListAsync();
             // ICollection<Player> playersCollection1 = homeClub.Players;
             // ICollection<Player> playersCollection2 = awayClub.Players;
             for (int i = 0; i < 5; i++)
             {
                
                 var type = i+1;
                 var eventNew = new EventInGame();
                 eventNew.Type=type;

             if (homePlayers.Count > 0 && awayPlayers.Count > 0)
             {
                 int randomIndex = rnd.Next(homePlayers.Count);
                 eventNew.Player_One = homePlayers.ElementAt(1); //to je QB
                 if(type ==2 || type == 3){
                     eventNew.Player_Two = awayPlayers.ElementAt(randomIndex);//int catc
                 }
                 else{
                     eventNew.Player_Two = homePlayers.ElementAt(randomIndex);//dobar cathc
                 }

             }
             eventsInGame.Add(eventNew);
             }
             //sad eventi za drugu ekipu
             for (int i = 0; i < 5; i++)
             {
                
                 var type = i+1;
                 var eventNew = new EventInGame();
                 eventNew.Type=type;

             if (homePlayers.Count > 0 && awayPlayers.Count > 0)
             {
                 int randomIndex = rnd.Next(homePlayers.Count);
                 eventNew.Player_One = awayPlayers.ElementAt(1); //to je QB
                 if(type ==2 || type == 3){
                     eventNew.Player_Two = homePlayers.ElementAt(randomIndex);//int catc
                 }
                 else{
                     eventNew.Player_Two = awayPlayers.ElementAt(randomIndex);//dobar cathc
                 }

             }
             eventsInGame.Add(eventNew);
             }

             //dodat sve evente u game
             game.Events = eventsInGame;
         }*/

        //add score in games
        // var i = 0;
        // foreach (var game in games)
        // {
        //     game.Club_Home_Score = 36 + i;
        //     game.Club_Away_Score = 38 - i;
        //     i++;
        // }

        //add admin

        // var admin = new Admin();
        // admin.FirstName = "Leon";
        // admin.LastName = "Ljubas";
        // admin.Username = "admin1";
        // admin.PasswordHash = BCrypt.Net.BCrypt.HashPassword("adminPass123");
        // dataContext.Admins.Add(admin);

        //await dataContext.SaveChangesAsync();
        
        return true;
    }
}
