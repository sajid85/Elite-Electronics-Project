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

namespace EliteElectronics
{
    public partial class OwnweControlForm : Form
    {
        public OwnweControlForm()
        {
            InitializeComponent();
            Populate();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-7GUAQOO\SQLEXPRESS;Initial Catalog=EliteElectronics;Integrated Security=True");
      
        private void Populate()
        {
            con.Open();
            string query = "select * from ProductsTb1";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductlistDGV.DataSource = ds.Tables[0];
            con.Close();

        }

        private void Filter()
        {
            con.Open();
            string query = "select * from ProductsTb1 where pcatagory ='"+filterCb.SelectedItem.ToString()+"'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductlistDGV.DataSource = ds.Tables[0];
            con.Close();

        }
        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (PtitleTb.Text == "" || PidTb.Text == "" || PbrandTb.Text == "" || PcatCb.SelectedIndex == -1 || PpriceTb.Text == "" || PquantityTb.Text == "") 
            {
                MessageBox.Show("Missing Information !");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "insert into ProductsTb1 values('" + PidTb.Text + "','" + PtitleTb.Text + "','" + PbrandTb.Text + "','" + PcatCb.SelectedItem.ToString() + "','" + PpriceTb.Text + "','" + PquantityTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Saved Successfully !");
                    con.Close();
                    Populate();
                    Reset();
                }catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void filterCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Filter();
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            Populate();
            filterCb.SelectedIndex = -1;
        }

        private void Reset()
        {
            PidTb.Text = "";
            PtitleTb.Text = "";
            PbrandTb.Text = "";
            PcatCb.SelectedIndex = -1;
            PpriceTb.Text = "";
            PquantityTb.Text = "";

        }
        private void resetBtn_Click(object sender, EventArgs e)
        {
            Reset();
            
        }

        int key = 0;
        private void ProductlistDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.PidTb.Text = this.ProductlistDGV.CurrentRow.Cells[0].Value.ToString();
            this.PtitleTb.Text = this.ProductlistDGV.CurrentRow.Cells[1].Value.ToString();
            this.PbrandTb.Text = this.ProductlistDGV.CurrentRow.Cells[2].Value.ToString();
            this.PcatCb.Text = this.ProductlistDGV.CurrentRow.Cells[3].Value.ToString();
            this.PpriceTb.Text = this.ProductlistDGV.CurrentRow.Cells[4].Value.ToString();
            this.PquantityTb.Text = this.ProductlistDGV.CurrentRow.Cells[5].Value.ToString();

            if(PtitleTb.Text =="")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(this.ProductlistDGV.CurrentRow.Cells[0].Value.ToString());
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (PtitleTb.Text == "" || PidTb.Text == "" || PbrandTb.Text == "" || PcatCb.SelectedIndex == -1 || PpriceTb.Text == "" || PquantityTb.Text == "")
            {
                MessageBox.Show("Missing Information !");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "delete from ProductsTb1 where pid="+ key +";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Delete Successfully !");
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

        private void editBtn_Click(object sender, EventArgs e)
        {

            if (PtitleTb.Text == "" || PidTb.Text == "" || PbrandTb.Text == "" || PcatCb.SelectedIndex == -1 || PpriceTb.Text == "" || PquantityTb.Text == "")
            {
                MessageBox.Show("Missing Information !");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "update ProductsTb1 set pid ='"+PidTb.Text+"', ptitle ='"+PtitleTb.Text+"',pbrand='"+PbrandTb.Text+"',pcatagory ='"+PcatCb.SelectedItem.ToString()+"',pprice = '"+PpriceTb.Text+"',pquantity='"+PquantityTb.Text+"' where pid ="+key+";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product update Successfully !");
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

        private void label4_Click(object sender, EventArgs e)
        {
            LoginForm log = new LoginForm();
            log.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            SellersForm log = new SellersForm();
            log.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            DashboardForm log = new DashboardForm();
            log.Show();
            this.Hide();
        }

        private void PpriceTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
