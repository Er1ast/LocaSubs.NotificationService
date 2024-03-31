using Newtonsoft.Json;
using NotificationService.External.Clients.Common;
using NotificationService.Models.Common;
using NotificationService.Models.ServiceReceiver;

namespace NotificationService.External.Clients.ServiceReceiver;

public class ServiceReceiverClient : IServiceReceiverClient
{
    private readonly IServiceReceiverQueryFactory _serviceReceiverQueryFactory;
    private readonly IHttpClientFactory _httpClientFactory;

    public ServiceReceiverClient(
        IServiceReceiverQueryFactory serviceReceiverQueryFactory,
        IHttpClientFactory httpClientFactory)
    {
        _serviceReceiverQueryFactory = serviceReceiverQueryFactory;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IReadOnlyCollection<NearbySeance>> GetNextSessionAsync(
            double coordinateLat,
            double coordinateLon,
            long radius,
            ServiceType serviceType)
    {
        string query = _serviceReceiverQueryFactory
            .NextSession(coordinateLat, coordinateLon, radius, serviceType);
        using HttpClient client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync(query);

        List<NearbySeance> getNextSessionResponse = null!;
        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            getNextSessionResponse = JsonConvert.DeserializeObject<List<NearbySeance>>(data)!;
        }
        return getNextSessionResponse ?? new List<NearbySeance>();
    }
}
