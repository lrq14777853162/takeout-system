using FoodDelivery.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace FoodDelivery.Notifications;

/// <summary>通知</summary>
public class Notification : FullAuditedAggregateRoot<Guid>
{
    public Guid UserId { get; set; }
    public NotificationType Type { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public Guid? RelatedEntityId { get; set; }

    protected Notification() { }

    public Notification(Guid id, Guid userId, NotificationType type, string title, string content) : base(id)
    {
        UserId = userId;
        Type = type;
        Title = title;
        Content = content;
    }
}
