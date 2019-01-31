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
using System.Security.Cryptography;


namespace HRIS.Controllers
{
   
    public class EmployeeDashboardController : Controller
    {
        string connectionString = @"Data Source = DBASUBICIT08; Initial Catalog = BIOMETRIC; Integrated Security=True;";
        // GET: EmployeeDashboard
        [HttpGet]
        public ActionResult Index()
        {
            //DataTable biometric = new DataTable();
            //using (SqlConnection sqlCon = new SqlConnection(connectionString))
            //{
            //    sqlCon.Open();
            //    SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM qryCheckInOut", sqlCon);
            //    sqlDa.Fill(biometric);

            //}
            return View();
        }
        [HttpPost]
        public ActionResult Index(Biometric biometric)
        {
            SqlConnection sqlcon = new SqlConnection(connectionString);
            string sqlquery = "SELECT CHECKTIME,CHECKTYPE FROM qryCheckInOut WHERE Badgenumber = @userID";
            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand(sqlquery, sqlcon);
            sqlcom.Parameters.AddWithValue("@userID", biometric.UserID);
            Session["userId"] = biometric.UserID;
            sqlcom.ExecuteNonQuery();
            return View();
            //SqlDataAdapter sqlDa = new SqlDataAdapter(sqlcom);
            //DataTable bio = new DataTable();
            //sqlDa.Fill(bio);
            //SqlDataReader sdr = sqlcom.ExecuteReader();

            //return View(bio);

        }
        // GET: Admin
        public ActionResult Biolist()
        {
            DataTable bio = new DataTable();
            using (SqlConnection Sqlcon = new SqlConnection(connectionString))
            {
                Sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT TOP 5 * FROM qrycheckInOut", Sqlcon);
                sqlDa.Fill(bio);
            }
            return View(bio);

            
            //if (Session["userId"] != null)
            //{


            //    SqlConnection sqlcon = new SqlConnection(connectionString);
            //    string sqlquery = "SELECT CHECKTIME,CHECKTYPE FROM qryCheckInOut WHERE Badgenumber = @userID LIMIT 5";
            //    sqlcon.Open();
            //    SqlCommand sqlcom = new SqlCommand(sqlquery, sqlcon);
            //    sqlcom.Parameters.AddWithValue("@userID", Session["userId"]);

            //    SqlDataAdapter sqlDa = new SqlDataAdapter(sqlcom);
            //    DataTable bio = new DataTable();
            //    sqlDa.Fill(bio);
            //    SqlDataReader sdr = sqlcom.ExecuteReader();
            //    if (sdr.Read())
            //    {
            //        return View(sdr);
            //    }
            //}
            //else
            //{
            //    DataTable bio = new DataTable();
            //    using (SqlConnection Sqlcon = new SqlConnection(connectionString))
            //    {
            //        Sqlcon.Open();
            //        SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT TOP 5 * FROM qrycheckInOut ", Sqlcon);
            //        sqlDa.Fill(bio);
            //    }
            //    return View(bio);

            //}

            //return View();


            //DataTable dataTable = new DataTable();
            //using (SqlConnection SqlCon = new SqlConnection(connectionString))

            //{
            //    SqlCon.Open();
            //    string sqlQuery = "SELECT * FROM qryCheckInOut WHERE Badgenumber = @userID";
            //    SqlDataAdapter sqlDa = new SqlDataAdapter(sqlQuery, SqlCon);
            //    sqlDa.SelectCommand.Parameters.AddWithValue("@userID", biometric.UserID);

            //    var model = new List<Biometric>();
            //    SqlCommand cmd = new SqlCommand(sqlQuery, SqlCon);
            //    sqlDa.Fill(dataTable);
            //    SqlDataReader dbReader = cmd.ExecuteReader();
            //    while (dbReader.Read())
            //    {
            //        var biometrics = new Biometric();
            //        biometrics.Date = dbReader["CHECKTIME"].ToString();
            //        biometrics.Time = dbReader["CHECKTIME"].ToString();
            //        biometrics.CheckMode = dbReader["CHECKTYPE"].ToString();
            //        model.Add(biometrics);
            //    }
            //    return View(model);
            //}
        }

    }
}