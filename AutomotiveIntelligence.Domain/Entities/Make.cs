namespace AutomotiveIntelligence.Domain.Entities;

public class Make
{
    public int MakeId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Country { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}