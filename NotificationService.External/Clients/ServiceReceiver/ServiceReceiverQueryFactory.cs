using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using NotificationService.External.Clients.Constants;
using NotificationService.Configuration.Options;
using NotificationService.Models.Common;

namespace NotificationService.External.Clients.ServiceReceiver;

public class ServiceReceiverQueryFactory : IServiceReceiverQueryFactory
{
    private readonly ServiceAddressesOptions _options;

    public ServiceReceiverQueryFactory(IOptions<ServiceAddressesOptions> options)
    {
        _options = options.Value;
    }

    public string NextSession(double coordinateLat, double coordinateLon, long radius, ServiceType serviceType)
    {
        const string NextSession = "next-session";

        const string CoordinateLat = "coordinateLat";
        const string CoordinateLon = "coordinateLon";
        const string Radius = "radius";
        const string ServiceType = "serviceType";

        var serviceReceiverOptions = _options.Services.FirstOrDefault(s => s.Service == ServiceNames.ServiceReceiver);

        string uri = $"https://{serviceReceiverOptions.Host}:{serviceReceiverOptions.Port}/{NextSession}";

        Dictionary<string, string?> queryParams = new()
        {
            { CoordinateLat, coordinateLat.ToString() },
            { CoordinateLon, coordinateLon.ToString() },
            { Radius, radius.ToString() },
            { ServiceType, ((int)serviceType).ToString() }
        };
        var resultQuery = QueryHelpers.AddQueryString(uri, queryParams);

        return resultQuery;
    }
}
