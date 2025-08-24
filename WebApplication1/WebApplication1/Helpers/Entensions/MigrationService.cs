using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Helpers.Entensions
{
    public class MigrationService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public MigrationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    await context.Database.MigrateAsync(cancellationToken);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
