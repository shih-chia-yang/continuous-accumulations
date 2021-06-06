using System;
using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace test_event
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connection = EventStoreConnection.Create(
                "ConnectTo=tcp://admin:changeit@localhost:1113; DefaultUserCredentials=admin:changeit;",
                ConnectionSettings.Create().KeepReconnecting()
                , "TestConnection");

            connection.ConnectAsync().GetAwaiter();
            var model = new MyModel();
            var item = new EventData(model.Id, model.GetTypeString(), true, model.GetByteArray(), new byte[] { });

            var result = await connection.AppendToStreamAsync("mystream", ExpectedVersion.Any, item);
            Console.WriteLine("Hello World!");
        }

        
    }
}
