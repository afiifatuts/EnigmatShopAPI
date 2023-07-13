using EnigmaShopApi.Entities;
using EnigmaShopApi.Exceptions;
using EnigmaShopApi.Repositories;

namespace EnigmaShopApi.Services;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _repository;
    private readonly IPersistance _persistance;

    public ProductService(IRepository<Product> repository, IPersistance persistance)
    {
        _repository = repository;
        _persistance = persistance;
    }

    public async Task<Product> Create(Product payload)
    {
        var product = await _repository.SaveAsync(payload);
        await _persistance.SaveChangeAsync();
        return product;
    }


    public async Task<List<Product>> GetAll()
    {
        return await _repository.FindAllAsync();
    }

    public async Task<Product> GetById(string id)
    {
        try
        {
            var product = await _repository.FindByIdAsync(Guid.Parse(id));
            if (product is null) throw new NotFoundException("Product not found");
            return product;
        }
        catch (System.Exception e)
        {
            System.Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Product> Update(Product payload)
    {
        var product = _repository.Update(payload);
        await _persistance.SaveChangeAsync();
        return product;
    }

    public async Task DeleteById(string id)
    {
        var product = await GetById(id);
        _repository.Delete(product);
        await _persistance.SaveChangeAsync();
    }

}