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
    public class DepartmentController : ApiController
    {
        public HttpResponseMessage get()
        {
            DataTable table = new DataTable();
            string querry = @"select DepartmentID,DepartmentName from dbo.Departments";

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(querry, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public String Post(Department dep)
        {
            try
            {
                DataTable table = new DataTable();
                string querry = @"insert into dbo.Departments values('" + dep.DepartmentName + @"')";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(querry, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }


                return "Added Succesfully";
            }
            catch (Exception)
            {

                return "Failed to Add";
            }

        }


        public String Put(Department dep)
        {
            try
            {
                DataTable table = new DataTable();
                string querry = @"update dbo.Departments set DepartmentName = '" + dep.DepartmentName + @"' where  DepartmentID =" + dep.DepartmentId +@"";

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
                string querry = @"delete from dbo.Departments where DepartmentID = " + id;

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
