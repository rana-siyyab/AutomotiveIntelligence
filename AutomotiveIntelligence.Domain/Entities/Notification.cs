namespace AutomotiveIntelligence.Domain.Entities;

public class Notification
{
    public long NotificationId { get; set; }

    public int UserId { get; set; }

    public string NotificationType { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public bool IsRead { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ReadDate { get; set; }
}