using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        public ActionResult Index()
        {
            Tesc tesc = new Tesc();
            StringBuilder strb = new StringBuilder();
            HomeViewModel homeviewmodel = new HomeViewModel();
            homeviewmodel.user = (from d in tesc.Users select d);

            return View(homeviewmodel);
        }
    }
}