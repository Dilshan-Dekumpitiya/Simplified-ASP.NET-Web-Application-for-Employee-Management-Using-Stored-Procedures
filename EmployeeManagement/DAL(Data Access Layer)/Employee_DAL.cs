using EmployeeManagement.Models;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;

namespace EmployeeManagement.DAL_Data_Access_Layer_
{
    public class Employee_DAL
    {
        SqlConnection _connection = null;
        SqlCommand _command = null; //Can pass the sp using _command.CommandText

        public static IConfiguration Configuration { get; set; }

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory
                ()).AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            return Configuration.GetConnectionString("DefaultConnection");
        }

        public List<Employee> GetAll()
        {
            List<Employee> employeeList = new List<Employee>();
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[dbo].[sp_Get_Employees]";
                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();

                while (dr.Read())
                {
                    Employee employee = new Employee();
                    employee.Id = Convert.ToInt32(dr["Id"]);
                    employee.FirstName = dr["FirstName"].ToString();
                    employee.LastName = dr["LastName"].ToString();
                    employee.DateOfBirth = Convert.ToDateTime(dr["DateofBirth"]);
                    employee.Email = dr["Email"].ToString();
                    employee.Salary =Convert.ToDouble(dr["Salary"]);
                    employeeList.Add(employee);
                }
                _connection.Close();
            }
            return employeeList;
        }
    }
}
