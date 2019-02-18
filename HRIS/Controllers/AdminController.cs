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
using Microsoft.Reporting.WebForms;

namespace HRIS.Controllers
{
    public class AdminController : Controller
    {
        string connectionString = @"Data Source=192.168.102.18;Initial Catalog=HRIS;Persist Security Info=True;User ID=panoramic;Password=GoLegal100;";
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

        UserModelEntities db = new UserModelEntities();

        // GET: Admin
        [HttpGet]
        public ActionResult UserList(int? i)
        {
            UserModelEntities db = new UserModelEntities();
            return View(db.Users.ToList().ToPagedList(i ?? 1, 3));
        }
        public ActionResult ApplicantList(int? i)
        {
            MasterListEntities master = new MasterListEntities();
            return View(master.Masterlists.ToList().ToPagedList(i ?? 1, 10));
        }
        public ActionResult Reports(string Reportype)
        {
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = Server.MapPath("~/Reports/UserReport.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "UserDataSet";

            reportDataSource.Value = db.Users.ToList();

            localreport.DataSources.Add(reportDataSource);
            string reportType = Reportype;
            string mimeType;
            string encoding;
            string fileNameExtension;
            if (reportType == "Excel") {
                fileNameExtension = "xlsx";
            }
            else if (reportType == "PDF")
            {
                fileNameExtension = "pdf";
            }

            string[] streams;
            Warning[] warnings;
            byte[] renderedByte;
            renderedByte = localreport.Render(reportType, null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            Response.AddHeader("Content-Disposition", "attachment;filename= user_report." + fileNameExtension);

            return File(renderedByte, fileNameExtension);
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult AddApplicant()
        {
            UserDropdownEntities userDropdownEntities = new UserDropdownEntities();
            var getuserlist = userDropdownEntities.Dropdowns.ToList();
            SelectList MaritalStatusList = new SelectList(getuserlist.Where(o => o.DropdownType == "MaritalStatus"), "DropdownName", "DropdownName");
            SelectList JobTitleList = new SelectList(getuserlist.Where(o => o.DropdownType == "JobTitle"), "DropdownName", "DropdownName");
            ViewBag.MaritalStatusList = MaritalStatusList;
            ViewBag.JobTitleList = JobTitleList;
            var tuple = new Tuple<Masterlist, WorkExperience>(new Masterlist(), new WorkExperience());
            return View(tuple);
        }
        [HttpPost]
        public ActionResult AddApplicant(Masterlist masterlist, WorkExperience workExperience)
        {
            MasterListEntities Master = new MasterListEntities();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = " select * from Masterlist where Birthday = @Birthday AND LastName = @LastName AND FirstName = @FirstName";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@Birthday", masterlist.Birthday);
                sqlCmd.Parameters.AddWithValue("@LastName", masterlist.LastName);
                sqlCmd.Parameters.AddWithValue("@FirstName", masterlist.FirstName);
                SqlDataReader sdr = sqlCmd.ExecuteReader();
                if (sdr.Read())
                {
                    TempData["error"] = "Applicant Already Applied!";
                }
                else
                {
                    sdr.Close();
                    string querys = "INSERT INTO Masterlist(FirstName,MiddleName,LastName,Birthday,MaritalStatus,JobTitle,Street_Address1,Street_Address2,City,Province,ZipCode,ContactNumber,PersonalEmail,Applicant_AppliedDate)" +
                        "VALUES(@FirstName,@MiddleName,@LastName,@Birthday,@MaritalStatus,@JobTitle,@Street_Address1,@Street_Address2,@City,@Province,@ZipCode,@ContactNumber,@PersonalEmail,@Applicant_AppliedDate)";
                    SqlCommand sqlCmds = new SqlCommand(querys, sqlCon);
                    sqlCmds.Parameters.AddWithValue("@FirstName", masterlist.FirstName);
                    sqlCmds.Parameters.AddWithValue("@MiddleName", masterlist.MiddleName);
                    sqlCmds.Parameters.AddWithValue("@LastName", masterlist.LastName);
                    sqlCmds.Parameters.AddWithValue("@Birthday", masterlist.Birthday);
                    sqlCmds.Parameters.AddWithValue("@MaritalStatus", masterlist.MaritalStatus);
                    sqlCmds.Parameters.AddWithValue("@JobTitle", masterlist.JobTitle);
                    sqlCmds.Parameters.AddWithValue("@Street_Address1", masterlist.Street_Address1);
                    sqlCmds.Parameters.AddWithValue("@Street_Address2", masterlist.Street_Address2);
                    sqlCmds.Parameters.AddWithValue("@City", masterlist.City);
                    sqlCmds.Parameters.AddWithValue("@Province", masterlist.Province);
                    sqlCmds.Parameters.AddWithValue("@ZipCode", masterlist.ZipCode);
                    sqlCmds.Parameters.AddWithValue("@ContactNumber", masterlist.ContactNumber);
                    sqlCmds.Parameters.AddWithValue("@PersonalEmail", masterlist.PersonalEmail);
                    sqlCmds.Parameters.AddWithValue("@Applicant_AppliedDate", masterlist.Applicant_AppliedDate);
                    SqlDataReader sdrs = sqlCmds.ExecuteReader();
                    int masterlistID = Master.Masterlists.Max(item => item.MasterlistID);
                    sdrs.Close();
                    string queries = "INSERT INTO WorkExperience(MasterlistID,CompanyName,JobTitle,DateFrom,DateTo)" +
                        "VALUES(@MasterlistID,@CompanyName,@JobTitles,@DateFrom,@DateTo)";
                    SqlCommand sqlcommands = new SqlCommand(queries, sqlCon);
                    sqlcommands.Parameters.AddWithValue("@MasterlistID", masterlistID);
                    sqlcommands.Parameters.AddWithValue("@CompanyName", workExperience.CompanyName);
                    sqlcommands.Parameters.AddWithValue("@JobTitles", workExperience.JobTitle);
                    sqlcommands.Parameters.AddWithValue("@DateFrom", workExperience.DateFrom);
                    sqlcommands.Parameters.AddWithValue("@DateTo", workExperience.DateTo);
                    SqlDataReader sqldrs = sqlcommands.ExecuteReader();
                    TempData["success"] = "New Applicant: " + masterlist.FirstName + " " + masterlist.LastName + " Added!";
                    sqldrs.Close();
                }

            }
            return RedirectToAction("AddApplicant");
        }
        [HttpGet]
        public ActionResult EditApplicant(int? MasterlistID)
        {
            if (MasterlistID == null)
            {
                return RedirectToAction("ApplicantList");
            }
            UserDropdownEntities userDropdownEntities = new UserDropdownEntities();
            var getuserlist = userDropdownEntities.Dropdowns.ToList();
            SelectList MaritalStatusList = new SelectList(getuserlist.Where(o => o.DropdownType == "MaritalStatus"), "DropdownName", "DropdownName");
            SelectList JobTitleList = new SelectList(getuserlist.Where(o => o.DropdownType == "JobTitle"), "DropdownName", "DropdownName");
            ViewBag.MaritalStatusList = MaritalStatusList;
            ViewBag.JobTitleList = JobTitleList;

            ViewApplicantModel model = new ViewApplicantModel();
            DataTable dtblApplicant = new DataTable();
            DataTable dtblWorkExperience = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "SELECT * from Masterlist Where MasterlistID = @MasterlistID";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@MasterlistID", MasterlistID);
                sqlDa.Fill(dtblApplicant);

                string querys = "SELECT * from WorkExperience Where MasterlistID = @MasterlistID";
                SqlDataAdapter sqlDas = new SqlDataAdapter(querys, sqlCon);
                sqlDas.SelectCommand.Parameters.AddWithValue("@MasterlistID", MasterlistID);
                sqlDas.Fill(dtblWorkExperience);
            }
            if (dtblApplicant.Rows.Count == 1)
            {
                model.MasterlistID = Convert.ToInt32(dtblApplicant.Rows[0][0].ToString());
                model.FirstName = dtblApplicant.Rows[0][2].ToString();
                model.MiddleName = dtblApplicant.Rows[0][3].ToString();
                model.LastName = dtblApplicant.Rows[0][4].ToString();
                model.Birthday = DateTime.Parse(dtblApplicant.Rows[0][7].ToString());
                model.MaritalStatus = dtblApplicant.Rows[0][8].ToString();
                model.JobTitle = dtblApplicant.Rows[0][6].ToString();
                model.Street_Address1 = dtblApplicant.Rows[0][11].ToString();
                model.Street_Address2 = dtblApplicant.Rows[0][12].ToString();
                model.City = dtblApplicant.Rows[0][13].ToString();
                model.Province = dtblApplicant.Rows[0][14].ToString();
                model.ZipCode = dtblApplicant.Rows[0][15].ToString();
                model.ContactNumber = dtblApplicant.Rows[0][10].ToString();
                model.PersonalEmail = dtblApplicant.Rows[0][4].ToString();
                ViewBag.MasterlistID = MasterlistID;

                model.CompanyName = dtblWorkExperience.Rows[0][2].ToString();
                model.WorkExperienceJobTitle = dtblWorkExperience.Rows[0][3].ToString();
                model.DateFrom = DateTime.Parse(dtblWorkExperience.Rows[0][4].ToString());
                model.DateTo = DateTime.Parse(dtblWorkExperience.Rows[0][5].ToString());
                return View(model);

            }
            else
            {
                return RedirectToAction("ApplicantList");
            }
        }
        [HttpPost]
        public ActionResult EditApplicant(ViewApplicantModel ViewApplicantModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "UPDATE Masterlist SET FirstName= @FirstName,MiddleName=@MiddleName,LastName=@LastName,Birthday=@Birthday,MaritalStatus=@MaritalStatus,JobTitle=@JobTitle,Street_Address1=@Street_Address1,Street_Address2=@Street_Address2,City=@City,Province=@Province,ZipCode=@ZipCode,ContactNumber=@ContactNumber,PersonalEmail=@PersonalEmail WHERE MasterlistID = @MasterlistID";
                SqlCommand sqlCmds = new SqlCommand(query, sqlCon);
                sqlCmds.Parameters.AddWithValue("@FirstName", ViewApplicantModel.FirstName);
                sqlCmds.Parameters.AddWithValue("@MiddleName", ViewApplicantModel.MiddleName);
                sqlCmds.Parameters.AddWithValue("@LastName", ViewApplicantModel.LastName);
                sqlCmds.Parameters.AddWithValue("@Birthday", ViewApplicantModel.Birthday);
                sqlCmds.Parameters.AddWithValue("@MaritalStatus", ViewApplicantModel.MaritalStatus);
                sqlCmds.Parameters.AddWithValue("@JobTitle", ViewApplicantModel.JobTitle);
                sqlCmds.Parameters.AddWithValue("@Street_Address1", ViewApplicantModel.Street_Address1);
                sqlCmds.Parameters.AddWithValue("@Street_Address2", ViewApplicantModel.Street_Address2);
                sqlCmds.Parameters.AddWithValue("@City", ViewApplicantModel.City);
                sqlCmds.Parameters.AddWithValue("@Province", ViewApplicantModel.Province);
                sqlCmds.Parameters.AddWithValue("@ZipCode", ViewApplicantModel.ZipCode);
                sqlCmds.Parameters.AddWithValue("@ContactNumber", ViewApplicantModel.ContactNumber);
                sqlCmds.Parameters.AddWithValue("@PersonalEmail", ViewApplicantModel.PersonalEmail);
                sqlCmds.Parameters.AddWithValue("@MasterlistID", ViewApplicantModel.MasterlistID);
                sqlCmds.ExecuteNonQuery();

                string querys = "UPDATE WorkExperience SET CompanyName=@CompanyName,JobTitle=@JobTitle,DateFrom=@DateFrom,DateTo=@DateTo WHERE MasterlistID = @MasterlistID";
                SqlCommand cmd = new SqlCommand(querys, sqlCon);
                cmd.Parameters.AddWithValue("@CompanyName", ViewApplicantModel.CompanyName);
                cmd.Parameters.AddWithValue("@JobTitle", ViewApplicantModel.WorkExperienceJobTitle);
                cmd.Parameters.AddWithValue("@DateFrom", ViewApplicantModel.DateFrom);
                cmd.Parameters.AddWithValue("@DateTo", ViewApplicantModel.DateTo);
                cmd.Parameters.AddWithValue("@MasterlistID", ViewApplicantModel.MasterlistID);
                cmd.ExecuteNonQuery();

                TempData["success"] = "User: " + ViewApplicantModel.FirstName + " " + ViewApplicantModel.LastName + " Updated!";
            }
            return RedirectToAction("ApplicantList");
        }
        [HttpGet]
        public ActionResult ViewApplicant(int? MasterlistID)
        {
            if (MasterlistID == null)
            {
                return RedirectToAction("ApplicantList");
            }
            UserDropdownEntities userDropdownEntities = new UserDropdownEntities();
            var getuserlist = userDropdownEntities.Dropdowns.ToList();
            SelectList MaritalStatusList = new SelectList(getuserlist.Where(o => o.DropdownType == "MaritalStatus"), "DropdownName", "DropdownName");
            SelectList JobTitleList = new SelectList(getuserlist.Where(o => o.DropdownType == "JobTitle"), "DropdownName", "DropdownName");
            ViewBag.MaritalStatusList = MaritalStatusList;
            ViewBag.JobTitleList = JobTitleList;

            ViewApplicantModel model = new ViewApplicantModel();
            DataTable dtblApplicant = new DataTable();
            DataTable dtblWorkExperience = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "SELECT * from Masterlist Where MasterlistID = @MasterlistID";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@MasterlistID", MasterlistID);
                sqlDa.Fill(dtblApplicant);

                string querys = "SELECT * from WorkExperience Where MasterlistID = @MasterlistID";
                SqlDataAdapter sqlDas = new SqlDataAdapter(querys, sqlCon);
                sqlDas.SelectCommand.Parameters.AddWithValue("@MasterlistID", MasterlistID);
                sqlDas.Fill(dtblWorkExperience);
            }
            if (dtblApplicant.Rows.Count == 1)
            {
                model.MasterlistID = Convert.ToInt32(dtblApplicant.Rows[0][0].ToString());
                model.FirstName = dtblApplicant.Rows[0][2].ToString();
                model.MiddleName = dtblApplicant.Rows[0][3].ToString();
                model.LastName = dtblApplicant.Rows[0][4].ToString();
                model.Birthday = DateTime.Parse(dtblApplicant.Rows[0][7].ToString());
                model.MaritalStatus = dtblApplicant.Rows[0][8].ToString();
                model.JobTitle = dtblApplicant.Rows[0][6].ToString();
                model.Street_Address1 = dtblApplicant.Rows[0][11].ToString();
                model.Street_Address2 = dtblApplicant.Rows[0][12].ToString();
                model.City = dtblApplicant.Rows[0][13].ToString();
                model.Province = dtblApplicant.Rows[0][14].ToString();
                model.ZipCode = dtblApplicant.Rows[0][15].ToString();
                model.ContactNumber = dtblApplicant.Rows[0][10].ToString();
                model.PersonalEmail = dtblApplicant.Rows[0][4].ToString();
                ViewBag.MasterlistID = MasterlistID;

                model.CompanyName = dtblWorkExperience.Rows[0][2].ToString();
                model.WorkExperienceJobTitle = dtblWorkExperience.Rows[0][3].ToString();
                model.DateFrom = DateTime.Parse(dtblWorkExperience.Rows[0][4].ToString());
                model.DateTo = DateTime.Parse(dtblWorkExperience.Rows[0][5].ToString());
                return View(model);

            }
            else
            {
                return RedirectToAction("ApplicantList");
            }
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
                    string fileName = Path.GetFileNameWithoutExtension(userModel.ImageFile.FileName);
                    string extension = Path.GetExtension(userModel.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    userModel.ProfilePic = "~/images/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/images/"), fileName);
                    userModel.ImageFile.SaveAs(fileName);
                    string querys = "INSERT INTO [User] VALUES(@Email,@UserLevel,@Password,@EmployeeNumber,@FirstName,@LastName,@ProfilePic)";
                    SqlCommand sqlCmds = new SqlCommand(querys, sqlCon);
                    sqlCmds.Parameters.AddWithValue("@Email", userModel.Email);
                    sqlCmds.Parameters.AddWithValue("@UserLevel", userModel.Userlevel);
                    sqlCmds.Parameters.AddWithValue("@Password", Encrypt(userModel.Password));
                    sqlCmds.Parameters.AddWithValue("@EmployeeNumber", userModel.EmployeeNumber);
                    sqlCmds.Parameters.AddWithValue("@FirstName", userModel.FirstName);
                    sqlCmds.Parameters.AddWithValue("@LastName", userModel.LastName);
                    sqlCmds.Parameters.AddWithValue("@ProfilePic", userModel.ProfilePic);
                    SqlDataReader sdrs = sqlCmds.ExecuteReader();
                    TempData["success"] = "New userlevel: " + userModel.Userlevel + " Added!";
                }

            }
            return RedirectToAction("AddUser");
        }
        [HttpGet]
        public ActionResult ViewUser(int Userid)
        {
            UserDropdownEntities userDropdownEntities = new UserDropdownEntities();
            var getuserlist = userDropdownEntities.Dropdowns.ToList();
            SelectList list = new SelectList(getuserlist.Where(o => o.DropdownType == "Userlevel"), "DropdownName", "DropdownName");
            ViewBag.userlist = list;

            UserModel userModel = new UserModel();
            DataTable dtblUser = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
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
                userModel.FirstName = dtblUser.Rows[0][5].ToString();
                userModel.LastName = dtblUser.Rows[0][6].ToString();
                userModel.ProfilePic = dtblUser.Rows[0][7].ToString();
                ViewBag.Userid = Userid;
                return View(userModel);
            }
            else
            {
                return RedirectToAction("Userlist");
            }
            return View();
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
                userModel.FirstName = dtblUser.Rows[0][5].ToString();
                userModel.LastName = dtblUser.Rows[0][6].ToString();
                userModel.ProfilePic = dtblUser.Rows[0][7].ToString();
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
                string fileName = Path.GetFileNameWithoutExtension(userModel.ImageFile.FileName);
                string extension = Path.GetExtension(userModel.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                userModel.ProfilePic = "~/images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/images/"), fileName);
                userModel.ImageFile.SaveAs(fileName);
                string querys = "UPDATE [User] SET EmployeeNumber = @EmployeeNumber, Email = @Email, Userlevel = @UserLevel, FirstName=@FirstName, LastName = @LastName, ProfilePic = @ProfilePic Where Userid = @Userid";
                SqlCommand sqlCmds = new SqlCommand(querys, sqlCon);
                sqlCmds.Parameters.AddWithValue("@Userid", userModel.Userid);
                sqlCmds.Parameters.AddWithValue("@EmployeeNumber", userModel.EmployeeNumber);
                sqlCmds.Parameters.AddWithValue("@Email", userModel.Email);
                sqlCmds.Parameters.AddWithValue("@UserLevel", userModel.Userlevel);
                sqlCmds.Parameters.AddWithValue("@FirstName", userModel.FirstName);
                sqlCmds.Parameters.AddWithValue("@LastName", userModel.LastName);
                sqlCmds.Parameters.AddWithValue("@ProfilePic", userModel.ProfilePic);
                sqlCmds.ExecuteNonQuery();
                TempData["success"] = "User Updated";
            }
            return RedirectToAction("Userlist");
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(UserModel userModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = " select * from [dbo].[User] where Email = @Email AND Password =@Password";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@Email", Session["email"]);
                sqlCmd.Parameters.AddWithValue("@Password", Encrypt(userModel.OldPassword));
                SqlDataReader sdr = sqlCmd.ExecuteReader();
                if (sdr.Read())
                {
                    sdr.Close();
                    string querys = "Update [User] SET Password=@Password WHERE Email=@Email";
                    SqlCommand sqlCmds = new SqlCommand(querys, sqlCon);
                    sqlCmds.Parameters.AddWithValue("@Email", Session["email"]);
                    sqlCmds.Parameters.AddWithValue("@Password", Encrypt(userModel.Password));
                    SqlDataReader sdrs = sqlCmds.ExecuteReader();
                    
                    Session.Abandon();
                    TempData["Success"] = "Password updated! Please login to continue";
                    return RedirectToAction("Index", "Login");

                }
                else
                {
                    TempData["error"] = "Error Password!";
                    return View();
                }
                
            }
        }

    }
}