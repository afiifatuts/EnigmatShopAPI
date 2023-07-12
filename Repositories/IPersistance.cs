namespace EnigmaShopApi.Repositories;

public interface IPersistance
{
    Task SaveChangeAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();

}