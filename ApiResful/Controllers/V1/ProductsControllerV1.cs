using ApiResful.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json; 

namespace ApiversionControl.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}[controller]")]
    [ApiController]
    public class ProductsControllerV2 : ControllerBase
    {
        private const string ApiTestUrl = "https://fakestoreapi.com/products";

        private readonly HttpClient _httpClient;

        public ProductsControllerV2(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [MapToApiVersion("1.0")]
        [HttpGet(Name = "GetProductsData")]
        public async Task<IActionResult> GetProductsDataAsync()
        {
            
            var response = await _httpClient.GetStreamAsync(ApiTestUrl);
            ProductsDataV1[]? productsData = await System.Text.Json.JsonSerializer.DeserializeAsync<ProductsDataV1[]>(response);
            
            var products = productsData;
            return Ok(products);
        }

       
    }
}
