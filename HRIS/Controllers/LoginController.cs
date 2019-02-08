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
    public class LoginController : Controller
    {
        // Encryption
        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Login lg)
        {
            string mainconn = @"Data Source = DBASUBICIT08; Initial Catalog = HRIS; Integrated Security=True;";
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery = " select * from [dbo].[User] where Email = @email and Password = @password";
            sqlconn.Open();
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlcomm.Parameters.AddWithValue("@email", lg.Email);
            sqlcomm.Parameters.AddWithValue("@password", Encrypt(lg.Password));

            SqlDataAdapter SqlDa = new SqlDataAdapter(sqlcomm);
            DataTable Logins = new DataTable();
            SqlDa.Fill(Logins);

            SqlDataReader sdr = sqlcomm.ExecuteReader();
            if (sdr.Read())
            {
                for (int i = 0; i < Logins.Rows.Count; i ++) {
                    Session["userlevel"] = Logins.Rows[i][2];
                    Session["employeenumber"] = Logins.Rows[i][4];
                }
                Session["email"] = lg.Email.ToString();
                string userlevel = Session["userlevel"] as String;
                if (userlevel == "Admin")
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                else if( userlevel == "Employee"){
                    return RedirectToAction("Index", "EmployeeDashboard");
                }
                
            }
            else {
                ViewData["Message"] = "Credentials are Invalid!";
            }
            sqlconn.Close();
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}