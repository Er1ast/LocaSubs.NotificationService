using NotificationService.Models.Common;

namespace NotificationService.Models.SubscriptionService;

public record Subscription
{
    public long Id { get; set; }
    public string? UserLogin { get; set; }
    public long Range { get; set; }
    public ServiceType ServiceType { get; set; }
}