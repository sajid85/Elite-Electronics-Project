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
    public partial class OwnerLogInForm : Form
    {
        public OwnerLogInForm()
        {
            InitializeComponent();
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            LoginForm log = new LoginForm();
            log.Show();
            this.Hide();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {

            string user = unTb.Text;
            string pass = passTb.Text;

            if (pass == "1234" && user == "Bipul")
            {

                OwnweControlForm ad = new OwnweControlForm();
                ad.Show();
                this.Hide();



            }
            else if (pass != "1234" && user == "Bipul")
            {
                MessageBox.Show("Admin Login Error !!");
            }
        }
    }
}
