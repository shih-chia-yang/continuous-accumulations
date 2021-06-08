using System.Threading;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using Microsoft.Extensions.Hosting;

namespace marketplace.api.Infrastructure
{
    public class HostedService:IHostedService
    {
        private readonly IEventStoreConnection _connection;

        private readonly EsSubscription _subscription;

        public HostedService(IEventStoreConnection connection,
        EsSubscription subscription)
        {
            _connection = connection;
            _subscription = subscription;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //連線後，再開啟訂閱功能
            await _connection.ConnectAsync();
            _subscription.Start();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            //關閉訂閱功能，再關閉連線
            _subscription.Stop();
            _connection.Close();
            return Task.CompletedTask;
        }
    }
}