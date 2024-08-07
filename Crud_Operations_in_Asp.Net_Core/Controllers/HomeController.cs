using Crud_Operations_in_Asp.Net_Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Crud_Operations_in_Asp.Net_Core.Controllers
{
    public class HomeController : Controller
    {
		private readonly AssignmentDbContext context;
		public HomeController(AssignmentDbContext _context)
		{
			this.context = _context;
		}

		public IActionResult Index()
        {
			var prdList = context.TblProducts.ToList();

			return View(prdList);
        }

        public IActionResult Products()
        {
			var prdList = context.TblProducts.ToList();

			return View(prdList);
		}

		public IActionResult About()
		{
			return View();
		}

		public IActionResult Services()
		{
			var prdList = context.TblProducts.ToList();

			return View(prdList);
		}

		public IActionResult Blog()
		{
			return View();
		}

		public IActionResult Contact()
		{
			return View();
		}

		public IActionResult Cart()
		{
			return View();
		}

		public IActionResult Checkout()
		{
			return View();
		}

        public IActionResult Thankyou()
        {
            return View();
        }
    }
}
