using Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Client.Controllers
{
    public class OutputController : Controller
    {
        private readonly HttpClient client = null;
        private string OutApiUrl = "";
        public static List<Output> _output;
        public static List<Models.Object> _products;
        public static List<Models.Customer> _customers;

        public IActionResult Index()
        {
            return View();
        }
        public OutputController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            OutApiUrl = "http://localhost:5292/api/AuthorsControllerAPI";
        }
        [HttpPost]
        public async Task<IActionResult> addOutPut(Output output)
        {
            if (ModelState.IsValid)
            {
                string data = JsonSerializer.Serialize(output);
                await client.PostAsync(OutApiUrl, new StringContent(data, Encoding.UTF8, "application/json"));
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
