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
                _command.CommandText = "[dbo].[sp_Get_Employees]"; //pass the sp
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

        public bool Insert(Employee model)
        {
            int id = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType= CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[sp_Insert_Employee]";
                _command.Parameters.AddWithValue("@FirstName",model.FirstName);
                _command.Parameters.AddWithValue("@LastName",model.LastName);
                _command.Parameters.AddWithValue("@DateOfBirth", model.DateOfBirth);
                _command.Parameters.AddWithValue("@Email",model.Email);
                _command.Parameters.AddWithValue("@Salary",model.Salary);
                _connection.Open();
                id = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return id > 0 ? true : false;
        }
    }
}
