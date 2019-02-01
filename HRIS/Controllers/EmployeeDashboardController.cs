using HRIS.Models;
using System;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web.Mvc;

namespace HRIS.Controllers
{
   
    public class EmployeeDashboardController : Controller
    {
        //string connectionString = @"Data Source = DBASUBICIT08s; Initial Catalog = BIOMETRIC; Integrated Security=True;";

        //BIOMETRICEntities db = new BIOMETRICEntities();

        //public ActionResult Index(string empNum, DateTime? dateFrom = null, DateTime? dateTo = null)
        //{

        //    //return View(db.qryCheckInOuts.Where(x => x.Badgenumber == empNum && DateTime.Compare(x.CHECKTIME.Value.Date, dateFrom) == 0).OrderByDescending(x=>x.CHECKTIME).ToList());
        //    return View(db.qryCheckInOuts.Where(x => x.Badgenumber == empNum).OrderByDescending(x => x.checkdate).ToList());
        //    //return View(db.qryCheckInOuts.Where(x => x.Badgenumber == empNum).ToList();
        //   // && x.checkdate.Value >= dateFrom.Value && x.checkdate.Value <= dateTo.Value).OrderByDescending(x => x.checkdate.Value).ToList());
        //}

        BIOMETRICEntities1 db = new BIOMETRICEntities1();

        public ActionResult Index(string empNum, DateTime? dateFrom = null, DateTime? dateTo = null)
        {

            //return View(db.qryCheckInOuts.Where(x => x.Badgenumber == empNum && DateTime.Compare(x.CHECKTIME.Value.Date, dateFrom) == 0).OrderByDescending(x=>x.CHECKTIME).ToList());
            return View(db.qryCheckInOuts1.Where(x => x.Badgenumber == empNum).OrderByDescending(x => x.checkdate).ToList());
            //return View(db.qryCheckInOuts.Where(x => x.Badgenumber == empNum).ToList();
            // && x.checkdate.Value >= dateFrom.Value && x.checkdate.Value <= dateTo.Value).OrderByDescending(x => x.checkdate.Value).ToList());
        }
    }
    }