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
    public partial class SellerLogInForm : Form
    {
        public SellerLogInForm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-7GUAQOO\SQLEXPRESS;Initial Catalog=EliteElectronics;Integrated Security=True");

        private void backBtn_Click(object sender, EventArgs e)
        {
            LoginForm log = new LoginForm();
            log.Show();
            this.Hide();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public static string Sellername = "";
        private void LogInBtn_Click(object sender, EventArgs e)
        {
            con.Open();
           SqlDataAdapter sda = new SqlDataAdapter("select count(*) from SellerTb1 where sname = '" + unTb.Text + "' and sPassword = '" + passTb.Text + "'", con);
           DataTable dt = new DataTable();
           sda.Fill(dt);
           con.Close();


            if (dt.Rows[0][0].ToString() == "1" )
            {

                Sellername = unTb.Text;
                BillingForm Obj = new BillingForm();
                Obj.Show();
                this.Hide();
                con.Close();
               
            }

            else { MessageBox.Show("Wrong UserName Or PassWord!!"); }
            con.Close();
        }
    }
}
