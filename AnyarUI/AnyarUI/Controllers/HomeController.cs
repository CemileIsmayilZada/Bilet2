using Anyar.Business.ViewModels;
using Anyor.DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace AnyarUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            HomeViewModel homeView = new HomeViewModel()
            {
                TeamItems =_context.TeamItems
            };
            return View(homeView);
        }
    }
}
