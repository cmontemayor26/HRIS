using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRIS.Models;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace HRIS.Controllers
{
    public class ExamController : Controller
    {
        string connectionString = @"Data Source=192.168.102.18;Initial Catalog=HRIS;Persist Security Info=True;User ID=panoramic;Password=GoLegal100;";

        // GET: Exam
        public ActionResult Essay()
        {
            return View();
        }
        public ActionResult IQtest1()
        {
            return View();
        }
        [HttpPost]
        public ActionResult IQtest1(IQtest iqtest)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "INSERT INTO IQtest(ApplicantID,IQtest1,IQtest2,IQtest3,IQtest4,IQtest5,IQtest6,IQtest7,IQtest8,IQtest9,IQtest10,IQtest11,IQtest12,IQtest13,IQtest14,IQtest15,IQtest16,IQtest17,IQtest18,IQtest19,IQtest20,IQtest21,IQtest22,IQtest23,IQtest24,IQtest25,IQtest26,IQtest27,IQtest28,IQtest29,IQtest30)" +
                        "VALUES(@ApplicantID,@IQtest1,@IQtest2,@IQtest3,@IQtest4,@IQtest5,@IQtest6,@IQtest7,@IQtest8,@IQtest9,@IQtest10,@IQtest11,@IQtest12,@IQtest13,@IQtest14,@IQtest15,@IQtest16,@IQtest17,@IQtest18,@IQtest19,@IQtest20,@IQtest21,@IQtest22,@IQtest23,@IQtest24,@IQtest25,@IQtest26,@IQtest27,@IQtest28,@IQtest29,@IQtest30)";
                SqlCommand sqlCmds = new SqlCommand(query, sqlCon);
                sqlCmds.Parameters.AddWithValue("@ApplicantID", Session["MasterlistID"].ToString());
                sqlCmds.Parameters.AddWithValue("@IQtest1", iqtest.IQtest1);
                sqlCmds.Parameters.AddWithValue("@IQtest2", iqtest.IQtest2);
                sqlCmds.Parameters.AddWithValue("@IQtest3", iqtest.IQtest3);
                sqlCmds.Parameters.AddWithValue("@IQtest4", iqtest.IQtest4);
                sqlCmds.Parameters.AddWithValue("@IQtest5", iqtest.IQtest5);
                sqlCmds.Parameters.AddWithValue("@IQtest6", iqtest.IQtest6);
                sqlCmds.Parameters.AddWithValue("@IQtest7", iqtest.IQtest7);
                sqlCmds.Parameters.AddWithValue("@IQtest8", iqtest.IQtest8);
                sqlCmds.Parameters.AddWithValue("@IQtest9", iqtest.IQtest9);
                sqlCmds.Parameters.AddWithValue("@IQtest10", iqtest.IQtest10);
                sqlCmds.Parameters.AddWithValue("@IQtest11", iqtest.IQtest11);
                sqlCmds.Parameters.AddWithValue("@IQtest12", iqtest.IQtest12);
                sqlCmds.Parameters.AddWithValue("@IQtest13", iqtest.IQtest13);
                sqlCmds.Parameters.AddWithValue("@IQtest14", iqtest.IQtest14);
                sqlCmds.Parameters.AddWithValue("@IQtest15", iqtest.IQtest15);
                sqlCmds.Parameters.AddWithValue("@IQtest16", iqtest.IQtest16);
                sqlCmds.Parameters.AddWithValue("@IQtest17", iqtest.IQtest17);
                sqlCmds.Parameters.AddWithValue("@IQtest18", iqtest.IQtest18);
                sqlCmds.Parameters.AddWithValue("@IQtest19", iqtest.IQtest19);
                sqlCmds.Parameters.AddWithValue("@IQtest20", iqtest.IQtest20);
                sqlCmds.Parameters.AddWithValue("@IQtest21", iqtest.IQtest21);
                sqlCmds.Parameters.AddWithValue("@IQtest22", iqtest.IQtest22);
                sqlCmds.Parameters.AddWithValue("@IQtest23", iqtest.IQtest23);
                sqlCmds.Parameters.AddWithValue("@IQtest24", iqtest.IQtest24);
                sqlCmds.Parameters.AddWithValue("@IQtest25", iqtest.IQtest25);
                sqlCmds.Parameters.AddWithValue("@IQtest26", iqtest.IQtest26);
                sqlCmds.Parameters.AddWithValue("@IQtest27", iqtest.IQtest27);
                sqlCmds.Parameters.AddWithValue("@IQtest28", iqtest.IQtest28);
                sqlCmds.Parameters.AddWithValue("@IQtest29", iqtest.IQtest29);
                sqlCmds.Parameters.AddWithValue("@IQtest30", iqtest.IQtest30);
                sqlCmds.ExecuteNonQuery();
            }
            if (Session["JobTitle"].ToString() == "Accountant") {
                return RedirectToAction("Accountingtest", "Accountant");
            }
            return RedirectToAction("Essay");
        }
        public ActionResult IQtest()
        {
                
            return View();
        }
        public ActionResult Accountingtest()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Masterlist lg)
        {
            string mainconn = @"Data Source=192.168.102.18;Initial Catalog=HRIS;Persist Security Info=True;User ID=panoramic;Password=GoLegal100;";
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery = " select Applicant_ExamNo, FirstName, LastName, JobTitle, MasterlistID from Masterlist where Applicant_ExamNo = @Applicant_ExamNo";
            sqlconn.Open();
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlcomm.Parameters.AddWithValue("@Applicant_ExamNo", lg.Applicant_ExamNo);

            SqlDataAdapter SqlDa = new SqlDataAdapter(sqlcomm);
            DataTable Logins = new DataTable();
            SqlDa.Fill(Logins);

            SqlDataReader sdr = sqlcomm.ExecuteReader();
            if (sdr.Read())
            {
                for (int i = 0; i < Logins.Rows.Count; i++)
                {
                    Session["Applicant_ExamNo"] = Logins.Rows[i][0];
                    Session["FirstName"] = Logins.Rows[i][1];
                    Session["LastName"] = Logins.Rows[i][2];
                    Session["JobTitle"] = Logins.Rows[i][3];
                    Session["MasterlistID"] = Logins.Rows[i][4];
                }
                    return RedirectToAction("IQtest", "Exam");
            }
            else
            {
                TempData["Message"] = "Credentials are Invalid!";
            }
            sqlconn.Close();
            return View();
        }
    }
}