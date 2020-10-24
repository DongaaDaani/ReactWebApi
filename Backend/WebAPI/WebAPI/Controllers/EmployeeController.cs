using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace WebAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage get()
        {
            DataTable table = new DataTable();
            string querry = @"select EmployeeID,EmployeeName,Department,MailID, convert(varchar(10),DOJ,120) as DOJ from dbo.Employee";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString)) 
            using (var cmd = new SqlCommand(querry, con))
                using(var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public String Post(Employee emp)
        {
            DateTime time = emp.DOJ;
            string format = "yyyy-MM-dd HH:mm:ss";

            try
            {
                DataTable table = new DataTable();
                string querry = @"insert into dbo.Employee (EmployeeName,Department,MailID,DOJ) values
                (
                '" + emp.EmployeeName + @"'  
                , '" + emp.Department + @"'  
                , '" + emp.MailID + @"'  
                , '" + time.ToString(format) + @"' 
                )
                ";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(querry, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }


                return "Added Succesfully";
            }
            catch (Exception ex)
            {

                return "Failed to Add";
            }

        }

        public String Put(Employee emp)
        {
            DateTime time = emp.DOJ;
            string format = "yyyy-MM-dd HH:mm:ss";
            try
            {
                DataTable table = new DataTable();
                string querry = @"update dbo.Employee set 
                    EmployeeName = '" + emp.EmployeeName + @"' 
                   , Department = '" + emp.Department + @"'
                ,   MailID = '" + emp.MailID + @"'
                  ,  DOJ = '" + time.ToString(format) + @"'  



            where  EmployeeID =" + emp.EmployeeID + @"";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(querry, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }


                return "Update Succesfully";
            }
            catch (Exception)
            {

                return "Failed to Update";
            }

        }

        public String Delete(int id)
        {
            try
            {
                DataTable table = new DataTable();
                string querry = @"delete from dbo.Employee where EmployeeID = " + id;

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(querry, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }


                return "Delete Succesfully";
            }
            catch (Exception)
            {

                return "Failed to Delete";
            }

        }

    }
}
