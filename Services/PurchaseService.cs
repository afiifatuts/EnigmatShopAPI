using EnigmaShopApi.Entities;
using EnigmaShopApi.Repositories;
using EnigmaShopApi.Dto;

namespace EnigmaShopApi.Services;

public class PurchaseService : IPurchaseService
{
    private readonly IRepository<Purchase> _repository;
    private readonly IPersistance _persistence;

    private readonly IProductService _productService;

    public PurchaseService(IRepository<Purchase> repository, IPersistance persistance, IProductService productService)
    {
        _repository = repository;
        _persistence = persistance;
        _productService = productService;
    }
    public async Task<TransactionResponse> CreateNewTransaction(Purchase payload)
    {
        await _persistence.BeginTransactionAsync();
        //supaya tidak stackoverflow kita buat dto
        try
        {
            payload.TransDate = DateTime.UtcNow;
            var purchase = await _repository.SaveAsync(payload);
            await _persistence.SaveChangeAsync();

            foreach (var purchaseDetail in purchase.PurchaseDetails)
            {
                var product = await _productService.GetById(purchaseDetail.ProductId.ToString());
                product.Stock -= purchaseDetail.Qty;
            }
            await _persistence.SaveChangeAsync();
            await _persistence.CommitTransactionAsync();

            var purchaseDetailResponses = new List<PurchaseDetailResponse>();

            foreach (var pd in purchase.PurchaseDetails)
            {
                purchaseDetailResponses.Add( new PurchaseDetailResponse {
                    ProductId = pd.ProductId.ToString(),
                    Qty = pd.Qty
                });
            }

            TransactionResponse response = new()
            {
                CustomerId = purchase.CustomerId.ToString(),
                TransDate = purchase.TransDate,
                PurchaseDetail = purchaseDetailResponses
            };

            return response;
        }
        catch (System.Exception e)
        {
            await _persistence.RollbackTransactionAsync();
            throw;
        }
    }

}