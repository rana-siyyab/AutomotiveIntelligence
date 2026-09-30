namespace AutomotiveIntelligence.Domain.Entities;

public class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string? Phone { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}