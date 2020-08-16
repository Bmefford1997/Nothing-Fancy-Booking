using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Nothing_Fancy.Controllers
{
    public class RoomsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Room101()
        {
            return View();
        }

        public IActionResult Room102()
        {
            return View();
        }

        public IActionResult Room103()
        {
            return View();
        }

        public IActionResult Rooms()
        {
            return View();
        }
    }
}
