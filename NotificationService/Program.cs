using NotificationService.Configuration.Options;
using NotificationService.External.Clients.Common;
using NotificationService.External.Clients.ServiceReceiver;
using NotificationService.External.Clients.SubscriptionService;

namespace NotificationService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<ServiceAddressesOptions>(options =>
                builder.Configuration.GetSection(ServiceAddressesOptions.SectionName).Bind(options));

            builder.Services.AddTransient<IServiceReceiverQueryFactory, ServiceReceiverQueryFactory>();
            builder.Services.AddTransient<External.Clients.Common.IHttpClientFactory, HttpClientFactory>();
            builder.Services.AddTransient<IServiceReceiverClient, ServiceReceiverClient>();
            builder.Services.AddTransient<ISubscriptionServiceQueryFactory, SubscriptionServiceQueryFactory>();
            builder.Services.AddTransient<ISubscriptionServiceClient, SubscriptionServiceClient>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
