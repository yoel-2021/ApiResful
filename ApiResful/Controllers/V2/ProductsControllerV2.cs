using ApiResful.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiResful.Controllers.V2
{
    [ApiVersion("2.0")]
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
        [MapToApiVersion("2.0")]
        [HttpGet(Name = "GetProductsData")]
        public async Task<IActionResult> GetProductsDataAsync()
        {

            var response = await _httpClient.GetStreamAsync(ApiTestUrl);
            ProductsDataV2[]? productsData = await System.Text.Json.JsonSerializer.DeserializeAsync<ProductsDataV2[]>(response);

            var products = productsData;
            return Ok(products);
        }

    }

}
