using HRIS.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace HRIS.Controllers
{
   
    public class EmployeeDashboardController : Controller
    {
        BIOMETRICEntities db = new BIOMETRICEntities();
        string connectionString = @"Data Source =TIM-PC; Initial Catalog = HRIS; Integrated Security=True;";

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

            ViewBag.emp = employeenumber;
            ViewBag.start = dtfrom;
            ViewBag.end = dtto;

            var item = db.qries
                .Where(x => x.Badgenumber == employeenumber
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

            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            int lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1).Day;

            double lateCount = Convert.ToInt32(lateQuery);
            double daysCount = Convert.ToInt32(lastDayOfMonth);

            double latePercentage = ((lateCount / daysCount) * 100);
            ViewBag.latePercentage = latePercentage;

            return View("Index",item);
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

        public ActionResult EmployeesProfile()
        {
            string employeenumber = Session["employeenumber"].ToString();
            Masterlist masterlist = new Masterlist();
            DataTable dtblEmployeeInfo = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "SELECT EmployeeNumber,FirstName,LastName,PersonalEmail,Department,JobTitle,DateHired,ContactNumber,Street_Address1,Street_Address2,City," +
                    "Province,ZipCode,Birthday,Gender,MaritalStatus from Masterlist Where EmployeeNumber = @ID";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@ID", employeenumber);
                sqlDa.Fill(dtblEmployeeInfo);
            }
            if (dtblEmployeeInfo.Rows.Count == 1)
            {
                ViewBag.empID = employeenumber;
                ViewBag.firstName = dtblEmployeeInfo.Rows[0][1].ToString();
                ViewBag.lastName = dtblEmployeeInfo.Rows[0][2].ToString();
                ViewBag.eMail = dtblEmployeeInfo.Rows[0][3].ToString();
                ViewBag.dept = dtblEmployeeInfo.Rows[0][4].ToString();
                ViewBag.jobTitle = dtblEmployeeInfo.Rows[0][5].ToString();
                ViewBag.dateHired = dtblEmployeeInfo.Rows[0][6].ToString();
                ViewBag.contactNum = dtblEmployeeInfo.Rows[0][7].ToString();
                ViewBag.street1 = dtblEmployeeInfo.Rows[0][8].ToString();
                ViewBag.street2 = dtblEmployeeInfo.Rows[0][9].ToString();
                ViewBag.city = dtblEmployeeInfo.Rows[0][10].ToString();
                ViewBag.province = dtblEmployeeInfo.Rows[0][11].ToString();
                ViewBag.zipcode = dtblEmployeeInfo.Rows[0][12].ToString();
                ViewBag.bday = dtblEmployeeInfo.Rows[0][13].ToString();
                ViewBag.gender = dtblEmployeeInfo.Rows[0][14].ToString();
                ViewBag.maritalStatus = dtblEmployeeInfo.Rows[0][15].ToString();

                return View(masterlist);
            }
            return View();
        }
        public ActionResult LeaveForm()
        {
            UserDropdownEntities userDropdownEntities = new UserDropdownEntities();
            UserModelEntities userModel = new UserModelEntities();
            var getuserlist = userDropdownEntities.Dropdowns.ToList();
            var getuser = userModel.Users.ToList();
            SelectList LeaveRequest = new SelectList(getuserlist.Where(o => o.DropdownType == "LeaveRequest"), "DropdownName", "DropdownName");
            SelectList Approver = new SelectList(getuser.Where(o => o.Userlevel == "Admin"), "Userid", "Email");
            ViewBag.LeaveRequest = LeaveRequest;
            ViewBag.Approver = Approver;
            return View();
        }
        [HttpPost]
        public ActionResult LeaveForm(LeaveForm leaveForm)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = " select * from LeaveForm where StartDate = @StartDate AND EndDate = @EndDate";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@StartDate", leaveForm.StartDate);
                sqlCmd.Parameters.AddWithValue("@EndDate", leaveForm.EndDate);
                SqlDataReader sdr = sqlCmd.ExecuteReader();
                if (sdr.Read())
                {
                    TempData["error"] = "You've already applied leave on this date!";
                }
                else
                {
                    sdr.Close();
                    string querys = "INSERT INTO LeaveForm(EmployeeNumber,TypeOfRequest,Description,Approver,StartDate,EndDate) VALUES(@EmployeeNumber,@TypeOfRequest,@Description,@Approver,@StartDate,@EndDate)";
                    SqlCommand sqlCmds = new SqlCommand(querys, sqlCon);
                    sqlCmds.Parameters.AddWithValue("@EmployeeNumber", Session["employeenumber"]);
                    sqlCmds.Parameters.AddWithValue("@TypeOfRequest", leaveForm.TypeOfRequest);
                    sqlCmds.Parameters.AddWithValue("@Description", leaveForm.Description);
                    sqlCmds.Parameters.AddWithValue("@Approver", leaveForm.Approver);
                    sqlCmds.Parameters.AddWithValue("@StartDate", leaveForm.StartDate);
                    sqlCmds.Parameters.AddWithValue("@EndDate", leaveForm.EndDate);
                    SqlDataReader sdrs = sqlCmds.ExecuteReader();
                    TempData["success"] = "New " + leaveForm.TypeOfRequest + " Request";
                }

            }
            return RedirectToAction("LeaveForm");
        }
    }
}