namespace AutomotiveIntelligence.Domain.Entities;

public class Province
{
    public int ProvinceId { get; set; }

    public int CountryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}