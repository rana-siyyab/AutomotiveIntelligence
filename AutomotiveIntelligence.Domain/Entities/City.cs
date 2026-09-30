namespace AutomotiveIntelligence.Domain.Entities;

public class City
{
    public int CityId { get; set; }

    public int ProvinceId { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}