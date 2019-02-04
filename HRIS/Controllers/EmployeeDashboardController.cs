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

        BIOMETRICEntities db = new BIOMETRICEntities();

        public ActionResult Index(string empNum, DateTime? dateFrom, DateTime? dateTo)
        {
            ViewBag.emp = empNum;
            ViewBag.start = dateFrom;
            ViewBag.end = dateTo;

            var dtfrom = Convert.ToDateTime(dateFrom).Date;
            var dtto = Convert.ToDateTime(dateTo).Date;

            var item = db.qries
                .Where(x => x.Badgenumber == empNum
                && x.CHECKTIME >= dateFrom
                && x.CHECKTIME.Year <= dtto.Year && x.CHECKTIME.Day <= dtto.Day && x.CHECKTIME.Month <= dtto.Month
                )
                //&& x.CHECKTIME <= dateTo)
                .OrderBy(x => x.CHECKTIME)
                .ToList();


            return View(item);


            //if (empNum != null)
            //{

            //}
            //return View(db.qryCheckInOuts.Where(x => x.Badgenumber == empNum).OrderByDescending(x => x.checkdate).ToList());
            //return View(db.qryCheckInOuts.Where(x => x.Badgenumber == empNum && DateTime.Compare(x.CHECKTIME.Value.Date, dateFrom) == 0).OrderByDescending(x=>x.CHECKTIME).ToList());

            //return View(db.qryCheckInOuts.Where(x => x.Badgenumber == empNum).ToList());
            //return View(db.qries.Where(x => x.Badgenumber.Contains(empNum)).ToList());
            // && x.checkdate.Value >= dateFrom.Value && x.checkdate.Value <= dateTo.Value).OrderByDescending(x => x.checkdate.Value).ToList());
        }
    }
    }