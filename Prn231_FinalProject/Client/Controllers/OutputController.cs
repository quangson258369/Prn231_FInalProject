using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private string ProductApiUrl = "";
        private string InputApiUrl = "";
        private string OutputApiUrl = "";
        private string CustomerAPIUrl = "";
        public OutputController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:7073/api/Product";
            InputApiUrl = "https://localhost:7073/api/Input";
            OutputApiUrl = "https://localhost:7073/api/Output";
            CustomerAPIUrl = "https://localhost:7073/api/Customer";
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            List<Models.Object> listProducts = JsonSerializer.Deserialize<List<Models.Object>>(strData, option);
            var selectListProduct = new List<SelectListItem>();
            foreach (var item in listProducts)
            {
                selectListProduct.Add(new SelectListItem { Text = "" + item.DisplayName, Value = "" + item.Id });
            }
            ViewBag.SelectListProduct = selectListProduct;

            HttpResponseMessage response2 = await client.GetAsync(CustomerAPIUrl);
            string strData2 = await response2.Content.ReadAsStringAsync();
            List<Models.Customer> listCustomers = JsonSerializer.Deserialize<List<Models.Customer>>(strData2, option);
            var selectListCustomer = new List<SelectListItem>();
            foreach (var item in listCustomers)
            {
                selectListCustomer.Add(new SelectListItem { Text = "" + item.DisplayName, Value = "" + item.Id });
            }
            ViewData["SelectListCustomer"] = selectListCustomer;


            HttpResponseMessage response3 = await client.GetAsync(OutputApiUrl);
            string strData3 = await response3.Content.ReadAsStringAsync();
            List<Models.OutputInfo> listOutputInfos = JsonSerializer.Deserialize<List<Models.OutputInfo>>(strData3, option);
            ViewData["MyList"] = listOutputInfos;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> addOutPut()
        {
            DateTime timeOutput = DateTime.Now;
            var id = GenerateRandom();
            HttpResponseMessage response = await client.GetAsync(OutputApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            List<Models.Output> listOutput = JsonSerializer.Deserialize<List<Models.Output>>(strData, option);
            foreach (var item in listOutput)
            {
                if (item.Id.Equals("" + id))
                {
                    id = GenerateRandom();
                }
            }
            Output o = new Output()
            {
                Id = "" + id,
                DateOutput = timeOutput,
            };

            string data = JsonSerializer.Serialize(o);
            await client.PostAsync(OutputApiUrl + "/CreateNewOutPut", new StringContent(data, Encoding.UTF8, "application/json"));
            string idObject = Request.Form["productId"];
            string quantity = Request.Form["Quantity"];
            string customerId = Request.Form["customerId"];

            HttpResponseMessage response2 = await client.GetAsync(InputApiUrl);
            string strData2 = await response2.Content.ReadAsStringAsync();
            var option2 = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            List<Models.InputInfo> listInfo = JsonSerializer.Deserialize<List<Models.InputInfo>>(strData2, option2);
            var count = 0;
            foreach (var item in listInfo)
            {
                if (item.IdObject.Equals(idObject))
                {
                    count += item.Count;
                }
            }
            if (Int32.Parse(quantity) > count)
            {
                return View();
            }

            OutputInfo output = new OutputInfo()
            {
                Id = "" + id,
                IdObject = idObject,
                Count = Int32.Parse(quantity),
                Status = "Success",
                IdOutput = "" + id,
                IdCustomer = Int32.Parse(customerId),
                IdInputSelct = "" + GenerateRandom(),
                IdCustomerNavigation = { },
                InputSelects = { },
                IdObjectNavigation = { },
                IdOutputNavigation = { }


            };
            string data2 = JsonSerializer.Serialize(output);
            await client.PostAsync(OutputApiUrl + "/CreateNewOutPutInfos", new StringContent(data2, Encoding.UTF8, "application/json"));


            return RedirectToAction(nameof(Index));


        }

        [HttpPost]
        public async Task<IActionResult> editOutPut(Output output)
        {
            if (ModelState.IsValid)
            {
                string data = JsonSerializer.Serialize(output);
                await client.PostAsync(OutApiUrl, new StringContent(data, Encoding.UTF8, "application/json"));
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public int GenerateRandom()
        {
            Random random = new Random();
            return random.Next(10000, 100000); // Generates a random number between 10000 and 99999

        }
    }
}
