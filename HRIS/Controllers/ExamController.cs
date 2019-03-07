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
        [HttpPost]
        public ActionResult Essay(Essay essay)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "INSERT INTO Essay(ApplicantID,Essay1,Essay2,Essay3,Essay4,Essay5,Essay6,Essay7,Essay8,Essay9,Essay10,Essay11,Essay12,Essay13)" +
                        "VALUES(@ApplicantID,@Essay1,@Essay2,@Essay3,@Essay4,@Essay5,@Essay6,@Essay7,@Essay8,@Essay9,@Essay10,@Essay11,@Essay12,@Essay13)";
                SqlCommand sqlCmds = new SqlCommand(query, sqlCon);
                sqlCmds.Parameters.AddWithValue("@ApplicantID", Session["MasterlistID"].ToString());
                sqlCmds.Parameters.AddWithValue("@Essay1", essay.Essay1);
                sqlCmds.Parameters.AddWithValue("@Essay2", essay.Essay2);
                sqlCmds.Parameters.AddWithValue("@Essay3", essay.Essay3);
                sqlCmds.Parameters.AddWithValue("@Essay4", essay.Essay4);
                sqlCmds.Parameters.AddWithValue("@Essay5", essay.Essay5);
                sqlCmds.Parameters.AddWithValue("@Essay6", essay.Essay6);
                sqlCmds.Parameters.AddWithValue("@Essay7", essay.Essay7);
                sqlCmds.Parameters.AddWithValue("@Essay8", essay.Essay8);
                sqlCmds.Parameters.AddWithValue("@Essay9", essay.Essay9);
                sqlCmds.Parameters.AddWithValue("@Essay10", essay.Essay10);
                sqlCmds.Parameters.AddWithValue("@Essay11", essay.Essay11);
                sqlCmds.Parameters.AddWithValue("@Essay12", essay.Essay12);
                sqlCmds.Parameters.AddWithValue("@Essay13", essay.Essay13);
                sqlCmds.ExecuteNonQuery();
                Session["timer"] = null;
            }
            return RedirectToAction("End");
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
                sqlCmds.Parameters.AddWithValue("@ApplicantID", Convert.ToInt32(Session["MasterlistID"].ToString()));
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
                Session["timer"] = null;
            }
            if (Session["JobTitle"].ToString() == "Associate Accountant" || Session["JobTitle"].ToString() == "Senior Accountant") {
                return RedirectToAction("Accountingtest", "Accountant");
            }
            return RedirectToAction("Essay");
        }
        public ActionResult IQtest()
        {
            Random rand = new Random();
            int random = rand.Next(1, 6);
            Session["random"] = random;

            if (Session["Applicant_ExamNo"] == null) {
                return RedirectToAction("Login");
            }
            return View();
        }
        public ActionResult End()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Accountingtest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Accountingtest(Accountingtest accountingtest)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "INSERT INTO Accountingtest(ApplicantID,Accountingtest1,Accountingtest2,[Accountingtest3-1],[Accountingtest3-2],[Accountingtest3-3],Accountingtest4,Accountingtest5,Accountingtest6,Accountingtest7,Accountingtest8,Accountingtest9,Accountingtest10,Solution1,[Solution3-1],[Solution3-2],[Solution3-3],Solution4,Solution7,Solution8,Solution10," +
                    "Accounttitle1,Accounttitle2,Accounttitle3,Accounttitle4,Accounttitle5,Accounttitle6,Accounttitle7,Accounttitle8,Accounttitle9,Accounttitle10,Accounttitle11,Accounttitle12,Accounttitle13,Debit1,Debit2,Debit3,Debit4,Debit5,Debit6,Debit7,Debit8,Debit9,Debit10,Debit11,Debit12,Debit13,Credit1,Credit2,Credit3,Credit4,Credit5,Credit6,Credit7,Credit8,Credit9,Credit10,Credit11,Credit12,Credit13,Accountingtestshort2)" +
                        "VALUES(@ApplicantID,@Accountingtest1,@Accountingtest2,@Accountingtest31,@Accountingtest32,@Accountingtest33,@Accountingtest4,@Accountingtest5,@Accountingtest6,@Accountingtest7,@Accountingtest8,@Accountingtest9,@Accountingtest10,@Solution1,@Solution31,@Solution32,@Solution33,@Solution4,@Solution7,@Solution8,@Solution10" +
                        ",@Accounttitle1,@Accounttitle2,@Accounttitle3,@Accounttitle4,@Accounttitle5,@Accounttitle6,@Accounttitle7,@Accounttitle8,@Accounttitle9,@Accounttitle10,@Accounttitle11,@Accounttitle12,@Accounttitle13,@Debit1,@Debit2,@Debit3,@Debit4,@Debit5,@Debit6,@Debit7,@Debit8,@Debit9,@Debit10,@Debit11,@Debit12,@Debit13,@Credit1,@Credit2,@Credit3,@Credit4,@Credit5,@Credit6,@Credit7,@Credit8,@Credit9,@Credit10,@Credit11,@Credit12,@Credit13,@Accountingtestshort2)";
                SqlCommand sqlCmds = new SqlCommand(query, sqlCon);
                sqlCmds.Parameters.AddWithValue("@ApplicantID", Session["MasterlistID"].ToString());
                sqlCmds.Parameters.AddWithValue("@Accountingtest1", accountingtest.Accountingtest1);
                sqlCmds.Parameters.AddWithValue("@Accountingtest2", accountingtest.Accountingtest2);
                sqlCmds.Parameters.AddWithValue("@Accountingtest31", accountingtest.Accountingtest31);
                sqlCmds.Parameters.AddWithValue("@Accountingtest32", accountingtest.Accountingtest32);
                sqlCmds.Parameters.AddWithValue("@Accountingtest33", accountingtest.Accountingtest33);
                sqlCmds.Parameters.AddWithValue("@Accountingtest4", accountingtest.Accountingtest4);
                sqlCmds.Parameters.AddWithValue("@Accountingtest5", accountingtest.Accountingtest5);
                sqlCmds.Parameters.AddWithValue("@Accountingtest6", accountingtest.Accountingtest6);
                sqlCmds.Parameters.AddWithValue("@Accountingtest7", accountingtest.Accountingtest7);
                sqlCmds.Parameters.AddWithValue("@Accountingtest8", accountingtest.Accountingtest8);
                sqlCmds.Parameters.AddWithValue("@Accountingtest9", accountingtest.Accountingtest9);
                sqlCmds.Parameters.AddWithValue("@Accountingtest10", accountingtest.Accountingtest10);
                sqlCmds.Parameters.AddWithValue("@Solution1", accountingtest.Solution1);
                sqlCmds.Parameters.AddWithValue("@Solution31", accountingtest.Solution31);
                sqlCmds.Parameters.AddWithValue("@Solution32", accountingtest.Solution32);
                sqlCmds.Parameters.AddWithValue("@Solution33", accountingtest.Solution33);
                sqlCmds.Parameters.AddWithValue("@Solution4", accountingtest.Solution4);
                sqlCmds.Parameters.AddWithValue("@Solution7", accountingtest.Solution7);
                sqlCmds.Parameters.AddWithValue("@Solution8", accountingtest.Solution8);
                sqlCmds.Parameters.AddWithValue("@Solution10", accountingtest.Solution10);
                sqlCmds.Parameters.AddWithValue("@Accounttitle1", accountingtest.Accounttitle1);
                sqlCmds.Parameters.AddWithValue("@Accounttitle2", accountingtest.Accounttitle2);
                sqlCmds.Parameters.AddWithValue("@Accounttitle3", accountingtest.Accounttitle3);
                sqlCmds.Parameters.AddWithValue("@Accounttitle4", accountingtest.Accounttitle4);
                sqlCmds.Parameters.AddWithValue("@Accounttitle5", accountingtest.Accounttitle5);
                sqlCmds.Parameters.AddWithValue("@Accounttitle6", accountingtest.Accounttitle6);
                sqlCmds.Parameters.AddWithValue("@Accounttitle7", accountingtest.Accounttitle7);
                sqlCmds.Parameters.AddWithValue("@Accounttitle8", accountingtest.Accounttitle8);
                sqlCmds.Parameters.AddWithValue("@Accounttitle9", accountingtest.Accounttitle9);
                sqlCmds.Parameters.AddWithValue("@Accounttitle10", accountingtest.Accounttitle10);
                sqlCmds.Parameters.AddWithValue("@Accounttitle11", accountingtest.Accounttitle11);
                sqlCmds.Parameters.AddWithValue("@Accounttitle12", accountingtest.Accounttitle12);
                sqlCmds.Parameters.AddWithValue("@Accounttitle13", accountingtest.Accounttitle13);
                sqlCmds.Parameters.AddWithValue("@Debit1", accountingtest.Debit1);
                sqlCmds.Parameters.AddWithValue("@Debit2", accountingtest.Debit2);
                sqlCmds.Parameters.AddWithValue("@Debit3", accountingtest.Debit3);
                sqlCmds.Parameters.AddWithValue("@Debit4", accountingtest.Debit4);
                sqlCmds.Parameters.AddWithValue("@Debit5", accountingtest.Debit5);
                sqlCmds.Parameters.AddWithValue("@Debit6", accountingtest.Debit6);
                sqlCmds.Parameters.AddWithValue("@Debit7", accountingtest.Debit7);
                sqlCmds.Parameters.AddWithValue("@Debit8", accountingtest.Debit8);
                sqlCmds.Parameters.AddWithValue("@Debit9", accountingtest.Debit9);
                sqlCmds.Parameters.AddWithValue("@Debit10", accountingtest.Debit10);
                sqlCmds.Parameters.AddWithValue("@Debit11", accountingtest.Debit11);
                sqlCmds.Parameters.AddWithValue("@Debit12", accountingtest.Debit12);
                sqlCmds.Parameters.AddWithValue("@Debit13", accountingtest.Debit13);
                sqlCmds.Parameters.AddWithValue("@Credit1", accountingtest.Credit1);
                sqlCmds.Parameters.AddWithValue("@Credit2", accountingtest.Credit2);
                sqlCmds.Parameters.AddWithValue("@Credit3", accountingtest.Credit3);
                sqlCmds.Parameters.AddWithValue("@Credit4", accountingtest.Credit4);
                sqlCmds.Parameters.AddWithValue("@Credit5", accountingtest.Credit5);
                sqlCmds.Parameters.AddWithValue("@Credit6", accountingtest.Credit6);
                sqlCmds.Parameters.AddWithValue("@Credit7", accountingtest.Credit7);
                sqlCmds.Parameters.AddWithValue("@Credit8", accountingtest.Credit8);
                sqlCmds.Parameters.AddWithValue("@Credit9", accountingtest.Credit9);
                sqlCmds.Parameters.AddWithValue("@Credit10", accountingtest.Credit10);
                sqlCmds.Parameters.AddWithValue("@Credit11", accountingtest.Credit11);
                sqlCmds.Parameters.AddWithValue("@Credit12", accountingtest.Credit12);
                sqlCmds.Parameters.AddWithValue("@Credit13", accountingtest.Credit13);
                sqlCmds.Parameters.AddWithValue("@Accountingtestshort2", accountingtest.Accountingtestshort2);

                sqlCmds.ExecuteNonQuery();
                Session["timer"] = null;
            }
            return RedirectToAction("End");
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
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}