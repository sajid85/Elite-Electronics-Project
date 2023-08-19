using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EliteElectronics
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OwnerLogInForm olog = new OwnerLogInForm();
            olog.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SellerLogInForm olog = new SellerLogInForm();
            olog.Show();
            this.Hide();
        }
    }
}
