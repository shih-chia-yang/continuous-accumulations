namespace marketplace.domain.kernel
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork{ get; }
    }
}