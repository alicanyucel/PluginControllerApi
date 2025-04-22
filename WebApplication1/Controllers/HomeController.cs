using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

public class HomeController : Controller
{
    private readonly HttpClient _httpClient;

    public HomeController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            // API'ye istek g�nder
            var response = await _httpClient.GetAsync("https://localhost:7057/api/Hello/Get");

            if (response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                ViewBag.Message = message;
            }
            else
            {
                // API hata verirse
                ViewBag.Message = "API �a�r�s� ba�ar�s�z oldu! " + response.StatusCode;
            }
        }
        catch (Exception ex)
        {
            // Hata durumunda
            ViewBag.Message = "API �a�r�s� s�ras�nda bir hata olu�tu: " + ex.Message;
        }

        return View();
    }
}
