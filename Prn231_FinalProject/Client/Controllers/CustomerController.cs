using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Client.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient client = null;
        private string GetAllCustomersApiUrl = "";
        private string CreateCustomerApiUrl = "";
        private string GetCustomerByIdApiUrl = "";
        private string DeleteCustomerApiUrl = "";

        public CustomerController() {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            GetAllCustomersApiUrl = "http://localhost:7073/api/GetAllCustomers";
            CreateCustomerApiUrl = "http://localhost:7073/api/CreateCustomer";
            GetCustomerByIdApiUrl = "http://localhost:7073/api/GetCustomerById";
            DeleteCustomerApiUrl = "http://localhost:7073/api/DeleteCustomer/";

        }
        public async Task<IActionResult> Index()
        {
            List<Customer> customers = new List<Customer>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:7073/api/GetAllCustomers"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        customers = JsonConvert.DeserializeObject<List<Customer>>(apiResponse);
                    }
                    else
                    {
                        // Handle the error response
                    }
                }
            }

            return View(customers);
        }
    }

}
