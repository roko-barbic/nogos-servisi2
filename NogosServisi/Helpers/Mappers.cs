namespace NogosServisi.Mappers;

using NogosServisi.Entities;
using NogosServisi.DTO;

public static class PlayerMapper
{
    public static PlayerIdDto ToDto(this Player player)
    {
        if (player == null) return null;

        return new PlayerIdDto(
            player.FirstName,
            player.LastName,
            player.Id
        );
    }
}