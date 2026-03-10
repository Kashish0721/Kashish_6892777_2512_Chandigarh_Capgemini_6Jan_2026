using System;
using System.Data;
using System.Data.SqlClient;   // ✅ Correct Namespace
using System.Windows.Forms;
using System.Xml.Linq;

namespace EmployeeManagementSystem
{
    public partial class Form1 : Form
    {
        // ✅ Correct Connection String
        string connectionString =
        @"Data Source=DESKTOP-4EDFUSD\SQLEXPRESS;
          Initial Catalog=CompanyDB;
          Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        // ==============================
        // FORM LOAD
        // ==============================
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDepartments();
            LoadEmployees();
        }

        // ==============================
        // LOAD DEPARTMENTS
        // ==============================
        private void LoadDepartments()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(
                        "SELECT DepartmentID, DepartmentName FROM Departments", con);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cmbDepartment.DataSource = dt;
                    cmbDepartment.DisplayMember = "DepartmentName";
                    cmbDepartment.ValueMember = "DepartmentID";
                    cmbDepartment.SelectedIndex = -1; // No default select
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Department Load Error: " + ex.Message);
            }
        }

        // ==============================
        // LOAD EMPLOYEES
        // ==============================
        private void LoadEmployees()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter("GetEmployees", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Employee Load Error: " + ex.Message);
            }
        }

        // ==============================
        // ADD EMPLOYEE
        // ==============================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "" || txtEmail.Text == "" ||
                    txtSalary.Text == "" || cmbDepartment.SelectedIndex == -1)
                {
                    MessageBox.Show("Please fill all fields!");
                    return;
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("AddEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@DepartmentID",
                        Convert.ToInt32(cmbDepartment.SelectedValue));
                    cmd.Parameters.AddWithValue("@Salary",
                        Convert.ToDecimal(txtSalary.Text));

                    con.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Employee Added Successfully!");
                }

                LoadEmployees();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add Error: " + ex.Message);
            }
        }

        // ==============================
        // UPDATE EMPLOYEE
        // ==============================
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("UpdateEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmployeeID",
                        Convert.ToInt32(txtID.Text));
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@DepartmentID",
                        Convert.ToInt32(cmbDepartment.SelectedValue));
                    cmd.Parameters.AddWithValue("@Salary",
                        Convert.ToDecimal(txtSalary.Text));

                    con.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Employee Updated Successfully!");
                }

                LoadEmployees();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update Error: " + ex.Message);
            }
        }

        // ==============================
        // DELETE EMPLOYEE
        // ==============================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("DeleteEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmployeeID",
                        Convert.ToInt32(txtID.Text));

                    con.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Employee Deleted Successfully!");
                }

                LoadEmployees();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete Error: " + ex.Message);
            }
        }

        // ==============================
        // SEARCH EMPLOYEE
        // ==============================
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SearchEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Keyword", txtSearch.Text);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search Error: " + ex.Message);
            }
        }

        // ==============================
        // DATAGRID CLICK
        // ==============================
        private void dataGridView1_CellClick(object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row =
                    dataGridView1.Rows[e.RowIndex];

                txtID.Text = row.Cells["EmployeeID"].Value.ToString();
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtSalary.Text = row.Cells["Salary"].Value.ToString();

                // ✅ Correct Way (important)
                cmbDepartment.SelectedValue =
                    row.Cells["DepartmentID"].Value;
            }
        }

        // ==============================
        // CLEAR FIELDS
        // ==============================
        private void ClearFields()
        {
            txtID.Clear();
            txtName.Clear();
            txtEmail.Clear();
            txtSalary.Clear();
            txtSearch.Clear();
            cmbDepartment.SelectedIndex = -1;
        }
    }
}