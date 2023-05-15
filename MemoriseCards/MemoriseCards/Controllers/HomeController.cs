using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MemoriseCards.Models;
using MemoriseCards.Data;
using Microsoft.Extensions.Configuration;

namespace MemoriseCards.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private IConfiguration _configuration;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

