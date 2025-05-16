using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FunctionalTests
{
    public class CustomWebAppFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment(EnvironmentConstants.DEVELOPMENT);

            var host = builder.Build();
            host.Start();

            var serviceProvider = host.Services;

            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<Database>();

                var logger = scopedServices
                    .GetRequiredService<ILogger<CustomWebAppFactory<TProgram>>>();

                db.Database.EnsureCreated();
            }

            return host;
        }
    }
}
