using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace marketplace.infrastructure.CheckPoints
{
    public class SqlCheckPointStore : ICheckPointStore
    {
        private readonly string _checkpointName;
        public SqlCheckPointStore(string checkpointName)
        {
            _checkpointName = checkpointName;
        }
        public Task<Position> GetCheckPoint()
        {
            throw new System.NotImplementedException();
        }

        public Task StoreCheckPoint(Position checkpoint)
        {
            throw new System.NotImplementedException();
        }
    }
}