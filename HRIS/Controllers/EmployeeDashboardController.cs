using HRIS.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.SqlServer;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace HRIS.Controllers
{
   
    public class EmployeeDashboardController : Controller
    {
        BIOMETRICEntities db = new BIOMETRICEntities();
       

        public ActionResult Index()
        {
            string employeenumber = Session["employeenumber"].ToString();
            ViewBag.emp = employeenumber;

            var item = db.qries
                .Where(x => x.Badgenumber == employeenumber
                && x.CHECKTIME.Year == DateTime.Now.Year && x.CHECKTIME.Month == 1//DateTime.Now.Month
                )
                //&& x.CHECKTIME <= dateTo)
                .OrderBy(x => x.CHECKTIME)
                .ToList();
            
            var lateQuery = db.qries
                .Where(x => x.Badgenumber == ((string)employeenumber)
                && x.CHECKTYPE == "I"
                && x.CHECKTIME.Year == DateTime.Now.Year
                && x.CHECKTIME.Month == 1
                && x.CHECKTIME.Hour == 7
                && x.CHECKTIME.Minute >= 1
                )
                .OrderBy(x => x.CHECKTIME)
                .Count();
            ViewBag.lateCount = lateQuery;

            

            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            int lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1).Day;

            double lateCount = Convert.ToInt32(lateQuery);
            double daysCount = Convert.ToInt32(lastDayOfMonth);

            double latePercentage = ((lateCount / daysCount) * 100);
            ViewBag.latePercentage = latePercentage;

          
            return View(item);
        }

        public ActionResult FilterAttendance(string empNum, DateTime? dateFrom, DateTime? dateTo)
        {
            string employeenumber = Session["employeenumber"].ToString();
            var dtfrom = Convert.ToDateTime(dateFrom).Date;
            var dtto = Convert.ToDateTime(dateTo).Date;

            ViewBag.emp = empNum;
            ViewBag.start = dtfrom;
            ViewBag.end = dtto;

            var item = db.qries
                .Where(x => x.Badgenumber == empNum
                && x.CHECKTIME >= dateFrom
                && x.CHECKTIME.Year <= dtto.Year && x.CHECKTIME.Day <= dtto.Day && x.CHECKTIME.Month <= dtto.Month
                )
                //&& x.CHECKTIME <= dateTo)
                .OrderBy(x => x.CHECKTIME)
                .ToList();

            var lateQuery = db.qries
                .Where(x => x.Badgenumber == ((string)employeenumber)
                && x.CHECKTYPE == "I"
                && x.CHECKTIME.Year == DateTime.Now.Year
                && x.CHECKTIME.Month == 1
                && x.CHECKTIME.Hour == 7
                && x.CHECKTIME.Minute >= 1
                )
                .OrderBy(x => x.CHECKTIME)
                .Count();

            ViewBag.lateCount = lateQuery;

            return View(item);
        }

        public FileResult Reports(string ReportType)
        {
            string employeenumber = Session["employeenumber"].ToString();

            var item = db.qries
               .Where(x => x.Badgenumber == employeenumber
               && x.CHECKTIME.Year <= DateTime.Now.Year && x.CHECKTIME.Month <= 1//DateTime.Now.Month
               )
               //&& x.CHECKTIME <= dateTo)
               .OrderBy(x => x.CHECKTIME)
               .ToList();

            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/attendance.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "attendanceDataSet";
            reportDataSource.Value = item;
            localReport.DataSources.Add(reportDataSource);
            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension;
            if (reportType == "PDF")
            {
                fileNameExtension = "pdf";
            }
            else if (reportType == "Excel")
            {
                fileNameExtension = "xlsx";
            }

            string[] streams;
            Warning[] warnings;
            byte[] renderedByte;
            renderedByte = localReport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment;filename= attendance." + fileNameExtension);
            return File(renderedByte, fileNameExtension);
        }
    }
}