using NotificationService.Models.Common;

namespace NotificationService.External.Clients.ServiceReceiver;

public interface IServiceReceiverQueryFactory
{
    string NextSession(double coordinateLat, double coordinateLon, long radius, ServiceType serviceType);
}
