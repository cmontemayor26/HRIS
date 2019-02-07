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
using PagedList.Mvc;
using PagedList;

namespace HRIS.Controllers
{
    public class AdminController : Controller
    {
        string connectionString = @"Data Source =TIM-PC; Initial Catalog = HRIS; Integrated Security=True;";
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


        // GET: Admin
        [HttpGet]
        public ActionResult UserList(int? i)
        {
            UserModelEntities db = new UserModelEntities();
            return View(db.Users.ToList().ToPagedList(i ?? 1,3));
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult AddApplicant()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddUser()
        {
            UserDropdownEntities userDropdownEntities = new UserDropdownEntities();
            var getuserlist = userDropdownEntities.Dropdowns.ToList();
            SelectList list = new SelectList(getuserlist.Where(o => o.DropdownType == "Userlevel"), "DropdownName", "DropdownName");
            ViewBag.userlist = list;
            return View();
        }
        [HttpPost]
        public ActionResult AddUser(UserModel userModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString)) {
                sqlCon.Open();
                string query = " select * from [dbo].[User] where Email = @email";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@Email", userModel.Email);
                SqlDataReader sdr = sqlCmd.ExecuteReader();
                if (sdr.Read())
                {
                    TempData["error"] = "Email already taken!";
                }
                else {
                    sdr.Close();
                    string querys = "INSERT INTO [User] VALUES(@Email,@UserLevel,@Password,@EmployeeNumber)";
                    SqlCommand sqlCmds = new SqlCommand(querys, sqlCon);
                    sqlCmds.Parameters.AddWithValue("@Email", userModel.Email);
                    sqlCmds.Parameters.AddWithValue("@UserLevel", userModel.Userlevel);
                    sqlCmds.Parameters.AddWithValue("@Password", Encrypt(userModel.Password));
                    sqlCmds.Parameters.AddWithValue("@EmployeeNumber", userModel.EmployeeNumber);
                    SqlDataReader sdrs = sqlCmds.ExecuteReader();
                    TempData["success"] = "New userlevel: " + userModel.Userlevel + " Added!";
                }
                
            }
                return RedirectToAction("AddUser");
        }
        [HttpGet]
        public ActionResult EditUser(int Userid)
        {
            UserDropdownEntities userDropdownEntities = new UserDropdownEntities();
            var getuserlist = userDropdownEntities.Dropdowns.ToList();
            SelectList list = new SelectList(getuserlist.Where(o => o.DropdownType == "Userlevel"), "DropdownName", "DropdownName");
            ViewBag.userlist = list;
            
            UserModel userModel = new UserModel();
            DataTable dtblUser = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString)) {
                sqlCon.Open();
                string query = "SELECT * from [User] Where Userid = @UserID";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@UserID", Userid);
                sqlDa.Fill(dtblUser);
            }
            if (dtblUser.Rows.Count == 1)
            {
                userModel.Userid = Convert.ToInt32(dtblUser.Rows[0][0].ToString());
                userModel.Email = dtblUser.Rows[0][1].ToString();
                userModel.Userlevel = dtblUser.Rows[0][2].ToString();
                userModel.EmployeeNumber = Convert.ToInt32(dtblUser.Rows[0][4].ToString());
                return View(userModel);
            }
            else {
            return RedirectToAction("Userlist");
            }
            return View();
        }
        [HttpPost]
        public ActionResult EditUser(UserModel userModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                    string querys = "UPDATE [User] SET EmployeeNumber = @EmployeeNumber, Email = @Email, Userlevel = @UserLevel Where Userid = @Userid";
                    SqlCommand sqlCmds = new SqlCommand(querys, sqlCon);
                    sqlCmds.Parameters.AddWithValue("@Userid", userModel.Userid);
                    sqlCmds.Parameters.AddWithValue("@EmployeeNumber", userModel.EmployeeNumber);
                    sqlCmds.Parameters.AddWithValue("@Email", userModel.Email);
                    sqlCmds.Parameters.AddWithValue("@UserLevel", userModel.Userlevel);
                    sqlCmds.ExecuteNonQuery();
                    TempData["success"] = "User Updated";
            }
            return RedirectToAction("Userlist");
        }

    }
}