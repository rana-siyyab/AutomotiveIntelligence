namespace AutomotiveIntelligence.Domain.Entities;

public class Favorite
{
    public long FavoriteId { get; set; }

    public int UserId { get; set; }

    public int VehicleId { get; set; }

    public DateTime CreatedDate { get; set; }
}