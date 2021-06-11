using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace marketplace.infrastructure.CheckPoints
{
    public interface ICheckPointStore
    {
        Task<Position> GetCheckPoint();
        Task StoreCheckPoint(Position checkpoint);
    }
}