using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString =
                @"Data Source=DESKTOP-4EDFUSD\SQLEXPRESS;
                  Initial Catalog=DB;
                  Integrated Security=True;
                  TrustServerCertificate=True;";

            try
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();
                MessageBox.Show("Connection Successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Failed: " + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
