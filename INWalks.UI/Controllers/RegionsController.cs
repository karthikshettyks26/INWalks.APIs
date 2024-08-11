using INWalks.UI.Models;
using INWalks.UI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace INWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDto> response = new List<RegionDto>();
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpResponseMessage = await client.GetAsync("https://localhost:7136/api/regions");
                httpResponseMessage.EnsureSuccessStatusCode();
                
                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());
            }
            catch (Exception ex)
            { 
                //Log the exception
            }
            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel addRegionViewModel)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpRequestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:7136/api/regions"),
                    Content = new StringContent(JsonSerializer.Serialize(addRegionViewModel), Encoding.UTF8, "application/json")
                };

                var httpResponseMessage = await client.SendAsync(httpRequestMessage);
                httpResponseMessage.EnsureSuccessStatusCode();

                var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

                if (response != null)
                {
                    return RedirectToAction("Index", "Regions");
                }
            }
            catch (Exception ex)
            {
                //Log
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var response = await client.GetFromJsonAsync<RegionDto>($"https://localhost:7136/api/regions/{id.ToString()}");
                if (response != null)
                {
                    return View(response);
                }
            }
            catch (Exception ex)
            {
                //Log
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit (RegionDto regionDto)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpRequstMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri($"https://localhost:7136/api/regions/{regionDto.Id.ToString()}"),
                    Content = new StringContent(JsonSerializer.Serialize(regionDto), Encoding.UTF8,"application/json"),
                };

                var httpResponseMessage = await client.SendAsync(httpRequstMessage);
                httpResponseMessage.EnsureSuccessStatusCode();

                var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();
                if (response != null)
                {
                    return RedirectToAction("Edit", "Regions");
                }

            }
            catch(Exception ex)
            {
                //Log
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDto regionDto)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7136/api/regions/{regionDto.Id.ToString()}");

                httpResponseMessage.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "Regions");
            }
            catch(Exception ex)
            {

            }
            return View("Edit");
        }


    }
}
