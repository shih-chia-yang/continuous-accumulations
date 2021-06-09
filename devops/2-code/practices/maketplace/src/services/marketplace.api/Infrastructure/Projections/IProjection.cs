using System.Threading.Tasks;

namespace marketplace.api.Infrastructure.Projections
{
    public interface IProjection
    {
        Task Project(object @event);
    }

}