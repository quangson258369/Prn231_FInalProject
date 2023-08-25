using Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";
        private string InputApiUrl = "";
        private string OutputApiUrl = "";
        public HomeController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:7073/api/Product";
            InputApiUrl = "https://localhost:7073/api/Input";
            OutputApiUrl = "https://localhost:7073/api/Output";
        }

        public async Task<IActionResult> Index()
        {

            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            List<Models.Object> listProducts = JsonSerializer.Deserialize<List<Models.Object>>(strData, option);

            HttpResponseMessage response2 = await client.GetAsync(InputApiUrl);
            string strData2 = await response2.Content.ReadAsStringAsync();
            List<Models.InputInfo> listInfo = JsonSerializer.Deserialize<List<Models.InputInfo>>(strData2, option);

            HttpResponseMessage response3 = await client.GetAsync(OutputApiUrl);
            string strData3 = await response3.Content.ReadAsStringAsync();
            List<Models.OutputInfo> listOutput = JsonSerializer.Deserialize<List<Models.OutputInfo>>(strData3, option);

            List<InStock> inStocks = new List<InStock>();
            foreach (var item in listProducts)
            {
                InStock inStock = new InStock()
                {
                    Id = item.Id,
                    Name = item.DisplayName,
                    Quantity = quantityInStock(item.Id, listInfo)
                };
                inStocks.Add(inStock);
            }

            HttpContext.Session.SetString("product", System.Text.Json.JsonSerializer.Serialize(inStocks, option));
            HttpContext.Session.SetString("info", System.Text.Json.JsonSerializer.Serialize(listInfo, option));
            HttpContext.Session.SetString("output", System.Text.Json.JsonSerializer.Serialize(listOutput, option));

            return View();
        }

        public int quantityInStock(string id, List<Models.InputInfo> listInfo)
        {
            int count = 0;
            foreach (var item in listInfo)
            {
                if (item.IdObject.Equals(id))
                {
                    count += item.Count;
                }
            }
            return count;
        }
    }
}
