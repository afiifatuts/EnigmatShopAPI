namespace EnigmaShopApi.Repositories;

public class DbPersistence : IPersistance
{
    private readonly AppDbContext _context;
    public DbPersistence(AppDbContext context)
    {
        _context = context;
    }
    public async Task SaveChangeAsync()
    {
        await _context.SaveChangesAsync();
    }
    public async Task BeginTransactionAsync()
    {
        await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _context.Database.CommitTransactionAsync();

    }

    public async Task RollbackTransactionAsync()
    {
        await _context.Database.RollbackTransactionAsync();

    }


}