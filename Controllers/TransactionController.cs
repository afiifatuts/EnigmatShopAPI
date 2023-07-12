using EnigmaShopApi.Services;
using EnigmaShopApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EnigmaShopApi.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;

    public TransactionController(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewTransaction([FromBody] Purchase payload)
    {
        var transaction = await _purchaseService.CreateNewTransaction(payload);
        return Created("/api/transactions", transaction);
    }
}
