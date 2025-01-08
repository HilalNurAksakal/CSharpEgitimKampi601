using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpEgitimKampi601
{
    public partial class FrmCustomer : Form
    {
        public FrmCustomer()
        {
            InitializeComponent();
        }

        string connectionString = "Server=localhost;port=5432;Database=CustomerDb;" +
            "user Id=postgres;Password=1";

        void GetAllCustomers()
        {
            var connection=new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "Select * From Customers";
            var command = new NpgsqlCommand(query, connection);
            var adapter=new NpgsqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }
        private void btnCustomerList_Click(object sender, EventArgs e)
        {
            GetAllCustomers();
        }

        private void btnCustomerCreate_Click(object sender, EventArgs e)
        {
            string customerName=txtCustomerName.Text;
            string customerCity=txtCustomerCity.Text;
            string customerSurname=txtCustomerSurname.Text;
            var connection= new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "insert into Customers (CustomerName,CustomerSurname,CustomerCity) values" +
                "(@customerName,@customerSurname,@customerCity)";
            var command= new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@customerName", customerName);
            command.Parameters.AddWithValue("@customerSurname", customerSurname);
            command.Parameters.AddWithValue("@customerCity", customerCity);
            command.ExecuteNonQuery();
            MessageBox.Show("Kişi eklendi");
            connection.Close();
            GetAllCustomers();

        }

        private void btnCustomerDelete_Click(object sender, EventArgs e)
        {
            int id=int.Parse(txtCustomerId.Text);
            var connection=new NpgsqlConnection(connectionString);
            connection.Open();
            string query="Delete From Customers where CustomerId="+ id +"";
            var command=new NpgsqlCommand(query, connection);
            command.ExecuteNonQuery();
            MessageBox.Show("Kişi silindi");
            connection.Close();
            GetAllCustomers();
        }

        private void btnCustomerUpdate_Click(object sender, EventArgs e)
        {
            string customerName = txtCustomerName.Text;
            string customerCity = txtCustomerCity.Text;
            string customerSurname = txtCustomerSurname.Text;
            int id = int.Parse(txtCustomerId.Text);
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "Update Customers set CustomerName='" + customerName + "', CustomerSurname='" + customerSurname + "'," +
                "CustomerCity='" + customerCity + "'where CustomerId=" + id ;
            var command= new NpgsqlCommand(query, connection);
            command.ExecuteNonQuery();
            MessageBox.Show("Kişi güncellendi");
            connection.Close();
            GetAllCustomers();
        }

        private void btnGetByCustomerId_Click(object sender, EventArgs e)
        {

        }
    }
}
