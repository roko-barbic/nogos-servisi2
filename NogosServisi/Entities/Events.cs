using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
namespace NogosServisi.Entities;
public class Events
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public Player Player_One {get; set;}
    public Player? Player_Two {get; set;}
    public int Type {get; set;}
}