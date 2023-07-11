using Microsoft.AspNetCore.Mvc;

namespace EnigmaShopApi.Controllers;

[ApiController]
[Route("api/hello")]
public class HelloController
{
    [HttpGet]
    public string SayHello()
    {
        return "Hello world";
    }

    [HttpGet("get-object")]
    public object GetObject()
    {
        return new
        {
            Id = Guid.NewGuid(),
            Name = "Budi",
            Date = DateTime.UtcNow,
        };
    }

    [HttpGet("get-array")]
    public List<object> GetArrayObj()
    {
        return new List<object>
        {
            new
            {
                Id = Guid.NewGuid(),
                Name = "Budi",
                Date = DateTime.UtcNow,
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "Rio",
                Date = DateTime.UtcNow,
            }
        };
    }
}