namespace NotificationService.External.Clients.Common;

public interface IHttpClientFactory
{
    HttpClient CreateClient();
}
