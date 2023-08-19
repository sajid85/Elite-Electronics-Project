using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EliteElectronics
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-7GUAQOO\SQLEXPRESS;Initial Catalog=EliteElectronics;Integrated Security=True");

        private void label4_Click(object sender, EventArgs e)
        {
            LoginForm log = new LoginForm();
            log.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            OwnweControlForm log = new OwnweControlForm();
            log.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            SellersForm log = new SellersForm();
            log.Show();
            this.Hide();
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {

            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select sum(pquantity) from ProductsTb1", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            pstockLb.Text = dt.Rows[0][0].ToString();
            SqlDataAdapter sda1 = new SqlDataAdapter("select sum(amount) from BillTb1", con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            amountLb.Text = dt1.Rows[0][0].ToString();

            SqlDataAdapter sda2 = new SqlDataAdapter("select Count(*) from SellerTb1", con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
           sellLb.Text = dt2.Rows[0][0].ToString();

            con.Close();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
