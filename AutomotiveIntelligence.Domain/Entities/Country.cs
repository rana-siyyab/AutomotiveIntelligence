namespace AutomotiveIntelligence.Domain.Entities;

public class Country
{
    public int CountryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Code { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}