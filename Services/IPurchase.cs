using EnigmaShopApi.Dto;
using EnigmaShopApi.Entities;

namespace EnigmaShopApi.Services;

public interface IPurchaseService
{
    Task<TransactionResponse> CreateNewTransaction(Purchase purchase);
}