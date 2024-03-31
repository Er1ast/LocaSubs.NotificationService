using Microsoft.AspNetCore.Mvc;
using NotificationService.External.Clients.ServiceReceiver;
using NotificationService.External.Clients.SubscriptionService;
using NotificationService.Models.Common;

namespace NotificationService.Controllers
{
    public class NotificationController : Controller
    {
        private readonly ISubscriptionServiceClient _subscriptionServiceClient;
        private readonly IServiceReceiverClient _serviceReceiverClient;

        public NotificationController(
            ISubscriptionServiceClient subscriptionServiceClient,
            IServiceReceiverClient serviceReceiverClient)
        {
            _subscriptionServiceClient = subscriptionServiceClient;
            _serviceReceiverClient = serviceReceiverClient;
        }

        [HttpGet("receive-notification")]
        public async Task<IActionResult> ReceiveNotification(
            double coordinateLat,
            double coordinateLon,
            ServiceType serviceType,
            [FromHeader] string userLogin)
        {
            var subscriptions = await _subscriptionServiceClient.GetSubscriptionsAsync(userLogin);
            var targetSubscription = subscriptions.FirstOrDefault(subscription => subscription.ServiceType == serviceType);

            if (targetSubscription is null) return NotFound();

            var nextSession = await _serviceReceiverClient
                .GetNextSessionAsync(coordinateLat, coordinateLon, targetSubscription.Range, serviceType);

            return Ok(nextSession);
        }
    }
}
