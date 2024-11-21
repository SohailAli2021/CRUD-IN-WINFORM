using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;

namespace CrudDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ConnectionString);
        void Gridfill()
        {
            SqlCommand cmd = new SqlCommand("SpStd", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Gridfill();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Sp_insert", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@n", txtname.Text);
            cmd.Parameters.AddWithValue("@e", txtemail.Text);
            cmd.Parameters.AddWithValue("@a", txtaddress.Text);
            cmd.Parameters.AddWithValue("@c", txtcontact.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Data inserted!");
            Gridfill();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtname.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtemail.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtaddress.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtcontact.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            SqlCommand cmd = new SqlCommand("SP_update", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@i", txtid.Text);
            cmd.Parameters.AddWithValue("@n", txtname.Text);
            cmd.Parameters.AddWithValue("@e", txtemail.Text);
            cmd.Parameters.AddWithValue("@ad", txtaddress.Text);
            cmd.Parameters.AddWithValue("@c", txtcontact.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Data Updated!");
            txtid.Text = "";
            txtname.Text = "";
            txtemail.Text = "";
            txtaddress.Text = "";
            txtcontact.Text = "";
            Gridfill();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SP_Delete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@i", txtid.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Data Deleted!");
            txtid.Text = "";
            txtname.Text = "";
            txtemail.Text = "";
            txtaddress.Text = "";
            txtcontact.Text = "";
            Gridfill();
        }

        private void serachbyname_TextChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SpByName", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@n", serachbyname.Text);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            con.Close();
        }
    }
}
