using Microsoft.AspNetCore.Mvc;

namespace LabWork28.Controllers;

public class Task1Controller : Controller
{
    [HttpGet]
    [Route("[controller]")]
    public async Task<double> Sum(double a, double b)
    {
        return await Task.Run(() => a + b);
    }
}