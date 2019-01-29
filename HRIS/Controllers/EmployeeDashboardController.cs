using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRIS.Controllers
{
    public class EmployeeDashboardController : Controller
    {
        // GET: EmployeeDashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}