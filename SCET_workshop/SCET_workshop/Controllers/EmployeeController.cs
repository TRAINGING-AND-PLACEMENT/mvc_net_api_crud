using Microsoft.AspNetCore.Mvc;
using SCET_workshop.Models;

namespace SCET_workshop.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult Create(Employee employee)
        {

        }
    }
}
