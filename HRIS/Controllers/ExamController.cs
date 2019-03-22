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

                string querys = "Update Masterlist set Applicant_TookExam = 1 WHERE MasterlistID = @Masterlistid";
                SqlCommand sqlCmdss = new SqlCommand(querys, sqlCon);
                sqlCmdss.Parameters.AddWithValue("@Masterlistid", Convert.ToInt32(Session["MasterlistID"].ToString()));
                sqlCmdss.ExecuteNonQuery();

                Session["timer"] = null;
            }
            return RedirectToAction("End");
        }
        public ActionResult IQtest1()
        {
            DataTable dtblQuestions = new DataTable();
            DataTable dtblAnswers = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "SELECT * from Answers";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.Fill(dtblAnswers);

                string querys = "SELECT * from Questions";
                SqlDataAdapter sqlDas = new SqlDataAdapter(querys, sqlCon);
                sqlDas.Fill(dtblQuestions);
            }
            if (dtblQuestions.Rows.Count == 1)
            {
                ViewBag.Q1 = dtblQuestions.Rows[0][1].ToString();
                ViewBag.Q2 = dtblQuestions.Rows[0][2].ToString();
                ViewBag.Q3 = dtblQuestions.Rows[0][3].ToString();
                ViewBag.Q4 = dtblQuestions.Rows[0][4].ToString();
                ViewBag.Q5 = dtblQuestions.Rows[0][5].ToString();
                ViewBag.Q6 = dtblQuestions.Rows[0][6].ToString();
                ViewBag.Q7 = dtblQuestions.Rows[0][7].ToString();
                ViewBag.Q8 = dtblQuestions.Rows[0][8].ToString();
                ViewBag.Q9 = dtblQuestions.Rows[0][9].ToString();
                ViewBag.Q10 = dtblQuestions.Rows[0][10].ToString();
                ViewBag.Q11 = dtblQuestions.Rows[0][11].ToString();
                ViewBag.Q12 = dtblQuestions.Rows[0][12].ToString();
                ViewBag.Q13 = dtblQuestions.Rows[0][13].ToString();
                ViewBag.Q14 = dtblQuestions.Rows[0][14].ToString();
                ViewBag.Q15 = dtblQuestions.Rows[0][15].ToString();
                ViewBag.Q16 = dtblQuestions.Rows[0][16].ToString();
                ViewBag.Q17 = dtblQuestions.Rows[0][17].ToString();
                ViewBag.Q18 = dtblQuestions.Rows[0][18].ToString();
                ViewBag.Q19 = dtblQuestions.Rows[0][19].ToString();
                ViewBag.Q20 = dtblQuestions.Rows[0][20].ToString();
                ViewBag.Q21 = dtblQuestions.Rows[0][21].ToString();
                ViewBag.Q22 = dtblQuestions.Rows[0][22].ToString();
                ViewBag.Q23 = dtblQuestions.Rows[0][23].ToString();
                ViewBag.Q24 = dtblQuestions.Rows[0][24].ToString();
                ViewBag.Q25 = dtblQuestions.Rows[0][25].ToString();
                ViewBag.Q26 = dtblQuestions.Rows[0][26].ToString();
                ViewBag.Q27 = dtblQuestions.Rows[0][27].ToString();
                ViewBag.Q28 = dtblQuestions.Rows[0][28].ToString();
                ViewBag.Q29 = dtblQuestions.Rows[0][29].ToString();
                ViewBag.Q30 = dtblQuestions.Rows[0][30].ToString();

                ViewBag.Answer_Q1_1 = dtblAnswers.Rows[0][1].ToString();
                ViewBag.Answer_Q1_2 = dtblAnswers.Rows[0][2].ToString();
                ViewBag.Answer_Q1_3 = dtblAnswers.Rows[0][3].ToString();
                ViewBag.Answer_Q1_4 = dtblAnswers.Rows[0][4].ToString();
                ViewBag.Answer_Q4_1 = dtblAnswers.Rows[0][5].ToString();
                ViewBag.Answer_Q4_2 = dtblAnswers.Rows[0][6].ToString();
                ViewBag.Answer_Q4_3 = dtblAnswers.Rows[0][7].ToString();
                ViewBag.Answer_Q4_4 = dtblAnswers.Rows[0][8].ToString();
                ViewBag.Answer_Q6_1 = dtblAnswers.Rows[0][9].ToString();
                ViewBag.Answer_Q6_2 = dtblAnswers.Rows[0][10].ToString();
                ViewBag.Answer_Q6_3 = dtblAnswers.Rows[0][11].ToString();
                ViewBag.Answer_Q6_4 = dtblAnswers.Rows[0][12].ToString();
                ViewBag.Answer_Q8_1 = dtblAnswers.Rows[0][13].ToString();
                ViewBag.Answer_Q8_2 = dtblAnswers.Rows[0][14].ToString();
                ViewBag.Answer_Q8_3 = dtblAnswers.Rows[0][15].ToString();
                ViewBag.Answer_Q8_4 = dtblAnswers.Rows[0][16].ToString();
                ViewBag.Answer_Q9_1 = dtblAnswers.Rows[0][17].ToString();
                ViewBag.Answer_Q9_2 = dtblAnswers.Rows[0][18].ToString();
                ViewBag.Answer_Q9_3 = dtblAnswers.Rows[0][19].ToString();
                ViewBag.Answer_Q9_4 = dtblAnswers.Rows[0][20].ToString();
                ViewBag.Answer_Q10_1 = dtblAnswers.Rows[0][21].ToString();
                ViewBag.Answer_Q10_2 = dtblAnswers.Rows[0][22].ToString();
                ViewBag.Answer_Q10_3 = dtblAnswers.Rows[0][23].ToString();
                ViewBag.Answer_Q10_4 = dtblAnswers.Rows[0][24].ToString();
                ViewBag.Answer_Q11_1 = dtblAnswers.Rows[0][25].ToString();
                ViewBag.Answer_Q11_2 = dtblAnswers.Rows[0][26].ToString();
                ViewBag.Answer_Q11_3 = dtblAnswers.Rows[0][27].ToString();
                ViewBag.Answer_Q11_4 = dtblAnswers.Rows[0][28].ToString();
                ViewBag.Answer_Q14_1 = dtblAnswers.Rows[0][29].ToString();
                ViewBag.Answer_Q14_2 = dtblAnswers.Rows[0][30].ToString();
                ViewBag.Answer_Q14_3 = dtblAnswers.Rows[0][31].ToString();
                ViewBag.Answer_Q14_4 = dtblAnswers.Rows[0][32].ToString();
                ViewBag.Answer_Q16_1 = dtblAnswers.Rows[0][33].ToString();
                ViewBag.Answer_Q16_2 = dtblAnswers.Rows[0][34].ToString();
                ViewBag.Answer_Q16_3 = dtblAnswers.Rows[0][35].ToString();
                ViewBag.Answer_Q16_4 = dtblAnswers.Rows[0][36].ToString();
                ViewBag.Answer_Q17_1 = dtblAnswers.Rows[0][37].ToString();
                ViewBag.Answer_Q17_2 = dtblAnswers.Rows[0][38].ToString();
                ViewBag.Answer_Q17_3 = dtblAnswers.Rows[0][39].ToString();
                ViewBag.Answer_Q17_4 = dtblAnswers.Rows[0][40].ToString();
                ViewBag.Answer_Q18_1 = dtblAnswers.Rows[0][41].ToString();
                ViewBag.Answer_Q18_2 = dtblAnswers.Rows[0][42].ToString();
                ViewBag.Answer_Q18_3 = dtblAnswers.Rows[0][43].ToString();
                ViewBag.Answer_Q18_4 = dtblAnswers.Rows[0][44].ToString();
                ViewBag.Answer_Q19_1 = dtblAnswers.Rows[0][45].ToString();
                ViewBag.Answer_Q19_2 = dtblAnswers.Rows[0][46].ToString();
                ViewBag.Answer_Q19_3 = dtblAnswers.Rows[0][47].ToString();
                ViewBag.Answer_Q19_4 = dtblAnswers.Rows[0][48].ToString();
                ViewBag.Answer_Q20_1 = dtblAnswers.Rows[0][49].ToString();
                ViewBag.Answer_Q20_2 = dtblAnswers.Rows[0][50].ToString();
                ViewBag.Answer_Q20_3 = dtblAnswers.Rows[0][51].ToString();
                ViewBag.Answer_Q20_4 = dtblAnswers.Rows[0][52].ToString();
                ViewBag.Answer_Q21_1 = dtblAnswers.Rows[0][53].ToString();
                ViewBag.Answer_Q21_2 = dtblAnswers.Rows[0][54].ToString();
                ViewBag.Answer_Q21_3 = dtblAnswers.Rows[0][55].ToString();
                ViewBag.Answer_Q21_4 = dtblAnswers.Rows[0][56].ToString();
                ViewBag.Answer_Q22_1 = dtblAnswers.Rows[0][57].ToString();
                ViewBag.Answer_Q22_2 = dtblAnswers.Rows[0][58].ToString();
                ViewBag.Answer_Q22_3 = dtblAnswers.Rows[0][59].ToString();
                ViewBag.Answer_Q22_4 = dtblAnswers.Rows[0][60].ToString();
                ViewBag.Answer_Q24_1 = dtblAnswers.Rows[0][61].ToString();
                ViewBag.Answer_Q24_2 = dtblAnswers.Rows[0][62].ToString();
                ViewBag.Answer_Q24_3 = dtblAnswers.Rows[0][63].ToString();
                ViewBag.Answer_Q24_4 = dtblAnswers.Rows[0][64].ToString();
                ViewBag.Answer_Q25_1 = dtblAnswers.Rows[0][65].ToString();
                ViewBag.Answer_Q25_2 = dtblAnswers.Rows[0][66].ToString();
                ViewBag.Answer_Q25_3 = dtblAnswers.Rows[0][67].ToString();
                ViewBag.Answer_Q25_4 = dtblAnswers.Rows[0][68].ToString();
                ViewBag.Answer_Q26_1 = dtblAnswers.Rows[0][69].ToString();
                ViewBag.Answer_Q26_2 = dtblAnswers.Rows[0][70].ToString();
                ViewBag.Answer_Q26_3 = dtblAnswers.Rows[0][71].ToString();
                ViewBag.Answer_Q26_4 = dtblAnswers.Rows[0][72].ToString();
                ViewBag.Answer_Q28_1 = dtblAnswers.Rows[0][73].ToString();
                ViewBag.Answer_Q28_2 = dtblAnswers.Rows[0][74].ToString();
                ViewBag.Answer_Q28_3 = dtblAnswers.Rows[0][75].ToString();
                ViewBag.Answer_Q28_4 = dtblAnswers.Rows[0][76].ToString();
                ViewBag.Answer_Q29_1 = dtblAnswers.Rows[0][77].ToString();
                ViewBag.Answer_Q29_2 = dtblAnswers.Rows[0][78].ToString();
                ViewBag.Answer_Q29_3 = dtblAnswers.Rows[0][79].ToString();
                ViewBag.Answer_Q29_4 = dtblAnswers.Rows[0][80].ToString();
                ViewBag.Answer_Q30_1 = dtblAnswers.Rows[0][81].ToString();
                ViewBag.Answer_Q30_2 = dtblAnswers.Rows[0][82].ToString();
                ViewBag.Answer_Q30_3 = dtblAnswers.Rows[0][83].ToString();
                ViewBag.Answer_Q30_4 = dtblAnswers.Rows[0][84].ToString();

                
                return View();
            }
            else
            {
                return RedirectToAction("ApplicantList");
            }
        }
        [HttpPost]
        public ActionResult IQtest1(IQtest iqtest)
        {
            DataTable dtblAnswers = new DataTable();

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string querys = "SELECT * from Answers";
                SqlDataAdapter sqlDas = new SqlDataAdapter(querys, sqlCon);
                sqlDas.Fill(dtblAnswers);

                ViewBag.Answer_Q1_1 = dtblAnswers.Rows[0][1].ToString();
                ViewBag.Answer_Q1_2 = dtblAnswers.Rows[0][2].ToString();
                ViewBag.Answer_Q1_3 = dtblAnswers.Rows[0][3].ToString();
                ViewBag.Answer_Q1_4 = dtblAnswers.Rows[0][4].ToString();
                ViewBag.Answer_Q4_1 = dtblAnswers.Rows[0][5].ToString();
                ViewBag.Answer_Q4_2 = dtblAnswers.Rows[0][6].ToString();
                ViewBag.Answer_Q4_3 = dtblAnswers.Rows[0][7].ToString();
                ViewBag.Answer_Q4_4 = dtblAnswers.Rows[0][8].ToString();
                ViewBag.Answer_Q6_1 = dtblAnswers.Rows[0][9].ToString();
                ViewBag.Answer_Q6_2 = dtblAnswers.Rows[0][10].ToString();
                ViewBag.Answer_Q6_3 = dtblAnswers.Rows[0][11].ToString();
                ViewBag.Answer_Q6_4 = dtblAnswers.Rows[0][12].ToString();
                ViewBag.Answer_Q8_1 = dtblAnswers.Rows[0][13].ToString();
                ViewBag.Answer_Q8_2 = dtblAnswers.Rows[0][14].ToString();
                ViewBag.Answer_Q8_3 = dtblAnswers.Rows[0][15].ToString();
                ViewBag.Answer_Q8_4 = dtblAnswers.Rows[0][16].ToString();
                ViewBag.Answer_Q9_1 = dtblAnswers.Rows[0][17].ToString();
                ViewBag.Answer_Q9_2 = dtblAnswers.Rows[0][18].ToString();
                ViewBag.Answer_Q9_3 = dtblAnswers.Rows[0][19].ToString();
                ViewBag.Answer_Q9_4 = dtblAnswers.Rows[0][20].ToString();
                ViewBag.Answer_Q10_1 = dtblAnswers.Rows[0][21].ToString();
                ViewBag.Answer_Q10_2 = dtblAnswers.Rows[0][22].ToString();
                ViewBag.Answer_Q10_3 = dtblAnswers.Rows[0][23].ToString();
                ViewBag.Answer_Q10_4 = dtblAnswers.Rows[0][24].ToString();
                ViewBag.Answer_Q11_1 = dtblAnswers.Rows[0][25].ToString();
                ViewBag.Answer_Q11_2 = dtblAnswers.Rows[0][26].ToString();
                ViewBag.Answer_Q11_3 = dtblAnswers.Rows[0][27].ToString();
                ViewBag.Answer_Q11_4 = dtblAnswers.Rows[0][28].ToString();
                ViewBag.Answer_Q14_1 = dtblAnswers.Rows[0][29].ToString();
                ViewBag.Answer_Q14_2 = dtblAnswers.Rows[0][30].ToString();
                ViewBag.Answer_Q14_3 = dtblAnswers.Rows[0][31].ToString();
                ViewBag.Answer_Q14_4 = dtblAnswers.Rows[0][32].ToString();
                ViewBag.Answer_Q16_1 = dtblAnswers.Rows[0][33].ToString();
                ViewBag.Answer_Q16_2 = dtblAnswers.Rows[0][34].ToString();
                ViewBag.Answer_Q16_3 = dtblAnswers.Rows[0][35].ToString();
                ViewBag.Answer_Q16_4 = dtblAnswers.Rows[0][36].ToString();
                ViewBag.Answer_Q17_1 = dtblAnswers.Rows[0][37].ToString();
                ViewBag.Answer_Q17_2 = dtblAnswers.Rows[0][38].ToString();
                ViewBag.Answer_Q17_3 = dtblAnswers.Rows[0][39].ToString();
                ViewBag.Answer_Q17_4 = dtblAnswers.Rows[0][40].ToString();
                ViewBag.Answer_Q18_1 = dtblAnswers.Rows[0][41].ToString();
                ViewBag.Answer_Q18_2 = dtblAnswers.Rows[0][42].ToString();
                ViewBag.Answer_Q18_3 = dtblAnswers.Rows[0][43].ToString();
                ViewBag.Answer_Q18_4 = dtblAnswers.Rows[0][44].ToString();
                ViewBag.Answer_Q19_1 = dtblAnswers.Rows[0][45].ToString();
                ViewBag.Answer_Q19_2 = dtblAnswers.Rows[0][46].ToString();
                ViewBag.Answer_Q19_3 = dtblAnswers.Rows[0][47].ToString();
                ViewBag.Answer_Q19_4 = dtblAnswers.Rows[0][48].ToString();
                ViewBag.Answer_Q20_1 = dtblAnswers.Rows[0][49].ToString();
                ViewBag.Answer_Q20_2 = dtblAnswers.Rows[0][50].ToString();
                ViewBag.Answer_Q20_3 = dtblAnswers.Rows[0][51].ToString();
                ViewBag.Answer_Q20_4 = dtblAnswers.Rows[0][52].ToString();
                ViewBag.Answer_Q21_1 = dtblAnswers.Rows[0][53].ToString();
                ViewBag.Answer_Q21_2 = dtblAnswers.Rows[0][54].ToString();
                ViewBag.Answer_Q21_3 = dtblAnswers.Rows[0][55].ToString();
                ViewBag.Answer_Q21_4 = dtblAnswers.Rows[0][56].ToString();
                ViewBag.Answer_Q22_1 = dtblAnswers.Rows[0][57].ToString();
                ViewBag.Answer_Q22_2 = dtblAnswers.Rows[0][58].ToString();
                ViewBag.Answer_Q22_3 = dtblAnswers.Rows[0][59].ToString();
                ViewBag.Answer_Q22_4 = dtblAnswers.Rows[0][60].ToString();
                ViewBag.Answer_Q24_1 = dtblAnswers.Rows[0][61].ToString();
                ViewBag.Answer_Q24_2 = dtblAnswers.Rows[0][62].ToString();
                ViewBag.Answer_Q24_3 = dtblAnswers.Rows[0][63].ToString();
                ViewBag.Answer_Q24_4 = dtblAnswers.Rows[0][64].ToString();
                ViewBag.Answer_Q25_1 = dtblAnswers.Rows[0][65].ToString();
                ViewBag.Answer_Q25_2 = dtblAnswers.Rows[0][66].ToString();
                ViewBag.Answer_Q25_3 = dtblAnswers.Rows[0][67].ToString();
                ViewBag.Answer_Q25_4 = dtblAnswers.Rows[0][68].ToString();
                ViewBag.Answer_Q26_1 = dtblAnswers.Rows[0][69].ToString();
                ViewBag.Answer_Q26_2 = dtblAnswers.Rows[0][70].ToString();
                ViewBag.Answer_Q26_3 = dtblAnswers.Rows[0][71].ToString();
                ViewBag.Answer_Q26_4 = dtblAnswers.Rows[0][72].ToString();
                ViewBag.Answer_Q28_1 = dtblAnswers.Rows[0][73].ToString();
                ViewBag.Answer_Q28_2 = dtblAnswers.Rows[0][74].ToString();
                ViewBag.Answer_Q28_3 = dtblAnswers.Rows[0][75].ToString();
                ViewBag.Answer_Q28_4 = dtblAnswers.Rows[0][76].ToString();
                ViewBag.Answer_Q29_1 = dtblAnswers.Rows[0][77].ToString();
                ViewBag.Answer_Q29_2 = dtblAnswers.Rows[0][78].ToString();
                ViewBag.Answer_Q29_3 = dtblAnswers.Rows[0][79].ToString();
                ViewBag.Answer_Q29_4 = dtblAnswers.Rows[0][80].ToString();
                ViewBag.Answer_Q30_1 = dtblAnswers.Rows[0][81].ToString();
                ViewBag.Answer_Q30_2 = dtblAnswers.Rows[0][82].ToString();
                ViewBag.Answer_Q30_3 = dtblAnswers.Rows[0][83].ToString();
                ViewBag.Answer_Q30_4 = dtblAnswers.Rows[0][84].ToString();



                int scores = 0;
                if (iqtest.IQtest1 == ViewBag.Answer_Q1_3) { scores = scores + 1; }
                if (iqtest.IQtest2 == "10") { scores = scores + 1; }
                if (iqtest.IQtest3 == "Match") { scores = scores + 1; }
                if (iqtest.IQtest4 == ViewBag.Answer_Q4_3) { scores = scores + 1; }
                if (iqtest.IQtest5 == "12") { scores = scores + 1; }
                if (iqtest.IQtest6 == ViewBag.Answer_Q6_2) { scores = scores + 1; }
                if (iqtest.IQtest7 == "25") { scores = scores + 1; }
                if (iqtest.IQtest8 == ViewBag.Answer_Q8_4) { scores = scores + 1; }
                if (iqtest.IQtest9 == ViewBag.Answer_Q9_1) { scores = scores + 1; }
                if (iqtest.IQtest10 == ViewBag.Answer_Q10_3) { scores = scores + 1; }
                if (iqtest.IQtest11 == ViewBag.Answer_Q11_3) { scores = scores + 1; }
                if (iqtest.IQtest12 == "20") { scores = scores + 1; }
                if (iqtest.IQtest13 == "14") { scores = scores + 1; }
                if (iqtest.IQtest14 == ViewBag.Answer_Q14_3) { scores = scores + 1; }
                if (iqtest.IQtest15 == "63") { scores = scores + 1; }
                if (iqtest.IQtest16 == ViewBag.Answer_Q16_1) { scores = scores + 1; }
                if (iqtest.IQtest17 == ViewBag.Answer_Q17_3) { scores = scores + 1; }
                if (iqtest.IQtest18 == ViewBag.Answer_Q18_1) { scores = scores + 1; }
                if (iqtest.IQtest19 == ViewBag.Answer_Q19_3) { scores = scores + 1; }
                if (iqtest.IQtest20 == ViewBag.Answer_Q20_2) { scores = scores + 1; }
                if (iqtest.IQtest21 == ViewBag.Answer_Q21_1) { scores = scores + 1; }
                if (iqtest.IQtest22 == ViewBag.Answer_Q22_3) { scores = scores + 1; }
                if (iqtest.IQtest23 == "1 hr" || iqtest.IQtest23 == "1 hour") { scores = scores + 1; }
                if (iqtest.IQtest24 == ViewBag.Answer_Q24_3) { scores = scores + 1; }
                if (iqtest.IQtest25 == ViewBag.Answer_Q25_3) { scores = scores + 1; }
                if (iqtest.IQtest26 == ViewBag.Answer_Q26_2) { scores = scores + 1; }
                if (iqtest.IQtest27 == "80") { scores = scores + 1; }
                if (iqtest.IQtest28 == ViewBag.Answer_Q28_1) { scores = scores + 1; }
                if (iqtest.IQtest29 == ViewBag.Answer_Q29_2) { scores = scores + 1; }
                if (iqtest.IQtest30 == ViewBag.Answer_Q30_1) { scores = scores + 1; }


                string query = "INSERT INTO IQtest(ApplicantID,IQtest1,IQtest2,IQtest3,IQtest4,IQtest5,IQtest6,IQtest7,IQtest8,IQtest9,IQtest10,IQtest11,IQtest12,IQtest13,IQtest14,IQtest15,IQtest16,IQtest17,IQtest18,IQtest19,IQtest20,IQtest21,IQtest22,IQtest23,IQtest24,IQtest25,IQtest26,IQtest27,IQtest28,IQtest29,IQtest30,score)" +
                        "VALUES(@ApplicantID,@IQtest1,@IQtest2,@IQtest3,@IQtest4,@IQtest5,@IQtest6,@IQtest7,@IQtest8,@IQtest9,@IQtest10,@IQtest11,@IQtest12,@IQtest13,@IQtest14,@IQtest15,@IQtest16,@IQtest17,@IQtest18,@IQtest19,@IQtest20,@IQtest21,@IQtest22,@IQtest23,@IQtest24,@IQtest25,@IQtest26,@IQtest27,@IQtest28,@IQtest29,@IQtest30,@score)";
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
                sqlCmds.Parameters.AddWithValue("@score", scores);
                sqlCmds.ExecuteNonQuery();

                Session["timer"] = null;
            }
            if (Session["JobTitle"].ToString() == "Associate Accountant" || Session["JobTitle"].ToString() == "Senior Accountant") {
                return RedirectToAction("Accountingtest", "Exam");
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

        public ActionResult AccountingIntro()
        {
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
                string query = "INSERT INTO Accountingtest(ApplicantID,Accountingtest1,Accountingtest2,Accountingtest3_1,Accountingtest3_2,Accountingtest3_3,Accountingtest4,Accountingtest5,Accountingtest6,Accountingtest7,Accountingtest8,Accountingtest9,Accountingtest10,Solution1,Solution3_1,Solution3_2,Solution3_3,Solution4,Solution7,Solution8,Solution10," +
                    "Accounttitle1,Accounttitle2,Accounttitle3,Accounttitle4,Accounttitle5,Accounttitle6,Accounttitle7,Accounttitle8,Accounttitle9,Accounttitle10,Accounttitle11,Accounttitle12,Accounttitle13,Debit1,Debit2,Debit3,Debit4,Debit5,Debit6,Debit7,Debit8,Debit9,Debit10,Debit11,Debit12,Debit13,Credit1,Credit2,Credit3,Credit4,Credit5,Credit6,Credit7,Credit8,Credit9,Credit10,Credit11,Credit12,Credit13,Accountingtestshort2" +
                    ",Taxbracket1,Taxbracket2,Taxbracket3,Rate1,Rate2,Rate3,Amount1,Amount2,Amount3,Totalincometax,Booktitle1,Booktitle2,Booktitle3,Booktitle4,Bookdebit1,Bookdebit2,Bookdebit3,Bookdebit4,Bookcredit1,Bookcredit2,Bookcredit3,Bookcredit4,Balancebook,Banktitle1,Banktitle2,Banktitle3,Banktitle4,Banktitle5,Banktitle6,Banktitle7,Bankdebit1,Bankdebit2,Bankdebit3,Bankdebit4,Bankdebit5,Bankdebit6,Bankdebit7,Bankcredit1,Bankcredit2,Bankcredit3,Bankcredit4,Bankcredit5,Bankcredit6,Bankcredit7,Balancebank" +
                    ",Asset1,Asset2,Asset3,Asset4,Asset5,Asset6,Asset7,Debitasset1,Debitasset2,Debitasset3,Debitasset4,Debitasset5,Debitasset6,Debitasset7,Creditasset1,Creditasset2,Creditasset3,Creditasset4,Creditasset5,Creditasset6,Creditasset7,Totalasset,Equity1,Equity2,Equity3,Equity4,Equity5,Equity6,Equity7,Equity8,Debitequity1,Debitequity2,Debitequity3,Debitequity4,Debitequity5,Debitequity6,Debitequity7,Debitequity8,Creditequity1,Creditequity2,Creditequity3,Creditequity4,Creditequity5,Creditequity6,Creditequity7,Creditequity8,Totalequity)" +
                        "VALUES(@ApplicantID,@Accountingtest1,@Accountingtest2,@Accountingtest31,@Accountingtest32,@Accountingtest33,@Accountingtest4,@Accountingtest5,@Accountingtest6,@Accountingtest7,@Accountingtest8,@Accountingtest9,@Accountingtest10,@Solution1,@Solution31,@Solution32,@Solution33,@Solution4,@Solution7,@Solution8,@Solution10" +
                        ",@Accounttitle1,@Accounttitle2,@Accounttitle3,@Accounttitle4,@Accounttitle5,@Accounttitle6,@Accounttitle7,@Accounttitle8,@Accounttitle9,@Accounttitle10,@Accounttitle11,@Accounttitle12,@Accounttitle13,@Debit1,@Debit2,@Debit3,@Debit4,@Debit5,@Debit6,@Debit7,@Debit8,@Debit9,@Debit10,@Debit11,@Debit12,@Debit13,@Credit1,@Credit2,@Credit3,@Credit4,@Credit5,@Credit6,@Credit7,@Credit8,@Credit9,@Credit10,@Credit11,@Credit12,@Credit13,@Accountingtestshort2" +
                        ", @Taxbracket1, @Taxbracket2, @Taxbracket3, @Rate1, @Rate2, @Rate3, @Amount1, @Amount2, @Amount3, @Totalincometax, @Booktitle1, @Booktitle2, @Booktitle3, @Booktitle4, @Bookdebit1, @Bookdebit2, @Bookdebit3, @Bookdebit4, @Bookcredit1, @Bookcredit2, @Bookcredit3, @Bookcredit4, @Balancebook, @Banktitle1, @Banktitle2, @Banktitle3, @Banktitle4, @Banktitle5, @Banktitle6, @Banktitle7, @Bankdebit1, @Bankdebit2, @Bankdebit3, @Bankdebit4, @Bankdebit5, @Bankdebit6, @Bankdebit7, @Bankcredit1, @Bankcredit2, @Bankcredit3, @Bankcredit4, @Bankcredit5, @Bankcredit6, @Bankcredit7, @Balancebank" +
                    ",@Asset1,@Asset2,@Asset3,@Asset4,@Asset5,@Asset6,@Asset7,@Debitasset1,@Debitasset2,@Debitasset3,@Debitasset4,@Debitasset5,@Debitasset6,@Debitasset7,@Creditasset1,@Creditasset2,@Creditasset3,@Creditasset4,@Creditasset5,@Creditasset6,@Creditasset7,@Totalasset,@Equity1,@Equity2,@Equity3,@Equity4,@Equity5,@Equity6,@Equity7,@Equity8,@Debitequity1,@Debitequity2,@Debitequity3,@Debitequity4,@Debitequity5,@Debitequity6,@Debitequity7,@Debitequity8,@Creditequity1,@Creditequity2,@Creditequity3,@Creditequity4,@Creditequity5,@Creditequity6,@Creditequity7,@Creditequity8,@Totalequity)";
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
                sqlCmds.Parameters.AddWithValue("@Taxbracket1", accountingtest.Taxbracket1);
                sqlCmds.Parameters.AddWithValue("@Taxbracket2", accountingtest.Taxbracket2);
                sqlCmds.Parameters.AddWithValue("@Taxbracket3", accountingtest.Taxbracket3);
                sqlCmds.Parameters.AddWithValue("@Rate1", accountingtest.Rate1);
                sqlCmds.Parameters.AddWithValue("@Rate2", accountingtest.Rate2);
                sqlCmds.Parameters.AddWithValue("@Rate3", accountingtest.Rate3);
                sqlCmds.Parameters.AddWithValue("@Amount1", accountingtest.Amount1);
                sqlCmds.Parameters.AddWithValue("@Amount2", accountingtest.Amount2);
                sqlCmds.Parameters.AddWithValue("@Amount3", accountingtest.Amount3);
                sqlCmds.Parameters.AddWithValue("@Totalincometax", accountingtest.Totalincometax);
                sqlCmds.Parameters.AddWithValue("@Booktitle1", accountingtest.Booktitle1);
                sqlCmds.Parameters.AddWithValue("@Booktitle2", accountingtest.Booktitle2);
                sqlCmds.Parameters.AddWithValue("@Booktitle3", accountingtest.Booktitle3);
                sqlCmds.Parameters.AddWithValue("@Booktitle4", accountingtest.Booktitle4);
                sqlCmds.Parameters.AddWithValue("@Bookdebit1", accountingtest.Bookdebit1);
                sqlCmds.Parameters.AddWithValue("@Bookdebit2", accountingtest.Bookdebit2);
                sqlCmds.Parameters.AddWithValue("@Bookdebit3", accountingtest.Bookdebit3);
                sqlCmds.Parameters.AddWithValue("@Bookdebit4", accountingtest.Bookdebit4);
                sqlCmds.Parameters.AddWithValue("@Bookcredit1", accountingtest.Bookcredit1);
                sqlCmds.Parameters.AddWithValue("@Bookcredit2", accountingtest.Bookcredit2);
                sqlCmds.Parameters.AddWithValue("@Bookcredit3", accountingtest.Bookcredit3);
                sqlCmds.Parameters.AddWithValue("@Bookcredit4", accountingtest.Bookcredit4);
                sqlCmds.Parameters.AddWithValue("@Balancebook", accountingtest.Balancebook);
                sqlCmds.Parameters.AddWithValue("@Banktitle1", accountingtest.Banktitle1);
                sqlCmds.Parameters.AddWithValue("@Banktitle2", accountingtest.Banktitle2);
                sqlCmds.Parameters.AddWithValue("@Banktitle3", accountingtest.Banktitle3);
                sqlCmds.Parameters.AddWithValue("@Banktitle4", accountingtest.Banktitle4);
                sqlCmds.Parameters.AddWithValue("@Banktitle5", accountingtest.Banktitle5);
                sqlCmds.Parameters.AddWithValue("@Banktitle6", accountingtest.Banktitle6);
                sqlCmds.Parameters.AddWithValue("@Banktitle7", accountingtest.Banktitle7);
                sqlCmds.Parameters.AddWithValue("@Bankdebit1", accountingtest.Bankdebit1);
                sqlCmds.Parameters.AddWithValue("@Bankdebit2", accountingtest.Bankdebit2);
                sqlCmds.Parameters.AddWithValue("@Bankdebit3", accountingtest.Bankdebit3);
                sqlCmds.Parameters.AddWithValue("@Bankdebit4", accountingtest.Bankdebit4);
                sqlCmds.Parameters.AddWithValue("@Bankdebit5", accountingtest.Bankdebit5);
                sqlCmds.Parameters.AddWithValue("@Bankdebit6", accountingtest.Bankdebit6);
                sqlCmds.Parameters.AddWithValue("@Bankdebit7", accountingtest.Bankdebit7);
                sqlCmds.Parameters.AddWithValue("@Bankcredit1", accountingtest.Bankcredit1);
                sqlCmds.Parameters.AddWithValue("@Bankcredit2", accountingtest.Bankcredit2);
                sqlCmds.Parameters.AddWithValue("@Bankcredit3", accountingtest.Bankcredit3);
                sqlCmds.Parameters.AddWithValue("@Bankcredit4", accountingtest.Bankcredit4);
                sqlCmds.Parameters.AddWithValue("@Bankcredit5", accountingtest.Bankcredit5);
                sqlCmds.Parameters.AddWithValue("@Bankcredit6", accountingtest.Bankcredit6);
                sqlCmds.Parameters.AddWithValue("@Bankcredit7", accountingtest.Bankcredit7);
                sqlCmds.Parameters.AddWithValue("@Balancebank", accountingtest.Balancebank);
                sqlCmds.Parameters.AddWithValue("@Asset1", accountingtest.Asset1);
                sqlCmds.Parameters.AddWithValue("@Asset2", accountingtest.Asset2);
                sqlCmds.Parameters.AddWithValue("@Asset3", accountingtest.Asset3);
                sqlCmds.Parameters.AddWithValue("@Asset4", accountingtest.Asset4);
                sqlCmds.Parameters.AddWithValue("@Asset5", accountingtest.Asset5);
                sqlCmds.Parameters.AddWithValue("@Asset6", accountingtest.Asset6);
                sqlCmds.Parameters.AddWithValue("@Asset7", accountingtest.Asset7);
                sqlCmds.Parameters.AddWithValue("@Debitasset1", accountingtest.Debitasset1);
                sqlCmds.Parameters.AddWithValue("@Debitasset2", accountingtest.Debitasset2);
                sqlCmds.Parameters.AddWithValue("@Debitasset3", accountingtest.Debitasset3);
                sqlCmds.Parameters.AddWithValue("@Debitasset4", accountingtest.Debitasset4);
                sqlCmds.Parameters.AddWithValue("@Debitasset5", accountingtest.Debitasset5);
                sqlCmds.Parameters.AddWithValue("@Debitasset6", accountingtest.Debitasset6);
                sqlCmds.Parameters.AddWithValue("@Debitasset7", accountingtest.Debitasset7);
                sqlCmds.Parameters.AddWithValue("@Creditasset1", accountingtest.Creditasset1);
                sqlCmds.Parameters.AddWithValue("@Creditasset2", accountingtest.Creditasset2);
                sqlCmds.Parameters.AddWithValue("@Creditasset3", accountingtest.Creditasset3);
                sqlCmds.Parameters.AddWithValue("@Creditasset4", accountingtest.Creditasset4);
                sqlCmds.Parameters.AddWithValue("@Creditasset5", accountingtest.Creditasset5);
                sqlCmds.Parameters.AddWithValue("@Creditasset6", accountingtest.Creditasset6);
                sqlCmds.Parameters.AddWithValue("@Creditasset7", accountingtest.Creditasset7);
                sqlCmds.Parameters.AddWithValue("@Totalasset", accountingtest.Totalasset);
                sqlCmds.Parameters.AddWithValue("@Equity1", accountingtest.Equity1);
                sqlCmds.Parameters.AddWithValue("@Equity2", accountingtest.Equity2);
                sqlCmds.Parameters.AddWithValue("@Equity3", accountingtest.Equity3);
                sqlCmds.Parameters.AddWithValue("@Equity4", accountingtest.Equity4);
                sqlCmds.Parameters.AddWithValue("@Equity5", accountingtest.Equity5);
                sqlCmds.Parameters.AddWithValue("@Equity6", accountingtest.Equity6);
                sqlCmds.Parameters.AddWithValue("@Equity7", accountingtest.Equity7);
                sqlCmds.Parameters.AddWithValue("@Equity8", accountingtest.Equity8);
                sqlCmds.Parameters.AddWithValue("@Debitequity1", accountingtest.Debitequity1);
                sqlCmds.Parameters.AddWithValue("@Debitequity2", accountingtest.Debitequity2);
                sqlCmds.Parameters.AddWithValue("@Debitequity3", accountingtest.Debitequity3);
                sqlCmds.Parameters.AddWithValue("@Debitequity4", accountingtest.Debitequity4);
                sqlCmds.Parameters.AddWithValue("@Debitequity5", accountingtest.Debitequity5);
                sqlCmds.Parameters.AddWithValue("@Debitequity6", accountingtest.Debitequity6);
                sqlCmds.Parameters.AddWithValue("@Debitequity7", accountingtest.Debitequity7);
                sqlCmds.Parameters.AddWithValue("@Debitequity8", accountingtest.Debitequity8);
                sqlCmds.Parameters.AddWithValue("@Creditequity1", accountingtest.Creditequity1);
                sqlCmds.Parameters.AddWithValue("@Creditequity2", accountingtest.Creditequity2);
                sqlCmds.Parameters.AddWithValue("@Creditequity3", accountingtest.Creditequity3);
                sqlCmds.Parameters.AddWithValue("@Creditequity4", accountingtest.Creditequity4);
                sqlCmds.Parameters.AddWithValue("@Creditequity5", accountingtest.Creditequity5);
                sqlCmds.Parameters.AddWithValue("@Creditequity6", accountingtest.Creditequity6);
                sqlCmds.Parameters.AddWithValue("@Creditequity7", accountingtest.Creditequity7);
                sqlCmds.Parameters.AddWithValue("@Creditequity8", accountingtest.Creditequity8);
                sqlCmds.Parameters.AddWithValue("@Totalequity", accountingtest.Totalequity);

                sqlCmds.ExecuteNonQuery();
                Session["timer"] = null;
            }
            return RedirectToAction("Essay");
        }

        public ActionResult EssayIntro()
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
            string sqlquery = " select Applicant_ExamNo, FirstName, LastName, JobTitle, MasterlistID, Applicant_TookExam from Masterlist where Applicant_ExamNo = @Applicant_ExamNo";
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
                    if (Logins.Rows[i][5].ToString() == "1")
                    {
                        TempData["Message"] = "You already have taken the Exam!";
                        return View();
                    }
                    else { 
                    Session["Applicant_ExamNo"] = Logins.Rows[i][0];
                    Session["FirstName"] = Logins.Rows[i][1];
                    Session["LastName"] = Logins.Rows[i][2];
                    Session["JobTitle"] = Logins.Rows[i][3];
                    Session["MasterlistID"] = Logins.Rows[i][4];
                    }
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