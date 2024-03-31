using NotificationService.Models.SubscriptionService;

namespace NotificationService.External.Clients.SubscriptionService;

public interface ISubscriptionServiceClient
{
    Task<IReadOnlyCollection<Subscription>> GetSubscriptionsAsync(string userLogin);
}
