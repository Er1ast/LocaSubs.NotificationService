using NotificationService.Models.Common;
using NotificationService.Models.ServiceReceiver;

namespace NotificationService.External.Clients.ServiceReceiver;

public interface IServiceReceiverClient
{
    Task<IReadOnlyCollection<NearbySeance>> GetNextSessionAsync(
        double coordinateLat,
        double coordinateLon,
            long radius,
            ServiceType serviceType);
}
