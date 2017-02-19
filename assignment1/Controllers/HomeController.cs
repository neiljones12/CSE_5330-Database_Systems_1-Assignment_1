using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace assignment1.Controllers
{
    public class HomeController : Controller
    {
        public string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

        public ActionResult Index()
        {
            //Read data from Department.txt
            var DepartmentContents = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Data/data/Department.txt"));
            var department = DepartmentContents.Replace('\n', ',');
            Char delimiter = ',';
            String[] DepartmentData = department.Split(delimiter);

            //Read data from Dept_locations.txt
            var DepartmentLocationContents = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Data/data/Dept_locations.txt"));
            var departmentLocation = DepartmentLocationContents.Replace('\n', ',');
            String[] DepartmentLocationData = departmentLocation.Split(delimiter);

            //Read data from Employee.txt
            var EmployeeContents = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Data/data/Employee.txt"));
            var employee = EmployeeContents.Replace('\n', ',');
            String[] EmployeeData = employee.Split(delimiter);

            //Read data from Projects.txt
            var ProjectsContents = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Data/data/Projects.txt"));
            var projects = ProjectsContents.Replace('\n', ',');
            String[] ProjectsData = projects.Split(delimiter);

            //Read data from Works_on.txt
            var WorksOnContents = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Data/data/Works_on.txt"));
            var worksOn = WorksOnContents.Replace('\n', ',');
            String[] worksOnData = worksOn.Split(delimiter);

            var query = string.Empty;

            //Check if the department table has data
            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            con.Open();
            using (con)
            {
                SqlCommand cmd = new SqlCommand("Select * from dbo.DEPARTMENT", con);
                SqlDataReader rd = cmd.ExecuteReader();
                if (!rd.HasRows)
                {
                    var iteration = 0;

                    while (iteration < DepartmentData.Length)
                    {

                        var Dname = DepartmentData[iteration];
                        var Dnumber = DepartmentData[iteration + 1];
                        var Mgr_ssn = DepartmentData[iteration + 2];
                        var Mgr_start_date = DepartmentData[iteration + 3];
                        query += "INSERT INTO [dbo].[DEPARTMENT]([Dname],[Dnumber],[Mgr_ssn],[Mgr_start_date]) VALUES(" + Dname + "," + Dnumber + "," + Mgr_ssn + "," + Mgr_start_date + ");";
                        iteration += 4;
                    };
                }

                SqlCommand cmd_depLoc = new SqlCommand("Select * from dbo.DEPT_LOCATIONS", con);
                SqlDataReader rd_depLoc = cmd_depLoc.ExecuteReader();
                if (!rd_depLoc.HasRows)
                {
                    var iteration = 0;

                    while (iteration < DepartmentLocationData.Length)
                    {
                        var Dlocation = DepartmentLocationData[iteration];
                        var Dnumber = DepartmentLocationData[iteration + 1];
                        query += "INSERT INTO [dbo].[DEPT_LOCATIONS]([Dnumber],[Dlocation]) VALUES(" + Dnumber + "," + Dlocation + ");";
                        iteration += 2;
                    };

                }

                SqlCommand cmd_emp = new SqlCommand("Select * from dbo.EMPLOYEE", con);
                SqlDataReader rd_emp = cmd_emp.ExecuteReader();
                if (!rd_emp.HasRows)
                {
                    var iteration = 0;

                    while (iteration + 2 < EmployeeData.Length)
                    {
                        var Fname = EmployeeData[iteration];
                        var Minit = EmployeeData[iteration + 1];
                        var Lname = EmployeeData[iteration + 2];
                        var Ssn = EmployeeData[iteration + 3];
                        var Bdate = EmployeeData[iteration + 4];
                        var Address = EmployeeData[iteration + 5];
                        var sex = "";
                        var Salary = "";
                        var Super_ssn = "";
                        var Dno = "";
                        if (EmployeeData[iteration + 6].Length > 1)
                        {
                            Address += EmployeeData[iteration + 6];
                            if(EmployeeData[iteration + 7].Length > 1)
                            {
                                Address += EmployeeData[iteration + 7];
                                sex = EmployeeData[iteration + 8];
                                Salary = EmployeeData[iteration + 9];
                                Super_ssn = EmployeeData[iteration + 10];
                                Dno = EmployeeData[iteration + 11];
                                iteration += 12;
                            }
                            else
                            {
                                sex = EmployeeData[iteration + 7];
                                Salary = EmployeeData[iteration + 8];
                                Super_ssn = EmployeeData[iteration + 9];
                                Dno = EmployeeData[iteration + 10];
                                iteration += 11;
                            }
                        }
                        else
                        {
                            sex = EmployeeData[iteration + 6];
                            Salary = EmployeeData[iteration + 7];
                            Super_ssn = EmployeeData[iteration + 8];
                            Dno = EmployeeData[iteration + 9];
                            iteration += 10;
                        }

                        query += "INSERT INTO [dbo].[EMPLOYEE]([Fname],[Minit],[Lname],[Ssn],[Bdate],[Address],[Sex],[Salary],[Super_ssn],[Dno])VALUES(" + Fname + "," + Minit + "," + Lname + "," + Ssn + "," + Bdate + "," + Address + "," + sex + "," + Salary + "," + Super_ssn + "," + Dno + "); ";

                    };

                }

                SqlCommand cmd_proj = new SqlCommand("Select * from dbo.PROJECT", con);
                SqlDataReader rd_proj = cmd_proj.ExecuteReader();
                if (!rd_proj.HasRows)
                {
                    var iteration = 0;

                    while (iteration < ProjectsData.Length)
                    {
                        var Pname = ProjectsData[iteration];
                        var Pnumber = ProjectsData[iteration + 1];
                        var Plocation = ProjectsData[iteration + 2];
                        var Dnum = ProjectsData[iteration + 3];
                        query += "INSERT INTO [dbo].[PROJECT]([Pname],[Pnumber],[Plocation],[Dnum]) VALUES(" + Pname + "," + Pnumber + "," + Plocation + "," + Dnum + ");";
                        iteration += 4;
                    };

                }

                SqlCommand cmd_works = new SqlCommand("Select * from dbo.WORKS_ON", con);
                SqlDataReader rd_works = cmd_works.ExecuteReader();
                if (!rd_works.HasRows)
                {
                    var iteration = 0;

                    while (iteration < worksOnData.Length)
                    {
                        var Essn = worksOnData[iteration];
                        var Pno = worksOnData[iteration + 1];
                        var Hours = worksOnData[iteration + 2];
                        query += "INSERT INTO [dbo].[WORKS_ON]([Essn],[Pno],[Hours]) VALUES(" + Essn + "," + Pno + "," + Hours + ");";
                        iteration += 3;
                    };

                }

                if (query != string.Empty)
                {
                    SqlCommand insert = new SqlCommand(query, con);
                    insert.ExecuteNonQuery();
                }
            }
            // -- End of data insertion --



            return View();
        }

    }
}