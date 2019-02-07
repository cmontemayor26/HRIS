using HRIS.Models;
using Microsoft.Reporting.WebForms;
using System;
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

        public ActionResult Index(string empNum, DateTime? dateFrom, DateTime? dateTo)
        {
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

            return View(item);
        }

        //public ActionResult DownloadPDF()
        //{
        //    var model = new GeneratePDFModel();
        //    return new Rotativa.ViewAsPdf("GeneratePDF", model) { FileName = "AttendanceViewAsPdf.pdf" };
        //}

        //public ActionResult DownloadActionAsPDF()
        //{
        //    var model = new GeneratePDFModel();
        //    return new Rotativa.ActionAsPdf("GeneratePDF", model) { FileName = "AttendanceActionAsPdf" };
        //}



        public FileResult Reports(string ReportType, string empNum, DateTime? dateFrom, DateTime? dateTo)
        {
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