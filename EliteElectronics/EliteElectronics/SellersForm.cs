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
    public partial class SellersForm : Form
    {
        public SellersForm()
        {
            InitializeComponent();
            Populate();
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-7GUAQOO\SQLEXPRESS;Initial Catalog=EliteElectronics;Integrated Security=True");

        private void Populate()
        {
            con.Open();
            string query = "select * from SellerTb1";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            sellerlistDGV.DataSource = ds.Tables[0];
            con.Close();

        }


        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (sIdTb.Text == "" || sNameTb.Text == "" || sAddTb.Text == "" || sPhoneTb.Text == "" || sPassTb.Text == "")
            {
                MessageBox.Show("Missing Information !");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "insert into SellerTb1 values('" + sIdTb.Text + "','" + sNameTb.Text + "','" + sAddTb.Text + "','" + sPhoneTb.Text + "','" + sPassTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Saved Successfully !");
                    con.Close();
                    Populate();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void Reset()
        {
            sIdTb.Text = "";
            sNameTb.Text = "";
            sAddTb.Text = "";
            sPhoneTb.Text = "";
            sPassTb.Text = "";
        }


        private void resetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        int key = 0;
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (sIdTb.Text == "" || sNameTb.Text == "" || sAddTb.Text == "" || sPhoneTb.Text == "" || sPassTb.Text == "")
            {
                MessageBox.Show("Missing Information !");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "delete from SellerTb1 where sid=" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Delete Successfully !");
                    con.Close();
                    Populate();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void sellerlistDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
       {
              this.sIdTb.Text = this.sellerlistDGV.CurrentRow.Cells[0].Value.ToString();
                this.sNameTb.Text = this.sellerlistDGV.CurrentRow.Cells[1].Value.ToString();
                this.sAddTb.Text = this.sellerlistDGV.CurrentRow.Cells[2].Value.ToString();
                this.sPhoneTb.Text = this.sellerlistDGV.CurrentRow.Cells[3].Value.ToString();
            this.sPassTb.Text = this.sellerlistDGV.CurrentRow.Cells[4].Value.ToString();

                if (sIdTb.Text == "")
                {
                    key = 0;
                }
                else
                {
                    key = Convert.ToInt32(this.sellerlistDGV.CurrentRow.Cells[0].Value.ToString());
                }
            
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            if (sIdTb.Text == "" || sNameTb.Text == "" || sAddTb.Text == "" || sPhoneTb.Text == "" || sPassTb.Text == "")
            { 
                MessageBox.Show("Missing Information !");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "update SellerTb1 set sid ='" + sIdTb.Text + "', sname ='" + sNameTb.Text + "',saddress='" + sAddTb.Text + "',sphone = '" + sPhoneTb.Text + "',spassword='" + sPassTb.Text + "' where sid =" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller update Successfully !");
                    con.Close();
                    Populate();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            Populate();
        }

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

        private void label3_Click(object sender, EventArgs e)
        {
            DashboardForm log = new DashboardForm();
            log.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
   

}

