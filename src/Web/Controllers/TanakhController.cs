using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class TanakhController : Controller
    {
        public IActionResult Chapter(string name, int number)
        {
            BookController ctrl = new BereshitController();
            if (!String.IsNullOrEmpty(name))
            {

            }
            if (number == 0)
            {
                number = 1;
            }
            var model = ctrl.GetChapterFromData(number);
            return View(model);
        }
    }
}
