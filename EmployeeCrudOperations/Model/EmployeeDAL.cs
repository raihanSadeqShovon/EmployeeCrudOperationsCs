using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace EmployeeCrudOperations.Model
{
    public class EmployeeDAL
    {
        private readonly string cs = ConfigurationManager.ConnectionStrings["EmpConnection"].ConnectionString;

        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            using (SqlConnection con = new SqlConnection(cs)) 
            {
                SqlCommand cmd = new SqlCommand("SELECT * from Employee", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Employee emp = new Employee
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        EmpName = reader["EmpName"].ToString(),
                        Position = reader["Position"].ToString(),
                        Salary = Convert.ToDecimal(reader["Salary"]),
                        ImageUrl = reader["ImageUrl"]?.ToString()
                    };
                    employees.Add(emp);
                }
            }
            return employees;
        }
        //Create Method
        public int AddEmp(Employee emp)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Employee (EmpName, Position, Salary, ImageUrl) VALUES (@EmpName, @Position, @Salary, @ImageUrl)", con);
                cmd.Parameters.AddWithValue("@EmpName", emp.EmpName);
                cmd.Parameters.AddWithValue("@Position", emp.Position);
                cmd.Parameters.AddWithValue("@Salary", emp.Salary);
                cmd.Parameters.AddWithValue("@ImageUrl", emp.ImageUrl ?? (object)DBNull.Value); // Handle null value
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        //Update Method
        public void UpdateEmp(Employee emp)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Update Employee set EmpName = @EmpName, Position = @Position, Salary = @Salary, ImageUrl = @imageUrl where Id = @Id", con);
                cmd.Parameters.AddWithValue("@EmpName", emp.EmpName);
                cmd.Parameters.AddWithValue("@Position", emp.Position);
                cmd.Parameters.AddWithValue("@Salary", emp.Salary);
                cmd.Parameters.AddWithValue("@imageUrl", emp.ImageUrl);
                cmd.Parameters.AddWithValue("@Id", emp.Id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //Delete Method
        public void DeleteEmp(int empId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Employee WHERE Id = @Id; ", con);
                cmd.Parameters.AddWithValue("@Id", empId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //public int AddEmp(Employee emp)
        //{
        //    using (SqlConnection con = new SqlConnection(cs))
        //    {
        //        SqlCommand cmd = new SqlCommand("INSERT INTO Employee (EmpName, Position, Salary, ImageUrl) VALUES (@EmpName, @Position, @Salary, @ImageUrl)", con);
        //        cmd.Parameters.AddWithValue("@EmpName", emp.EmpName);
        //        cmd.Parameters.AddWithValue("@Position", emp.Position);
        //        cmd.Parameters.AddWithValue("@Salary", emp.Salary);
        //        cmd.Parameters.AddWithValue("@ImageUrl", emp.ImageUrl); // Save the ImageUrl
        //        con.Open();
        //        return Convert.ToInt32(cmd.ExecuteScalar());
        //    }
        //}


        //Get data by Id
        //public Employee GetEmpById(int empId)
        //{
        //    Employee emp = null;    
        //    using (SqlConnection con = new SqlConnection(_conncectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("Select * from Employee where Id = @Id", con);
        //        cmd.Parameters.AddWithValue("@Id", empId);
        //        con.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();
        //        if (reader.Read())
        //        {   
        //            emp = new Employee();
        //            emp.Id = Convert.ToInt32(reader["Id"]);
        //            emp.EmpName = reader["EmpName"].ToString();
        //            emp.Position = reader["Position"].ToString();
        //            emp.Salary = Convert.ToDecimal(reader["Salary"]);
        //        }
        //    }
        //    return emp;
        //}
        //public Employee GetEmpById(int empId)
        //{
        //    Employee emp = null;
        //    using (SqlConnection con = new SqlConnection(_conncectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("Select * from Employee where Id = @Id", con);
        //        cmd.Parameters.AddWithValue("@Id", empId);
        //        con.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();
        //        if (reader.Read())
        //        {
        //            emp = new Employee();
        //            emp.Id = Convert.ToInt32(reader["Id"]);
        //            emp.EmpName = reader["EmpName"].ToString();
        //            emp.Position = reader["Position"].ToString();
        //            emp.Salary = Convert.ToDecimal(reader["Salary"]);
        //            emp.ImagePath = reader["ImagePath"]?.ToString(); // Add this line if you use ImagePath
        //        }
        //    }
        //    return emp;
        //}

        public Employee GetEmpById(int empId)
        {
            Employee emp = null;
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("Select * from Employee where Id = @Id", con);
                    cmd.Parameters.AddWithValue("@Id", empId);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        emp = new Employee
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            EmpName = reader["EmpName"].ToString(),
                            Position = reader["Position"].ToString(),
                            Salary = Convert.ToDecimal(reader["Salary"]),
                            ImageUrl = reader["ImagePath"]?.ToString() // Ensure this line matches the database
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error message
                throw new Exception("Error in GetEmpById: " + ex.Message);
            }
            return emp;
        }

    }
}