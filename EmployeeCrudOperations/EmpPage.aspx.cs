using EmployeeCrudOperations.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeCrudOperations
{
    public partial class EmpPage : System.Web.UI.Page
    {
        String cs = ConfigurationManager.ConnectionStrings["EmpConnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindView();
            }
        }

        private void BindView()
        {
            EmployeeDAL empDAL = new EmployeeDAL();
            List<Employee> employees = empDAL.GetEmployees();

            GridViewEmployees.DataSource = employees;
            GridViewEmployees.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string imageUrl = UploadImage();
            Employee emp = new Employee
            {
                EmpName = txtEmpName.Text,
                Position = txtPosition.Text,
                Salary = Convert.ToDecimal(txtSalary.Text),
                ImageUrl = imageUrl // Add image path
            };
            EmployeeDAL empDAL = new EmployeeDAL();
            empDAL.AddEmp(emp);
            BindView();
            //Clear();
        }

        private string UploadImage()
        {
            if (FileUploadImage.HasFile)
            {
                string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(FileUploadImage.FileName);
                string filePath = "~/Uploads/" + fileName;
                string serverPath = Server.MapPath(filePath);

                //Ensure directory exists
                string folderPath = System.IO.Path.GetDirectoryName(serverPath);
                if (!System.IO.Directory.Exists(folderPath))
                {
                    System.IO.Directory.CreateDirectory(folderPath);
                }

                //Save file

                FileUploadImage.SaveAs(serverPath);
                return filePath;
            }
            return null;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int empId = Convert.ToInt32(txtId.Text);
            string imageUrl = UploadImage(); //Handle image upload for update, if any
            Employee emp = new Employee
            {
                Id = empId,
                EmpName = txtEmpName.Text,
                Position = txtPosition.Text,
                Salary = Convert.ToDecimal(txtSalary.Text),
                ImageUrl = imageUrl //Update image URL if a new file is uploaded
            };
            EmployeeDAL empDAL = new EmployeeDAL();
            empDAL.UpdateEmp(emp);
            BindView();
            //Clear();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int empId = Convert.ToInt32(txtId.Text);

            EmployeeDAL empDAL = new EmployeeDAL();
            empDAL.DeleteEmp(empId);
            BindView();
            //Clear();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (FileUploadImage.HasFile)
            {
                try
                {
                    // Get the file name
                    string fileName = System.IO.Path.GetFileName(FileUploadImage.PostedFile.FileName);
                    // Define the path where the file will be saved
                    string folderPath = Server.MapPath("~/Images/");
                    // Ensure the directory exists
                    if (!System.IO.Directory.Exists(folderPath))
                    {
                        System.IO.Directory.CreateDirectory(folderPath);
                    }
                    // Combine folder path with the file name
                    string filePath = folderPath + fileName;
                    // Save the file
                    FileUploadImage.SaveAs(filePath);

                    // Update the database with the image URL
                    string imageUrl = "~/Images/" + fileName;
                    Employee emp = new Employee
                    {
                        EmpName = txtEmpName.Text,
                        Position = txtPosition.Text,
                        Salary = Convert.ToDecimal(txtSalary.Text),
                        ImageUrl = imageUrl // Save the relative URL to the database
                    };

                    EmployeeDAL empDAL = new EmployeeDAL();
                    empDAL.AddEmp(emp); // Call the AddEmp method to save the employee details along with the image URL

                    BindView(); // Refresh the GridView to show the updated data
                }
                catch (Exception ex)
                {
                    // Handle the error
                    lblMessage.Text = "Error: " + ex.Message;
                }
            }
            else
            {
                lblMessage.Text = "Please select a file to upload.";
            }
        }


        //public void DeleteEmp(int empId)
        //{
        //    // Get image path before deleting
        //    Employee emp = GetEmpById(empId);
        //    if (emp != null && !string.IsNullOrEmpty(emp.ImagePath))
        //    {
        //        string filePath = Server.MapPath(emp.ImagePath);
        //        if (System.IO.File.Exists(filePath))
        //        {
        //            System.IO.File.Delete(filePath); // Delete the file from server
        //        }
        //    }

        public void DeleteEmp(int empId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Employee WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", empId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
            {
                int empId = Convert.ToInt32(txtId.Text);

                EmployeeDAL empDAL = new EmployeeDAL();
                Employee emp = empDAL.GetEmpById(empId);

                if (emp != null && !string.IsNullOrEmpty(emp.ImageUrl))
                {
                    string filePath = Server.MapPath(emp.ImageUrl); // Use Server.MapPath in the code-behind
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath); // Delete the file from the server
                    }
                }

                // Now delete the employee record from the database
                empDAL.DeleteEmp(empId);
                BindView();
            }

        //    using (SqlConnection con = new SqlConnection(cs))
        //    {
        //        SqlCommand cmd = new SqlCommand("DELETE FROM Employee WHERE Id = @Id", con);
        //        cmd.Parameters.AddWithValue("@Id", empId);
        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //    }
        //}


        //protected void txtId_TextChanged(object sender, EventArgs e)
        //{
        //    int empId = Convert.ToInt32(txtId.Text);
        //    EmployeeDAL empDAL = new EmployeeDAL();
        //    Employee employee = empDAL.GetEmpById(empId);
        //    if (employee != null)
        //    {
        //        txtEmpName.Text = employee.EmpName;
        //        txtPosition.Text = employee.Position;
        //        txtSalary.Text = Convert.ToDecimal(employee.Salary).ToString();
        //        txtId.Text = Convert.ToInt32(employee.Id).ToString();
        //    }
        //    else
        //    {
        //        txtEmpName.Text = "";
        //        txtPosition.Text = "";
        //        txtSalary.Text = "";
        //    }
        //}
        //public void Clear()
        //{
        //    txtId.Text = "";
        //    txtEmpName.Text = "";
        //    txtPosition.Text = "";
        //    txtSalary.Text = "".ToString();
        //}
    }
}