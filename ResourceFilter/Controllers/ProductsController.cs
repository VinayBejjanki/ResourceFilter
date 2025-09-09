using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResourceFilter.ResourceFilter;

namespace ResourceFilter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        [CacheFilter]
        public async Task<IActionResult> Getproduct()
        {
            var products = new List<string>
            {
                "Laptop",
                "Modile",
                "Cell phones",
                "Clothes"
            };

            return Ok(products);
        }
    }
}
