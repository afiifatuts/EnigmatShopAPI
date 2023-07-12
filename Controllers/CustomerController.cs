using Microsoft.AspNetCore.Mvc;
using EnigmaShopApi.Repositories;
using EnigmaShopApi.Entities;

namespace EnigmaShopApi.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly IRepository<Customer> _customerRepository;
    private readonly IPersistance _persistence;

    public CustomerController(IRepository<Customer> customerRepository, IPersistance persistance)
    {
        _customerRepository = customerRepository;
        _persistence = persistance;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewCustomer([FromBody] Customer payload)
    {
        var customer = await _customerRepository.SaveAsync(payload);
        await _persistence.SaveChangeAsync();
        return Created("api/customers", customer);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCustomer([FromBody] Customer payload)
    {
        var customer = _customerRepository.Update(payload);
        await _persistence.SaveChangeAsync();
        return Ok(customer);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(string id)
    {
        try
        {
            var customer = await _customerRepository.FindByIdAsync(Guid.Parse(id));
            if (customer is null) return NotFound("customer not found");
            _customerRepository.Delete(customer);
            await _persistence.SaveChangeAsync();
            return Ok();
        }
        catch (System.Exception e)
        {
            return new StatusCodeResult(500);
        }
    }
}