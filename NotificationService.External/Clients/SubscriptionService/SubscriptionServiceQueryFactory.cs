using Microsoft.Extensions.Options;
using NotificationService.Configuration.Options;
using NotificationService.External.Clients.Constants;

namespace NotificationService.External.Clients.SubscriptionService;

public class SubscriptionServiceQueryFactory : ISubscriptionServiceQueryFactory
{
    private readonly ServiceAddressesOptions _options;

    public SubscriptionServiceQueryFactory(IOptions<ServiceAddressesOptions> options)
    {
        _options = options.Value;
    }

    public string Subscriptions()
    {
        const string method = "subscriptions";

        var subscriptionServiceOptions = _options.Services.FirstOrDefault(s => s.Service == ServiceNames.SubscriptionService);

        string uri = $"https://{subscriptionServiceOptions.Host}:{subscriptionServiceOptions.Port}/{method}";

        return uri;
    }
}
