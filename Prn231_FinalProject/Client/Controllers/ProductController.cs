using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Client.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";
        private string InputApiUrl = "";
        private string OutputApiUrl = "";
        public ProductController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:7073/api/Product";
        }
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            List<Models.Object> listProducts = JsonSerializer.Deserialize<List<Models.Object>>(strData, option);
            HttpContext.Session.SetString("product", System.Text.Json.JsonSerializer.Serialize(listProducts, option));
            return View();
        }
    }
}
